using MapEditor.Dialogs;
using MapEditor.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MapEditor.ViewModels
{
    class ViewModel : ViewModelBase
    {
        //readonly IDialogService dialogService;
        readonly IFileDialogService fileDialogService;
        public ICommand YesNoCommand { get; private set; }
        public ICommand AlertCommand { get; private set; }
        public ICommand FileDialogCommand { get; private set; }
        public ICommand BlitCommand { get; private set; }
        public TileSheet TileSheet { get; set; } = new TileSheet();
        public TiledMap Map { get; set; } = new TiledMap();
        public WriteableBitmap Bitmap { get; set; }

        public ViewModel()
        {
            //dialogService = new DialogService<Views.DialogWindow>();
            fileDialogService = new FileDialogService<Views.FileDialogView>();
            FileDialogCommand = new DelegateCommand(FileDialog);
            BlitCommand = new DelegateCommand(Blit);
            Bitmap = new WriteableBitmap(Map.Width * TileSheet.TileWidth, Map.Height * TileSheet.TileHeight, 96, 96, PixelFormats.Bgra32, null);
        }

        public void Blit()
        {
            for (int row = 0; row < 15; row++)
                for (int col = 0; col < 15; col++)
                    BlitTile(row, col, row, col);
        }

        public void BlitTile(int sourceRow, int sourceCol, int destRow, int destCol)
        {
            int stride = TileSheet.TileWidth * ((Bitmap.Format.BitsPerPixel + 7) / 8);
            byte[] data = new byte[TileSheet.TileHeight * stride];
            Int32Rect sourceRect;
            sourceRect = new Int32Rect(sourceCol * TileSheet.TileWidth, sourceRow * TileSheet.TileHeight, TileSheet.TileWidth, TileSheet.TileHeight);
            int offset = destRow * TileSheet.TileHeight * stride + destCol * TileSheet.TileWidth * 4;
            TileSheet.Bitmap.CopyPixels(sourceRect, data, stride, 0);
            sourceRect = new Int32Rect(sourceCol * TileSheet.TileWidth, sourceRow * TileSheet.TileHeight, TileSheet.TileWidth, TileSheet.TileHeight);
            Bitmap.Lock();
            Bitmap.WritePixels(sourceRect, data, stride, 0);
            Bitmap.AddDirtyRect(sourceRect);
            Bitmap.Unlock();
        }

        void FileDialog()
        {
            var filename = fileDialogService.OpenFileDialog("");
            TileSheet.LoadFromFile(filename);
        }
    }
}
