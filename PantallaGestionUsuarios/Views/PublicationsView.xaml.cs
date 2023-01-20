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
            publicationsButton.Click += Utilities.GoToPublications;
            commentsButton.Click += Utilities.GoToComments;
            usersButton.Click += Utilities.GoToUsers;
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
                if (col.Header.ToString() == "_id" || col.Header.ToString() == "id_usuario")
                {
                    col.Visibility = Visibility.Collapsed;
                }
            }
        }

        private async void CreateUser(object sender, RoutedEventArgs e)
        {
            await PublicationsProcessor.PostPublication();
            CreateTable(sender, e);
        }

        public async void DeleteUser(object sender, RoutedEventArgs e)
        {
            PublicationModel publication = (PublicationModel)publicationsDataGrid.SelectedItem;
            await PublicationsProcessor.DeletePublication(publication._id);
            CreateTable(sender, e);
        }

        public async void UpdateUser(object sender, RoutedEventArgs e)
        {
            PublicationModel publication = (PublicationModel)publicationsDataGrid.SelectedItem;

            await PublicationsProcessor.DeletePublication(publication._id);
            CreateTable(sender, e);
        }
    }
}
