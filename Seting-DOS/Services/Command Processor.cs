/// 
/// Command handler, Last modified: 2023. 07. 30.
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
using System.IO;
using Seting_DOS.Apps;
using Seting_DOS.Drivers;
using Seting_DOS.TextUI;
using System.Collections.Generic;

namespace Seting_DOS.Services
{
	public static class CmdProc
	{
		public static void Process(string command)
		{
			#region Preparation
			string[] cmdSplit = command.Split(' ');
			string cmd = cmdSplit[0].ToLower();
			List<string> tempArgs = new List<string>();
			for (int i = 1; i < cmdSplit.Length; i++)
			{
				if (cmdSplit[i] != "" && cmdSplit[i] != " ") { tempArgs.Add(cmdSplit[i].ToLower()); }
			}
			string[] args = tempArgs.ToArray();
			#endregion
			#region Easter eggs. Find them normally, don't look inside
			if (command == "OwO")
			{
				Console.WriteLine("OwO UwU :3");
			}
			else if (command.ToLower() == "sus")
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
			else if (cmd == "shutdown") //Shut down system
			{
				if (args.Length == 0 || args[0] == "-s") { Power.Shutdown.System(); }
				else if (args[0] == "-acpi") { Power.Shutdown.ACPI(); }
				else if (args[0] == "-cpu") { Power.Shutdown.CPU(); }
				else
				{
					Messages.Error("Invalid argument(s)!");
				}
			}
			else if (cmd == "restart" || cmd == "reboot") //Reboot system
			{
				if (args.Length == 0 || args[0] == "-s") { Power.Reboot.System(); }
				else if (args[0] == "-acpi") { Power.Reboot.ACPI(); }
				else if (args[0] == "-cpu") { Power.Reboot.CPU(); }
				else
				{
					Messages.Error("Invalid argument(s)!");
				}
			}
			else if (cmd == "logoff")
			{
				LogonUI.LockScreen();
			}
			#endregion
			#region Filesystem related commands
			else if (cmd == "reset")
			{
				File.Delete(@"0:\SDOS\System\installed.idp");
				Process("reboot");
			}
			else if (cmd == "dir" || cmd == "ls")
			{
				VSFS.ListContent();
			}
			else if (cmd == "cd..") { VSFS.ChangeDir(".."); }
			else if (cmd == "cd")
			{
				if (args.Length == 0) { VSFS.ChangeDir(); }
				else { VSFS.ChangeDir(args[0]); }
			}
			else if (cmd == "md" || cmd == "mkdir")
			{
				if (args.Length == 0)
				{
					Messages.Error("Error: Missing parameter: Folder name");
				}
				else if (args.Length > 1)
				{
					Messages.Error("Error: Too many parameters!\nTip: Do not use any space in a file or folder name!");
				}
				else
				{
					string name = args[0];
					if (!name.StartsWith("/")) { name = VSFS.act_dir + name; }
					name = VSFS.ToRelPath(name).Replace("_", "-");
					VSFS.MakeDir(name, false, true);
				}
			}
			else if (cmd == "rmdir" || cmd == "rd")
			{
				if (args.Length == 0)
				{
					Messages.Error("Error: Missing parameter: Folder name");
				}
				else if (args.Length == 1 && (args[0] == "-r" | args[0] == "/f"))
				{
					Messages.Error("Error: Missing parameter: Folder name");
				}
				else
				{
					if (args[0] == "-r" || args[0] == "/f") { VSFS.RemoveDir(true, args[1]); }
					else { VSFS.RemoveDir(false, args[0]); }
				}
			}
			else if (cmd == "del" || cmd == "rm")
			{
				if (args.Length == 0)
				{
					Messages.Error("Error: Missing parameter: File name");
				}
				else if (args.Length == 1 && (args[0] == "-q" | args[0] == "/q"))
				{
					Messages.Error("Error: Missing parameter: File name");
				}
				else
				{
					if (args[0] == "-q" || args[0] == "/q") { VSFS.RemoveFile(args[1], true); }
					else { VSFS.RemoveFile(args[0]); }
				}
			}
			else if (cmd == "copy")
			{
				if (args.Length == 0)
				{
					Messages.Error("Error: Missing parameters: Source and target file or folder name");
				}
				else if (args.Length == 1)
				{
					Messages.Error("Error: Missing parameter: Target file or folder name");
				}
				else
				{
					VSFS.Copy(args[0], args[1]);
				}
			}
			else if (cmd == "move")
			{
				if (args.Length == 0)
				{
					Messages.Error("Error: Missing parameters: Source and target file or folder name");
				}
				else if (args.Length == 1)
				{
					Messages.Error("Error: Missing parameter: Target file or folder name");
				}
				else
				{
					//No move command yet :3
				}
			}
			else if (cmd == "ren")
			{
				if (args.Length == 0)
				{
					Messages.Error("Error: Missing parameters: Source and target file or folder name");
				}
				else if (args.Length == 1)
				{
					Messages.Error("Error: Missing parameter: New name");
				}
				else
				{
					//No rename command yet :3
				}
			}
			else if (cmd == "cat" || cmd == "type")
			{
				if (args.Length == 0)
				{
					TextOperations.Read();
				}
				else if (args.Length == 2 && args[0] == "furry" && args[1] == "fox")
				{
					TextOperations.Read("furry fox");
				}
				else
				{
					TextOperations.Read(args[0]);
				}
			}
			else if (cmd == "write")
			{
				if (args.Length == 0)
				{
					Messages.Error("Error: Missing parameters: File name and text to be written");
				}
				else if (args.Length == 1)
				{
					Messages.Warning("Warning: An empty file was created as no text was given!");
					TextOperations.Write("", args[0]);
				}
				else
				{
					TextOperations.Write(command.Remove(0, cmd.Length + args[0].Length + 2), args[0]);
				}
			}
			#endregion
			#region System commands
			else if (cmd == "clr" || cmd == "cls" || cmd == "clear")
			{
				Console.Clear();
			}
			else if (cmd == "crash")
			{
				if (args.Length == 0 || (args.Length == 1 & args[0] == "k"))
				{
					Exception ex = new Exception("User-initiated kernel crash");
					CrashUI.KernelCrash(ex);
				}
				else if (args.Length == 1 && args[0] == "a")
				{
					Exception ex = new Exception("User-initiated application crash");
					CrashUI.ApplicationCrash(ex);
				}
				else
				{
					Exception ex = new Exception(command.Remove(0, cmd.Length + args[0].Length + 2));
					if (args[0] == "u")
					{
						CrashUI.ApplicationCrash(ex);
					}
					else
					{
						CrashUI.KernelCrash(ex);
					}
				}
			}
			else if (cmd == "postinstall")
			{
				PostInstall.Start();
			}
			else if (cmd == "envvars")
			{
				EnvVars.WriteThem();
			}
			else if (cmd == "pmft")
			{
				VSFS.Pmft();
			}
			else if (cmd == "zerosix")
			{
				VSFS.Zerosix(true);
			}
			else if (cmd == "cmdhistory")
			{
				CommandHistoryManager.PrintAll();
			}
			else if (cmd == "debug")
			{
				Console.WriteLine("Debug command recieved!");
				if (args.Length > 0) { TextEditTEMP.StartApp(args[0]); }
				else { TextEditTEMP.StartApp(); }
				//Console.WriteLine("No new test feature... OwO");
			}
			#endregion
			#region Applications
			else if (cmd == "calc")
			{
				Calculator.StartApp();
			}
			else if (cmd == "credits")
			{
				Credits.Show();
			}
			else if (cmd == "neofetch")
			{
				Neofetch.Open();
			}
			else if (cmd == "play")
			{
				if (args.Length == 0)
				{
					Messages.Error("Error: Missing parameter: File name");
				}
				else
				{
					string path = VSFS.ToRelPath(VSFS.act_dir + args[0]);
					BeepMusicPlayer.MusicPlayer(path);
				}
			}
			else if (cmd == "edit")
			{
				if (args.Length == 0)
				{
					Messages.Error("Error: Missing parameter: File name");
				}
				else
				{
					string path = args[0];
					if (!path.StartsWith("/0/")) { path = VSFS.act_dir + path; }
					TextEditOLD.Start(VSFS.ToRelPath(path));
				}
			}
			else if (cmd == "maze" || cmd == "mazegame")
			{
				if (args.Length > 0)
				{
					MazeGame.Start(VSFS.ToRelPath(args[0]));
				}
				else { MazeGame.Start(); }
			}
			#endregion
			#region Alias test, incorrect operation message
			else
			{
				//Aliases are not supported yet :3
				if (cmd != "") { Messages.Custom("'" + cmd + "' is not a valid command!", ConsoleColor.Red); }
			}
			#endregion
		}
	}
}