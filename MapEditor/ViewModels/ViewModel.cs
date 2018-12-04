using MapEditor.Dialogs;
using MapEditor.Models;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MapEditor.ViewModels
{
    class ViewModel : ViewModelBase
    {
        readonly IDialogService dialogService;
        readonly IFileDialogService fileDialogService;

        public ICommand YesNoCommand { get; private set; }
        public ICommand AlertCommand { get; private set; }
        public ICommand FileDialogCommand { get; private set; }
        public ICommand NewMapCommand { get; private set; }
        TiledMap map;
        public TiledMap Map { get { return map; } set { SetField(ref map, value); } }
        WriteableBitmap mapRepresentation;
        public WriteableBitmap MapRepresentation { get { return mapRepresentation; } set { SetField(ref mapRepresentation, value); } }

        public ViewModel()
        {
            // Normally use dependency injection
            dialogService = new DialogService<Views.DialogWindow>();
            fileDialogService = new FileDialogService<Views.FileDialogView>();

            FileDialogCommand = new DelegateCommand(FileDialog);
            NewMapCommand = new DelegateCommand(NewMapDialog);
        }

        public void BlitTile(BitmapImage sourceBitmap, WriteableBitmap targetBitmap, Int32Rect sourceRect, Int32Rect targetRect)
        {
            int stride = (sourceRect.Width * sourceBitmap.Format.BitsPerPixel + 7) / 8;
            byte[] data = new byte[sourceRect.Height * stride];
            sourceBitmap.CopyPixels(sourceRect, data, stride, 0);
            targetBitmap.Lock();
            targetBitmap.WritePixels(targetRect, data, stride, 0);
            targetBitmap.AddDirtyRect(targetRect);
            targetBitmap.Unlock();
        }

        void FileDialog()
        {
            var filename = fileDialogService.OpenFileDialog("");
        }

        void NewMapDialog()
        {
            var dialog = new NewMapDialogViewModel();
            var result = dialogService.OpenDialog(dialog);
            if (result == DialogResults.OK)
            {
                Map = new TiledMap(dialog.MapWidth, dialog.MapHeight, new TileSheet(dialog.TileWidth, dialog.TileHeight, dialog.Bitmap));
                CreateMapRepresentation(dialog.MapWidth, dialog.MapHeight, dialog.TileWidth, dialog.TileHeight);
            }
        }

        void CreateMapRepresentation(int mapWidth, int mapHeight, int tileWidth, int tileHeight)
        {
            MapRepresentation = new WriteableBitmap(mapWidth * tileWidth, mapHeight * tileHeight, 96, 96, PixelFormats.Bgra32, null);
            var sourceRect = Map.TileSheet.GetRectangleFromTileNumber(0);
            Int32Rect targetRect = new Int32Rect(0, 0, Map.TileSheet.TileWidth, Map.TileSheet.TileHeight);
            for (int row = 0; row < Map.Height; row++)
            {
                targetRect.Y = row * Map.TileSheet.TileHeight;
                for (int col = 0; col < Map.Width; col++)
                {
                    targetRect.X = col * Map.TileSheet.TileWidth;
                    BlitTile(Map.TileSheet.Bitmap, MapRepresentation, sourceRect, targetRect);
                }
            }
        }
    }
}
