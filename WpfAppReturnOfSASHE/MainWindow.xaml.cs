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
        int backgroundspeed = 7;
        int backgroundspeedGalaxy = 8;
        int jumpspeed = 9;
        int hindernisspeed = 10;
        int i;
        int x;
        int anzahlBeschleunigen = 0;

        bool goUp;

        Random platziereHinderniss = new Random();

        Rect hindernis1Box;
        Rect hindernis2Box;
        Rect hindernis3Box;
        Rect charakterBox;


        ImageBrush hg1 = new ImageBrush();
        ImageBrush hg2 = new ImageBrush();      
        ImageBrush hgGalaxy1 = new ImageBrush();
        ImageBrush hgGalaxy2 = new ImageBrush();
        ImageBrush hgDecke = new ImageBrush();

        ImageBrush charakterIMG = new ImageBrush();

        ImageBrush hindernisIMG = new ImageBrush();

        ImageBrush zwischenraumLinks = new ImageBrush();
        ImageBrush zwischenraumMitte = new ImageBrush();
        ImageBrush zwischenraumRechts = new ImageBrush();


        DispatcherTimer gameTimer = new DispatcherTimer();
        DispatcherTimer gameTimer2 = new DispatcherTimer();
        DispatcherTimer gameTimer3 = new DispatcherTimer();
        DispatcherTimer gameTimer4 = new DispatcherTimer();
        DispatcherTimer gameTimer5 = new DispatcherTimer();


        private SoundPlayer splayer;
        private MediaPlayer mediaPlayer = new MediaPlayer();

        public MainWindow()
        {
            InitializeComponent();
            HintergrundEinrichten();
            myCanvas.Focus();
            gameTimer.Tick += startEvents;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();

            gameTimer2.Tick += AnimateCharakter;
            gameTimer2.Interval = TimeSpan.FromMilliseconds(75);
            gameTimer2.Start();

            gameTimer3.Tick += HindernisMovements;
            gameTimer3.Interval = TimeSpan.FromMilliseconds(150);
            gameTimer3.Start();

            charakterIMG.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Charakter/newRunner_02.gif"));
            hindernisIMG.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Hindernis/Gaswolke7.png"));
            splayer = new SoundPlayer("Musik\\JumpAndRun\\ReturnOfSASHEMusik.wav");
            splayer.Play();

            gameTimer4.Tick += MusikAbspielen;
            gameTimer4.Interval = TimeSpan.FromSeconds(395);
            gameTimer4.Start();

            gameTimer5.Tick += levelSchwierigkeit;
            gameTimer5.Interval = TimeSpan.FromSeconds(30);
            gameTimer5.Start();
         
        }

        private void levelSchwierigkeit(object sender, EventArgs e)
        {
            if(anzahlBeschleunigen < 5)
            {
                backgroundspeed += 1;
                backgroundspeedGalaxy += 2;
                if(anzahlBeschleunigen % 2 == 0)
                {
                    hindernisspeed += 1;
                }
                anzahlBeschleunigen++;
            }
            
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


        private void MusikAbspielen(object sender, EventArgs e)
        {
            splayer.Play();
        }

        private void OnMyButtonClickResult(object sender, RoutedEventArgs e)
        {
            stopGame();
        }

        private void startEvents(object sender, EventArgs e)
        {
            MoveHintergrund(hintergrund1, hintergrund2, hintergrundZwischenraumLinks1, hintergrundZwischenraumMitte1, hintergrundZwischenraumRechts1, hintergrundZwischenraumLinks2, hintergrundZwischenraumMitte2, hintergrundZwischenraumRechts2, hintergrundDach1, hintergrundDach2, hintergrundBoden1, hintergrundBoden2, hintergrundGalaxy1, hintergrundGalaxy2, backgroundspeed, backgroundspeedGalaxy);
            moveCharakter(Charakter,Hindernis1,Hindernis2,Hindernis3);
            moveHindernis(Hindernis1,Hindernis2,Hindernis3,anzahlBeschleunigen);
        }

        private void HindernisMovements(object sender, EventArgs e)
        {
            animateHinderniss(Hindernis1,Hindernis2,Hindernis3);
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
            if (Canvas.GetLeft(hintergrundZwischenraumLinks1) < -25 - 5)
            {
                Canvas.SetLeft(hintergrundZwischenraumLinks1, Canvas.GetLeft(hintergrund1) + hintergrund1.Width - 80);
            }
            if (Canvas.GetLeft(hintergrundZwischenraumMitte1) < -421 - 5)
            {
                Canvas.SetLeft(hintergrundZwischenraumMitte1, Canvas.GetLeft(hintergrundZwischenraumLinks1) + hintergrundZwischenraumLinks1.Width);
            }
            if (Canvas.GetLeft(hintergrundZwischenraumRechts1) < -34 - 5)
            {
                Canvas.SetLeft(hintergrundZwischenraumRechts1, Canvas.GetLeft(hintergrundZwischenraumMitte1) + hintergrundZwischenraumMitte1.Width);
            }

            if (Canvas.GetLeft(hintergrund2) < -1579)
            {
                Canvas.SetLeft(hintergrund2, Canvas.GetLeft(hintergrundZwischenraumRechts1) + hintergrundZwischenraumRechts1.Width - 80);
            }
            if (Canvas.GetLeft(hintergrundZwischenraumLinks2) < -25 - 5)
            {
                Canvas.SetLeft(hintergrundZwischenraumLinks2, Canvas.GetLeft(hintergrund2) + hintergrund2.Width - 80);
            }
            if (Canvas.GetLeft(hintergrundZwischenraumMitte2) < -421 - 5)
            {
                Canvas.SetLeft(hintergrundZwischenraumMitte2, Canvas.GetLeft(hintergrundZwischenraumLinks2) + hintergrundZwischenraumLinks2.Width);
            }
            if (Canvas.GetLeft(hintergrundZwischenraumRechts2) < -34 - 5)
            {
                Canvas.SetLeft(hintergrundZwischenraumRechts2, Canvas.GetLeft(hintergrundZwischenraumMitte2) + hintergrundZwischenraumMitte2.Width);
            }

            if (Canvas.GetLeft(hintergrundGalaxy1) < -5000)
            {
                Canvas.SetLeft(hintergrundGalaxy1, Canvas.GetLeft(hintergrundGalaxy2) + hintergrundGalaxy2.Width);
            }
            if (Canvas.GetLeft(hintergrundGalaxy2) < -5000)
            {
                Canvas.SetLeft(hintergrundGalaxy2, Canvas.GetLeft(hintergrundGalaxy1) + hintergrundGalaxy1.Width);
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

        private void moveHindernis(Rectangle hindernis1, Rectangle hindernis2,Rectangle hindernis3, int anzahlBeschleunigen)
        {
            Canvas.SetLeft(hindernis1, Canvas.GetLeft(hindernis1) - hindernisspeed);

            if(anzahlBeschleunigen >=2)
            {
                Canvas.SetLeft(hindernis2, Canvas.GetLeft(hindernis2) - hindernisspeed);
            }
            if(anzahlBeschleunigen >= 4)
            {
                Canvas.SetLeft(hindernis3, Canvas.GetLeft(hindernis3) - hindernisspeed);
            }
            

            if (Canvas.GetLeft(hindernis1) < -60 && anzahlBeschleunigen < 2)
            {
                Canvas.SetLeft(hindernis1, 900);
            }
            else if(Canvas.GetLeft(hindernis1) < -60 && anzahlBeschleunigen < 4)
            {
                Canvas.SetLeft(hindernis1, Canvas.GetLeft(hindernis2) + platziereHinderniss.Next(250, 450));
            }
            else if(Canvas.GetLeft(hindernis1) < -60 && anzahlBeschleunigen >= 4)
            {
                Canvas.SetLeft(hindernis1, Canvas.GetLeft(hindernis3) + platziereHinderniss.Next(250, 450));
            }

            if(anzahlBeschleunigen >= 2 && Canvas.GetLeft(hindernis2) < -60)
            {
                    Canvas.SetLeft(hindernis2, Canvas.GetLeft(hindernis1) + platziereHinderniss.Next(250,450));
            }
            if(anzahlBeschleunigen >= 4 && Canvas.GetLeft(hindernis3) < -60)
            {
                    Canvas.SetLeft(hindernis3, Canvas.GetLeft(hindernis2) + platziereHinderniss.Next(250, 450));
            }
        }

       
        
        private void moveCharakter(Rectangle charakter,Rectangle hindernis1, Rectangle hindernis2, Rectangle hindernis3)
        {
            if (Canvas.GetTop(charakter) < 303 && goUp == false )
            {
                Canvas.SetTop(charakter, Canvas.GetTop(charakter) + jumpspeed);
            }

        
            if(goUp == true && Canvas.GetTop(charakter) >= 150 )
            {
                Canvas.SetTop(charakter, Canvas.GetTop(charakter) - jumpspeed);
                
            }
            if (goUp == true && Canvas.GetTop(charakter) <= 150)
            {
                goUp = false;
            }

            if (goUp == false && Canvas.GetTop(charakter) < 303)
            {
                Canvas.SetTop(charakter, Canvas.GetTop(charakter) + jumpspeed);
                charakterIMG.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Charakter/newRunner_02.gif"));
            }

            charakterBox = new Rect(Canvas.GetLeft(charakter), Canvas.GetTop(charakter), charakter.Width - 50, charakter.Height - 50);
            hindernis1Box = new Rect(Canvas.GetLeft(hindernis1), Canvas.GetTop(hindernis1), hindernis1.Width, hindernis1.Height);
            hindernis2Box = new Rect(Canvas.GetLeft(hindernis2), Canvas.GetTop(hindernis2), hindernis2.Width, hindernis2.Height);
            hindernis3Box = new Rect(Canvas.GetLeft(hindernis3), Canvas.GetTop(hindernis3), hindernis3.Width, hindernis3.Height);

            
            if(charakterBox.IntersectsWith(hindernis1Box) || charakterBox.IntersectsWith(hindernis2Box) || charakterBox.IntersectsWith(hindernis3Box))
            {
                stopGame();
            }
            
        }

        private void stopGame()
        {
            gameTimer.Stop();
            gameTimer2.Stop();
            gameTimer3.Stop();
            gameTimer4.Stop();
            gameTimer5.Stop();
            splayer.Stop();

            var wnd = new SpielErgebnis();

            wnd.Show();
            this.Close();
        }

        private void animateHinderniss(Rectangle hindernis1,Rectangle hindernis2, Rectangle hindernis3)
        {
            switch (x)
            {
                case 0:
                    hindernisIMG.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Hindernis/Gaswolke0.png"));

                    break;
                case 1:
                    hindernisIMG.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Hindernis/Gaswolke1.png"));
                    break;
                case 2:
                    hindernisIMG.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Hindernis/Gaswolke2.png"));
                    break;
                case 3:
                    hindernisIMG.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Hindernis/Gaswolke3.png"));
                    break;
                case 4:
                    hindernisIMG.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Hindernis/Gaswolke4.png"));
                    break;
                case 5:
                    hindernisIMG.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Hindernis/Gaswolke5.png"));
                    break;
                case 6:
                    hindernisIMG.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Hindernis/Gaswolke6.png"));
                    break;
                case 7:
                    hindernisIMG.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Hindernis/Gaswolke7.png"));
                    break;
            }

            hindernis1.Fill = hindernisIMG;
            hindernis2.Fill = hindernisIMG;
            hindernis3.Fill = hindernisIMG;

            x++;
            if (x > 7)
            {
                x = 0;
            }
        }


        private void AnimateCharakter(object sender, EventArgs e)
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
                charakterIMG.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/Charakter/newRunner_02.gif"));

            }
        }
    }

    

}

