using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Bullseye
{
    public partial class AdminForm : Form
    {
        public AdminForm() { }

        private DateTime lastActivityTime;
       // int inactivityLogout = ConstantsClass.TimeToAutoLogout; //20 min of inactivity


        public AdminForm(Employee user)
        {
            InitializeComponent();
            Init(user.FirstName);
            userLogged = user;      
        }

        //class=level config to connection string
        static string connStr = ConfigurationManager.ConnectionStrings[ConstantsClass.DatabaseName].ConnectionString;

        //create connection
        MySqlConnection conn = new MySqlConnection(connStr);

        Employee userLogged = null;
        private void Init(string fName)
        {
            lblUser.Text = fName;

            lastActivityTime = DateTime.Now;
            Application.Idle += Application_Idle;

            timer2 = new Timer();
            timer2.Interval = 1000;
            timer2.Tick += timer2_Tick;
            timer2.Start();
            // this.Text = "Admin - " + DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToLongTimeString();
            tabAdmin.SelectedIndex = 0;
            btnRefresh.PerformClick();

        }

        //Close the adminForm
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Application_Idle(object sender, EventArgs e)
        {

            TimeSpan idleTime = DateTime.Now - lastActivityTime;

            if (idleTime >= ConstantsClass.TimeToAutoLogout)
            {
                Application.Idle -= Application_Idle;
                this.Close();
                MessageBox.Show("Auto Logout due to inactivity","User Inactive");
                
            }
        }

        private void ResetLastActivity()
        {
            lastActivityTime = DateTime.Now;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            this.Text = "Bullseye - "+ now.ToShortDateString() +" - " + now.ToLongTimeString();
        }

        private void tabAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabAdmin.SelectedIndex == 0)
            {

            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ResetLastActivity();
            if (tabAdmin.SelectedIndex == 0)
            {                         
                    MySqlClass m= new MySqlClass();
                Employee[] employeesArr = m.GetAllEmployees();
                
                dgvEmployees.DataSource = employeesArr;


                dgvEmployees.ReadOnly = true;
                dgvEmployees.ClearSelection();         
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {//Add new user
            ResetLastActivity();
            AddUpdateUserForm au= new AddUpdateUserForm("add",userLogged);
            au.ShowDialog();
        }

        private void btnEditUser_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.DataSource != null)
            {
                if (dgvEmployees.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgvEmployees.SelectedRows[0];

                    int empId = Convert.ToInt32(selectedRow.Cells[0].Value);
                    string pw= selectedRow.Cells[1].Value.ToString();
                    string fn= selectedRow.Cells[2].Value.ToString();
                    string ln= selectedRow.Cells[3].Value.ToString();
                    string email= selectedRow.Cells[4].Value.ToString();
                    bool active = Convert.ToBoolean(selectedRow.Cells[5].Value);
                    int posn = Convert.ToInt32(selectedRow.Cells[6].Value);
                    int site = Convert.ToInt32((selectedRow.Cells[7].Value));
                    bool locked= Convert.ToBoolean((selectedRow.Cells[8].Value));
                    string userName= selectedRow.Cells[9].Value.ToString();
                    string notes = selectedRow.Cells[10].Value?.ToString();

                    Employee empl = new Employee(empId,pw,fn,ln,email,active,posn,site,locked,userName,notes);
                    
                    AddUpdateUserForm au= new AddUpdateUserForm("edit",empl); 
                    au.ShowDialog();
                }
                else//if no row selected                
                    MessageBox.Show("Please select a user to update.", "Error - no user selection");            
            }
            else
            {
                MessageBox.Show("To Update a user please, refresh the table and select a user first.", "Error - Update User.");
            }
          
          
        }

        private void btnInactivateUser_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.DataSource != null)
            {
                if (dgvEmployees.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgvEmployees.SelectedRows[0];

                    int empId = Convert.ToInt32(selectedRow.Cells[0].Value);

                   // string cmd = "DELETE employee where employeeID=@empId;
                    MySqlClass m = new MySqlClass();

                    bool confirmUpdate = MessageBox.Show("Confirm Inactivate User?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes;
                    if (confirmUpdate)
                    {
                        int success = m.InactiveUser(empId);
                        if (success == 1)
                        {
                            MessageBox.Show("User Inactivated successfully", "Inactive user");
                            btnRefresh.PerformClick();
                        }
                    }
                   
                    
                }
                else//if no row selected                
                    MessageBox.Show("Please select a user to Inanctivate.", "Error - no user selection");
            }
            else
            {
                MessageBox.Show("To Inactivate a user please, refresh the table and select a user first.", "Error - Inactivate User.");
            }
        }

        
        private void dgvEmployees_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
           /* if (e.ColumnIndex == 1 && e.Value != null)
            {
                string originalValue = e.Value.ToString();
                string maskedValue = new string('*', originalValue.Length);      
                e.Value = maskedValue;
                e.FormattingApplied = true; 
            }*/
        }

        private void dgvEmployees_Scroll(object sender, ScrollEventArgs e)
        {
            ResetLastActivity();
        }

        private void dgvEmployees_SelectionChanged(object sender, EventArgs e)
        {
            ResetLastActivity();
        }
    }
}
