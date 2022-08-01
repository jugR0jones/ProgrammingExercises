using System;
using System.Collections.Generic;
using System.Reflection;

using CommandLineArguments.DTOs;

namespace CommandLineArguments
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Jenkins FastDeploy UI ver " + Assembly.GetExecutingAssembly().GetName().Version.ToString());
            Console.WriteLine("");

            CommandLineArgument[] arguments = {
                new CommandLineArgument {
                    shortName="p",
                    longName="projectPath",
                    description="Set the location of the project path on start up",
                    numberOfArguments=1
                },
                new CommandLineArgument
                {
                    shortName="l",
                    longName="lorem",
                    description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    numberOfArguments = 0
                }
            };

            string errorMessage;
            CommandLineParser commandLineParser = new CommandLineParser(arguments);
            Dictionary<string, string> processedCommandLineArguments = commandLineParser.TryParseArguments(args, out errorMessage);
            if(processedCommandLineArguments == null)
            {
                Console.WriteLine(errorMessage);
            }

            //string projectPath;
            //if (processedCommandLineArguments.TryGetValue("projectPath", out projectPath))
            //{
            //    settings.PathOnBuildServer = projectPath;
            //    settings.SavePathOnBuildServerToRegistry = false;
            //}
        }
    }
}
