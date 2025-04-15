#pragma once
#define WIN32_LEAN_AND_MEAN             // Exclude rarely-used stuff from Windows headers
#include "Server.h"

namespace Server_Library
{
    class Framework_Server
    {
    public:
        Framework_Server();
        virtual ~Framework_Server();
        static void Create_Hosting_Server();
        static class Server* Get_HostServer();

    protected:
        
    private:
        static class Server_Library::Server* ptr_HostServer;
    };
}