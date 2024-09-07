/// 
/// System terminal UI, Last modified: 2024. 06. 19.
/// 
/// Copyright (C) 2023-
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

using Seting_DOS.Drivers;
using Seting_DOS.Services;
using System;

namespace Seting_DOS.Apps
{
    public static class Terminal
    {
        public static string[] Init()
        {
            try
            {
                VSFS.Zerosix();
            }
            catch (Exception e)
            {
                string[] msg = { "error", e.Message };
                return msg;
            }
            string[] ok = { "done", "Terminal shell initialized successfully" };
            return ok;
        }
        public static string WriteShell()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(EnvVars.username.ToLower());
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("@");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(EnvVars.hostname.ToLower());
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" " + VSFS.act_dir + "$ ");
            return Keyboard.KeyHandler();
        }
    }
}
