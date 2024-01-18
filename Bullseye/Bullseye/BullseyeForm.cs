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
      
        private DateTime lastActivityTime;

        public BullseyeForm(Employee emp)
        {
            InitializeComponent();
            user = emp;
            Init();         
        }

        private void Init()
        {
            lastActivityTime = DateTime.Now;
            Application.Idle += Application_Idle;

            lblUserHeader.Text = user.FirstName;
            lblLocationHeader.Text = m.GetLocation(user.SiteID);
           
            timer1 = new Timer();
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }
        //GLOBAL array of items. When click Refresh loads items
        ItemClass[] itemsArray = null;


        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            this.Text = "Bullseye - " + now.ToShortDateString() + " - " + now.ToLongTimeString();
        }

        private void Application_Idle(object sender, EventArgs e)
        {

            TimeSpan idleTime = DateTime.Now - lastActivityTime;

            if (idleTime >= ConstantsClass.TimeToAutoLogout)
            {
                Application.Idle -= Application_Idle;
                this.Close();
                MessageBox.Show("Auto Logout due to inactivity", "User Inactive");

            }
        }

        private void ResetLastActivity()
        {
            lastActivityTime = DateTime.Now;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ResetLastActivity();
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
            ResetLastActivity();
            if (dgvInventory.DataSource != null)
            {          
                string searchText = txtSearch.Text.ToLower();
                ItemClass[] filteredItems = itemsArray.Where(item =>
                item.Name.ToLower().Contains(searchText) || item.ItemID.ToString().Contains(searchText)).ToArray();
                dgvInventory.DataSource = filteredItems;
            }   

        }

        //Edit Item
        private void btnEditItem_Click(object sender, EventArgs e)
        {
            string role = m.GetPosition(user.PositionID);
            if (role == "Warehouse Manager")
            {
                if(dgvInventory.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgvInventory.SelectedRows[0];

                    int itemID= Convert.ToInt32(selectedRow.Cells[0].Value);
                    string name= selectedRow.Cells[1].Value.ToString();
                    int sku = Convert.ToInt32(selectedRow.Cells[2].Value);
                    string desc = selectedRow.Cells[3].Value.ToString();
                    string categ= selectedRow.Cells[4].Value.ToString();
                    double weight = Convert.ToDouble(selectedRow.Cells[5].Value);
                    int caseSize = Convert.ToInt32(selectedRow.Cells[6].Value);
                    double costPrice = Convert.ToDouble(selectedRow.Cells[7].Value);
                    double retailPrice= Convert.ToDouble(selectedRow.Cells[8].Value);
                    int supplierID = Convert.ToInt32(selectedRow.Cells[9].Value);
                    int active= Convert.ToInt32(selectedRow.Cells[10].Value);
                    string notes = selectedRow.Cells[11].Value == null ? null : selectedRow.Cells[10].Value.ToString();

                    ItemClass item = new ItemClass(itemID,name, sku, desc,
                    categ, weight, caseSize, costPrice, retailPrice, supplierID, active, notes);

                    AddEditItemsForm itemsForm = new AddEditItemsForm("edit", item);                                  
                    itemsForm.ShowDialog();
                }
               
            }
            else
            {
                MessageBox.Show("Permission Denied. Only Warehouse Manager can Edit Items", "Permission Denied");
            }
        }

        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetLastActivity();
        }

        private void dgvInventory_Scroll(object sender, ScrollEventArgs e)
        {
            ResetLastActivity();
        }

        private void dgvInventory_SelectionChanged(object sender, EventArgs e)
        {
            ResetLastActivity();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddEditItemsForm addEditItemsForm = new AddEditItemsForm("add");
            addEditItemsForm.ShowDialog();
        }
    }
}
