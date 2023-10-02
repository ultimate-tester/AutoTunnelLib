using Force.AutoTunnel;
using Force.AutoTunnel.Config;
using System;
using System.Reflection;

namespace AutoTunnelLib
{
    public class Library
    {
        public void StartClient(RemoteServerConfig[] remoteServers, int port = 12017)
        {
            if (!NativeHelper.IsNativeAvailable)
            {
                throw new Exception("Cannot load WinDivert library");
            }

            if (!ConfigHelper.SetConfig(
                RemoteServers: remoteServers,
                Port: port
                ))
                return;

            Starter.Start();
        }

        public void StartServer(int port = 12017)
        {
            if (!NativeHelper.IsNativeAvailable)
            {
                throw new Exception("Cannot load WinDivert library");
            }

            if (!ConfigHelper.SetConfig(
                EnableListening: true,
                RemoteClients: new RemoteClientConfig[0],
                Port: port
                ))
                return;

            Starter.Start();
        }

        public void Stop()
        {
            Starter.Stop();
        }
    }
}