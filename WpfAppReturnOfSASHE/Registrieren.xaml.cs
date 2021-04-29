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
        Datenbank datenbank;
        Spieler spieler;
       
        public Registrieren()
        {
            InitializeComponent();
            
        }

        private void btn_startMainWindow(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void btn_Registrieren(object sender, RoutedEventArgs e)
        {
            if (reg_Vorname.Text == "" || reg_Nachname.Text == "" || reg_Benutzername.Text == "" || reg_Passwort.Text == "" || reg_PasswortErneut.Text == "")
            {
                MessageBox.Show("Bitte keine Box leer lassen");
            }
            else if (!(reg_Passwort.Text.Equals(reg_PasswortErneut.Text)))
            {
                MessageBox.Show("Passwörter übereinstimmen nicht! Bitte überprüfen sie das Passwort");
            }
            else if (reg_PasswortErneut.Text.Length < 5)
            {
                MessageBox.Show("Ihr Passwort ist zu kurz");
            }
            else if(reg_Passwort.Text.Length > 15)
            {
                MessageBox.Show("Ihr Passwort ist zu lang");
            }
            else
            {
                datenbank = new Datenbank();
                spieler = datenbank.Registrieren(reg_Vorname.Text, reg_Nachname.Text, reg_Benutzername.Text, reg_Passwort.Text, reg_PasswortErneut.Text);

                datenbank.Stop();
            }
        }
    }
}
