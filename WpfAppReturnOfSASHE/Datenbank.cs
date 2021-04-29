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
        MySqlConnection connection = null;
        MySqlCommand command = null;
        MySqlDataReader reader = null;
        MySqlDataAdapter adapter = null;
        

        Spieler spieler;
        int versuche = 0;

  
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
                MessageBox.Show(ex +"");
            }
        }

        private void Command()
        {
            command = connection.CreateCommand();
            command.CommandText = "spieler";
            command.CommandType =  System.Data.CommandType.TableDirect;
        }

        public Spieler checkAnmeldung(string gui_Username, string gui_Userpasswort)
        {
            connection.Open();
            Command();
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
                        versuche++;
                        reader.Close();
                    }
                    
                }

            }//Ende While reader

            return null;
        }

        public Spieler Registrieren(string vorname, string nachname, string benutzername, string benutzerpasswort, string benutzerpasswortErneut)
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
                     return null;
                 }
                 else
                 {
                    MessageBox.Show(reader.GetString(3) + " " + benutzername);
                    Stop();
                    
                    command.CommandText = "INSERT INTO spieler (firstname,lastname,username,userpassword,Score,locked) VALUES ('" + vorname + "','" + nachname + "','" + benutzername + "','" + benutzerpasswort + "',0,0);";
                    connection.Open();
                    command.ExecuteNonQuery();
                    reader.Close();

                    break;
                }
             }

            return null;
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
