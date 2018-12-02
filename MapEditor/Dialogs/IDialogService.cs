namespace MapEditor.Dialogs
{
    interface IDialogService
    {
        DialogResults OpenDialog(DialogViewModelBase viewModel);
    }
}
