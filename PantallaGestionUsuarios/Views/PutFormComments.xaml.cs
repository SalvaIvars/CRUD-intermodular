using PantallaGestionUsuarios.Api;
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
    /// Lógica de interacción para PutFormComments.xaml
    /// </summary>
    public partial class PutFormComments : Window
    {
        private CommentsModel comment { get; set; }
        public PutFormComments(CommentsModel comment)
        {
            InitializeComponent();
            this.comment = comment;

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
            fechaBox.textBox.Text = comment.fecha;
            mensajeBox.textBox.Text = comment.mensaje;
            idusuarioBox.textBox.Text = comment.id_usuario;
            idpublicacionBox.textBox.Text = comment.id_publicacion;
        }

        public async void SendComment(object sender, RoutedEventArgs e)
        {

            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {

                    fecha = fechaBox.textBox.Text,
                    mensaje = mensajeBox.textBox.Text,
                    id_usuario = idusuarioBox.textBox.Text,
                    id_publicacion = idpublicacionBox.textBox.Text,
                }),
                Encoding.UTF8,
                "application/json");
            await CommentsProcessor.UpdateComment(comment._id, jsonContent);
            Utils.Utilities.GoToComments(sender, e);
            this.Close();
        }
    }
}
