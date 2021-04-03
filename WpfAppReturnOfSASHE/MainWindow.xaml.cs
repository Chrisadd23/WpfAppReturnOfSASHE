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
        int backgroundspeed = 2;
        int backgroundspeedGalaxy = 5;
        int i;


        ImageBrush hg1 = new ImageBrush();
        ImageBrush zwischenraum1 = new ImageBrush();
        ImageBrush hg2 = new ImageBrush();
        ImageBrush zwischenraum2 = new ImageBrush();
        ImageBrush hgGalaxy1 = new ImageBrush();
        ImageBrush hgGalaxy2 = new ImageBrush();
        ImageBrush hgDecke = new ImageBrush();
        ImageBrush charakterIMG = new ImageBrush();

        DispatcherTimer gameTimer = new DispatcherTimer();
        DispatcherTimer gameTimer2 = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            HintergrundEinrichten();
            myCanvas.Focus();
            gameTimer.Tick += startEvents;
            gameTimer2.Tick += moveCharakter;
            gameTimer2.Interval = TimeSpan.FromMilliseconds(60);
            gameTimer.Start();
            gameTimer2.Start();
            
            
            
            
        }

        private void MusikAbspielen()
        {
            SoundPlayer splayer = new SoundPlayer("Musik\\JumpAndRun\\ReturnOfSASHEMusik1.wav");
            splayer.Play();
        }


        public void HintergrundEinrichten()
        {
            /*
             * Hintergrund Einrichten:
             * - Raumschiffbereich
             * - Weltall 
             * - Hindernisse 
             * 
             */
            hg1.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/HintergrundBilder/Raum1.png"));
            zwischenraum1.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/HintergrundBilder/Zwischenraum.png"));
            hg2.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/HintergrundBilder/Raum3erweitert.png"));
            zwischenraum2.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/HintergrundBilder/Zwischenraum.png"));
            hgGalaxy1.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/HintergrundBilder/Hintergrund.png"));
            hgGalaxy2.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/HintergrundBilder/HintergrundSpiegel.png"));
            hgDecke.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/HintergrundBilder/Decke.png"));

            hintergrund1.Fill = hg1;
            hintergrundZwischenraum1.Fill = zwischenraum1;
            hintergrund2.Fill = hg2;
            hintergrundZwischenraum2.Fill = zwischenraum2;
            hintergrundGalaxy1.Fill = hgGalaxy1;
            hintergrundGalaxy2.Fill = hgGalaxy2;
            hintergrundDach1.Fill = hgDecke;
            hintergrundDach2.Fill = hgDecke;
            hintergrundBoden1.Fill = hgDecke;
            hintergrundBoden2.Fill = hgDecke;
        }



        private void OnMyButtonClickResult(object sender, EventArgs e)
        {
            var wnd = new SpielErgebnis();
            wnd.Show();
            gameTimer.Stop();
            gameTimer2.Stop();
            this.Close();
            
        }


        private void startEvents(object sender, EventArgs e)
        {
            MusikAbspielen();
            MoveHintergrund(hintergrund1, hintergrund2, hintergrundZwischenraum1, hintergrundZwischenraum2, hintergrundDach1, hintergrundDach2, hintergrundBoden1, hintergrundBoden2, hintergrundGalaxy1, hintergrundGalaxy2, backgroundspeed, backgroundspeedGalaxy);
        }

        private void MoveHintergrund(Rectangle hintergrund1, Rectangle hintergrund2, Rectangle hintergrundZwischenraum1, Rectangle hintergrundZwischenraum2, Rectangle hintergrundDach1, Rectangle hintergrundDach2, Rectangle hintergrundBoden1, Rectangle hintergrundBoden2, Rectangle hintergrundGalaxy1, Rectangle hintergrundGalaxy2, int backgroundspeed, int backgroundspeedGalaxy)
        {
            /*Zu Beginn  hat das Element hintergrund1 (mit dem Bild Raum1.png) das Attribut Canvas.Left="-289" sobald
         * die Elemente die für das Raumschiff stehen nicht mehr in der Benutzerfläche gesehen werden, werden sie nach 
         * einer bestimmten Reihenfolge and das Ende gehängt.
         * 
         */


            Canvas.SetLeft(hintergrund1, Canvas.GetLeft(hintergrund1) - backgroundspeed);
            Canvas.SetLeft(hintergrundZwischenraum1, Canvas.GetLeft(hintergrundZwischenraum1) - backgroundspeed);
            Canvas.SetLeft(hintergrund2, Canvas.GetLeft(hintergrund2) - backgroundspeed);
            Canvas.SetLeft(hintergrundZwischenraum2, Canvas.GetLeft(hintergrundZwischenraum2) - backgroundspeed);
            Canvas.SetLeft(hintergrundDach1, Canvas.GetLeft(hintergrundDach1) - backgroundspeed);
            Canvas.SetLeft(hintergrundDach2, Canvas.GetLeft(hintergrundDach2) - backgroundspeed);
            Canvas.SetLeft(hintergrundBoden1, Canvas.GetLeft(hintergrundBoden1) - backgroundspeed);
            Canvas.SetLeft(hintergrundBoden2, Canvas.GetLeft(hintergrundBoden2) - backgroundspeed);

            Canvas.SetLeft(hintergrundGalaxy1, Canvas.GetLeft(hintergrundGalaxy1) - backgroundspeedGalaxy);
            Canvas.SetLeft(hintergrundGalaxy2, Canvas.GetLeft(hintergrundGalaxy2) - backgroundspeedGalaxy);


            if (Canvas.GetLeft(hintergrund1) < -1579)
            {
                Canvas.SetLeft(hintergrund1, Canvas.GetLeft(hintergrundZwischenraum2) + hintergrundZwischenraum2.Width - 80);
            }
            if (Canvas.GetLeft(hintergrundZwischenraum1) < -480)
            {
                Canvas.SetLeft(hintergrundZwischenraum1, Canvas.GetLeft(hintergrund1) + hintergrund1.Width - 80);
            }
            if (Canvas.GetLeft(hintergrund2) < -1579)
            {
                Canvas.SetLeft(hintergrund2, Canvas.GetLeft(hintergrundZwischenraum1) + hintergrundZwischenraum1.Width - 80);
            }
            if (Canvas.GetLeft(hintergrundZwischenraum2) < -480)
            {
                Canvas.SetLeft(hintergrundZwischenraum2, Canvas.GetLeft(hintergrund2) + hintergrund2.Width - 80);
            }
            if (Canvas.GetLeft(hintergrundGalaxy1) < -5100)
            {
                Canvas.SetLeft(hintergrundGalaxy1, Canvas.GetLeft(hintergrundGalaxy2) + 5000);
            }
            if (Canvas.GetLeft(hintergrundGalaxy2) < -5100)
            {
                Canvas.SetLeft(hintergrundGalaxy2, Canvas.GetLeft(hintergrundGalaxy1) + 5000);
            }
            if (Canvas.GetLeft(hintergrundDach1) < -5100)
            {
                Canvas.SetLeft(hintergrundDach1, Canvas.GetLeft(hintergrundDach2) + 5000);
            }
            if (Canvas.GetLeft(hintergrundDach2) < -5100)
            {
                Canvas.SetLeft(hintergrundDach2, Canvas.GetLeft(hintergrundDach1) + 5000);
            }
            if (Canvas.GetLeft(hintergrundBoden1) < -5100)
            {
                Canvas.SetLeft(hintergrundBoden1, Canvas.GetLeft(hintergrundBoden2) + 5000);
            }
            if (Canvas.GetLeft(hintergrundBoden2) < -5100)
            {
                Canvas.SetLeft(hintergrundBoden2, Canvas.GetLeft(hintergrundBoden1) + 5000);
            }
        }

        private void moveCharakter(object sender, EventArgs e)
        {

            switch (i)
            {
                case 0:
                    charakterIMG.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Charakter/newRunner_01.gif"));

                    break;
                case 1:
                    charakterIMG.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Charakter/newRunner_02.gif"));
                    break;
                case 2:
                    charakterIMG.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Charakter/newRunner_03.gif"));
                    break;
                case 3:
                    charakterIMG.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Charakter/newRunner_04.gif"));
                    break;
                case 4:
                    charakterIMG.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Charakter/newRunner_05.gif"));
                    break;
                case 5:
                    charakterIMG.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Charakter/newRunner_06.gif"));
                    break;
                case 6:
                    charakterIMG.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Charakter/newRunner_07.gif"));
                    break;
                case 7:
                    charakterIMG.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Charakter/newRunner_08.gif"));
                    break;
            }


            Charakter.Fill = charakterIMG;
            i++;
            if (i > 7)
            {
                i = 0;
            }
        }
    }

    

}

