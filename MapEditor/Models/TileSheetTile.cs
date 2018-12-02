namespace MapEditor.Models
{
    class TileSheetTile : ViewModels.ViewModelBase
    {
        int tileID;
        public int TileID { get { return tileID; } set { SetField(ref tileID, value); } }

        public readonly int Row;
        public readonly int Col;

        public TileSheetTile(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }
}
