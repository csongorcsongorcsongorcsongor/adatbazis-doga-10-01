using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp9
{
    class databaseHandler
    {
        public List<aru> aruk = new List<aru>();

        MySqlConnection connection;
        
        public databaseHandler()
        {
            string host = "localhost";
            string user = "root";
            string password = "";
            string database = "bazisss";

            string connectionString = $"server={host};user={user};password={password};database={database}";
            connection = new MySqlConnection(connectionString);


        }
        string tableName = "pek";
        public void readAll()
        {
            try
            {
                connection.Open();
                string query = $"SELECT * FROM {tableName}";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader read = command.ExecuteReader();
                while (read.Read())
                {
                    aru onearu = new aru();
                    onearu.id = read.GetInt32(read.GetOrdinal("id"));
                    onearu.nev = read.GetString(read.GetOrdinal("nev"));
                    onearu.mennyiseg = read.GetInt32(read.GetOrdinal("mennyiseg"));
                    onearu.ar = read.GetInt32(read.GetOrdinal("ar"));
                    aruk.Add(onearu);
                }
                read.Close();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void addone(string nev, int mennyiseg, int ar)
        {
            try
            {
                connection.Open();
                string query = $"INSERT INTO {tableName} (nev, mennyiseg, ar) VALUES ('{nev}','{mennyiseg}','{ar}')";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void delone(aru aru)
        {
            try
            {
                connection.Open();
                string query = $"DELETE FROM {tableName} where id = {aru.id}";
                aruk.Remove(aru);
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
    class aru
    {
        public int id { get; set; }
        public string nev { get; set; }
        public int mennyiseg { get; set; }
        public int ar { get; set; }
    }
}
