﻿using System;
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
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;

namespace Bullseye
{
    public partial class AddEditItemsForm : Form
    {
        DateTime loginTime;
        MemoryStream mStream = new MemoryStream();


        string action = "";
        public AddEditItemsForm(string add)
        {
            InitializeComponent();
            txtItemID.Text = "Auto Generated";
            action = add;
            PopulateForm(add);
            Init();
        }

        public AddEditItemsForm(string edit, ItemClass item)
        {
            InitializeComponent();
            action = edit;
            itemObj = item;
            Init();
           
            PopulateForm(edit);
           
        }

        ItemClass itemObj = null;
        MySqlClass m = new MySqlClass();
        private void Init()
        {          
            btnResetItem.PerformClick();
            loginTime = DateTime.Now;
            timer1 = new Timer();
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            timer1.Start();
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
                ckbActive.Checked = Convert.ToBoolean(itemObj.Active);

                if (itemObj.Image != null && itemObj.Image.Length > 0)
                {
                    MemoryStream ms = new MemoryStream(itemObj.Image);
                    
                    picImgEditItem.Image = Image.FromStream(ms);
                    
                }
               // else
               // {
                    // If the image is not available, you may set a default image or handle it as needed
               //    picImgEditItem.Image = null;
               // }

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
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
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
                //string itemCat = cboCategory.Text;
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
                        this.Close();
                    }
                }
                else //If action == "edit"
                {
                    ItemClass item = new ItemClass(itemID, name, sku, desc, category, weight, caseSize, costPrice, retailPrice, supplierID, active, image,notes);
                    int result = m.EditItem(item);
                    if (result == 1)
                    {
                        MessageBox.Show("Item Updated Successfully", "Update Item");
                        this.Close();
                    }

                }
            }
            else //if fields empty
            {
                MessageBox.Show("There are empty fields. All fields with '*' must be filled.", "Error - Empty Fields");
            }



        }//end of function

        private bool CheckEmptyFields()
        {
            bool ok = true;
            if (txtName.Text == "" || txtDescription.Text == "" || txtSku.Text == "" || cboSuppliers.SelectedIndex== -1 
                || txtRetailPrice.Text == "" || txtWeight.Text == "" || txtCostPrice.Text == "" || txtCaseSize.Text == "")
            {
                ok = false;
            }
            return ok;
        }

        private void txtSku_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch=e.KeyChar;
           
            if(!Char.IsDigit(ch)&& ch!=8)            
                e.Handled = true;
            
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
    }
}
