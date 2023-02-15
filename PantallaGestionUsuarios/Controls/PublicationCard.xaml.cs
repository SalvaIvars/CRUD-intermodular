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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PantallaGestionUsuarios.Controls
{
    /// <summary>
    /// Lógica de interacción para PublicationCard.xaml
    /// </summary>
    public partial class PublicationCard : UserControl
    {
        public PublicationCard(string routeName, string difficultyRoute, string distanceRoute)
        {
            InitializeComponent();
            distance.Text = distanceRoute;
            difficulty.Text = difficultyRoute;
            name.Text = routeName;
        }
    }
}
