using PantallaGestionUsuarios.Api;
using PantallaGestionUsuarios.Models;
using PantallaGestionUsuarios.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PantallaGestionUsuarios.Views
{
    /// <summary>
    /// Lógica de interacción para PutFormPublications.xaml
    /// </summary>
    public partial class PutFormPublications : Window
    {
        private int centralImageN;
        private int leftImageN;
        private int rightImageN;
        private PublicationModel publication { get; set; }
        public PutFormPublications(PublicationModel publication)
        {
            InitializeComponent();
            this.publication = publication;

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
            nombreBox.textBox.Text = publication.nombre;
            categoriaBox.textBox.Text = publication.categoria;
            distancaiBox.textBox.Text = publication.distancia;
            dificultadBox.textBox.Text = publication.dificultad;
            duracionBox.textBox.Text = publication.duracion;
            descripcionBox.textBox.Text = publication.descripcion;
            privacidadBox.textBox.Text = publication.privacidad;

            if(publication.foto.Length >= 3)
            {
                leftImageN = 0;
                centralImageN = 1;
                rightImageN = 2;
                routePicture.Source = Base64ToImage(publication.foto[centralImageN]);
                rightImage.Source = Base64ToImage(publication.foto[rightImageN]);
                leftImage.Source = Base64ToImage(publication.foto[leftImageN]);
            }
        }

        public BitmapImage Base64ToImage(string base64String)
        {
            if (base64String == null)
            {
                leftButton.Visibility = Visibility.Collapsed;
                rightButton.Visibility = Visibility.Collapsed;
                return null;
            }

            byte[] binaryData = Convert.FromBase64String(base64String);

            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = new MemoryStream(binaryData);

            bi.EndInit();

            return bi;
        }

        public FormatConvertedBitmap Base64ToImageGrey(string base64String)
        {
            if (base64String == null)
            {
                leftButton.Visibility = Visibility.Collapsed;
                rightButton.Visibility = Visibility.Collapsed;
                return null;
            }

            byte[] binaryData = Convert.FromBase64String(base64String);

            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = new MemoryStream(binaryData);

            bi.EndInit();

            FormatConvertedBitmap grayBitmap = new FormatConvertedBitmap();

            grayBitmap.BeginInit();

            grayBitmap.Source = bi;

            grayBitmap.DestinationFormat = PixelFormats.Gray8;

            grayBitmap.EndInit();

            return grayBitmap;  
        }


        public async void SendPublication(object sender, RoutedEventArgs e)
        {
            bool canUpdate = true;
            if (nombreBox.textBox.Text.Length == 0)
            {
                canUpdate = false;
                nombreBox.textBox.BorderBrush = System.Windows.Media.Brushes.Red;
                nombreBox.textBox.BorderThickness = new Thickness(1.5f);
            }
             if (distancaiBox.textBox.Text.Length == 0)
            {
                canUpdate = false;
                distancaiBox.textBox.BorderBrush = System.Windows.Media.Brushes.Red;
                distancaiBox.textBox.BorderThickness = new Thickness(1.5f);
            }
             if (duracionBox.textBox.Text.Length == 0)
            {
                canUpdate = false;
                duracionBox.textBox.BorderBrush = System.Windows.Media.Brushes.Red;
                duracionBox.textBox.BorderThickness = new Thickness(1.5f);
            }
            if (descripcionBox.textBox.Text.Length == 0)
            {
                canUpdate = false;
                descripcionBox.textBox.BorderBrush = System.Windows.Media.Brushes.Red;
                descripcionBox.textBox.BorderThickness = new Thickness(1.5f);
            }
            if(canUpdate)
            {
                using StringContent jsonContent = new(
                    JsonSerializer.Serialize(new
                    {
                        nombre = nombreBox.textBox.Text,
                        categoria = categoriaBox.textBox.Text,
                        distancia = distancaiBox.textBox.Text,
                        dificultad = dificultadBox.textBox.Text,
                        duracion = duracionBox.textBox.Text,
                        descripcion = descripcionBox.textBox.Text,
                        privacidad = privacidadBox.textBox.Text,
                    }),
                    Encoding.UTF8,
                    "application/json");
                await PublicationsProcessor.UpdatePublication(publication._id, jsonContent);
                Utils.Utilities.GoToPublications(sender, e);
                this.Close();
            }
        }

        private void buttonLeftClick(object sender, RoutedEventArgs e)
        {
            if(publication.foto.Length >= 2)
            {
                if (centralImageN == 0)
                {
                    leftImageN = leftImageN -1;
                    centralImageN = publication.foto.Length - 1;
                    rightImageN = 0;
                    routePicture.Source = Base64ToImage(publication.foto[centralImageN]);
                    leftImage.Source = Base64ToImageGrey(publication.foto[leftImageN]);
                    rightImage.Source = Base64ToImageGrey(publication.foto[rightImageN]);

                }
                else if (centralImageN == 1)
                {
                    leftImageN = publication.foto.Length - 1;
                    centralImageN = centralImageN - 1;
                    rightImageN = 1;
                    routePicture.Source = Base64ToImage(publication.foto[centralImageN]);
                    leftImage.Source = Base64ToImageGrey(publication.foto[leftImageN]);
                    rightImage.Source = Base64ToImageGrey(publication.foto[rightImageN]);

                }
                else if (centralImageN == publication.foto.Length-2)
                {
                    leftImageN = leftImageN - 1;
                    centralImageN = centralImageN - 1;
                    rightImageN = rightImageN - 1;
                    routePicture.Source = Base64ToImage(publication.foto[centralImageN]);
                    leftImage.Source = Base64ToImage(publication.foto[leftImageN]);
                    rightImage.Source = Base64ToImage(publication.foto[rightImageN]);

                }
                else if (centralImageN == publication.foto.Length - 1)
                {
                    leftImageN = leftImageN - 1;
                    centralImageN = centralImageN - 1;
                    rightImageN = publication.foto.Length - 1;
                    routePicture.Source = Base64ToImage(publication.foto[centralImageN]);
                    leftImage.Source = Base64ToImage(publication.foto[leftImageN]);
                    rightImage.Source = Base64ToImage(publication.foto[rightImageN]);

                }
                else
                {
                    centralImageN = centralImageN-1;
                    leftImageN = leftImageN - 1;
                    rightImageN = rightImageN -1;
                    routePicture.Source = Base64ToImage(publication.foto[centralImageN]);
                    leftImage.Source = Base64ToImage(publication.foto[leftImageN]);
                    rightImage.Source = Base64ToImage(publication.foto[rightImageN]);
                }

            }
        }

        private void nombreBoxTextChange(object sender, TextChangedEventArgs args)
        {
            if (nombreBox.textBox.Text.Length > 0)
            {
                nombreBox.textBox.BorderThickness = new Thickness(0);
            }
        }
        private void distanciaBoxTextChange(object sender, TextChangedEventArgs args)
        {
            if (distancaiBox.textBox.Text.Length > 0)
            {
                distancaiBox.textBox.BorderThickness = new Thickness(0);
            }
        }
        private void duracionBoxTextChange(object sender, TextChangedEventArgs args)
        {
            if (duracionBox.textBox.Text.Length > 0)
            {
                duracionBox.textBox.BorderThickness = new Thickness(0);
            }
        }

        private void descripcionBoxTextChange(object sender, TextChangedEventArgs args)
        {
            if (descripcionBox.textBox.Text.Length > 0)
            {
                descripcionBox.textBox.BorderThickness = new Thickness(0);
            }
        }

        private void buttonRightClick(object sender, RoutedEventArgs e)
        {
            if (publication.foto.Length >= 2)
            {
                if (centralImageN == 0)
                {
                    leftImageN = 0; // publication.foto.Length - 1;
                    centralImageN = centralImageN+1;
                    rightImageN = rightImageN+1;
                    routePicture.Source = Base64ToImage(publication.foto[centralImageN]);
                    leftImage.Source = Base64ToImage(publication.foto[leftImageN]);
                    rightImage.Source = Base64ToImage(publication.foto[rightImageN]);

                }
                else if (centralImageN == 1)
                {
                    leftImageN = leftImageN+1;
                    centralImageN = centralImageN + 1;
                    rightImageN = rightImageN+1;
                    routePicture.Source = Base64ToImage(publication.foto[centralImageN]);
                    leftImage.Source = Base64ToImage(publication.foto[leftImageN]);
                    rightImage.Source = Base64ToImage(publication.foto[rightImageN]);

                }
                else if (centralImageN == publication.foto.Length - 2)
                {
                    leftImageN = leftImageN + 1;
                    centralImageN = centralImageN+1;
                    rightImageN =0 ;
                    routePicture.Source = Base64ToImage(publication.foto[centralImageN]);
                    leftImage.Source = Base64ToImage(publication.foto[leftImageN]);
                    rightImage.Source = Base64ToImage(publication.foto[rightImageN]);

                }
                else if (centralImageN == publication.foto.Length - 1)
                {
                    leftImageN = publication.foto.Length - 1;
                    centralImageN =0;
                    rightImageN = rightImageN + 1;
                    routePicture.Source = Base64ToImage(publication.foto[centralImageN]);
                    leftImage.Source = Base64ToImage(publication.foto[leftImageN]);
                    rightImage.Source = Base64ToImage(publication.foto[rightImageN]);

                }
                else
                {
                    centralImageN = centralImageN + 1;
                    leftImageN = leftImageN + 1;
                    rightImageN = rightImageN + 1;
                    routePicture.Source = Base64ToImage(publication.foto[centralImageN]);
                    leftImage.Source = Base64ToImage(publication.foto[leftImageN]);
                    rightImage.Source = Base64ToImage(publication.foto[rightImageN]);
                }

            }
        }
    }
}
