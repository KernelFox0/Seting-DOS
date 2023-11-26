/// 
/// Preferences editor app, Last modified: 2023. 11. 26.
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
using System.Xml.Linq;

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
					Console.SetCursorPosition(11, 15);
					Console.WriteLine("User Settings");
					Console.SetCursorPosition(11, 9);
					Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White;
					Console.WriteLine("System Settings");
				}
				else
				{

					TUIBGCol.Set(); Console.ForegroundColor = ConsoleColor.White;
					Console.SetCursorPosition(11, 9);
					Console.WriteLine("System Settings");
					Console.SetCursorPosition(11, 15);
					Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White;
					Console.WriteLine("User Settings");
				}
				key = Console.ReadKey(true);
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
				Console.Write(@"   Change computer name: x                                                      ");
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
				Console.SetCursorPosition(25, 17);
				Console.Write(EnvVars.hostname);
				if (selection == 0)
				{
					TUIBGCol.Set(); Console.ForegroundColor = ConsoleColor.White;
					Console.SetCursorPosition(3, 11);
					Console.Write("Verbose boot:");
					Console.SetCursorPosition(3, 14);
					Console.Write("Debug boot:");
					Console.SetCursorPosition(3, 17);
					Console.Write("Change computer name:");
					Console.SetCursorPosition(3, 8);
					Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White;
					Console.Write("Theme:");
				}
				else if (selection == 1)
				{
					TUIBGCol.Set(); Console.ForegroundColor = ConsoleColor.White;
					Console.SetCursorPosition(3, 8);
					Console.Write("Theme:");
					Console.SetCursorPosition(3, 14);
					Console.Write("Debug boot:");
					Console.SetCursorPosition(3, 17);
					Console.Write("Change computer name:");
					Console.SetCursorPosition(3, 11);
					Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White;
					Console.Write("Verbose boot:");
				}
				else if (selection == 2)
				{
					TUIBGCol.Set(); Console.ForegroundColor = ConsoleColor.White;
					Console.SetCursorPosition(3, 11);
					Console.Write("Verbose boot:");
					Console.SetCursorPosition(3, 8);
					Console.Write("Theme:");
					Console.SetCursorPosition(3, 17);
					Console.Write("Change computer name:");
					Console.SetCursorPosition(3, 14);
					Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White;
					Console.Write("Debug Boot:");
				}
				else
				{
					TUIBGCol.Set(); Console.ForegroundColor = ConsoleColor.White;
					Console.SetCursorPosition(3, 11);
					Console.Write("Verbose boot:");
					Console.SetCursorPosition(3, 8);
					Console.Write("Theme:");
					Console.SetCursorPosition(3, 14);
					Console.Write("Debug Boot:");
					Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White;
					Console.SetCursorPosition(3, 17);
					Console.Write("Change computer name:");
				}
				key = Console.ReadKey(true);
				TUIBGCol.Set(); Console.ForegroundColor = ConsoleColor.White;
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
							selection = 3;
							break;
						case 3:
							selection = 0;
							break;
					}
				}
				else if (key.Key == ConsoleKey.UpArrow)
				{
					switch (selection)
					{
						case 0:
							selection = 3;
							break;
						case 1:
							selection = 0;
							break;
						case 2:
							selection = 1;
							break;
						case 3:
							selection = 2;
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
					else if (selection == 2)
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
					else
					{
						Console.SetCursorPosition(25, 17);
						for (int i = 0; i < EnvVars.hostname.Length; i++)
						{
							Console.Write(" ");
						}
						Console.SetCursorPosition(25, 17);
						string newName = Keyboard.KeyHandler(true, false);
						if (newName.Replace(" ", "") != "") { EnvVars.hostname = newName.Replace(" ", "-"); }
						if (newName.Replace("-", "") != "")
						{
							File.Delete(@"0:\SDOS\preferences\compName.pref");
							StreamWriter name = new StreamWriter(@"0:\SDOS\preferences\compName.pref");
							name.Write(newName);
							name.Close();
							EnvVars.hostname = newName.Trim('\n');
						}
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
			int selection = 0;
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
				Console.Write(@"   Change username: x                                                           ");
				Console.Write(@"                                                                                ");
				Console.Write(@"   Remove password                                                              ");
				Console.Write(@"                                                                                ");
				Console.Write(@"   Change password                                                              ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				/*Console.Write(@"   Manage other accounts (NOT YET IMPLEMENTED)                                  ");*/
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
				Console.SetCursorPosition(20, 8);
				Console.Write(EnvVars.username);
				if (selection == 0)
				{
					TUIBGCol.Set(); Console.ForegroundColor = ConsoleColor.White;
					Console.SetCursorPosition(3, 10);
					Console.Write("Remove password");
					Console.SetCursorPosition(3, 12);
					Console.Write("Change password");
					Console.SetCursorPosition(3, 8);
					Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White;
					Console.Write("Change username:");
				}
				else if (selection == 1)
				{
					TUIBGCol.Set(); Console.ForegroundColor = ConsoleColor.White;
					Console.SetCursorPosition(3, 8);
					Console.Write("Change username:");
					Console.SetCursorPosition(3, 12);
					Console.Write("Change password");
					Console.SetCursorPosition(3, 10);
					Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White;
					Console.Write("Remove password");
				}
				else
				{
					TUIBGCol.Set(); Console.ForegroundColor = ConsoleColor.White;
					Console.SetCursorPosition(3, 8);
					Console.Write("Change username:");
					Console.SetCursorPosition(3, 10);
					Console.Write("Remove password");
					Console.SetCursorPosition(3, 12);
					Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White;
					Console.Write("Change password");
				}
				key = Console.ReadKey(true);
				TUIBGCol.Set(); Console.ForegroundColor = ConsoleColor.White;
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
						//Change username
						Console.SetCursorPosition(20, 8);
						for (int i = 0; i < EnvVars.username.Length; i++) { Console.Write(" "); }
						Console.SetCursorPosition(20, 8);
						string name = Console.ReadLine().Trim('\n');
						if (name.Trim(' ').Trim('_').Trim('-') != "" || name != null)
						{
							string userFolder = name.Replace(" ", "-").Replace("_", "-").Trim('\n');
							File.Delete(@"0:\users\" + EnvVars.username + @"\fullName.dat");
							StreamWriter fn = new StreamWriter(@"0:\users\" + EnvVars.username + @"\fullName.dat");
							fn.Write(userFolder);
							fn.Close();
							VSFS.cur_dir = "0:\\users\\";
							VSFS.act_dir = "/0/users/";
							VSFS.Rename(EnvVars.username, userFolder, true);
							EnvVars.username = userFolder;
							Console.SetCursorPosition(3, 9);
							Console.ForegroundColor = ConsoleColor.Green;
							Console.Write("User renaming completed! Press any key to restart...");
							Console.ReadKey();
							Cosmos.System.Power.Reboot();
						}
					}
					else if (selection == 1)
					{
						//Remove password
						try
						{
							File.Delete(@"0:\users\" + EnvVars.username + @"\password.pwd");
							File.Delete(@"0:\users\" + EnvVars.username + @"\passRem.dat");
							EnvVars.hasPassword = false;
						}
						catch { }
					}
					else if (selection == 2)
					{
						//Change password
						Console.SetCursorPosition(3, 14);
						Console.Write("Repeat password: ");
						Console.SetCursorPosition(3, 16);
						Console.Write("Password reminder: ");
						Console.SetCursorPosition(3, 12);
						Console.Write("Change password: ");
						string password = Keyboard.KeyHandler(true, true);
						string reminder = null;
						if (password == "" || password == null) { }
						else
						{
							Console.SetCursorPosition(20, 14);
							string tmp = Keyboard.KeyHandler(true, true);
							if (password != tmp)
							{
								Console.SetCursorPosition(3, 15);
								Console.ForegroundColor = ConsoleColor.Red;
								Console.Write("[!] Passwords don't match! Press any key to go back...");
								Console.ReadKey();
							}
							else
							{
								Console.SetCursorPosition(22, 16);
								reminder = Console.ReadLine();
								try
								{
									File.Delete(@"0:\users\" + EnvVars.username + @"\password.pwd");
									File.Delete(@"0:\users\" + EnvVars.username + @"\passRem.dat");
									EnvVars.hasPassword = false;
								}
								catch { }
								StreamWriter pwd = new StreamWriter(@"0:\users\" + EnvVars.username + @"\password.pwd");
								pwd.Write(password);
								pwd.Close();
								StreamWriter rem = new StreamWriter(@"0:\users\" + EnvVars.username + @"\passRem.dat");
								rem.Write(reminder);
								rem.Close();
								EnvVars.hasPassword = true;
							}
						}
					}
				}
				else if (key.Modifiers == ConsoleModifiers.Control && key.Key == ConsoleKey.X)
				{
					break;
				}
			}
			StartApp();
		}
	}
}
