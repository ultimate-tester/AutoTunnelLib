

namespace Force.AutoTunnel.Config
{
	public class RemoteClientConfig
	{
		public string Key { get; set; }

		public byte[] BinaryKey { get; set; }

		public string Description { get; set; }

		public int? ClampMss { get; set; }
	}
}
