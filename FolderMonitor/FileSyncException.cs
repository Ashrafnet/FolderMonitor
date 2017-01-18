using System;

namespace Ashrafnet.FileSync
{

    /// <summary>
    /// The FileSync Exception class
    /// </summary>
    public class FileSyncException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FileSyncException()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">The error message</param>
        public FileSyncException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="inner">the nested exception</param>
        public FileSyncException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

}
