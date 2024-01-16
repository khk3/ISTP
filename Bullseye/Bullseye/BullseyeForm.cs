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
        Employee user= null;
        MySqlClass m = new MySqlClass();

        public BullseyeForm(Employee emp)
        {
            InitializeComponent();
            Init();
            lblUserHeader.Text = emp.FirstName;
            user = emp;
        }

        private void Init()
        {
           

            timer1 = new Timer();
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }
        //GLOBAL array of items. When click Refresh loads items
        ItemClass[] itemsArray = null;

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
        
        

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
           
            
            if (tabMain.SelectedIndex == 1)//Inventory
            {
                itemsArray = m.GetAllItems();                
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
               ItemClass[] filteredItems = itemsArray.Where(item => item.Name.Contains(searchText)).ToArray();
                dgvInventory.DataSource = filteredItems;
            }

        }

        //Edit Item
        private void btnEditItem_Click(object sender, EventArgs e)
        {
            string role = m.GetPosition(user.PositionID);
            if (role == "Warehouse Manager")
            {

            }
            else
            {
                MessageBox.Show("Permission Denied. Only Warehouse Manager can Edit Items", "Permission Denied");
            }
        }
    }
}
