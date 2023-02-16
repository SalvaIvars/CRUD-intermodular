using Org.BouncyCastle.Crmf;
using PantallaGestionUsuarios.Api;
using PantallaGestionUsuarios.Controls;
using PantallaGestionUsuarios.Models;
using PantallaGestionUsuarios.Utils;
using System;
using System.Collections.Generic;
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
        string[] photos { get; set; }
        public UserProfile(UserModel user)
        {
            InitializeComponent();
            this.user = user;
            GetFollowers();
            GetFollowing();
            GetUserPublications();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private async void GetFollowers()
        {
            var users = await UserProcessor.GetFollowers(user.email);
            followers = new List<UserModel>();
            foreach (var u in users)
            {
                followers.Add(u);
            }

            followersCard.Text += followers.Count.ToString();
        }

        private async void GetFollowing()
        {

            if(user.following == null)
            {
                followingCard.Text += 0;
                return;
            }

            following = new List<UserModel>();

            UserModel userFollowing;
            foreach(var u in user.following)
            {
                userFollowing = await UserProcessor.LoadUser(u);
                following.Add(userFollowing);
            }

            followingCard.Text += following.Count.ToString();

            for(int i = 0; i < following.Count; i++)
            {
                UserCard uc = new UserCard(following[i].nick);

                if (following[i].photo != null && following[i].photo.Length > 0)
                {
                    uc.imageUser.ImageSource = new BitmapImage(new Uri("http://localhost:8080/profilePicture/" + following[i].photo));
                }

                if (i != following.Count - 1)
                {
                    Thickness margin = uc.Margin;
                    margin.Bottom = 5;
                    uc.Margin = margin;
                }
                uc.userEmail = following[i].email;
                followingUser.Children.Add(uc);
            }

        }

        private async void GetUserPublications()
        {
            var routes = await PublicationsProcessor.GetUserPublications(user.email);
            publications = new List<PublicationModel>();

            foreach (var pub in routes)
            {
                publications.Add(pub);
            }

            routesCard.Text += publications.Count.ToString();
            LoadUserPublications();
        }

        private async void LoadUserPublications()
        {
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
                    margin.Bottom = 5;
                    cd.Margin = margin;
                }
                cd.id = publications[i]._id;
                routesUser.Children.Add(cd);
            }
        }
    }
}
