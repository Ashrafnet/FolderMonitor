using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FolderMonitor.UI
{
    public partial class ExcludedItemsDialog : Form
    {
        public ExcludedItemsDialog()
        {
            InitializeComponent();
        }
        bool HasChanged = false;
        /// <summary>
        /// Gets the list of excluded files.
        /// </summary>
        public List<string> ExcludedFiles { get; private set; }

        /// <summary>
        /// Gets the list of excluded folders.
        /// </summary>
        public List<string> ExcludedFolders { get; private set; }

        /// <summary>
        /// Gets the string encoding the excluded attributes (RASHCNETO).
        /// </summary>
        public string ExcludedAttributes { get; private set; }
        public string sourceFolder { get; private set; }


        /// <param name="task">Task whose excluded items are to be edited.</param>
        public ExcludedItemsDialog(PathFromAndTo task)
        {
            if (task == null)
                throw new ArgumentNullException("task");
            InitializeComponent();
            sourceFolder = task.From.Path;
            excludedFilesControl.BaseFolder = sourceFolder;
            excludedFoldersControl.BaseFolder = sourceFolder;
            ExcludedFiles = new List<string>(task.From.ExcludedFiles);
            ExcludedFolders = new List<string>(task.From.ExcludedFolders);
           
            
            foreach (string file in ExcludedFiles)
                excludedFilesControl.ExcludedItems.Add(file);
            foreach (string folder in ExcludedFolders)
                excludedFoldersControl.ExcludedItems.Add(folder);

         
        }

      


        private void Control_Changed(object sender, EventArgs e)
        {
            HasChanged = true;
        }


        protected  bool ApplyChanges()
        {
            ExcludedFiles.Clear();
            foreach (string item in excludedFilesControl.ExcludedItems)
                ExcludedFiles.Add(item);

            ExcludedFolders.Clear();
            foreach (string item in excludedFoldersControl.ExcludedItems)
                ExcludedFolders.Add(item);

          

            return true;
        }

        private void ExcludedItemsDialog_Load(object sender, EventArgs e)
        {
           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (HasChanged)
            {
                ApplyChanges();
                DialogResult = DialogResult.OK;
            }
            else
                DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
