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
    /// Lógica de interacción para CommentsView.xaml
    /// </summary>
    public partial class CommentsView : Window
    {
        public CommentsView()
        {
            InitializeComponent();
            publicationsButton.Click += GoToPublications;
            commentsButton.Click += GoToComments;
            usersButton.Click += GoToUsers;
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
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

        private async void CreateTable(object sender, RoutedEventArgs e)
        {
            var comments = await CommentsProcessor.LoadAllComments();
            List<CommentsModel> commentsList = new List<CommentsModel>();

            foreach (var comment in comments)
            {
                commentsList.Add(comment);
            }

            var content = new ObservableCollection<CommentsModel>(commentsList);
            commentsDataGrid.ItemsSource = content;

            foreach (DataGridColumn col in commentsDataGrid.Columns)
            {
                if (col.Header.ToString() == "_id")
                {
                    col.Visibility = Visibility.Collapsed;
                }
            }
        }

        public async void DeleteComment(object sender, RoutedEventArgs e)
        {
            CommentsModel publication = (CommentsModel)commentsDataGrid.SelectedItem;
            await CommentsProcessor.DeleteComment(publication._id);
            CreateTable(sender, e);
        }

        public async void UpdateComment(object sender, RoutedEventArgs e)
        {
            CommentsModel comment = (CommentsModel)commentsDataGrid.SelectedItem;

            PutFormComments win = new PutFormComments(comment);
            win.Show();
            this.Close();
        }
    }
}


   