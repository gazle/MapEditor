using MapEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor.Models
{
    class TiledMap : ViewModelBase
    {
        TileSheet tileSheet;
        public TileSheet TileSheet { get { return tileSheet; } set { SetField(ref tileSheet, value); } }

        int width = 16;
        public int Width { get { return width; } set { SetField(ref width, value); } }

        int height = 16;
        public int Height { get { return height; } set { SetField(ref height, value); } }

        public List<List<int>> Tiles { get; set; }

        public TiledMap()
        {
            Tiles = new List<List<int>>();
            for (int row = 0; row < height; row++)
            {
                var sheetRow = new List<int>();
                Tiles.Add(sheetRow);
                for (int col = 0; col < width; col++)
                {
                    sheetRow.Add(7);
                }
            }
            Tiles[0][3] = 7;
        }
    }
}
