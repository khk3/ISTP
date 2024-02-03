using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bullseye
{
    public partial class AddUpdateUserForm : Form
    {
        LocationClass[] locationsArray = null;
        //PositionClass[] positionArray = null;
        Employee adminLogged=null;

        private DateTime lastActivityTime;

        public AddUpdateUserForm() { }

        string addOrDelete = "";
        public AddUpdateUserForm(string action, Employee emp, Employee adm)
        {
            InitializeComponent();
            ClearWarnings();
            //adminLogged = iAdminLogged;
            Application.Idle += Application_Idle;
            ResetLastActivity();
            timer1 = new Timer();
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            timer1.Enabled = true;
            timer1.Start();

            lblUserName.Text = emp.UserName;
            adminLogged = adm;
            MySqlClass m=new MySqlClass();

            locationsArray = m.GetAllLocations();
            //user location
            lblLocation.Text = locationsArray.FirstOrDefault(loc => loc.SiteID == emp.SiteID).Name;
            
            PositionClass[] positionArray = m.GetAllPositions();
            LoadComboboxes(positionArray);

            addOrDelete = action;

            //if update user
            if (addOrDelete == "edit")
            {
                lblEmpID.Text = emp.EmployeeID.ToString();
                txtFName.Text = emp.FirstName.ToString();
                txtLName.Text = emp.LastName.ToString();
                lblUser.Text = emp.UserName;
                //txtPassword.Text = emp.Password.ToString();
                txtPassword.Text = "";
                //txtConfirmPassword.Text = emp.Password.ToString();
                txtConfirmPassword.Text = "";
                lblEmail.Text = emp.Email;

                cboPosn.SelectedValue= emp.PositionID;
                cboLocation.SelectedIndex = emp.SiteID;
                ckbActive.Checked = emp.Active;
                ckbActive.Enabled = true;
                ckbLocked.Checked = emp.Locked;
                ckbLocked.Enabled = true;
            }
            else //if add
            {
                txtPassword.Text = ConstantsClass.DefaultPassword;
                txtConfirmPassword.Text= ConstantsClass.DefaultPassword;
                ckbActive.Checked = ConstantsClass.Active;
                ckbActive.Enabled = false;

            }
        }
        private void ResetLastActivity()
        {
            lastActivityTime = DateTime.Now;
        }

        private void LoadComboboxes(PositionClass[] positionArray)
        {         
            try
            {
                cboPosn.DataSource = new BindingSource(positionArray, null);
                cboPosn.DisplayMember = "PermissionLevel";
                cboPosn.ValueMember = "PositionID";
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
           CloseForm();
        }

       


        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetLastActivity();
            ClearWarnings();
            txtFName.Text = "";
            txtFName.Text = "";
            txtLName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            txtAreaNotes.Text = "";
            cboLocation.SelectedIndex = -1;
            cboPosn.SelectedIndex = -1;
            //ckbActive.Checked = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckEmptyFields())//if any field empty
            {
                MessageBox.Show("All fields must be filled to add new user", "Error - Empty Field(s)");
            }
            else //all fields filled
            {
                Validation v = new Validation();
                
                bool isPasswordEqual = txtPassword.Text.Equals(txtConfirmPassword.Text);
                if (!isPasswordEqual)
                {
                    MessageBox.Show("Password does not match with the confirm password", "Error Confirm Password");
                    //txtConfirmPassword.Text = "";
                    txtConfirmPassword.Focus();
                }
                else //Password VALID AND SAME
                {                     
                    string password ="";
                        
                    string firstName = txtFName.Text.ToLower();
                    string lastName = txtLName.Text.ToLower();
                    bool active = ckbActive.Checked;
                    int position = Convert.ToInt32(cboPosn.SelectedValue);
                    int location = cboLocation.SelectedIndex + 1;
                    string notes = txtAreaNotes.Text;
                    bool locked= ckbLocked.Checked;
                    MySqlClass m = new MySqlClass();

                    if (addOrDelete == "add")
                    {
                        txtPassword.Text = ConstantsClass.DefaultPassword;
                        password = ConstantsClass.DefaultPassword;             
                        string userNameForm = firstName.Substring(0, 1) + lastName;
                        //string userNameDuplicate = userNameForm;
                        int count = 1;
                        string finalUserName = userNameForm;

                        while (m.IsUserNameDuplicated(finalUserName))
                        {
                            finalUserName = userNameForm + count;
                            count++;
                        }
                        string email = finalUserName + "@bullseye.ca";

                        Employee emp = new Employee(0, password, firstName, lastName, email, ConstantsClass.Active, position, location, ConstantsClass.Locked, finalUserName, notes);
                        bool confirmAddUser = MessageBox.Show("Confirm Add User?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes;
                        if (confirmAddUser)
                        {
                            int added = m.AddUser(emp);
                            if (added == 1)
                            {
                                
                                MessageBox.Show("Your Username is: " + finalUserName + " , your email is : " + email, "SAVE YOUR USERNAME PASSWORD AND EMAIL");
                                CloseForm();
                            }
                            else
                            {                        
                                MessageBox.Show("Could not Add User. Please contact the administration: admin@bullseye.ca  ", "Error- Could not Add User");
                                CloseForm();
                            }
                        }
                    }
                    else //update
                    {
                        password= txtPassword.Text;
                        string hashedPassword = Validation.HashPassword(password);
                        int empID = Convert.ToInt32(lblEmpID.Text);
                        string email = lblEmail.Text;
                        string userName = lblUser.Text;
                        Employee emp = new Employee(empID, hashedPassword, firstName, lastName, email, active, position, location, locked, userName, notes);

                        int success = m.UpdateUser(emp);
                        if (success == 1)
                        {
                            
                            MessageBox.Show("User: " + userName + " updated successfully", "User Updated");
                            CloseForm();
                        }

                    }//end of else update                            
                

                }
                //BullseyeLogin bullseyeLogin = new BullseyeLogin();
               

            }
        }



        private bool CheckEmptyFields()
        {
            bool isEmpty = false;
            if (!ckbActive.Checked)
            {
                txtPassword.Text = ConstantsClass.DefaultPassword;
                txtConfirmPassword.Text = ConstantsClass.DefaultPassword;
            }
 

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
            ResetLastActivity();
            string tooltopStr = "Password must have:\n1-minimun 8 characters\n2-One capital letter\n3-One special character";
            toolTip1.Show(tooltopStr, lblQuestion);
        }
        private void Application_Idle(object sender, EventArgs e)
        {

            TimeSpan idleTime = DateTime.Now - lastActivityTime;

            if (idleTime >= ConstantsClass.TimeToAutoLogout)
            {
                Application.Idle -= Application_Idle;
                MessageBox.Show("Auto Logout due to inactivity", "Add Edit User Form- User Inactive");
                CloseForm();


            }
        }
        private void CloseForm()
        {

            Application.Idle -= Application_Idle;
            Hide();
            Close();

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
        
            DateTime now = DateTime.Now;
            this.Text = "Bullseye - " + now.ToShortDateString() + " - " + now.ToLongTimeString();
        }

        //When closing reopen admin form
        private void AddUpdateUserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AdminForm aForm= new AdminForm(adminLogged);
            aForm.ShowDialog();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            ResetLastActivity();
        }

        private void txtFName_TextChanged(object sender, EventArgs e)
        {
            ResetLastActivity();
        }

        private void txtLName_TextChanged(object sender, EventArgs e)
        {
            ResetLastActivity();

        }

        private void txtAreaNotes_TextChanged(object sender, EventArgs e)
        {
            ResetLastActivity();
        }

        private void btnDefaultPassword_Click(object sender, EventArgs e)
        {
            string defaultPass= ConstantsClass.DefaultPassword;
            txtConfirmPassword.Text=defaultPass;
            txtPassword.Text=defaultPass;
        }
    }
}