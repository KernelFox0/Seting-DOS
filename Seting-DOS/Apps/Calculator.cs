/// 
/// Calculator, Last modified: 2022. 10. 19.
/// Made for Seting-DOS, feel free to use any code from this
/// 

using System;

namespace Seting_DOS.Apps
{
	public static class Calculator
	{
		static bool running = true;
		public static void StartApp()
		{
			try
			{
				running = true;
				Console.WriteLine("Seting-DOS Calculator version a0.1");
				Console.ForegroundColor = ConsoleColor.Blue;
				Console.WriteLine("Supported operations: Add (+), Subtarct (-), Multiply (*), Divide (/)");
				Console.WriteLine("Press ENTER to exit!");
				while (running)
				{
					bool printResult = true;
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.Write("Enter operation: ");
					Console.ForegroundColor = ConsoleColor.White;
					StatusBar.TerminalDisp();
					string math = HandleKeys().Replace(" ", "").Replace(",", ".");
					char op = ' ';
					if (!running) { return; }
					if (math.Contains("+")) { op = '+'; }
					else if (math.Contains("-")) { op = '-'; }
					else if (math.Contains("*")) { op = '*'; }
					else if (math.Contains("/")) { op = '/'; }
					double num1 = Convert.ToDouble(math.Remove(math.IndexOf(op.ToString())));
					double num2 = Convert.ToDouble(math.Remove(0, math.IndexOf(op.ToString()) + 1)); ;
					double result = 0;
					if (op == '+') { result = num1 + num2; }
					else if (op == '-') { result = num1 - num2; }
					else if (op == '*') { result = num1 * num2; }
					else if (op == '/')
					{
						if (num2 == 0)
						{
							Console.ForegroundColor = ConsoleColor.Red;
							Drivers.Beep.Sound.Error();
							Console.WriteLine("Cannot divide by zero!");
							printResult = false;
						}
						else { result = num1 / num2; }
					}
					if (printResult)
					{
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine("Result: {0} = {1}", math, result);
					}
				}
			}
			catch (Exception ex)
            {
				Console.ForegroundColor = ConsoleColor.Red;
				Drivers.Beep.Sound.Error();
				Console.WriteLine("Error: " + ex.Message);
				Console.ForegroundColor = ConsoleColor.White;
			}
		}
		public static string HandleKeys()
		{
			string cmd = "";
			bool opAdded = false;
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
					Console.Write("\n");
					if (x == originX) { running = false; }
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
						cmd = cmd.Remove(cmd.Length - 1);
					}
					if (y > originY && x == 0)
					{
						Console.SetCursorPosition(79, y - 1);
						Console.Write(" ");
						Console.SetCursorPosition(79, y - 1);
						x = 79;
						y--;
						cmd = cmd.Remove(cmd.Length - 1);
					}
					if (y > originY && x > 0)
					{
						Console.SetCursorPosition(x - 1, y);
						Console.Write(" ");
						Console.SetCursorPosition(x - 1, y);
						x--;
						cmd = cmd.Remove(cmd.Length - 1);
					}
					if (!cmd.Contains("+") && !cmd.Contains("-") && !cmd.Contains("*") && !cmd.Contains("/"))
					{
						opAdded = false;
					}
				}
				else if (p.Key == ConsoleKey.F1)
				{
					Drivers.Beep.Mute();
				}
				else if (p.KeyChar == '0' || p.KeyChar == '1' || p.KeyChar == '2' || p.KeyChar == '3' || p.KeyChar == '4' || p.KeyChar == '5' || p.KeyChar == '6' || p.KeyChar == '7' || p.KeyChar == '8' || p.KeyChar == '9')
				{
					cmd += p.KeyChar;
					Console.Write(p.KeyChar);
					x++;
				}
				else if ((p.KeyChar == '+' || p.KeyChar == '-' || p.KeyChar == '*' || p.KeyChar == '/') && !opAdded)
				{
					cmd += p.KeyChar;
					x++;
					Console.Write(p.KeyChar);
					opAdded = true;
				}
				if (x == 80)
				{
					x = 0;
					y++;
				}
				StatusBar.TerminalDisp();
				Console.SetCursorPosition(x, y);
			}
			return cmd;
		}
	}
}