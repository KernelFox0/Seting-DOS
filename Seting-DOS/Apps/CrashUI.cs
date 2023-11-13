/// 
/// System crash UI, Last modified: 2023. 07. 30.
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
using Seting_DOS.Services;

namespace Seting_DOS.Apps
{
    public static class CrashUI
    {
        static Random rnd = new Random();
        public static void ApplicationCrash(Exception crash)
        {
            #region UI
            int screen = rnd.Next(1, 4);
            int y = 11;
            int y2 = 15;
            if (screen == 1)
            {
                y = 11;
                y2 = 15;
            }
            else if (screen == 2)
            {
                y = 14;
                y2 = 18;
            }
            else
            {
                y = 13;
                y2 = 17;
            }
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            if (screen == 1)
            {
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write("                                __            __                                ");
                Console.Write("                                \\_\\__      __/_/                                ");
                Console.Write("                               / _ \\ \\ /\\ / / _ \\                               ");
                Console.Write("                              | |_| \\ V  V / |_| |                              ");
                Console.Write("                               \\___/ \\_/\\_/ \\___/                               ");
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write(" Your system crashed. That's not good! Crash type: Application crash            ");
                Console.Write(" Reason:                                                                        ");
                Console.Write("                                                                                ");
                Console.Write(" You can either return to the system or restart the kernel.                     ");
                Console.Write("                                                                                ");
                Console.Write(" KERNEL VERSION: h                                                              ");
                Console.Write(" SYSTEM VERSION: h                                                              ");
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write(" R = Return to system     Any other = Restart kernel                            ");
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write("                                                                              ");
            }
            else if (screen == 2)
            {
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write("        _                                                                       ");
                Console.Write("       / )                                                                      ");
                Console.Write("   _  / /                                                                       ");
                Console.Write("  (_)( (                                                                        ");
                Console.Write("     | |                                                                        ");
                Console.Write("   _ ( (                                                                        ");
                Console.Write("  (_) \\ \\                                                                       ");
                Console.Write("       \\_)                                                                      ");
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write(" Your system crashed. That's not good! Crash type: Application crash            ");
                Console.Write(" Reason:                                                                        ");
                Console.Write("                                                                                ");
                Console.Write(" You can either return to the system or restart the kernel.                     ");
                Console.Write("                                                                                ");
                Console.Write(" KERNEL VERSION: h                                                              ");
                Console.Write(" SYSTEM VERSION: h                                                              ");
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write(" R = Return to system     Any other = Restart kernel                            ");
                Console.Write("                                                                              ");
            }
            else if (screen == 3)
            {
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write("    ___  ___  ___  ___          ________  ___  ___                              ");
                Console.Write("   |\\  \\|\\  \\|\\  \\|\\  \\        |\\   __  \\|\\  \\|\\  \\                             ");
                Console.Write("   \\ \\  \\\\\\  \\ \\  \\\\\\  \\       \\ \\  \\|\\  \\ \\  \\\\\\  \\                            ");
                Console.Write("    \\ \\  \\\\\\  \\ \\   __  \\       \\ \\  \\\\\\  \\ \\   __  \\                           ");
                Console.Write("     \\ \\  \\\\\\  \\ \\  \\ \\  \\       \\ \\  \\\\\\  \\ \\  \\ \\  \\ ___ ___ ___              ");
                Console.Write("      \\ \\_______\\ \\__\\ \\__\\       \\ \\_______\\ \\__\\ \\__\\\\__\\\\__\\\\__\\             ");
                Console.Write("       \\|_______|\\|__|\\|__|        \\|_______|\\|__|\\|__\\|__\\|__\\|__|             ");
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write(" Your system crashed. That's not good! Crash type: Application crash            ");
                Console.Write(" Reason:                                                                        ");
                Console.Write("                                                                                ");
                Console.Write(" You can either return to the system or restart the kernel.                     ");
                Console.Write("                                                                                ");
                Console.Write(" KERNEL VERSION: h                                                              ");
                Console.Write(" SYSTEM VERSION: h                                                              ");
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write(" R = Return to system     Any other = Restart kernel                            ");
                Console.Write("                                                                                ");
                Console.Write("                                                                              ");
            }
            #endregion
            #region Write reason
            Cosmos.System.PCSpeaker.Beep();
            Console.SetCursorPosition(17, y2);
            Console.Write(EnvVars.kernelVer);
            Console.SetCursorPosition(17, y2 + 1);
            Console.Write(EnvVars.shortversion);
            Console.SetCursorPosition(9, y);
            Console.Write(crash.Message);
            #endregion
            #region Reboot
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.R) { }
            else { Cosmos.Core.CPU.Reboot(); }
            #endregion
        }
        public static void KernelCrash(Exception crash)
        {
            #region UI
            int screen = rnd.Next(1, 4);
            int y = 11;
            int y2 = 15;
            if (screen == 1)
            {
                y = 11;
                y2 = 15;
            }
            else if (screen == 2)
            {
                y = 14;
                y2 = 18;
            }
            else
            {
                y = 13;
                y2 = 17;
            }
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            if (screen == 1)
            {
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write("                                __            __                                ");
                Console.Write("                                \\_\\__      __/_/                                ");
                Console.Write("                               / _ \\ \\ /\\ / / _ \\                               ");
                Console.Write("                              | |_| \\ V  V / |_| |                              ");
                Console.Write("                               \\___/ \\_/\\_/ \\___/                               ");
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write(" Your system crashed. That's not good! Crash type: Kernel crash                 ");
                Console.Write(" Reason:                                                                        ");
                Console.Write("                                                                                ");
                Console.Write(" Your system will reboot if you press a key.                                    ");
                Console.Write("                                                                                ");
                Console.Write(" KERNEL VERSION: h                                                              ");
                Console.Write(" SYSTEM VERSION: h                                                              ");
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write("                                                                              ");
            }
            else if (screen == 2)
            {
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write("        _                                                                       ");
                Console.Write("       / )                                                                      ");
                Console.Write("   _  / /                                                                       ");
                Console.Write("  (_)( (                                                                        ");
                Console.Write("     | |                                                                        ");
                Console.Write("   _ ( (                                                                        ");
                Console.Write("  (_) \\ \\                                                                       ");
                Console.Write("       \\_)                                                                      ");
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write(" Your system crashed. That's not good! Crash type: Kernel crash                 ");
                Console.Write(" Reason:                                                                        ");
                Console.Write("                                                                                ");
                Console.Write(" Your system will reboot if you press a key.                                    ");
                Console.Write("                                                                                ");
                Console.Write(" KERNEL VERSION: h                                                              ");
                Console.Write(" SYSTEM VERSION: h                                                              ");
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write("                                                                              ");
            }
            else if (screen == 3)
            {
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write("    ___  ___  ___  ___          ________  ___  ___                              ");
                Console.Write("   |\\  \\|\\  \\|\\  \\|\\  \\        |\\   __  \\|\\  \\|\\  \\                             ");
                Console.Write("   \\ \\  \\\\\\  \\ \\  \\\\\\  \\       \\ \\  \\|\\  \\ \\  \\\\\\  \\                            ");
                Console.Write("    \\ \\  \\\\\\  \\ \\   __  \\       \\ \\  \\\\\\  \\ \\   __  \\                           ");
                Console.Write("     \\ \\  \\\\\\  \\ \\  \\ \\  \\       \\ \\  \\\\\\  \\ \\  \\ \\  \\ ___ ___ ___              ");
                Console.Write("      \\ \\_______\\ \\__\\ \\__\\       \\ \\_______\\ \\__\\ \\__\\\\__\\\\__\\\\__\\             ");
                Console.Write("       \\|_______|\\|__|\\|__|        \\|_______|\\|__|\\|__\\|__\\|__\\|__|             ");
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write(" Your system crashed. That's not good! Crash type: Kernel crash                 ");
                Console.Write(" Reason:                                                                        ");
                Console.Write("                                                                                ");
                Console.Write(" Your system will reboot if you press a key.                                    ");
                Console.Write("                                                                                ");
                Console.Write(" KERNEL VERSION: h                                                              ");
                Console.Write(" SYSTEM VERSION: h                                                              ");
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write("                                                                                ");
                Console.Write("                                                                              ");
            }
            #endregion
            #region Write reason
            Cosmos.System.PCSpeaker.Beep();
            Console.SetCursorPosition(17, y2);
            Console.Write(EnvVars.kernelVer);
            Console.SetCursorPosition(17, y2 + 1);
            Console.Write(EnvVars.shortversion);
            Console.SetCursorPosition(9, y);
            Console.Write(crash.Message);
            #endregion
            #region Reboot
            Console.ReadKey();
            Cosmos.Core.CPU.Reboot();
            #endregion
        }
    }
}
