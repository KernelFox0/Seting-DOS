/// 
/// PCSpeaker Beep driver, Last modified: 2023. 08. 11.
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
using System.Linq;
using Cosmos.HAL;
using Seting_DOS.Services;

namespace Seting_DOS.Drivers
{
    public static class Beep
	{
		public static string[] Load()
		{
			try
			{
				StreamReader mute = new StreamReader(@"0:\SDOS\system\mute.dat");
				string content = mute.ReadLine();
				if (content == "1") { Services.EnvVars.mute = true; } //Read mute file and set variable
				string[] result = { "done", "Beep driver initialized!" };
				return result; //Return message for BootMSG
			}
			catch (Exception ex)
            {
				string[] result = { "error", ex.Message };
				return result; //Return error for BootMSG
            }
		} //Load beep driver data
		public static void Mute() //Mute switch
		{
			StreamWriter mute = new StreamWriter(@"0:\SDOS\system\mute.dat"); //Open mute file
			switch (Services.EnvVars.mute)
			{
				case true:
					Services.EnvVars.mute = false;
					mute.Write("0");
					break; //If mute is on turn it off and wrrite value to file
				case false:
					Services.EnvVars.mute = true;
					mute.Write("1");
					break; //If mute is off turn it on and wrrite value to file
			}
			mute.Close(); //Close mute file
		}
		public static class Sound
        {
			public static void Error()
            {
				BeepMusicPlayer.MusicPlayer(@"0:\SDOS\System\errorSound.beep", false);
            }
			public static void Warning()
			{
				BeepMusicPlayer.MusicPlayer(@"0:\SDOS\System\warnSound.beep", false);
			}
			public static void Question()
			{
				BeepMusicPlayer.MusicPlayer(@"0:\SDOS\System\questionSound.beep", false);
			}
			public static void Startup()
			{
				BeepMusicPlayer.MusicPlayer(@"0:\SDOS\System\startSound.beep", false);
			}
			public static void Shutdown()
			{
				BeepMusicPlayer.MusicPlayer(@"0:\SDOS\System\shutdownSound.beep", false);
			}
		}
		public static uint[] ReadFile(string path) //Read BeepMusic files to buffer
        {
			/*
			 * Read beep music files (extension: .beep)
			 */
			if (!path.EndsWith(".beep")) { return null; } //If the file is not a BeepMusic file it can't read it
			StreamReader beepFile = new StreamReader(path); //Open file
			int lines = TextOperations.GetLines(path); //Get lines of file
			string line;
			bool hasDuration = TextOperations.DoesItContain(",", path);
			uint note;
			uint duration;
			int multiplier = 1; //Buffer size multiplier
			if (TextOperations.DoesItContain(",", path)) { multiplier = 2; } //If file has duration data the buffer multiplier is set to 2
			uint[] buffer = new uint[lines * multiplier]; //Create the buffer
			for (int i = 0; i < lines; i++)
            {
				line = beepFile.ReadLine(); //Read next line of BeepMusic file
				if (hasDuration) //If BM file has duration data
                {
					if (line.Contains(","))
					{
						note = Convert.ToUInt32(line.Remove(line.IndexOf(","))); //Get beep frequency from file
						duration = Convert.ToUInt32(line.Remove(0, line.IndexOf(",") + 1)); //Get beep duration from file
						buffer[i] = note; //Add frequency to buffer
						buffer[i + 1] = duration; //Add duration to buffer
					}
					else
                    {
						note = Convert.ToUInt32(line); //Get beep frequency from file
						buffer[i] = note; //Add frequency to buffer
						buffer[i + 1] = 0; //Set default duration to buffer
					}
				}
				else
                {
					note = Convert.ToUInt32(line); //Get beep frequency from file
					buffer[i] = note; //Add frequency to buffer
				}
            }
			beepFile.Close(); //Close BeepMusic file
			return buffer; //Return the buffer
        }
		public static void PCBeep(uint note, uint duration = 0) //Beep function
        {
			if (note < 37 || note > 32767) { return; } //If frequency is out of the supported range do nothing
			if (duration == 0) { PCSpeaker.Beep(note, 1); }
			else { PCSpeaker.Beep(note, duration); } //Play the sound
        }
	}
}