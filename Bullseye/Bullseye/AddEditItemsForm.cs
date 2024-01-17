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
    public partial class AddEditItemsForm : Form
    {
        public AddEditItemsForm(string action, ItemClass item)
        {
            InitializeComponent();
            Init();
            if(action=="edit") //Edit an Item
                PopulateForm(action);
            else
            {

            }
        }

        private void Init()
        {
            timer1 = new Timer();
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }
        private void PopulateForm(string action)
        {

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Text = " Bullseye - " + DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToLongTimeString();
        }
    }
}
