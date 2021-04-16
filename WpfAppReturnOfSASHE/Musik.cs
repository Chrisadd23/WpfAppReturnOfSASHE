using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppReturnOfSASHE
{
    public class Musik
    {
        private string name;
        private int zeitSekunden;

        private System.Media.SoundPlayer splayer;

        public Musik()
        {
            splayer = new System.Media.SoundPlayer("Musik\\JumpAndRun\\ReturnOfSASHEMusik.wav");
        }


        public void Musikabspielen()
        {
            splayer.Play();
        }

        public void Musikstop()
        {
            splayer.Stop();
        }




    }
}
