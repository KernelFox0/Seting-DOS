/// 
/// Command handler, Last modified: 2023. 11. 13.
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
			#region Easter eggs. Find them, don't look inside
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
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.Write("Are you sure? This will delete your data! [Y/N]: ");
				Console.ForegroundColor = ConsoleColor.White;
				ConsoleKeyInfo key = Console.ReadKey(); Console.Write("\n");
				if (key.Key != ConsoleKey.Y) { return; }
				if (EnvVars.hasPassword)
				{
					StreamReader p = new StreamReader(@"0:\Users\" + EnvVars.username + "\\password.pwd");
					string password = p.ReadToEnd();
					p.Close();
					Console.Write("Enter your password: ");
					string pass = Console.ReadLine();
					if (pass != password) { return; }
				}
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write("Are you really sure? This is not reversible! [Y/N]: ");
				Console.ForegroundColor = ConsoleColor.White;
				key = Console.ReadKey(); Console.Write("\n");
				if (key.Key != ConsoleKey.Y) { return; }
				Console.WriteLine("Deleting data...");
				File.Delete(@"0:\SDOS\System\installed.idp");
				try { VSFS.EmptyRootPartition(); }
				catch (Exception e)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Error while deleting data! After restarting make sure to choose Format because\n otherwise the install process can fail!\nError message: {0}", e.Message);
				}
				Console.ForegroundColor = ConsoleColor.Green;
				Beep.PCBeep(500);
				Beep.PCBeep(300);
				Console.Write("Done. Press any key to restart...");
				Console.ReadKey();
				Cosmos.System.Power.Reboot();
			}
			else if (cmd == "dir" || cmd == "ls")
			{
				if (args.Length == 0 || args[0] == ".") { VSFS.ListContent(); }
				else if (args[0] == "..") { VSFS.ListContent(".."); }
				else { string path = args[0]; if (!path.StartsWith("/")) { path = VSFS.act_dir + path; } VSFS.ListContent(VSFS.ToRelPath(path)); }
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
			else if (cmd == "copy" || cmd == "cp")
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
			else if (cmd == "move" || cmd == "mv")
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
					VSFS.Move(args[0], args[1]);
				}
			}
			else if (cmd == "ren" || cmd == "rename")
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
					VSFS.Rename(args[0], args[1]);
				}
			}
			else if (cmd == "cat" || cmd == "type" || cmd == "read")
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
			else if (cmd == "alias")
			{
				if (args.Length < 1 || args.Length > 3)
				{
					Messages.Error("Insufficient number or arguments!\nProper format: alias [ -s | -r ] <alias> [<command>]");
				}
				else if (args[0] == "-r")
				{
					if (args.Length != 2)
					{
						Messages.Error("Insufficient number or arguments!\nProper format: alias -r <alias>");
					}
					else
					{
						AliasManager.Delete(args[1]);
					}
				}
				else if (args[0] == "-s")
				{
					if (args[1] == "-r")
					{
						if (args.Length != 3)
						{
							Messages.Error("Insufficient number or arguments!\nProper format: alias -s -r <alias>");
						}
						else
						{
							AliasManager.Delete(args[2], true);
						}
					}
					else if (args.Length == 3)
					{
						AliasManager.Create(args[1], args[2], true);
					}
					else
					{
						Messages.Error("Insufficient number or arguments!\nProper format: alias -s [-r] <alias> [<command>]");
					}
				}
				else if (args[0] == "-e")
				{
					if (args.Length < 2) { Console.WriteLine("List of alias manager extra commands:\nreload - reloads aliases from file\nlist - print out the contents of the alias container\n\nSyntax: alias -e [option]"); }
					else if (args[1] == "reload")
					{
						AliasManager.Load();
					}
					else if (args[1] == "list")
					{
						string[] list = AliasManager.List();
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine("System aliases:");
						Console.ForegroundColor = ConsoleColor.White;
						Console.WriteLine(list[0]);
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine("User aliases:");
						Console.ForegroundColor = ConsoleColor.White;
						Console.WriteLine(list[1]);
					}
					else { Messages.Error("Unknown extra! Type alias -e for a list of extras."); }
				}
				else
				{
					AliasManager.Create(args[0], args[1]);
				}
			}
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
			else if (cmd == "cmdhistory" || cmd == "history")
			{
				CommandHistoryManager.PrintAll();
			}
			else if (cmd == "license")
			{
				if (args.Length == 0) { TextEdit.StartApp("/0/SDOS/etc/license.txt"); }
				else if (args[0] == "show")
				{
					if (args.Length == 1) { Messages.Error("Missing parameter! Use show c or show w!"); }
					else if (args[1] == "w")
					{
						Console.Write("Disclaimer of Warranty:\n\nTHERE IS NO WARRANTY FOR THE PROGRAM, TO THE EXTENT PERMITTED BY APPLICABLE LAW." +
							" EXCEPT WHEN OTHERWISE STATED IN WRITING THE COPYRIGHT HOLDERS AND/OR OTHER PARTIES" +
							" PROVIDE THE PROGRAM “AS IS” WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR" +
							" IMPLIED, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND" +
							" FITNESS FOR A PARTICULAR PURPOSE. THE ENTIRE RISK AS TO THE QUALITY AND PERFORMANCE" +
							" OF THE PROGRAM IS WITH YOU. SHOULD THE PROGRAM PROVE DEFECTIVE, YOU ASSUME THE" +
							" COST OF ALL NECESSARY SERVICING, REPAIR OR CORRECTION.\n");
					}
					else if (args[1] == "c")
					{
						Console.Write("You may convey a work based on the Program, or the modifications to produce it from the Program, in the form of" +
							" source code under the terms of section 4, provided that you also meet all of these conditions:" +
							"\n\n- a) The work must carry prominent notices stating that you modified it, and giving a relevant date." +
							"\n- b) The work must carry prominent notices stating that it is released under this License and any conditions added" +
							" under section 7. This requirement modifies the requirement in section 4 to “keep intact all notices”." +
							"\n- c) You must license the entire work, as a whole, under this License to anyone who comes into possession of a" +
							"copy. This License will therefore apply, along with any applicable section 7 additional terms, to the whole of the work, and all its parts, regardless of how they are packaged.\n"); Console.ReadKey();
							Console.Write(" This License gives no permission to license the work" +
							" in any other way, but it does not invalidate such permission if you have separately received it." +
							" - d) If the work has interactive user interfaces, each must display Appropriate Legal Notices; however, if the" +
							" Program has interactive interfaces that do not display Appropriate Legal Notices, your work need not make them\ndo so." +
							"\n\nA compilation of a covered work with other separate and independent works, which are not by their nature extensions" +
							" of the covered work, and which are not combined with it such as to form a larger program, in or on a volume of a" +
							" storage or distribution medium, is called an “aggregate” if the compilation and its resulting copyright are not used to" +
							" limit the access or legal rights of the compilation's users beyond what the individual works permit. Inclusion of a" +
							" covered work in an aggregate does not cause this License to apply to the other parts of the aggregate.\n");
					}
				}
				else { TextEdit.StartApp("/0/SDOS/etc/license.txt"); }
			}
			else if (cmd == "help")
			{
				TextEdit.StartApp("/0/SDOS/etc/help.txt");
			}
			else if (cmd == "preferences" || cmd == "prefs" || cmd == "settings")
			{
				PreferencesEditor.StartApp();
				Console.BackgroundColor = ConsoleColor.Black; Console.ForegroundColor = ConsoleColor.White; Console.Clear();
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
			else if (cmd == "edit" || cmd == "textedit" || cmd == "notepad")
			{
				if (args.Length > 0)
				{
					string path = args[0];
					if (!path.StartsWith("/")) { path = VSFS.act_dir + path; }
					TextEdit.StartApp(path);
				}
				else { TextEdit.StartApp(); }
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
				string alias = AliasManager.GetCmd(cmd);
				if (alias.Replace(" ", "") == "") { alias = cmd; }
				if (alias != cmd && cmd != "")
				{
					string aliasCmd = alias;
					for (int i = 0; i < args.Length; i++)
					{
						aliasCmd = aliasCmd + " " + args[i];
					}
					Process(aliasCmd);
				}
				else if (cmd != "") { Messages.Custom("'" + cmd + "' is not a valid command!", ConsoleColor.Red); }
			}
			#endregion
		}
	}
}