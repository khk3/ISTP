using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

//Kang Kwon
namespace Bullseye
{
    public partial class BullseyeLogin : Form
    {
        public BullseyeLogin()
        {
            InitializeComponent();
            Init();
        }

        //class=level config to connection string
        static string connStr = ConfigurationManager.ConnectionStrings[ConstantsClass.DatabaseName].ConnectionString;

        //create connection
        MySqlConnection conn = new MySqlConnection(connStr);

        //Class Level of Employees
        List<Employee> employees = new List<Employee>();

        //Helper Class level MySqlClass 
        MySqlClass m = new MySqlClass();

        private void Init()
        {
            //Open connection
            m.OpenDb();

            // m.RunScript();   //DEFAULT DATABASE

            RefreshEmployees();

            lblWarnP.Visible= false;
            lblWarnU.Visible=false;

            timer1 = new Timer();
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }

        //Display Datetime at the Title
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Text = " Bullseye - " + DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToLongTimeString();

        }
        //Query to refresh Employees' data
        public void RefreshEmployees()
        {
            employees = m.LoadEmployees();
        }

        //Global Variable to count number of errors to login.
        int errorCount = 0;

        //Btn Login
        private void btnLogin_Click(object sender, EventArgs e)
        {          
            string userName= txtUserName.Text;
            string password= txtPassword.Text;

            if(CheckEmptyFields("login"))
            {
                Employee user = employees.FirstOrDefault(emp => emp.UserName == userName);

                if (user!=null)
                {                       
                    bool access = false;
                    
                    if (txtPassword.Text == user.Password)
                    {
                        if (user.Password == ConstantsClass.DefaultPassword)                      
                            access = true;                             
                    }
                    else
                    {
                        //Helper Class Validation contain method to verify encrypted Password 
                        bool verifyPassword = Validation.VerifyPassword(password, user.Password);
                        if (verifyPassword)
                            access = true;
                    }
              
                    if (access) // if password Match
                    {
                        if (!user.Active)//if user is not Active
                            MessageBox.Show("User is not Active. Please contact your Administrator admin@bullseye.ca for assistance.", "Error- user not active");
                        else //if user Active
                        {
                            if (user.Locked == false)  //If user not Locked
                            {                  
                                //if firstTime login. Helper class ConstantsClass stores theh Default values
                                if(user.Password == ConstantsClass.DefaultPassword)
                                {
                                    RecoverPasswordForm rf= new RecoverPasswordForm(user);
                                    rf.ShowDialog();
                                }
                                else
                                {
                                   
                                    switch (user.PositionID)
                                    {                                        
                                        case 99999999:
                                            //MessageBox.Show("Welcome " + user.FirstName, " Login Successful");
                                            DialogResult result = MessageBox.Show("Welcome " + user.FirstName + ". Do you want to open the Admin Form? Click 'yes' to be directed to Admin Form or 'no' to be directed to Bullseye Form", "Login Successful", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                            if (result == DialogResult.Yes)
                                            {
                                               
                                                AdminForm adminForm = new AdminForm(user);
                                                adminForm.ShowDialog();
                                                Hide();
                                                Close();

                                            }
                                            else
                                            {
                                               
                                                BullseyeForm bullseyeForm = new BullseyeForm(user);
                                                bullseyeForm.ShowDialog();
                                                Hide();
                                                Close();

                                            }
                                            break;
                                        default: //other users
                                            MessageBox.Show("Welcome "+user.FirstName, "Login Successful");
                                           
                                            BullseyeForm bForm = new BullseyeForm(user);
                                            bForm.ShowDialog();
                                            Hide();
                                            Close();
                                            break;
                                    }

                                }
                               
                            }
                            else //if user is Locked
                                MessageBox.Show("Account is locked. Please contact your Administrator admin@bullseye.ca for assistance.", "Error - Account Locked");
                        }
                    }
                    else //if password does NOT match
                    {
                        if (errorCount < 2)
                        {
                            MessageBox.Show("Password Invalid", "Error- Password");
                            errorCount++;
                            txtPassword.Text = "";
                            txtPassword.Focus();
                        }
                        else //change locked to 1
                        {
                            MySqlClass m= new MySqlClass();
         
                            bool isLocked=m.CheckLocked(userName);
                            if (!isLocked)
                            {                               
                                int update=m.LockUser(userName);
                                if (update > 0)
                                    MessageBox.Show("Your account has been locked because of too many incorrect login attempts. Please contact your Administrator at admin@bullseye.ca for assistance", "Error- User Locked");
                            }
                            else //if user already locked
                            {
                                MessageBox.Show("Account is locked. Please contact your Administrator admin@bullseye.ca for assistance.", "Error - Account Locked");

                            }
                        }
                    }
                }
                else //if user doe not exist in the DB
                {
                    MessageBox.Show("This user dos not exists", "Error- User does not exists");
                }
            }             
        }//end of login event

 

        //Form Closing Event
        private void BullseyeLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Close the connection
            conn.Close();
            //timer1.Stop();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       

        //Recover Password Click
        private void btnRecoverPassword_Click(object sender, EventArgs e)
        {
            lblWarnP.Visible = false;
            string userName= txtUserName.Text;

            MySqlClass m = new MySqlClass();

            if (CheckEmptyFields("recoverPassword")) //ad else to focus
            {
                bool isActive = m.CheckActive(userName);
                if (isActive)// if user Active
                {
                    bool isLocked = m.CheckLocked(userName);
                    if (!isLocked) // if user is NOT locked
                    {
                        Employee emp = employees.FirstOrDefault(em => em.UserName == txtUserName.Text);

                        if (emp != null)
                        {
                            RecoverPasswordForm rPassForm = new RecoverPasswordForm(emp);
                            rPassForm.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("User not found", " Error - Invalid User");
                            txtUserName.Text = "";
                            txtUserName.Focus();
                        }
                    }
                    else //If user is locked
                    {
                        MessageBox.Show("Cannot recover password. User is locked. Please contact your Administrator admin@bullseye.ca for assistance.", "Error- Recover Password ");
                        Close();
                    }
                }
                else//if user Inactive
                {
                    MessageBox.Show("Cannot recover password. User is inactive. Please contact your Administrator admin@bullseye.ca for assistance.", "Error- Recover Password ");
                    Close();
                }
                   
            }
            else
                txtUserName.Focus();
        
        }

        //return false if empty
        private bool CheckEmptyFields(string btn)
        {
            if(txtUserName.Text!=""&&txtPassword.Text!="")
                return true;
            else
            {               
                if (btn == "recoverPassword")
                {
                    if (txtUserName.Text == "")
                    {
                        MessageBox.Show("Username field cannot be empty", "Error - Recover password");
                        lblWarnU.Visible = true;
                        lblWarnP.Visible = false;
                        return false;
                    }
                    else
                        return true;   
                }                  
                else
                {
                    MessageBox.Show("Fields cannot be empty", "Error to Login");
                    if (txtUserName.Text == "")
                        lblWarnU.Visible = true;
                    else
                        lblWarnU.Visible = false;
                    if (txtPassword.Text == "")
                        lblWarnP.Visible = true;
                    else
                        lblWarnP.Visible = false;
                }    
                return false;
            }
                
        }

        //txtPassword evt to display '*' instad of actual password
        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '*';
        }

    
        private void picEye_MouseEnter(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '\0';
        }

        private void picEye_MouseLeave(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '*';
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshEmployees();
            txtUserName.Text = "";
            txtPassword.Text = "";
            lblWarnP.Visible=false;
            lblWarnU.Visible=false;
            errorCount = 0;
        }
    }
}
