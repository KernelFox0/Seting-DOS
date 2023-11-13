/// 
/// Seting-DOS Text Editor, Last modified: 2023. 08. 24.
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

using Microsoft.VisualBasic;
using Seting_DOS.Drivers;
using Seting_DOS.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seting_DOS.Apps
{
    public static class TextEdit
	{
		public static string savedPath = "/0/unknown.txt";
		public static string fileType = "File Not Saved";
		public static int pages = 1;
		public static int currentPage = 1;
		public static int charCount = 0;
		public static int lines = 1;
		public static int currentLine = 1;
		public const int maxPLines = 23;
		public static Dictionary<int, string> lineBuffer = new Dictionary<int, string>();
		public static Dictionary<int, string> pageData = new Dictionary<int, string>();
		public static bool open = true;
		public static string tempText = "";
		public static void StartApp(string path = null)
		{
			tempText = "";
			open = true;
			if (path == null)
			{
				NoSaveError();
				return;
			}
			if (!path.StartsWith("/"))
			{
				path = "/0/Users/" + EnvVars.username + "/Documents/" + path;
			}
			try
			{
				savedPath = VSFS.ToRelPath(path);
				fileType = FileTypes.GiveTypeOwO(VSFS.GetFileExtension(savedPath));
				EmptyPage();
				if (File.Exists(savedPath))
				{
					LoadFile();
				}
				else
				{
					File.OpenWrite(savedPath);
					File.Delete(savedPath);
				}
			}
			catch (Exception)
			{
				NoSaveError();
				return;
			}
			try
			{
				if (open) { Handle(); }
				return;
			}
			catch (Exception ex) { CrashUI.ApplicationCrash(ex); }
		}
		public static void NoSaveError()
		{
			TUIBGCol.Set();
			Console.ForegroundColor = ConsoleColor.White;
			Console.Clear();
			Console.SetCursorPosition(0, 0);
			Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White;
			Console.Write(" Seting-DOS Text Editor | Error: No save!                                       ");
			TUIBGCol.Set(); Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write(" [Error] Specifying the path where the file will be saved is required!          ");
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write("                                                                                ");
			Console.Write(" File name (will be saved in your Documents folder or you can specify the path) ");
			Console.Write(" [                                                                              ");
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
			TUIBGCol.Set(); Console.ForegroundColor = ConsoleColor.White;
			Console.SetCursorPosition(3, 6);
			Beep.Sound.Error();
			string name = Console.ReadLine();
			if (name.Replace(" ", "") == "") { Exit(); return; }
			else { StartApp(name); }
		}
		public static void EmptyPage()
		{
			TUIBGCol.Set();
			Console.ForegroundColor = ConsoleColor.White;
			Console.Clear();
			Console.SetCursorPosition(0, 0);
			Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White;
			Console.Write(" Seting-DOS Text Editor | Ctrl-S: Save | Ctrl-L: Load | Ctrl-X: Exit    | XX:XX ");
			TUIBGCol.Set(); Console.ForegroundColor = ConsoleColor.White;
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
			Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White;
			Console.Write(" X | X characters | Page X/X                                                   ");
			Console.SetCursorPosition(0, 24);
			Console.Write("                                                                               ");
			Console.SetCursorPosition(1, 24);
			Console.Write("{0} | {1} characters | Page {2}/{3}", fileType, charCount, currentPage, pages);
			Console.SetCursorPosition(74, 0);
			string[] time = RTC.GetTime();
			Console.Write("{0}:{1}", time[0], time[1]);
			Console.SetCursorPosition(0, 1);
			TUIBGCol.Set(); Console.ForegroundColor = ConsoleColor.White;
		}
		public static void LoadFile()
		{
			try
			{
				string path = savedPath;
				StreamReader doc = new(path);
				string result = doc.ReadToEnd().ReplaceLineEndings("\n");
				int buffer = 0;
				int lineNum = 0;
				pages = 0;
				string bufferStr = "";
				foreach (char c in result.ToCharArray())
				{
					charCount++;
					buffer++;
					if (c != '\n') { bufferStr = bufferStr + c; }
					if (c == '\n' || buffer > 79)
					{
						lineNum++;
						lineBuffer.Add(lineNum, bufferStr);
						bufferStr = "";
						buffer = 0;
					}
					if (lineBuffer.Count >= maxPLines)
					{
                        string data = "";
						for (int i = 1; i <= lineBuffer.Count; i++)
						{
							data += lineBuffer[i];
							if (i < lineBuffer.Count) { data = data + "\n"; }
						}
                        pages++;
						pageData.Add(pages, data);
						bufferStr = "";
						buffer = 0;
						lineNum = 0;
						lineBuffer.Clear();
                    }
				}
				lineBuffer.Add(lineNum + 1, bufferStr);
				string data_ = "";
				for (int i = 1; i <= lineBuffer.Count; i++)
				{
					data_ += lineBuffer[i];
					if (i < lineBuffer.Count) { data_ = data_ + "\n"; }
				}
				tempText = lineBuffer[lineBuffer.Count];
				pages++;
				pageData.Add(pages, data_);
				lineBuffer.Clear();
				currentPage = pages;
				LoadPage();
				pageData.Remove(currentPage);
				lineBuffer.Remove(lineBuffer.Count);
			}
			catch (Exception e)
			{
				Console.Clear();
				Console.BackgroundColor = ConsoleColor.Black;
				Console.ForegroundColor = ConsoleColor.White;
				Messages.Error("Loading failed: " + e.Message + "\nDetails:\nRead pages: " + pageData.Count + "\nRead lines on current page: "
					+ lineBuffer.Count + "\nPages: " + pages + "\nCurrent page: " + currentPage);
				open = false; return;
			}
		}
		public static void LoadPage()
		{
			EmptyPage();
			Console.Write(pageData[currentPage]);
			LoadToLineBuffer();
		}
		public static void LoadToLineBuffer()
		{
			lineBuffer.Clear();
			string data = pageData[currentPage];
			int count = 0;
			lines = 1;
			string last = data.Replace("\n", "");
			string lineData = "";
			foreach (char c in data.ToCharArray())
			{
				if (count >= 80 || c == '\n')
				{
					lineBuffer.Add(lines, lineData);
					lines++;
					last = last.Remove(0, lineData.Length);
					count = 0;
					lineData = "";
				}
				else
				{
					lineData = lineData + c;
					count++;
				}
			}
			lineBuffer.Add(lines, last);
			lines = lineBuffer.Count();
			currentLine = lines;
		}
		public static void Load()
		{
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.Clear();
			Console.SetCursorPosition(0, 0);
			Console.Write(" Seting-DOS Text Editor - Load File                                             ");
			TUIBGCol.Set();
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write(@"   ______                                                                       ");
			Console.Write(@"  /     |                                                                       ");
			Console.Write(@"  | --- |                                                                       ");
			Console.Write(@"  | --- |                                                                       ");
			Console.Write(@"  |_____|                                                                       ");
			Console.Write(@"  Specify path of file. If only file name is added it will be loaded from your  ");
			Console.Write(@"  Documents folder from your user directory. Don't enter anything to exit.      ");
			Console.Write(@"                                                                                ");
			Console.Write(@"                                                                                ");
			Console.Write(@"  Path:                                                                         ");
			Console.Write(@"  [                                                                             ");
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
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.Write(EnvVars.versionstring);
			Console.SetCursorPosition(4, 11);
			TUIBGCol.Set();
			Console.ForegroundColor = ConsoleColor.White;
			savedPath = Console.ReadLine();
			if (savedPath == "")
			{
				Exit();
				return;
			}
			else if (!savedPath.StartsWith("/")) { savedPath = "/0/Users/" + EnvVars.username + "/Documents/" + savedPath; }
			savedPath = VSFS.ToRelPath(savedPath);
			if (!File.Exists(savedPath))
			{
				Console.SetCursorPosition(2, 9);
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write("[X] File doesn't exist! Press ESC to exit or any key to re-enter...");
				Beep.Sound.Error();
				ConsoleKeyInfo key = Console.ReadKey();
				if (key.Key == ConsoleKey.Escape)
				{
					Console.BackgroundColor = ConsoleColor.Black;
					Console.ForegroundColor = ConsoleColor.White;
					Console.Clear();
					return;
				}
				else { Load(); }
			}
			LoadFile();
		}
		public static void Handle()
		{
			ConsoleKeyInfo p;
			string text = tempText;
			while (true)
			{
				int x = text.Length;
				int y = Console.GetCursorPosition().Top;
				Console.SetCursorPosition(x, y);
				p = Console.ReadKey(true);
				if (p.Key == ConsoleKey.PageUp)
				{
					if (pages == currentPage)
					{
						string data = "";
						lineBuffer.Add(currentLine, text);
						for (int i = 1; i <= lines; i++)
						{
							data = data + lineBuffer[i];
							if (i < lines) { data = data + "\n"; }
						}
						pageData.Add(currentPage, data);
					}
					if (currentPage > 1)
					{
						currentPage--;
						LoadPage();
					}
					try
					{
						string temp = lineBuffer.Values.Last();
						text = temp;
					}
					catch (Exception)
					{
						text = "";
					}
				}
				else if (p.Key == ConsoleKey.PageDown)
				{
					if (currentPage < pages)
					{
						currentPage++;
						LoadPage();
						try
						{
							text = lineBuffer.Values.Last();
							lineBuffer.Remove(lineBuffer.Count);
							if (pages == currentPage)
							{
								pageData.Remove(currentPage);
							}
						}
						catch (Exception)
						{
							text = "";
						}
					}
				}
				else if (p.Modifiers == ConsoleModifiers.Control && p.Key == ConsoleKey.S)
				{
					StreamWriter file = new(savedPath);
					string lineData = "";
					lineBuffer.Add(currentLine, text);
					for (int i = 1; i <= lines; i++)
					{
						lineData = lineData + lineBuffer[i];
						if (i < lines) { lineData = lineData + "\n"; }
					}
					pageData.Add(pages, lineData);
					string data = "";
					for (int i = 1; i <= pages; i++)
					{
						data = data + pageData[i];
						if (i != pages)
						{
							data = data + "\n";
						}
					}
					file.Write(data);
					file.Close();
					pageData.Remove(pages);
					lineBuffer.Remove(currentLine);
				}
				else if (p.Modifiers == ConsoleModifiers.Control && p.Key == ConsoleKey.L)
				{
					Load();
				}
				else if (p.Modifiers == ConsoleModifiers.Control && p.Key == ConsoleKey.X)
				{
					Exit();
					return;
				}
				if (currentPage == pages)
				{
					if (p.Key == ConsoleKey.PageUp) { }
					else if (p.Key == ConsoleKey.PageDown) { }
					else if (p.Modifiers == ConsoleModifiers.Control && p.Key == ConsoleKey.S) { }
					else if (p.Modifiers == ConsoleModifiers.Control && p.Key == ConsoleKey.L) { }
					else if (p.Modifiers == ConsoleModifiers.Control && p.Key == ConsoleKey.X) { }
					else if (p.Key == ConsoleKey.Backspace)
					{
						if (x > 0)
						{
							text = text.Remove(text.Length - 1, 1);
							Console.SetCursorPosition(x - 1, y);
							Console.Write(" ");
							Console.SetCursorPosition(x - 1, y);
							charCount--;
							x--;
						}
						else if (x == 0 && y > 1)
						{
							text = lineBuffer[currentLine - 1];
							lineBuffer.Remove(currentLine - 1);
							lines--;
							currentLine--;
							if (text.Length > 79)
							{
								Console.SetCursorPosition(text.Length - 1, y - 1);
								Console.Write(" ");
								Console.SetCursorPosition(text.Length - 1, y - 1);
								text = text.Remove(text.Length - 1, 1);
							}
							else
							{
								Console.SetCursorPosition(text.Length, y - 1);
							}
							y--;
							charCount--;
						}
						else if (x == 0 && y == 1 && currentPage > 1)
						{
							lineBuffer.Clear();
							pages--;
							currentPage--;
							charCount--;
							LoadPage();
							pageData.Remove(pageData.Count);
							text = lineBuffer[currentLine];
							if (text.Length >= 80)
							{
								text = text.Remove(text.Length - 1, 1);
							}
							lineBuffer.Remove(lineBuffer.Count);
						}
					}
					else if (p.Key == ConsoleKey.Enter)
					{
						Console.Write("\n");
						lineBuffer.Add(currentLine, text);
						text = "";
						currentLine++;
						lines++;
						charCount++;
					}
					else
					{
						Console.Write(p.KeyChar);
						text = text + p.KeyChar;
						charCount++;
						if (text.Length >= 80 || (text.Length >= 79 & currentLine == maxPLines))
						{
							lineBuffer.Add(currentLine, text);
							text = "";
							currentLine++;
							lines++;
						}
					}
					if (lineBuffer.Count > maxPLines || currentLine > maxPLines)
					{
						string data = "";
						lineBuffer.Add(currentLine, text);
						for (int i = 1; i <= maxPLines; i++)
						{
							data = data + lineBuffer[i];
							if (i < maxPLines) { data = data + "\n"; }
						}
						pageData.Add(pages, data);
						pages++;
						currentPage++;
						currentLine = 1;
						lines = 1;
						text = "";
						lineBuffer.Clear();
						EmptyPage();
					}
				}
				x = Console.GetCursorPosition().Left;
				y = Console.GetCursorPosition().Top;
				Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White;
				Console.SetCursorPosition(0, 24);
				Console.Write("                                                                               ");
				Console.SetCursorPosition(1, 24);
				Console.Write("{0} | {1} characters | Page {2}/{3}", fileType, charCount, currentPage, pages);
				Console.SetCursorPosition(74, 0);
				string[] time = RTC.GetTime();
				Console.Write("{0}:{1}", time[0], time[1]);
				Console.SetCursorPosition(x, y);
				TUIBGCol.Set(); Console.ForegroundColor = ConsoleColor.White;
			}
		}
		public static void Exit()
		{
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;
			Console.Clear();
			Console.SetCursorPosition(0, 0);
			savedPath = "/0/unknown.txt";
			fileType = "File Not Saved";
			pages = 1;
			currentPage = 1;
			charCount = 0;
			lines = 1;
			currentLine = 1;
			lineBuffer.Clear();
			pageData.Clear();
			return;
		}
	}
}