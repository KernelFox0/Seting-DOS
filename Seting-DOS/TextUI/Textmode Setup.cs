/// 
/// System setup, Last modified: 2022. 10. 07.
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

namespace Seting_DOS.TextUI
{
	public static class Setup
	{
		public static void Call()
		{
			Services.TextBase.DrawBase();
			#region Welcome screen draw
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.Clear();
			Console.BackgroundColor = ConsoleColor.Blue;
			Console.ForegroundColor = ConsoleColor.White;
			Console.SetCursorPosition(0, 0);
			Console.Write("                                                                                ");
			Console.Write(" Seting-DOS 2022 Textmode Setup                                                 ");
			Console.Write("================================                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("    Welcome to Seting-DOS! A simple operating system programmed in the COSMOS   ");
			Console.Write("    kernel. This installation process is simple and short, it's only for        ");
			Console.Write("    setting up your user account.                                               ");
			Console.Write("                                                                                ");
			Console.Write("    - To set up your computer now, press Enter                                  ");
			Console.Write("    - To enter testing (Live) mode, press L                                     ");
			Console.Write("    - To exit the setup and shut down the computer, press F3                    ");
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
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.Write(" ENTER=Continue   L=Live mode   F3=Quit and shutdown                           ");
			#endregion
			ConsoleKeyInfo key;
			int setupMode = -1;
			key = Console.ReadKey();
			if (key.Key == ConsoleKey.Enter) { setupMode = 0; }
			else if (key.Key == ConsoleKey.L) { setupMode = 1; }
			else if (key.Key == ConsoleKey.F3) { setupMode = -1; }
			else { Call(); }
			if (setupMode == -1) { Sys.Power.Shutdown(); }
			else if (setupMode == 1) { } //TODO: Implement live mode
			else { LicScr(); }
		}
		private static void LicScr()
		{
			#region License screen draw
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.Clear();
			Console.BackgroundColor = ConsoleColor.Blue;
			Console.ForegroundColor = ConsoleColor.White;
			Console.SetCursorPosition(0, 0);
			Console.Write("                                                                                ");
			Console.Write(" Seting-DOS 2022 License agreement                                              ");
			Console.Write("===================================                                             ");
			Console.Write("                                                                                ");
			Console.Write("    Seting-DOS(R) 2022 Home/Professional, COSMOS(R) Kernel                      ");
			Console.Write("                                                                                ");
			Console.Write("    END-USER LICENSE AGREEMENT FOR ALPHA SETING-DOS SOFTWARE                    ");
			Console.Write("                                                                                ");
			Console.Write("    IMPORTANT-READ CAREFULLY:                                                   ");
			Console.Write("    Do not make illegal copies!                                                 ");
			Console.Write("    Do not redistribute without permission                                      ");
			Console.Write("    Always credit the creator and the kernel maker (COSMOS) of this OS!         ");
			Console.Write("                                                                                ");
			Console.Write("    F8 - Agree the EULA and continue setting up                                 ");
			Console.Write("    ESC - Disagree and return to the welcome screen                             ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.Write(" F8=I agree  ESC=I do not agree                                                ");
			#endregion
			ConsoleKeyInfo key;
			key = Console.ReadKey();
			if (key.Key == ConsoleKey.F8) { FormAsk(); }
			else if (key.Key == ConsoleKey.Escape) { Call(); }
			else { LicScr(); }
		}
		private static void FormAsk()
		{
			#region Format screen draw
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.Clear();
			Console.BackgroundColor = ConsoleColor.Blue;
			Console.ForegroundColor = ConsoleColor.White;
			Console.SetCursorPosition(0, 0);
			Console.Write("                                                                                ");
			Console.Write(" Seting-DOS 2022 Textmode Setup                                                 ");
			Console.Write("================================                                                ");
			Console.Write("                                                                                ");
			Console.Write("    Formatting tool will open and guide you through formatting your drive.      ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                    === Formatting Tool ================[X]                     ");
			Console.Write("                    |                                     |                     ");
			Console.Write("                    |  Do you want to format your drive?  |                     ");
			Console.Write("                    |                                     |                     ");
			Console.Write("                    |     [ Yes ]              [ No ]     |                     ");
			Console.Write("                    |                                     |                     ");
			Console.Write("                    =======================================                     ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.Write(" Arrows=Navigate in the dialog box  Y=Format  N/X=Do not format                ");
			#endregion
			Console.SetCursorPosition(26, 14);
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.Write("[ Yes ]");
			Console.BackgroundColor = ConsoleColor.Blue;
			Console.ForegroundColor = ConsoleColor.White;
			int yn = 0;
			ConsoleKeyInfo key;
			bool isGetting = true;
			while (isGetting)
			{
				key = Console.ReadKey();
				if (key.Key == ConsoleKey.RightArrow && yn == 0)
				{
					Console.SetCursorPosition(26, 14);
					Console.BackgroundColor = ConsoleColor.Blue;
					Console.ForegroundColor = ConsoleColor.White;
					Console.Write("[ Yes ]");
					Console.SetCursorPosition(47, 14);
					Console.BackgroundColor = ConsoleColor.White;
					Console.ForegroundColor = ConsoleColor.Black;
					Console.Write("[ No ]");
					Console.BackgroundColor = ConsoleColor.Blue;
					Console.ForegroundColor = ConsoleColor.White;
					yn = 1;
				}
				else if (key.Key == ConsoleKey.LeftArrow && yn == 1)
				{
					Console.SetCursorPosition(47, 14);
					Console.BackgroundColor = ConsoleColor.Blue;
					Console.ForegroundColor = ConsoleColor.White;
					Console.Write("[ No ]");
					Console.SetCursorPosition(26, 14);
					Console.BackgroundColor = ConsoleColor.White;
					Console.ForegroundColor = ConsoleColor.Black;
					Console.Write("[ Yes ]");
					Console.BackgroundColor = ConsoleColor.Blue;
					Console.ForegroundColor = ConsoleColor.White;
					yn = 0;
				}
				else if (key.Key == ConsoleKey.Enter) { isGetting = false; break; }
				else if (key.Key == ConsoleKey.Y) { yn = 0; isGetting = false; break; }
				else if (key.Key == ConsoleKey.N || key.Key == ConsoleKey.X) { yn = 1; isGetting = false; break; }
				else { FormAsk(); }
			}
			if (yn == 0) { Format(); }
			else if (yn == 1) { UserSetup(); }
		}
		private static void Format()
		{
			bool format = false;
		here:
			#region Format screen draw
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.Clear();
			Console.BackgroundColor = ConsoleColor.Blue;
			Console.ForegroundColor = ConsoleColor.White;
			Console.SetCursorPosition(0, 0);
			Console.Write("                                                                                ");
			Console.Write(" Seting-DOS 2022 Textmode Setup                                                 ");
			Console.Write("================================                                                ");
			Console.Write("                                                                                ");
			Console.Write("    Formatting tool will open and guide you through formatting your drive.      ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                    === Formatting Tool ===================                     ");
			Console.Write("                    |                                     |                     ");
			Console.Write("                    |  Formatting drive...                |                     ");
			Console.Write("                    |  =================================  |                     ");
			Console.Write("                    |  |                               |  |                     ");
			Console.Write("                    |  =================================  |                     ");
			Console.Write("                    =======================================                     ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.Write(" Please wait while the drive is being formatten...                             ");
			#endregion
			Console.BackgroundColor = ConsoleColor.Blue;
			Console.ForegroundColor = ConsoleColor.Yellow;
			if (!format)
			{
				VSFS.EmptyRootPartition(); Console.SetCursorPosition(24, 14); Console.Write("█████████████████");
				foreach (var disk in Sys.FileSystem.VFS.VFSManager.GetDisks())
				{
					foreach (var part in disk.Partitions)
					{
						if (part.RootPath == "0:\\")
						{
							Cosmos.HAL.Global.PIT.Wait(6000);
							disk.FormatPartition(0, "FAT32", false);
							Console.Write("████████████████");
							format = true;
							goto here;
						}
					}
				}
			}
			Console.SetCursorPosition(24, 14); Console.Write("███████████████████████████████");
			UserSetup();
		}
		private static void UserSetup()
		{
			#region User setup draw
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.Clear();
			Console.BackgroundColor = ConsoleColor.Blue;
			Console.ForegroundColor = ConsoleColor.White;
			Console.SetCursorPosition(0, 0);
			Console.Write("                                                                                ");
			Console.Write(" Seting-DOS 2022 Textmode Setup                                                 ");
			Console.Write("================================                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("    === Preferences > First account setup ===================================   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    |  You need to create an account to set up your computer.               |   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    |  Username                                                             |   ");
			Console.Write("    |  [                          ]                                         |   ");
			Console.Write("    |  Password                                                             |   ");
			Console.Write("    |  [                          ]                                         |   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    |  Account type                                                         |   ");
			Console.Write("    |  [+] Administrator                                                    |   ");
			Console.Write("    |  [!] The first user needs to be an administrator                      |   ");
			Console.Write("    |                                                           [ Create ]  |   ");
			Console.Write("    =========================================================================   ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.Write(" ENTER=Next menu                                                               ");
			#endregion
			Console.BackgroundColor = ConsoleColor.Blue;
			Console.ForegroundColor = ConsoleColor.White;
			Console.SetCursorPosition(8, 10);
			string username = Console.ReadLine();
			if (username == null || username == "") { username = "Admin"; }
			if (username.Length > 26) { Console.SetCursorPosition(8 + username.Length, 10); Console.Write("]"); }
			Console.SetCursorPosition(8, 12);
			string password = Drivers.Keyboard.KeyHandler(true, true);
			string reminder = null;
			if (password.Length > 26) { Console.SetCursorPosition(8 + password.Length, 12); Console.Write("]"); }
			if (password != null && password != "") //Create password repeat and reminder boxes
			{
				Console.SetCursorPosition(7, 13);
				Console.Write("Password repeat");
				Console.SetCursorPosition(7, 14);
				Console.Write("[                          ]");
				Console.SetCursorPosition(7, 15);
				Console.Write("Password reminder");
				Console.SetCursorPosition(7, 16);
				Console.Write("[                          ]");
			get:
				Console.SetCursorPosition(8, 14);
				string tmp = Drivers.Keyboard.KeyHandler(true);
				if (password != tmp)
				{
					Console.SetCursorPosition(0, 14);
					Console.Write("    |  [                          ]  [!] Password doesn't match!            |   ");
					goto get;
				}
				if (tmp.Length > 26) { Console.SetCursorPosition(8 + tmp.Length, 14); Console.Write("]"); }
				Console.SetCursorPosition(8, 16);
				reminder = Console.ReadLine();
				if (reminder.Length > 26) { Console.SetCursorPosition(8 + reminder.Length, 16); Console.Write("]"); }
			}
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.SetCursorPosition(18, 24);
			Console.Write("ESC=Clear");
			Console.SetCursorPosition(64, 20);
			Console.Write("[ Create ]");
			ConsoleKeyInfo key = Console.ReadKey();
			if (key.Key == ConsoleKey.Escape) { UserSetup(); }
			else { Installer(username, password, reminder); }
		}
		private static void Installer(string username, string password, string reminder)
		{
			#region Install screen draw
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.Clear();
			Console.BackgroundColor = ConsoleColor.Blue;
			Console.ForegroundColor = ConsoleColor.White;
			Console.SetCursorPosition(0, 0);
			Console.Write("                                                                                ");
			Console.Write(" Seting-DOS 2022 Textmode Setup                                                 ");
			Console.Write("================================                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("    === System Configurator > Installer =====================================   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    |   Please wait while your modifications are applied...                 |   ");
			Console.Write("    |   [ ] Creating folders...                                             |   ");
			Console.Write("    |   [ ] Copying system files and apps...                                |   ");
			Console.Write("    |   [ ] Creating and writing files...                                   |   ");
			Console.Write("    |   [ ] Restarting...                                                   |   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    |   The installer is writing to the local disk. While the kernel        |   ");
			Console.Write("    |   itself isn't stored on the disk, your account and most of the       |   ");
			Console.Write("    |   files will be stored on it.                                         |   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    |  ===================================================================  |   ");
			Console.Write("    |  |                                                                 |  |   ");
			Console.Write("    |  ===================================================================  |   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    =========================================================================   ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.Write(" System version: OWO UA WIP                                                    ");
			#endregion
			//Remove _-s and spaces from username for folder name
			string userFolder = username.Replace(" ", "-").Replace("_", "-");
			Console.SetCursorPosition(8, 18);
			Console.BackgroundColor = ConsoleColor.Blue;
			Console.ForegroundColor = ConsoleColor.Yellow;
            #region Creating folders
            VSFS.act_dir = "/0/";
			VSFS.cur_dir = "0:\\"; Console.Write("██"); Cosmos.HAL.Global.PIT.Wait(10);
			VSFS.MakeDir("SDOS", true); Console.Write("██"); Cosmos.HAL.Global.PIT.Wait(10);
			VSFS.MakeDir("Users", true); Console.Write("██"); Cosmos.HAL.Global.PIT.Wait(10);
			VSFS.MakeDir("Applications", true); Console.Write("██"); Cosmos.HAL.Global.PIT.Wait(10);
			VSFS.act_dir = "/0/SDOS/";
			VSFS.cur_dir = "0:\\SDOS\\"; Console.Write("██"); Cosmos.HAL.Global.PIT.Wait(10);
			VSFS.MakeDir("Preferences", true); Console.Write("██"); Cosmos.HAL.Global.PIT.Wait(10);
			VSFS.MakeDir("ProgramData", true); Console.Write("██"); Cosmos.HAL.Global.PIT.Wait(10);
			VSFS.MakeDir("System", true); Console.Write("██"); Cosmos.HAL.Global.PIT.Wait(10);
			VSFS.MakeDir("etc", true); Console.Write("██"); Cosmos.HAL.Global.PIT.Wait(10);
			VSFS.act_dir = "/0/Users/";
			VSFS.cur_dir = "0:\\Users\\"; Console.Write("██"); Cosmos.HAL.Global.PIT.Wait(10);
			VSFS.MakeDir(userFolder, true); Console.Write("██"); Cosmos.HAL.Global.PIT.Wait(10);
			VSFS.act_dir = "/0/Users/" + userFolder + "/";
			VSFS.cur_dir = "0:\\Users\\" + userFolder + "\\"; Console.Write("██"); Cosmos.HAL.Global.PIT.Wait(10);
			VSFS.MakeDir("Documents", true); Console.Write("██"); Cosmos.HAL.Global.PIT.Wait(10);
			VSFS.MakeDir("Music", true); Console.Write("██"); Cosmos.HAL.Global.PIT.Wait(10);
			VSFS.MakeDir("AppData", true); Console.Write("██"); Cosmos.HAL.Global.PIT.Wait(10);
			#endregion
			int x = Console.GetCursorPosition().Left;
			Console.SetCursorPosition(9, 8);
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("+");
			Console.SetCursorPosition(x, 18);
			Console.BackgroundColor = ConsoleColor.Blue;
			Console.ForegroundColor = ConsoleColor.Yellow;
			#region Copying system files and apps
			//Copying files: DotNetParser apps and sound files
			VSFS.CopyAllResourceStreams();
			Console.Write("███████"); Cosmos.HAL.Global.PIT.Wait(10);
			#endregion
			x = Console.GetCursorPosition().Left;
			Console.SetCursorPosition(9, 9);
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("+");
			Console.SetCursorPosition(x, 18);
			Console.BackgroundColor = ConsoleColor.Blue;
			Console.ForegroundColor = ConsoleColor.Yellow;
            #region Creating and writing files
            StreamWriter cn = new StreamWriter(@"0:\SDOS\preferences\compName.pref"); Console.Write("██");
			cn.Write("localhost");
			cn.Close(); Cosmos.HAL.Global.PIT.Wait(10); Console.Write("██");
			StreamWriter mute = new StreamWriter(@"0:\SDOS\system\mute.dat"); Console.Write("██");
			mute.Write("0");
			mute.Close(); Cosmos.HAL.Global.PIT.Wait(10); Console.Write("██");
			StreamWriter vb = new StreamWriter(@"0:\SDOS\preferences\verboseBoot.pref"); Console.Write("██");
			vb.Write("1");
			vb.Close(); Cosmos.HAL.Global.PIT.Wait(10); Console.Write("██");
            StreamWriter pmft = new StreamWriter(@"0:\SDOS\preferences\pmft.pref");
            pmft.Write("progress");
            pmft.Close(); Cosmos.HAL.Global.PIT.Wait(10);
            Console.Write("██");
			StreamWriter thm = new StreamWriter(@"0:\SDOS\preferences\theme.dat"); Console.Write("██");
			thm.Write("classic");
			thm.Close(); Cosmos.HAL.Global.PIT.Wait(10); Console.Write("██");
			StreamWriter fn = new StreamWriter(@"0:\users\" + userFolder + @"\fullName.dat"); Console.Write("██");
			fn.Write(username);
			fn.Close(); Cosmos.HAL.Global.PIT.Wait(10); Console.Write("██");
			if (password != "" && password != null)
			{
				StreamWriter pw = new StreamWriter(@"0:\users\" + userFolder + @"\password.pwd");
				pw.Write(password);
				pw.Close(); Cosmos.HAL.Global.PIT.Wait(10);
				StreamWriter pr = new StreamWriter(@"0:\users\" + userFolder + @"\passRem.dat");
				pr.Write(reminder);
				pr.Close(); Cosmos.HAL.Global.PIT.Wait(10);
			}
			Console.Write("██████");
			#endregion
			Console.SetCursorPosition(9, 10);
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("+");
			Console.SetCursorPosition(8, 16); Console.ForegroundColor = ConsoleColor.Green;
			File.Create(@"0:\SDOS\system\installed.idp");
			Console.Write("Installation was succesful. PRESS ANY KEY TO RESTART YOUR COMPUTER!"); Console.ForegroundColor = ConsoleColor.White;
			Sys.PCSpeaker.Beep(800, 1);
			Console.ReadKey();
			Sys.Power.Reboot();
		}
    }
}