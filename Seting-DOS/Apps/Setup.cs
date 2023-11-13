/// 
/// System setup, Last modified: 2023. 11. 13.
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys = Cosmos.System;
using Cosmos.System.ExtendedASCII;
using System.IO;
using Cosmos.System.FileSystem;
using Cosmos.HAL;
using Seting_DOS.Drivers;
using Cosmos.System.FileSystem.VFS;
using Seting_DOS.Services;

namespace Seting_DOS.Apps
{
    public static class Setup
    {
        public static void Call()
        {
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
            Console.Write("                                                                                ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(" ENTER=Continue   F3=Quit and shutdown                                         ");
            #endregion
            ConsoleKeyInfo key;
            int setupMode = -1;
            key = Console.ReadKey();
            if (key.Key == ConsoleKey.Enter) { setupMode = 0; }
            else if (key.Key == ConsoleKey.F3) { setupMode = -1; }
            else { Call(); }
            if (setupMode == -1) { Sys.Power.Shutdown(); }
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
            Console.Write("    Seting-DOS 2022 Home/Professional, COSMOS Kernel                            ");
            Console.Write("                                                                                ");
            Console.Write("    END-USER LICENSE AGREEMENT FOR BETA SETING-DOS SOFTWARE                     ");
            Console.Write("                                                                                ");
            Console.Write("    IMPORTANT-READ CAREFULLY:                                                   ");
            Console.Write("    The software is licensed under GPL-3.0-or-later.                            ");
            Console.Write("    It can be viewed at: https://www.gnu.org/licenses/gpl-3.0.html              ");
            Console.Write("    Or you can read it with the 'license' command after the install process.    ");
            Console.Write("                                                                                ");
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
                VSFS.EmptyRootPartition(); Console.SetCursorPosition(24, 14); //Console.Write("█████████████████");
                                                                              //VSFS.FormatRoot(); Console.Write("██████████████");
                format = true;
                //goto here;
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
            Console.Write("    |                                                                       |   ");
            Console.Write("    |  Press Enter at password textbox if you don't want a user password.   |   ");
            Console.Write("    |                                                                       |   ");
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
            string password = Keyboard.KeyHandler(true, true);
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
                string tmp = Keyboard.KeyHandler(true, true);
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
            Console.Write(" System version: " + EnvVars.versionstring);
            #endregion
            //Remove _-s and spaces from username for folder name
            string userFolder = username.Replace(" ", "-").Replace("_", "-");
            Console.SetCursorPosition(8, 18);
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            #region Creating folders
            VSFS.act_dir = "/0/";
            VSFS.cur_dir = "0:\\"; Console.Write("██"); Global.PIT.Wait(10);
            VSFS.MakeDir("SDOS", true); Console.Write("██"); Global.PIT.Wait(10);
            VSFS.MakeDir("Users", true); Console.Write("██"); Global.PIT.Wait(10);
            VSFS.MakeDir("Applications", true); Console.Write("██"); Global.PIT.Wait(10);
            VSFS.act_dir = "/0/SDOS/";
            VSFS.cur_dir = "0:\\SDOS\\"; Console.Write("██"); Global.PIT.Wait(10);
            VSFS.MakeDir("Preferences", true); Console.Write("██"); Global.PIT.Wait(10);
            VSFS.MakeDir("ProgramData", true); Console.Write("██"); Global.PIT.Wait(10);
            VSFS.MakeDir("System", true); Console.Write("██"); Global.PIT.Wait(10);
            VSFS.MakeDir("etc", true); Console.Write("██"); Global.PIT.Wait(10);
            VSFS.act_dir = "/0/SDOS/ProgramData/";
            VSFS.cur_dir = "0:\\SDOS\\ProgramData\\";
            VSFS.MakeDir("MazeGame", true); Global.PIT.Wait(10);
            VSFS.act_dir = "/0/Users/";
            VSFS.cur_dir = "0:\\Users\\"; Console.Write("██"); Global.PIT.Wait(10);
            VSFS.MakeDir(userFolder, true); Console.Write("██"); Global.PIT.Wait(10);
            VSFS.act_dir = "/0/Users/" + userFolder + "/";
            VSFS.cur_dir = "0:\\Users\\" + userFolder + "\\"; Console.Write("██"); Global.PIT.Wait(10);
            VSFS.MakeDir("Documents", true); Console.Write("██"); Global.PIT.Wait(10);
            VSFS.MakeDir("Music", true); Console.Write("██"); Global.PIT.Wait(10);
            VSFS.MakeDir("AppData", true); Console.Write("██"); Global.PIT.Wait(10);
            VSFS.act_dir = "/0/Users/" + userFolder + "/AppData/";
            VSFS.cur_dir = "0:\\Users\\" + userFolder + "\\AppData\\";
            VSFS.MakeDir("MazeGame", true); Global.PIT.Wait(10);
            AliasManager.Prepare(false, false);
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
            Console.Write("███████"); Global.PIT.Wait(10);
            #endregion
            x = Console.GetCursorPosition().Left;
            Console.SetCursorPosition(9, 9);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("+");
            Console.SetCursorPosition(x, 18);
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            #region Creating and writing files
            StreamWriter cn = new StreamWriter(@"0:\SDOS\preferences\compName.pref"); 
            cn.Write("localhost");
            cn.Close(); Global.PIT.Wait(10); Console.Write("██");
            StreamWriter ver = new StreamWriter(@"0:\SDOS\version.dat");
            ver.Write(EnvVars.shortversion);
            ver.Close(); Global.PIT.Wait(10); Console.Write("██");
            StreamWriter mute = new StreamWriter(@"0:\SDOS\system\mute.dat"); Console.Write("██");
            mute.Write("0");
            mute.Close(); Global.PIT.Wait(10); Console.Write("██");
            StreamWriter vb = new StreamWriter(@"0:\SDOS\preferences\verboseBoot.pref"); Console.Write("██");
            vb.Write("0");
            vb.Close(); Global.PIT.Wait(10); Console.Write("█");
            StreamWriter db = new StreamWriter(@"0:\SDOS\preferences\debugBoot.pref"); Console.Write("██");
            db.Write("0");
            db.Close(); Global.PIT.Wait(10); Console.Write("█");
            StreamWriter pmft = new StreamWriter(@"0:\SDOS\preferences\pmft.pref");
            pmft.Write("progress");
            pmft.Close(); Global.PIT.Wait(10);
            Console.Write("██");
            StreamWriter thm = new StreamWriter(@"0:\SDOS\preferences\theme.dat"); Console.Write("██");
            thm.Write("classic");
            thm.Close(); Global.PIT.Wait(10); Console.Write("██");
            StreamWriter fn = new StreamWriter(@"0:\users\" + userFolder + @"\fullName.dat"); Console.Write("██");
            fn.Write(username);
            fn.Close(); Global.PIT.Wait(10); Console.Write("██");
            if (password != "" && password != null)
            {
                StreamWriter pw = new StreamWriter(@"0:\users\" + userFolder + @"\password.pwd");
                pw.Write(password);
                pw.Close(); Global.PIT.Wait(10);
                StreamWriter pr = new StreamWriter(@"0:\users\" + userFolder + @"\passRem.dat");
                pr.Write(reminder);
                pr.Close(); Global.PIT.Wait(10);
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