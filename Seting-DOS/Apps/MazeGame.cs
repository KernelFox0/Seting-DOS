/// 
/// Basic maze game, Last modified: 2023. 07. 31.
/// Game version: v1.0
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

using Seting_DOS.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Seting_DOS.Apps
{
	public static class MazeGame
	{
		static List<string> blocksList = new List<string>();
		static int startX = 0;
		static int startY = 0;
		static int finishX = 0;
		static int finishY = 0;
		static int maxMoves = 0;
		static int moves = 0;
		static int playerX = 0;
		static int playerY = 0;
		static int startTimeM = 0;
		static int endTimeM = 0;
		static int startTimeS = 0;
		static int endTimeS = 0;
		public static void Start(string arg = "")
		{
			if (arg.Length > 0 && arg.StartsWith("0:\\"))
			{
				if (File.Exists(arg))
				{
					Load(arg);
				}
				else
				{
					Messages.Error("File doesn't exist!");
				}
			}
			else
			{
				Console.BackgroundColor = ConsoleColor.White;
				Console.ForegroundColor = ConsoleColor.Black;
				Console.Clear();
				Console.SetCursorPosition(0, 0);
				Console.Write(" Seting-DOS Color Operating System - Basic maze game                            ");
				TUIBGCol.Set();
				Console.ForegroundColor = ConsoleColor.White;
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                               ______                                           ");
				Console.Write(@"          |\                  /     |             \ /   O            \   /      ");
				Console.Write(@"          | \                 | --- |              |    |             \ /       ");
				Console.Write(@"          | /                 | --- |              |    |             / \       ");
				Console.Write(@"          |/                  |_____|              |   ( )           /   \      ");
				Console.Write(@"    Play main level       Load level file       Create level       Exit game    ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                               Game version: v1.0                               ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.BackgroundColor = ConsoleColor.White;
				Console.ForegroundColor = ConsoleColor.Black;
				Console.Write(EnvVars.versionstring);
				bool isInSelector = true;
				int selected = 1;
				ConsoleKeyInfo key;
				while (isInSelector)
				{
					Console.ForegroundColor = ConsoleColor.White;
					Console.BackgroundColor = ConsoleColor.Black;
					Console.SetCursorPosition(0, 12); Console.Write(@"    Play main level       Load level file       Create level       Exit game    ");
					Console.ForegroundColor = ConsoleColor.Black;
					Console.BackgroundColor = ConsoleColor.White;
					if (selected == 1)
					{
						Console.SetCursorPosition(4, 12);
						Console.Write("Play main level");
					}
					else if (selected == 2)
					{
						Console.SetCursorPosition(26, 12);
						Console.Write("Load level file");
					}
					else if (selected == 3)
					{
						Console.SetCursorPosition(48, 12);
						Console.Write("Create level");
					}
					else
					{
						Console.SetCursorPosition(67, 12);
						Console.Write("Exit game");
					}
					key = Console.ReadKey(true);
					if (key.Key == ConsoleKey.LeftArrow)
					{
						if (selected == 1)
						{
							selected = 4;
						}
						else
						{
							selected--;
						}
					}
					else if (key.Key == ConsoleKey.RightArrow)
					{
						if (selected == 4)
						{
							selected = 1;
						}
						else
						{
							selected++;
						}
					}
					else if (key.Key == ConsoleKey.Enter)
					{
						isInSelector = false; break;
					}
				}
				if (selected == 1) { Load(); }
				else if (selected == 2) { Load("file"); }
				else if (selected == 3) { Create(); }
				else if (selected == 4)
				{
					Exit();
				}
			}
		}
		public static void Load(string file = null)
		{
			if (file == "file")
			{
				Console.BackgroundColor = ConsoleColor.White;
				Console.ForegroundColor = ConsoleColor.Black;
				Console.Clear();
				Console.SetCursorPosition(0, 0);
				Console.Write(" Seting-DOS Color Operating System - Basic maze game                            ");
				TUIBGCol.Set();
				Console.ForegroundColor = ConsoleColor.White;
				Console.Write(@"   ______                                                                       ");
				Console.Write(@"  /     |                                                                       ");
				Console.Write(@"  | --- |                                                                       ");
				Console.Write(@"  | --- |                                                                       ");
				Console.Write(@"  |_____|                                                                       ");
				Console.Write(@"  Specify path of file. If only file name is added it will be loaded from the   ");
				Console.Write(@"  game's AppData folder from your user directory. Don't enter anything to go    ");
				Console.Write(@"  back to the menu.                                                             ");
				Console.Write(@"                                                                                ");
				Console.Write(@"  Path:                                                                         ");
				Console.Write(@"  [                                                                             ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.BackgroundColor = ConsoleColor.White;
				Console.ForegroundColor = ConsoleColor.Black;
				Console.Write(EnvVars.versionstring);
				Console.SetCursorPosition(4, 11);
				TUIBGCol.Set();
                Console.ForegroundColor = ConsoleColor.White;
                file = Console.ReadLine();
				if (file == "") { Start(); return; }
				else if (!file.StartsWith("/")) { file = "/0/Users/" + EnvVars.username + "/AppData/MazeGame/" + file; }
				if (!File.Exists(file))
				{
					Console.SetCursorPosition(2, 9);
					Console.ForegroundColor = ConsoleColor.Red;
					Console.Write("[X] File doesn't exist! Press ESC to go back to menu or any key to re-enter...");
					Drivers.Beep.Sound.Error();
					ConsoleKeyInfo key = Console.ReadKey();
					if (key.Key == ConsoleKey.Escape) { Start(); }
					else { Load(); return; }
				}
			}
			else if (file == null)
			{
				file = "0:\\SDOS\\ProgramData\\MazeGame\\main.mze";
			}
			try
			{
				FileInfo read = new(file);
				int lines = CountLinesLINQ(read) - 3;
				StreamReader maze = new StreamReader(file);
				string rawmoves = maze.ReadLine();
				string[] rawstart = maze.ReadLine().Split(',');
				string[] rawgoal = maze.ReadLine().Split(',');
				maxMoves = Convert.ToInt32(rawmoves);
				startX = Convert.ToInt32(rawstart[0]) + 1;
				startY = Convert.ToInt32(rawstart[1]) + 1;
				finishX = Convert.ToInt32(rawgoal[0]);
				finishY = Convert.ToInt32(rawgoal[1]) + 1;
				playerX = startX - 1;
				playerY = startY - 1;
				for (int i = 0; i < (lines - 1); i++)
				{
					blocksList.Add(maze.ReadLine());
				}
			}
			catch (Exception ex)
			{
				Console.BackgroundColor = ConsoleColor.Black;
				Console.ForegroundColor = ConsoleColor.White;
				Console.Clear();
				Console.SetCursorPosition(0, 0);
				Messages.Error("Loading game values failed!\nError message: " + ex.Message); Console.ReadKey();
				return;
			}
			Render();
		}
		public static void Render()
		{
			Console.BackgroundColor = ConsoleColor.Black; Console.ForegroundColor = ConsoleColor.White;
			Console.Clear();
			Console.Write(@" Moves: e                                                     Press ESC to exit ");
			Console.Write(@"                                                                                ");
			Console.Write(@"                                                                                ");
			Console.Write(@"                                                                                ");
			Console.Write(@"                                                                                ");
			Console.Write(@"                                                                                ");
			Console.Write(@"                                                                                ");
			Console.Write(@"                                                                                ");
			Console.Write(@"                                                                                ");
			Console.Write(@"                                                                                ");
			Console.Write(@"                                                                                ");
			Console.Write(@"                                                                                ");
			Console.Write(@"                                                                                ");
			Console.Write(@"                                                                                ");
			Console.Write(@"                                                                                ");
			Console.Write(@"                                                                                ");
			Console.Write(@"                                                                                ");
			Console.Write(@"                                                                                ");
			Console.Write(@"                                                                                ");
			Console.Write(@"                                                                                ");
			Console.Write(@"                                                                                ");
			Console.Write(@"                                                                                ");
			Console.Write(@"                                                                                ");
			Console.Write(@"                                                                                ");
			Console.Write(@"                                                                               ");
			string[] blocks = blocksList.ToArray();
			string[] buffer = new string[2];
			int bufferX;
			int bufferY;
			for (int i = 0; i < blocksList.Count; i++)
			{
				buffer = blocks[i].Split(',');
				bufferX = Convert.ToInt32(buffer[0]) + 1;
				bufferY = Convert.ToInt32(buffer[1]) + 1;
				Console.SetCursorPosition(bufferX, bufferY);
				Console.Write("█");
			}
			Console.ForegroundColor = ConsoleColor.Green;
			Console.SetCursorPosition(finishX, finishY);
			Console.Write("█");
			Console.ForegroundColor = ConsoleColor.White;
			Console.SetCursorPosition(8, 0);
			if (maxMoves > 0) { Console.Write(moves + "/" + maxMoves); }
			else { Console.Write(moves); }
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.SetCursorPosition(startX, startY);
			Console.Write("█");
			Play();
		}
		public static void Play()
		{
			string[] time = Drivers.RTC.GetTime(true);
			startTimeM = Convert.ToInt32(time[1]);
			startTimeS = Convert.ToInt32(time[2]);
			ConsoleKeyInfo key;
			bool game = true;
			bool validMove = true;
			int bufferX = 0, bufferY = 0;
			string buffer;
			string[] blocks = blocksList.ToArray();
			while (game)
			{
				key = Console.ReadKey(true);
				if (key.Key == ConsoleKey.UpArrow)
				{
					bufferX = playerX;
					bufferY = playerY - 1;
					buffer = bufferX + "," + bufferY;
					foreach (string check in blocks)
					{
						if (buffer == check)
						{
							validMove = false; break;
						}
					}
					if (bufferY < 0 || bufferY > 22)
					{
						validMove = false;
					}
				}
				else if (key.Key == ConsoleKey.DownArrow)
				{
					bufferX = playerX;
					bufferY = playerY + 1;
					buffer = bufferX + "," + bufferY;
					foreach (string check in blocks)
					{
						if (buffer == check)
						{
							validMove = false; break;
						}
					}
					if (bufferY < 0 || bufferY > 22)
					{
						validMove = false;
					}
				}
				else if (key.Key == ConsoleKey.RightArrow)
				{
					bufferX = playerX + 1;
					bufferY = playerY;
					buffer = bufferX + "," + bufferY;
					foreach (string check in blocks)
					{
						if (buffer == check)
						{
							validMove = false; break;
						}
					}
					if (bufferX < 0 || bufferX > 78)
					{
						validMove = false;
					}
				}
				else if (key.Key == ConsoleKey.LeftArrow)
				{
					bufferX = playerX - 1;
					bufferY = playerY;
					buffer = bufferX + "," + bufferY;
					foreach (string check in blocks)
					{
						if (buffer == check)
						{
							validMove = false; break;
						}
					}
					if (bufferX < 0 || bufferX > 78)
					{
						validMove = false;
					}
				}
				else if (key.Key == ConsoleKey.Escape)
				{
					Exit();
					game = false;
					return;
				}
				if (validMove)
				{
					moves++;
					Console.SetCursorPosition(playerX + 1, playerY + 1);
					Console.Write(" ");
					Console.BackgroundColor = ConsoleColor.Black;
					Console.SetCursorPosition(bufferX + 1, bufferY + 1);
					Console.ForegroundColor = ConsoleColor.Blue;
					Console.Write("█");
					playerX = bufferX;
					playerY = bufferY;
				}
				Console.SetCursorPosition(8, 0);
				Console.ForegroundColor = ConsoleColor.White;
				if (maxMoves > 0) { Console.Write(moves + "/" + maxMoves); }
				else { Console.Write(moves); }
				if (playerX == (finishX - 1) && playerY == (finishY - 1)) //Win :3
				{
					game = false;
					Stop(true);
				}
				else if (maxMoves > 0 && moves == maxMoves) //Lose :(
				{
					game = false;
					Stop(false);
				}
				Console.SetCursorPosition(playerX + 1, playerY + 1);
				bufferX = 0;
				bufferY = 0;
				buffer = "";
				validMove = true;
			}
		}
		public static void Stop(bool win)
		{
			string[] time = Drivers.RTC.GetTime(true);
			endTimeM = Convert.ToInt32(time[1]);
			endTimeS = Convert.ToInt32(time[2]);
			int timeM = endTimeM - startTimeM;
			int timeS = endTimeS - startTimeS;
			if (timeS < 0)
			{
				timeM--;
				timeS = 60 + timeS;
			}
			string playTime = timeM + ":" + timeS;
			if (timeS.ToString().Length == 1) { playTime = timeM + ":0" + timeS; } 
			int finalMoves = moves;
			int finalMaxM = maxMoves;
			blocksList.Clear();
			startX = 0;
			startY = 0;
			finishX = 0;
			finishY = 0;
			maxMoves = 0;
			moves = 0;
			playerX = 0;
			playerY = 0;
			startTimeM = 0;
			endTimeM = 0;
			startTimeS = 0;
			endTimeS = 0;
			if (win)
			{
				Console.BackgroundColor = ConsoleColor.White;
				Console.ForegroundColor = ConsoleColor.Black;
				Console.Clear();
				Console.SetCursorPosition(0, 0);
				Console.Write(" Seting-DOS Color Operating System - Basic maze game                            ");
				TUIBGCol.Set();
				Console.ForegroundColor = ConsoleColor.White;
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"      ..-'''-.                                                                  ");
				Console.Write(@"      \.-'''\ \         Congratulations! You won the game!                      ");
				Console.Write(@"   ,.--.     | |                                                                ");
				Console.Write(@"  //    \ __/ /         You completed the maze!                                 ");
				Console.Write(@"  \\    /|_  '.         Game stats:                                             ");
				Console.Write(@"   `'--'    `.  \       Moves:                                                  ");
				Console.Write(@"   ,.--.      \ '.      Time (mm:ss):                                           ");
				Console.Write(@"  //    \      , |                                                              ");
				Console.Write(@"  \\    /      | |      Press any key to return to menu...                      ");
				Console.Write(@"   `'--'      / ,'                                                              ");
				Console.Write(@"      -....--'  /                                                               ");
				Console.Write(@"      `.. __..-'                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.BackgroundColor = ConsoleColor.White;
				Console.ForegroundColor = ConsoleColor.Black;
				Console.Write(EnvVars.versionstring);
				TUIBGCol.Set();
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(31, 8);
				if (maxMoves > 0)
				{
					Console.Write(finalMoves + "/" + finalMaxM);
				}
				else
				{
					Console.Write(finalMoves);
				}
				Console.SetCursorPosition(38, 9);
				Console.Write(playTime);
			}
			else
			{
				Console.BackgroundColor = ConsoleColor.White;
				Console.ForegroundColor = ConsoleColor.Black;
				Console.Clear();
				Console.SetCursorPosition(0, 0);
				Console.Write(" Seting-DOS Color Operating System - Basic maze game                            ");
				TUIBGCol.Set();
				Console.ForegroundColor = ConsoleColor.White;
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"               _                                                                ");
				Console.Write(@"             .' )       Oh no! You lost the game.                               ");
				Console.Write(@"   ,.--.    / .'                                                                ");
				Console.Write(@"  //    \  / /          You ran out of moves.                                   ");
				Console.Write(@"  \\    / / /                                                                   ");
				Console.Write(@"   `'--' . '            Press any key to return to menu...                      ");
				Console.Write(@"   ,.--. | |                                                                    ");
				Console.Write(@"  //    \' '                                                                    ");
				Console.Write(@"  \\    / \ \                                                                   ");
				Console.Write(@"   `'--'   \ \                                                                  ");
				Console.Write(@"            \ '.                                                                ");
				Console.Write(@"             '._)                                                               ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.BackgroundColor = ConsoleColor.White;
				Console.ForegroundColor = ConsoleColor.Black;
				Console.Write(EnvVars.versionstring);
			}
			Console.ReadKey();
			Start();
		}
		public static void Exit()
		{
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            startX = 0;
            startY = 0;
            finishX = 0;
            finishY = 0;
            maxMoves = 0;
            moves = 0;
            playerX = 0;
            playerY = 0;
            startTimeM = 0;
            endTimeM = 0;
            startTimeS = 0;
            endTimeS = 0;
			blocksList.Clear();
        }
		public static void Create()
		{
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.Clear();
			Console.SetCursorPosition(0, 0);
			Console.Write(" Seting-DOS Color Operating System - Maze game creator                          ");
			TUIBGCol.Set();
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write("                                                                                ");
			Console.Write(" The creator is currently not implemented to the game :(                        ");
			Console.Write(" Will get added in game update v1.1, but the game is not updated in every OS    ");
			Console.Write(" update.                                                                        ");
			Console.Write("                                                                                ");
			Console.Write(" It is still possible to create a level but it is a little harder.              ");
			Console.Write(" You need to create a text document with the .mze extension.                    ");
			Console.Write("                                                                                ");
			Console.Write(" The first part of the file is the max number of moves. 0 means there's no max. ");
			Console.Write(" The second part is the starting coordinate. It looks like this: x,y            ");
			Console.Write(" The third part is the goal coordinate. This is where the maze ends. It also    ");
			Console.Write(" uses the x,y format. The comma is important.                                   ");
			Console.Write(" All the other parts are the block coordinates. Every line is a block. Those    ");
			Console.Write(" blocks are the walls of your maxe. It also uses the x,y format.                ");
			Console.Write("                                                                                ");
			Console.Write(" Drawing a map in a paint program and converting it using ImageMagick can help. ");
			Console.Write("                                                                                ");
			Console.Write(" Max coordinate values:                                                         ");
			Console.Write(" X: 78                                                                          ");
			Console.Write(" Y: 22                                                                          ");
			Console.Write("                                                                                ");
			Console.Write(" Press any key to go back...                                                    ");
			Console.Write("                                                                                ");
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.Write(EnvVars.versionstring);
			TUIBGCol.Set();
			Console.ForegroundColor = ConsoleColor.White;
			Console.ReadKey();
			Console.BackgroundColor = ConsoleColor.Black;
			Console.Clear();
			Start(null);
		}
		static int CountLinesLINQ(FileInfo file)
	=> File.ReadLines(file.FullName).Count();
	}
}