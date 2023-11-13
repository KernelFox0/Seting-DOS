/// 
/// Seting-DOS File type name reference file, Last modified: 2023. 11. 13.
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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Seting_DOS.Services
{
	public static class AliasManager
	{
		public static Dictionary<string, string> aliases = new Dictionary<string, string>();
		public static string system = @"0:\SDOS\System\alias.dat";
		public static void Create(string alias, string command, bool sys = false)
		{
			Prepare(sys, !sys);
			if (!aliases.ContainsKey(alias))
			{
				aliases.Add(alias, command);
				StreamWriter write;
				StreamReader read;
				if (sys) { read = new(system); }
				else { read = new(@"0:\Users\" + EnvVars.username + @"\AppData\alias.dat"); }
				read.BaseStream.Position = 0;
				read.DiscardBufferedData();
                string data = read.ReadToEnd();
				read.Close();
				if (sys) { try { File.Delete(system); } catch { } write = new(system);}
				else { try { File.Delete(@"0:\Users\" + EnvVars.username + @"\AppData\alias.dat"); } catch { } write = new(@"0:\Users\" + EnvVars.username + @"\AppData\alias.dat");}
				data = data + "\n" + alias + "," + command;
				write.Write(data);
				write.Close();
				Load();
			}
			else { Messages.Error("Alias name already exists!"); }
		}
		public static string[] Load(bool onlySys = false, bool onlyUsr = false)
		{
			aliases.Clear();
			try
			{
				//File format: alias,command
				Prepare(onlySys, onlyUsr);
				if (!onlyUsr)
				{
					StreamReader sysFile = new(system);
					string content = sysFile.ReadToEnd();
					sysFile.Close();
					string[] lines = content.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
					foreach (string line in lines)
					{
						string[] current = line.Split(',');
						try { aliases.Add(current[0], current[1]); } catch { }
					}
				}
				if (!onlySys)
				{
					StreamReader userFile = new("0:\\Users\\" + EnvVars.username + "\\AppData\\alias.dat");
					string content = userFile.ReadToEnd();
					userFile.Close();
					string[] lines = content.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
					foreach (string line in lines)
					{
						string[] current = line.Split(',');
						try { aliases.Add(current[0], current[1]); } catch { }
					}
				}
			}
			catch (Exception e)
			{
				string[] error = { "error", e.Message };
				return error;
			}
			string[] result = { "done", "System alias values loaded to memory" };
			return result;
		}
		public static string GetCmd(string alias)
		{
			try
			{
				return aliases[alias];
			}
			catch { return alias; }
		}
		public static string[] List(bool onlySys = false, bool onlyUsr = false)
		{
			Prepare(onlySys, onlyUsr);
			string[] list = new string[2];
			if (!onlyUsr)
			{
				StreamReader sysFile = new(system);
				list[0] = sysFile.ReadToEnd();
				sysFile.Close();
			}
			if (!onlySys)
			{
				StreamReader userFile = new("0:\\Users\\" + EnvVars.username + "\\AppData\\alias.dat");
				list[1] = userFile.ReadToEnd();
				userFile.Close();
			}
			return list;
		}
		public static void Delete(string alias, bool sys = false)
		{
			if (aliases.ContainsKey(alias))
			{
				aliases.Remove(alias);
				StreamWriter write;
				StreamReader read;
                if (sys) { read = new(system); }
                else { read = new(@"0:\Users\" + EnvVars.username + @"\AppData\alias.dat"); }
                read.BaseStream.Position = 0;
                read.DiscardBufferedData();
                string content = read.ReadToEnd();
				read.Close();
                if (sys) { try { File.Delete(system); } catch { } write = new(system); }
                else { try { File.Delete(@"0:\Users\" + EnvVars.username + @"\AppData\alias.dat"); } catch { } write = new(@"0:\Users\" + EnvVars.username + @"\AppData\alias.dat"); }
                string data = "";
				string[] lines = content.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
				foreach (string line in lines)
				{
					if (!line.StartsWith(alias)) { data = data + line + "\n"; }
				}
				write.Write(data);
				write.Close();
				Load();
			}
			else { Messages.Error("Alias name doesn't exists!"); }
		}
		public static void Prepare(bool onlySys, bool onlyUsr)
		{
			if (!File.Exists(system) && !onlyUsr) { StreamWriter temp = new(system); temp.Close(); }
			string user = @"0:\Users\" + EnvVars.username + @"\AppData\alias.dat";
			if (!File.Exists(user) && !onlySys) { StreamWriter temp = new(user); temp.Close(); }
		}
	}
}
