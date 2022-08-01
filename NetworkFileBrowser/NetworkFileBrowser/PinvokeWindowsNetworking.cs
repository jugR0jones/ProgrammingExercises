using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

//NetworkCredential theNetworkCredential = new NetworkCredential(username, password, domain);

//CredentialCache theNetCache = new CredentialCache();

//theNetCache.Add(@"\\files.mydomain.com", theNetworkCredential, "Basic", theNetworkCredential);

namespace NetworkFileBrowser
{
    internal class PinvokeWindowsNetworking
    {
        #region Consts
        const int RESOURCE_CONNECTED = 0x00000001;
        const int RESOURCE_GLOBALNET = 0x00000002;
        const int RESOURCE_REMEMBERED = 0x00000003;

        const int RESOURCETYPE_ANY = 0x00000000;
        const int RESOURCETYPE_DISK = 0x00000001;
        const int RESOURCETYPE_PRINT = 0x00000002;

        const int RESOURCEDISPLAYTYPE_GENERIC = 0x00000000;
        const int RESOURCEDISPLAYTYPE_DOMAIN = 0x00000001;
        const int RESOURCEDISPLAYTYPE_SERVER = 0x00000002;
        const int RESOURCEDISPLAYTYPE_SHARE = 0x00000003;
        const int RESOURCEDISPLAYTYPE_FILE = 0x00000004;
        const int RESOURCEDISPLAYTYPE_GROUP = 0x00000005;

        const int RESOURCEUSAGE_CONNECTABLE = 0x00000001;
        const int RESOURCEUSAGE_CONTAINER = 0x00000002;


        const int CONNECT_INTERACTIVE = 0x00000008;
        const int CONNECT_PROMPT = 0x00000010;
        const int CONNECT_REDIRECT = 0x00000080;
        const int CONNECT_UPDATE_PROFILE = 0x00000001;
        const int CONNECT_COMMANDLINE = 0x00000800;
        const int CONNECT_CMD_SAVECRED = 0x00001000;

        const int CONNECT_LOCALDRIVE = 0x00000100;
        #endregion

        [DllImport("Mpr.dll")]
        private static extern int WNetUseConnection(
            IntPtr hwndOwner,
            NETRESOURCE lpNetResource,
            string lpPassword,
            string lpUserID,
            int dwFlags,
            string lpAccessName,
            string lpBufferSize,
            string lpResult
        );

        [DllImport("Mpr.dll")]
        private static extern int WNetCancelConnection2(
            string lpName,
            int dwFlags,
            bool fForce
        );

        [StructLayout(LayoutKind.Sequential)]
        private class NETRESOURCE
        {
            public int dwScope = 0;
            public int dwType = 0;
            public int dwDisplayType = 0;
            public int dwUsage = 0;
            public string lpLocalName = "";
            public string lpRemoteName = "";
            public string lpComment = "";
            public string lpProvider = "";
        }


        public static int ConnectToRemote(string remoteUNC, string username, string password)
        {
            return ConnectToRemote(remoteUNC, username, password, false);
        }

        public static int ConnectToRemote(string remoteUNC, string username, string password, bool promptUser)
        {
            NETRESOURCE netResource = new NETRESOURCE
            {
                dwType = RESOURCETYPE_DISK,
                lpRemoteName = remoteUNC
            };

            int ret;
            if (promptUser)
            {
                ret = WNetUseConnection(IntPtr.Zero, netResource, string.Empty, string.Empty, CONNECT_INTERACTIVE | CONNECT_PROMPT, null, null, null);
            }
            else
            {
                ret = WNetUseConnection(IntPtr.Zero, netResource, password, username, 0, null, null, null);
            }

            return ret;
        }

        public static int disconnectRemote(string remoteUNC)
        {
            return WNetCancelConnection2(remoteUNC, CONNECT_UPDATE_PROFILE, false);
        }
    }

    public class Win32ErrorHelper
    {
        private const int ERROR_SUCCESS = 0;

        private const int ERROR_ACCESS_DENIED = 5;


        private const int ERROR_DUP_NAME = 0x034;
        private const int ERROR_BAD_NETPATH = 0x035;

        private const int ERROR_BAD_NET_NAME = 0x043;
        private const int ERROR_ALREADY_ASSIGNED = 85;
        private const int ERROR_INVALID_PARAMETER = 87;

        private const int ERROR_MORE_DATA = 234;
        private const int ERROR_NO_MORE_ITEMS = 259;
        private const int ERROR_INVALID_ADDRESS = 487;

        private const int ERROR_BAD_DEVICE = 1200;
        private const int ERROR_NO_NET_OR_BAD_PATH = 1203;
        private const int ERROR_BAD_PROVIDER = 1204;
        private const int ERROR_CANNOT_OPEN_PROFILE = 1205;
        private const int ERROR_BAD_PROFILE = 1206;
        private const int ERROR_EXTENDED_ERROR = 1208;
        private const int ERROR_INVALID_PASSWORD = 1216;
        private const int ERROR_SESSION_CREDENTIAL_CONFLICT = 0x4C3;
        private const int ERROR_NO_NETWORK = 1222;
        private const int ERROR_CANCELLED = 1223;


        private const int NERR_UseNotFound = 2250;
        private const int ERROR_OPEN_FILES = 2401;
        private const int ERROR_DEVICE_IN_USE = 2404;

        private static readonly Dictionary<int, string> errorDataStore = new Dictionary<int, string>() {
            { ERROR_SUCCESS, "The operation completed successfully." },
            { ERROR_ACCESS_DENIED, "Access is denied." },
            {ERROR_DUP_NAME, "You were not connected because a duplicate name exists on the network. Go to System in Control Panel to change the computer name, and then try again." },
            {ERROR_BAD_NETPATH, "The network path was not found." },
            { ERROR_ALREADY_ASSIGNED, "Error: Already Assigned" },
            { ERROR_BAD_DEVICE, "Error: Bad Device" },
            { ERROR_BAD_NET_NAME, "The network name cannot be found." },
            { ERROR_BAD_PROVIDER, "Error: Bad Provider" },
            { ERROR_CANCELLED, "Error: Cancelled" },
            { ERROR_EXTENDED_ERROR, "Error: Extended Error" },
            { ERROR_INVALID_ADDRESS, "Error: Invalid Address" },
            { ERROR_INVALID_PARAMETER, "Error: Invalid Parameter" },
            { ERROR_INVALID_PASSWORD, "Error: Invalid Password" },
            { ERROR_SESSION_CREDENTIAL_CONFLICT, "Multiple connections to a server or shared resource by the same user, using more than one user name, are not allowed. Disconnect all previous connections to the server or shared resource and try again." },
            { ERROR_MORE_DATA, "Error: More Data" },
            { ERROR_NO_MORE_ITEMS, "Error: No More Items" },
            { ERROR_NO_NET_OR_BAD_PATH, "Error: No Net Or Bad Path" },
            { ERROR_NO_NETWORK, "Error: No Network" },
            { ERROR_BAD_PROFILE, "Error: Bad Profile" },
            { ERROR_CANNOT_OPEN_PROFILE, "Error: Cannot Open Profile" },
            { ERROR_DEVICE_IN_USE, "Error: Device In Use" },
            { NERR_UseNotFound, "The network connection could not be found." },
            { ERROR_OPEN_FILES, "Error: Open Files" }
        };

        public static string Win32ErrorDescription(int win32ErrorCode)
        {
            if (errorDataStore.TryGetValue(win32ErrorCode, out string description))
            {
                return description;
            }

            return "Unknown error with code: " + win32ErrorCode;
        }
    }
}