using System;
using System.Windows.Forms;
using Inject_some_SQL.Database;
using System.IO;

/* Test-Anwendung für SQL-Injection
 * Bewusst werden mehrere Sicherheitslücken eingebaut um SQL-Injection demonstrieren zu können
 * 
 * Work_from_work-Branche um von der Arbeit aus zu arbeiten
 * 
 * by Tim Lukas Förster 
 * 31.10.2016 - 
 */


namespace Inject_some_SQL
{
    public partial class frmMain : Form
    {
        private Configuration config = null;
        private DataManager datamgr = null;

        public frmMain()
        {
            InitializeComponent();
            InitConfiguration();
            InitDataManager();
        }

        #region Datenbank - Konfiguration laden und Verbindung herstellen
        /// <summary>
        /// Läd die (Datenbank-) Konfiguration
        /// </summary>
        private void InitConfiguration()
        {
            // Konfigurationsdatei
            string configFile = Configuration.DEFAULT_CONFIG_FILE_NAME;

            // Prüfen, ob eine Konfigurationsdatei vorhanden ist...
            try
            {
                if (!File.Exists(configFile))
                {
                    this.config = new Configuration();

                    // ... wenn nicht: Konfigurationsdatei erstellen
                    config.Save(configFile);
                    MessageBox.Show("Es wurde eine neue Konfigurationsdatei erstellt, diese muss angepasst werden!\r\n" + configFile);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Konfigurationsadtei konnte nicht erstellt werden!\r\n\r\n" + e.Message);
            }

            // Konfiguration laden
            try
            {
                this.config = Configuration.Load(configFile);
            }
            catch (Exception e)
            {
                MessageBox.Show("Es ist ein Fehler aufgetreten \n\r\n\r" + e, "Achtung!");
            }

        }

        /// <summary>
        /// Verbindung zur Datenbank mit den in der Konfiguration angegebenen Daten herstellen
        /// </summary> 
        private void InitDataManager()
        {
            datamgr = new DataManager(config);

            try
            {
                GetDataManager();
            }
            catch (Exception e)
            {
                MessageBox.Show("Es ist ein Fehler aufgetreten \n\r\n\r" + e, "Achtung!");
            }
        }

        /// <summary>
        /// Liefert ein Datamanager-Objekt. 
        /// Falls kein Objekt bisher erstellt wurde, wird dieses instanziiert und verbunden.
        /// </summary>
        public DataManager GetDataManager()
        {
            if (datamgr == null)
                datamgr = new DataManager(config);

            try
            {
                if (!datamgr.Connected)
                {
                    datamgr.Connect();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Es ist ein Fehler aufgetreten \n\r\n\r" + e, "Achtung!");
            }

            return datamgr;
        }

        #endregion
    }
}
