/// 
/// Load status message, mainly used on boot while loading drivers, Last modified: 2023. 07. 30.
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
using Seting_DOS;

namespace Seting_DOS.Services
{
    public static class BootMSG
    {
        public static void Write(string[] msg) //Writes bootup status messages to the screen
        {
            /* done: OK; Driver loaded
             * info: INFO; Driver loaded but has a message
             * warning: WARN; Driver partially loaded, had an error
             * error: ERROR; Driver not loaded
             */
            if (EnvVars.verboseMode)
            {
                Console.Write("[");
                if (msg[0] == "done") { Console.ForegroundColor = ConsoleColor.Green; Console.Write("OK"); }
                else if (msg[0] == "info") { Console.ForegroundColor = ConsoleColor.Blue; Console.Write("INFO"); }
                else if (msg[0] == "warning") { Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("WARN"); }
                else if (msg[0] == "error") { Console.ForegroundColor = ConsoleColor.Red; Console.Write("ERROR"); }
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("] " + msg[1] + "\n");
            }
        }
    }
}