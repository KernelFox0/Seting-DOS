/// 
/// System crash UI, Last modified: 2022. 09. 24.
/// Made for Seting-DOS, feel free to use any code from this
/// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seting_DOS.TextUI
{
	public static class CrashUI
	{
		public static void UserspaceCrash(Exception crash)
		{
			#region UI
			Console.BackgroundColor = ConsoleColor.Blue;
			Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Console.Write("                                                                                ");
            Console.Write("                                                                                ");
            Console.Write("                 *@@,                                      g@P                  ");
            Console.Write("                   ]RW                                    **`                   ");
            Console.Write("               ,ggg@@@gg,                              ,gp@@@gg,                ");
            Console.Write("             g@@@NP***N@@@g                         ,@@@@MP*PMB@@@              ");
            Console.Write("            @@@P        ]@@@  ,,,      ,,     ,,,  ]@@@\"       `$@@w            ");
            Console.Write("           ]@@K          ]@@K ]@@K    @@@@    @@@  @@@          '@@@            ");
            Console.Write("           $@@P          ]@@@  $@@   ]@P@@w  ]@@  j@@@           $@@            ");
            Console.Write("           ]@@K          ]@@P   @@@  @@ ]@@  @@P   @@@           @@@            ");
            Console.Write("            $@@g        g@@@    ]@@ $@`  $@W]@@    ]@@@        ,@@@C            ");
            Console.Write("             *@@@@gggg@@@@P      $@@@@   ]@@@@-     \"B@@@ggggg@@@N              ");
            Console.Write("               \"*MNNNNM\"          MMM     *MMP        `\"MNNNNM*\"                ");
            Console.Write("                                                                                ");
            Console.Write("                                                                                ");
            Console.Write(" Your system crashed. That's not good! Crash type: Userspace crash              ");
            Console.Write(" Reason:                                                                        ");
            Console.Write("                                                                                ");
            Console.Write(" You can either return to the system or restart the kernel.                     ");
            Console.Write("                                                                                ");
            Console.Write(" KERNEL VERSION: OwO UA WIP                                                     ");
            Console.Write(" SYSTEM VERSION: UA0.0                                                          ");
            Console.Write(" .NETPARSER VERSION: NaN                                                        ");
            Console.Write(" .NETAPP VERSIONS: NaN                                                          ");
            Console.Write(" R = Return to system     Any other = Restart kernel                          ");
            #endregion
            #region Write reason
            Cosmos.System.PCSpeaker.Beep();
            Console.SetCursorPosition(9, 16);
            Console.Write(crash.Message);
            #endregion
            #region Reboot
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.R) { }
            else { Cosmos.Core.CPU.Reboot(); }
            #endregion
        }
        public static void KernelspaceCrash(Exception crash)
        {
            #region UI
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Console.Write("                                                                                ");
            Console.Write("                                                                                ");
            Console.Write("                 *@@,                                      g@P                  ");
            Console.Write("                   ]RW                                    **`                   ");
            Console.Write("               ,ggg@@@gg,                              ,gp@@@gg,                ");
            Console.Write("             g@@@NP***N@@@g                         ,@@@@MP*PMB@@@              ");
            Console.Write("            @@@P        ]@@@  ,,,      ,,     ,,,  ]@@@\"       `$@@w            ");
            Console.Write("           ]@@K          ]@@K ]@@K    @@@@    @@@  @@@          '@@@            ");
            Console.Write("           $@@P          ]@@@  $@@   ]@P@@w  ]@@  j@@@           $@@            ");
            Console.Write("           ]@@K          ]@@P   @@@  @@ ]@@  @@P   @@@           @@@            ");
            Console.Write("            $@@g        g@@@    ]@@ $@`  $@W]@@    ]@@@        ,@@@C            ");
            Console.Write("             *@@@@gggg@@@@P      $@@@@   ]@@@@-     \"B@@@ggggg@@@N              ");
            Console.Write("               \"*MNNNNM\"          MMM     *MMP        `\"MNNNNM*\"                ");
            Console.Write("                                                                                ");
            Console.Write("                                                                                ");
            Console.Write(" Your system crashed. That's not good! Crash type: Kernelspace crash            ");
            Console.Write(" Reason:                                                                        ");
            Console.Write("                                                                                ");
            Console.Write(" Your system will reboot if you press a key.                                    ");
            Console.Write("                                                                                ");
            Console.Write(" KERNEL VERSION: OwO UA WIP                                                     ");
            Console.Write(" SYSTEM VERSION: UA0.0                                                          ");
            Console.Write(" .NETPARSER VERSION: NaN                                                        ");
            Console.Write(" .NETAPP VERSIONS: NaN                                                          ");
            Console.Write("                                                                               ");
            #endregion
            #region Write reason
            Cosmos.System.PCSpeaker.Beep();
            Console.SetCursorPosition(9, 16);
            Console.Write(crash.Message);
            #endregion
            #region Reboot
            Console.ReadKey();
            Cosmos.Core.CPU.Reboot();
            #endregion
        }
    }
}
