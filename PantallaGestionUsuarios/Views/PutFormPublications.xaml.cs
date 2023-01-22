﻿using PantallaGestionUsuarios.Api;
using PantallaGestionUsuarios.Models;
using PantallaGestionUsuarios.Utils;
using System;
using System.Collections.Generic;
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

            publicationsButton.Click += GoToPublications;
            commentsButton.Click += GoToComments;
            usersButton.Click += GoToUsers;
        }

        private void GoToPublications(object sender, RoutedEventArgs e)
        {
            Utilities.GoToPublications(sender, e);
            this.Close();
        }
        private void GoToComments(object sender, RoutedEventArgs e)
        {
            Utilities.GoToComments(sender, e);
            this.Close();
        }
        private void GoToUsers(object sender, RoutedEventArgs e)
        {
            Utilities.GoToUsers(sender, e);
            this.Close();
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
            distancaiBox.textBox.Text = publication.distancia.ToString();
            dificultadBox.textBox.Text = publication.dificultad;
            duracionBox.textBox.Text = publication.duracion.ToString();
            descripcionBox.textBox.Text = publication.descripcion;
            fotoBox.textBox.Text = publication.foto;
            privacidadBox.textBox.Text = publication.privacidad;
            empresaBox.textBox.Text = publication.empresa;
            urlBox.textBox.Text = publication.url;
        }

        public async void SendPublication(object sender, RoutedEventArgs e)
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
                    foto = fotoBox.textBox.Text,
                    privacidad = privacidadBox.textBox.Text,
                    empresa = empresaBox.textBox.Text,
                    url = urlBox.textBox.Text
            }),
                Encoding.UTF8,
                "application/json");
            await PublicationsProcessor.UpdatePublication(publication._id, jsonContent);
            Utils.Utilities.GoToPublications(sender, e);
            this.Close();
        }
    }
}
