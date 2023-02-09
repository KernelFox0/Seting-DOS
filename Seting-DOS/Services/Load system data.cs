/// 
/// Environment variable loader, Last modified: 2022. 09. 24.
/// Made for Seting-DOS, feel free to use any code from this
/// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Seting_DOS.Services
{
    public static class SystemData
    {
        public static string[] Load()
        {
            try
            {
                StreamReader host = new StreamReader(@"0:\SDOS\preferences\compName.pref");
                Services.EnvVars.hostname = host.ReadToEnd();
                host.Close();
                StreamReader theme = new StreamReader(@"0:\SDOS\preferences\theme.dat");
                Services.EnvVars.theme = theme.ReadToEnd();
                theme.Close();
            }
            catch (Exception e)
            {
                string[] error = { "error", e.Message };
                return error;
            }
            string[] result = { "done", "System data loaded into environment variables" };
            return result;
        }
    }
}
