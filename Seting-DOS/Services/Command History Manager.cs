/// 
/// Seting-DOS Command History Manager, Last modified: 2023. 07. 30.
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

namespace Seting_DOS.Services
{
	public static class CommandHistoryManager
	{
		public static List<string> commandHistory = new List<string>();
		public static string save = null;
		public static bool initState = false;
		public static int currentIndex = 0;
		public static int saveX = 0;
		public static int saveY = 0;

		public static void Init(string cmd, int x, int y)
		{
			initState = true;
			commandHistory.Add(cmd);
			currentIndex = commandHistory.Count - 1;
			save = cmd;
			saveX = x;
			saveY = y;
		}
		public static void AddValue(string cmd)
		{
			commandHistory.Add(cmd);
		}
		public static void End()
		{
			commandHistory.RemoveAt(commandHistory.Count - 1);
			initState = false;
		}
		public static string GetPreviousCmd()
		{
			if (initState)
			{
				if (currentIndex > 0) { currentIndex--; }
				return commandHistory[currentIndex];
			}
			else { return null; }
		}
		public static string GetNextCmd()
		{
			if (initState)
			{
                if (currentIndex < commandHistory.Count - 1) { currentIndex++; }
                return commandHistory[currentIndex];
            }
			else { return null; }
		}
		public static void PrintAll()
		{
			for (int i = 0; i < commandHistory.Count; i++) { Console.WriteLine(commandHistory[i]); }
		}
	}
}