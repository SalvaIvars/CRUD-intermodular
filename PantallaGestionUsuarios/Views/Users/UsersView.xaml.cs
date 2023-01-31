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

        private void logOut(object sender, RoutedEventArgs e)
        {
            Utilities.GoToLogin(sender, e);
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
            dataGrid.ItemsSource = content;

            foreach (DataGridColumn col in dataGrid.Columns)
            {
                if (col.Header.ToString() == "_id" || col.Header.ToString() == "password" || col.Header.ToString() == "foto" )
                {
                    col.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void CreateUser(object sender, RoutedEventArgs e)
        {
            Utilities.GoToPostUser(sender, e);
            this.Close();
        }

        public async void DeleteUser(object sender, RoutedEventArgs e)
        {
            UserModel user = (UserModel)dataGrid.SelectedItem;
            await UserProcessor.DeleteUser(user._id);
            CreateTable(sender, e);
        }

        public void UpdateUser(object sender, RoutedEventArgs e)
        {
            UserModel user = (UserModel)dataGrid.SelectedItem;

            PutFormUsers win = new PutFormUsers(user);
            win.Show();
            this.Close();
        }
    }
}
