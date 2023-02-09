/// 
/// Set TextUI background color based on the user preference, Last modified: 2022. 09. 24.
/// Made for Seting-DOS, feel free to use any code from this
/// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seting_DOS.Services
{
    public static class TUIBGCol
    {
        public static void Set()
        {
            if (Services.EnvVars.theme == "classic") { Console.BackgroundColor = ConsoleColor.Blue; }
            else { Console.BackgroundColor = ConsoleColor.Black; }
        }
    }
}
