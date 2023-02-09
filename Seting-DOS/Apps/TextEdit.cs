/// 
/// Seting-DOS Text Editor, Last modified: 2022. 09. 24.
/// Made for Seting-DOS, feel free to use any code from this
/// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seting_DOS.Drivers;
using System.IO;

namespace Seting_DOS.Apps
{
	public static class TextEdit
	{
		public static void Start(string path)
		{
			Services.TUIBGCol.Set();
			Console.ForegroundColor = ConsoleColor.White;
			Console.Clear();
			Console.SetCursorPosition(0, 0);
			Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White;
			Console.Write(" Seting-DOS Text Editor | Ctrl-S: Save | Ctrl-L: Load | Ctrl-X: Exit    | XX:XX ");
			Services.TUIBGCol.Set(); Console.ForegroundColor = ConsoleColor.White;
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                               ");
			Console.SetCursorPosition(0, 1);
			try { Handle(path); }
			catch (Exception ex) { TextUI.CrashUI.UserspaceCrash(ex); }
		}
		private static void DrawUIBar()
		{
			int x = Console.GetCursorPosition().Left;
			int y = Console.GetCursorPosition().Top;
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White;
            Console.Write(" Seting-DOS Text Editor | Ctrl-S: Save | Ctrl-L: Load | Ctrl-X: Exit    | XX:XX ");
            Services.TUIBGCol.Set(); Console.ForegroundColor = ConsoleColor.White;
			Console.SetCursorPosition(x, y);
        }
		private static void LoadFile(string path)
		{
			StreamReader file = new StreamReader(path);
			Console.Write(file.ReadToEnd());
			file.Close();
		}
		private static string Editor() //Keyboard handler (Normal driver is different, can't be used)
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
					x = 0;
					y++;
					cmd += "\n";
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
						if (cmd.EndsWith("\n"))
						{
							y--;
							cmd = cmd.Remove(cmd.Length - 2);
							x = cmd.Remove(0, cmd.LastIndexOf("\n")).Length;
						}
						else
						{
							Console.SetCursorPosition(79, y - 1);
							Console.Write(" ");
							Console.SetCursorPosition(79, y - 1);
							x = 79;
							y--;
							cmd = cmd.Remove(cmd.Length - 1);
						}
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
				else if (p.Modifiers == ConsoleModifiers.Control && p.Key == ConsoleKey.S)
				{
					break;
				}
				else
				{
					cmd += p.KeyChar;
					x++;
				}
				if (x == 80)
				{
					x = 0;
					y++;
				}
				Console.SetCursorPosition(x, y);
				Services.TUIBGCol.Set();
				Console.ForegroundColor = ConsoleColor.White;
			}
			return cmd;
		}
		private static void Handle(string path)
        {
			StreamWriter file = new StreamWriter(path);
			file.Write(Editor());
			file.Close();
			Close();
        }
		private static void Close()
        {
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;
			Console.Clear();
			Console.SetCursorPosition(0, 0);
        }
	}
}
