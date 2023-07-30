/// 
/// StatusBar application, Last modified: 2023. 07. 30.
/// Note: App is always "running", no terminal command
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
