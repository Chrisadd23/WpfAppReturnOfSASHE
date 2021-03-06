using WpfAppReturnOfSASHE;

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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

using System.Windows.Threading;

namespace WpfAppReturnOfSASHE
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Datenbank datenbank;
        GameWindow gameWindow;
        Registrieren registrieren;
        Spieler spieler;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_startGameOffline(object sender, RoutedEventArgs e)
        {
            gameWindow = new GameWindow();
            gameWindow.Show();
            this.Close();
        }

        private void btn_clickRegistrieren(object sender, RoutedEventArgs e)
        {
            datenbank = new Datenbank();
            registrieren = new Registrieren(datenbank);
            registrieren.Show();
            this.Close();
        }

        private void btn_clickAnmelden(object sender, RoutedEventArgs e)
        {
            if(txt_userName.Text == "" || txt_userPsw.Password == "")
            {
                MessageBox.Show("Bitte lassen sie die Felder nicht leer!");
            }
            else
            {
                datenbank = new Datenbank();
                spieler = datenbank.checkAnmeldung(txt_userName.Text, txt_userPsw.Password);
                //datenbank.Stop();
                if (spieler != null)
                {
                    gameWindow = new GameWindow(spieler,datenbank);
                    gameWindow.Show();
                    this.Close();
                }
            }
            
        }
    }

    

}

