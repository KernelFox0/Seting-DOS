/// 
/// Seting-DOS Neofetch program, Last modified: 2022. 09. 24.
/// Made for Seting-DOS, feel free to use any code from this
/// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seting_DOS.Apps
{
	public static class Neofetch
	{
		public static void Open() //App open command
		{
			WriteBase();
		}
		private static void WriteBase() //Draw the base neofetch UI on the screen
		{
			#region Draw logo base and default text
			Console.Write("=====================================    ?@?                                    ");
			Console.Write("| X                                 |    ---------------                        "); //"); Console.ForegroundColor = ConsoleColor.White; Console.Write("
			Console.Write("|  X           XXXXXXX              |    "); Console.ForegroundColor = ConsoleColor.Green; Console.Write("OS:"); Console.ForegroundColor = ConsoleColor.White; Console.Write("                                    ");
			Console.Write("|   X        XX   X   XX            |    "); Console.ForegroundColor = ConsoleColor.Green; Console.Write("Host:"); Console.ForegroundColor = ConsoleColor.White; Console.Write("                                  ");
			Console.Write("|  X        X    XXX    X           |    "); Console.ForegroundColor = ConsoleColor.Green; Console.Write("Kernel: "); Console.ForegroundColor = ConsoleColor.White; Console.Write("ß¤×                            ");
			Console.Write("| X   XXXX  X   X X X   X           |    "); Console.ForegroundColor = ConsoleColor.Green; Console.Write("Uptime: "); Console.ForegroundColor = ConsoleColor.White; Console.Write("                               ");
			Console.Write("|           X     X     X           |    "); Console.ForegroundColor = ConsoleColor.Green; Console.Write("Shell: "); Console.ForegroundColor = ConsoleColor.White; Console.Write("Seting-Shell 0.1                ");
			Console.Write("|            XX   X   XX            |    "); Console.ForegroundColor = ConsoleColor.Green; Console.Write("Resolution: "); Console.ForegroundColor = ConsoleColor.White; Console.Write("                           ");
			Console.Write("|              XXXXXXX              |    "); Console.ForegroundColor = ConsoleColor.Green; Console.Write("Color support: "); Console.ForegroundColor = ConsoleColor.White; Console.Write("16 Colors               ");
			Console.Write("|                                   |    "); Console.ForegroundColor = ConsoleColor.Green; Console.Write("TextUI Theme: "); Console.ForegroundColor = ConsoleColor.White; Console.Write("                         ");
			Console.Write("=====================================    "); Console.ForegroundColor = ConsoleColor.Green; Console.Write("Terminal: "); Console.ForegroundColor = ConsoleColor.White; Console.Write("Seting-DOS-terminal          ");
			Console.Write("                                         "); Console.ForegroundColor = ConsoleColor.Green; Console.Write("CPU: "); Console.ForegroundColor = ConsoleColor.White; Console.Write("                                  ");
			Console.Write("                                         "); Console.ForegroundColor = ConsoleColor.Green; Console.Write("Memory: "); Console.ForegroundColor = ConsoleColor.White; Console.Write("MB                             ");
			Console.Write("                                         ==================                     ");
			Console.Write("                                         |                |                     ");
			Console.Write("                                         |                |                     ");
			Console.Write("                                         ==================                     ");
			int endY = Console.GetCursorPosition().Top - 2;
			#endregion
			#region Draw Seting-DOS logo's outline
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.SetCursorPosition(12, endY - 13);
			Console.Write("   XXXXXXX   ");
			Console.SetCursorPosition(12, endY - 12);
			Console.Write(" XX   X   XX ");
			Console.SetCursorPosition(12, endY - 11);
			Console.Write("X    XXX    X");
			Console.SetCursorPosition(12, endY - 10);
			Console.Write("X   X X X   X");
			Console.SetCursorPosition(12, endY - 9);
			Console.Write("X     X     X");
			Console.SetCursorPosition(12, endY - 8);
			Console.Write(" XX   X   XX ");
			Console.SetCursorPosition(12, endY - 7);
			Console.Write("   XXXXXXX   ");
            #endregion
            #region Draw Seting-DOS logo's arrow
            Console.ForegroundColor = ConsoleColor.Green;
			Console.SetCursorPosition(16, endY - 12);
			Console.Write("  X  ");
			Console.SetCursorPosition(16, endY - 11);
			Console.Write(" XXX ");
			Console.SetCursorPosition(16, endY - 10);
			Console.Write("X X X");
			Console.SetCursorPosition(16, endY - 9);
			Console.Write("  X  ");
			Console.SetCursorPosition(16, endY - 8);
			Console.Write("  X  ");
			Console.SetCursorPosition(42, endY - 1);
            #endregion
            #region Write color blocks on the screen
            Console.ForegroundColor = ConsoleColor.Black;
			Console.Write("██");
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.Write("██");
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("██");
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.Write("██");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write("██");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("██");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("██");
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write("██");
			Console.SetCursorPosition(42, endY);
			Console.BackgroundColor = ConsoleColor.Black;
			Console.Write("  ");
			Console.BackgroundColor = ConsoleColor.Gray;
			Console.Write("  ");
			Console.BackgroundColor = ConsoleColor.Red;
			Console.Write("  ");
			Console.BackgroundColor = ConsoleColor.Magenta;
			Console.Write("  ");
			Console.BackgroundColor = ConsoleColor.Blue;
			Console.Write("  ");
			Console.BackgroundColor = ConsoleColor.Yellow;
			Console.Write("  ");
			Console.BackgroundColor = ConsoleColor.Green;
			Console.Write("  ");
			Console.BackgroundColor = ConsoleColor.White;
			Console.Write("  ");
			Console.BackgroundColor = ConsoleColor.Black;
			Console.SetCursorPosition(0, endY + 2);
			#endregion
			WriteData(endY); //Call data write script
		}
		private static void WriteData(int endY) //Write data on screen
		{
            #region Getting data be displayed
            string OSVer = Services.EnvVars.shortversion; //Set OSVer to the short version string
			int width = 80; //Screen width in characters
			int height = 25; //Screen height in characters
			string cpu = Cosmos.Core.CPU.GetCPUBrandString().Replace("(TM)", "").Replace("(R)", "").Replace(" CPU", ""); //Get CPU name and remove some parts to shorten it.
			uint maxmem = Cosmos.Core.CPU.GetAmountOfRAM(); //Get installed memory
			ulong availmem = Cosmos.Core.GCImplementation.GetAvailableRAM(); //Get available (free) memory
			ulong usedmem = maxmem - availmem; //Calculate used memory
			int uptime = Convert.ToInt32(Cosmos.Core.CPU.GetCPUUptime() / 1000 / 1000 / 1000 / 5); //Get system uptime and convert it to seconds
			#region Convert uptime from seconds to M:SS format
			int sec = uptime % 60;
			int mins = (uptime - sec) / 60;
			string secs = sec.ToString();
			if (secs.Length != 2) //If seconds is not double digit, add a 0 to front to keep M:SS format.
			{
				secs = "0" + secs;
			}
            #endregion
            string theme = "Classic Blue"; //Default theme is Classic Blue
			if (Services.EnvVars.theme != "classic") { theme = "Black"; } //But if not, set it to black
            #endregion
            #region Write data to screen
            Console.SetCursorPosition(45, endY - 13);
			Console.Write(OSVer); //Write OS Version
            #region Write host type based on the CPU
            Console.SetCursorPosition(47, endY - 12);
			if (cpu.ToLower().Contains("i3")) { Console.Write("Intel Core i3 Machine"); }
			else if (cpu.ToLower().Contains("i5")) { Console.Write("Intel Core i5 Machine"); }
			else if (cpu.ToLower().Contains("i7")) { Console.Write("Intel Core i7 Machine"); }
			else if (cpu.ToLower().Contains("i9")) { Console.Write("Intel Core i9 Machine"); }
			else if (cpu.ToLower().Contains("pentium")) { Console.Write("Intel Pentium Machine"); }
			else if (cpu.ToLower().Contains("celeron")) { Console.Write("Intel Celeron Machine"); }
			else if (cpu.ToLower().Contains("xeon")) { Console.Write("Intel Xeon Machine"); }
			else if (cpu.ToLower().Contains("ryzen 3")) { Console.Write("AMD Ryzen 3 Machine"); }
			else if (cpu.ToLower().Contains("ryzen 5")) { Console.Write("AMD Ryzen 5 Machine"); }
			else if (cpu.ToLower().Contains("ryzen 7")) { Console.Write("AMD Ryzen 7 Machine"); }
			else if (cpu.ToLower().Contains("ryzen 9")) { Console.Write("AMD Ryzen 9 Machine"); }
			else if (cpu.ToLower().Contains("epyc")) { Console.Write("AMD EPYC Machine"); }
			else if (cpu.ToLower().Contains("threadripper")) { Console.Write("AMD Threadripper Machine"); }
			else { Console.Write("Other/Unknown Machine"); }
            #endregion
            Console.SetCursorPosition(49, endY - 10);
			Console.Write("{0}:{1} minutes", mins, secs); //Write uptime
			Console.SetCursorPosition(49, endY - 11);
			Console.Write(Services.EnvVars.kernelVer); //Write COSMOS kernel version
			Console.SetCursorPosition(53, endY - 8);
			Console.Write("{0}x{1}", width, height); //Write screen resolution in characters
			Console.SetCursorPosition(55, endY - 6);
			Console.Write(theme); //Write theme
			Console.SetCursorPosition(46, endY - 4);
			Console.Write(cpu); //Write CPU name
			Console.SetCursorPosition(49, endY - 3);
			Console.Write("{0}MB/{1}MB", usedmem, maxmem); //Write used and maximum memory
            #region Write username and hostname in user@host format
            Console.SetCursorPosition(41, endY - 15);
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write(Services.EnvVars.username);
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write("@");
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write(Services.EnvVars.hostname);
			Console.ForegroundColor = ConsoleColor.White;
            #endregion
            #endregion
            Console.SetCursorPosition(0, endY + 2); //Place the cursor to the end
		}
	}
}