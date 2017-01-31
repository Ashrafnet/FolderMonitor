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
using System.Net.NetworkInformation;
using System.Security;

namespace Alphaleonis.Win32.Network
{/*
   partial class Host
   {
      /// <summary>Enumerates drives from the local host.</summary>
      /// <returns><see cref="IEnumerable{String}"/> drives from the local host.</returns>
      /// <exception cref="NetworkInformationException"/>
      [SecurityCritical]
      public static IEnumerable<string> EnumerateDrives()
      {
         return EnumerateDrivesCore(null, false);
      }

      /// <summary>Enumerates local drives from the specified host.</summary>
      /// <returns><see cref="IEnumerable{String}"/> drives from the specified host.</returns>
      /// <exception cref="NetworkInformationException"/>
      /// <param name="host">The DNS or NetBIOS name of the remote server. <see langword="null"/> refers to the local host.</param>
      /// <param name="continueOnException">
      ///   <para><see langword="true"/> suppress any Exception that might be thrown as a result from a failure,</para>
      ///   <para>such as unavailable resources.</para>
      /// </param>
      [SecurityCritical]
      public static IEnumerable<string> EnumerateDrives(string host, bool continueOnException)
      {
         return EnumerateDrivesCore(host, continueOnException);
      }



      /// <summary>Enumerates local drives from the specified host.</summary>
      /// <returns><see cref="IEnumerable{String}"/> drives from the specified host.</returns>
      /// <exception cref="ArgumentNullException"/>
      /// <exception cref="NetworkInformationException"/>
      /// <param name="host">The DNS or NetBIOS name of the remote server. <see langword="null"/> refers to the local host.</param>
      /// <param name="continueOnException">
      ///   <para><see langword="true"/> suppress any Exception that might be thrown as a result from a failure,</para>
      ///   <para>such as unavailable resources.</para>
      /// </param>
      [SecurityCritical]
      private static IEnumerable<string> EnumerateDrivesCore(string host, bool continueOnException)
      {
         return EnumerateNetworkObjectCore(new FunctionData { EnumType = 1 }, (string structure, SafeGlobalMemoryBufferHandle buffer) =>

            structure,

            (FunctionData functionData, out SafeGlobalMemoryBufferHandle buffer, int prefMaxLen, out uint entriesRead, out uint totalEntries, out uint resume) =>
            {
               // When host == null, the local computer is used.
               // However, the resulting Host property will be empty.
               // So, explicitly state Environment.MachineName to prevent this.
               // Furthermore, the UNC prefix: \\ is not required and always removed.
               string stripUnc = Utils.IsNullOrWhiteSpace(host) ? Environment.MachineName : Path.GetRegularPathCore(host, GetFullPathOptions.CheckInvalidPathChars, false).Replace(Path.UncPrefix, string.Empty);

               return NativeMethods.NetServerDiskEnum(stripUnc, 0, out buffer, NativeMethods.MaxPreferredLength, out entriesRead, out totalEntries, out resume);

            }, continueOnException);
      }
   }
    */
}
