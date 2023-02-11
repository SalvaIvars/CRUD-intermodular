using PantallaGestionUsuarios.Api;
using PantallaGestionUsuarios.Models;
using PantallaGestionUsuarios.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
    /// Lógica de interacción para PublicationsView.xaml
    /// </summary>
    public partial class PublicationsView : Window
    {
        public PublicationsView()
        {
            InitializeComponent();
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void minimizeWindow(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void closeWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void CreateTable(object sender, RoutedEventArgs e)
        {
            var publications = await PublicationsProcessor.LoadAllPublications();
            List<PublicationModel> publicationsList = new List<PublicationModel>();

            foreach (var pub in publications)
            {
                publicationsList.Add(pub);
            }

            var content = new ObservableCollection<PublicationModel>(publicationsList);
            publicationsDataGrid.ItemsSource = content;

            foreach (DataGridColumn col in publicationsDataGrid.Columns)
            {
                if (col.Header.ToString() == "_id" || col.Header.ToString() == "id_user" || col.Header.ToString() == "photo" || col.Header.ToString() == "description")
                {
                    col.Visibility = Visibility.Collapsed;
                }
            }
        }

        public async void DeletePublication(object sender, RoutedEventArgs e)
        {
            PublicationModel publication = (PublicationModel)publicationsDataGrid.SelectedItem;
            await PublicationsProcessor.DeletePublication(publication._id);
            CreateTable(sender, e);
        }

        public void UpdatePublication(object sender, RoutedEventArgs e)
        {

            PublicationModel publication = (PublicationModel)publicationsDataGrid.SelectedItem;

            PutFormPublications win = new PutFormPublications(publication);
            win.Show();
            this.Close();

        }
    }
}
