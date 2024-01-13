﻿using System;
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
    public partial class AddUpdateUserForm : Form
    {
        LocationClass[] locationsArray = null;

        public AddUpdateUserForm() { }

        string addOrDelete = "";
        public AddUpdateUserForm(string action, Employee emp)
        {
            InitializeComponent();
            ClearWarnings();

            timer1 = new Timer();
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            timer1.Start();

            lblUserName.Text = emp.UserName;
            locationsArray = new LocationClass().Locations;
            //user location
            lblLocation.Text = locationsArray.FirstOrDefault(loc => loc.SiteID == emp.SiteID).Name;
            LoadComboboxes();

            addOrDelete = action;
            if (addOrDelete == "edit")
            {
                lblEmpID.Text = emp.EmployeeID.ToString();
                txtFName.Text = emp.FirstName.ToString();
                txtLName.Text = emp.LastName.ToString();
                lblUser.Text = emp.UserName;
                txtPassword.Text = emp.Password.ToString();
                txtConfirmPassword.Text = emp.Password.ToString();
                lblEmail.Text = emp.Email;

                cboPosn.SelectedIndex = emp.PositionID;
                cboLocation.SelectedIndex = emp.SiteID;
                ckbActive.Checked = emp.Active;

            }


        }

        private void LoadComboboxes()
        {
            try
            {
                cboPosn.DataSource = new BindingSource(PositionClass.PositionNames, null);
                cboPosn.DisplayMember = "Value";
                cboPosn.ValueMember = "Key";
                cboPosn.SelectedIndex = -1;
            }
            catch (Exception ex1)
            {
                MessageBox.Show("An error occured to retrieve Positions: " + ex1.Message, "Error - Positions");
            }

            try
            {
                cboLocation.DataSource = new BindingSource(locationsArray, null);
                cboLocation.DisplayMember = "Name";  // Specify the display member
                cboLocation.ValueMember = "SiteID";
                cboLocation.SelectedIndex = -1;
            }// Specify the value member}
            catch (Exception ex2)
            {
                MessageBox.Show("An error occured to retrieve Locations: " + ex2.Message, "Error - Locations");
            }

        }//end of LoadBomboboxes

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearWarnings();
            txtFName.Text = "";
            txtFName.Text = "";
            txtLName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            ckbActive.Checked = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckEmptyFields())//if any field empty
                MessageBox.Show("All fields must be filled to add new user", "Error - Empty Field(s)");
            else //all fields filled
            {
                Validation v = new Validation();
                if (!v.ValidadePassword(txtPassword.Text))
                {
                    MessageBox.Show("Password invalid. Must contain at least 8 characters, 1 uppercase letter and 1 special character.", "Error - Invalid Password");
                    txtPassword.Text = "";
                    txtConfirmPassword.Text = "";
                    txtPassword.Focus();
                }
                else
                {
                    bool isPasswordEqual = txtPassword.Text.Equals(txtConfirmPassword.Text);
                    if (!isPasswordEqual)
                    {
                        MessageBox.Show("Password does not match with the confirm password", "Error Confirm Password");
                        txtConfirmPassword.Text = "";
                        txtConfirmPassword.Focus();
                    }
                    else //Password VALID AND SAME
                    {

                        string password = txtPassword.Text;

                        string firstName = txtFName.Text.ToLower();
                        string lastName = txtLName.Text.ToLower();
                        bool active = ckbActive.Checked;
                        int position = cboPosn.SelectedIndex + 1;
                        int location = cboLocation.SelectedIndex + 1;
                        string notes = txtAreaNotes.Text;

                        MySqlClass m = new MySqlClass();

                        if (addOrDelete == "add")
                        {
                            string userNameForm = firstName.Substring(0, 1) + lastName;
                            //string userNameDuplicate = userNameForm;
                            int count = 1;
                            string finalUserName = userNameForm;

                            while (m.isUserNameDuplicated(finalUserName))
                            {
                                finalUserName = userNameForm + count;
                                count++;
                            }
                            string email = finalUserName + "@bullseye.ca";
                            Employee emp = new Employee(password, firstName, lastName, email, active, position, location, false, finalUserName, notes);

                            m.AddUser(emp);
                        }
                        else //update
                        {
                            int empID = Convert.ToInt32(lblEmpID.Text);
                            string email = lblEmail.Text;
                            string userName = lblUser.Text;


                            Employee emp = new Employee(empID, password, firstName, lastName, email, active, position, location, active, userName, notes);

                            //

                            int success = m.UpdateUser(emp);
                            if (success == 1)
                            {
                                MessageBox.Show("User: " + userName + " updated successfully", "User Updated");
                                this.Close();
                            }

                        }//end of else update                            
                    }

                }



            }
        }

        private bool CheckEmptyFields()
        {
            bool isEmpty = false;
            if (txtFName.Text == "" || txtLName.Text == "" || txtPassword.Text == "" || txtConfirmPassword.Text == "" || cboPosn.SelectedIndex == -1 || cboLocation.SelectedIndex == -1)
            {
                ClearWarnings();


                if (txtPassword.Text == "")
                    warnPw.Visible = true;
                if (txtFName.Text == "")
                    warnFn.Visible = true;
                if (txtLName.Text == "")
                    warnLn.Visible = true;
                if (txtConfirmPassword.Text == "")
                    warnCP.Visible = true;
                if (cboPosn.SelectedIndex == -1)
                    warnPosn.Visible = true;
                if (cboLocation.SelectedIndex == -1)
                    warnLoc.Visible = true;
                isEmpty = true;
            }
            return isEmpty;
        }

        private void ClearWarnings()
        {
            warnUn.Visible = false;
            warnPw.Visible = false;
            warnCP.Visible = false;
            warnFn.Visible = false;
            warnLn.Visible = false;
            warnPosn.Visible = false;
            warnLoc.Visible = false;

        }

        private void lblQuestion_MouseEnter(object sender, EventArgs e)
        {
            string tooltopStr = "Password must have:\n1-minimun 8 characters\n2-One capital letter\n3-One special character";
            toolTip1.Show(tooltopStr, lblQuestion);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Text = " Bullseye - " + DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToLongTimeString();
        }
    }
}