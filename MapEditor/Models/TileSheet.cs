using MapEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MapEditor.Models
{
    public class TileSheet : ViewModelBase
    {
        BitmapImage bitmap;
        public BitmapImage Bitmap { get => bitmap; set => SetField(ref bitmap, value); }

        int tileWidth = 32;
        public int TileWidth { get { return tileWidth; } set { SetField(ref tileWidth, value); } }

        int tileHeight = 32;
        public int TileHeight { get { return tileHeight; } set { SetField(ref tileHeight, value); } }

        public void LoadFromFile(string filename)
        {
            Bitmap = new BitmapImage(new Uri(filename));
        }
    }
}
