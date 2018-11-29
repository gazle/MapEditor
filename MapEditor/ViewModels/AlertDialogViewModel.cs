using MapEditor.Dialogs;
using System.Windows.Input;

namespace MapEditor.ViewModels
{
    class AlertDialogViewModel : DialogViewModelBase<DialogResults>
    {
        public ICommand OKCommand { get; private set; }

        public AlertDialogViewModel(string title, string message)
            : base(title, message)
        {
            DialogResult = DialogResults.Undefined;
            OKCommand = new DelegateCommand<IDialogWindow>(OK);
        }

        void OK(IDialogWindow window)
        {
            CloseDialogWithResult(window, DialogResults.Undefined);
        }
    }
}
