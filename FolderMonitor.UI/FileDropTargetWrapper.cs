/*
 * Copyright (c) Martin Kinkelin
 *
 * See the "License.txt" file in the root directory for infos
 * about permitted and prohibited uses of this code.
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace FolderMonitor
{
	[Flags]
	public enum FileDropMode
	{
		File = 1,
		Folder = 2,
		MultiDrop = 4,

		SingleFileOrFolder = 3,
		Files = 5,
		Folders = 6,
		FilesAndFolders = 7
	}

	/// <summary>
	/// Wraps a control's functionality as drag & drop target for files and/or folders.
	/// Construct an instance by passing the control and then listen for the FilesDropped event.
	/// </summary>
	public class FileDropTargetWrapper
	{
		private FileDropMode _mode;

		public event EventHandler<FileDropEventArgs> FilesDropped;

		public FileDropTargetWrapper(Control target, FileDropMode mode)
		{
			if (target == null)
				throw new ArgumentNullException("target");

			_mode = mode;

			target.AllowDrop = true;
			target.DragEnter += OnDragEnter;
			target.DragDrop += OnDragDrop;
		}

		private void OnDragEnter(object sender, DragEventArgs e)
		{
			string[] paths;
			e.Effect = (IsSupported(e.Data, out paths) ? DragDropEffects.Copy : DragDropEffects.None);
		}

		private void OnDragDrop(object sender, DragEventArgs e)
		{
			string[] paths;
			if (FilesDropped == null || !IsSupported(e.Data, out paths))
				return;

			FilesDropped(sender, new FileDropEventArgs(paths));
		}

		private bool IsSupported(IDataObject obj, out string[] paths)
		{
			paths = null;

			if (!obj.GetDataPresent(DataFormats.FileDrop))
				return false;

			paths = obj.GetData(DataFormats.FileDrop) as string[];
			if (paths == null || paths.Length == 0)
				return false;

			if ((_mode & FileDropMode.MultiDrop) == 0 && paths.Length > 1)
				return false;

			var singleMode = _mode & FileDropMode.SingleFileOrFolder;
			if (singleMode == FileDropMode.SingleFileOrFolder)
				return true;

			foreach (var path in paths)
			{
				if ((singleMode == FileDropMode.File && !System.IO.File.Exists(path)) ||
					(singleMode == FileDropMode.Folder && !System.IO.Directory.Exists(path)))
					return false;
			}

			return true;
		}
	}

	public class FileDropEventArgs : EventArgs
	{
		public string[] Paths { get; private set; }

		public FileDropEventArgs(string[] paths)
		{
			Paths = paths;
		}
	}
}
