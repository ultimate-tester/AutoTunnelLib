using System.Net.NetworkInformation;
using System.Reflection;

namespace Force.AutoTunnel
{
	public static class InterfaceHelper
	{
		public static uint GetInterfaceId()
		{
			NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
			foreach (NetworkInterface interface2 in allNetworkInterfaces)
			{
				if ((((interface2.OperationalStatus == OperationalStatus.Up) &&
					(interface2.Speed > 0L)) && 
					(interface2.NetworkInterfaceType != NetworkInterfaceType.Loopback)) && 
					(interface2.NetworkInterfaceType != NetworkInterfaceType.Tunnel))
				{
					var props = interface2.GetIPProperties().GetIPv4Properties();

					return (uint)props.Index;
					// Console.WriteLine(interface2.Id + " " + interface2.Name + " " + prop.GetValue(interface2));
				}
			}

			return 0;
		}
	}
}
