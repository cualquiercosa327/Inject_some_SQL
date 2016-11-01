using System;
using System.Xml.Serialization;
using System.IO;

namespace Inject_some_SQL
{
    public class Configuration
    {
        #region Unterklassen
        /// <summary>
        /// Datenbank-Konfiguration (MY-SQL)
        /// </summary>
        public class DatabaseConf
        {
            private string server = "127.0.0.1";

            public string Server
            {
                get { return server; }
                set { server = value; }
            }

            private string login = "username";

            public string Login
            {
                get { return login; }
                set { login = value; }
            }
            private string password = "password";

            public string Password
            {
                get { return password; }
                set { password = value; }
            }

            private string dataBase = "local";

            public string DataBase
            {
                get { return dataBase; }
                set { dataBase = value; }
            }
        }
        #endregion

        public const string DEFAULT_CONFIG_FILE_NAME = "Configuration.xml";
        private DatabaseConf database = new DatabaseConf();
        public DatabaseConf Database
        {
            get { return database; }
            set { database = value; }
        }

        #region Serialisierungs-Methoden

        /// <summary>
        /// Liest eine Konfigurationsdatei ein
        /// </summary>
        public static Configuration Load(string filename)
        {
            // Serializer erzeugen
            System.Xml.Serialization.XmlSerializer xmlSerializer = new XmlSerializer(typeof(Configuration));

            Object obj = xmlSerializer.Deserialize(new System.Xml.XmlTextReader(new StreamReader(filename)));
            Configuration conf = (Configuration)obj;

            return conf;
        }

        /// <summary>
        /// Speichert das Objekt in der angegebenen Datei
        /// </summary>
        /// <param name="filename"></param>
        public void Save(string filename)
        {
            // Serializer erzeugen
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Configuration));

            TextWriter w = new StreamWriter(filename);
            xmlSerializer.Serialize(w, this);
            w.Close();

        }
        #endregion
    }
}