/// 
/// COSMOS kernel, boot process, terminal and crash handler, Last modified: 2022. 10. 08.
/// Made for Seting-DOS, feel free to use any code from this
/// 

using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.System.ExtendedASCII;
using System.IO;
using Cosmos.System.FileSystem;
using Cosmos.HAL;
using Seting_DOS;
using Seting_DOS.Drivers;
using Seting_DOS.Services;

namespace Seting_DOS
{
	public class Kernel : Sys.Kernel
	{

		protected override void BeforeRun() //Boot process
		{
			try
			{
				Drivers.Power.Booter.Preboot();
                #region Inactive Old Booter
                /*string[] kern = { "done", "Seting-DOS base kernel loaded" };
				Services.BootMSG.Write(kern);
				#region Pre-boot and installation check
				//Load important drivers
				Services.BootMSG.Write(Drivers.DisplayDriver.Load());
				Services.BootMSG.Write(Drivers.Keyboard.Load());
				//Load filesystem driver to check installation
				Services.BootMSG.Write(Drivers.VSFS.Load());
				if (File.Exists(@"0:\SDOS\System\installed.idp")) { }
				else //No setup, call installer
				{
					string[] waitMSG = { "warning", "System isn't set up!" };
					BootMSG.Write(waitMSG);
					Cosmos.HAL.Global.PIT.Wait(5000);
					TextUI.Setup.Call();
				}
				#endregion
				BootMSG.Write(Drivers.RTC.Check());
				BootMSG.Write(Network.Load());
				BootMSG.Write(Beep.Load());
				BootMSG.Write(SystemData.Load());
				//BootMSG.Write(DotNetParser.Load());
				//Logon window
				TextUI.LogonUI.LockScreen();
				TextUI.Terminal.Init();*/
                #endregion
            }
            catch (Exception ex)
            {
				TextUI.CrashUI.KernelspaceCrash(ex);
            }
		}

		protected override void Run() //Main prompt
		{
			try
			{
				Apps.StatusBar.TerminalDisp();
				CommandHandler.Handle(TextUI.Terminal.WriteShell());
				Cosmos.Core.Memory.Heap.Collect();
			}
			catch (Exception ex)
            {
				TextUI.CrashUI.KernelspaceCrash(ex);
            }
		}
	}
}