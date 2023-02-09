using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Drawing;
using System.Drawing.Imaging;
using System;
using Microsoft.Win32;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.InteropServices;
using Image = System.Drawing.Image;

namespace PantallaGestionUsuarios.Views
{
    /// <summary>
    /// Lógica de interacción para PutFormUsers.xaml
    /// </summary>
    public partial class PutFormUsers : Window
    {
        private UserModel user { get; set; }
        public PutFormUsers(UserModel user)
        {
            InitializeComponent();
            this.user = user;
        }

        public async void LoadUserPhoto()
        {
            if (user.photo != null && user.photo.Length > 0)
            {
                profilePictureImage.Source = new BitmapImage(new Uri("http://localhost:8080/profilePicture/" + user.photo));
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private void FillData(object sender, RoutedEventArgs e)
        {
            nombreBox.textBox.Text = user.name;
            apellidosBox.textBox.Text = user.lastname;
            fechaBox.textBox.Text = user.date;
            LoadUserPhoto();
        }

        public async void SendUser(object sender, RoutedEventArgs e)
        {
            bool canUpdate = true;
            if (nombreBox.textBox.Text.Length == 0)
            {
                canUpdate = false;
                nombreBox.textBox.BorderBrush = System.Windows.Media.Brushes.Red;
                nombreBox.textBox.BorderThickness = new Thickness(1.5f);
            }

            if (canUpdate)
            {
                if (profilePictureImage.Source.ToString() != "defaulProfilePicture.png")
                {
                    var ms = new MemoryStream();
                    BitmapSource bmp = (BitmapSource)profilePictureImage.Source;
                    Bitmap bitm = ConvertToBitmap(bmp);


                    Image img = (Image)bitm;
                    var imageStream = ToStream(img, ImageFormat.Png);
                    await UserProcessor.PostPhoto(imageStream, img.RawFormat.ToString(), user.email);
                }

                nombreBox.textBox.BorderThickness = new Thickness(0);
                using StringContent jsonContent = new(
                    JsonSerializer.Serialize(new
                    {
                        nombre = nombreBox.textBox.Text,
                        apellidos = apellidosBox.textBox.Text,
                        fecha = fechaBox.textBox.Text,
                    }),
                    Encoding.UTF8,
                    "application/json");
                await UserProcessor.UpdateUser(user._id, jsonContent);
                Utils.Utilities.GoToUsers(sender, e);
                this.Close();
            }
        }

        public static ImageFormat ConvertStringToImageFormat(string imageFormat)
        {
            switch(imageFormat)
            {
                case "png":
                    return ImageFormat.Png;
                    break;
                case "jpeg":
                    return ImageFormat.Jpeg;
                default: return ImageFormat.Bmp;
            }
        }

        public static Stream ToStream(System.Drawing.Image image, ImageFormat format)
        {
            var stream = new System.IO.MemoryStream();
            MessageBox.Show(format.ToString());
            image.Save(stream, format);
            stream.Position = 0;
            return stream;
        }

        public static Bitmap ConvertToBitmap(BitmapSource bitmapSource)
        {
            var width = bitmapSource.PixelWidth;
            var height = bitmapSource.PixelHeight;
            var stride = width * ((bitmapSource.Format.BitsPerPixel + 7) / 8);
            var memoryBlockPointer = Marshal.AllocHGlobal(height * stride);
            bitmapSource.CopyPixels(new Int32Rect(0, 0, width, height), memoryBlockPointer, height * stride, stride);
            var bitmap = new Bitmap(width, height, stride, PixelFormat.Format32bppPArgb, memoryBlockPointer);
            return bitmap;
        }



        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                profilePictureImage.Source = new BitmapImage(new Uri(op.FileName));
            }
        }

        void nombreBoxTextChange(object sender, TextChangedEventArgs args)
        {
            if (nombreBox.textBox.Text.Length > 0)
            {
                nombreBox.textBox.BorderThickness = new Thickness(0);
            }
        }
    }
}
