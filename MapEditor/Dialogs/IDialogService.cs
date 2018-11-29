namespace MapEditor.Dialogs
{
    interface IDialogService
    {
        TResult OpenDialog<TResult>(DialogViewModelBase<TResult> viewModel);
    }
}
