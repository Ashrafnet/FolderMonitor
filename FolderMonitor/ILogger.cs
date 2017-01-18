using System;

namespace Ashrafnet.FileSync
{
    /// <summary>
    /// the logger interface
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Triggers on <c>FileSync.Changed</c> events
        /// </summary>
        /// <param name="source">The sender</param>
        /// <param name="path">Path to the destination file, that was changed</param>
        void OnSyncChanged(object source, string path);

        /// <summary>
        /// Triggers on <c>FileSync.Created</c> events
        /// </summary>
        /// <param name="source">The sender</param>
        /// <param name="path">Path to the destination file, that was changed</param>
        void OnSyncCreated(object source, string path);

        /// <summary>
        /// Triggers on <c>FileSync.Deleted</c> events
        /// </summary>
        /// <param name="source">The sender</param>
        /// <param name="path">Path to the destination file, that was changed</param>
        void OnSyncDeleted(object source, string path);

        /// <summary>
        /// Triggers on <c>FileSync.Renamed</c> events
        /// </summary>
        /// <param name="source">The sender</param>
        /// <param name="oldpath">Old Path to the destination file, that was changed</param>
        /// <param name="newpath">New Path to the destination file, that was changed</param>
        void OnSyncRenamed(object source, string oldpath, string newpath);

        void OnErrorOccure(object source, Exception exception);
    }        

}
