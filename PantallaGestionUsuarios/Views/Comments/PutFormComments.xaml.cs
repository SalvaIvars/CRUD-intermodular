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

        }

        private void FillData(object sender, RoutedEventArgs e)
        {
            fechaBox.textBox.Text = comment.fecha;
            mensajeBox.textBox.Text = comment.mensaje;
            idusuarioBox.textBox.Text = comment.id_usuario.ToString();
            idpublicacionBox.textBox.Text = comment.id_publicacion.ToString();
        }

        public async void SendComment(object sender, RoutedEventArgs e)
        {
            bool canUpdate = true;
            if (fechaBox.textBox.Text.Length == 0)
            {
                canUpdate = false;
                fechaBox.textBox.BorderBrush = System.Windows.Media.Brushes.Red;
                fechaBox.textBox.BorderThickness = new Thickness(1.5f);

            }
             if (mensajeBox.textBox.Text.Length == 0)
            {
                canUpdate = false;
                mensajeBox.textBox.BorderBrush = System.Windows.Media.Brushes.Red;
                mensajeBox.textBox.BorderThickness = new Thickness(1.5f);
            }
             if (idusuarioBox.textBox.Text.Length == 0)
            {
                canUpdate = false;
                idusuarioBox.textBox.BorderBrush = System.Windows.Media.Brushes.Red;
                idusuarioBox.textBox.BorderThickness = new Thickness(1.5f);
            }
             if (idpublicacionBox.textBox.Text.Length == 0)
            {
                canUpdate = false;
                idpublicacionBox.textBox.BorderBrush = System.Windows.Media.Brushes.Red;
                idpublicacionBox.textBox.BorderThickness = new Thickness(1.5f);
            }
            if(canUpdate)
            {
                using StringContent jsonContent = new(
                    JsonSerializer.Serialize(new
                    {

                        fecha = fechaBox.textBox.Text,
                        mensaje = mensajeBox.textBox.Text,
                        id_usuario = int.Parse(idusuarioBox.textBox.Text),
                        id_publicacion = int.Parse(idpublicacionBox.textBox.Text),
                    }),
                    Encoding.UTF8,
                    "application/json");
                await CommentsProcessor.UpdateComment(comment._id, jsonContent);
                Utils.Utilities.GoToComments(sender, e);
                this.Close();
            }
        }

        private void fechaBoxTextChange(object sender, TextChangedEventArgs args)
        {
            if (fechaBox.textBox.Text.Length > 0)
            {
                fechaBox.textBox.BorderThickness = new Thickness(0);
            }
        }

        private void mensajeBoxTextChange(object sender, TextChangedEventArgs args)
        {
            if (mensajeBox.textBox.Text.Length > 0)
            {
                mensajeBox.textBox.BorderThickness = new Thickness(0);
            }
        }

        private void idUsuarioBoxTextChange(object sender, TextChangedEventArgs args)
        {
            if (idusuarioBox.textBox.Text.Length > 0)
            {
                idusuarioBox.textBox.BorderThickness = new Thickness(0);
            }
        }

        private void idPublicacionBoxTextChange(object sender, TextChangedEventArgs args)
        {
            if (idpublicacionBox.textBox.Text.Length > 0)
            {
                idpublicacionBox.textBox.BorderThickness = new Thickness(0);
            }
        }
    }
}
