using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;

namespace FolderMonitor.UI
{
    public class ServiceManager : ServiceController
    {
        private string m_ImagePath;
        public static string _FolderMonitorServiceName = "DirectoryMonitor";
        #region Private : Fields

#pragma warning disable CS0169 // The field 'ServiceManager.m_bExists' is never used
        private bool m_bExists;
#pragma warning restore CS0169 // The field 'ServiceManager.m_bExists' is never used
#pragma warning disable CS0169 // The field 'ServiceManager.m_bExistsAvailable' is never used
        private bool m_bExistsAvailable;
#pragma warning restore CS0169 // The field 'ServiceManager.m_bExistsAvailable' is never used
#pragma warning disable CS0169 // The field 'ServiceManager.m_sRemoteResource' is never used
        private string m_sRemoteResource;
#pragma warning restore CS0169 // The field 'ServiceManager.m_sRemoteResource' is never used
        private ServiceStartMode m_eStartMode;
        private bool m_bStartModeAvailable;

        #endregion

        #region Private : Properties

        private bool BrowseGranted
        {
            get
            {
                return GetPrivateField<bool>("browseGranted");
            }

            set
            {
                SetPrivateField("browseGranted", value);
            }
        }

        private bool ControlGranted
        {
            get
            {
                return GetPrivateField<bool>("controlGranted");
            }

            set
            {
                SetPrivateField("controlGranted", value);
            }
        }

        #endregion        

        public ServiceStartMode StartMode
        {
            get
            {
                return GetStartMode();
            }

            set
            {
                SetStartMode(value);
            }
        }

        private FieldInfo GetPrivateFieldInfo(string sFieldName)
        {
            Type oType = typeof(ServiceController);

            FieldInfo oFieldInfo = oType.GetField(
                sFieldName,
                (BindingFlags.Instance | BindingFlags.NonPublic)
            );

            return oFieldInfo;
        }

        private T GetPrivateField<T>(string sFieldName)
        {
            FieldInfo oFieldInfo = GetPrivateFieldInfo(sFieldName);

            Debug.Assert(null != oFieldInfo);

            return (T)oFieldInfo.GetValue(this);
        }

        private MethodInfo GetPrivateMethodInfo(string sMethodName, bool bStatic)
        {
            Type oType = typeof(ServiceController);

            MethodInfo oMethodInfo = oType.GetMethod(
                sMethodName,
                (BindingFlags.NonPublic | (bStatic ? BindingFlags.Static : BindingFlags.Instance))
            );

            return oMethodInfo;
        }

        private IntPtr GetServiceHandle(uint nDesiredAccess)
        {
            MethodInfo oMethodInfo = GetPrivateMethodInfo("GetServiceHandle", false);

            Debug.Assert(null != oMethodInfo);

            return (IntPtr)oMethodInfo.Invoke(this, new object[] { (int)nDesiredAccess });
        }

        private void SetPrivateField<T>(string sFieldName, T oValue)
        {
            FieldInfo oFieldInfo = GetPrivateFieldInfo(sFieldName);

            Debug.Assert(null != oFieldInfo);

            oFieldInfo.SetValue(this, oValue);
        }

        public ServiceManager()
            : base(_FolderMonitorServiceName)
        {
        }

        public ServiceManager(string name)
            : base(name)
        {
        }

        public ServiceManager(string name, string machineName)
            : base(name, machineName)
        {
        }

        public bool ServiceInstalled { get
            {
                try
                {
                    Refresh();
                    var ss=Status;
                    return true;
                }
                catch
                {
                    return false;
                    
                }
                
            }
        }
        public string ImagePath
        {
            get
            {
                if (m_ImagePath == null)
                {
                    m_ImagePath = GetImagePath();
                }
                return m_ImagePath;
            }
        }

        private ServiceStartMode GetStartMode()
        {
            //if (m_bStartModeAvailable)
            //{
            //    return m_eStartMode;
            //}

            CheckBrowsePermission();

            IntPtr oServiceHandle = GetServiceHandle(WindowsServicesHelper.SERVICE_QUERY_CONFIG);

            try
            {
                int nBytesNeeded;

                if (!WindowsServicesHelper.QueryServiceConfig(
                    oServiceHandle,
                    IntPtr.Zero,
                    0,
                    out nBytesNeeded
                ))
                {
                    int nLastWin32Error = Marshal.GetLastWin32Error();

                    if (WindowsServicesHelper.ERROR_INSUFFICIENT_BUFFER != nLastWin32Error)
                    {
                        throw CreateSafeWin32Exception();
                    }

                    IntPtr oConfigPointer = Marshal.AllocHGlobal(nBytesNeeded);

                    try
                    {
                        if (!WindowsServicesHelper.QueryServiceConfig(
                            oServiceHandle,
                            oConfigPointer,
                            nBytesNeeded,
                            out nBytesNeeded
                        ))
                        {
                            throw CreateSafeWin32Exception();
                        }

                        WindowsServicesHelper.QUERY_SERVICE_CONFIG oQueryServiceConfig = new WindowsServicesHelper.QUERY_SERVICE_CONFIG();
                        Marshal.PtrToStructure(oConfigPointer, oQueryServiceConfig);

                        m_eStartMode = (ServiceStartMode)oQueryServiceConfig.dwStartType;
                        m_bStartModeAvailable = true;
                    }
                    finally
                    {
                        Marshal.FreeHGlobal(oConfigPointer);
                    }
                }

                return m_eStartMode;
            }
            finally
            {
                WindowsServicesHelper.CloseServiceHandle(oServiceHandle);
            }
        }
        private Win32Exception CreateSafeWin32Exception()
        {
            MethodInfo oMethodInfo = GetPrivateMethodInfo("CreateSafeWin32Exception", true);

            Debug.Assert(null != oMethodInfo);

            return (Win32Exception)oMethodInfo.Invoke(this, null);
        }
        private void SetStartMode(ServiceStartMode eStartMode)
        {
            if (m_bStartModeAvailable && (eStartMode == m_eStartMode))
            {
                return;
            }

            CheckControlPermission();

            IntPtr oServiceHandle = GetServiceHandle(WindowsServicesHelper.SERVICE_QUERY_CONFIG | WindowsServicesHelper.SERVICE_CHANGE_CONFIG);

            try
            {
                if (!WindowsServicesHelper.ChangeServiceConfig(
                    oServiceHandle,
                    WindowsServicesHelper.SERVICE_NO_CHANGE,
                    (int)eStartMode,
                    WindowsServicesHelper.SERVICE_NO_CHANGE,
                    null,
                    null,
                    IntPtr.Zero,
                    null,
                    null,
                    null,
                    null
                ))
                {
                    throw CreateSafeWin32Exception();
                }

                m_eStartMode = eStartMode;
                m_bStartModeAvailable = true;
            }
            finally
            {
                WindowsServicesHelper.CloseServiceHandle(oServiceHandle);
            }
        }


        private void SetAccount(string username,string password)
        {
          

            CheckControlPermission();

            IntPtr oServiceHandle = GetServiceHandle(WindowsServicesHelper.SERVICE_QUERY_CONFIG | WindowsServicesHelper.SERVICE_CHANGE_CONFIG);

            try
            {
                if (!WindowsServicesHelper.ChangeServiceConfig(
                    oServiceHandle,
                    WindowsServicesHelper.SERVICE_NO_CHANGE, WindowsServicesHelper.SERVICE_NO_CHANGE,
                    
                    WindowsServicesHelper.SERVICE_NO_CHANGE,
                    null,
                    null,
                    IntPtr.Zero,
                    null,
                    username ,
                    password ,
                    null
                ))
                {
                    throw CreateSafeWin32Exception();
                }

                
                
            }
            finally
            {
                WindowsServicesHelper.CloseServiceHandle(oServiceHandle);
            }
        }

        private void CheckBrowsePermission()
        {
            if (!BrowseGranted)
            {
                new ServiceControllerPermission(
                    ServiceControllerPermissionAccess.Browse,
                    MachineName,
                    ServiceName
                ).Demand();

                BrowseGranted = true;
            }
        }

        private void CheckControlPermission()
        {
            if (!ControlGranted)
            {
                new ServiceControllerPermission(
                    ServiceControllerPermissionAccess.Control,
                    MachineName,
                    ServiceName
                ).Demand();

                ControlGranted = true;
            }
        }

        public static new ServiceController[] GetServices()
        {
            return GetServices(".");
        }

        public static new ServiceController[] GetServices(string machineName)
        {
            return GetServices(ServiceController.GetServices
                (machineName));
        }

        private string GetImagePath()
        {
            string registryPath = @"SYSTEM\CurrentControlSet\Services\" + _FolderMonitorServiceName;
            RegistryKey keyHKLM = Registry.LocalMachine;

            RegistryKey key;
            
                key = keyHKLM.OpenSubKey(registryPath);


            string value = key.GetValue("ImagePath") + "";
            key.Close();
            return ExpandEnvironmentVariables(value);
            //return value;
        }


        public  string GetAccountName()
        {
            string registryPath = @"SYSTEM\CurrentControlSet\Services\" + _FolderMonitorServiceName;
            RegistryKey keyHKLM = Registry.LocalMachine;

            RegistryKey key;

            key = keyHKLM.OpenSubKey(registryPath);


            string value = key.GetValue("ObjectName") + "";
            key.Close();
            return ExpandEnvironmentVariables(value);
            //return value;
        }
        public void  SetAccountName(string AccountName)
        {
            string registryPath = @"SYSTEM\CurrentControlSet\Services\" + _FolderMonitorServiceName;
            RegistryKey keyHKLM = Registry.LocalMachine;

            RegistryKey key;

            key = keyHKLM.OpenSubKey(registryPath,true );


             key.SetValue ("ObjectName", AccountName) ;
            key.Close();
            //return value;
        }

        private string ExpandEnvironmentVariables(string path)
        {
            
                return Environment.ExpandEnvironmentVariables(path);
           
            
        }

        private static ServiceController[] GetServices
             (ServiceController[] systemServices)
        {
            List<ServiceController> services = new List<ServiceController>
                      (systemServices.Length);
            foreach (ServiceController service in systemServices)
            {
                services.Add(new ServiceController
                    (service.ServiceName, service.MachineName));
            }
            return services.ToArray();
        }
    }
}
