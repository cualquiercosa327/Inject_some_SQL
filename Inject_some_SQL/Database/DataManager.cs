using System;
using MySql.Data.MySqlClient;
using System.Data;

namespace Inject_some_SQL.Database
{
    public class DataManager
    {
        private Configuration configuration = null;

        private String dbConnectionString;
        private MySqlConnection dbConnection;
        private String dbServer;
        private String dbUser;
        private String dbPassword;
        private String dbDataBase;
        private bool connected = false;
        private MySqlDataAdapter itemListAdapter;

        /// <summary>
        /// Gibt an, ob die Verbindung zur Datenbank besteht
        /// </summary>
        public bool Connected
        {
            get { return connected; }
            set { connected = value; }
        }


        // ~~~~~~~ KONSTRUKTOR ~~~~~~~
        public DataManager(Configuration conf)
        {
            this.configuration = conf;

            // Datenbank-Verbindungsdaten laden
            dbServer = conf.Database.Server;
            dbUser = conf.Database.Login;
            dbPassword = conf.Database.Password;
            dbDataBase = conf.Database.DataBase;
        }

        // -----------------------------------------------
        // Datenbankverbindung herstellen
        public void Connect()
        {
            connected = false;
            Connect(dbServer, dbUser, dbPassword, dbDataBase);
            this.connected = true;
        }

        // -----------------------------------------------
        // Datenbankverbindung herstellen
        private void Connect(String sServer, String sUid, String sPws, String sDataBase)
        {
            // Connectionstring zusammenbauen
            dbConnectionString = "Server=" + sServer + ";";
            dbConnectionString += "uid=" + sUid + ";";
            dbConnectionString += "pwd=" + sPws + ";";
            dbConnectionString += "database=" + sDataBase + ";";

            // Verbindung herstellen
            dbConnection = new MySqlConnection(dbConnectionString);
            dbConnection.Open();
        }

        public void LoadItemsFromDB(string sAbez1, string sAbez2, DataTable table) {
            MySqlCommand cmd = new MySqlCommand();

            

            cmd.Connection = dbConnection;
            cmd.CommandText = " SELECT art.artnr, art.abez1, art.abez2, art.kosten, wtyp.bezeichnung, concat(ben.vorname, ' ', ben.name)" +
                              " FROM artikel art, werkzeug_typen wtyp, benutzer ben " +
                              " WHERE art.werk_typ = wtyp.lfdnr AND art.erstellungs_login = ben.id " +
                              " AND art.abez1 like '" + sAbez1 + "%' AND art.abez2 like '" + sAbez2 + "%'; ";

            itemListAdapter = new MySqlDataAdapter(cmd);
            itemListAdapter.Fill(table);
         }
    }
}