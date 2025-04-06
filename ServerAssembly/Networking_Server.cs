using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Valve.Sockets;

namespace Valve
{
    public class Networking
    {
        static private Valve.Sockets.NetworkingIdentity identity;
        static private Valve.Sockets.NetworkingSockets server = null;
        static private Valve.Sockets.NetworkingMessage netMessage;

        public Networking()
        {
            netMessage = new Valve.Sockets.NetworkingMessage();
        }

        static public void CreateNetworkingServer()
        {
            server = new Valve.Sockets.NetworkingSockets();

            uint pollGroup = server.CreatePollGroup();

            Valve.Sockets.StatusCallback status = (ref Valve.Sockets.StatusInfo info) =>
            {
                switch (info.connectionInfo.state)
                {
                    case Valve.Sockets.ConnectionState.None:
                        break;

                    case Valve.Sockets.ConnectionState.Connecting:
                        server.AcceptConnection(info.connection);
                        server.SetConnectionPollGroup(pollGroup, info.connection);
                        break;

                    case Valve.Sockets.ConnectionState.Connected:
                        Console.WriteLine("Client connected - ID: " + info.connection + ", IP: " + info.connectionInfo.address.GetIP());
                        break;

                    case Valve.Sockets.ConnectionState.ClosedByPeer:
                    case Valve.Sockets.ConnectionState.ProblemDetectedLocally:
                        server.CloseConnection(info.connection);
                        Console.WriteLine("Client disconnected - ID: " + info.connection + ", IP: " + info.connectionInfo.address.GetIP());
                        break;
                }
            };

            Valve.Sockets.NetworkingUtils utils = new Valve.Sockets.NetworkingUtils();
            utils.SetStatusCallback(status);

            Valve.Sockets.Address address = new Valve.Sockets.Address();

            address.SetAddress(Valve.Networking.Get_Local_IPAddress(), 3074);

            uint listenSocket = server.CreateListenSocket(ref address);

#if VALVESOCKETS_SPAN
	        MessageCallback message = (in NetworkingMessage netMessage) => {
		        Console.WriteLine("Message received from - ID: " + netMessage.connection + ", Channel ID: " + netMessage.channel + ", Data length: " + netMessage.length);
	        };
#else
            const int maxMessages = 20;

            Valve.Sockets.NetworkingMessage[] netMessages = new Valve.Sockets.NetworkingMessage[maxMessages];
#endif

            while (!Console.KeyAvailable)
            {
                server.RunCallbacks();

#if VALVESOCKETS_SPAN
		        server.ReceiveMessagesOnPollGroup(pollGroup, message, 20);
#else
                int netMessagesCount = server.ReceiveMessagesOnPollGroup(pollGroup, netMessages, maxMessages);

                if (netMessagesCount > 0)
                {
                    for (int i = 0; i < netMessagesCount; i++)
                    {
                        ref Valve.Sockets.NetworkingMessage netMessage = ref netMessages[i];

                        Console.WriteLine("Message received from - ID: " + netMessage.connection + ", Channel ID: " + netMessage.channel + ", Data length: " + netMessage.length);

                        netMessage.Destroy();
                    }
                }
#endif

                Thread.Sleep(15);
            }
            server.DestroyPollGroup(pollGroup);
        }

        public static void CreateAndSendNewMessage()
        {
            byte[] data = new byte[64];

            Address address = new Address();
            address.SetAddress("192.168.8.2", 3074);//todo
            uint connection = server.Connect(ref address);

            server.SendMessageToConnection(connection, data);
        }

        public static void CopyPayloadFromMessage()
        {
            byte[] buffer = new byte[1024];
            netMessage.CopyTo(buffer);

            Florence.Server_IO.PraiseEvents.Set_PraiseEventId(buffer[0]);
            switch (buffer[0])
            {
                case 0:
                   
                    
                    break;

                case 1:
                    //ToDo
                    break;

                default:
                    break;
            }
        }

        public static void SetA_HookForDebugInformation()
        {
            Valve.Sockets.DebugCallback debug = (type, message) =>
            {
                Console.WriteLine("Debug - Type: " + type + ", Message: " + message);
            };

            Valve.Sockets.NetworkingUtils utils = new Valve.Sockets.NetworkingUtils();

            utils.SetDebugCallback(Valve.Sockets.DebugType.Everything, debug);
        }

        private static string Get_Local_IPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
