using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace FolderMonitor
{
    public sealed class WindowsServicesHelper
    {
        #region Public : Constants

        public const uint ERROR_INSUFFICIENT_BUFFER = 122;

        public const int  SERVICE_NO_CHANGE         = -1;         /* 0xFFFFFFFF */
	    public const uint SERVICE_QUERY_CONFIG      = 0x00000001;
	    public const uint SERVICE_CHANGE_CONFIG     = 0x00000002;

        #endregion
        
        #region Public : Static

        [StructLayout(LayoutKind.Sequential)]
        
#pragma warning disable 649, 169

        public class QUERY_SERVICE_CONFIG
        {
          public int          dwServiceType;
          public int          dwStartType;
          public int          dwErrorControl;
          public unsafe char* lpBinaryPathName;
          public unsafe char* lpLoadOrderGroup;
          public int          dwTagId;
          public unsafe char* lpDependencies;
          public unsafe char* lpServiceStartName;
          public unsafe char* lpDisplayName;
        }

#pragma warning restore 649, 169

        [DllImport(DllNames.ADVAPI32, CharSet = CharSet.Unicode, SetLastError = true)]

	    public static extern bool ChangeServiceConfig
        (
	        IntPtr hService,
	        int    nServiceType,
	        int    nStartType,
	        int    nErrorControl,
	        string lpBinaryPathName,
	        string lpLoadOrderGroup,
	        IntPtr lpdwTagId,
	        char[] lpDependencies,
	        string lpServiceStartName,
	        string lpPassword,
	        string lpDisplayName
        );

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(DllNames.ADVAPI32, CharSet = CharSet.Unicode, SetLastError = true)]
    
        public static extern bool CloseServiceHandle(IntPtr oHandle);

        [DllImport(DllNames.ADVAPI32, CharSet = CharSet.Unicode, SetLastError = true)]

        public static extern bool QueryServiceConfig(
            IntPtr  hServiceHandle, 
            IntPtr  oQueryServiceConfigPtr, 
            int     nBufferSize, 
            out int nBytesNeeded
        );

        #endregion
    }

    public static class DllNames
    {
        #region Public : Static

        public const string ADVAPI32 = "advapi32.dll";
        public const string MPR = "mpr.dll";
        public const string NETAPI32 = "netapi32.dll";

        #endregion
    }
}
