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
            
            /*nombreBox.textBox.Text = publication.name;
            categoriaBox.textBox.Text = publication.category;
            distancaiBox.textBox.Text = publication.distance;
            dificultadBox.textBox.Text = publication.difficulty;
            duracionBox.textBox.Text = publication.duration;
            descripcionBox.textBox.Text = publication.description;
            privacidadBox.textBox.Text = publication.privacy;*/
        }

        public async void SendPublication(object sender, RoutedEventArgs e)
        {
            /*
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
                        name = nombreBox.textBox.Text,
                        category = categoriaBox.textBox.Text,
                        distance = distancaiBox.textBox.Text,
                        difficulty = dificultadBox.textBox.Text,
                        duration = duracionBox.textBox.Text,
                        description = descripcionBox.textBox.Text,
                        privacy = privacidadBox.textBox.Text,
                    }),
                    Encoding.UTF8,
                    "application/json");
                await PublicationsProcessor.UpdatePublication(publication._id, jsonContent);
                Utils.Utilities.GoToPublications(sender, e);
                this.Close();
            }
            */
        }
    }
}
