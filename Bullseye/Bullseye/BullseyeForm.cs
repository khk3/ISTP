using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        MemoryStream mStream= new MemoryStream();
        private DateTime lastActivityTime;
        //public TransactionClass[] FilteredTransactions { get; set; }
        
        
        //Global arrays - TO be used in dynamic search;
        ItemClass[] itemsArray = null;
        TransactionClass[] transactionsArray= null;


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
       


        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            this.Text = "Bullseye - " + now.ToShortDateString() + " - " + now.ToLongTimeString();
        }

        private void Application_Idle(object sender, EventArgs e)
        {

        }

        private void ResetLastActivity()
        {
            lastActivityTime = DateTime.Now;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ResetLastActivity();
            txtSearchInventory.Text = "";
        
            if (tabMain.SelectedIndex == 1)//Inventory
            {  
                itemsArray=m.GetAllItems();
                 dgvInventory.DataSource = itemsArray;
                dgvInventory.Columns[11].Visible = false; // will hide image in datagridview        
            }
            else if(tabMain.SelectedIndex ==0) //Orders
            {
                transactionsArray= m.GetAllTransactions();
                dgvOrders.DataSource= transactionsArray;
               //DynamicSearch(dgvOrders, txtSearchOrders);
            }
        }

        //Event for synamic research
        private void DynamicSearch(DataGridView dataGridView, TextBox textBox)
        {
            ResetLastActivity();

            if (dataGridView.DataSource != null)
            {
                string searchText = textBox.Text.ToLower();
                if (dataGridView== dgvInventory)
                {
                    ItemClass[] filteredItems = itemsArray.Where(item =>
                        item.Name.ToLower().Contains(searchText) || item.ItemID.ToString().Contains(searchText)).ToArray();

                    dataGridView.DataSource = filteredItems;
                }
                else if(dataGridView== dgvOrders)
                {
                    TransactionClass[] filteredTransactions =transactionsArray.Where(txn =>
                      txn.TxnID.ToString().Contains(searchText)).ToArray();

                    dataGridView.DataSource = filteredTransactions;
                }
              
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
                    byte[] image = selectedRow.Cells[11].Value as byte[];
                    string notes = selectedRow.Cells[12].Value == null ? null : selectedRow.Cells[10].Value.ToString();

                    ItemClass item = new ItemClass(itemID,name, sku, desc,
                    categ, weight, caseSize, costPrice, retailPrice, supplierID, active, image, notes);

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

        //Row selection changed dgvInventory
        private void dgvInventory_SelectionChanged(object sender, EventArgs e)
        {
            ResetLastActivity();
            if (dgvInventory.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvInventory.SelectedRows[0];

                // Assuming the image column is the 12th column (index 11)
                byte[] imageBytes = selectedRow.Cells[11].Value as byte[];

                if (imageBytes != null && imageBytes.Length > 0)
                {
                    mStream.Position = 0; // Reset the position to the beginning
                    mStream.Write(imageBytes, 0, imageBytes.Length);
                    pivInvImage.Image = Image.FromStream(mStream);

                }
                else
                {
                    //
                    pivInvImage.Image = Image.FromFile("ImagesItems/noImageAvailable.png");
                }
            }
           // else
           // {
                // Clear the PictureBox if no row is selected
            //    pivInvImage.Image = Image.FromFile("ImagesItems/noImageAvailable.png");
           // }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddEditItemsForm addEditItemsForm = new AddEditItemsForm("add");
            addEditItemsForm.ShowDialog();
        }

        private void txtSearchOrders_TextChanged(object sender, EventArgs e)
        {
            DynamicSearch(dgvOrders, txtSearchOrders);
        }

        private void txtSearchInventory_TextChanged(object sender, EventArgs e)
        {
            DynamicSearch(dgvInventory, txtSearchInventory);
        }

        private void picFilterOrders_Click(object sender, EventArgs e)
        {
            if (dgvOrders.DataSource != null)
            {
                FilterOrdersForm filterOrdersForm = new FilterOrdersForm();
                filterOrdersForm.ShowDialog();

                DateTime fromDate = filterOrdersForm.FromDate;
                DateTime toDate = filterOrdersForm.ToDate;
                int eOrders = filterOrdersForm.EmergencyOrder;
                TransactionClass[] filteredArray = m.GetTransactionsFiltered(fromDate, toDate, eOrders);
                
                dgvOrders.DataSource = filteredArray;
            }
            
        }
    }
}
