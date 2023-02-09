/// 
/// Feedback Message System Service, Last modified: 2022. 10. 19.
/// Made for Seting-DOS, feel free to use any code from this
/// 

using System;
using Seting_DOS.Drivers;

namespace Seting_DOS.Services
{
	public static class Messages
	{
		public static void Error(string message)
		{
			Beep.Sound.Error();
			ConsoleColor og = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(message);
			Console.ForegroundColor = og;
		}
		public static void Warning(string message)
		{
			Beep.Sound.Warning();
			ConsoleColor og = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(message);
			Console.ForegroundColor = og;
		}
		public static void Info(string message)
		{
			Beep.Sound.Question();
			ConsoleColor og = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine(message);
			Console.ForegroundColor = og;
		}
		public static void Success(string message)
		{
			ConsoleColor og = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine(message);
			Console.ForegroundColor = og;
		}
		public static void Custom(string message, ConsoleColor fgc = ConsoleColor.White, int sound = 0)
        {
			if (sound == 1) { Beep.Sound.Error(); }
			if (sound == 2) { Beep.Sound.Warning(); }
			if (sound == 3) { Beep.Sound.Question(); }
			ConsoleColor og = Console.ForegroundColor;
			Console.ForegroundColor = fgc;
			Console.WriteLine(message);
			Console.ForegroundColor = og;
		}
	}
}
