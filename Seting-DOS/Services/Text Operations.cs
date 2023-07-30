/// 
/// Textfile operation services, Last modified: 2023. 07. 30.
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
using System.IO;
using Seting_DOS.Drivers;

namespace Seting_DOS.Services
{
	public static class TextOperations
	{
		public static void Read(string file = "")
		{
			string path = VSFS.ToRelPath(VSFS.act_dir + file);
			if (file == "")
			{
				Console.Write("The quick brown ");
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write("fox ");
				Console.ForegroundColor = ConsoleColor.White;
				Console.Write("jumps over the lazy dog\n");
			}
			#region Yes
			else if (file.ToLower() == "furry fox")
			{
				Console.WriteLine("Yes I'm a furry with a fox fursona. Deal with it.");
			}
			#endregion
			else
			{
				if (File.Exists(path))
				{
					if (!file.EndsWith(".pwd"))
					{
						StreamReader text = new StreamReader(path);
						Console.WriteLine(text.ReadToEnd());
						text.Close();
					}
					else
					{
						Messages.Error("Error: For security reasons, the ability to read password files is disabled!");
					}
				}
				else
				{
					Messages.Error("Error: File doesn't exists!");
				}
			}
		}
		public static int GetLines(string path)
        {
			return File.ReadLines(path).Count();
		}
		public static bool DoesItContain(string what, string path)
        {
			StreamReader read = new StreamReader(path);
			if (read.ReadToEnd().Contains(what)) { return true; }
			return false;
        }
		public static void Write(string text, string file)
		{
			string path = VSFS.ToRelPath(VSFS.act_dir + file);
			StreamWriter doc = new StreamWriter(path);
			doc.Write(text);
			doc.Close();
		}
	}
}