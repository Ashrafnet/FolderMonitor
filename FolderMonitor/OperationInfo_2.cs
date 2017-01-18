using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FolderMonitor
{
   public  class OperationInfo
    {
        public string From { get; set; }

        public System.IO.WatcherChangeTypes OperationType { get; set; }
    }
}
