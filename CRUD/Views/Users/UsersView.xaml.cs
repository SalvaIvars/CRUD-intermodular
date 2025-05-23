﻿using System;
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
using MahApps.Metro.IconPacks;
using CRUD.Utils;
namespace CRUD.Views
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

        private void minimizeWindow(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void closeWindow(object sender, RoutedEventArgs e)
        {
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
                if (col.Header.ToString() == "_id" || col.Header.ToString() == "fav_routes" || col.Header.ToString() == "description"  || col.Header.ToString() == "following" || col.Header.ToString() == "lastname" || col.Header.ToString() == "password" || col.Header.ToString() == "photo" )
                {
                    col.Visibility = Visibility.Collapsed;
                }
            }
        }

        private async void  CreateUser(object sender, RoutedEventArgs e)
        {
            Utilities.GoToPostUser(sender, e);
            this.Close();
        }

        public async void DeleteUser(object sender, RoutedEventArgs e)
        {
            UserModel user = (UserModel)dataGrid.SelectedItem;
            await UserProcessor.DeleteUser(user.email);
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
