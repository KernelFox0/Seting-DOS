/// 
/// Network driver, Last modified: 2022. 09. 30.
/// Made for Seting-DOS, feel free to use any code from this
/// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using net = Cosmos.System.Network;

namespace Seting_DOS.Drivers
{
	public static class Network
	{
		public static string[] Load()
		{
			try
			{
				net.NetworkStack.Init();
				Cosmos.HAL.NetworkDevice nic = null;
				//nic.Enable(); Console.Write("1");
				net.IPv4.Address ip = new net.IPv4.Address(192, 168, 1, 42);
				net.IPv4.Address subnet = new net.IPv4.Address(255, 255, 255, 0);
				net.IPv4.Address gateway = new net.IPv4.Address(127, 0, 0, 1);
				net.Config.IPConfig.Enable(nic, ip, subnet, gateway);
				net.Config.IPConfig ipc = new net.Config.IPConfig(ip, subnet);
				net.Config.NetworkConfiguration.AddConfig(nic, ipc);
				net.Config.NetworkConfiguration.SetCurrentConfig(nic, ipc);
				//net.NetworkStack.ConfigIP(nic, ipc); Console.Write("9");
				net.NetworkStack.Update();
				string[] result = { "done", "Network initialization completed!" };
				return result;
			}
			catch (Exception e)
			{
				string[] result = { "error", e.Message };
				return result;
			}
		}
		public static bool Test(ushort port)
		{
			try
			{
				net.NetworkDebugger ndbg = new net.NetworkDebugger(port);
				ndbg.Start();
				ndbg.Send("TEST");
				ndbg.Stop();
				return true;
			}
			catch (Exception) { return false; }
		}
		public static bool isAvailable()
        {
			net.IPv4.Address targetIP = new net.IPv4.Address(192, 168, 1, 1);
			net.IPv4.Address returnIP = net.Config.IPConfig.FindNetwork(targetIP);
            //Console.WriteLine(targetIP.ToString());
            //Console.WriteLine(returnIP.ToString());
            if (targetIP == returnIP)
            {
				return true;
            }
			else
            {
				return false;
            }
        }
	}
}