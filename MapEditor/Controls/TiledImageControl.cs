using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MapEditor.Controls
{
    [TemplatePart(Name = "PART_Image", Type = typeof(Image))]
    [TemplatePart(Name = "PART_Cursor", Type = typeof(Rectangle))]
    class TiledImageControl : ContentControl
    {
        static TiledImageControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TiledImageControl), new FrameworkPropertyMetadata(typeof(TiledImageControl)));
        }

        #region Source
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(BitmapImage), typeof(TiledImageControl), new PropertyMetadata(null, SourceChanged));
        public BitmapImage Source
        {
            get { return (BitmapImage)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        private static void SourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.ClearValue(CursorXProperty);
            d.ClearValue(CursorYProperty);
            d.CoerceValue(CursorWidthProperty);
            d.CoerceValue(CursorHeightProperty);
            ((TiledImageControl)d).RecalcSelectedTileNumber();
        }
        #endregion Source

        #region CursorX in units of CursorWidth
        public static readonly DependencyProperty CursorXProperty =
            DependencyProperty.Register("CursorX", typeof(int), typeof(TiledImageControl), new PropertyMetadata(0, CursorXChanged, CoerceCursorX));
        public int CursorX
        {
            get { return (int)GetValue(CursorXProperty); }
            set { SetValue(CursorXProperty, value); }
        }

        private static object CoerceCursorX(DependencyObject d, object value)
        {
            TiledImageControl tic = (TiledImageControl)d;
            int cursorX = (int)value;
            if (cursorX < 0)
                cursorX = 0;
            int div = tic.CursorWidth > 0 ? (tic.Source?.PixelWidth ?? 0) / tic.CursorWidth : 0;
            if ((cursorX + 1) * tic.CursorWidth >= (tic.Source?.PixelWidth ?? 0))
                cursorX = div - 1;        // Maximum CursorX can be
            return cursorX;
        }

        private static void CursorXChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((TiledImageControl)d).RecalcSelectedTileNumber();
        }
        #endregion CursorX

        #region CursorY in units of CursorHeight
        public static readonly DependencyProperty CursorYProperty =
            DependencyProperty.Register("CursorY", typeof(int), typeof(TiledImageControl), new PropertyMetadata(0, CursorYChanged, CoerceCursorY));
        public int CursorY
        {
            get { return (int)GetValue(CursorYProperty); }
            set { SetValue(CursorYProperty, value); }
        }

        private static object CoerceCursorY(DependencyObject d, object value)
        {
            TiledImageControl tic = (TiledImageControl)d;
            int cursorY = (int)value;
            if (cursorY < 0)
                cursorY = 0;
            int div = tic.CursorHeight > 0 ? (tic.Source?.PixelHeight ?? 0) / tic.CursorHeight : 0;
            if ((cursorY + 1) * tic.CursorHeight >= (tic.Source?.PixelHeight ?? 0))
                cursorY = div - 1;        // Maximum CursorY can be
            return cursorY;
        }

        private static void CursorYChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((TiledImageControl)d).RecalcSelectedTileNumber();
        }
        #endregion CursorY

        #region CursorWidth
        public static readonly DependencyProperty CursorWidthProperty =
            DependencyProperty.Register("CursorWidth", typeof(int), typeof(TiledImageControl), new PropertyMetadata(0, CursorWidthChanged, CoerceCursorWidth));

        public int CursorWidth
        {
            get { return (int)GetValue(CursorWidthProperty); }
            set { SetValue(CursorWidthProperty, value); }
        }

        private static object CoerceCursorWidth(DependencyObject d, object value)
        {
            TiledImageControl tic = (TiledImageControl)d;
            int cursorWidth = (int)value;
            if (cursorWidth < 0)
                cursorWidth = 0;
            if (cursorWidth > (tic.Source?.PixelWidth ?? 0))
                cursorWidth = tic.Source?.PixelWidth ?? 0;
            return cursorWidth;
        }

        private static void CursorWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.CoerceValue(CursorXProperty);
            ((TiledImageControl)d).RecalcSelectedTileNumber();
        }
        #endregion CursorWidth

        #region CursorHeight
        public static readonly DependencyProperty CursorHeightProperty =
            DependencyProperty.Register("CursorHeight", typeof(int), typeof(TiledImageControl), new PropertyMetadata(0, CursorHeightChanged, CoerceCursorHeight));

        public int CursorHeight
        {
            get { return (int)GetValue(CursorHeightProperty); }
            set { SetValue(CursorHeightProperty, value); }
        }

        private static object CoerceCursorHeight(DependencyObject d, object value)
        {
            TiledImageControl tic = (TiledImageControl)d;
            int cursorHeight = (int)value;
            if (cursorHeight < 0)
                cursorHeight = 0;
            if (cursorHeight > (tic.Source?.PixelHeight ?? 0))
                cursorHeight = tic.Source?.PixelHeight ?? 0;
            return cursorHeight;
        }

        private static void CursorHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.CoerceValue(CursorYProperty);
            ((TiledImageControl)d).RecalcSelectedTileNumber();
        }
        #endregion CursorHeight

        #region SelectedTileNumber
        internal static readonly DependencyPropertyKey SelectedTileNumberKey =
            DependencyProperty.RegisterReadOnly("SelectedTileNumber", typeof(int), typeof(TiledImageControl), new PropertyMetadata(0));
        public static readonly DependencyProperty SelectedTileNumberProperty = SelectedTileNumberKey.DependencyProperty;

        public int SelectedTileNumber
        {
            get { return (int)GetValue(SelectedTileNumberProperty); }
            private set { SetValue(SelectedTileNumberKey, value); }
        }

        void RecalcSelectedTileNumber()
        {
            int cursorWidths = CursorWidth > 0 ? (Source?.PixelWidth ?? 0) / CursorWidth : 0;
            SelectedTileNumber = CursorY * cursorWidths + CursorX;
        }
        #endregion SelectedTileNumber

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image image = (Image)sender;
            Point pos = e.GetPosition(image);
            Point bitmapPos = new Point
            {
                X = Math.Floor(pos.X * ((BitmapImage)image.Source).PixelWidth / image.ActualWidth),
                Y = Math.Floor(pos.Y * ((BitmapImage)image.Source).PixelHeight / image.ActualHeight)
            };
            CursorX = (int)bitmapPos.X / CursorWidth;
            CursorY = (int)bitmapPos.Y / CursorHeight;
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
