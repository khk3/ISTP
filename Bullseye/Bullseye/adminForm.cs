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
        MySqlClass m = new MySqlClass();
        //Employee adminLogged = null;
        Employee userLogged= null;
        private void Init(string fName)
        {
            lblUser.Text = fName;

            lastActivityTime = DateTime.Now;
            Application.Idle += Application_Idle;
            ResetLastActivity();

            timer1 = new Timer();
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            timer1.Start();
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
                //Application.Idle -= Application_Idle;
                CloseForm();
                MessageBox.Show("Auto Logout due to inactivity","Admin Form- User Inactive");
                
            }
        }

        private void CloseForm()
        {
            Application.Idle -= Application_Idle; 
            Hide();
            Close();
        }

        private void ResetLastActivity()
        {
            lastActivityTime = DateTime.Now;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            this.Text = "Bullseye - "+ now.ToShortDateString() +" - " + now.ToLongTimeString();
        }

  

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ResetLastActivity();
            if (tabAdmin.SelectedIndex == 0)
            {                         
                
                DataTable empDataTable = m.GetAllEmployeesForAdmin();
                
                dgvEmployees.DataSource = empDataTable;
                //dgvEmployees.Columns[1].Visible = false; //hide positionID - will be used ti 
                dgvEmployees.Columns[5].Visible = false; //hido column positionID - will be used later to add/edit user
                dgvEmployees.Columns[7].Visible = false; //hide column siteID - will be used later to add/edit user
                dgvEmployees.ReadOnly = true;
                dgvEmployees.ClearSelection();         
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {//Add new user
            ResetLastActivity();
            dgvEmployees.DataSource = null;


            Application.Idle -= Application_Idle; //Stop Autologout
            CloseForm();
            AddUpdateUserForm au = new AddUpdateUserForm("add",userLogged,userLogged);           
            au.ShowDialog();
           
        }

        private void btnEditUser_Click(object sender, EventArgs e)
        {
            ResetLastActivity();
            if (dgvEmployees.DataSource != null)
            {
                if (dgvEmployees.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgvEmployees.SelectedRows[0];

                    int empId = Convert.ToInt32(selectedRow.Cells[0].Value);
                   // string pw= selectedRow.Cells[1].Value.ToString();
                    string fn= selectedRow.Cells[1].Value.ToString();
                    string ln= selectedRow.Cells[2].Value.ToString();
                    string email= selectedRow.Cells[3].Value.ToString();
                    bool active = Convert.ToBoolean(selectedRow.Cells[4].Value);
                    int posn = Convert.ToInt32(selectedRow.Cells[5].Value);
                    int site = Convert.ToInt32((selectedRow.Cells[7].Value));
                    bool locked= Convert.ToBoolean((selectedRow.Cells[9].Value));
                    string userName= selectedRow.Cells[10].Value.ToString();
                    string notes = selectedRow.Cells[11].Value?.ToString();

                    Employee empl = new Employee(empId,fn,ln,email,active,posn,site,locked,userName,notes); //

                    dgvEmployees.DataSource = null;
                    CloseForm();

                   
                    AddUpdateUserForm au = new AddUpdateUserForm("edit",empl, userLogged);
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


        private void dgvEmployees_Scroll(object sender, ScrollEventArgs e)
        {
            ResetLastActivity();
        }

        private void dgvEmployees_SelectionChanged(object sender, EventArgs e)
        {
            ResetLastActivity();
        }

        private void AdminForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Application.Idle -= Application_Idle;

        }
    }
}
