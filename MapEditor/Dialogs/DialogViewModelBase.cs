namespace MapEditor.Dialogs
{
    abstract class DialogViewModelBase<TResult>
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public TResult DialogResult { get; set; }

        public DialogViewModelBase(string title = "", string message = "")
        {
            Title = title;
            Message = message;
        }

        public void CloseDialogWithResult(IDialogWindow dialog, TResult result)
        {
            DialogResult = result;

            if (dialog != null)
                dialog.DialogResult = true;     // Closes the Window if it was a WPF Window
        }
    }
}
