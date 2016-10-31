using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Inject_some_SQL
{
    public class Configuration
    {
        private string title = "DEFAULT";
        /// <summary>
        /// Bezeichnung der Konfiguration
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        #region Unterklassen
        /// <summary>
        /// Datenbank-Konfiguration
        /// </summary>
        public class DatabaseConf
        {
            private string provider = "MSDAORA.1";

            public string Provider
            {
                get { return provider; }
                set { provider = value; }
            }
            private string dataSource = "SODB";

            public string DataSource
            {
                get { return dataSource; }
                set { dataSource = value; }
            }
            private string login = "USERNAME";

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