using MapEditor.Dialogs;
using MapEditor.Views;
using System.Windows.Input;

namespace MapEditor.ViewModels
{
    class ViewModel : ViewModelBase
    {
        IDialogService dialogService;
        public ICommand YesNoCommand { get; private set; }
        public ICommand AlertCommand { get; private set; }

        public ViewModel()
        {
            // Normally we would do this with dependency injection
            dialogService = new DialogService<DialogWindow>();
            YesNoCommand = new DelegateCommand(YesNo);
            AlertCommand = new DelegateCommand(Alert);
        }

        private void YesNo()
        {
            var dialog = new YesNoDialogViewModel("Question", "Can you see this?");
            var result = dialogService.OpenDialog(dialog);
        }

        private void Alert()
        {
            var dialog = new AlertDialogViewModel("Attention", "Alert");
            var result = dialogService.OpenDialog(dialog);
        }
    }
}
