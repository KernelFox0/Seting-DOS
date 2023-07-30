/// 
/// Credits application, Last modified: 2023. 07. 30.
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

namespace Seting_DOS.Apps
{
    public static class Credits
    {
        public static void Show()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.Write(" Seting-DOS Color Operating System - Credits                                    ");
            Services.TUIBGCol.Set();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("                                                                                ");
            Console.Write(" OS Kernel: C# Open Source Managed Operating System (COSMOS)                    ");
            Console.Write(" Website: www.gocosmos.org                                                      ");
            Console.Write(" Github: github.com/CosmosOS/Cosmos                                             ");
            Console.Write("                                                                                ");
            Console.Write(" Developer: Kernel Fox                                                          ");
            Console.Write(" Reddit: u/MinecraftW06                                                         ");
            Console.Write(" Twitter: @KernelFox0                                                           ");
            Console.Write(" OS Github: github.com/KernelFox0/Seting-DOS                                    ");
            Console.Write("                                                                                ");
            Console.Write(" Written in C#                                                                  ");
            Console.Write(" If you have issues contact me on the given social media usernames or open an   ");
            Console.Write(" issue on the GitHub page. Same with feature suggestions.                       ");
            Console.Write("                                                                                ");
            Console.Write(" Other information:                                                             ");
            Console.Write(" This OS is a hobby project. Updates are not frequent.                          ");
            Console.Write(" The source code might contain unoptimized and/or messy code. Sorry not sorry.  ");
            Console.Write("                                                                                ");
            Console.Write(" Have personal problems that is not an OS bug? Do it yourself. It's open source.");
            Console.Write("                                                                                ");
            Console.Write("                                                                                ");
            Console.Write(" Press any key to exit...                                                       ");
            Console.Write("                                                                                ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(Services.EnvVars.versionstring);
            Services.TUIBGCol.Set();
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
        }
    }
}