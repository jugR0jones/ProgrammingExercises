using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace NetworkFileBrowser
{
    internal class DotNetImpersonate : IDisposable
    {
        #region Constants

        // See https://docs.microsoft.com/en-us/windows/win32/api/winnt/ne-winnt-security_impersonation_level
        private enum SecurityImpersonationLevel
        {
            SecurityAnonymous,
            SecurityIdentification,
            SecurityImpersonation,
            SecurityDelegation
        };
        
        private const int LogonNewCredentials = 9;
        
        private const int LogonDefaultProvider = 0;
        private const int LogonDefaultInteractive = 2;
        
        #endregion
        
        #region Private Variables
        
        private WindowsImpersonationContext windowsImpersonationContext = null;

        #endregion

        public DotNetImpersonate(string remoteServerPath, string username, string password)
        {
         //   try
          //  {
                // Revert to the user currently logged onto the machine.
                bool revertToSelf = RevertToSelf();
                if (!revertToSelf)
                {
                    int win32ErrorCode = Marshal.GetLastWin32Error();
                    throw new Win32Exception(win32ErrorCode);
                }

                string domain = string.Empty;
                bool result = ImpersonateUser(domain, username, password);
                if (!result)
                {
                    int win32ErrorCode = Marshal.GetLastWin32Error();
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
       //     }
            // catch (Win32Exception ex)
            // {
            //     Console.WriteLine("[ERROR]: " + ex.NativeErrorCode);
            //     Console.WriteLine(ex.Message);
            // }
            // catch (Exception ex)
            // {
            //     //TODO: Add a proper log function to handle exceptions and inner exceptions
            //     Console.WriteLine(ex.Message);
            // }
        }
        
        #region Private Methods
        
        private bool ImpersonateUser(string domainName, string username, string password)
        {
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;

            try
            {
                // bool returnValue = LogonUser(userName, domainName, Console.ReadLine(),
                //     LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT,
                //     out safeTokenHandle);
                
                bool returnValue = LogonUser(username, domainName, password,
                    LogonDefaultInteractive, LogonDefaultProvider,
                    out token);
                
                // if (LogonUser(username, domainName, password,  LogonNewCredentials, LogonDefaultProvider, out token))
                // {
                //     //TODO: Check the return values
                //     if (DuplicateToken(token, (int)SecurityImpersonationLevel.SecurityImpersonation, ref tokenDuplicate) != 0)
                //     {
                //         WindowsIdentity windowsIdentity = new WindowsIdentity(tokenDuplicate);
                //         windowsImpersonationContext = windowsIdentity.Impersonate();
                //
                //         return true;
                //     }
                // }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]: " + ex.Message);

                return false;
            }
            finally
            {
                if (token != IntPtr.Zero)
                {
                    CloseHandle(token);
                }

                if (tokenDuplicate != IntPtr.Zero)
                {
                    CloseHandle(tokenDuplicate);
                }
            }

            return false;
        }
        
        #endregion
        
        #region IDisposable

        public void Dispose()
        {
            if (windowsImpersonationContext != null)
            {
                windowsImpersonationContext.Undo();
                windowsImpersonationContext.Dispose();
            }
        }

        #endregion
        
        #region Win32 Function Imports
        
        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool LogonUser(String lpszUsername, String lpszDomain, String lpszPassword,
            int dwLogonType, int dwLogonProvider, out IntPtr phToken);
        
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int DuplicateToken(IntPtr hToken, int impersonationLevel, ref IntPtr hNewToken);
        
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool RevertToSelf();
        
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern bool CloseHandle(IntPtr handle);
        
        #endregion
    }
}
