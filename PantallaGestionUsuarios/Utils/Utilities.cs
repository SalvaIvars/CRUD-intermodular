using PantallaGestionUsuarios.Views;
using PantallaGestionUsuarios.Views.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace PantallaGestionUsuarios.Utils
{
    public static class Utilities
    {
        public static void GoToPublications(object sender, RoutedEventArgs e)
        {
            PublicationsView win = new PublicationsView();
            win.Show();
        }

        public static void GoToUserProfile(object sender, RoutedEventArgs e, UserModel user)
        {
            UserProfile win = new UserProfile(user);
            win.Show();
        }

        public static void GoToLogin(object sender, RoutedEventArgs e)
        {
            LoginView win = new LoginView();
            win.Show();
        }

        public static void GoToUsers(object sender, RoutedEventArgs e)
        {
            UsersView win = new UsersView();
            win.Show();
        }
        
        public static void GoToComments(object sender, RoutedEventArgs e)
        {
            CommentsView win = new CommentsView();
            win.Show();
        }
        public static void GoToPostUser(object sender, RoutedEventArgs e)
        {
            PostUser win = new PostUser();
            win.Show();
        }

        public static string ActivityToImage(string activity)
        {
            switch (activity)
            {
                case "escalada":
                    return "../Images/Activities/escalada.png";
                case "caminata":
                    return "../Images/Activities/caminata.png";
                case "bicicleta":
                    return "../Images/Activities/bicicleta.png";
                default:
                    return "../images/defaultProfilePicture.png";
            }
        }
    }
}
