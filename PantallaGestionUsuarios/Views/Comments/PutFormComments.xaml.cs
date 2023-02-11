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

        private void minimizeWindow(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void closeWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void FillData(object sender, RoutedEventArgs e)
        {
            fechaBox.textBox.Text = comment.date;
            mensajeBox.textBox.Text = comment.message;
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
            if(canUpdate)
            {
                using StringContent jsonContent = new(
                    JsonSerializer.Serialize(new
                    {

                        fecha = fechaBox.textBox.Text,
                        mensaje = mensajeBox.textBox.Text,
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

    }
}
