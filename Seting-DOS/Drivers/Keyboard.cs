/// 
/// Keyboard driver, Last modified: 2022. 09. 30.
/// Made for Seting-DOS, feel free to use any code from this
/// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seting_DOS.Drivers
{
    public static class Keyboard
    {
        public static string[] Load()
        {
            try { Console.InputEncoding = Encoding.GetEncoding(437); }
            catch (Exception e)
            {
                string[] error = { "error", e.Message };
                return error;
            }
            string[] result = { "done", "Keyboard driver loaded" };
            return result;
        }
        public static string KeyHandler(bool isTextUI = false, bool isPassword = false)
        {
            string cmd = "";
            ConsoleKeyInfo p;
            int x = Console.GetCursorPosition().Left;
            int y = Console.GetCursorPosition().Top;
            int originY = Console.GetCursorPosition().Top;
            int originX = Console.GetCursorPosition().Left;
            while (true)
            {
                p = Console.ReadKey();
                if (p.Key == ConsoleKey.Enter)
                {
                    Console.Write("\n");
                    break;
                }
                else if (p.Key == ConsoleKey.Backspace)
                {
                    if (x > originX && y == originY)
                    {
                        Console.SetCursorPosition(x - 1, y);
                        Console.Write(" ");
                        Console.SetCursorPosition(x - 1, y);
                        x--;
                        cmd = cmd.Remove(cmd.Length - 1);
                    }
                    if (y > originY && x == 0)
                    {
                        Console.SetCursorPosition(79, y - 1);
                        Console.Write(" ");
                        Console.SetCursorPosition(79, y - 1);
                        x = 79;
                        y--;
                        cmd = cmd.Remove(cmd.Length - 1);
                    }
                    if (y > originY && x > 0)
                    {
                        Console.SetCursorPosition(x - 1, y);
                        Console.Write(" ");
                        Console.SetCursorPosition(x - 1, y);
                        x--;
                        cmd = cmd.Remove(cmd.Length - 1);
                    }
                }
                else if (p.Key == ConsoleKey.F9)
                {
                    TextUI.LogonUI.LockScreen();
                }
                else if (p.Key == ConsoleKey.F1)
                {
                    Drivers.Beep.Mute();
                }
                else
                {
                    cmd += p.KeyChar;
                    x++;
                    if (isPassword)
                    {
                        Console.SetCursorPosition(Console.GetCursorPosition().Left - 1, Console.GetCursorPosition().Top);
                        Console.Write("*");
                    }
                }
                if (x == 80)
                {
                    x = 0;
                    y++;
                }
                if (!isTextUI) { Apps.StatusBar.TerminalDisp(); }
                Console.SetCursorPosition(x, y);
                if (isTextUI)
                {
                    Services.TUIBGCol.Set();
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            return cmd;
        }
    }
}
