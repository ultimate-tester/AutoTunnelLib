using System.ComponentModel;

namespace Force.AutoTunnel.Config
{
    public class MainConfig
    {
        [DefaultValue(true)]
        public bool EnableListening { get; set; }

        [DefaultValue(true)]
        public bool AddFirewallRule { get; set; }

        public RemoteClientConfig[] RemoteClients { get; set; }

        public string ListenAddress { get; set; }

        [DefaultValue(12017)]
        public int Port { get; set; }

        public RemoteServerConfig[] RemoteServers { get; set; }

        [DefaultValue(10 * 60)]
        public int IdleSessionTime { get; set; }

        public string LogFileName { get; set; }

        [DefaultValue(15)]
        public int PingBackTime { get; set; }

        [DefaultValue(true)]
        public bool AutoReloadOnChange { get; set; }
    }
}
