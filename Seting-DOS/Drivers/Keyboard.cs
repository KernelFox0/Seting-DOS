/// 
/// Keyboard driver, Last modified: 2023. 07. 30.
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
using Seting_DOS.Services;
using Seting_DOS.Apps;

namespace Seting_DOS.Drivers
{
    public static class Keyboard
	{
		public static string[] Load()
		{
			try { Console.InputEncoding = Cosmos.System.ExtendedASCII.CosmosEncodingProvider.Instance.GetEncoding(437); }
			catch (Exception e)
			{
				string[] error = { "error", e.Message };
				return error;
			}
			string[] result = { "done", "Keyboard driver loaded" };
			return result;
		}
		public static string KeyHandler(bool isTextUI = false, bool isPassword = false)
		{
			string cmd = "";
			ConsoleKeyInfo p;
			int x = Console.GetCursorPosition().Left;
			int y = Console.GetCursorPosition().Top;
			int originY = Console.GetCursorPosition().Top;
			int originX = Console.GetCursorPosition().Left;
			while (true)
			{
				p = Console.ReadKey();
				if (p.Key == ConsoleKey.Enter)
				{
					if (CommandHistoryManager.initState) { CommandHistoryManager.End(); }
					if (!isPassword) { CommandHistoryManager.AddValue(cmd); }
					Console.Write("\n");
					break;
				}
				else if (p.Key == ConsoleKey.Backspace)
				{
					if (x > originX && y == originY)
					{
						Console.SetCursorPosition(x - 1, y);
						Console.Write(" ");
						Console.SetCursorPosition(x - 1, y);
						x--;
						cmd = cmd.Remove(cmd.Length - 1);
					}
					if (y > originY && x == 0)
					{
						Console.SetCursorPosition(79, y - 1);
						Console.Write(" ");
						Console.SetCursorPosition(79, y - 1);
						x = 79;
						y--;
						cmd = cmd.Remove(cmd.Length - 1);
					}
					if (y > originY && x > 0)
					{
						Console.SetCursorPosition(x - 1, y);
						Console.Write(" ");
						Console.SetCursorPosition(x - 1, y);
						x--;
						cmd = cmd.Remove(cmd.Length - 1);
					}
				}
				else if (p.Key == ConsoleKey.F9)
				{
					LogonUI.LockScreen();
				}
				else if (p.Key == ConsoleKey.F1)
				{
					Drivers.Beep.Mute();
				}
				else if (p.Key == ConsoleKey.UpArrow && !isTextUI)
				{
					if (!CommandHistoryManager.initState) { CommandHistoryManager.Init(cmd, x, y); }
					Console.SetCursorPosition(originX, originY);
					for (int i = 0; i <= x; i++)
					{
						Console.Write(" ");
					}
					Console.SetCursorPosition(originX, originY);
					cmd = CommandHistoryManager.GetPreviousCmd();
					Console.Write(cmd);
					x = originX + cmd.Length; y = originY;
					while (x >= 80)
					{
						x = x - 80;
						y++;
					}
				}
				else if (p.Key == ConsoleKey.DownArrow && !isTextUI && CommandHistoryManager.initState)
				{
					if (!CommandHistoryManager.initState) { CommandHistoryManager.Init(cmd, x, y); }
					Console.SetCursorPosition(originX, originY);
					for (int i = 0; i <= x; i++)
					{
						Console.Write(" ");
					}
					Console.SetCursorPosition(originX, originY);
					cmd = CommandHistoryManager.GetNextCmd();
					Console.Write(cmd);
					x = originX + cmd.Length; y = originY;
					while (x >= 80)
					{
						x = x - 80;
						y++;
					}
				}
				else
				{
					cmd += p.KeyChar;
					x++;
					if (isPassword)
					{
						Console.SetCursorPosition(Console.GetCursorPosition().Left - 1, Console.GetCursorPosition().Top);
						Console.Write("*");
					}
				}
				if (x == 80)
				{
					x = 0;
					y++;
				}
				if (!isTextUI) { StatusBar.TerminalDisp(); }
				Console.SetCursorPosition(x, y);
				if (isTextUI)
				{
					TUIBGCol.Set();
					Console.ForegroundColor = ConsoleColor.White;
				}
			}
			return cmd;
		}
	}
}
