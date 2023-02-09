﻿/// 
/// Power manager driver, Last modified: 2022. 10. 19.
/// Made for Seting-DOS, feel free to use any code from this
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
				string[] kern = { "done", "Seting-DOS base kernel loaded" };
				BootMSG.Write(kern);
				BootMSG.Write(DisplayDriver.Load());
				BootMSG.Write(VSFS.Load());
				if (!File.Exists(@"0:\SDOS\System\installed.idp"))
				{
					string[] waitMSG = { "warning", "System isn't set up!" };
					BootMSG.Write(waitMSG);
					Global.PIT.Wait(5000);
					TextUI.Setup.Call();
				}
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
                    Console.Write(" Seting-DOS(tm) Color Operating System                                         "); Global.PIT.Wait(10);
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
				Console.ForegroundColor = ConsoleColor.Blue;
				BootMSG.Write(Keyboard.Load()); Console.ForegroundColor = ConsoleColor.Blue;
                if (animation) { Console.Write("██"); }
				BootMSG.Write(RTC.Check()); Console.ForegroundColor = ConsoleColor.Blue;
                if (animation) { Console.Write("██"); }
				BootMSG.Write(Beep.Load()); Console.ForegroundColor = ConsoleColor.Blue;
                if (animation) { Console.Write("██"); }
				BootMSG.Write(SystemData.Load()); Console.ForegroundColor = ConsoleColor.Blue;
                if (animation) { Console.Write("██"); }
				//BootMSG.Write(DotNetParser.Load());
				if (animation) { Console.Write("████"); Global.PIT.Wait(500); }
				if (animation) { Console.Write("██"); Global.PIT.Wait(500); }
                if (animation) { Console.Write("██"); Global.PIT.Wait(500); }
                if (animation) { Console.Write("██"); Global.PIT.Wait(500); }
				if (animation) { Console.Write("██"); Global.PIT.Wait(500); }
				if (animation) { Console.Write("██"); Global.PIT.Wait(500); }
				if (animation) { Console.Write("██"); Global.PIT.Wait(500); }
				if (animation) { Console.Write("██"); Global.PIT.Wait(500); }
				if (animation) { Console.Write("██"); Global.PIT.Wait(500); }
				if (animation) { Console.Write("███"); Global.PIT.Wait(200); }
				TextUI.LogonUI.LockScreen();
				TextUI.Terminal.Init();
			}
		}
	}
}