using System;

namespace MapEditor.Dialogs
{
    class FileDialogService<TFileDialogWindow> : IFileDialogService where TFileDialogWindow : IFileDialogWindow
    {
        public string OpenFileDialog(string initialDirectory)
        {
            var window = (IFileDialogWindow)Activator.CreateInstance(typeof(TFileDialogWindow));
            window.InitialDirectory = initialDirectory;
            window.ShowDialog();
            return window.FileName;
        }
    }
}
