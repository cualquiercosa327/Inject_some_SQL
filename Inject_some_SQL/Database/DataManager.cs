using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Inject_some_SQL.Database
{
    class DataManager
    {
        string myConnectionString = null;
        MySqlConnection conn = null;


        public void ConnectToDB()
        {
            myConnectionString = "server=127.0.0.1;uid=root;pwd=Deinemudda1221;database=world;";

            conn = new MySqlConnection();
            conn.ConnectionString = myConnectionString;
            conn.Open();
        }
    }
}
