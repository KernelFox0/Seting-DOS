/// 
/// Environment variable loader, Last modified: 2023. 11. 13.
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
using System.IO;

namespace Seting_DOS.Services
{
    public static class SystemData
    {
        public static string[] Load()
        {
            try
            {
                StreamReader host = new StreamReader(@"0:\SDOS\preferences\compName.pref");
                EnvVars.hostname = host.ReadToEnd();
                host.Close();
                StreamReader theme = new StreamReader(@"0:\SDOS\preferences\theme.dat");
                EnvVars.theme = theme.ReadToEnd();
                theme.Close();
                StreamReader verbose = new StreamReader(@"0:\SDOS\preferences\verboseBoot.pref");
                string content = verbose.ReadToEnd().ToLower().Trim(' ').Trim('\n');
                EnvVars.verboseMode = (content == "1");
                verbose.Close();
                StreamReader debug = new StreamReader(@"0:\SDOS\preferences\debugBoot.pref");
                content = debug.ReadToEnd().ToLower().Trim(' ').Trim('\n');
                EnvVars.debugBoot = (content == "1");
                debug.Close();
            }
            catch (Exception e)
            {
                string[] error = { "error", e.Message };
                return error;
            }
            string[] result = { "done", "System data loaded into environment variables" };
            return result;
        }
    }
}