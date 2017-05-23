using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JHDockCA
{
    public partial class FrmSelKey : Form
    {
        public DataTable dt = null;
        public string DN = "";
        public FrmSelKey()
        {
            InitializeComponent();
        }

        private void FrmSelKey_Load(object sender, EventArgs e)
        {
            if (dt != null)
            {
                dataGridView1.DataSource = dt;
                this.dataGridView1.Columns[0].HeaderText = "名称";
                this.dataGridView1.Columns[1].HeaderText = "值";
            }
        }

        private void btnSel_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count <= 0)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.No;
            }
            else
            {
                DN = dataGridView1.CurrentRow.Cells["itemValue"].Value.ToString();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;             
        }
    }
}