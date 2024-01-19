using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bullseye
{
    public partial class FilterOrdersForm : Form
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int EmergencyOrder { get; set; }

        public FilterOrdersForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void btnEnter_Click(object sender, EventArgs e)
        {
            MySqlClass m= new MySqlClass();

            FromDate = dtpFrom.Value;
            ToDate = dtpTo.Value;
            EmergencyOrder= ckbEOrder.Checked ? 1 : 0;
            Close();
        
        }
    }
}
