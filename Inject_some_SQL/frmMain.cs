using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inject_some_SQL.Database;

/* Test-Anwendung für SQL-Injection
 * Bewusst werden mehrere Sicherheitslücken eingebaut um SQL-Injection demonstrieren zu können
 * 
 * by Tim Lukas Förster 
 * 31.10.2016 - 
 */


namespace Inject_some_SQL
{
    public partial class frmMain : Form
    {
        DataManager mgr = new DataManager();

        public frmMain()
        {
            InitializeComponent();
            ConnectDB();
        }

        private void ConnectDB()
        {
            mgr.ConnectToDB();
        }
    }
}
