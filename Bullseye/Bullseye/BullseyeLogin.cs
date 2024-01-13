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
        static string connStr = ConfigurationManager.ConnectionStrings["bullseyedb"].ConnectionString;

        //create connection
        MySqlConnection conn = new MySqlConnection(connStr);

        //Class Level of Employees
        List<Employee> employees = new List<Employee>();

        private void Init()
        {
            MySqlClass m= new MySqlClass();
            //OpenDb();
            m.OpenDb();
            //RunScript();
            m.RunScript();
            //LoadEmployees();
            employees=m.LoadEmployees();

            lblWarnP.Visible= false;
            lblWarnU.Visible=false;

            timer1 = new Timer();
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }

        private void OpenDb()
        {
            //open the connection - needs a try catch
            try
            {
                conn.Open();
                //run the script
         
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error - Closing form");
                Close();
            }

        }//end of OpenDB

        private void RunScript()
        {         
           
            //need try catch
            try
            {
                string script = System.IO.File.ReadAllText("Bullseye_DB2024_1.3.sql");
                MySqlCommand cmd = new MySqlCommand(script, conn);
                //Run the script
                int num=cmd.ExecuteNonQuery();            
            }
            catch (SqlException sqlEx)
            {
                // Handle SQL-related exceptions
                MessageBox.Show("SQL Exception: " + sqlEx.Message, "Error - Cannot run script");
            }
            catch (Exception ex)
            {
                // Handle other general exceptions
                MessageBox.Show("Exception: " + ex.Message, "Error - Cannot run script");
            }
        }

        private void LoadEmployees()
        {
            //create script and command
            string cmd = "select * from employee ";
            MySqlCommand sqlCmd = new MySqlCommand(cmd, conn);
           
            try {
                MySqlDataReader reader = sqlCmd.ExecuteReader();
                if (reader.HasRows)
                {           
                    while(reader.Read())
                    {
                        Employee employee = new Employee
                        {
                            EmployeeID = reader.GetInt32(reader.GetOrdinal("employeeID")),
                            Password = reader.GetString(reader.GetOrdinal("password")),
                            FirstName = reader.GetString(reader.GetOrdinal("firstName")),
                            LastName = reader.GetString(reader.GetOrdinal("lastName")),
                            Email = reader.GetString(reader.GetOrdinal("email")),
                            Active = reader.GetBoolean(reader.GetOrdinal("active")),
                            PositionID = reader.GetString(reader.GetOrdinal("PositionID")),
                            SiteID = reader.GetInt32(reader.GetOrdinal("siteID")),
                            Locked=reader.GetBoolean(reader.GetOrdinal("locked")),
                            UserName=reader.GetString(reader.GetOrdinal("username")),
                            Notes = reader.IsDBNull(reader.GetOrdinal("notes")) ? null : reader.GetString(reader.GetOrdinal("notes"))
                        };

                        // Add the Employee object to the list
                        employees.Add(employee);

                    }//end of while
                    reader.Close();

                }
            }
            catch (SqlException  sqlEx)
            {
                MessageBox.Show("Could not find Employees. System Error: " + sqlEx.Message, "Error - could not find Employees");
            }
            catch (Exception ex)
            {
                // Handle other general exceptions
                MessageBox.Show("Exception: " + ex.Message, "Error - Cannot run script");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //lblTime.Text= DateTime.Now.ToLongTimeString();
            //this.Text = "";
            
            this.Text = " Bullseye - " + DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToLongTimeString();

        }

        int errorCount = 0;

        //Btn Login
        private void btnLogin_Click(object sender, EventArgs e)
        {
           
            string userName= txtUserName.Text;
            //string fName= txtLogin.Text;
            //string lName= txtLName.Text;
            string password= txtPassword.Text;

            if(CheckEmptyFields("login"))
            {
                //HASH THE PASSWORD admin= P@ssw0rd-

                // string hashedPassword = HashPassword(password);
                //bool userExists = employees.Any(emp => emp.FirstName == fName && emp.LastName == lName);

                Employee user = employees.FirstOrDefault(emp => emp.UserName == userName);

                if (user!=null)
                {
                    if (user.Password == password)
                    {
                        if (!user.Active)
                            MessageBox.Show("User is not Active. Please contact your Administrator admin@bullseye.ca for assistance.", "Error- user not active");
                        else
                        {
                            if (user.Locked == false)  //If all correct
                            {
                                switch (user.PositionID.ToString())
                                {
                                    //case 1: //Regional Manager
                                    //case 2: Financial Manager
                                    //case 3: Store Manager
                                    //case 4://Warehouse Manager
                                    //case 6 warehouse employee
                                    case "99999999":
                                        MessageBox.Show("Welcome " + user.FirstName, "Login Successful");
                                        AdminForm adminForm = new AdminForm(user);
                                        adminForm.ShowDialog();

                                        break;
                                    default:
                                        MessageBox.Show("Invalid positionID", "Error - PositionID");
                                        break;
                                }
                            }

                            else
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
                                int update=m.UpdateEmployee(userName);
                                if (update > 0)
                                    MessageBox.Show("Your account has been locked because of too many incorrect login attempts. Please contact your Administrator at admin@bullseye.ca for assistance", "Error- User Locked");

                            }
                            else
                            {
                                MessageBox.Show("Account is locked. Please contact your Administrator admin@bullseye.ca for assistancer.", "Error - Account Locked");

                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("This user dos not exists", "Error- User does not exists");
                }
            }             
        }//end of login event

        //Function to encrypt password
        private string HashPassword(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        //Form Closing Event
        private void BullseyeLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Close the connection
            conn.Close();
            timer1.Stop();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       


        private void btnRecoverPassword_Click(object sender, EventArgs e)
        {
            lblWarnP.Visible = false;
            string userName= txtUserName.Text;

            MySqlClass m = new MySqlClass();
            bool isLocked = m.CheckLocked(userName); 
            if (!isLocked)
            {
                if (CheckEmptyFields("password"))
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
                else
                    txtUserName.Focus();
            }
            else //if user locked
            {
                MessageBox.Show("Cannot Recover Password. User is Locked. Please contact your Administrator at admin@bullseye.ca for assistance", "Error- Recover Password" );
            }
          
            
           
        }

        private bool CheckEmptyFields(string btn)
        {
            if(txtUserName.Text!=""&&txtPassword.Text!="")
                return true;
            else
            {               
                if (btn == "password")
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
    }
}
