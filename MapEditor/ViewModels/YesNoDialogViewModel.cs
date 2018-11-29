using MapEditor.Dialogs;
using System.Windows.Input;

namespace MapEditor.ViewModels
{
    class YesNoDialogViewModel : DialogViewModelBase<DialogResults>
    {
        public ICommand YesCommand { get; private set; }
        public ICommand NoCommand { get; private set; }

        public YesNoDialogViewModel(string title, string message)
            : base(title, message)
        {
            DialogResult = DialogResults.Undefined;
            YesCommand = new DelegateCommand<IDialogWindow>(Yes);
            NoCommand = new DelegateCommand<IDialogWindow>(No);
        }

        void Yes(IDialogWindow window)
        {
            CloseDialogWithResult(window, DialogResults.Yes);
        }

        void No(IDialogWindow window)
        {
            CloseDialogWithResult(window, DialogResults.No);
        }
    }
}
