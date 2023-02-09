/// 
/// Post-install script, Last modified: 2022. 09. 24.
/// Made for Seting-DOS, feel free to use any code from this
/// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Sys = Cosmos;

namespace Seting_DOS.TextUI
{
    public static class PostInstall
    {
        public static void Start()
        {
			Console.BackgroundColor = ConsoleColor.Blue;
			Console.ForegroundColor = ConsoleColor.White;
			Console.Clear();
			Console.SetCursorPosition(0, 0);
			Console.Write("                                                                                ");
			Console.Write(" Seting-DOS 2022 Post-Install                                                   ");
			Console.Write("==============================                                                  ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("    === System Configurator > Post-Installer ================================   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    | Welcome!                                                              |   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    | The operating system was installed successfully.                      |   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    | Your user account was made, but there are still settings that need to |   ");
			Console.Write("    | be set.                                                               |   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    | This post-install utility will guide you throught the process of      |   ");
			Console.Write("    | finishing the setup.                                                  |   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    | Thanks for choosing Seting-DOS!                                       |   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    |                                                            [ Start ]  |   ");
			Console.Write("    =========================================================================   ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write(Services.EnvVars.versionstring);
			Console.SetCursorPosition(65, 20);
			Console.BackgroundColor = ConsoleColor.White;
			Console.Write("[ Start ]");
			if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
				TextUIColor();
            }
			else
            {
				Start();
            }
		}
		public static void TextUIColor(int sel = 0)
		{
			if (sel == 0) { Console.BackgroundColor = ConsoleColor.Blue; }
			else { Console.BackgroundColor = ConsoleColor.Black; }
			Console.ForegroundColor = ConsoleColor.White;
			Console.Clear();
			Console.SetCursorPosition(0, 0);
			Console.Write("                                                                                ");
			Console.Write(" Seting-DOS 2022 Post-Install                                                   ");
			Console.Write("==============================                                                  ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("    === System Configurator > Post-Installer ================================   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    | The system is a shell operating system that's always black, but there |   ");
			Console.Write("    | is an application interface called TextUI. The default color for that |   ");
			Console.Write("    | is blue (also called Classic Blue), but you can set that to dark. For |   ");
			Console.Write("    | example, the post-installer is also running in TextUI, and it's blue. |   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    | Select the theme you want to use:                                     |   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    | [ Classic Blue ]                                                      |   ");
			Console.Write("    | [ Black ]                                                             |   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    =========================================================================   ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write(Services.EnvVars.versionstring);
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			if (sel == 0)
			{
				Console.SetCursorPosition(6, 14);
				Console.Write("[ Classic Blue ]");
			}
			else
            {
				Console.SetCursorPosition(6, 15);
				Console.Write("[ Black ]");
			}
			ConsoleKeyInfo key = Console.ReadKey();
			if (key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.DownArrow)
            {
				if (sel == 0) { TextUIColor(1); }
				else { TextUIColor(0); }
            }
			else if (key.Key == ConsoleKey.Enter)
            {
				if (sel == 0)
                {
					Services.EnvVars.theme = "classic";
                }
				else
                {
					Services.EnvVars.theme = "black";
				}
				CompNameSet();
            }
			else { TextUIColor(sel); }
		}
		public static void CompNameSet()
		{
			Services.TUIBGCol.Set();
			Console.ForegroundColor = ConsoleColor.White;
			Console.Clear();
			Console.SetCursorPosition(0, 0);
			Console.Write("                                                                                ");
			Console.Write(" Seting-DOS 2022 Post-Install                                                   ");
			Console.Write("==============================                                                  ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("    === System Configurator > Post-Installer ================================   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    | You can also set a name for your computer. For example it shows in    |   ");
			Console.Write("    | the Terminal as username@computerName. Default name is localhost.     |   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    | Set the name by pressing Enter.                                       |   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    | Name: [                                                               |   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    =========================================================================   ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write(Services.EnvVars.versionstring);
			Console.SetCursorPosition(14, 12);
			string name = Drivers.Keyboard.KeyHandler(true, false);
			if (name.Replace(" ", "") != "") { Services.EnvVars.hostname = name.Replace(" ", "-"); }
			Write();
		}

		public static void Write(int sel = 0)
		{
			Services.TUIBGCol.Set();
			Console.ForegroundColor = ConsoleColor.White;
			Console.Clear();
			Console.SetCursorPosition(0, 0);
			Console.Write("                                                                                ");
			Console.Write(" Seting-DOS 2022 Post-Install                                                   ");
			Console.Write("==============================                                                  ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("    === System Configurator > Post-Installer ================================   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    | The post-installer is almost finished. You set everything, it just    |   ");
			Console.Write("    | needs to be written on the disk.                                      |   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    | You selected:                                                         |   ");
			Console.Write("    | Theme:                                                                |   ");
			Console.Write("    | Computer name:                                                        |   ");
			Console.Write("    | Is this right?                                                        |   ");
			Console.Write("    | [ Yes ]                                                               |   ");
			Console.Write("    | [ No ]                                                                |   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    | If you select 'Yes', the data will be written to the disk. If you     |   ");
			Console.Write("    | select 'No', you will be taken back to the start screen.              |   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    |                                                                       |   ");
			Console.Write("    =========================================================================   ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write(Services.EnvVars.versionstring);
			Console.SetCursorPosition(13, 11);
			if (Services.EnvVars.theme == "classic") { Console.Write("Classic Blue"); }
			else { Console.Write("Black"); }
			Console.SetCursorPosition(21, 12);
			Console.Write(Services.EnvVars.hostname);
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			if (sel == 0)
			{
				Console.SetCursorPosition(6, 14);
				Console.Write("[ Yes ]");
			}
			else
			{
				Console.SetCursorPosition(6, 15);
				Console.Write("[ No ]");
			}
			ConsoleKeyInfo key = Console.ReadKey();
			if (key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.DownArrow)
			{
				if (sel == 0) { Write(1); }
				else { Write(0); }
			}
			else if (key.Key == ConsoleKey.Enter)
			{
				if (sel == 0)
				{
					//Write data to disk
					Console.SetCursorPosition(6, 19);
					Services.TUIBGCol.Set(); Console.ForegroundColor = ConsoleColor.Red;
					Console.Write("Writing to disk... ");
					Sys.System.PCSpeaker.Beep();
					StreamWriter theme = new StreamWriter(@"0:\SDOS\preferences\theme.dat");
					theme.Write(Services.EnvVars.theme);
					theme.Close();
					StreamWriter cn = new StreamWriter(@"0:\SDOS\preferences\compName.pref");
					cn.Write(Services.EnvVars.hostname);
					cn.Close();
					File.Create(@"0:\SDOS\preferences\postins.idp");
					Console.Write("OK. ");
					Sys.System.PCSpeaker.Beep();
					Services.TUIBGCol.Set(); Console.ForegroundColor = ConsoleColor.White;
					Console.Write("Press any key to restart your computer.");
					Console.ReadKey(); Sys.System.Power.Reboot();
				}
				else
				{
					Start();
				}
			}
			else { Write(sel); }
		}
	}
}