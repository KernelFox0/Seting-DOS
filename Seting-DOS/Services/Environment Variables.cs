/// 
/// File for storing environment variables, Last modified: 2023. 07. 30.
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

namespace Seting_DOS.Services
{
	public static class EnvVars
	{
		public static string username = "unknown";
		public static string userFolder = "unknown";
		public static string hostname = "unknown";
		public static string theme = "classic";
		public static bool mute = false;

		public static bool verboseMode = false;
		public static bool hasPassword = false;
		public const string versionstring = "Seting-DOS Unreleased Alpha 0.1 Work In Progress, Codename: OwO";
		public static string shortversion = "Seting-DOS OwO UA WIP 0.1";
		public const string kernelVer = "Cosmos Dev Kit Commit 189f4e1";


		public static void Write(string type, string name, string value)
		{
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("({0})", type);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("{0}: ", name);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("{0}\n", value);
        }
		public static void WriteThem() //Write environment variables to terminal
        {
			Write("static string", "username", username);
            Write("static string", "userFolder", userFolder);
            Write("static string", "hostname", hostname);
            Write("static string", "theme", theme);
            Write("static bool", "mute", mute.ToString());
            Write("static bool", "verboseMode", verboseMode.ToString());
            Write("static bool", "hasPassword", hasPassword.ToString());
            Write("const string", "versionstring", versionstring);
            Write("const string", "kernelVer", kernelVer);
        }
	}
}
