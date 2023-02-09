/// 
/// Logon window and user loader, Last modified: 2022. 10. 09.
/// Made for Seting-DOS, feel free to use any code from this
/// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Seting_DOS.Drivers;
using Sys = Cosmos.System;

namespace Seting_DOS.TextUI
{
	public static class LogonUI
	{
		public static void LockScreen()
		{
			#region Write UI
			Services.TUIBGCol.Set();
			Console.ForegroundColor = ConsoleColor.White;
			Console.Clear();
			Console.SetCursorPosition(0, 0);
			Console.Write(" Seting-DOS User Logon Screen - Locked                                          ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                    Press CTRL+ALT+DEL to unlock this system                    ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                               ");
			Console.SetCursorPosition(0, Console.GetCursorPosition().Top); Console.Write(Services.EnvVars.versionstring);
			#endregion
			ConsoleKeyInfo key = Console.ReadKey();
			if (key.Modifiers == (ConsoleModifiers.Control | ConsoleModifiers.Alt) && key.Key == ConsoleKey.Delete)
            {
				LogonScreen();
            }
			else { LockScreen(); }
		}
		public static void LogonScreen()
        {
			#region Write UI
			Services.TUIBGCol.Set();
			Console.ForegroundColor = ConsoleColor.White;
			Console.Clear();
			Console.SetCursorPosition(0, 0);
			Console.Write(" Seting-DOS User Logon Screen - Log in to system                                ");
			Console.Write("                                                                                ");
			Console.Write(" Select account to log into (Navigate with arrows, select with Enter):          ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                               ");
			Console.SetCursorPosition(0, Console.GetCursorPosition().Top); Console.Write(Services.EnvVars.versionstring);
			#endregion
			string[] users = GetUserAccounts(); //Retrieve account list
			#region Print accounts to screen
			Console.SetCursorPosition(1, 3);
			int y = Console.GetCursorPosition().Top;
			int userN = 0;
			foreach (var user in users)
            {
				if (userN == 0)
				{
					Console.BackgroundColor = ConsoleColor.White;
					Console.ForegroundColor = ConsoleColor.Black;
					Console.Write(users[0]);
					Services.TUIBGCol.Set();
					Console.ForegroundColor = ConsoleColor.White;
				}
				else
                {
					Console.SetCursorPosition(1, y);
					Console.Write(user);
				}
				userN++;
				y++;
			}
			#endregion
			#region Select User
			int current = 0;
			ConsoleKeyInfo key = Console.ReadKey();
			if (key.Key == ConsoleKey.Enter)
            {
				StreamReader name = new StreamReader(@"0:\Users\" + users[0] + @"\fullName.dat");
				string usr = name.ReadToEnd();
				name.Close();
				PasswordScreen(users[current], usr);
            }
			else
            {
				LogonScreen();
            }
            #endregion
        }
		public static void PasswordScreen(string folderName, string username, bool wrong = false, int tries = 5)
        {
			#region Write UI
			Services.TUIBGCol.Set();
			Console.ForegroundColor = ConsoleColor.White;
			Console.Clear();
			Console.SetCursorPosition(0, 0);
			Console.Write(" Seting-DOS User Logon Screen - Log in to system                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write(" Selected user: ¤                                                               ");
			Console.Write(" [!] User is protected by password!                                             ");
			Console.Write(" Password: [                                                                    ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                                ");
			Console.Write("                                                                               ");
			Console.SetCursorPosition(0, Console.GetCursorPosition().Top); Console.Write(Services.EnvVars.versionstring);
			#endregion
			if (File.Exists(@"0:\systemLocker.lck"))
            {
				Console.SetCursorPosition(0, 8);
				Console.Write("              === System Protection Message ======================              ");
				Console.Write("              |                                                  |              ");
				Console.Write("              | You entered a wrong password five (5) times. As  |              ");
				Console.Write(" Selected user| a result, System Protection Service locked your  |              ");
				Console.Write(" [!] User is p| computer for five (5) minutes.                   |              ");
				Console.Write(" Password: [  |                                                  |              ");
				Console.Write("              | X:XX minute(s) remaining!                        |              ");
				Console.Write("              |                                                  |              ");
				Console.Write("              ====================================================              ");
				for (int remaining = 300; remaining > 0; remaining--)
                {
					//Convert seconds remaining to M:SS format
					int sec = remaining % 60;
					int mins = (remaining - sec) / 60;
					string secs = sec.ToString();
					if (secs.Length != 2) //If seconds is not double digit, add a 0 to front to keep M:SS format.
                    {
						secs = "0" + secs;
                    }
					//Write remaining time
					Console.SetCursorPosition(16, 14);
					Console.Write("{0}:{1}", mins, secs);
					Cosmos.HAL.Global.PIT.Wait(1000); //Wait 1 second (1000ms)
                }
				//After done, delete lock file and recall method.
				File.Delete(@"0:\systemLocker.lck");
				PasswordScreen(folderName, username, true);
			}
			string userfolder = @"0:\Users\" + folderName + @"\";
			if (File.Exists(userfolder + "password.pwd"))
            {
				Services.EnvVars.hasPassword = true;
				Console.SetCursorPosition(16, 11);
				Console.Write(username);
				if (wrong)
				{
					if (tries == 0)
					{
						File.Create(@"0:\systemLocker.lck");
						PasswordScreen(folderName, username, true);
					}
					Console.SetCursorPosition(1, 14);
					Console.Write("[X] Password is invalid! {0} tries left!", tries);
					if (File.Exists(userfolder + "passRem.dat"))
					{
						StreamReader rem = new StreamReader(userfolder + "passRem.dat");
						string reminder = rem.ReadToEnd();
						rem.Close();
						Console.SetCursorPosition(1, 15);
						Console.Write("Password reminder: " + reminder);
					}
				}
				Console.SetCursorPosition(13, 13);
				StreamReader p = new StreamReader(userfolder + "password.pwd");
				string password = p.ReadToEnd();
				p.Close();
				string pass = Drivers.Keyboard.KeyHandler(true, true);
				if (pass == password)
				{
					Login(username, folderName);
				}
				else
				{
					PasswordScreen(folderName, username, true, tries - 1);
				}
			}
			else
            {
				Services.EnvVars.hasPassword = false;
				Login(username, folderName);
            }
		}
		public static void Login(string username, string folderName)
        {
			Services.EnvVars.username = username;
			Services.EnvVars.userFolder = folderName;
			Beep.Sound.Startup();
			if (!File.Exists(@"0:\SDOS\preferences\postins.idp"))
            {
				TextUI.PostInstall.Start();
            }
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;
			Console.Clear();
        }
		public static string[] GetUserAccounts()
        {
			string[] users = Directory.GetDirectories(@"0:\Users\");
			int num = 0;
			foreach (var user in users) { num++; }
			string[] accounts = new string[num];
			num = 0;
			foreach (var account in users)
            {
				accounts[num] = account;
				num++;
            }
			return accounts;
        }
	}
}