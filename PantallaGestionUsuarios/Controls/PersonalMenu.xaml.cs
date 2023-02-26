using PantallaGestionUsuarios.Utils;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PantallaGestionUsuarios.Controls
{
    /// <summary>
    /// Lógica de interacción para PersonalMenu.xaml
    /// </summary>
    public partial class PersonalMenu : UserControl
    {
        public PersonalMenu()
        {
            InitializeComponent();
            publicationsButton.Click += GoToPublications;
            commentsButton.Click += GoToComments;
            usersButton.Click += GoToUsers;

        }

        public async Task<string> Refresh()
        {
            Application.Current.Properties["user"] = await UserProcessor.LoadUser(((UserModel)Application.Current.Properties["user"]).email);
            UserModel user = (UserModel)Application.Current.Properties["user"];
            textProfileButton.Text = user.name;
            if (user.photo != null && user.photo.Length > 0)
            {
                profilePicture.ImageSource = new BitmapImage(new Uri("http://localhost:8080/profilePicture/" + user.photo));

            }
            else
            {
                profilePicture.ImageSource = new BitmapImage(new Uri("../images/defaultProfilePicture.png"));
            }
            return "";
        }
        
       public void LoadUserName (object sender, RoutedEventArgs e)
        {
            UserModel user = (UserModel)Application.Current.Properties["user"];
            textProfileButton.Text = user.name;
            if (user.photo != null && user.photo.Length > 0)
            {

                profilePicture.ImageSource = new BitmapImage(new Uri("http://localhost:8080/profilePicture/" + user.photo));
            }
        }

        private void GoToUserProfile(object sender, RoutedEventArgs e)
        {
            Utilities.GoToUserProfile(sender, e, (UserModel)Application.Current.Properties["user"]);
            var myWindow = Window.GetWindow(this);
            myWindow.Close();
        }

        private void GoToPublications(object sender, RoutedEventArgs e)
        {
            Utilities.GoToPublications(sender, e);
            var myWindow = Window.GetWindow(this);
            myWindow.Close();
        }
        private void GoToComments(object sender, RoutedEventArgs e)
        {
            Utilities.GoToComments(sender, e);
            var myWindow = Window.GetWindow(this);
            myWindow.Close();
        }
        private void GoToUsers(object sender, RoutedEventArgs e)
        {
            Utilities.GoToUsers(sender, e);
            var myWindow = Window.GetWindow(this);
            myWindow.Close();
        }

        private void logOut(object sender, RoutedEventArgs e)
        {
            Utilities.GoToLogin(sender, e);
            var myWindow = Window.GetWindow(this);
            myWindow.Close();
        }
    }
}
