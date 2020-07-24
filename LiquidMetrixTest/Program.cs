using LiquidMetrixTest.Helpers;
using LiquidMetrixTest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LiquidMetrixTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter command string");
            var commandString = Console.ReadLine();
            
            var commands = Parse(commandString);
            if (!commands.Any())
            {
                Console.WriteLine("No valid commands found");
                return;
            }
            
            IRover rover = new Rover(new Logger());
            rover.SetPosition(10, 10, Direction.N);
            
            foreach(var command in commands)
            {
                rover.Move(command);
            }
            
            Console.WriteLine("Completed!");
        }

        static IEnumerable<string> Parse(string commandString)
        {
            if (commandString.Length % 2 != 0)
                Console.WriteLine("Invalid length of command");

            string pattern = @"[rlRL][0-9]{1,2}";
            var regex = new Regex(pattern);

            var cleanedCommandString = commandString.Trim().Replace(" ", string.Empty).ToUpper();
            return regex.Matches(cleanedCommandString).Select(match => match.Value);
        }
    }
}
