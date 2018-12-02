using System;

namespace MapEditor.Dialogs
{
    class DialogService<TDialogWindow> : IDialogService where TDialogWindow : IDialogWindow
    {
        public DialogResults OpenDialog(DialogViewModelBase viewModel)
        {
            var window = (IDialogWindow)Activator.CreateInstance(typeof(TDialogWindow));
            window.DataContext = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            window.ShowDialog();
            return viewModel.DialogResult;
        }
    }
}
