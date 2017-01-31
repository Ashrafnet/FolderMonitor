/*  Copyright (C) 2008-2016 Peter Palotas, Jeffrey Jangli, Alexandr Normuradov
 *  
 *  Permission is hereby granted, free of charge, to any person obtaining a copy 
 *  of this software and associated documentation files (the "Software"), to deal 
 *  in the Software without restriction, including without limitation the rights 
 *  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
 *  copies of the Software, and to permit persons to whom the Software is 
 *  furnished to do so, subject to the following conditions:
 *  
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *  
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
 *  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN 
 *  THE SOFTWARE. 
 */

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using Path = System.IO.Path ;
using FolderMonitor;

namespace Alphaleonis.Win32.Network
{
   /// <summary>Provides static methods to retrieve network resource information from a local- or remote host.</summary>
   public static partial class Host
   {
      #region GetUncName

      /// <summary>Return the host name in UNC format, for example: \\hostname.</summary>
      /// <returns>The unc name.</returns>
      [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
      [SecurityCritical]
      public static string GetUncName()
      {
         return string.Format(CultureInfo.InvariantCulture, "{0}{1}", MyExtensions .UncPrefix, Environment.MachineName);
      }

      /// <summary>Return the host name in UNC format, for example: \\hostname.</summary>
      /// <param name="computerName">Name of the computer.</param>
      /// <returns>The unc name.</returns>
      [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Utils.IsNullOrWhiteSpace validates arguments.")]
      [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
      [SecurityCritical]
      public static string GetUncName(string computerName)
      {
         return Utils.IsNullOrWhiteSpace(computerName)
            ? GetUncName()
            : (computerName.StartsWith(MyExtensions .UncPrefix, StringComparison.OrdinalIgnoreCase)
               ? computerName.Trim()
               : MyExtensions.UncPrefix + computerName.Trim());
      }

      #endregion // GetUncName

      #region Internal Methods

      private delegate uint EnumerateNetworkObjectDelegate(
         FunctionData functionData, out SafeGlobalMemoryBufferHandle netApiBuffer, [MarshalAs(UnmanagedType.I4)] int prefMaxLen,
         [MarshalAs(UnmanagedType.U4)] out uint entriesRead, [MarshalAs(UnmanagedType.U4)] out uint totalEntries,
         [MarshalAs(UnmanagedType.U4)] out uint resumeHandle);

      /// <summary>Structure is used to pass additional data to the Win32 function.</summary>
      private struct FunctionData
      {
         public int EnumType;
         public string ExtraData1;
         public string ExtraData2;
      }

      [SecurityCritical]
      private static IEnumerable<TStruct> EnumerateNetworkObjectCore<TStruct, TNative>(FunctionData functionData, Func<TNative, SafeGlobalMemoryBufferHandle, TStruct> createTStruct, EnumerateNetworkObjectDelegate enumerateNetworkObject, bool continueOnException) 
      {
         Type objectType;
         int objectSize;
         bool isString;

         switch (functionData.EnumType)
         {
            // Logical Drives
            case 1:
               objectType = typeof(IntPtr);
               isString = true;
               objectSize = Marshal.SizeOf(objectType) + UnicodeEncoding.CharSize;
               break;

            default:
               objectType = typeof(TNative);
               isString = objectType == typeof(string);
               objectSize = isString ? 0 : Marshal.SizeOf(objectType);
               break;
         }


         uint lastError;
         do
         {
            uint entriesRead;
            uint totalEntries;
            uint resumeHandle;
            SafeGlobalMemoryBufferHandle buffer;

            lastError = enumerateNetworkObject(functionData, out buffer, NativeMethods.MaxPreferredLength, out entriesRead, out totalEntries, out resumeHandle);

            using (buffer)
               switch (lastError)
               {
                  case Win32Errors.NERR_Success:
                  case Win32Errors.ERROR_MORE_DATA:
                     if (entriesRead > 0)
                     {
                        for (int i = 0, itemOffset = 0; i < entriesRead; i++, itemOffset += objectSize)
                           yield return (TStruct) (isString
                              ? buffer.PtrToStringUni(itemOffset, 2)
                              : (object) createTStruct(buffer.PtrToStructure<TNative>(itemOffset), buffer));
                     }
                     break;

                  case Win32Errors.ERROR_BAD_NETPATH:
                     break;

                  // Observed when SHARE_INFO_503 is requested but not supported/possible.
                  case Win32Errors.RPC_X_BAD_STUB_DATA:
                     yield break;
               }

         } while (lastError == Win32Errors.ERROR_MORE_DATA);

         if (lastError != Win32Errors.NO_ERROR && !continueOnException)
            throw new NetworkInformationException((int) lastError);
      }
      
   

      #endregion // Internal Methods
   }
}
