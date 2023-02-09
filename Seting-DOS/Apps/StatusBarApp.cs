/// 
/// StatusBar application, Last modified: 2022. 09. 30.
/// Note: App is always "running", no terminal command
/// Made for Seting-DOS, feel free to use any code from this
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seting_DOS.Apps
{
	public static class StatusBar
	{
		public static void TerminalDisp()
		{
			ConsoleColor color = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.White;
			string[] time = Drivers.RTC.GetTime();
			bool net = Drivers.Network.isAvailable();
			int xPos = Console.GetCursorPosition().Left;
			int yPos = Console.GetCursorPosition().Top;
			Console.SetCursorPosition(69, 0);
			Console.Write("MUTE XX:XX");
			Console.SetCursorPosition(69, 0);
			if (!Services.EnvVars.mute) { Console.Write("    "); }
			Console.SetCursorPosition(74, 0);
			Console.Write("{0}:{1}", time[0], time[1]);
			Console.SetCursorPosition(xPos, yPos);
			Console.ForegroundColor = color;
		}
	}
}
