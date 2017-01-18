/*
 * Copyright (c) Martin Kinkelin
 *
 * See the "License.txt" file in the root directory for infos
 * about permitted and prohibited uses of this code.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace RoboMirror.GUI
{
	public partial class ExcludedItemsControl : UserControl
	{
		private string _baseFolder;

		public ExcludedItemsMode Mode { get; set; }

		public string BaseFolder
		{
			get { return _baseFolder; }
			set
			{
				_baseFolder = value;
				folderBrowserDialog.SelectedPath = value;
				openFileDialog.InitialDirectory = value;
			}
		}

		public System.Collections.IList ExcludedItems { get { return listBox.Items; } }

		public event EventHandler Changed;

		public ExcludedItemsControl()
		{
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			if (DesignMode)
				return;

			if (string.IsNullOrEmpty(BaseFolder))
				throw new InvalidOperationException("The BaseFolder property has not been set yet.");

			var fileDropTargetWrapper = new FileDropTargetWrapper(listBox, Mode == ExcludedItemsMode.Folders ? FileDropMode.Folders : FileDropMode.Files);
			fileDropTargetWrapper.FilesDropped += (s, ea) =>
			{
				if (!TryAddItems(ea.Paths))
				{
					MessageBox.Show(this, "A dropped item is not contained in the source folder.",
						"Invalid item", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			};

			toolTip.SetToolTip(browseButton, Mode == ExcludedItemsMode.Folders
				? "Add a specific subfolder to be excluded with all of its contents."
				: "Add specific files to be excluded.");

			toolTip.SetToolTip(wildcardTextBox, Mode == ExcludedItemsMode.Folders
				? "Enter a wildcard, e.g. \"temp*\" or \"obj\"."
				: "Enter a wildcard, e.g. \"*.tmp\" or \"thumbs.db\".");

			toolTip.SetToolTip(addWildcardButton, Mode == ExcludedItemsMode.Folders
				? "Add the wildcard above to the list.\nAll subfolders with a matching name will be excluded with all of their contents."
				: "Add the wildcard above to the list.\nAll files with a matching name will be excluded from each folder.");

			// add a wildcard by pressing Enter
			wildcardTextBox.KeyPress += (s, ea) => { if (ea.KeyChar == 13 && wildcardTextBox.TextLength > 0) addWildcardButton.PerformClick(); };
		}

		protected virtual void OnChanged(EventArgs e)
		{
			if (Changed != null)
				Changed(this, e);
		}


		private void listBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			removeButton.Enabled = (listBox.SelectedIndices.Count > 0);
		}

		private void listBox_KeyDown(object sender, KeyEventArgs e)
		{
			// simulate a click on the remove button when del or backspace is pressed
			if ((e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back) && removeButton.Enabled)
				removeButton.PerformClick();
		}


		private void browseButton_Click(object sender, EventArgs e)
		{
			if (Mode == ExcludedItemsMode.Folders)
				AddFolder();
			else
				AddFiles();
		}

		private void AddFolder()
		{
			while (true)
			{
				if (folderBrowserDialog.ShowDialog(this) != DialogResult.OK)
					return;

				if (TryAddItems(new[] { folderBrowserDialog.SelectedPath }))
					break;

				MessageBox.Show(this, "The selected folder is not contained in the source folder.",
					"Invalid subfolder", MessageBoxButtons.OK, MessageBoxIcon.Error);

				folderBrowserDialog.SelectedPath = BaseFolder;
			}
		}

		private void AddFiles()
		{
			while (true)
			{
				if (openFileDialog.ShowDialog(this) != DialogResult.OK)
					return;

				if (TryAddItems(openFileDialog.FileNames))
					break;

				MessageBox.Show(this, "A selected file is not contained in the source folder.",
					"Invalid file", MessageBoxButtons.OK, MessageBoxIcon.Error);

				openFileDialog.InitialDirectory = BaseFolder;
				openFileDialog.FileName = string.Empty;
			}

			openFileDialog.InitialDirectory = Path.GetDirectoryName(openFileDialog.FileName);
			openFileDialog.FileName = string.Empty;
		}

		bool TryAddItems(IEnumerable<string> paths)
		{
			var relativePaths = new List<string>();

			foreach (string path in paths)
			{
				string relativePath;
				if (!IsInFolder(path, BaseFolder, out relativePath) ||
				    relativePath.Length == 1) // path is the BaseFolder itself
					return false;

				if (!listBox.Items.Contains(relativePath))
					relativePaths.Add(relativePath);
			}

			if (relativePaths.Count > 0)
			{
				listBox.Items.AddRange(relativePaths.ToArray());
				OnChanged(EventArgs.Empty);
			}

			return true;
		}

        /// <summary>
        /// Returns true if the specified path is contained by the specified folder or the folder itself.
        /// </summary>
        /// <param name="relativePath">Set to the resulting relative path, always beginning with a directory separator character.</param>
        public static bool IsInFolder(string path, string folder, out string relativePath)
        {
            if (string.IsNullOrEmpty(folder))
                throw new ArgumentNullException("folder");

            relativePath = null;

            if (string.IsNullOrEmpty(path))
                return false;

            // use a case-insensitive comparison under Windows
            var comparison = (Path.DirectorySeparatorChar == '\\' ?
                StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);

            string prefix = AppendSeparator(folder);
            if (!path.StartsWith(prefix, comparison))
                return false;

            relativePath = path.Substring(prefix.Length - 1);
            return true;
        }
        public static string AppendSeparator(string path)
        {
            return (EndsWithSeparator(path) ? path : path + Path.DirectorySeparatorChar);
        }
        public static bool EndsWithSeparator(string path)
        {
            return (!string.IsNullOrEmpty(path) && path[path.Length - 1] == Path.DirectorySeparatorChar);
        }
        private void removeButton_Click(object sender, EventArgs e)
		{
			if (listBox.SelectedIndices.Count == 0)
				return;

			if (MessageBox.Show(this, "Are you sure you want to remove the selected exclusion(s)?", "Confirmation",
				MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
				return;

			var selectedIndices = new System.Collections.ArrayList(listBox.SelectedIndices);

			// if the items are deleted beginning with the highest index,
			// the lower indices remain valid
			selectedIndices.Sort();
			for (int i = selectedIndices.Count - 1; i >= 0; --i)
				listBox.Items.RemoveAt((int)selectedIndices[i]);

			OnChanged(EventArgs.Empty);
		}


		private void wildcardTextBox_TextChanged(object sender, EventArgs e)
		{
			addWildcardButton.Enabled = !string.IsNullOrEmpty(wildcardTextBox.Text);
		}

		private void addWildcardButton_Click(object sender, EventArgs e)
		{
			if (wildcardTextBox.TextLength == 0)
				return;

			string wildcard = wildcardTextBox.Text;
			if (wildcard.Contains(Path.DirectorySeparatorChar.ToString()))
			{
				MessageBox.Show(this, "Wildcards must not contain any path information.",
					"Invalid wildcard", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else if (wildcard.Contains("\""))
			{
				MessageBox.Show(this, "Wildcards must not contain any double-quotes.",
					"Invalid wildcard", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else if (!listBox.Items.Contains(wildcard))
			{
				listBox.Items.Add(wildcard);
				OnChanged(EventArgs.Empty);
				wildcardTextBox.Text = string.Empty;
			}

			wildcardTextBox.Focus();
		}
	}

	public enum ExcludedItemsMode
	{
		Folders,
		Files
	}
}
