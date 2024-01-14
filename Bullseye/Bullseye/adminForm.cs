using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

        DateTime loginTime;

        public AdminForm(Employee user)
        {
            InitializeComponent();
            Init(user.FirstName);
            userLogged = user;

            //Time of login
            loginTime = DateTime.Now;
        }

        //class=level config to connection string
        static string connStr = ConfigurationManager.ConnectionStrings["bullseyedb"].ConnectionString;

        //create connection
        MySqlConnection conn = new MySqlConnection(connStr);

        Employee userLogged = null;
        private void Init(string fName)
        {
            lblUser.Text = fName;

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



        private void timer2_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            TimeSpan elapsed = now - loginTime;
            TimeSpan remainingTime = ConstantsClass.TimeToAutoLogout - elapsed;

            if (remainingTime <= TimeSpan.Zero)
            {
                btnLogOut.PerformClick();
                return;
            }

            this.Text = "Bullseye - "+ now.ToShortDateString() +" - " + now.ToLongTimeString()+" | Time to Auto Logout: "+ remainingTime.ToString(@"hh\:mm\:ss");
        }

        private void tabAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabAdmin.SelectedIndex == 0)
            {

            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if(tabAdmin.SelectedIndex == 0)
            {                         
                    MySqlClass m= new MySqlClass();
                    DataTable dt = m.RefreshDgv();
                    dgvEmployees.DataSource = dt;
                    dgvEmployees.ReadOnly = true;
                    dgvEmployees.ClearSelection();         
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {//Add new user

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
                    string notes= selectedRow.Cells[10].Value.ToString();

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
    }
}
