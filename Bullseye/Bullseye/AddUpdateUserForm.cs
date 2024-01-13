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
    public partial class AddUpdateUserForm : Form
    {
        LocationClass[] locationsArray = null;

        public AddUpdateUserForm() { }
        public AddUpdateUserForm(string action, Employee user)
        {
            InitializeComponent();
            lblUserName.Text = user.UserName;
            locationsArray = new LocationClass().Locations;
            lblLocation.Text = locationsArray.FirstOrDefault(loc => loc.SiteID == user.SiteID).Name;

            LoadComboboxes();
        }

   

        public AddUpdateUserForm(Employee emp)
        {
            InitializeComponent();
            ClearWarnings();

            lblEmpID.Text = emp.EmployeeID.ToString();
            txtFName.Text= emp.FirstName.ToString();
            txtLName.Text= emp.LastName.ToString();
        
            txtPassword.Text= emp.Password.ToString();
            
        }

        private void LoadComboboxes()
        {
            try {
                cboPosn.DataSource = new BindingSource(PositionClass.PositionNames, null);
                cboPosn.DisplayMember = "Value";
                cboPosn.ValueMember = "Key";
            }
            catch(Exception ex1) {
                MessageBox.Show("An error occured to retrieve Positions: "+ex1.Message,"Error - Positions");
            }

            try
            {              
                cboLocation.DataSource = new BindingSource(locationsArray, null);
                cboLocation.DisplayMember = "Name";  // Specify the display member
                cboLocation.ValueMember = "SiteID";
            }// Specify the value member}
            catch (Exception ex2)
            {
                MessageBox.Show("An error occured to retrieve Locations: " + ex2.Message, "Error - Locations");
            }
            
        }//end of LoadBomboboxes

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
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
                Validation v=new Validation();
                if (!v.ValidadePassword(txtPassword.Text))
                {
                    MessageBox.Show("Password invalid. Must contain at least 8 characters, 1 uppercase letter and 1 special character.", "Error - Invalid Password");
                    txtPassword.Text= "";
                    txtPassword.Focus();
                }
                else
                {
                    bool isPasswordEqual= txtPassword.Text.Equals(txtConfirmPassword.Text);
                    if (!isPasswordEqual)
                    {
                        MessageBox.Show("Password does not match with the confirm password", "Error Confirm Password");
                        txtConfirmPassword.Text = "";
                        txtConfirmPassword.Focus();
                    }
                    else //Password VALID AND SAME
                    {
                        string firstName = txtFName.Text.ToLower();
                        string lastName = txtLName.Text.ToLower();
                        string userNameForm = firstName.Substring(0, 1) + lastName;
                        string userNameDuplicate = userNameForm;
                        MySqlClass m = new MySqlClass();

                        int count = 1;

                        string finalUserName = userNameForm;

                        while (m.isUserNameDuplicated(finalUserName))
                        {
                          
                            finalUserName = userNameForm + count;
                            count++;
                        }



                    }

                }

              

            }
        }

        private bool CheckEmptyFields()
        {
            bool isEmpty=false;
            if(txtFName.Text ==""||txtLName.Text==""|| txtPassword.Text == ""||txtConfirmPassword.Text=="")
            {
               ClearWarnings();

             
                if(txtPassword.Text=="")
                    warnPw.Visible= true;
                if(txtFName.Text=="")
                    warnFn.Visible= true;
                if(txtLName.Text=="")
                    warnLn.Visible= true;            
                if(txtConfirmPassword.Text=="")
                    warnCP.Visible= true;
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
         
        }

        private void lblQuestion_MouseEnter(object sender, EventArgs e)
        {
            string tooltopStr = "Password must have:\n1-minimun 8 characters\n2-One capital letter\n3-One special character";
            toolTip1.Show(tooltopStr, lblQuestion);
        }
    }
}
