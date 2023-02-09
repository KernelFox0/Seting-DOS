/// 
/// Command handler, Last modified: 2022. 10. 19.
/// Made for Seting-DOS, feel free to use any code from this
/// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys = Cosmos.System;
using Cosmos.System.ExtendedASCII;
using System.IO;
using Cosmos.System.FileSystem;
using Cosmos.HAL;
using Seting_DOS;
using Seting_DOS.Drivers;
using Seting_DOS.Apps;
using Seting_DOS.Services;
using Seting_DOS.TextUI;

namespace Seting_DOS.Services
{
	public static class CommandHandler
	{
		public static void Handle(string typed)
		{
			string cmd = typed.ToLower();
			#region Easter egg. Find it normally, don't look inside
			if (typed == "OwO")
			{
				Console.WriteLine("*Notices your input* OwO what's this?");
			}
			else if (cmd == "sus")
			{
				Console.ForegroundColor = ConsoleColor.Red; Console.Write("a");
				Console.ForegroundColor = ConsoleColor.Green; Console.Write("m");
				Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("o");
				Console.ForegroundColor = ConsoleColor.Blue; Console.Write("g");
				Console.ForegroundColor = ConsoleColor.White; Console.Write("u");
				Console.ForegroundColor = ConsoleColor.Magenta; Console.Write("s");
				Console.ForegroundColor = ConsoleColor.White; Console.Write("\n");
			}
			#endregion
			else if (cmd.Replace(" ", "") == "shutdown") { Console.WriteLine("Bye! Shutting down..."); Beep.Sound.Shutdown(); Sys.Power.Shutdown(); }
			else if (cmd.Replace(" ", "") == "restart" || cmd.Replace(" ", "") == "reboot") { Console.WriteLine("Restarting..."); Beep.Sound.Shutdown(); Sys.Power.Reboot(); }
			else if (cmd.Replace(" ", "") == "reset") { File.Delete(@"0:\SDOS\System\installed.idp"); Handle("reboot"); }
			else if (cmd.Replace(" ", "") == "crash" || cmd.Replace(" ", "") == "crash k")
			{
				Exception ex = new Exception("User-initiated kernelspace crash");
				TextUI.CrashUI.KernelspaceCrash(ex);
			}
			else if (cmd.Replace(" ", "") == "crash u")
			{
				Exception ex = new Exception("User-initiated userspace crash");
				TextUI.CrashUI.UserspaceCrash(ex);
			}
			else if (cmd.StartsWith("crash k ") || cmd.StartsWith("crash u "))
			{
				Exception ex = new Exception(typed.Remove(0, 8));
				if (cmd.StartsWith("crash u"))
				{
					TextUI.CrashUI.UserspaceCrash(ex);
				}
				else
				{
					TextUI.CrashUI.KernelspaceCrash(ex);
				}
			}
			else if (cmd.Replace(" ", "") == "clr" || cmd.Replace(" ", "") == "cls" || cmd.Replace(" ", "") == "clear") { Console.Clear(); }
			else if (cmd.Replace(" ", "") == "dir" || cmd.Replace(" ", "") == "ls") { Drivers.VSFS.ListContent(); }
			else if (cmd.Replace(" ", "") == "cd") { Drivers.VSFS.ChangeDir(); }
			else if (cmd == "cd..") { Drivers.VSFS.ChangeDir(".."); }
			else if (cmd.StartsWith("cd ")) { Drivers.VSFS.ChangeDir(typed.Remove(0, 3)); }
			else if (cmd.Replace(" ", "") == "md" || cmd.Replace(" ", "") == "mkdir")
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Error: Missing parameter: Folder name");
				Console.ForegroundColor = ConsoleColor.White;
			}
			else if (cmd.StartsWith("md ")) { Drivers.VSFS.MakeDir(typed.Remove(0, 3)); }
			else if (cmd.StartsWith("mkdir ")) { Drivers.VSFS.MakeDir(typed.Remove(0, 6)); }
			else if (cmd.Replace(" ", "") == "rd" || cmd.Replace(" ", "") == "rm" || cmd == "rd /f" || cmd == "rm -rf" || cmd == "rd /f " || cmd == "rm -rf ")
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Error: Missing parameter: Folder name");
				Console.ForegroundColor = ConsoleColor.White;
			}
			else if (cmd.StartsWith("rd /f ")) { Drivers.VSFS.RemoveDir(true, typed.Remove(0, 6)); }
			else if (cmd.StartsWith("rm -rf ")) { Drivers.VSFS.RemoveDir(true, typed.Remove(0, 7)); }
			else if (cmd.StartsWith("rd ") || cmd.StartsWith("rm ")) { Drivers.VSFS.RemoveDir(false, typed.Remove(0, 3)); }
			else if (cmd.Replace(" ", "") == "neofetch") { Apps.Neofetch.Open(); }
			else if (cmd.Replace(" ", "") == "cat" || cmd.Replace(" ", "") == "type") { Services.TextOperations.Read(); }
			else if (cmd.StartsWith("cat ")) { Services.TextOperations.Read(typed.Remove(0, 4)); }
			else if (cmd.StartsWith("type ")) { Services.TextOperations.Read(typed.Remove(0, 5)); }
			else if (cmd.Replace(" ", "") == "logoff") { TextUI.LogonUI.LockScreen(); }
			else if (cmd.Replace(" ", "") == "postinstall") { TextUI.PostInstall.Start(); }
			else if (cmd.Replace(" ", "") == "envvars") { Services.EnvVars.WriteThem(); }
			else if (cmd.Replace(" ", "") == "write")
            {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Error: Missing parameters: File name, text to be written");
				Console.WriteLine("Correct syntax: write <filename> [text]");
				Console.ForegroundColor = ConsoleColor.White;
			}
			else if (cmd.StartsWith("write ") && cmd.Length > 6)
            {
				string name = typed.Remove(0, 6);
				name = name.Remove(name.IndexOf(" "));
				string text = typed.Remove(0, 6).Replace(name, "").Remove(0, 1);
				Services.TextOperations.Write(text, name);
            }
			else if (cmd.Replace(" ", "") == "del" || cmd == "del -q" || cmd == "del -q " || cmd == "del /q" || cmd == "del /q ")
            {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Error: Missing parameter: File name");
				Console.ForegroundColor = ConsoleColor.White;
			}
			else if (cmd.StartsWith("del -q ") || cmd.StartsWith("del /q ")) { Drivers.VSFS.RemoveFile(typed.Remove(0, 7), true); }
			else if (cmd.StartsWith("del ")) { Drivers.VSFS.RemoveFile(typed.Remove(0, 4)); }
			else if (cmd.Replace(" ", "") == "copy")
            {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Error: Missing parameter: File name");
				Console.ForegroundColor = ConsoleColor.White;
			}
			else if (cmd.Replace(" ", "") == "move")
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Error: Missing parameter: File/Folder name");
				Console.ForegroundColor = ConsoleColor.White;
			}
			else if (cmd.Replace(" ", "") == "ren")
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Error: Missing parameter: File/Folder name");
				Console.ForegroundColor = ConsoleColor.White;
			}
			else if (cmd.StartsWith("copy "))
            {
				string nocmd = typed.Remove(0, 5);
				string file1 = nocmd.Remove(nocmd.IndexOf(" "));
				string file2 = nocmd.Remove(0, file1.Length + 1); //nocmd.Remove(0, nocmd.IndexOf(" "));
				//Console.WriteLine("nocmd: {0}\nfile1: {1}\nfile2: {3}", nocmd, file1, file2);
				Console.ReadKey();
				VSFS.CopyFile(file1, file2);
			}
			else if (cmd.StartsWith("move "))
			{
				string nocmd = typed.Remove(0, 5);
				string path1 = nocmd.Remove(nocmd.IndexOf(" "));
				string path2 = nocmd.Remove(0, nocmd.IndexOf(" "));
			}
			else if (cmd.StartsWith("ren "))
			{
				string nocmd = typed.Remove(0, 4);
				string name1 = nocmd.Remove(nocmd.IndexOf(" "));
				string name2 = nocmd.Remove(0, nocmd.IndexOf(" "));
			}
			else if (cmd.StartsWith("edit ") && cmd.Length > 5)
			{
				string path = VSFS.ToRelPath(VSFS.act_dir + typed.Remove(0, 5));
				TextEdit.Start(path);
			}
			else if (cmd.Replace(" ", "") == "play")
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Error: Missing parameter: File name");
				Console.WriteLine("Correct syntax: play <filename>");
				Console.ForegroundColor = ConsoleColor.White;
			}
			else if (cmd.StartsWith("play ") && cmd.Length > 5)
			{
				string path = VSFS.ToRelPath(VSFS.act_dir + typed.Remove(0, 5));
				BeepMusicMaker.MusicPlayer(path);
			}
			else if (cmd.Replace(" ", "") == "calc")
            {
				Calculator.StartApp();
            }
			else if (cmd.Replace(" ", "") == "dotnet")
            {
				BootMSG.Write(DotNetParser.Load());
            }
			else if (cmd.StartsWith("dotnet "))
            {
				string path = typed.Remove(0, 7);
				if (!File.Exists(VSFS.ToRelPath(path))) { path = VSFS.act_dir + path; }
				path = VSFS.ToRelPath(path);
				DotNetParser.StartApp(path);
            }
			else if (cmd == "pmft")
			{
                /*StreamWriter pmft = new StreamWriter(@"0:\SDOS\preferences\pmft.pref");
                pmft.Write("progress");
                pmft.Close();*/
				VSFS.Pmft();
			}
			else if (cmd.Replace(" ", "") == "zerosix") { VSFS.Zerosix(true); }
			else if (cmd.Replace(" ", "") == "debug")
            {
				Console.WriteLine("GOT COMMAND");
				//VSFS.CopyAllResourceStreams();
				Console.WriteLine("No new test feature... OwO");
			} //Debug command is for debugging a new feature, the operations is always changed to it manually
		}
	}
}