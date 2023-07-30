/// 
/// VSFS (Virtual Syntax File System) Driver, Last modified: 2023. 07. 30.
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
using Sys = Cosmos.System;
using System.IO;
using Cosmos.System.FileSystem;
using Cosmos.HAL;
using Seting_DOS;
using IL2CPU.API.Attribs;
using System.Xml.Linq;
using Seting_DOS.Services;
using Cosmos.System.FileSystem.VFS;

namespace Seting_DOS.Drivers
{
	public static class VSFS
	{
		static CosmosVFS fs;
		public const string root_dir = @"0:\";
		public static string cur_dir = @"0:\";
		public static string act_dir = "/0/";

		public static string[] Load()
		{
			try
			{
				fs = new CosmosVFS();
				Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
				foreach (var part in Cosmos.HAL.BlockDevice.Partition.Partitions)
				{
					if (part.Type == Cosmos.HAL.BlockDevice.BlockDeviceType.RemovableCD)
					{
						var s = new Sys.FileSystem.ISO9660.ISO9660FileSystem(part, "1:\\", (long)part.BlockSize);
						var e = new Sys.FileSystem.ISO9660.ISO9660FileSystemFactory();
					}
				}
			}
			catch (Exception e)
			{
				string[] error = { "error", e.Message };
				return error;
			}
			string[] ok = { "done", "VSFS Filesystem driver loaded" };
			return ok;
		}
		public static void ListContent(string sdir = ".")
		{
			if (sdir == ".")
			{
				int fNum = 0;
				int dNum = 0;
				string[] dirs = Directory.GetDirectories(cur_dir);
				foreach (var dir in dirs)
				{
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.Write("                       Folder");
					Console.SetCursorPosition(0, Console.GetCursorPosition().Top);
					Console.ForegroundColor = ConsoleColor.Blue;
					Console.Write(dir.Replace(cur_dir, ""));
					Console.Write("\n");
					dNum++;
				}
				string[] files = Directory.GetFiles(cur_dir);
				foreach (var file in files)
				{
					Console.ForegroundColor = ConsoleColor.Yellow;
					string type = FileTypes.GiveTypeOwO(GetFileExtension(file));
					if (file.Length >  20) { Console.Write("                       " + "File"); }
					int y = Console.GetCursorPosition().Top;
                    Console.SetCursorPosition(0, y);
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write(file.Replace(cur_dir, ""));
                    Console.ForegroundColor = ConsoleColor.Yellow;
					if (file.Length > 20) { Console.Write("   " + type); }
					else { Console.SetCursorPosition(23, y); Console.Write(type); }
                    Console.Write("\n");
					fNum++;
				}
				Console.ForegroundColor = ConsoleColor.Magenta;
				Console.Write("{0} folder", dNum);
				if (dNum != 1) { Console.Write("s"); }
				Console.Write(", {0} file", fNum);
				if (fNum != 1) { Console.Write("s"); }
				Console.Write("\n");
				Console.ForegroundColor = ConsoleColor.White;
			}
		}
		public static void ChangeDir(string dest = ".")
		{
			if (dest == ".") { Console.WriteLine("Current folder: {0}", act_dir); }
			else if (dest == "..")
			{
				if(Directory.GetParent(cur_dir) != null)
				{
					cur_dir = Directory.GetParent(cur_dir).FullName;
					act_dir = ToVirtPath(cur_dir);
					if (!act_dir.EndsWith("/")) { act_dir += "/"; }
					if (!cur_dir.EndsWith("\\")) { cur_dir += "\\"; }
				}
			}
			else if (dest.StartsWith("/"))
			{
				if (dest == "/") { act_dir = "/0/"; cur_dir = "0:\\"; }
				else if (Directory.Exists(ToRelPath(dest)))
				{
					act_dir = dest;
					cur_dir = ToRelPath(act_dir);
					if (!act_dir.EndsWith("/")) { act_dir += "/"; }
					if (!cur_dir.EndsWith("\\")) { cur_dir += "\\"; }
				}
				else
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Error: Folder doesn't exists!");
					Beep.Sound.Error();
					Console.ForegroundColor = ConsoleColor.White;
				}
			}
			else if (dest == "\\") { act_dir = "/0/"; cur_dir = "0:\\"; }
			else
			{
				if (Directory.Exists(ToRelPath(act_dir + dest)))
				{
					act_dir = act_dir + dest;
					if (!act_dir.EndsWith("/")) { act_dir += "/"; }
					cur_dir = ToRelPath(act_dir);
					if (!cur_dir.EndsWith("\\")) { cur_dir += "\\"; }
				}
				else
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Error: Folder doesn't exists!");
					Beep.Sound.Error();
					Console.ForegroundColor = ConsoleColor.White;
				}
			}
		}
		public static void MakeDir(string name, bool noFeedBack = false, bool noPathCombine = false)
		{
            string path = name;
            if (!noPathCombine) { path = ToRelPath(act_dir + name.Replace(" ", "-").Replace("_", "-")); }
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
				if (!noFeedBack)
				{
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine("Folder '{0}' were created successfully!", name.Remove(0, name.LastIndexOf("\\")));
					Console.ForegroundColor = ConsoleColor.White;
				}
			}
			else
			{
				if (!noFeedBack)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Error: Folder already exists!");
					Console.ForegroundColor = ConsoleColor.White;
				}
			}
		}
		public static void RemoveDir(bool force, string name, bool noFeedBack = false)
		{
			string path = name;
			if (!name.StartsWith("0:\\")) { path = ToRelPath(act_dir + name); }
			if (Directory.Exists(path))
			{
				string[] dirs = Directory.GetDirectories(path);
				int dNum = 0;
				foreach (var dir in dirs) { dNum++; }
				string[] files = Directory.GetFiles(path);
				int fNum = 0;
				foreach (var file in files) { fNum++; }
				if (dNum > 0 || fNum > 0)
				{
					if (force)
					{
						Directory.Delete(path, true);
						if (!noFeedBack)
						{
							Console.ForegroundColor = ConsoleColor.Green;
							Console.WriteLine("Folder '{0}' and it's content(s) were deleted successfully!", name);
							Console.ForegroundColor = ConsoleColor.White;
						}
					}
					else
					{
						if (!noFeedBack)
						{
							Console.ForegroundColor = ConsoleColor.Red;
							Console.WriteLine("Error: Folder is not empty!");
							Console.ForegroundColor = ConsoleColor.Blue;
							Console.WriteLine("Tip: To delete a non-empty folder use the -r or the /f switch!");
							Console.ForegroundColor = ConsoleColor.White;
						}
					}
				}
				else
				{
					Directory.Delete(path);
					if (!noFeedBack)
					{
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine("Folder '{0}' were deleted successfully!", name);
						Console.ForegroundColor = ConsoleColor.White;
					}
				}
			}
			else
			{
				if (!noFeedBack)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Error: Folder doesn't exists!");
					Console.ForegroundColor = ConsoleColor.White;
				}
			}
		}
		public static void RemoveFile(string name, bool noAsk = false, bool noFeedBack = false)
		{
			string path = ToRelPath(act_dir + name);
			if (File.Exists(path))
			{
				if (noAsk)
				{
					File.Delete(path);
					if (!noFeedBack)
					{
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine("File '{0}' were deleted successfully!", name);
						Console.ForegroundColor = ConsoleColor.White;
					}
				}
				else
				{
					Console.Write("Delete: {0}\nAre you sure? [Y/N]: ", name);
					ConsoleKeyInfo key = Console.ReadKey();
					Console.Write("\n");
					if (key.Key == ConsoleKey.Y)
					{
						File.Delete(path);
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine("File '{0}' were deleted successfully!", name);
						Console.ForegroundColor = ConsoleColor.White;
					}
				}
			}
			else
			{
				if (!noFeedBack)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Error: File doesn't exists!");
					Beep.Sound.Error();
					Console.ForegroundColor = ConsoleColor.White;
				}
			}
		}
		public static void Copy(string sourceName, string targetName, bool noFeedBack = false, bool multiple = false)
		{
			//To do: Implement working file copy
			if (!multiple)
			{
				if (!sourceName.StartsWith("/0/")) { sourceName = act_dir + sourceName; }
				if (!targetName.StartsWith("/0/")) { targetName = act_dir + targetName; }
				sourceName = ToRelPath(sourceName);
				targetName = ToRelPath(targetName);
			}
			if (File.Exists(sourceName))
			{
				if (File.Exists(targetName))
				{
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.Write("Warning: Target file \"{0}\" already exists! Overwrite? [Y/N]: ", Path.GetFileName(targetName));
					Beep.Sound.Warning();
					ConsoleKeyInfo choice = Console.ReadKey();
					if (choice.Key == ConsoleKey.Y) { RemoveFile(targetName, true, true); }
					else { Console.Write("\n"); goto done; }
                    Console.Write("\n");
                    Console.ForegroundColor = ConsoleColor.White;
				}
				File.Copy(sourceName, targetName);
				if (!noFeedBack)
				{
					Console.ForegroundColor = ConsoleColor.White;
					Console.WriteLine("{0}=>{1}", Path.GetFileName(sourceName), Path.GetFileName(targetName));
                    Console.ForegroundColor = ConsoleColor.Green;
                    if (!multiple) { Console.WriteLine("File copied successfully!"); }
					Console.ForegroundColor = ConsoleColor.White;
				}
			done:;
			}
			else if (sourceName.Contains("*.*"))
			{
				sourceName = sourceName.Replace("*.*", "");
				string[] files = Directory.GetFiles(sourceName);
				if (File.Exists(targetName)) { targetName = Path.GetDirectoryName(targetName); }
                if (!targetName.EndsWith("\\")) { targetName = targetName + "\\"; }
                foreach (var file in files)
				{
					Copy(sourceName + file, targetName + file, noFeedBack, true);
				}
				if (!noFeedBack)
				{
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine("Files copied successfully!");
					Console.ForegroundColor = ConsoleColor.White;
				}
            }
			else if (Directory.Exists(sourceName))
			{
				if (Directory.Exists(targetName))
				{
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.Write("Warning: Target folder already exists! Overwrite? [Y/N]: ");
					Beep.Sound.Warning();
					ConsoleKeyInfo choice = Console.ReadKey();
					if (choice.Key == ConsoleKey.Y) { RemoveDir(true, targetName, true); }
					else { Console.Write("\n"); goto done; }
					Console.Write("\n");
					Console.ForegroundColor = ConsoleColor.White;
				}
				Console.WriteLine("SRC: {0}, TRG: {1}", sourceName, targetName); Console.ReadKey();
				MakeDir(targetName, true, true);
				if (!sourceName.EndsWith("\\")) { sourceName = sourceName + "\\"; }
				Copy(sourceName + "*.*", targetName, true, true);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Folder copied successfully!");
                Console.ForegroundColor = ConsoleColor.White;
			done:;
            }
			else
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("T: {0}", sourceName);
				Console.WriteLine("Error: Source file or folder doesn't exists!");
				Beep.Sound.Error();
				Console.ForegroundColor = ConsoleColor.White;
			}
		}
		public static void EmptyRootPartition()
		{
			string[] dirs = Directory.GetDirectories(root_dir);
			string[] files = Directory.GetFiles(root_dir);
			foreach (var dir in dirs)
			{
				string path = "0:\\" + dir;
				if (Directory.Exists(path))
				{
					RemoveDir(true, path, true);
				}
			}
			foreach (var file in files)
			{
				File.Delete(root_dir + file);
			}
		}
		public static string GetFileExtension(string file)
		{
            string[] ext = file.Split('.');
            return ext[ext.Length - 1];
        }
		#region Import resourceStream
		[ManifestResourceStream(ResourceName = "Seting_DOS.Files.mscorlib.dll")]
		public static byte[] mscorlib;
		[ManifestResourceStream(ResourceName = "Seting_DOS.Files.dotNetTest.dll")]
		public static byte[] dotnettest;
		[ManifestResourceStream(ResourceName = "Seting_DOS.Files.shutdownSound.beep")]
		public static byte[] shutdownSound;
		[ManifestResourceStream(ResourceName = "Seting_DOS.Files.startSound.beep")]
		public static byte[] startSound;
		[ManifestResourceStream(ResourceName = "Seting_DOS.Files.errorSound.beep")]
		public static byte[] errorSound;
		[ManifestResourceStream(ResourceName = "Seting_DOS.Files.warnSound.beep")]
		public static byte[] warnSound;
		[ManifestResourceStream(ResourceName = "Seting_DOS.Files.questionSound.beep")]
		public static byte[] questionSound;
        [ManifestResourceStream(ResourceName = "Seting_DOS.Files.main.mze")]
        public static byte[] mainMaze;
        [ManifestResourceStream(ResourceName = "Seting_DOS.Files.license.txt")]
        public static byte[] licenseFile;
        #endregion
        public static void CopyAllResourceStreams()
		{
			using var startbeep = new BinaryWriter(File.OpenWrite(@"0:\SDOS\System\startSound.beep"));
			startbeep.Write(startSound);
			startbeep.Close();
			using var shutbeep = new BinaryWriter(File.OpenWrite(@"0:\SDOS\System\shutdownSound.beep"));
			shutbeep.Write(shutdownSound);
			shutbeep.Close();
			using var errbeep = new BinaryWriter(File.OpenWrite(@"0:\SDOS\System\errorSound.beep"));
			errbeep.Write(errorSound);
			errbeep.Close();
			using var warnbeep = new BinaryWriter(File.OpenWrite(@"0:\SDOS\System\warnSound.beep"));
			warnbeep.Write(warnSound);
			warnbeep.Close();
			using var questbeep = new BinaryWriter(File.OpenWrite(@"0:\SDOS\System\questionSound.beep"));
			questbeep.Write(questionSound);
			questbeep.Close();
            using var mainmze = new BinaryWriter(File.OpenWrite(@"0:\SDOS\ProgramData\MazeGame\main.mze"));
            mainmze.Write(mainMaze);
            mainmze.Close();
            using var licfile = new BinaryWriter(File.OpenWrite(@"0:\SDOS\etc\license.txt"));
            licfile.Write(licenseFile);
            licfile.Close();
        }
		public static string ToVirtPath(string relPath)
		{
			return "/" + relPath.Replace("\\", "/").Replace(":", "");
		}
		public static string ToRelPath(string virtPath)
		{
			if (virtPath.StartsWith("./"))
			{
				virtPath = act_dir + virtPath.Remove(0, 2);
			}
			return virtPath.Replace("/", "\\").Remove(0, 1).Insert(1, ":");
		}
		public static void Zerosix(bool o = false)
		{
			//ZEROSIX EASTER EGG
			string[] date = RTC.GetDate();
			if (o) { date[1] = "06"; }
			if (date[1] == "06")
			{
				StreamReader pmft = new StreamReader(@"0:\SDOS\preferences\pmft.pref");
				string type = pmft.ReadLine();
				pmft.Close();
				if (type == "progress")
				{
					Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("██"); Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write("██"); Console.ForegroundColor = ConsoleColor.Black; Console.Write("██"); Console.ForegroundColor = ConsoleColor.Red; Console.Write("██████████████████"); Console.Write("\n");
					Console.ForegroundColor = ConsoleColor.Magenta; Console.Write("██"); Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("██"); Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write("██"); Console.ForegroundColor = ConsoleColor.Black; Console.Write("██"); Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write("████████████████"); Console.Write("\n");
					Console.ForegroundColor = ConsoleColor.White; Console.Write("██"); Console.ForegroundColor = ConsoleColor.Magenta; Console.Write("██"); Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("██"); Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write("██"); Console.ForegroundColor = ConsoleColor.Black; Console.Write("██"); Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("██████████████"); Console.Write("\n");
					Console.ForegroundColor = ConsoleColor.White; Console.Write("██"); Console.ForegroundColor = ConsoleColor.Magenta; Console.Write("██"); Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("██"); Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write("██"); Console.ForegroundColor = ConsoleColor.Black; Console.Write("██"); Console.ForegroundColor = ConsoleColor.DarkGreen; Console.Write("██████████████"); Console.Write("                 Type pmft to change flag!"); Console.Write("\n");
					Console.ForegroundColor = ConsoleColor.Magenta; Console.Write("██"); Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("██"); Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write("██"); Console.ForegroundColor = ConsoleColor.Black; Console.Write("██"); Console.ForegroundColor = ConsoleColor.DarkBlue; Console.Write("████████████████"); Console.Write("\n");
					Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("██"); Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write("██"); Console.ForegroundColor = ConsoleColor.Black; Console.Write("██"); Console.ForegroundColor = ConsoleColor.DarkMagenta; Console.Write("██████████████████"); Console.Write("\n");
				}
				if (type == "rainbow")
				{
					Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.DarkYellow; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.DarkGreen; Console.Write("████████████████████████"); Console.Write("                 Type pmft to change flag!"); Console.Write("\n");
					Console.ForegroundColor = ConsoleColor.DarkBlue; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.DarkMagenta; Console.WriteLine("████████████████████████");
				}
				if (type == "bi")
				{
					Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.DarkMagenta; Console.Write("████████████████████████"); Console.Write("                 Type pmft to change flag!"); Console.Write("\n");
					Console.ForegroundColor = ConsoleColor.DarkBlue; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.DarkBlue; Console.WriteLine("████████████████████████");
				}
				if (type == "pan")
				{
					Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("████████████████████████"); Console.Write("                 Type pmft to change flag!"); Console.Write("\n");
					Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine("████████████████████████");
				}
				if (type == "gay")
				{
					Console.ForegroundColor = ConsoleColor.DarkGreen; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.White; Console.Write("████████████████████████"); Console.Write("                 Type pmft to change flag!"); Console.Write("\n");
					Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.Blue; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.DarkBlue; Console.WriteLine("████████████████████████");
				}
				if (type == "lesbian")
				{
					Console.ForegroundColor = ConsoleColor.DarkRed; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.White; Console.Write("████████████████████████"); Console.Write("                 Type pmft to change flag!"); Console.Write("\n");
					Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.DarkMagenta; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.DarkRed; Console.WriteLine("████████████████████████");
				}
				if (type == "trans")
				{
					Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.White; Console.Write("████████████████████████"); Console.Write("                 Type pmft to change flag!"); Console.Write("\n");
					Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine("████████████████████████");
				}
				if (type == "nonbinary")
				{
					Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.DarkMagenta; Console.Write("████████████████████████"); Console.Write("                 Type pmft to change flag!"); Console.Write("\n");
					Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("████████████████████████");
				}
				if (type == "ace")
				{
					Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.Gray; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.White; Console.Write("████████████████████████"); Console.Write("                 Type pmft to change flag!"); Console.Write("\n");
					Console.ForegroundColor = ConsoleColor.DarkMagenta; Console.WriteLine("████████████████████████");
				}
				if (type == "aro")
				{
					Console.ForegroundColor = ConsoleColor.DarkGreen; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.White; Console.Write("████████████████████████"); Console.Write("                 Type pmft to change flag!"); Console.Write("\n");
					Console.ForegroundColor = ConsoleColor.Gray; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("████████████████████████");
				}
				if (type == "demiace")
				{
					Console.ForegroundColor = ConsoleColor.Black; Console.Write("██"); Console.ForegroundColor = ConsoleColor.White; Console.Write("██████████████████████"); Console.Write("\n");
					Console.ForegroundColor = ConsoleColor.Black; Console.Write("████"); Console.ForegroundColor = ConsoleColor.White; Console.Write("████████████████████"); Console.Write("\n");
					Console.ForegroundColor = ConsoleColor.Black; Console.Write("██████"); Console.ForegroundColor = ConsoleColor.DarkMagenta; Console.Write("██████████████████"); Console.Write("                 Type pmft to change flag!"); Console.Write("\n");
					Console.ForegroundColor = ConsoleColor.Black; Console.Write("████"); Console.ForegroundColor = ConsoleColor.Gray; Console.Write("████████████████████"); Console.Write("\n");
					Console.ForegroundColor = ConsoleColor.Black; Console.Write("██"); Console.ForegroundColor = ConsoleColor.Gray; Console.Write("██████████████████████"); Console.Write("\n");
				}
				if (type == "demiaro")
				{
					Console.ForegroundColor = ConsoleColor.Black; Console.Write("██"); Console.ForegroundColor = ConsoleColor.White; Console.Write("██████████████████████"); Console.Write("\n");
					Console.ForegroundColor = ConsoleColor.Black; Console.Write("████"); Console.ForegroundColor = ConsoleColor.White; Console.Write("████████████████████"); Console.Write("\n");
					Console.ForegroundColor = ConsoleColor.Black; Console.Write("██████"); Console.ForegroundColor = ConsoleColor.DarkGreen; Console.Write("██████████████████"); Console.Write("                 Type pmft to change flag!"); Console.Write("\n");
					Console.ForegroundColor = ConsoleColor.Black; Console.Write("████"); Console.ForegroundColor = ConsoleColor.Gray; Console.Write("████████████████████"); Console.Write("\n");
					Console.ForegroundColor = ConsoleColor.Black; Console.Write("██"); Console.ForegroundColor = ConsoleColor.Gray; Console.Write("██████████████████████"); Console.Write("\n");
				}
				if (type == "genderfluid")
				{
					Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.DarkMagenta; Console.Write("████████████████████████"); Console.Write("                 Type pmft to change flag!"); Console.Write("\n");
					Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.DarkBlue; Console.WriteLine("████████████████████████");
				}
				if (type == "intersex")
				{
					Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("█████████"); Console.ForegroundColor = ConsoleColor.DarkMagenta; Console.Write("██████"); Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("█████████");
					Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("████████"); Console.ForegroundColor = ConsoleColor.DarkMagenta; Console.Write("█"); Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("██████"); Console.ForegroundColor = ConsoleColor.DarkMagenta; Console.Write("█"); Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("████████");
					Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("████████"); Console.ForegroundColor = ConsoleColor.DarkMagenta; Console.Write("█"); Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("██████"); Console.ForegroundColor = ConsoleColor.DarkMagenta; Console.Write("█"); Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("████████"); Console.Write("                 Type pmft to change flag!"); Console.Write("\n");
					Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("█████████"); Console.ForegroundColor = ConsoleColor.DarkMagenta; Console.Write("██████"); Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("█████████");
					Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("████████████████████████");
				}
				if (type == "agender")
				{
					Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.Gray; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("████████████████████████"); 
					Console.ForegroundColor = ConsoleColor.Green; Console.Write("████████████████████████"); Console.Write("                 Type pmft to change flag!"); Console.Write("\n");
					Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.Gray; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("████████████████████████");
				}
				if (type == "aroace")
				{
					Console.ForegroundColor = ConsoleColor.DarkYellow; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.White; Console.Write("████████████████████████"); Console.Write("                 Type pmft to change flag!"); Console.Write("\n");
					Console.ForegroundColor = ConsoleColor.Blue; Console.WriteLine("████████████████████████");
					Console.ForegroundColor = ConsoleColor.DarkBlue; Console.WriteLine("████████████████████████");
				}
			}
		}
		public static void Pmft()
		{
			Console.WriteLine("=== Choose flag type ===");
			Console.WriteLine("[0] Progress flag     [1] Rainbow flag");
			Console.WriteLine("[2] Bi flag           [3] Pan flag");
			Console.WriteLine("[4] Gay flag          [5] Lesbian flag");
			Console.WriteLine("[6] Trans flag        [7] Nonbinary flag");
			Console.WriteLine("[8] Asexual flag      [9] Aromantic flag");
			Console.WriteLine("[A] Demisexual flag   [B] Demiromantic flag");
			Console.WriteLine("[C] Genderfluid flag  [D] Intersex flag");
			Console.WriteLine("[E] Agender flag      [F] Aroace flag");
			Console.WriteLine("Sorry, no other flags :(.\nAnd also sorry if yours is not perfect. It's because of color limitations.");
			Console.WriteLine("If you want to turn this off modify the source code and recompile yourself lol");
			ConsoleKeyInfo key;
			string choice = "";
			while (true)
			{
				key = Console.ReadKey(true);
				if (key.Key == ConsoleKey.D0) { choice = "progress"; break; }
				if (key.Key == ConsoleKey.D1) { choice = "rainbow"; break; }
				if (key.Key == ConsoleKey.D2) { choice = "bi"; break; }
				if (key.Key == ConsoleKey.D3) { choice = "pan"; break; }
				if (key.Key == ConsoleKey.D4) { choice = "gay"; break; }
				if (key.Key == ConsoleKey.D5) { choice = "lesbian"; break; }
				if (key.Key == ConsoleKey.D6) { choice = "trans"; break; }
				if (key.Key == ConsoleKey.D7) { choice = "nonbinary"; break; }
				if (key.Key == ConsoleKey.D8) { choice = "ace"; break; }
				if (key.Key == ConsoleKey.D9) { choice = "aro"; break; }
				if (key.Key == ConsoleKey.A) { choice = "demiace"; break; }
				if (key.Key == ConsoleKey.B) { choice = "demiaro"; break; }
				if (key.Key == ConsoleKey.C) { choice = "genderfluid"; break; }
				if (key.Key == ConsoleKey.D) { choice = "intersex"; break; }
				if (key.Key == ConsoleKey.E) { choice = "agender"; break; }
				if (key.Key == ConsoleKey.F) { choice = "aroace"; break; }
			}
			StreamWriter pmft = new StreamWriter(@"0:\SDOS\preferences\pmft.pref");
			pmft.Write(choice);
			pmft.Close();
		}
	}
}