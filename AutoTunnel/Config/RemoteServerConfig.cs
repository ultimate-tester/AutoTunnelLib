﻿using System.ComponentModel;

namespace Force.AutoTunnel.Config
{
	public class RemoteServerConfig
	{
		public string TunnelHost { get; set; }

		public string ProxyHost { get; set; }

		public string ConnectHost { get; set; }

		public string Key { get; set; }

		public bool KeepAlive { get; set; }

		public bool ConnectOnStart { get; set; }

		public int? ClampMss { get; set; }

		[DefaultValue(15)]
		public int PingInterval { get; set; }
	}
}
