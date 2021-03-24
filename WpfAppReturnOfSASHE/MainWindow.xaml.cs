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

using System.Windows.Threading;

namespace WpfAppReturnOfSASHE
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int backgroundspeed = 12;

        DispatcherTimer gameTimer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            HintergrundEinrichten();
            myCanvas.Focus();
            gameTimer.Tick += moveHintergrund;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();
            
            
        }

        private void moveHintergrund(object sender, EventArgs e)
        {
            /*Zu Beginn  hat das Element hintergrund1 (mit dem Bild Raum1.png) das Attribut Canvas.Left="-289" sobald
             * die Elemente die für das Raumschiff stehen nicht mehr in der Benutzerfläche gesehen werden, werden sie nach 
             * einer bestimmten Reihenfolge and das Ende gehängt.
             * 
             */

            Canvas.SetLeft(hintergrund1, Canvas.GetLeft(hintergrund1) - backgroundspeed);
            Canvas.SetLeft(hintergrundZwischenraum, Canvas.GetLeft(hintergrundZwischenraum) - backgroundspeed);
            Canvas.SetLeft(hintergrund3, Canvas.GetLeft(hintergrund3) - backgroundspeed);


            if (Canvas.GetLeft(hintergrund1) < -289 - (1579 - 289))
            {
                Canvas.SetLeft(hintergrund1, 1770 
            );
            }
            /*
            if (Canvas.GetLeft(hintergrundZwischenraum) < -289 - (376))
            {
                Canvas.SetLeft(hintergrundZwischenraum, 1770);
            }
            if (Canvas.GetLeft(hintergrund3) < -289 - (1579 - 289))
            {
                Canvas.SetLeft(hintergrund3, 1770);
            }
            */
        }

        public void HintergrundEinrichten()
        {
            hintergrund1.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/HintergrundBilder/Raum1.png"))
            };

            hintergrundZwischenraum.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/HintergrundBilder/Zwischenraum.png"))

            };
            

            hintergrund3.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Bilder/HintergrundBilder/Raum2.png"))
            };
        }

      


    }
}
