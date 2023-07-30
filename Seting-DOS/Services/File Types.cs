/// 
/// Seting-DOS File type name reference file, Last modified: 2023. 07. 30.
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

namespace Seting_DOS.Services
{
	public static class FileTypes
	{
		public static string GiveTypeOwO(string extension)
		{
			if (!extension.StartsWith(".")) { extension = "." + extension; }
			string returnVal = "";
			switch (extension)
			{
				default: returnVal = "Unknown"; break;
				case ".txt": returnVal = "Text"; break;
				case ".beep": returnVal = "Beep Music"; break;
				case ".pref": returnVal = "Preference"; break;
				case ".dat": returnVal = "Data"; break;
				case ".pwd": returnVal = "Password"; break;
				case ".idp": returnVal = "Install Type Data"; break;
				case ".mze": returnVal = "Maze Game Level"; break;
			}
			return returnVal + " File";
		}
	}
}
