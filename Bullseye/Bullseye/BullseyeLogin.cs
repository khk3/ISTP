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
            lblDate.Text = DateTime.Now.ToLongDateString();
            lblTime.Text = DateTime.Now.ToLongTimeString();
            OpenDb();
      
        }

        private void OpenDb()
        {
            //open the connection - needs a try catch
            try
            {
                conn.Open();
                //run the script
                RunScript();

                //load employees
                LoadEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error - Closing form");
                Close();
            }

        }//end of OpenDB

        private void RunScript()
        {         
            string script = System.IO.File.ReadAllText("bullseyedb2024.sql");
            MySqlCommand cmd = new MySqlCommand(script, conn);
            //need try catch
            try
            {
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
            //create datatable and attempt to load
            // DataTable empTable = new DataTable();
            //need try catch
            try {
                //  empTable.Load(cmd.ExecuteReader());
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
                            Notes = reader.IsDBNull(reader.GetOrdinal("notes")) ? null : reader.GetString(reader.GetOrdinal("notes"))
                        };

                        // Add the Employee object to the list
                        employees.Add(employee);

                    }

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
            lblTime.Text= DateTime.Now.ToLongTimeString();
        }

        //Btn Login
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string fName= txtFName.Text;
            string lName= txtLName.Text;
            string password= txtPassword.Text;

            if(fName==""|| lName=="" || password=="")
            {
                MessageBox.Show("Login and Password cannot be empty", "Error to Login");
            }
            else
            {
                //HASH THE PASSWORD
                // string hashedPassword = HashPassword(password);
                bool userExists = employees.Any(emp => emp.FirstName == fName && emp.LastName == lName && emp.Password == password);
                if(userExists)
                {
                    MessageBox.Show("LOGIN!");
                }
                else
                {
                    MessageBox.Show("Not exists");
                }

            }
        }//end of login event

        private string HashPassword(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

    }
}
