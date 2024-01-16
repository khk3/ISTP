using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bullseye
{
    public partial class RecoverPasswordForm : Form
    {
        public RecoverPasswordForm() { }


        public RecoverPasswordForm(Employee user)
        {
            InitializeComponent();
            OpenDb();
            lblUserName.Text = user.UserName;
        }
        //class=level config to connection string
        static string connStr = ConfigurationManager.ConnectionStrings["bullseyedb"].ConnectionString;

        //create connection
        MySqlConnection conn = new MySqlConnection(connStr);

        private void OpenDb()
        {
            //open the connection - needs a try catch
            try
            {
                conn.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error - Closing form");
                Close();
            }

        }//end of OpenDB

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblQuestion_MouseEnter(object sender, EventArgs e)
        {
            string tooltopStr = "Password must have:\n1-minimun 8 characters\n2-One capital letter\n3-One special character";
            toolTip1.Show(tooltopStr,lblQuestion);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtNewPassWord.Text = "";
            txtConfirmPassword.Text = "";
            txtNewPassWord.Focus();
        }
  

        private bool CheckEmptyFields()
        {
            if(txtNewPassWord.Text !="" && txtConfirmPassword.Text !="")
                return true;
            else
            {
                MessageBox.Show("The fields cannot be empty", "Error - Empty Fields");
                if(txtNewPassWord.Text =="")
                    lblWarnNP.Visible = true;
                if(txtConfirmPassword.Text=="")
                    lblWarnC.Visible = true;
                return false;
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            ClearWarns();
            if (CheckEmptyFields())
            {
                string password = txtNewPassWord.Text;

                if(password != ConstantsClass.DefaultPassword)
                {
                    Validation v = new Validation();
                    if (v.ValidadePassword(password))
                    { //password valid

                        string confirmPassword = txtConfirmPassword.Text;
                        if (password == confirmPassword)
                        {
                            string cmd = "update employee set password='" + password + "' where username='" + lblUserName.Text + "'";
                            MySqlCommand sqlCommand = new MySqlCommand(cmd, conn);
                            try
                            {
                                int update = sqlCommand.ExecuteNonQuery();
                                if (update > 0)
                                {
                                    MessageBox.Show("Password updated Successfully", "Update Password");
                                    btnExit.PerformClick(); //close the form after updating the password
                                }
                            }
                            catch (MySqlException sqlEx)
                            {
                                MessageBox.Show("Could not update password. Error: " + sqlEx.Message, "Error - MySQL error");

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Could not update password. Error: " + ex.Message, "Error - Exception");
                            }

                        }
                        else //if confirm password does not match
                        {
                            MessageBox.Show("Confirm Password does no match with the Password", "Error - Confirm Password does not match");
                            txtConfirmPassword.Text = "";
                        }
                    }
                    else //if password not valid
                    {
                        MessageBox.Show("Password invalid. Must contain at least 8 characters, 1 uppercase letter and 1 special character.", "Error - Invalid Password");
                        btnReset.PerformClick();
                    }
                }
                else //if password === Default Password 
                {
                    MessageBox.Show("Please select a different password. Password cannot be the same as default", "Choose different Password");
                    ClearFields();
                }
               
            }
        }

        private void ClearFields()
        {
            txtNewPassWord.Text = "";
            txtConfirmPassword.Text = "";
            txtNewPassWord.Focus();
        }
        private void ClearWarns()
        {
            lblWarnNP.Visible=false;
            lblWarnC.Visible=false;
        }

        private void RecoverPasswordForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.Close(); //close connection
        }
    }
}
