/// 
/// Real Time Clock driver, Last modified: 2023. 07. 30.
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seting_DOS.Drivers
{
    public static class RTC //RTC: Real Time Clock
    {
        public static string[] Check() //Check if RTC works
        {
            try
            {
                _ = Cosmos.HAL.RTC.Second;
                _ = Cosmos.HAL.RTC.Century;
                _ = Cosmos.HAL.RTC.Year; //Check if system can get values from COSMOS RTC driver
                string[] status = { "done", "RTC service check was successful" };
                return status; //Return message for BootMSG
            }
            catch (Exception e) //If anything goes wrond while getting RTC values
            {
                string[] error = {"error", "RTC service unavailable! Reason: " + e.Message };
                return error; //Return error for BootMSG
            }
        }
        public static string[] GetTime(bool seconds = false) //Get the current time
        {
            string hour = Cosmos.HAL.RTC.Hour.ToString();
            if (hour.Length == 1)
            {
                hour = "0" + hour;
            } //Get hour and and make sure it's in double digit format
            string minute = Cosmos.HAL.RTC.Minute.ToString();
            if (minute.Length == 1)
            {
                minute = "0" + minute;
            } //Get minute and and make sure it's in double digit format
            if (seconds)
            {
                string second = Cosmos.HAL.RTC.Hour.ToString();
                if (second.Length == 1)
                {
                    second = "0" + second;
                } //Get second and and make sure it's in double digit format
                string[] timeS = { hour, minute, second };
                return timeS; //Return time with seconds
            }
            string[] time = { hour, minute };
            return time; //Return time
        }
        public static string[] GetDate()
        {
            string century = Cosmos.HAL.RTC.Century.ToString();
            if (century.Length == 1)
            {
                century = "0" + century;
            } //Get century and and make sure it's in double digit format
            string cYear = Cosmos.HAL.RTC.Year.ToString();
            if (cYear.Length == 1)
            {
                cYear = "0" + cYear;
            } //Get century year and and make sure it's in double digit format
            string year = century + cYear; //Combine two variables to get the current year
            string month = Cosmos.HAL.RTC.Month.ToString();
            if (month.Length == 1)
            {
                month = "0" + month;
            } //Get month and and make sure it's in double digit format
            string day = Cosmos.HAL.RTC.DayOfTheMonth.ToString();
            if (day.Length == 1)
            {
                day = "0" + day;
            } //Get day and and make sure it's in double digit format
            string[] date = { year, month, day };
            return date; //Return date
        }
    }
}
