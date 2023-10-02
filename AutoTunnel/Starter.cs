﻿using System.Collections.Generic;
using System.Globalization;

using Force.AutoTunnel.Config;

namespace Force.AutoTunnel
{
	public class Starter
	{
		private static TunnelStorage _storage;

		private static List<ClientSender> _clientSenders;

		private static Listener _listener;

		public static void Start()
		{
			_storage = new TunnelStorage();
			var config = ConfigHelper.Config;

			if (config.EnableListening)
			{
				_listener = new Listener(_storage, config);
				_listener.Start();
			}

			_clientSenders = new List<ClientSender>();
			foreach (var rs in config.RemoteServers)
			{
				_clientSenders.Add(new ClientSender(rs, _storage));
			}
		}

		public static void Stop()
		{
			_clientSenders.ForEach(x => x.Dispose());
			_storage.RemoveAllSessions();
            if (_listener != null)
				_listener.Dispose();
		}
	}
}
