using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace MapEditor.Views
{
    /// <summary>
    /// Interaction logic for NewMapDialogView.xaml
    /// </summary>
    public partial class NewMapDialogView : UserControl
    {
        // Using a DependencyProperty as the backing store for Bitmap.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BitmapProperty =
            DependencyProperty.Register("Bitmap", typeof(BitmapImage), typeof(NewMapDialogView), new PropertyMetadata(null));
        public BitmapImage Bitmap
        {
            get { return (BitmapImage)GetValue(BitmapProperty); }
            set { SetValue(BitmapProperty, value); }
        }

        public NewMapDialogView()
        {
            InitializeComponent();
            Loaded += NewMapDialogView_Loaded;
        }

        private void NewMapDialogView_Loaded(object sender, RoutedEventArgs e)
        {
            Binding binding = new Binding("Bitmap") { Mode = BindingMode.OneWayToSource, ValidatesOnDataErrors = true };
            // Leave binding.Source as null, DataContext will be inherited in the visual tree
            SetBinding(BitmapProperty, binding);
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            bool? result = fileDialog.ShowDialog();
            if (result == true)
            {
                try
                {
                    Bitmap = new BitmapImage(new Uri(fileDialog.FileName));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                tbkFileName.Text = fileDialog.FileName;
            }
        }
    }
}
