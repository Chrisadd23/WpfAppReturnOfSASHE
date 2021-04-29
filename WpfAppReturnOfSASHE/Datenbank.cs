using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;     //--------------wichtig und notwendig für MySql verbindung/nutzung!!!
using MySql.Data;


namespace WpfAppReturnOfSASHE
{
    public class Datenbank
    {
        string myConnectionString = "Server=localhost;Uid=root;Pwd=;database=csprojektros;"; //auf xampp basierend
        
        MySqlConnection connection;
        MySqlCommand command;
        MySqlDataReader reader;
       
        

        Spieler spieler;
        

  
        private string account;

        public Datenbank()
        {
            OpenConnect();  
        }

       
        private void OpenConnect()
        {
            try
            {
                connection = new MySqlConnection(myConnectionString);
            }
            catch(MySqlException ex)
            {
                string QueryCreateDB = "create database csprojektros;";
                MySqlConnection conDataBase = new MySqlConnection(myConnectionString);
                MySqlCommand cmdDataBase = new MySqlCommand(QueryCreateDB, conDataBase);
                
                try
                {
                    conDataBase.Open();
                    cmdDataBase.ExecuteNonQuery();
                    conDataBase.Close();


                    string createTable = "CREATE TABLE spieler(id INT(6) UNSIGNED AUTO_INCREMENT PRIMARY KEY,"+
                        "firstname VARCHAR(30) NOT NULL, lastname VARCHAR(30) NOT NULL,"+
                        "username VARCHAR(30) NOT NULL, userpassword VARCHAR(15) NOT NULL,"+
                        "Score INT(15) NOT NULL, locked INT(1) NOT NULL)";
                    MySqlConnection con = new MySqlConnection(myConnectionString);
                    MySqlCommand cmdTable = new MySqlCommand(createTable, con);
                    con.Open();
                    cmdDataBase.ExecuteNonQuery();
                    con.Close();

                }
                catch(MySqlException e)
                {
                    MessageBox.Show(e + "");
                }

                MessageBox.Show(ex +"");
            }
        }

        private void Command()
        {
            command = connection.CreateCommand();
        }

        public Spieler checkAnmeldung(string gui_Username, string gui_Userpasswort)
        {
            connection.Open();
            account = "SElECT * FROM spieler;";
            command = new MySqlCommand(account, connection);
            reader = command.ExecuteReader();

            while(reader.Read())
            {
                if (reader.GetString(3).Equals(gui_Username) && reader.GetString(4).Equals(gui_Userpasswort))
                {
                    spieler = new Spieler();
                    spieler.Vorname = reader.GetString(1);
                    spieler.Nachname = reader.GetString(2);
                    spieler.UserName = reader.GetString(3);
                    spieler.Passwort = reader.GetString(4);
                    spieler.Score = Convert.ToInt32(check(reader[5]));

                    return spieler;

                }
                else if (reader.GetString(3).Equals(gui_Username))
                {
                    if (!(reader.GetString(4).Equals(gui_Userpasswort)))
                    {
                        MessageBox.Show("Sie haben das Passwort falsch eingegeben!");
                        reader.Close();
                    }
                    
                }
                

            }//Ende While reader
            MessageBox.Show("Bitte Registirieren Sie sich oder spielen Sie offline");
            return null;
        }

        public void Registrieren(string vorname, string nachname, string benutzername, string benutzerpasswort, string benutzerpasswortErneut)
        {
             connection.Open();
             account = "SElECT * FROM spieler;";
             command = new MySqlCommand(account, connection);
             reader = command.ExecuteReader();
             while (reader.Read())
             {

                 if (reader.GetString(3).Equals(benutzername))
                 {
                     MessageBox.Show(benutzername + " ist schon vergeben");
                    break;
                 }
                 else
                 {
                    MessageBox.Show(reader.GetString(3) + " " + benutzername);
                    Stop();
                    
                    account = "INSERT INTO spieler (firstname,lastname,username,userpassword,Score,locked) VALUES ('" + vorname + "','" + nachname + "','" + benutzername + "','" + benutzerpasswort + "',0,0);";
                    connection.Open();
                    command = new MySqlCommand(account, connection);
                    command.ExecuteNonQuery();
                    

                    break;
                }
             }

            reader.Close();
        }

        public List<Spieler> RanglisteSpieler()
        {
            List<Spieler> listSpieler = new List<Spieler>();

            try
            {
          
                connection.Open();
                string cmd = "SELECT * FROM spieler ORDER BY Score DESC;";
                command = new MySqlCommand(cmd, connection);
                reader = command.ExecuteReader();



                while (reader.Read())
                {
                    if (listSpieler.Count < 5)
                    {
                        int i = 1;
                        Spieler spieler = new Spieler();
                        spieler.Vorname = reader.GetString(i++);
                        spieler.Nachname = reader.GetString(i++);
                        spieler.UserName = reader.GetString(i++);
                        i++;
                        spieler.Score = Convert.ToInt32(reader.GetValue(i++));
                        Console.WriteLine(spieler.ToString());

                        listSpieler.Add(spieler);
                    }
                    else
                    {

                        break;
                    }
                }
                if(listSpieler.Count < 5)
                {
                    while(listSpieler.Count < 5)
                    {
                        listSpieler.Add(new Spieler());
                    }
                }

                reader.Close();
            
            }
            catch(MySqlException ex)
            {
                Console.WriteLine(ex + "");
            }
            


            return listSpieler;
            
        }


        public void UpdateScore(Spieler spieler)
        {
            string cmd = "UPDATE spieler SET Score = "+spieler.Score+" WHERE username = '"+ spieler.UserName+"' ;";
            connection.Open();
            command = new MySqlCommand(cmd, connection);
            command.ExecuteNonQuery();
            
        }

        private object check(object o)
        {
            if(o != DBNull.Value)
            {
                return o;
            }
            else
            {
                return null;
            }
        }

        public void Stop()
        {
            connection.Close();
        }

    }
}
