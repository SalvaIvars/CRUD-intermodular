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
using PantallaGestionUsuarios.Utils;
namespace PantallaGestionUsuarios.Views
{
    /// <summary>
    /// Lógica de interacción para UsersView.xaml
    /// </summary>
    public partial class UsersView : Window
    {
        public UsersView()
        {
            InitializeComponent();
            string token = UserProcessor.ObtainToken();
            ApiHelper.token = token.Substring(1,token.Length-2); // delete " "
            ApiHelper.AddToken();


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
            var users = await UserProcessor.LoadAllUsers();
            List<UserModel> userList = new List<UserModel>();
            foreach (var user in users)
            {
                userList.Add(user);
            }

            var content = new ObservableCollection<UserModel>(userList);
            usersDataGrid.ItemsSource = content;

            foreach (DataGridColumn col in usersDataGrid.Columns)
            {
                if (col.Header.ToString() == "_id" || col.Header.ToString() == "password")
                {
                    col.Visibility = Visibility.Collapsed;
                }
            }
        }

        private async void CreateUser(object sender, RoutedEventArgs e)
        {
            UserModel user = (UserModel)usersDataGrid.SelectedItem;

            PostUser win = new PostUser(user);
            win.Show();
            this.Close();
        }

        public async void DeleteUser(object sender, RoutedEventArgs e)
        {
            UserModel user = (UserModel)usersDataGrid.SelectedItem;
            await UserProcessor.DeleteUser(user._id);
            CreateTable(sender, e);
        }

        public void UpdateUser(object sender, RoutedEventArgs e)
        {
            UserModel user = (UserModel)usersDataGrid.SelectedItem;

            PutFormUsers win = new PutFormUsers(user);
            win.Show();
            this.Close();
        }
    }
}
