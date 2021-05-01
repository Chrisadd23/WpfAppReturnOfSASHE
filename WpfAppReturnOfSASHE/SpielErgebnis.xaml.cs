using WpfAppReturnOfSASHE;

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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfAppReturnOfSASHE
{
    /// <summary>
    /// Interaktionslogik für SpielErgebnis.xaml
    /// </summary>
    /// 

    
    public partial class SpielErgebnis : Window
    {
        GameWindow gameWindow;

        DispatcherTimer gameTimer = new DispatcherTimer();
        DispatcherTimer gameTimer2 = new DispatcherTimer();

        ImageBrush runner = new ImageBrush();
        ImageBrush runner2 = new ImageBrush();

        Datenbank datenbank;
        
        int i = 0;

        SoundPlayer splayer;
        Spieler spieler;
        List<Spieler> listSpieler = new List<Spieler>();

        public List<Spieler> ListSpieler { get => listSpieler; set => listSpieler = value; }

        public SpielErgebnis()
        {
            InitializeComponent();
            splayer = new SoundPlayer("Musik\\EndeScore\\NGNL.wav");
            splayer.Play();
            
            canvasSpielstand.Focus();
            
            gameTimer.Tick += CharakterBewegen;
            gameTimer.Interval = TimeSpan.FromMilliseconds(50);
            gameTimer.Start();


        

            gameTimer2.Tick += hintergrundMusik;
            gameTimer2.Interval += TimeSpan.FromSeconds(168);
            gameTimer2.Start();
           
        }
        public SpielErgebnis(Spieler spieler,Datenbank datenbank) : this()
        {
            this.spieler = spieler;
            this.datenbank = datenbank;
            listSpieler = datenbank.RanglisteSpieler();
            //datenbank.Stop();

            if (listSpieler[0] != null)
            {
                platz1.Text = ListSpieler[0].ToString();
            }
            else
                platz1.Text = "";

            if (listSpieler[1] != null)
            {
                platz2.Text = ListSpieler[1].ToString();
            }
            else
                platz2.Text = "";

            if (listSpieler[2] != null)
            {
                platz3.Text = ListSpieler[2].ToString();
            }
            else
                platz3.Text = "";

            if (listSpieler[3] != null)
            {
                platz4.Text = ListSpieler[3].ToString();
            }
            else
                platz4.Text = "";

            if (listSpieler[4] != null)
            {
                platz5.Text = ListSpieler[4].ToString();
            }
            else
                platz5.Text = "";

        }

        private void CharakterBewegen(object sender, EventArgs e)
        {
            CharakterBewegenErgebnis();
        }

        private void hintergrundMusik(object sender, EventArgs e)
        {
            splayer.Play();
        }

        

        private void CharakterBewegenErgebnis()
        {
            

            switch (i)
            {
                case 0:
                    runner2.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Charakter/newRunner_01.gif"));

                    break;
                case 1:
                    runner2.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Charakter/newRunner_02.gif"));
                    break;
                case 2:
                    runner2.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Charakter/newRunner_03.gif"));
                    break;
                case 3:
                    runner2.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Charakter/newRunner_04.gif"));
                    break;
                case 4:
                    runner2.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Charakter/newRunner_05.gif"));
                    break;
                case 5:
                    runner2.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Charakter/newRunner_06.gif"));
                    break;
                case 6:
                    runner2.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Charakter/newRunner_07.gif"));
                    break;
                case 7:
                    runner2.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Charakter/newRunner_08.gif"));
                    break;
            }

            
            Darstellung1.Fill = runner2;
            Darstellung2.Fill = runner2;
            i++;
            if (i > 7)
            {
                i = 0;
            }
        }

        private void btn_restart_Click(object sender, RoutedEventArgs e)
        {
            Stop();
            restartGame();
        }

        private void restartGame()
        {
            Stop();
            if (spieler == null)
            {
                gameWindow = new GameWindow();
                gameWindow.Show();
            }
            else
            {
                gameWindow = new GameWindow(spieler,datenbank);
                gameWindow.Show();
            }

            
            this.Close();
        }

        private void Stop()
        {
            gameTimer.Stop();
            gameTimer2.Stop();
            splayer.Stop();
        }

        private void btn_lougout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Stop();
            this.Close();
        }
    }
}
