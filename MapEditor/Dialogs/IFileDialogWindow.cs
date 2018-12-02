namespace MapEditor.Dialogs
{
    interface IFileDialogWindow
    {
        string FileName { get; set; }
        string InitialDirectory { get; set; }
        bool? ShowDialog();
    }
}
