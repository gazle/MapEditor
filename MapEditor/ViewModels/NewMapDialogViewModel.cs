using MapEditor.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media.Imaging;

namespace MapEditor.ViewModels
{
    class NewMapDialogViewModel : DialogViewModelBase, IDataErrorInfo
    {
        public int MapWidth { get; set; }
        public int MapHeight { get; set; }
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }
        public BitmapImage Bitmap { get; set; }
        public string Error => string.Empty;
        Dictionary<string, (Func<bool> Condition, string Message)> conditions = new Dictionary<string, (Func<bool>, string)>();

        public string this[string name]
        {
            get
            {
                //int value = (int)GetType().GetProperty(name).GetValue(this);      // Reflection
                string result = !conditions[name].Condition() ? conditions[name].Message : null;
                ((DelegateCommand<IDialogWindow>)OKCommand).RaiseCanExecuteChanged();
                return result;
            }
        }

        public NewMapDialogViewModel()
            : base("New Map")
        {
            conditions.Add("MapWidth", (() => MapWidth >= 1 && MapWidth <= 100, "Must be greater than 0 and less than or equal 100."));
            conditions.Add("MapHeight", (() => MapHeight >= 1 && MapHeight <= 100, "Must be greater than 0 and less than or equal 100."));
            conditions.Add("TileWidth", (() => TileWidth >= 1 && TileWidth <= 100, "Must be greater than 0 and less than or equal 100."));
            conditions.Add("TileHeight", (() => TileHeight >= 1 && TileHeight <= 100, "Must be greater than 0 and less than or equal 100."));
            conditions.Add("Bitmap", (() => !(Bitmap is null), "Load a BitmapSource."));
        }

        protected override bool CanOK(IDialogWindow w)
        {
            return conditions.Values.All(o => o.Condition());
        }
    }
}
