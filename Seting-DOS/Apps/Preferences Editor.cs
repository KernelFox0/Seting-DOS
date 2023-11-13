/// 
/// Preferences editor app, Last modified: 2023. 11. 13.
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
using Seting_DOS.Services;
using Seting_DOS.Drivers;
using System.IO;

namespace Seting_DOS.Apps
{
	public static class PreferencesEditor
	{
		public static void StartApp()
		{
			TUIBGCol.Set();
			Console.ForegroundColor = ConsoleColor.White;
			Console.Clear();
			Console.SetCursorPosition(0, 0);
			Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White;
			Console.Write(" Seting-DOS Preferences Editor   | Ctrl-X - Exit                                ");
			TUIBGCol.Set(); Console.ForegroundColor = ConsoleColor.White;
			Console.Write(@"                                                                                ");
			Console.Write(@"   \ /   o                                                                      ");
			Console.Write(@"    |    |       Seting-DOS                                                     ");
			Console.Write(@"    |    |       Preferences Editor                                             ");
			Console.Write(@"    |   ( )                                                                     ");
			Console.Write(@"                                                                                ");
			Console.Write(@"                                                                                ");
			Console.Write(@"   \ | /                                                                        ");
			Console.Write(@"   - O -   System Settings                                                      ");
			Console.Write(@"   / | \                                                                        ");
			Console.Write(@"                                                                                ");
			Console.Write(@"                                                                                ");
			Console.Write(@"                                                                                ");
			Console.Write(@"     O                                                                          ");
			Console.Write(@"    ___    User Settings                                                        ");
			Console.Write(@"   /   \                                                                        ");
			Console.Write(@"                                                                                ");
			Console.Write(@"                                                                                ");
			Console.Write(@"                                                                                ");
			Console.Write(@"                                                                                ");
			Console.Write(@"                                                                                ");
			Console.Write(@"                                                                                ");
			Console.Write(@"                                                                                ");
			Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White;
			Console.Write(@"                                                                               ");
			Console.SetCursorPosition(0, 24);
			Console.Write(@"                                                                               ");
			Console.SetCursorPosition(1, 24);
			Console.Write(EnvVars.versionstring);
			TUIBGCol.Set(); Console.ForegroundColor = ConsoleColor.White;
			Console.SetCursorPosition(0, 1);
			int selection = 0;
			ConsoleKeyInfo key;
			while (true)
			{
				if (selection == 0)
				{
					TUIBGCol.Set(); Console.ForegroundColor = ConsoleColor.White;
					Console.SetCursorPosition(11, 16);
					Console.WriteLine("User Settings");
					Console.SetCursorPosition(11, 10);
					Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White;
					Console.WriteLine("System Settings");
				}
				else
				{

					TUIBGCol.Set(); Console.ForegroundColor = ConsoleColor.White;
					Console.SetCursorPosition(11, 10);
					Console.WriteLine("System Settings");
					Console.SetCursorPosition(11, 16);
					Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White;
					Console.WriteLine("User Settings");
				}
				key = Console.ReadKey();
				if (key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.DownArrow)
				{
					switch (selection)
					{
						case 0:
							selection = 1;
							break;
						case 1:
							selection = 0;
							break;
					}
				}
				else if (key.Key == ConsoleKey.Enter) { break; }
				else if (key.Modifiers == ConsoleModifiers.Control && key.Key == ConsoleKey.X)
				{
					return;
				}
			}
			if (selection == 0)
			{
				System();
			}
			else
			{
				User();
			}
		}
		public static void System()
		{
			int selection = 0;
			ConsoleKeyInfo key;
			while (true)
			{
				TUIBGCol.Set();
				Console.ForegroundColor = ConsoleColor.White;
				Console.Clear();
				Console.SetCursorPosition(0, 0);
				Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White;
				Console.Write(" Seting-DOS Preferences Editor - System   | Ctrl-X - Back                       ");
				TUIBGCol.Set(); Console.ForegroundColor = ConsoleColor.White;
				Console.Write(@"                                                                                ");
				Console.Write(@"   \ /   o                                                                      ");
				Console.Write(@"    |    |       Seting-DOS                                                     ");
				Console.Write(@"    |    |       Preferences Editor                                             ");
				Console.Write(@"    |   ( )                                                                     ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"   Theme: x                                                                     ");
				Console.Write(@"   The background color of TextUI apps                                          ");
				Console.Write(@"                                                                                ");
				Console.Write(@"   Verbose boot: [ ]                                                            ");
				Console.Write(@"   Shows boot messages instead of boot logo                                     ");
				Console.Write(@"                                                                                ");
				Console.Write(@"   Debug boot: [ ]                                                              ");
				Console.Write(@"   Pauses boot after it's complete. Only works with verbose boot enabled.       ");
				Console.Write(@"                                                                                ");
				Console.Write(@"   Changing computer name is not yet implemented.                               ");
				Console.Write(@"   It will get added in the next version.                                       ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White;
				Console.Write(@"                                                                               ");
				Console.SetCursorPosition(0, 24);
				Console.Write(@"                                                                               ");
				Console.SetCursorPosition(1, 24);
				Console.Write(EnvVars.versionstring);
				TUIBGCol.Set(); Console.ForegroundColor = ConsoleColor.White;
				Console.SetCursorPosition(10, 8);
				if (EnvVars.theme == "classic") { Console.Write("Classic Blue"); }
				else { Console.Write("Black          "); }
				Console.SetCursorPosition(18, 11);
				if (EnvVars.verboseMode) { Console.BackgroundColor = ConsoleColor.Green; Console.Write(" "); }
				else { TUIBGCol.Set(); Console.Write(" "); }
				Console.SetCursorPosition(16, 14);
				if (EnvVars.debugBoot) { Console.BackgroundColor = ConsoleColor.Green; Console.Write(" "); }
				else { TUIBGCol.Set(); Console.Write(" "); }
				TUIBGCol.Set(); Console.ForegroundColor = ConsoleColor.White;
				if (selection == 0)
				{
					TUIBGCol.Set(); Console.ForegroundColor = ConsoleColor.White;
					Console.SetCursorPosition(3, 11);
					Console.WriteLine("Verbose boot:");
					Console.SetCursorPosition(3, 14);
					Console.WriteLine("Debug boot:");
					Console.SetCursorPosition(3, 8);
					Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White;
					Console.WriteLine("Theme:");
				}
				else if (selection == 1)
				{
					TUIBGCol.Set(); Console.ForegroundColor = ConsoleColor.White;
					Console.SetCursorPosition(3, 8);
					Console.WriteLine("Theme:");
					Console.SetCursorPosition(3, 14);
					Console.WriteLine("Debug boot:");
					Console.SetCursorPosition(3, 11);
					Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White;
					Console.WriteLine("Verbose boot:");
				}
				else
				{
					TUIBGCol.Set(); Console.ForegroundColor = ConsoleColor.White;
					Console.SetCursorPosition(3, 11);
					Console.WriteLine("Verbose boot:");
					Console.SetCursorPosition(3, 8);
					Console.WriteLine("Theme:");
					Console.SetCursorPosition(3, 14);
					Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White;
					Console.WriteLine("Debug Boot:");
				}
				key = Console.ReadKey();
				if (key.Key == ConsoleKey.DownArrow)
				{
					switch (selection)
					{
						case 0:
							selection = 1;
							break;
						case 1:
							selection = 2;
							break;
						case 2:
							selection = 0;
							break;
					}
				}
				else if (key.Key == ConsoleKey.UpArrow)
				{
					switch (selection)
					{
						case 0:
							selection = 2;
							break;
						case 1:
							selection = 0;
							break;
						case 2:
							selection = 1;
							break;
					}
				}
				else if (key.Key == ConsoleKey.Enter)
				{
					if (selection == 0)
					{
						if (EnvVars.theme == "black")
						{
							EnvVars.theme = "classic";
						}
						else
						{
							EnvVars.theme = "black";
						}
						File.Delete(@"0:\SDOS\preferences\theme.dat");
						StreamWriter theme = new StreamWriter(@"0:\SDOS\preferences\theme.dat");
						theme.Write(EnvVars.theme);
						theme.Close();
					}
					else if (selection == 1)
					{
						StreamWriter vb = new StreamWriter(@"0:\SDOS\preferences\verboseBoot.pref");
						if (EnvVars.verboseMode)
						{
							EnvVars.verboseMode = false;
							vb.Write("0");
						}
						else
						{
							EnvVars.verboseMode = true;
							vb.Write("1");
						}
						vb.Close();
					}
					else
					{
						StreamWriter db = new StreamWriter(@"0:\SDOS\preferences\debugBoot.pref");
						if (EnvVars.debugBoot)
						{
							EnvVars.debugBoot = false;
							db.Write("0");
						}
						else
						{
							EnvVars.debugBoot = true;
							db.Write("1");
						}
						db.Close();
					}
				}
				else if (key.Modifiers == ConsoleModifiers.Control && key.Key == ConsoleKey.X)
				{
					break;
				}
			}
			StartApp();
		}
		public static void User()
		{
			ConsoleKeyInfo key;
			while (true)
			{
				TUIBGCol.Set();
				Console.ForegroundColor = ConsoleColor.White;
				Console.Clear();
				Console.SetCursorPosition(0, 0);
				Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White;
				Console.Write(" Seting-DOS Preferences Editor - User   | Ctrl-X - Back                         ");
				TUIBGCol.Set(); Console.ForegroundColor = ConsoleColor.White;
				Console.Write(@"                                                                                ");
				Console.Write(@"   \ /   o                                                                      ");
				Console.Write(@"    |    |       Seting-DOS                                                     ");
				Console.Write(@"    |    |       Preferences Editor                                             ");
				Console.Write(@"    |   ( )                                                                     ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"   Changing username and password is not yet implemented.                       ");
				Console.Write(@"   They will get added in the next version.                                     ");
				Console.Write(@"                                                                                ");
				Console.Write(@"   Press Ctrl-X to go back.                                                     ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White;
				Console.Write(@"                                                                               ");
				Console.SetCursorPosition(0, 24);
				Console.Write(@"                                                                               ");
				Console.SetCursorPosition(1, 24);
				Console.Write(EnvVars.versionstring);
				TUIBGCol.Set(); Console.ForegroundColor = ConsoleColor.White;
				key = Console.ReadKey();
				if (key.Modifiers == ConsoleModifiers.Control && key.Key == ConsoleKey.X)
				{
					break;
				}
			}
			StartApp();
		}
	}
}
