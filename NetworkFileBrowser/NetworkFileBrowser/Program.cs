using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace NetworkFileBrowser
{
    class Program
    {
        // Test harness.
        // If you incorporate this code into a DLL, be sure to demand FullTrust.
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        static void Main()
        {
            const string pathOnRemoteServer = @"\\172.16.0.188\builds";
//            const string pathOnRemoteServer = @"\\172.16.4.94\builds";

            using (DotNetImpersonate dotNetImpersonate = new DotNetImpersonate(pathOnRemoteServer, string.Empty, string.Empty))
            {
                string[] subdirectoryEntries = Directory.GetDirectories(pathOnRemoteServer);
                
                foreach (string directory in subdirectoryEntries)
                {
                    Console.WriteLine(directory);
                }
            }
            
            // int result = PinvokeWindowsNetworking.ConnectToRemote(pathOnRemoteServer, "", "", true);
            // if (result == 0)
            // {
            //     Console.WriteLine("Successfully connected to '" + pathOnRemoteServer + "'.");
            //
            //     string[] subdirectoryEntries = Directory.GetDirectories(pathOnRemoteServer);
            //
            //     foreach (string directory in subdirectoryEntries)
            //     {
            //         Console.WriteLine(directory);
            //     }
            // }
            // else
            // {
            //     string errorDescription = Win32ErrorHelper.Win32ErrorDescription(result);
            //     Console.WriteLine("[ERROR] "  + errorDescription);
            // }
            //
            // result = PinvokeWindowsNetworking.disconnectRemote(pathOnRemoteServer);
            // if (result != 0)
            // {
            //     string errorDescription = Win32ErrorHelper.Win32ErrorDescription(result);
            //     Console.WriteLine("[ERROR] " + errorDescription);
            // }

            Console.WriteLine("Press any key to exit....");
            Console.ReadKey();
        }
    }
}
