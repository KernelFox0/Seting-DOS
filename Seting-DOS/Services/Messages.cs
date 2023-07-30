/// 
/// Feedback Message System Service, Last modified: 2023. 07. 30.
/// 
/// Copyright (C) 2023
/// 
/// This file is part of Seting-DOS.
/// Seting-DOS is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
/// as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
/// 
/// Seting-DOS is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty
/// of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
/// 
/// You should have received a copy of the GNU General Public License along with Seting-DOS. If not, see <https://www.gnu.org/licenses/>.
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
