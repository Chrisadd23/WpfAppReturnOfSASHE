using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppReturnOfSASHE
{
    public class Spieler
    {
        private string name;
        private int score;
        private DateTime spielStart;


        public Spieler()
        {
            spielStart = DateTime.Now;
        }

        public string Name { get => name; set => name = value; }
        public int Score { get => score;  }

        public void addPoint()
        {
            this.score++;
        }

    }
}
