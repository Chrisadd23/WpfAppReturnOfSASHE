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

namespace WpfAppReturnOfSASHE
{
    /// <summary>
    /// Interaktionslogik für Registrieren.xaml
    /// </summary>
    public partial class Registrieren : Window
    {
        public Registrieren()
        {
            InitializeComponent();
        }

        private void btn_startMainWindow(object sender, RoutedEventArgs e)
        {
            MainWindow anmelden = new MainWindow();
            anmelden.Show();
            this.Close();
        }

        private void btn_Registrieren(object sender, RoutedEventArgs e)
        {

        }
    }
}
