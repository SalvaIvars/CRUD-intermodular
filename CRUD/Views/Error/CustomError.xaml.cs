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

namespace CRUD.Views.Error
{
    /// <summary>
    /// Lógica de interacción para PruebaError.xaml
    /// </summary>
    public partial class CustomError : Window
    {
        public CustomError(string Message)
        {
            InitializeComponent();
            errorMessage.Text = Message;
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}
