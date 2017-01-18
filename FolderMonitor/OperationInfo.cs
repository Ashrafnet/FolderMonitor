using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FolderMonitor
{
  public   class OperationInfo
    {
        public string From { get; set; }
        public string To { get; set; }
        public WatcherChangeTypes OperationType { get; set; }
    }

 
}
