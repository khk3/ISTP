using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
//using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;

namespace Bullseye
{
    public partial class AddEditItemsForm : Form
    {
        
        MemoryStream mStream = new MemoryStream();
        private DateTime lastActivityTime;

        Employee emp=null;

        string action = "";

        public AddEditItemsForm(string add, Employee user)
        {
            InitializeComponent();
            txtItemID.Text = "Auto Generated";
            action = add;
            PopulateForm(add);
            Init(user);

        }

        public AddEditItemsForm(string edit, ItemClass item,Employee user)
        {
            InitializeComponent();
            action = edit;
            itemObj = item;
            Init(user);
           
            PopulateForm(edit);
           
        }

        ItemClass itemObj = null;
        MySqlClass m = new MySqlClass();
        private void Init(Employee user)
        {          
            btnResetItem.PerformClick();
            emp = user;
            timer1 = new Timer();
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            timer1.Start();
            Application.Idle += Application_Idle;
            ResetLastActivity();
        }
        private void PopulateForm(string action)
        {
            
            LoadComboboxes();
            if (action == "edit")
            {
                txtItemID.Text = itemObj.ItemID.ToString();
                txtName.Text = itemObj.Name;
                txtDescription.Text = itemObj.Description;             
                txtCaseSize.Text = itemObj.CaseSize.ToString();
                txtCostPrice.Text = itemObj.CostPrice.ToString();
                txtRetailPrice.Text = itemObj.RetailPrice.ToString();
                txtSku.Text = itemObj.Sku.ToString();
                //txtSupplierID.Text = itemObj.SupplierID.ToString();
                txtWeight.Text = itemObj.Weight.ToString();
                txtNotes.Text = itemObj.Notes;

                cboCategory.SelectedItem = itemObj.Category.ToString();
                
                cboSuppliers.SelectedValue= itemObj.SupplierID;
                //cboSuppliers.DisplayMember = "name";

                ckbActive.Checked = Convert.ToBoolean(itemObj.Active);

                if (itemObj.Image != null && itemObj.Image.Length > 0)
                {
                    MemoryStream ms = new MemoryStream(itemObj.Image);
                    
                    picImgEditItem.Image = Image.FromStream(ms);
                    
                }

            }       
            
        }
        private void LoadComboboxes()
        {
            string[] itemCategoriesArray = m.GetItemCategories();
            cboCategory.DataSource = itemCategoriesArray;           
            cboCategory.SelectedIndex = -1;

            SupplierClass[] suppliersArray= m.GetAllSuppliers();
            cboSuppliers.DataSource = suppliersArray;
            cboSuppliers.DisplayMember = "Name";
            cboSuppliers.ValueMember= "SupplierID";
            cboSuppliers.SelectedIndex = -1;
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            TimeSpan idleTime = DateTime.Now - lastActivityTime;

            if (idleTime >= ConstantsClass.TimeToAutoLogout)
            {
                System.Windows.Forms.Application.Idle -= Application_Idle;
                MessageBox.Show("Auto Logout due to inactivity", "Add Edit Item Form - User Inactive");
                CloseForm();
            }
        }

        private void ResetLastActivity()
        {
            lastActivityTime = DateTime.Now;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;         
            this.Text = "Bullseye - " + now.ToShortDateString() + " - " + now.ToLongTimeString();
        }

        private void btnResetItem_Click(object sender, EventArgs e)
        {
            txtItemID.Text = "";
            txtName.Text = "";
            txtDescription.Text = "";
            txtSku.Text = "";
            //txtSupplierID.Text = "";
            txtWeight.Text = "";
            txtRetailPrice.Text = "";
            txtCaseSize.Text = "";
            txtCostPrice.Text = "";
            txtNotes.Text = "";
            ckbActive.Checked = false;
            ResetWarns(); //Remove all warnings 
            ResetLastActivity();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void btnSaveItem_Click(object sender, EventArgs e)
        {
            //check empty fields
            if (CheckEmptyFields())
            {
                int itemID=0;
                if(!(action=="add"))
                    itemID = Convert.ToInt32(txtItemID.Text);
                string name = txtName.Text;
                string desc = txtDescription.Text;
                string category = cboCategory.SelectedItem.ToString();
                int caseSize = Convert.ToInt32(txtCaseSize.Text);
                double costPrice = Convert.ToDouble(txtCostPrice.Text);
                double retailPrice = Convert.ToDouble(txtRetailPrice.Text);
                int sku = Convert.ToInt32(txtSku.Text);
                int supplierID = Convert.ToInt32(cboSuppliers.SelectedValue);
                double weight = Convert.ToDouble(txtWeight.Text);
                int active = Convert.ToInt32(ckbActive.Checked);

                MemoryStream ms = new MemoryStream();
                picImgEditItem.Image.Save(ms, picImgEditItem.Image.RawFormat);
                byte[] image = ms.ToArray();

                string notes = txtNotes.Text;
              
                if (action == "add")
                {
                    ItemClass item = new ItemClass(name, sku, desc, category, weight, caseSize, costPrice, retailPrice, supplierID, active, image, notes);
                    int res = m.AddItem(item);
                    if (res == 1)
                    {
                        MessageBox.Show("Item Added Successfully", "Add Item");
                       CloseForm();
                    }
                }
                else //If action == "edit"
                {
                    ItemClass item = new ItemClass(itemID, name, sku, desc, category, weight, caseSize, costPrice, retailPrice, supplierID, active, image,notes);
                    int result = m.EditItem(item);
                    if (result == 1)
                    {
                        MessageBox.Show("Item Updated Successfully", "Update Item");
                        CloseForm();
                    }
                }
            }
            else //if fields empty
            {
                MessageBox.Show("There are empty fields. All fields with '!' must be filled.", "Error - Empty Fields");
            }
        }//end of function

        private bool CheckEmptyFields()
        {
            bool ok = true;

            if (txtSku.Text == "")
            {
                warnSku.Visible = true;
                ok = false;
            }

            if (txtName.Text == "")
            {
                warnName.Visible = true;
                ok = false;
            }

            if (txtDescription.Text == "")
            {
                warnDesc.Visible = true;
                ok = false;
            }

            if (cboCategory.SelectedIndex == -1)
            {
                warnCat.Visible = true;
                ok = false;
            }

            if (txtRetailPrice.Text == "")
            {
                warnRetail.Visible = true;
                ok = false;
            }

            if (txtCostPrice.Text == "")
            {
                warnCost.Visible = true;
                ok = false;
            }

            if (txtCaseSize.Text == "")
            {
                warnCase.Visible = true;
                ok = false;
            }

            if (txtWeight.Text == "")
            {
                warnWeight.Visible = true;
                ok = false;
            }

            if (cboSuppliers.SelectedIndex == -1)
            {
                warnSupp.Visible = true;
                ok = false;
            }

            return ok;
        }

        private void ResetWarns()
        {
            warnSku.Visible = false;
            warnName.Visible = false;
            warnDesc.Visible = false;
            warnCat.Visible = false;
            warnRetail.Visible = false;
            warnCost.Visible = false;
            warnCase.Visible = false;
            warnWeight.Visible = false;
            warnSupp.Visible = false;
        }

        //Accept only numbers
        private void txtSku_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch=e.KeyChar;
           
            if(!Char.IsDigit(ch)&& ch!=8)            
                e.Handled = true;           
        }

        private void CloseForm()
        {
            Application.Idle -= Application_Idle;
            Hide();
            Close();
        }

        private void txtCaseSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
           
            if (!Char.IsDigit(ch) && ch != 8)            
                e.Handled = true;            
        }

        private void txtCostPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 46 && txtSku.Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                return;
            }
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void txtWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 46 && txtSku.Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                return;
            }
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void txtRetailPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 46 && txtSku.Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                return;
            }
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void btnAddImage_Click(object sender, EventArgs e)
        {
            ResetLastActivity();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Load the selected image into the PictureBox
                picImgEditItem.Image = Image.FromFile(openFileDialog.FileName);
                
                
                //byte[] imageConverted;

               // picImgEditItem.Image.Save(mStream, picImgEditItem.Image.RawFormat);
                //imageConverted = mStream.ToArray();

            }
        }

        //When closing should open Bullseyeform again
        private void AddEditItemsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            BullseyeForm bForm= new BullseyeForm(emp);
            bForm.ShowDialog();
        }


        private void txtItemID_TextChanged(object sender, EventArgs e)
        {
            ResetLastActivity();
        }

        private void txtSku_TextChanged(object sender, EventArgs e)
        {
            ResetLastActivity();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            ResetLastActivity();
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            ResetLastActivity();
        }

        private void txtNotes_TextChanged(object sender, EventArgs e)
        {
            ResetLastActivity();
        }

        private void txtWeight_TextChanged(object sender, EventArgs e)
        {
            ResetLastActivity();
        }

        private void txtCaseSize_TextChanged(object sender, EventArgs e)
        {
            ResetLastActivity();
        }
    }
}
