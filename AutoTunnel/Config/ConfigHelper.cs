using System;

namespace Force.AutoTunnel.Config
{
    public static class ConfigHelper
    {
        public static MainConfig Config { get; private set; }

        public static bool SetConfig(
            RemoteServerConfig[] RemoteServers = null,
            RemoteClientConfig[] RemoteClients = null,
            bool EnableListening = false,
            string ListenAddress = "127.0.0.1",
            int Port = 12017
        )
        {
            MainConfig config = new MainConfig
            {
                EnableListening = EnableListening,
                RemoteClients = RemoteClients,
                ListenAddress = ListenAddress,
                Port = Port,
                RemoteServers = RemoteServers,

                AddFirewallRule = false,
                LogFileName = null,
                AutoReloadOnChange = false,
            };

            if (config.RemoteServers != null)
            {
                foreach (var remoteServerConfig in config.RemoteServers)
                {
                    if (string.IsNullOrEmpty(remoteServerConfig.ConnectHost) && string.IsNullOrEmpty(remoteServerConfig.TunnelHost))
                        throw new InvalidOperationException("Missing host info in config");

                    if (string.IsNullOrEmpty(remoteServerConfig.TunnelHost))
                        remoteServerConfig.TunnelHost = remoteServerConfig.ConnectHost;
                    if (string.IsNullOrEmpty(remoteServerConfig.ConnectHost))
                        remoteServerConfig.ConnectHost = remoteServerConfig.TunnelHost;
                }
            }

            Config = config;

            return true;
        }
    }
}
