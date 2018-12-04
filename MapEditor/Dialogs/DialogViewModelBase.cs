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
            YesCommand = new DelegateCommand<IDialogWindow>(Yes, CanYes);
            NoCommand = new DelegateCommand<IDialogWindow>(No, CanNo);
            OKCommand = new DelegateCommand<IDialogWindow>(OK, CanOK);
            CancelCommand = new DelegateCommand<IDialogWindow>(Cancel, CanCancel);
            Title = title;
            Message = message;
        }

        protected virtual void Yes(IDialogWindow window)
        {
            CloseDialogWithResult(window, DialogResults.Yes);
        }

        protected virtual void No(IDialogWindow window)
        {
            CloseDialogWithResult(window, DialogResults.No);
        }

        protected virtual void OK(IDialogWindow window)
        {
            CloseDialogWithResult(window, DialogResults.OK);
        }

        protected virtual void Cancel(IDialogWindow window)
        {
            CloseDialogWithResult(window, DialogResults.Cancel);
        }

        protected virtual bool CanYes(IDialogWindow window)
        {
            return true;
        }

        protected virtual bool CanNo(IDialogWindow window)
        {
            return true;
        }

        protected virtual bool CanOK(IDialogWindow window)
        {
            return true;
        }

        protected virtual bool CanCancel(IDialogWindow window)
        {
            return true;
        }

        public void CloseDialogWithResult(IDialogWindow dialog, DialogResults result)
        {
            DialogResult = result;

            if (dialog != null)
                dialog.DialogResult = true;     // Closes the Window if it was a WPF Window
        }
    }
}
