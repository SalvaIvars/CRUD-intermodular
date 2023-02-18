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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PantallaGestionUsuarios.Controls
{
    /// <summary>
    /// Lógica de interacción para UserCard.xaml
    /// </summary>
    public partial class UserCard : UserControl
    {
        public string userEmail { get; set; }   
        public UserCard(string name, string nick)
        {
            InitializeComponent();
            userName.Text = name;
            userNick.Text = nick;
        }

        private async void loadUser(object sender, MouseButtonEventArgs e)
        {
            Utilities.GoToUserProfile(sender, e, await UserProcessor.LoadUser(userEmail));
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();
        }
    }
}
