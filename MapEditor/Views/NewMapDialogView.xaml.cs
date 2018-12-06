using Microsoft.Win32;
using System;
using System.IO;
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
                BitmapImage bitmap = new BitmapImage();
                try
                {
                    // https://docs.microsoft.com/en-us/dotnet/api/system.windows.media.imaging.bitmapimage.cacheoption?view=netframework-4.7.2
                    // Remarks: "Set the CacheOption to BitmapCacheOption.OnLoad if you wish to close a stream used to create the BitmapImage."
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;              // Might have to review this eg. is the cache ever refreshed?
                    bitmap.StreamSource = File.OpenRead(fileDialog.FileName);
                    bitmap.EndInit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                finally
                {
                    // Attempt to close the stream whatever happens otherwise the app keeps a handle on the file even if there was an excepion
                    // CacheOption = OnLoad => image was cached into memory at initialization
                    bitmap.StreamSource?.Close();
                }
                // Success
                Bitmap = bitmap;
                tbkFileName.Text = fileDialog.FileName;
            }
        }
    }
}
