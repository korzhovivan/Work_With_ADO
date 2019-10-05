using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;



namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        EasySQL easySQL = new EasySQL();
        public Form1()
        {
            InitializeComponent();

            cmb_dbNames.SelectedIndexChanged += Cmb_dbNames_SelectedIndexChanged;
            cmb_dbNames.Items.Add("Authors");
            cmb_dbNames.Items.Add("Books");
            cmb_dbNames.Items.Add("Group");
            easySQL.Connect();
        }

        private void Cmb_dbNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView.DataSource = easySQL.GetTable(cmb_dbNames.SelectedItem.ToString());
        }
    }
}
