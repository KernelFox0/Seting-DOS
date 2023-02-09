/// 
/// Display driver, Last modified: 2022. 09. 24.
/// Made for Seting-DOS, feel free to use any code from this
/// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cosmos.System.ExtendedASCII;
using Sys = Cosmos.System;

namespace Seting_DOS.Drivers
{
    public static class DisplayDriver
    {
        public static string[] Load()
        {
            try
            {
                Encoding.RegisterProvider(CosmosEncodingProvider.Instance);
                Console.OutputEncoding = Encoding.GetEncoding(437);
            }
            catch (Exception e)
            {
                string[] error = { "error", e.Message };
                return error;
            }
            string[] result = { "done", "Display driver loaded" };
            return result;
        }
    }
}
