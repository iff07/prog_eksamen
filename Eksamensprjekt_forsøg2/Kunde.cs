using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Eksamensprjekt_forsøg2
{
    public class Kunde
    {
        string Fornavn;
        string Efternavn;
        int Alder;
        string Brugernavn;
        string Kodeord;

        public Kunde SignUp(string fornavn, string efternavn, int alder, string brugernavn, string kodeord)
        {
            string connectionString =
                      "server=localhost;database=Sportsbooking;uid=root;pwd=Sune1212;";
            MySqlConnection conn = new MySqlConnection(connectionString);

            // MySqlCommand repræsenterer en SQL-forespørgsel vi vil sende til databasen
            MySqlCommand cmd = null;

            // MySqlDataReader svarer til et ResultSet fra databasen
            // Den bruges til at læse resultatet én række ad gangen
            MySqlDataReader reader = null;


            try
            {
                // Åbner forbindelsen til databasen
                conn.Open();

                // SQL-forespørgslen vi sender til databasen
                string query = "INSERT INTO Bog (Titel, Forfatter, Udgivelsesaar)" +
                    "VALUES (@titel, @forfatter, @udgivelsesår);";


                // Opretter kommando-objektet og kobler det til forbindelsen
                cmd = new MySqlCommand(query, conn);

                // Parameter (@id) beskytter mod SQL injection
                // og sender værdien sikkert til databasen
                cmd.Parameters.AddWithValue("@Fornavn", fornavn);
                cmd.Parameters.AddWithValue("@Efternavn", efternavn);
                cmd.Parameters.AddWithValue("@alder", alder);

                // ExecuteReader sender SELECT-forespørgslen til databasen
                // og returnerer et result set (som DataReader læser)
                reader = cmd.ExecuteReader();


                if (reader.Read())
                {
                    // Her mapper vi database-rækken over i et C# objekt
                    Kunde k = new Kunde();

                    // reader["Kolonnenavn"] henter værdien fra result set
                    // Convert bruges fordi databasen returnerer et object
                    k.Fornavn = Convert.ToString(reader["Fornavn"]);
                    k.Efternavn = Convert.ToString(reader["Efternavn"]);
                    k.Alder = Convert.ToInt32(reader["Alder"]);
                    k.Brugernavn = Convert.ToString(reader["Brugernavn"]);
                    k.Kodeord = Convert.ToString(reader["Kodeord"]);

                    // Vi returnerer vores C# objekt
                    return k;
                }

            }
            finally
            {
                // Lukker reader (lukker result set)
                if (reader != null)
                    reader.Close();

                // Lukker forbindelsen til databasen
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return null;

        }

    }
    
    public void Login(string brugernavn, string kodeord)
    {






    }
    
}
