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
        DispatcherTimer gameTimer = new DispatcherTimer();
        DispatcherTimer gameTimer2 = new DispatcherTimer();


        


        ImageBrush runner = new ImageBrush();
        ImageBrush runner2 = new ImageBrush();

        
        int i = 0;

        SoundPlayer splayer  = new SoundPlayer("Musik\\EndeScore\\NGNL.wav");

        public SpielErgebnis()
        {
            InitializeComponent();
            splayer.Play();
            canvasSpielstand.Focus();
            
            gameTimer.Tick += CharakterBewegen;
            gameTimer.Interval = TimeSpan.FromMilliseconds(80);
            gameTimer.Start();


            //----- Musik wird je nach Rang festgelegt im weiteren verlauf

            gameTimer2.Tick += hintergrundMusik;
            gameTimer2.Interval += TimeSpan.FromSeconds(168);
            gameTimer2.Start();
           
        }

        private void CharakterBewegen(object sender, EventArgs e)
        {
            CharakterBewegenErgebnisLinks();
            CharakterBewegenErgebnisRechts();
        }

        private void hintergrundMusik(object sender, EventArgs e)
        {
            splayer.Play();
        }

        

        private void CharakterBewegenErgebnisRechts()
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
            i++;
            if (i > 7)
            {
                i = 0;
            }
        }

        private void CharakterBewegenErgebnisLinks()
        {
            
            
                switch(i)
                {
                    case 0:
                        runner.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Charakter/newRunner_01.gif"));
                        break;
                    case 1:
                        runner.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Charakter/newRunner_02.gif"));
                        break;
                    case 2:
                        runner.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Charakter/newRunner_03.gif"));
                        break;
                    case 3:
                        runner.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Charakter/newRunner_04.gif"));
                        break;
                    case 4:
                        runner.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Charakter/newRunner_05.gif"));
                        break;
                    case 5:
                        runner.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Charakter/newRunner_06.gif"));
                        break;
                    case 6:
                        runner.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Charakter/newRunner_07.gif"));
                        break;
                    case 7:
                        runner.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Charakter/newRunner_08.gif"));
                        break;
                }

                Darstellung2.Fill = runner;
                i++;
                if(i > 7)
                {
                    i = 0;
                }
            
        }

        private void btn_restart_Click(object sender, RoutedEventArgs e)
        {
            restartGame();
        }

        private void restartGame()
        {
            MainWindow spiel = new MainWindow();
            spiel.Show();

            this.Close();
        }
    }
}
