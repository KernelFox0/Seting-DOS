/// 
/// Set TextUI background color based on the user preference, Last modified: 2024. 06. 19.
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

using System;

namespace Seting_DOS.Services
{
	public static class TUIBGCol
	{
		public static void Set(bool systemApp = false)
		{
			if (systemApp)
			{
				if (EnvVars.systemTheme == "classic") { Console.BackgroundColor = ConsoleColor.Blue; }
				else { Console.BackgroundColor = ConsoleColor.Black; }
			}
			else
			{
				if (EnvVars.theme == "classic") { Console.BackgroundColor = ConsoleColor.Blue; }
				else { Console.BackgroundColor = ConsoleColor.Black; }
			}
		}
	}
}
