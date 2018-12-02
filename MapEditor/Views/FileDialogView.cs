using MapEditor.Dialogs;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor.Views
{
    // OpenFileDialog is sealed so we have to use composition
    class FileDialogView : IFileDialogWindow
    {
        OpenFileDialog dialog;
        public string FileName { get; set; }
        public string InitialDirectory { get; set; }

        public bool? ShowDialog()
        {
            dialog = new OpenFileDialog();
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                FileName = dialog.FileName;
            }
            return result;
        }
    }
}
