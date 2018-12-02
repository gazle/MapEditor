using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor.Dialogs
{
    interface IFileDialogWindow
    {
        string FileName { get; set; }
        string InitialDirectory { get; set; }
        bool? ShowDialog();
    }
}
