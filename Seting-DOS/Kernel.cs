/// 
/// COSMOS kernel, boot process, terminal and crash handler, Last modified: 2023. 08. 01.
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
using Sys = Cosmos.System;
using Seting_DOS.Services;
using Seting_DOS.Apps;

namespace Seting_DOS
{
    public class Kernel : Sys.Kernel
	{

		protected override void BeforeRun() //Code the kernel executes at boot
		{
			try
			{
				Drivers.Power.Booter.Preboot();
            }
            catch (Exception ex)
            {
				CrashUI.KernelCrash(ex);
				_ = !false;
				//It's funny because it's true
            }
		}
		protected override void Run() //Main loop
		{
			try
			{
				StatusBar.TerminalDisp();
				CmdProc.Process(Terminal.WriteShell());
				Cosmos.Core.Memory.Heap.Collect();
			}
			catch (Exception ex)
            {
				CrashUI.KernelCrash(ex);
            }
		}
	}
}