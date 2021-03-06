﻿using System;
using System.Windows.Forms;
using Inject_some_SQL.Database;
using System.IO;
using System.Data;

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
        private DataTable table = null;

        public frmMain()
        {
            InitializeComponent();
            InitConfiguration();
            InitDataManager();
            InitLoad();
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

        private void InitLoad()
        {
            LoadBySearchterm();
        }


        private void LoadBySearchterm()
        {
            string sAbez1 = null;
            string sAbez2 = null;

            table = new DataTable();

            sAbez1 = this.tbAbez1.Text;
            sAbez2 = this.tbAbez2.Text;

            if (sAbez1 == "")
                sAbez1 = "%";

            if (sAbez2 == "")
                sAbez2 = "%";

            datamgr.LoadItemsFromDB(sAbez1, sAbez2, table);
            
            dataGridViewItems.DataSource = table;

            this.dataGridViewItems.DataSource = table;

            this.dataGridViewItems.Columns[0].HeaderText = "Artikel Nr.";
            this.dataGridViewItems.Columns[1].HeaderText = "Artikel Bez.1";
            this.dataGridViewItems.Columns[2].HeaderText = "Artikel Bez.2";
            this.dataGridViewItems.Columns[3].HeaderText = "Kosten";
            this.dataGridViewItems.Columns[4].HeaderText = "Werkzeugtyp";
            this.dataGridViewItems.Columns[5].HeaderText = "Angelegt von";
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            LoadBySearchterm();
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2)
            {
                LoadBySearchterm();
            }
        }

        private void tbAbez2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2)
            {
                LoadBySearchterm();
            }
        }
    }
}