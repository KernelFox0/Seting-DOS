/// 
/// DotNetParser driver, Last modified: 2022. 10. 09.
/// Made for Seting-DOS, feel free to use any code from this
/// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibDotNetParser;
using LibDotNetParser.CILApi;
using libDotNetClr;
using System.IO;

namespace Seting_DOS.Drivers
{
	public static class DotNetParser
	{
		public static string[] Load()
		{
			try
			{
				//Test if dotnetparser is working by loding a sample app
				Console.WriteLine("Configuring executable...");
				byte[] file = File.ReadAllBytes(@"0:\SDOS\System\dotNetTest.app");
				var executable = new DotNetFile(file);
				Console.WriteLine("Configured.\nSetting up CLR");
				var clr = new DotNetClr(executable, @"0:\SDOS\System\");
				Console.WriteLine("CLR Set up.\nStarting app...");
				clr.Start();
			}
			catch (Exception e)
			{
				string[] error = { "error", e.Message };
				return error;
			}
			string[] result = { "done", "DotNetParser testing completed." };
			return result;
		}
		public static void StartApp(string path)
		{
			try
			{
				if (!path.EndsWith(".app"))
                {
					Beep.Sound.Error();
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Error: Specified path isn't a dotnetparser app (.app)!");
					Console.ForegroundColor = ConsoleColor.White;
					return;
				}
				Console.WriteLine("Reading file and setting up environment...");
                byte[] file = File.ReadAllBytes(path);
				var executable = new DotNetFile(file);
				var clr = new DotNetClr(executable, @"0:\SDOS\System\");
				string[] result = { "done", "App environment setup completed. Ready to run!" };
				Services.BootMSG.Write(result);
				clr.Start();
			}
			catch (Exception ex)
			{
				string[] result = { "error", ex.Message };
				Services.BootMSG.Write(result);
			}
		}
	}
}
