using PantallaGestionUsuarios.Api;
using PantallaGestionUsuarios.Models;
using PantallaGestionUsuarios.Utils;
using PantallaGestionUsuarios.Views.Error;
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
        private PublicationModel publication { get; set; }
        private string[] photos { get; set; }
        private int photo1Number { get; set; }
        private int photo2Number { get; set; }
        private int photo3Number { get; set; }
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
        private async void FillData(object sender, RoutedEventArgs e)
        {
            nombreBox.Text = publication.name;
            categoriaBox.Text = publication.category;   
            distanciaBox.Text = publication.distance;
            dificultadBox.Text = publication.difficulty;
            duracionBox.Text = publication.duration;
            descripcionBox.Text = publication.description;
            privacidadBox.Text = publication.privacy;
            photos = await PublicationsProcessor.GetNumberOfPhotos(publication._id);
            loadPhotos();
        }

        private void loadPhotos()
        {
            if(photos == null)
            {
                photo1.Visibility = Visibility.Collapsed;
                photo2.Visibility = Visibility.Collapsed;
                photo3.Visibility = Visibility.Collapsed;
                botonDerecho.Visibility = Visibility.Collapsed;
                botonIzquierda.Visibility = Visibility.Collapsed;
                return;
            }
 
            if(photos.Length == 1)
            {
               photo2.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publication._id + "/" + photos[0]));
            }
            else if (photos.Length == 2)
            {
                photo3.Visibility= Visibility.Collapsed;
                botonDerecho.Visibility = Visibility.Collapsed;
                botonIzquierda.Visibility = Visibility.Collapsed;
                photo1.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publication._id + "/" + photos[0]));
                photo2.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publication._id + "/" + photos[1]));
            }
            else if (photos.Length >= 3)
            {
                photo1Number = 0;
                photo2Number = 1; 
                photo3Number = 2;

                photo1.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publication._id + "/" + photos[0]));
                photo2.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publication._id + "/" + photos[1]));
                photo3.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publication._id + "/" + photos[2]));
            }
        }

        private void izquierda(object sender, RoutedEventArgs e)
        {
            if (photos == null || photos.Length < 3)
            {
                return;
            }

            if (photo2Number == 0)
            {
                photo1Number--;
                photo2Number = photos.Length -1;
                photo3Number = 0;
                photo1.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publication._id + "/" + photos[photo1Number]));
                photo2.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publication._id + "/" + photos[photo2Number]));
                photo3.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publication._id + "/" + photos[photo3Number]));

            }
            else if (photo2Number == 1)
            {
                photo1Number = photos.Length - 1;
                photo2Number--;
                photo3Number = 1;
                photo1.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publication._id + "/" + photos[photo1Number]));
                photo2.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publication._id + "/" + photos[photo2Number]));
                photo3.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publication._id + "/" + photos[photo3Number]));
            }
            else if (photo2Number == photos.Length - 1)
            {
                photo1Number--;
                photo2Number--;
                photo3Number = photos.Length - 1;
                photo1.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publication._id + "/" + photos[photo1Number]));
                photo2.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publication._id + "/" + photos[photo2Number]));
                photo3.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publication._id + "/" + photos[photo3Number]));
            }
            else
            {
                photo1Number--;
                photo2Number--;
                photo3Number--;
                photo1.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publication._id + "/" + photos[photo1Number]));
                photo2.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publication._id + "/" + photos[photo2Number]));
                photo3.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publication._id + "/" + photos[photo3Number]));
            }

        }

        private void derecha(object sender, RoutedEventArgs e)
        {
            if (photos == null)
            {
                return;
            }

            if (photo2Number == 0)
            {
                photo1Number = 0;
                photo2Number++;
                photo3Number++;
                photo1.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publication._id + "/" + photos[photo1Number]));
                photo2.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publication._id + "/" + photos[photo2Number]));
                photo3.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publication._id + "/" + photos[photo3Number]));

            }
            else if (photo2Number == 1)
            {
                photo1Number =1;
                photo2Number++;
                photo3Number++;
                photo1.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publication._id + "/" + photos[photo1Number]));
                photo2.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publication._id + "/" + photos[photo2Number]));
                photo3.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publication._id + "/" + photos[photo3Number]));
            }
            else if (photo2Number == photos.Length - 2)
            {
                photo1Number++;
                photo2Number = photos.Length - 1;
                photo3Number = 0;
                photo1.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publication._id + "/" + photos[photo1Number]));
                photo2.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publication._id + "/" + photos[photo2Number]));
                photo3.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publication._id + "/" + photos[photo3Number]));
            }
            else if (photo2Number == photos.Length - 1)
            {
                photo1Number = photos.Length - 1;
                photo2Number=0;
                photo3Number=1;
                photo1.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publication._id + "/" + photos[photo1Number]));
                photo2.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publication._id + "/" + photos[photo2Number]));
                photo3.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publication._id + "/" + photos[photo3Number]));
            }
            else
            {
                photo1Number++;
                photo2Number++;
                photo3Number++;
                photo1.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publication._id + "/" + photos[photo1Number]));
                photo2.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publication._id + "/" + photos[photo2Number]));
                photo3.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publication._id + "/" + photos[photo3Number]));
            }
        }

        public async void SendPublication(object sender, RoutedEventArgs e)
        {
            
            bool canUpdate = true;
            if (nombreBox.Text.Length == 0)
            {
                MessageBox.Show(dificultadBox.Text);
                canUpdate = false;
                nombreBox.BorderBrush = System.Windows.Media.Brushes.Red;
                nombreBox.BorderThickness = new Thickness(1.5f);
            }
             if (distanciaBox.Text.Length == 0)
            {
                canUpdate = false;
                distanciaBox.BorderBrush = System.Windows.Media.Brushes.Red;
                distanciaBox.BorderThickness = new Thickness(1.5f);
            }
             if (duracionBox.Text.Length == 0)
            {
                canUpdate = false;
                duracionBox.BorderBrush = System.Windows.Media.Brushes.Red;
                duracionBox.BorderThickness = new Thickness(1.5f);
            }
            if (descripcionBox.Text.Length == 0)
            {
                canUpdate = false;
                descripcionBox.BorderBrush = System.Windows.Media.Brushes.Red;
                descripcionBox.BorderThickness = new Thickness(1.5f);
            }
            if (dificultadBox.Text.Length == 0)
            {
                canUpdate = false;
                dificultadBox.BorderBrush = System.Windows.Media.Brushes.Red;
                dificultadBox.BorderThickness = new Thickness(1.5f);
            }
            if (privacidadBox.Text.Length == 0)
            {
                canUpdate = false;
                privacidadBox.BorderBrush = System.Windows.Media.Brushes.Red;
                privacidadBox.BorderThickness = new Thickness(1.5f);
            }
            if (canUpdate)
            {
                using StringContent jsonContent = new(
                    JsonSerializer.Serialize(new
                    {
                        name = nombreBox.Text,
                        category = categoriaBox.Text,
                        distance = distanciaBox.Text,
                        difficulty = dificultadBox.Text,
                        duration = duracionBox.Text,
                        description = descripcionBox.Text,
                        privacy = privacidadBox.Text,
                    }),
                    Encoding.UTF8,
                    "application/json");
                await PublicationsProcessor.UpdatePublication(publication._id, jsonContent);
                Utils.Utilities.GoToPublications(sender, e);
                this.Close();
            }
        }
    }
}
