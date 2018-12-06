using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace AttachedBehaviors
{
    static class Behavior
    {
        #region SaveCommandProperty
        static readonly DependencyProperty PlaceTileCommandProperty =
            DependencyProperty.RegisterAttached("PlaceTileCommand", typeof(ICommand), typeof(Behavior), new PropertyMetadata(PlaceTileCommandChanged));

        public static ICommand GetPlaceTileCommand(Image element)
        {
            return (ICommand)element.GetValue(PlaceTileCommandProperty);
        }

        public static void SetPlaceTileCommand(Image element, ICommand command)
        {
            element.SetValue(PlaceTileCommandProperty, command);
        }

        static void PlaceTileCommandChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (o is Image ele)
            {
                if (!(e.OldValue is null) && e.NewValue is null)
                    ele.MouseDown -= Element_Click;
                if (e.OldValue is null && !(e.NewValue is null))
                    ele.MouseDown += Element_Click;
            }
        }

        static readonly DependencyProperty PlaceTileCommandParameterProperty =
            DependencyProperty.RegisterAttached("PlaceTileCommandParameter", typeof(int), typeof(Behavior));

        public static int GetPlaceTileCommandParameter(Image element)
        {
            return (int)element.GetValue(PlaceTileCommandParameterProperty);
        }

        public static void SetPlaceTileCommandParameter(Image element, int value)
        {
            element.SetValue(PlaceTileCommandParameterProperty, value);
        }

        static void Element_Click(object sender, MouseButtonEventArgs e)
        {
            Image element = sender as Image;
            ICommand command = (ICommand)element.GetValue(PlaceTileCommandProperty);
            Point pos = e.GetPosition(element);
            Point bitmapPos = new Point
            {
                X = Math.Floor(pos.X * ((BitmapImage)element.Source).Width / element.ActualWidth),
                Y = Math.Floor(pos.Y * ((BitmapImage)element.Source).Height / element.ActualHeight)
            };
            (Point, int) tuple = (bitmapPos, (int)element.GetValue(PlaceTileCommandParameterProperty));
            command.Execute(tuple);
        }
        #endregion
    }
}
