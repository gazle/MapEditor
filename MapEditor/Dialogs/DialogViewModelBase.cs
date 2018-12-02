using System.Windows.Input;

namespace MapEditor.Dialogs
{
    abstract class DialogViewModelBase : ViewModels.ViewModelBase
    {
        public ICommand YesCommand { get; protected set; }
        public ICommand NoCommand { get; protected set; }
        public ICommand OKCommand { get; protected set; }
        public ICommand CancelCommand { get; protected set; }

        public string Title { get; }
        public string Message { get; }
        public DialogResults DialogResult { get; protected set; }

        public DialogViewModelBase(string title = "", string message = "")
        {
            YesCommand = new DelegateCommand<IDialogWindow>(Yes);
            NoCommand = new DelegateCommand<IDialogWindow>(No);
            OKCommand = new DelegateCommand<IDialogWindow>(OK);
            CancelCommand = new DelegateCommand<IDialogWindow>(Cancel);
            Title = title;
            Message = message;
        }

        void Yes(IDialogWindow window)
        {
            CloseDialogWithResult(window, DialogResults.Yes);
        }

        void No(IDialogWindow window)
        {
            CloseDialogWithResult(window, DialogResults.No);
        }

        void OK(IDialogWindow window)
        {
            CloseDialogWithResult(window, DialogResults.OK);
        }

        void Cancel(IDialogWindow window)
        {
            CloseDialogWithResult(window, DialogResults.Cancel);
        }

        public void CloseDialogWithResult(IDialogWindow dialog, DialogResults result)
        {
            DialogResult = result;

            if (dialog != null)
                dialog.DialogResult = true;     // Closes the Window if it was a WPF Window
        }
    }
}
