/// 
/// User Privilege control service, Last modified: 2024. 03. 08.
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

using Seting_DOS.Apps;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Seting_DOS.Services
{
	public static class UPC
	{
		public static Privilege root = new Privilege(true, true, true, true, true, true, "Root", 0);
		public static Privilege GetPrivilegeOf(string user)
		{
			//Coming in a later version
			//Currently assumes everything has root Privilege because nothing else exists
			return root;
		}
		public static bool RequestAuthentication(Privilege requiredMinPrivilege) 
		{
			//check Privilege
			Privilege currentUser = GetPrivilegeOf("");
			if (requiredMinPrivilege.unsupervised)
			{
				if (currentUser.unsupervised) return true;
			}
			if (requiredMinPrivilege.canEditSystem)
			{
				if (!currentUser.canEditSystem) return false;
			}
			if (requiredMinPrivilege.canAccessFS)
			{
				if (!currentUser.canAccessFS) return false;
			}
			if (requiredMinPrivilege.containerNotRequired)
			{
				if (!currentUser.containerNotRequired) return false;
			}
			if (requiredMinPrivilege.canManageAliases)
			{
				if (!currentUser.canManageAliases) return false;
			}
			if (requiredMinPrivilege.notLoggedAccount)
			{
				if (!currentUser.notLoggedAccount) return false;
			}
			return true;
		}
		public static bool CallUPC(Privilege requiredPrivilege, string programName, string taskName, bool usesDefaultFailHandler = true) // requiredMinPrivilege: true is required, false is not; usesDefaultFailHandler: if program should 
		{
			if (EnvVars.hasPassword)
			{
				TUIBGCol.Set();
				Console.ForegroundColor = ConsoleColor.White;
				Console.Clear();
				Console.SetCursorPosition(0, 0);
				Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White;
				Console.Write(" Seting-DOS User Privilege Control   | Ctrl-X - Cancel                          ");
				TUIBGCol.Set(); Console.ForegroundColor = ConsoleColor.White;
				Console.Write(@"                                                                                ");
				Console.Write(@"    __                                                                          ");
				Console.Write(@"   /o \______    Seting-DOS User Privilege Control                              ");
				Console.Write("   \\__/-=\"=\"'                                                                   ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"   A program required authentication to perform a task. Press Ctrl-X to cancel. ");
				Console.Write(@"   Program name: c                                                              ");
				Console.Write(@"   Task name: c                                                                 ");
				Console.Write(@"   Required Privilege level: c                                                  ");
				Console.Write(@"   You have: c                                                                  ");
				Console.Write(@"                                                                                ");
				Console.Write(@"   Username:                                                                    ");
				Console.Write(@"   Enter password:                                                              ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.Write(@"                                                                                ");
				Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White;
				Console.Write(@"                                                                               ");
				Console.SetCursorPosition(1, 24);
				Console.Write(EnvVars.versionstring);
				TUIBGCol.Set(); Console.ForegroundColor = ConsoleColor.White;
				Console.SetCursorPosition(17, 8);
				Console.Write(programName);
				Console.SetCursorPosition(14, 9);
				Console.Write(taskName);
				Console.SetCursorPosition(29, 10);
				Console.Write(requiredPrivilege.PrivilegeName);
				Console.SetCursorPosition(13, 11);
				Console.Write(root.PrivilegeName); //TODO: replace with actual level after adding support for that
				Console.SetCursorPosition(13, 13);
				Console.Write(EnvVars.username);
				Console.SetCursorPosition(19, 14);
				string pwd = "";
				ConsoleKeyInfo p;
				int x = Console.GetCursorPosition().Left;
				int y = Console.GetCursorPosition().Top;
				int originY = Console.GetCursorPosition().Top;
				int originX = Console.GetCursorPosition().Left;
				while (true)
				{
					p = Console.ReadKey(true);
					if (p.Key == ConsoleKey.Enter)
					{
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
							pwd = pwd.Remove(pwd.Length - 1);
						}
						if (y > originY && x == 0)
						{
							Console.SetCursorPosition(79, y - 1);
							Console.Write(" ");
							Console.SetCursorPosition(79, y - 1);
							x = 79;
							y--;
							pwd = pwd.Remove(pwd.Length - 1);
						}
						if (y > originY && x > 0)
						{
							Console.SetCursorPosition(x - 1, y);
							Console.Write(" ");
							Console.SetCursorPosition(x - 1, y);
							x--;
							pwd = pwd.Remove(pwd.Length - 1);
						}
					}
					else if (p.Modifiers == ConsoleModifiers.Control && p.Key == ConsoleKey.X)
					{
						return false;
					}
					else
					{
						pwd += p.KeyChar;
						x++;
						Console.Write("*");
					}
					if (x == 80)
					{
						x = 0;
						y++;
					}
					Console.SetCursorPosition(x, y);
					TUIBGCol.Set();
					Console.ForegroundColor = ConsoleColor.White;
				}
				pwd = pwd.Trim('\n');
				StreamReader pw = new StreamReader("0:\\Users\\" + EnvVars.username + "\\password.pwd");
				string password = pw.ReadToEnd().Trim('\n');
				pw.Close();
				if (password == pwd)
				{
					return true;
				}
			}
			if (usesDefaultFailHandler) DefaultFailHandler();
			return false;
		}
		private static void DefaultFailHandler()
		{
			TUIBGCol.Set();
			Console.ForegroundColor = ConsoleColor.White;
			Console.Clear();
			Console.SetCursorPosition(0, 0);
			Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White;
			Console.Write(" Seting-DOS User Privilege Control   | Authentication failed                    ");
			TUIBGCol.Set(); Console.ForegroundColor = ConsoleColor.White;
			Console.Write(@"            ///                                                                 ");
			Console.Write(@"    __    ///                                                                   ");
			Console.Write(@"   /o \_///__    Seting-DOS User Privilege Control                              ");
			Console.Write("   \\__///\"=\"'                                                                   ");
			Console.Write(@"    ///                                                                         ");
			Console.Write(@"  ///                                                                           ");
			Console.Write(@"                                                                                ");
			Console.Write(@"   Authentication failed! Wrong password!                                       ");
			Console.Write(@"   The task won't be performed.                                                 ");
			Console.Write(@"                                                                                ");
			Console.Write(@"   Press any key to exit...                                                     ");
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
			Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White;
			Console.Write(@"                                                                               ");
			Console.SetCursorPosition(1, 24);
			Console.Write(EnvVars.versionstring);
			TUIBGCol.Set(); Console.ForegroundColor = ConsoleColor.Red;
			Console.SetCursorPosition(12, 1);
			Console.Write("///");
			Console.SetCursorPosition(10, 2);
			Console.Write("///");
			Console.SetCursorPosition(8, 3);
			Console.Write("///");
			Console.SetCursorPosition(6, 4);
			Console.Write("///");
			Console.SetCursorPosition(4, 5);
			Console.Write("///");
			Console.SetCursorPosition(2, 6);
			Console.Write("///");
			Console.SetCursorPosition(27, 11);
			TUIBGCol.Set(); Console.ForegroundColor = ConsoleColor.White;
			Console.ReadKey(true);
			Console.Clear();
			return;
		}
	}
	public struct Privilege
	{
		public bool unsupervised; //root Privilege
		public bool canEditSystem; //admin: can edit system, system FS access, can't access other's containers normally
		public bool canAccessFS; //user: Can edit rootFS and own container, but not systen
		public bool containerNotRequired; //untrusted: can only access their container, no prefs
		public bool canManageAliases; //guest: can't add user aliases
		public bool notLoggedAccount; //if account should be logged or not

		public string PrivilegeName;
		public int PrivilegeID;

		public Privilege(bool unsupervisedV, bool canEditSystemV, bool canAccessFSV, bool containerNotRequiredV, bool canManageAliasesV, bool notLoggedAccountV, string PrivilegeNameV, int PrivilegeIDV) 
		{
			unsupervised = unsupervisedV;
			canEditSystem = canEditSystemV;
			canAccessFS = canAccessFSV;
			containerNotRequired = containerNotRequiredV;
			canManageAliases = canManageAliasesV;
			notLoggedAccount = notLoggedAccountV;
			PrivilegeName = PrivilegeNameV;
			PrivilegeID = PrivilegeIDV;
		}
	}
}
