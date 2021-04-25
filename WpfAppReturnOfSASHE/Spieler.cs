using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppReturnOfSASHE
{
    public class Spieler
    {
        private string userName;
        private string vorname;
        private string nachname;
        private int score = 0;
        private DateTime spielStart;

        public string UserName { get => userName; set => userName = value; }
        public string Vorname { get => vorname; set => vorname = value; }
        public string Nachname { get => nachname; set => nachname = value; }
        public int Score { get => score; set => score = value; }

        public Spieler()
        {
            spielStart = DateTime.Now;
        }

        

        
    }
}
