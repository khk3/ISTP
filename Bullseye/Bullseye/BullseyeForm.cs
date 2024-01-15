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
    public partial class BullseyeForm : Form
    {
        public BullseyeForm(Employee emp)
        {
            InitializeComponent();
            Init();
            lblUserHeader.Text = emp.FirstName;
        }

        private void Init()
        {
           

            timer1 = new Timer();
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }

        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            MySqlClass m= new MySqlClass();
            if(tabMain.SelectedIndex == 0) // Tab Order
            {
              

            }
            else if(tabMain.SelectedIndex == 1) //Tag Inventory
            {
               
            }
            else if(tabMain.SelectedIndex == 2)//Tab Loss/Return
            {
                
            }
            else //Tab Reports
            {
                //ShowTab(tabMain.SelectedIndex);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Text = " Bullseye - " + DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToLongTimeString();
        }
        ItemClass[] itemsArray = null;
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            MySqlClass m = new MySqlClass();
            
            if (tabMain.SelectedIndex == 1)//Inventory
            {
                itemsArray = m.GetItems();                
                dgvInventory.DataSource = itemsArray;
            }
        }

        //Event for synamic research
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (dgvInventory.DataSource != null)
            {
                string searchText = txtSearch.Text;
                //TODO - CREATE A METHOD TO DO DYNAMIC SEARCH
            }

        }
    }
}
