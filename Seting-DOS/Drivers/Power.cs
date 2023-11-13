/// 
/// Power manager & boot driver, Last modified: 2023. 11. 13.
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
using Sys = Cosmos.System;
using Dev = Cosmos.Core;
using System.Collections.Generic;
using System.Text;
using Cosmos.System.ExtendedASCII;
using System.IO;
using Cosmos.System.FileSystem;
using Cosmos.HAL;
using Seting_DOS;
using Seting_DOS.Drivers;
using Seting_DOS.Services;
using Seting_DOS.Apps;

namespace Seting_DOS.Drivers
{
	public static class Power
	{
		public static class Shutdown
		{
			public static void System()
			{
				Console.WriteLine("Bye! Shutting down...");
				Beep.Sound.Shutdown();
				Sys.Power.Shutdown();
			}
			public static void ACPI()
			{
				Dev.ACPI.Shutdown();
			}
			public static void CPU()
			{
				Dev.CPU.Halt();
			}
		}
		public static class Reboot
		{
			public static void System()
			{
				Console.WriteLine("Restarting...");
				Beep.Sound.Shutdown();
				Sys.Power.Reboot();
			}
			public static void ACPI()
			{
				Dev.ACPI.Reboot();
			}
			public static void CPU()
			{
				Dev.CPU.Reboot();
			}
		}
		public static class Booter
		{
			public static void Preboot()
			{
				// This code will load the base display and FS driver, check for installed OS and load pre-boot system variables to memory
				string[] kernelMSG = { "done", "COSMOS kernel loaded and initialized" };
				BootMSG.Write(kernelMSG, true);
				kernelMSG[0] = "info"; kernelMSG[1] = "COSMOS kernel boot done, SetingBoot will handle the rest.";
				BootMSG.Write(kernelMSG, true);
				BootMSG.Write(DisplayDriver.Load(), true);
				BootMSG.Write(VSFS.Load(), true);
				if (!File.Exists(@"0:\SDOS\System\installed.idp"))
				{
					string[] waitMSG = { "warning", "System isn't set up!" };
					BootMSG.Write(waitMSG, true);
					Global.PIT.Wait(5000);
					Setup.Call();
				}
				string[] msg = { "info", "Calling post-update utility" };
				BootMSG.Write(msg);
				BootMSG.Write(PostUpdateUtil.CheckVersionDifference(), true);
				BootMSG.Write(SystemData.Load(), true);
				Boot();
			}
			public static void Boot()
			{
				bool animation = !EnvVars.verboseMode;
				// This code will load all the drivers and system variables to memory and either print verbose boot or play the boot animation
				if (animation)
				{
					Console.Clear();
					Console.SetCursorPosition(0, 0);
					Console.ForegroundColor = ConsoleColor.White; Console.BackgroundColor = ConsoleColor.Black;
					Console.Write("                                                                                "); Global.PIT.Wait(10);
					Console.Write("                                                                                "); Global.PIT.Wait(10);
					Console.Write("                                                                                "); Global.PIT.Wait(10);
					Console.Write("                                                                                "); Global.PIT.Wait(10);
					Console.Write("                                     XXXXXXX                                    "); Global.PIT.Wait(10);
					Console.Write("                                   XX   X   XX                                  "); Global.PIT.Wait(10);
					Console.Write("                                  X    XXX    X                                 "); Global.PIT.Wait(10);
					Console.Write("                                  X   X X X   X                                 "); Global.PIT.Wait(10);
					Console.Write("                                  X     X     X                                 "); Global.PIT.Wait(10);
					Console.Write("                                   XX   X   XX                                  "); Global.PIT.Wait(10);
					Console.Write("                                     XXXXXXX                                    "); Global.PIT.Wait(10);
					Console.Write("                 .-.       .                     .--.  .--.  .-.                "); Global.PIT.Wait(10);
					Console.Write("                (   )     _|_  o                 |   ::    :(   )               "); Global.PIT.Wait(10);
					Console.Write("                 `-.  .-.  |   .  .--. .-.. ____ |   ||    | `-.                "); Global.PIT.Wait(10);
					Console.Write("                (   )(.-'  |   |  |  |(   |      |   ;:    ;(   )               "); Global.PIT.Wait(10);
					Console.Write("                 `-'  `--' `-' '  '  ` `-`|      '--'  `--'  `-'                "); Global.PIT.Wait(10);
					Console.Write("                                       ._.'                                     "); Global.PIT.Wait(10);
					Console.Write("                                                                                "); Global.PIT.Wait(10);
					Console.Write("                       ===================================                      "); Global.PIT.Wait(10);
					Console.Write("                       |                                 |                      "); Global.PIT.Wait(10);
					Console.Write("                       ===================================                      "); Global.PIT.Wait(10);
					Console.Write("                                                                                "); Global.PIT.Wait(10);
					Console.Write("                                                                                "); Global.PIT.Wait(10);
					Console.Write("                                                                                "); Global.PIT.Wait(10);
					Console.Write(" Seting-DOS Color Operating System                                             "); Global.PIT.Wait(10);
					Console.ForegroundColor = ConsoleColor.Blue;
					Console.SetCursorPosition(34, 4);
					Console.Write("   XXXXXXX   ");
					Console.SetCursorPosition(34, 5);
					Console.Write(" XX   X   XX ");
					Console.SetCursorPosition(34, 6);
					Console.Write("X    XXX    X");
					Console.SetCursorPosition(34, 7);
					Console.Write("X   X X X   X");
					Console.SetCursorPosition(34, 8);
					Console.Write("X     X     X");
					Console.SetCursorPosition(34, 9);
					Console.Write(" XX   X   XX ");
					Console.SetCursorPosition(34, 10);
					Console.Write("   XXXXXXX   ");
					Console.ForegroundColor = ConsoleColor.Green;
					Console.SetCursorPosition(38, 5);
					Console.Write("  X  ");
					Console.SetCursorPosition(38, 6);
					Console.Write(" XXX ");
					Console.SetCursorPosition(38, 7);
					Console.Write("X X X");
					Console.SetCursorPosition(38, 8);
					Console.Write("  X  ");
					Console.SetCursorPosition(38, 9);
					Console.Write("  X  ");
					Console.SetCursorPosition(25, 19);
				}
				BootMSG.Write(Keyboard.Load());
				if (animation) { Console.ForegroundColor = ConsoleColor.Blue; Console.Write("██"); }
				BootMSG.Write(RTC.Check());
				if (animation) { Console.ForegroundColor = ConsoleColor.Blue; Console.Write("██"); }
				BootMSG.Write(Beep.Load());
				if (animation) { Console.ForegroundColor = ConsoleColor.Blue; Console.Write("██"); }
                BootMSG.Write(AliasManager.Load(true));
                if (animation) { Console.ForegroundColor = ConsoleColor.Blue; Console.Write("██"); }
				//This part is for future boot operations and for filling the boot status bar
				if (animation) { Console.ForegroundColor = ConsoleColor.Blue; Console.Write("████"); Global.PIT.Wait(500); }
				if (animation) { Console.ForegroundColor = ConsoleColor.Blue; Console.Write("██"); Global.PIT.Wait(500); }
				if (animation) { Console.ForegroundColor = ConsoleColor.Blue; Console.Write("██"); Global.PIT.Wait(500); }
				if (animation) { Console.ForegroundColor = ConsoleColor.Blue; Console.Write("██"); Global.PIT.Wait(500); }
				if (animation) { Console.ForegroundColor = ConsoleColor.Blue; Console.Write("██"); Global.PIT.Wait(500); }
				if (animation) { Console.ForegroundColor = ConsoleColor.Blue; Console.Write("██"); Global.PIT.Wait(500); }
				if (animation) { Console.ForegroundColor = ConsoleColor.Blue; Console.Write("██"); Global.PIT.Wait(500); }
				if (animation) { Console.ForegroundColor = ConsoleColor.Blue; Console.Write("██"); Global.PIT.Wait(500); }
				if (animation) { Console.ForegroundColor = ConsoleColor.Blue; Console.Write("██"); Global.PIT.Wait(500); }
				if (animation) { Console.ForegroundColor = ConsoleColor.Blue; Console.Write("███"); Global.PIT.Wait(200); }
				if (EnvVars.verboseMode && EnvVars.debugBoot)
				{
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write("Booting has finished. Press any key to continue... "); Console.ReadKey();
				}
				LogonUI.LockScreen();
				Terminal.Init();
			}
		}
	}
}