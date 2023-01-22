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
    /// Lógica de interacción para PersonalPasswordBox.xaml
    /// </summary>
    public partial class PersonalPasswordBox : UserControl
    {
        public PersonalPasswordBox()
        {
            InitializeComponent();
        }
        public string HintPassword
        {
            get { return (string)GetValue(HintPasswordProperty); }
            set { SetValue(HintPasswordProperty, value); }
        }

        public static readonly DependencyProperty HintPasswordProperty = DependencyProperty.Register
            ("HintPassword", typeof(string), typeof(PersonalPasswordBox));
    }
}
