/// 
/// Textfile operation services, Last modified: 2022. 10. 02.
/// Made for Seting-DOS, feel free to use any code from this
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
			string path = Drivers.VSFS.ToRelPath(Drivers.VSFS.act_dir + file);
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
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("Error: For security reasons, reading password files are disabled!");
						Console.ForegroundColor = ConsoleColor.White;
					}
				}
				else
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Error: File doesn't exists!");
					Console.ForegroundColor = ConsoleColor.White;
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