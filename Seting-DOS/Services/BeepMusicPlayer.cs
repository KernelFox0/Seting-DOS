/// 
/// BeepMusic player, Last modified: 2023. 07. 30.
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
using Cosmos.HAL;
using Seting_DOS.Drivers;

namespace Seting_DOS.Services
{
    public static class BeepMusicPlayer
    {
        public static void MusicPlayer(string path, bool feedback = true) //BeepMusic player.
        {
            try
            {
                if (!File.Exists(path))
                {
                    if (feedback)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Beep.Sound.Error();
                        Console.WriteLine("Error: File doesn't exists!");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    return;
                } //If the file doesn't exists it can't play it (obviously)
                if (!path.EndsWith(".beep"))
                {
                    if (feedback)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Beep.Sound.Error();
                        Console.WriteLine("Error: File is not a beep music file!");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    return;
                } //If the file is not a BeepMusic file it can't play it
                if (EnvVars.mute)
                {
                    if (feedback)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Notice: System is muted. File will not play.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    return;
                } //If the system is muted it won't play it because it's unneccesary.
                #region Print Player
                int lines = TextOperations.GetLines(path);
                int prog = 0;
                if (feedback)
                {
                    Console.Write("[");
                    Console.Write("|");
                    for (int i = 1; i < lines - 1; i++)
                    {
                        Console.Write("=");
                    }
                    Console.Write("]\n");
                    Console.WriteLine("{0}/{1}", prog, lines);
                }
                int y = Console.GetCursorPosition().Top; //Console.Write("1");
                #endregion
                uint[] buffer = Beep.ReadFile(path); //Read BeepMusic file and fill it in the buffer
                bool hasDurData = TextOperations.DoesItContain(",", path); //Check if BeepMusic file specifies beep sound duration
                int len = buffer.Length; //Check length of buffer
                if (hasDurData) { len = len / 2; } //If it has duration data the buffer is double size so it needs to be "cut in half"
                for (int i = 0; i < len; i++)
                {
                    if (feedback)
                    {
                        prog++;
                        Console.SetCursorPosition(0, y - 1);
                        Console.Write("{0}/{1}", prog, lines);
                        Console.SetCursorPosition(prog, y - 2);
                        Console.Write("-");
                        Console.SetCursorPosition(1 + prog, y - 2);
                        Console.Write("|");
                    }
                    if (!hasDurData)
                    {
                        if (buffer[i] == 0)
                        {
                            Global.PIT.Wait(1000); //Wait note
                        }
                        else
                        {
                            Beep.PCBeep(buffer[i]); //Play note from buffer
                        }
                    }
                    else
                    {
                        if (buffer[i] == 0)
                        {

                            Global.PIT.Wait(buffer[i + 1] * 1000); //Wait note with duration
                        }
                        else if (buffer[i + 1] == 0)
                        {
                            Beep.PCBeep(buffer[i]); //Play note from buffer
                        }
                        else
                        {
                            Beep.PCBeep(buffer[i], buffer[i + 1]); //Play note with duration from buffer
                        }
                    }
                }
                if (feedback)
                {
                    /*Console.SetCursorPosition(0, y - 1);
					Console.Write("{0}/{1}", prog, lines);
					Console.SetCursorPosition(prog, y - 2);
					Console.Write("-");
					Console.SetCursorPosition(1 + prog, y - 2);
					Console.Write("|");*/
                    Console.SetCursorPosition(0, y);
                }
            }
            catch (Exception ex) //Catch any error
            {
                if (feedback)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error while playing file: {0}", ex.Message);
                    Beep.Sound.Error();
                    Console.ForegroundColor = ConsoleColor.White;
                }
                return;
            }
        }
    }
}