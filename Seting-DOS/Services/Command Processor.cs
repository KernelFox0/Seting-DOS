/// 
/// Command handler, Last modified: 2022. 10. 19.
/// Made for Seting-DOS, feel free to use any code from this
/// 

using System;
using System.IO;
using Sys = Cosmos.System;
using HAL = Cosmos.HAL;
using Common = Cosmos.Common;
using Seting_DOS.Apps;
using Seting_DOS.Drivers;
using Seting_DOS.TextUI;

namespace Seting_DOS.Services
{
	public static class CmdProc
	{
		public static void Process(string command)
		{
			string shortC = command.ToLower().Trim(' '); //Processable version of command. All lowercase, no spaces.
			#region Easter egg. Find it normally, don't look inside
			if (command == "OwO")
			{
				Console.WriteLine("*Notices your input* OwO what's this?");
			}
			else if (shortC == "sus")
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
			#region Power related commands
			#region Shutdown
			else if (shortC == "shutdown") //Shut down system using Cosmos System
			{
				Power.Shutdown.System();
			}
			else if (shortC == "acpishutdown") //Shut down system using ACPI
			{
				Power.Shutdown.ACPI();
			}
			else if (shortC == "cpushutdown") //Shut down system by halting the CPU
			{
				Power.Shutdown.CPU();
			}
			#endregion
			#region Reboot
			if (shortC == "restart" || shortC == "reboot") //Reboot system using Cosmos System
			{
				Power.Reboot.System();
			}
			else if (shortC == "acpireboot") //Reboot system using ACPI
			{
				Power.Reboot.ACPI();
			}
			else if (shortC == "cpureboot") //Reboot system by rebooting the CPU
			{
				Power.Reboot.CPU();
			}
			#endregion
			#endregion
			#region Filesystem related commands
			else if (shortC == "reset")
			{
				File.Delete(@"0:\SDOS\System\installed.idp");
				Process("reboot");
			}
			else if (shortC == "dir" || shortC == "ls")
			{
				VSFS.ListContent();
			}
			else if (shortC.StartsWith("cd"))
			{
				if (shortC == "cd") { VSFS.ChangeDir(); }
				if (shortC == "cd..") { VSFS.ChangeDir(".."); }
				else { VSFS.ChangeDir(command.Substring(3)); }
			}
			else if (shortC.StartsWith("md") || shortC.StartsWith("mkdir"))
            {
				if (command.Length !> 3 || (shortC.StartsWith("mkdir") && command.Length !> 6))
                {
					Messages.Error("Error: Missing parameter: Folder name");
                }
				else if (shortC.StartsWith("md"))
                {
					string path = command.Substring(3);
					if (!path.StartsWith("/")) { path = VSFS.act_dir + path; }
					VSFS.MakeDir(VSFS.ToRelPath(path));
                }
            }
			#endregion
		}
	}
}