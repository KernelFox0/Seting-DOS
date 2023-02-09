/// 
/// File for storing environment variables, Last modified: 2022. 09. 24.
/// Made for Seting-DOS, feel free to use any code from this
/// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		public const string kernelVer = "Cosmos Dev Kit v106027";


		public static void WriteThem() //Write environment variables to terminal
        {
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("(static string)");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("username: ");
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.Write("{0}\n", username);
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("(static string)");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("userFolder: ");
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.Write("{0}\n", userFolder);
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("(static string)");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("hostname: ");
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.Write("{0}\n", hostname);
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("(static string)");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("theme: ");
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.Write("{0}\n", theme);
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("(static bool)");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("mute: ");
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.Write("{0}\n", mute);
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("(static bool)");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("verboseMode: ");
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.Write("{0}\n", verboseMode);
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("(static bool)");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("hasPassword: ");
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.Write("{0}\n", hasPassword);
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("(const string)");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("versionstring: ");
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.Write("{0}\n", versionstring);
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("(const string)");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("kernelVer: ");
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.Write("{0}\n", kernelVer);
		}
	}
}
