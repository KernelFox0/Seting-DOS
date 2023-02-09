/// 
/// Load status message, mainly used on boot while loading drivers, Last modified: 2022. 09. 24.
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