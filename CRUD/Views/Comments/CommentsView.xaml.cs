using CRUD.Api;
using CRUD.Models;
using CRUD.Utils;
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

namespace CRUD.Views
{
    /// <summary>
    /// Lógica de interacción para CommentsView.xaml
    /// </summary>
    public partial class CommentsView : Window
    {
        public CommentsView()
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
            var comments = await CommentsProcessor.LoadAllComments();
            List<CommentsModel> commentsList = new List<CommentsModel>();

            foreach (var comment in comments)
            {
                var nombreUsuario = await UserProcessor.LoadUser(comment.email);
                var nombreRuta = await PublicationsProcessor.LoadPublication(comment.id_publication);
                comment.id_publication = nombreRuta.name;
                comment.email = nombreUsuario.email;
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
                if (col.Header.ToString() == "email")
                {
                    col.Header = "name";
                }else if(col.Header.ToString() == "id_publication")
                {
                    col.Header = "publication";
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


   