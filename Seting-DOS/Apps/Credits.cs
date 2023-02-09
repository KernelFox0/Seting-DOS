using System;

namespace Seting_DOS.Apps
{
    public static class Credits
    {
        public static void Show()
        {
            Services.TUIBGCol.Set();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.Write(" Seting-DOS Color Operating System - Credits                                    ");
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
            Console.Write(" This is a hobby project. Updates are not frequent.                             ");
            Console.Write(" The source code might contain unoptimized and/or messy code. Sorry not sorry.  ");
            Console.Write("                                                                                ");
            Console.Write(" Have personal problems that is not an OS bug? Do it yourself. It's open source.");
            Console.Write("                                                                                ");
            Console.Write("                                                                                ");
            Console.Write("                                                                                ");
            Console.Write("                                                                                ");
            Console.Write(Services.EnvVars.versionstring);
        }
    }
}