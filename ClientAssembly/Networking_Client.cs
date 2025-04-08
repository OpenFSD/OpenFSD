using System;
using System.Collections;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Valve.Sockets;


namespace Valve
{
    public class Networking
    {
        static private Valve.Sockets.NetworkingIdentity identity;
        static private NetworkingSockets client = null;
        static private NetworkingMessage netMessage;
        
        public Networking()
        {
            netMessage = new NetworkingMessage();
        }
 
        static public void CreateNetworkingClient()
        {
            client = new NetworkingSockets();

            uint connection = 0;

            StatusCallback status = (ref StatusInfo info) => {
                switch (info.connectionInfo.state)
                {
                    case ConnectionState.None:
                        break;

                    case ConnectionState.Connected:
                        Console.WriteLine("Client connected to server - ID: " + connection);
                        break;

                    case ConnectionState.ClosedByPeer:
                    case ConnectionState.ProblemDetectedLocally:
                        client.CloseConnection(connection);
                        Console.WriteLine("Client disconnected from server");
                        break;
                }
            };

            Valve.Sockets.NetworkingUtils utils = new Valve.Sockets.NetworkingUtils();
            utils.SetStatusCallback(status);

            Address address = new Address();

            address.SetAddress(Valve.Networking.Get_Local_IPAddress(), 3074);

            connection = client.Connect(ref address);

#if VALVESOCKETS_SPAN
	MessageCallback message = (in NetworkingMessage netMessage) => {
		Console.WriteLine("Message received from server - Channel ID: " + netMessage.channel + ", Data length: " + netMessage.length);
	};
#else
            const int maxMessages = 20;

            NetworkingMessage[] netMessages = new NetworkingMessage[maxMessages];
#endif

            while (!Console.KeyAvailable)
            {
                client.RunCallbacks();

#if VALVESOCKETS_SPAN
		client.ReceiveMessagesOnConnection(connection, message, 20);
#else
                int netMessagesCount = client.ReceiveMessagesOnConnection(connection, netMessages, maxMessages);

                if (netMessagesCount > 0)
                {
                    for (int i = 0; i < netMessagesCount; i++)
                    {
                        ref NetworkingMessage netMessage = ref netMessages[i];

                        Console.WriteLine("Message received from server - Channel ID: " + netMessage.channel + ", Data length: " + netMessage.length);

                        netMessage.Destroy();
                    }
                }
#endif

                Thread.Sleep(15);
            }
        }

        public static void CreateAndSendNewMessage(byte praiseEventId)
        {
            Console.WriteLine("entered => CreateAndSendNewMessage()");//ToDo
            byte[] data = new byte[64];
            data[0] = praiseEventId;
            byte[] bytes;
            switch (praiseEventId)
            {
            case 0:
                Florence.ClientAssembly.Praise_Files.Praise0_Input input_subset_Praise0 = (Florence.ClientAssembly.Praise_Files.Praise0_Input)Florence.ClientAssembly.Framework.GetClient().GetData().GetInput_Instnace().GetBuffer_Back_InputDouble().Get_InputBufferSubset();
                data[1] = Convert.ToByte(input_subset_Praise0.GetFlag_IsPingActive());
                break;

            case 1:
                Florence.ClientAssembly.Praise_Files.Praise1_Input input_subset_Praise1 = (Florence.ClientAssembly.Praise_Files.Praise1_Input)Florence.ClientAssembly.Framework.GetClient().GetData().GetInput_Instnace().GetBuffer_Back_InputDouble().Get_InputBufferSubset();
                bytes = BitConverter.GetBytes(input_subset_Praise1.Get_Mouse_X());
                for (byte index = 0; index < 2; index++)
                {
                    data[index + 1] = bytes[index];
                }
                bytes = BitConverter.GetBytes(input_subset_Praise1.Get_Mouse_Y());
                for (byte index = 0; index < 2; index++)
                {
                    data[index + 3] = bytes[index];
                }
                break;

            case 2:
                Florence.ClientAssembly.Praise_Files.Praise2_Input input_subset_Praise2 = (Florence.ClientAssembly.Praise_Files.Praise2_Input)Florence.ClientAssembly.Framework.GetClient().GetData().GetInput_Instnace().GetBuffer_Back_InputDouble().Get_InputBufferSubset();
                data[1] = Convert.ToByte(input_subset_Praise2.Get_Fowards());
                data[2] = Convert.ToByte(input_subset_Praise2.Get_Backwards());
                data[3] = Convert.ToByte(input_subset_Praise2.Get_Left());
                data[4] = Convert.ToByte(input_subset_Praise2.Get_Right());
                bytes = BitConverter.GetBytes(input_subset_Praise2.GetPeriod());
                for (byte index = 0; index < 2; index++)
                {
                    data[index + 5] = bytes[index];
                }
                    break;
            }

            Address address = new Address();
            address.SetAddress("192.168.8.2", 3074);
            uint connection = client.Connect(ref address);

            client.SendMessageToConnection(connection, data);//ToDo
        }

        public static void CopyPayloadFromMessage()
        {
            byte[] buffer = new byte[1024];
            netMessage.CopyTo(buffer);

            switch (buffer[0])
            {
            case 0:

                break;

            case 1:

                break;
            }
        }

        public static void SetA_HookForDebugInformation()
        {
            DebugCallback debug = (type, message) =>
            {
                Console.WriteLine("Debug - Type: " + type + ", Message: " + message);
            };

            NetworkingUtils utils = new NetworkingUtils();

            utils.SetDebugCallback(DebugType.Everything, debug);
        }

        public static ref Valve.Sockets.NetworkingIdentity Get_Identity()
        {
            return ref identity;
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
