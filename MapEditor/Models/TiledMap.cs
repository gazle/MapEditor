using MapEditor.ViewModels;
using System.Collections.Generic;

namespace MapEditor.Models
{
    class TiledMap : ViewModelBase
    {
        TileSheet tileSheet;
        public TileSheet TileSheet { get { return tileSheet; } set { SetField(ref tileSheet, value); } }

        int width;
        public int Width { get { return width; } set { SetField(ref width, value); } }

        int height;
        public int Height { get { return height; } set { SetField(ref height, value); } }

        public List<List<int>> Tiles { get; set; }

        public TiledMap(int width, int height, TileSheet tileSheet)
        {
            Width = width;
            Height = height;
            TileSheet = tileSheet;
            Tiles = new List<List<int>>();
            for (int row = 0; row < height; row++)
            {
                var sheetRow = new List<int>();
                Tiles.Add(sheetRow);
                for (int col = 0; col < width; col++)
                {
                    sheetRow.Add(0);
                }
            }
        }
    }
}
