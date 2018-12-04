using MapEditor.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace MapEditor.Controls
{
    [TemplatePart(Name = "PART_Image", Type = typeof(Image))]
    [TemplatePart(Name = "PART_Rectangle", Type = typeof(Rectangle))]
    class TiledImageControl : ContentControl
    {
        static TiledImageControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TiledImageControl), new FrameworkPropertyMetadata(typeof(TiledImageControl)));
        }
        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(TileSheet), typeof(TiledImageControl), new PropertyMetadata(null));
        public TileSheet Source
        {
            get { return (TileSheet)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedRow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedRowProperty =
            DependencyProperty.Register("SelectedRow", typeof(int), typeof(TiledImageControl), new PropertyMetadata(0));
        public int SelectedRow
        {
            get { return (int)GetValue(SelectedRowProperty); }
            set { SetValue(SelectedRowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedColumn.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedColumnProperty =
            DependencyProperty.Register("SelectedColumn", typeof(int), typeof(TiledImageControl), new PropertyMetadata(0));
        public int SelectedColumn
        {
            get { return (int)GetValue(SelectedColumnProperty); }
            set { SetValue(SelectedColumnProperty, value); }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image image = (Image)sender;
            Point pos = e.GetPosition(image);
            SelectedRow = (int)pos.Y / Source.TileHeight;
            SelectedColumn = (int)pos.X / Source.TileWidth;
        }

        public override void OnApplyTemplate()
        {
            // WPFu4 page 744
            base.OnApplyTemplate();
            if (GetTemplateChild("PART_Image") is Image image)
                image.MouseDown += Image_MouseDown;
        }
    }
}
