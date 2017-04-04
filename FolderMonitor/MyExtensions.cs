using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;

namespace FolderMonitor
{
    public static class MyExtensions
    {
       public static string  UncPrefix="\\\\";
        /// <summary>
        /// Returns a Secure string from the source string
        /// </summary>
        /// <param name="Source"></param>
        /// <returns></returns>
        public static SecureString ToSecureString(this string Source)
        {
            if (string.IsNullOrWhiteSpace(Source))
                return null;
            else
            {
                SecureString Result = new SecureString();
                foreach (char c in Source.ToCharArray())
                    Result.AppendChar(c);
                return Result;
            }
        }
        /// <summary>[AlphaFS] Removes the trailing <see cref="DirectorySeparatorChar"/> character from the string, when present.</summary>
        /// <returns>A text string where the trailing <see cref="DirectorySeparatorChar"/> character has been removed. The function returns <see langword="null"/> when <paramref name="path"/> is <see langword="null"/>.</returns>
        /// <param name="path">A text string from which the trailing <see cref="DirectorySeparatorChar"/> is to be removed, when present.</param>
        [SecurityCritical]
        public static string RemoveTrailingDirectorySeparator(this  string path)
        {
            return path == null ? null : path.TrimEnd(System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar);
        }

        /// <summary>[AlphaFS] Removes the trailing <see cref="DirectorySeparatorChar"/> or <see cref="AltDirectorySeparatorChar"/> character from the string, when present.</summary>
        /// <returns>A text string where the trailing <see cref="DirectorySeparatorChar"/> or <see cref="AltDirectorySeparatorChar"/> character has been removed. The function returns <see langword="null"/> when <paramref name="path"/> is <see langword="null"/>.</returns>
        /// <param name="path">A text string from which the trailing <see cref="DirectorySeparatorChar"/> or <see cref="AltDirectorySeparatorChar"/> is to be removed, when present.</param>
        /// <param name="removeAlternateSeparator">If <see langword="true"/> the trailing <see cref="AltDirectorySeparatorChar"/> character will be removed instead.</param>
        [SecurityCritical]
        public static string RemoveTrailingDirectorySeparator(this string path, bool removeAlternateSeparator)
        {
            return path == null
               ? null
               : path.TrimEnd(removeAlternateSeparator ? System.IO.Path.AltDirectorySeparatorChar : System.IO.Path.DirectorySeparatorChar);
        }
        /// <summary>
        /// Encloses a path in double-quotes if it contains spaces and
        /// makes sure it can be used as Robocopy command-line argument.
        /// </summary>
        public static string QuoteForRobocopy(this string path, bool force = false)
        {
            if (string.IsNullOrEmpty(path))
                return path;

            path = Quote(path, force);

            //// \" is interpreted as escaped double-quote, so if the path ends with \, we need to escape the backslash => \\"
            if (path.EndsWith("\\\"", StringComparison.Ordinal))
                path = path.Substring(0, path.Length - 2) + "\\\\\"";

            return path;
        }
        /// <summary>
        /// Encloses a path in double-quotes if it contains spaces.
        /// </summary>
        public static string Quote(string path, bool force = false)
        {
            if (string.IsNullOrEmpty(path))
                return path;

            if (!force && path.IndexOf(' ') < 0)
                return path;

            return string.Concat('"', path, '"');
        }

        public static string GetFolderName(this string path)
        {
            var end = -1;
            path += "";
            path = path.Trim();
            if (!path.EndsWith("\\"))
                path += "\\";
            for (var i = path.Length; --i >= 0;)
            {
                var ch = path[i];
                if (ch == System.IO.Path.DirectorySeparatorChar ||
                    ch == System.IO.Path.AltDirectorySeparatorChar ||
                    ch == System.IO.Path.VolumeSeparatorChar)
                {
                    if (end > 0)
                    {
                        return path.Substring(i + 1, end - i - 1);
                    }

                    end = i;
                }
            }

            if (end > 0)
            {
                return path.Substring(0, end);
            }

            return path;
        }

        public static string InnerMessages(this Exception er)
        {
            var msg = er.Message;

            while (er.InnerException != null)
            {

                er = er.InnerException;
                msg += Environment.NewLine + "InnerException: " + er.Message;
            }
            return msg;
        }

        public static string Encrypts(this string str)
        {
            return Encryption.Encrypt(str);
        }
        public static string Decrypts(this string str)
        {
            return Encryption.Decrypt(str);
        }

        public static void AppendPathOrWildcard(this StringBuilder arguments, string folder, string pathOrWildcard)
        {
            // paths begin with a directory separator character
            if (pathOrWildcard[0] == System.IO.Path.DirectorySeparatorChar)
                pathOrWildcard = System.IO.Path.Combine(folder, pathOrWildcard.Substring(1));

            arguments.Append(' ');

            // enforce enclosing double-quotes because if a volume shadow copy is used,
            // the source volume will be replaced by the mount point which may contain spaces
            arguments.Append(pathOrWildcard.QuoteForRobocopy(force: true));
        }

        public static DateTime StartOfMinute(this DateTime date)
        {
            
            return date.Floor(TimeSpan.FromMinutes(1));
        }
        public static DateTime Floor(this DateTime date, TimeSpan interval)
        {
            return date.AddTicks(-(date.Ticks % interval.Ticks));
        }
    }
}
