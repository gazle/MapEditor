﻿using MapEditor.ViewModels;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MapEditor.Models
{
    class TileSheet : ViewModelBase
    {
        BitmapImage bitmap;
        public BitmapImage Bitmap { get => bitmap; set => SetField(ref bitmap, value); }

        int tileWidth;
        public int TileWidth { get { return tileWidth; } set { SetField(ref tileWidth, value); } }

        int tileHeight;
        public int TileHeight { get { return tileHeight; } set { SetField(ref tileHeight, value); } }

        public int SheetWidth => bitmap.PixelWidth / tileWidth;
        public int SheetHeight => bitmap.PixelHeight / tileHeight;
        public int TotalNumberOfTiles => SheetWidth * SheetHeight;

        public TileSheet(int tileWidth, int tileHeight, BitmapImage bitmap)
        {
            Bitmap = bitmap;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
        }

        public Int32Rect GetRectangleFromTileNumber(int tileNumber)
        {
            if (tileNumber < 0 || tileNumber > TotalNumberOfTiles)
                throw new ArgumentOutOfRangeException("tileNumber");
            return new Int32Rect(tileNumber % SheetWidth * tileWidth, tileNumber / SheetWidth * tileHeight, tileWidth, tileHeight);
        }

        public int GetTileNumberFromPoint(int x, int y)
        {
            int tx = x % tileWidth;
            int ty = y % tileHeight;
            if (tx < 0 || tx >= SheetWidth || ty < 0 || ty >= SheetHeight)
                throw new ArgumentOutOfRangeException("x, y");
            return ty * SheetWidth + tx;
        }
    }
}
