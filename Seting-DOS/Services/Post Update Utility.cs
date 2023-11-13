/// 
/// File for storing environment variables, Last modified: 2023. 11. 13.
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
using System.IO;
using Cosmos.HAL;

namespace Seting_DOS.Services
{
	public static class PostUpdateUtil
	{
		/// <summary>
		/// How does this work:
		/// In this example we assume the latest version is beta 1.2, and you are on beta 1.0
		/// 1. The system reads the version file located at /0/SDOS/version.dat and compares it to the
		/// shortVersion string in Services.EnvVars on every boot
		/// 2. If there's a difference the program will call the Update() method
		/// 3. It will update the system to the latest version in steps. What does this mean? Let's use our example.
		/// It won't update from b1.0 to b1.2. It will first update b1.0 to b1.1 then to b1.2.
		/// </summary>
		public static readonly string[] versionHistory = { "Seting-DOS b1.0" };
		public static string localVer;
		public static int rem = 0;

        public static string[] CheckVersionDifference()
		{
			try
			{
				string[] msg = { "info", "[Updater] Checking for updates..." };
				BootMSG.Write(msg, true);
                StreamReader storedVer = new StreamReader("0:\\SDOS\\version.dat");
				localVer = storedVer.ReadLine();
				storedVer.Close();
				if (localVer != EnvVars.shortversion)
				{
					Update();
					string[] ok = { "done", "[Updater] System updated. Booting can continue." };
					return ok;
				}
				else
				{
					string[] inf = { "info", "[Updater] Nothing to update. Booting can continue." };
					return inf;
				}
			}
			catch (Exception e)
			{
                string[] err = { "error", "[Updater] Updating failed!\nError message: " + e.Message + "\nPress any key to reboot..." };
				BootMSG.Write(err);
				Console.ReadKey();
				Drivers.Power.Reboot.ACPI();
				return null;
            }
        }
        public static void Update()
		{
			Console.WriteLine("[Updater] Updating...");
			if (localVer == versionHistory[0])
			{
				rem = versionHistory.Count();
				DrawScreen("Seting-DOS b1.0 to ");
				//Updater if version is b1.0. Empty because no new updates.
			}
			StreamWriter ver = new StreamWriter("0:\\SDOS\\version.dat");
			ver.WriteLine(EnvVars.shortversion);
			ver.Close();
		}
		public static void DrawScreen(string curTask)
		{
            TUIBGCol.Set();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White;
            Console.Write(" Seting-DOS Post Update Utility | Performing tasks...                           ");
            TUIBGCol.Set(); Console.ForegroundColor = ConsoleColor.White;
            Console.Write("                                                                                ");
            Console.Write(" Full task:                                                                     ");
            Console.Write(" Update system files from x to y                                                ");
            Console.Write("                                                                                ");
            Console.Write(" Current task:                                                                  ");
            Console.Write(" Updating from x to y                                                           ");
            Console.Write("                                                                                ");
            Console.Write(" Tasks remaining: z                                                             ");
            Console.Write("                                                                                ");
            Console.Write("                                                                                ");
            Console.Write(" Do not turn off or restart the computer unless you want to see what happens.   ");
            Console.Write(" The operating system may become unbootable if you do.                          ");
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
            Console.Write("                                                                               ");
            TUIBGCol.Set(); Console.ForegroundColor = ConsoleColor.White;
			Console.SetCursorPosition(26, 3); Console.Write("{0} to {1}", localVer, EnvVars.shortversion);
            Console.SetCursorPosition(15, 6); Console.Write(curTask);
            Console.SetCursorPosition(19, 8); Console.Write("{0} to {1}", localVer, EnvVars.shortversion);
        }
	}
}
