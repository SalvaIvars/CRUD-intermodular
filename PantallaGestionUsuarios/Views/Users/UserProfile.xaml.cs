using Org.BouncyCastle.Crmf;
using PantallaGestionUsuarios.Api;
using PantallaGestionUsuarios.Controls;
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

namespace PantallaGestionUsuarios.Views.Users
{
    /// <summary>
    /// Lógica de interacción para UserProfile.xaml
    /// </summary>
    public partial class UserProfile : Window
    {
        UserModel user { get; set; }
        List<UserModel> followers { get; set; }
        List<UserModel> following { get; set; }
        List<PublicationModel> publications { get; set; }
        List<PublicationModel> allPublications { get; set; }
        string[] photos { get; set; }

        public UserProfile(UserModel userL)
        {
            InitializeComponent();
            this.user = userL;
            ReloadAll();

        }

        private void minimizeWindow(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void closeWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void ReloadAll()
        {
            GetFollowers();
            GetFollowing();
            GetPublications();
            GetUserPublications();
            GetUserFavRoutes();

        }

        private void changeToRoutes(object sender, MouseButtonEventArgs e)
        {
            routesMainCard.Visibility = Visibility.Visible;
            followingMainCard.Visibility = Visibility.Collapsed;
            followersMainCard.Visibility = Visibility.Collapsed;
            favMainCard.Visibility = Visibility.Collapsed;
            routesButton.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#497800");
            followingButton.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#A4BE7B");
            followersButton.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#A4BE7B");
            favButton.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#A4BE7B");
        }

        private void changeToFollowers(object sender, MouseButtonEventArgs e)
        {
            routesMainCard.Visibility = Visibility.Collapsed;
            followingMainCard.Visibility = Visibility.Collapsed;
            followersMainCard.Visibility = Visibility.Visible;
            favMainCard.Visibility = Visibility.Collapsed;
            routesButton.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#A4BE7B");
            followingButton.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#A4BE7B");
            followersButton.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#497800");
            favButton.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#A4BE7B");
        }

        private void changeToFollowing(object sender, MouseButtonEventArgs e)
        {
            routesMainCard.Visibility = Visibility.Collapsed;
            followingMainCard.Visibility = Visibility.Visible;
            followersMainCard.Visibility = Visibility.Collapsed;
            favMainCard.Visibility = Visibility.Collapsed;
            routesButton.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#A4BE7B");
            followingButton.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#497800");
            followersButton.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#A4BE7B");
            favButton.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#A4BE7B");
        }

        private void changeToFav(object sender, MouseButtonEventArgs e)
        {
            routesMainCard.Visibility = Visibility.Collapsed;
            followingMainCard.Visibility = Visibility.Collapsed;
            followersMainCard.Visibility = Visibility.Collapsed;
            favMainCard.Visibility = Visibility.Visible;
            routesButton.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#A4BE7B");
            followingButton.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#A4BE7B");
            followersButton.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#A4BE7B");
            favButton.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#497800");
        }

        private void LoadUserInformation()
        {
            userName.Text = user.name;
            userNick.Text = user.nick;
            userDescription.Text = user.description;

            if (user.photo != null && user.photo.Length > 0)
            {
                userImage.ImageSource = new BitmapImage(new Uri("http://localhost:8080/profilePicture/" + user.photo));
            }


            if (user.nick == ((UserModel)Application.Current.Properties["user"]).nick)
            {
                userFollowButton.Content = "Editar usuario";
            }
            else
            {
                UserModel actualUser = (UserModel)Application.Current.Properties["user"];

                for(int i = 0; i < actualUser.following.Length; i++)
                {
                    if (actualUser.following[i] == user.email){
                        userFollowButton.Content = "Siguiendo";
                        break;
                    }
                    else
                    {
                        userFollowButton.Content = "Seguir";
                    }
                }
            }
        }

        private async void GetFollowers()
        {
            var users = await UserProcessor.GetFollowers(user.email);
            followers = new List<UserModel>();
            foreach (var u in users)
            {
                followers.Add(u);
            }

            followersCardText.Text += followers.Count.ToString();
            LoadFollowers();
        }

        private async void GetFollowing()
        {

            if(user.following == null)
            {
                followingCardText.Text += 0;
                return;
            }

            following = new List<UserModel>();

            UserModel userFollowing;
            foreach(var u in user.following)
            {
                userFollowing = await UserProcessor.LoadUser(u);
                following.Add(userFollowing);
            }

            followingCardText.Text += following.Count.ToString();

            for(int i = 0; i < following.Count; i++)
            {
                UserCard uc = new UserCard(following[i].name, following[i].nick);

                if (following[i].photo != null && following[i].photo.Length > 0)
                {
                    uc.imageUser.ImageSource = new BitmapImage(new Uri("http://localhost:8080/profilePicture/" + following[i].photo));
                }

                if (i != following.Count - 1)
                {
                    Thickness margin = uc.Margin;
                    margin.Bottom = 1;
                    uc.Margin = margin;
                }
                uc.userEmail = following[i].email;
                followingUser.Children.Add(uc);
            }

        }

        private async void LoadFollowers()
        {
            if (followers == null)
            {
                return;
            }

            for (int i = 0; i < followers.Count; i++)
            {
                UserCard uc = new UserCard(followers[i].name, followers[i].nick);

                if (followers[i].photo != null && followers[i].photo.Length > 0)
                {
                    uc.imageUser.ImageSource = new BitmapImage(new Uri("http://localhost:8080/profilePicture/" + followers[i].photo));
                }

                if (i != followers.Count - 1)
                {
                    Thickness margin = uc.Margin;
                    margin.Bottom = 1;
                    uc.Margin = margin;
                }
                uc.userEmail = followers[i].email;
                followersMain.Children.Add(uc);
            }
        }

        private void GetUserFavRoutes()
        {
            if (user.fav_routes == null)
            {
                favCardText.Text += "0";
            }
            else
            {
                favCardText.Text += user.fav_routes.Length.ToString();
            }
            LoadUserInformation();
        }

        private async void GetUserPublications()
        {
            var routes = await PublicationsProcessor.GetUserPublications(user.email);
            publications = new List<PublicationModel>();

            foreach (var pub in routes)
            {
                publications.Add(pub);
            }

            routesCardText.Text += publications.Count.ToString();
            LoadUserPublications();
            LoadUserFavPublications();
        }

        private async void GetPublications()
        {
            var pubs = await PublicationsProcessor.LoadAllPublications();
            allPublications = new List<PublicationModel>();

            foreach (var pub in pubs)
            {
                allPublications.Add(pub);
            }
        }

        private async void LoadUserPublications()
        {
            if(publications == null)
            {
                return;
            }
            string photo = "";
            for(int i = 0; i <  publications.Count; i++)
            {
                PublicationCard cd = new PublicationCard(publications[i].name, publications[i].difficulty, publications[i].distance.ToString());
                photos = await PublicationsProcessor.GetNumberOfPhotos(publications[i]._id);

                if(photos != null)
                {
                    if (photos.Length >= 3)
                    {
                        cd.photo1.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publications[i]._id + "/" + photos[0]));
                        cd.photo2.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publications[i]._id + "/" + photos[1]));
                        cd.photo3.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publications[i]._id + "/" + photos[2]));
                    }
                    else if(photos.Length == 2)
                    {
                        cd.photo1.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publications[i]._id + "/" + photos[0]));
                        cd.photo2.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publications[i]._id + "/" + photos[1]));
                    }
                    else if(photos.Length == 1)
                    {
                        cd.photo1.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publications[i]._id + "/" + photos[0]));
                    }
                }

                photo = Utilities.ActivityToImage(publications[i].category);
                cd.imageActivity.Source = new BitmapImage(new Uri(photo, UriKind.Relative));

                if (i != publications.Count - 1)
                {
                    Thickness margin = cd.Margin;
                    margin.Bottom = 1;
                    cd.Margin = margin;
                }
                cd.id = publications[i]._id;
                routesUser.Children.Add(cd);
            }
        }

        private async void LoadUserFavPublications()
        {
            if(allPublications == null)
            {
                return;
            }
            string photo = "";
            for (int i = 0; i < allPublications.Count; i++)
            {
                if (!user.fav_routes.Contains(allPublications[i]._id))
                {
                    continue;
                }

                PublicationCard cd = new PublicationCard(allPublications[i].name, allPublications[i].difficulty, allPublications[i].distance.ToString());
                photos = await PublicationsProcessor.GetNumberOfPhotos(allPublications[i]._id);

                if (photos != null)
                {
                    if (photos.Length >= 3)
                    {
                        cd.photo1.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publications[i]._id + "/" + photos[0]));
                        cd.photo2.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publications[i]._id + "/" + photos[1]));
                        cd.photo3.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publications[i]._id + "/" + photos[2]));
                    }
                    else if (photos.Length == 2)
                    {
                        cd.photo1.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publications[i]._id + "/" + photos[0]));
                        cd.photo2.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publications[i]._id + "/" + photos[1]));
                    }
                    else if (photos.Length == 1)
                    {
                        cd.photo1.Source = new BitmapImage(new Uri("http://localhost:8080/publicationPicture/" + publications[i]._id + "/" + photos[0]));
                    }
                }

                photo = Utilities.ActivityToImage(allPublications[i].category);
                cd.imageActivity.Source = new BitmapImage(new Uri(photo, UriKind.Relative));

                if (i != allPublications.Count - 1)
                {
                    Thickness margin = cd.Margin;
                    margin.Bottom = 1 ;
                    cd.Margin = margin;
                }
                cd.id = allPublications[i]._id;
                favRoutes.Children.Add(cd);

            }
        }

        private async void userClickButton(object sender, RoutedEventArgs e)
        {
            if (user.nick == ((UserModel)Application.Current.Properties["user"]).nick)
            {
                UserModel userUpdate = (UserModel)Application.Current.Properties["user"];

                PutFormUsers win = new PutFormUsers(userUpdate);
                win.Show();
                this.Close();
            }
            else if(userFollowButton.Content == "Siguiendo")
            {
                userFollowButton.Content = "Seguir";
                using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    email = ((UserModel)Application.Current.Properties["user"]).email,
                    emailToUnfollow = user.email,
                }),
                Encoding.UTF8,
                "application/json");
                await UserProcessor.UnfollowUser(jsonContent);

                await Task.Delay(2000);
                LoadUserInformation();
                UserProfile a = new UserProfile(user);
                a.Show();
                this.Close();
            }
            else if (userFollowButton.Content == "Seguir")
            {
                userFollowButton.Content = "Siguiendo";
                using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    email = ((UserModel)Application.Current.Properties["user"]).email,
                    emailToFollow = user.email,
                }),
                Encoding.UTF8,
                "application/json");
                await UserProcessor.FollowUser(jsonContent);

                await Task.Delay(2000);
                LoadUserInformation();

                UserProfile a = new UserProfile(user);
                a.Show();
                this.Close();
            }
        }
    }
}
