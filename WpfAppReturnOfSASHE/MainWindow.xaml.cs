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
        int backgroundspeed = 5;
        int backgroundspeedGalaxy = 9;
        int jumpspeed = 6;
        int i;

        bool goUp;


        ImageBrush hg1 = new ImageBrush();
        ImageBrush hg2 = new ImageBrush();      
        ImageBrush hgGalaxy1 = new ImageBrush();
        ImageBrush hgGalaxy2 = new ImageBrush();
        ImageBrush hgDecke = new ImageBrush();

        ImageBrush charakterIMG = new ImageBrush();

        ImageBrush zwischenraumLinks = new ImageBrush();
        ImageBrush zwischenraumMitte = new ImageBrush();
        ImageBrush zwischenraumRechts = new ImageBrush();


        DispatcherTimer gameTimer = new DispatcherTimer();
        DispatcherTimer gameTimer2 = new DispatcherTimer();

        SoundPlayer splayer;

        public MainWindow()
        {
            InitializeComponent();
            HintergrundEinrichten();
            myCanvas.Focus();
            gameTimer.Tick += startEvents;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();

            gameTimer2.Tick += CharakterMovements;
            gameTimer2.Interval = TimeSpan.FromMilliseconds(75);
            gameTimer2.Start();

            charakterIMG.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Charakter/newRunner_02.gif"));

            MusikAbspielen();
         
        }

        private void MusikAbspielen()
        {
            splayer = new SoundPlayer("Musik\\JumpAndRun\\ReturnOfSASHEMusik1.wav");
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
            hg2.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/HintergrundBilder/Raum3erweitert.png"));
            

            zwischenraumLinks.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/HintergrundBilder/ZwischenraumLinks.png"));
            zwischenraumMitte.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/HintergrundBilder/ZwischenraumMitte.png"));
            zwischenraumRechts.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/HintergrundBilder/ZwischenraumRechts.png"));


            

            hgGalaxy1.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/HintergrundBilder/Hintergrund.png"));
            hgGalaxy2.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/HintergrundBilder/HintergrundSpiegel.png"));
            hgDecke.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/HintergrundBilder/Decke.png"));

            hintergrund1.Fill = hg1; 
            hintergrund2.Fill = hg2;

            hintergrundGalaxy1.Fill = hgGalaxy1;
            hintergrundGalaxy2.Fill = hgGalaxy2;
            hintergrundDach1.Fill = hgDecke;
            hintergrundDach2.Fill = hgDecke;
            hintergrundBoden1.Fill = hgDecke;
            hintergrundBoden2.Fill = hgDecke;

            hintergrundZwischenraumLinks1.Fill = zwischenraumLinks;
            hintergrundZwischenraumMitte1.Fill = zwischenraumMitte;
            hintergrundZwischenraumRechts1.Fill = zwischenraumRechts;

            hintergrundZwischenraumLinks2.Fill = zwischenraumLinks;
            hintergrundZwischenraumMitte2.Fill = zwischenraumMitte;
            hintergrundZwischenraumRechts2.Fill = zwischenraumRechts;
        }



        private void OnMyButtonClickResult(object sender, RoutedEventArgs e)
        {
            var wnd = new SpielErgebnis();
            gameTimer.Stop();
            gameTimer2.Stop();
            splayer.Stop();

            wnd.Show();
            this.Close();


        }
        
        private void moveCharakter(Rectangle charakter)
        {
            if (Canvas.GetTop(charakter) < 303 && goUp == false )
            {
                Canvas.SetTop(charakter, Canvas.GetTop(charakter) + jumpspeed);
            }

        
            if(goUp == true && Canvas.GetTop(charakter) >= 200 )
            {
                Canvas.SetTop(charakter, Canvas.GetTop(charakter) - jumpspeed);
                charakterIMG.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Charakter/newRunner_02.gif"));
            }
            if (goUp == true && Canvas.GetTop(charakter) <= 200)
            {
                goUp = false;
            }

            if (goUp == false && Canvas.GetTop(charakter) < 303)
            {
                Canvas.SetTop(charakter, Canvas.GetTop(charakter) + jumpspeed);
                charakterIMG.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Charakter/newRunner_02.gif"));
            }
        }


        private void startEvents(object sender, EventArgs e)
        { 
            MoveHintergrund(hintergrund1, hintergrund2, hintergrundZwischenraumLinks1, hintergrundZwischenraumMitte1, hintergrundZwischenraumRechts1,hintergrundZwischenraumLinks2, hintergrundZwischenraumMitte2, hintergrundZwischenraumRechts2 , hintergrundDach1, hintergrundDach2, hintergrundBoden1, hintergrundBoden2, hintergrundGalaxy1, hintergrundGalaxy2, backgroundspeed, backgroundspeedGalaxy);
            moveCharakter(Charakter);
        }

        private void MoveHintergrund(Rectangle hintergrund1, Rectangle hintergrund2, Rectangle hintergrundZwischenraumLinks1, Rectangle hintergrundZwischenraumMitte1, Rectangle hintergrundZwischenraumRechts1, Rectangle hintergrundZwischenraumLinks2, Rectangle hintergrundZwischenraumMitte2, Rectangle hintergrundZwischenraumRechts2, Rectangle hintergrundDach1, Rectangle hintergrundDach2, Rectangle hintergrundBoden1, Rectangle hintergrundBoden2, Rectangle hintergrundGalaxy1, Rectangle hintergrundGalaxy2, int backgroundspeed, int backgroundspeedGalaxy)
        {
            /*Zu Beginn  hat das Element hintergrund1 (mit dem Bild Raum1.png) das Attribut Canvas.Left="-289" sobald
         * die Elemente die für das Raumschiff stehen nicht mehr in der Benutzerfläche gesehen werden, werden sie nach 
         * einer bestimmten Reihenfolge and das Ende gehängt.
         * 
         */


            Canvas.SetLeft(hintergrund1, Canvas.GetLeft(hintergrund1) - backgroundspeed);
            Canvas.SetLeft(hintergrund2, Canvas.GetLeft(hintergrund2) - backgroundspeed);
            
            Canvas.SetLeft(hintergrundDach1, Canvas.GetLeft(hintergrundDach1) - backgroundspeed);
            Canvas.SetLeft(hintergrundDach2, Canvas.GetLeft(hintergrundDach2) - backgroundspeed);
            Canvas.SetLeft(hintergrundBoden1, Canvas.GetLeft(hintergrundBoden1) - backgroundspeed);
            Canvas.SetLeft(hintergrundBoden2, Canvas.GetLeft(hintergrundBoden2) - backgroundspeed);
            
            Canvas.SetLeft(hintergrundZwischenraumLinks1, Canvas.GetLeft(hintergrundZwischenraumLinks1) - backgroundspeed);
            Canvas.SetLeft(hintergrundZwischenraumMitte1, Canvas.GetLeft(hintergrundZwischenraumMitte1) - backgroundspeed);
            Canvas.SetLeft(hintergrundZwischenraumRechts1, Canvas.GetLeft(hintergrundZwischenraumRechts1) - backgroundspeed);

            Canvas.SetLeft(hintergrundZwischenraumLinks2, Canvas.GetLeft(hintergrundZwischenraumLinks2) - backgroundspeed);
            Canvas.SetLeft(hintergrundZwischenraumMitte2, Canvas.GetLeft(hintergrundZwischenraumMitte2) - backgroundspeed);
            Canvas.SetLeft(hintergrundZwischenraumRechts2, Canvas.GetLeft(hintergrundZwischenraumRechts2) - backgroundspeed);

            Canvas.SetLeft(hintergrundGalaxy1, Canvas.GetLeft(hintergrundGalaxy1) - backgroundspeedGalaxy);
            Canvas.SetLeft(hintergrundGalaxy2, Canvas.GetLeft(hintergrundGalaxy2) - backgroundspeedGalaxy);


            if (Canvas.GetLeft(hintergrund1) < -1579)
            {
                Canvas.SetLeft(hintergrund1, Canvas.GetLeft(hintergrundZwischenraumRechts2) + hintergrundZwischenraumRechts2.Width - 80);
            }
            if(Canvas.GetLeft(hintergrundZwischenraumLinks1) < -25-5)
            {
                Canvas.SetLeft(hintergrundZwischenraumLinks1, Canvas.GetLeft(hintergrund1) + hintergrund1.Width -80);
            }
            if(Canvas.GetLeft(hintergrundZwischenraumMitte1) < -421-5)
            {
                Canvas.SetLeft(hintergrundZwischenraumMitte1, Canvas.GetLeft(hintergrundZwischenraumLinks1) + hintergrundZwischenraumLinks1.Width);
            }
            if(Canvas.GetLeft(hintergrundZwischenraumRechts1) < -34-5)
            {
                Canvas.SetLeft(hintergrundZwischenraumRechts1, Canvas.GetLeft(hintergrundZwischenraumMitte1) + hintergrundZwischenraumMitte1.Width);
            }
         
            if (Canvas.GetLeft(hintergrund2) < -1579)
            {
                Canvas.SetLeft(hintergrund2, Canvas.GetLeft(hintergrundZwischenraumRechts1) + hintergrundZwischenraumRechts1.Width - 80);
            }
            if (Canvas.GetLeft(hintergrundZwischenraumLinks2) < -25-5)
            {
                Canvas.SetLeft(hintergrundZwischenraumLinks2, Canvas.GetLeft(hintergrund2) + hintergrund2.Width - 80);
            }
            if(Canvas.GetLeft(hintergrundZwischenraumMitte2) < -421-5)
            {
                Canvas.SetLeft(hintergrundZwischenraumMitte2, Canvas.GetLeft(hintergrundZwischenraumLinks2) + hintergrundZwischenraumLinks2.Width);
            }
            if(Canvas.GetLeft(hintergrundZwischenraumRechts2) < - 34-5)
            {
                Canvas.SetLeft(hintergrundZwischenraumRechts2, Canvas.GetLeft(hintergrundZwischenraumMitte2) + hintergrundZwischenraumMitte2.Width);
            }

            if (Canvas.GetLeft(hintergrundGalaxy1) < -5000)
            {
                Canvas.SetLeft(hintergrundGalaxy1, Canvas.GetLeft(hintergrundGalaxy2) + hintergrundGalaxy2.Width);
            }
            if (Canvas.GetLeft(hintergrundGalaxy2) < -5000)
            {
                Canvas.SetLeft(hintergrundGalaxy2, Canvas.GetLeft(hintergrundGalaxy1) + hintergrundGalaxy1.Width );
            }
            if (Canvas.GetLeft(hintergrundDach1) < -5000)
            {
                Canvas.SetLeft(hintergrundDach1, Canvas.GetLeft(hintergrundDach2) + hintergrundDach2.Width);
            }
            if (Canvas.GetLeft(hintergrundDach2) < -5000)
            {
                Canvas.SetLeft(hintergrundDach2, Canvas.GetLeft(hintergrundDach1) + hintergrundDach1.Width);
            }
            if (Canvas.GetLeft(hintergrundBoden1) < -5000)
            {
                Canvas.SetLeft(hintergrundBoden1, Canvas.GetLeft(hintergrundBoden2) + hintergrundBoden2.Width);
            }
            if (Canvas.GetLeft(hintergrundBoden2) < -5000)
            {
                Canvas.SetLeft(hintergrundBoden2, Canvas.GetLeft(hintergrundBoden1) + hintergrundBoden1.Width);
            }




        }

        private void CharakterMovements(object sender, EventArgs e)
        {
            if (goUp == false)
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
            }

            Charakter.Fill = charakterIMG;
            i++;
            if (i > 7)
            {
                i = 0;
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
           
  
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space && goUp == false && Canvas.GetTop(Charakter) == 303)
            {
                goUp = true;
                

            }
        }
    }

    

}

