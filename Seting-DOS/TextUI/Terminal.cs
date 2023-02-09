/// 
/// System terminal UI, Last modified: 2022. 09. 24.
/// Made for Seting-DOS, feel free to use any code from this
/// 

using Seting_DOS.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seting_DOS.TextUI
{
	public static class Terminal
	{
		public static string username = "unknown";
		public static string hostname = "unknown";
		public static string[] Init()
		{
			try
			{
				username = Services.EnvVars.username;
				hostname = Services.EnvVars.hostname;
				VSFS.Zerosix();
			}
			catch (Exception e)
            {
				string[] msg = { "error", e.Message };
				return msg;
            }
			string[] ok = { "done", "Terminal shell initialized successfully" };
			return ok;
		}
		public static string WriteShell()
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write(username.ToLower());
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.Write("@");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write(hostname.ToLower());
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write(" " + VSFS.act_dir + "$ ");
			return Keyboard.KeyHandler();
		}
	}
}
