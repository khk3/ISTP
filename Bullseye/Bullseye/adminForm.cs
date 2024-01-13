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

        public AdminForm(Employee user)
        {
            InitializeComponent();
            Init(user.FirstName);
        }

        //class=level config to connection string
        static string connStr = ConfigurationManager.ConnectionStrings["bullseyedb"].ConnectionString;

        //create connection
        MySqlConnection conn = new MySqlConnection(connStr);

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
            this.Text = "Admin - " + DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToLongTimeString();
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
                string cmd = "select * from employee";
                try
                {
                    MySqlCommand sqlCmd = new MySqlCommand(cmd, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvEmployees.DataSource = dt;
                    dgvEmployees.ReadOnly = true;
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Could not retrieve employees. Error: " + sqlEx.Message, "Error - SQL Exception");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not retrieve employees. Error: " + ex.Message, "Error");

                }

            }
           
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {//Add new user
            AddUpdateUserForm au= new AddUpdateUserForm("add");
            au.ShowDialog();
        }

        private void btnEditUser_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.DataSource != null)
            {
                if (dgvEmployees.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgvEmployees.SelectedRows[0];

                    int empID = Convert.ToInt32(selectedRow.Cells[0].Value);
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

                    Employee emp = new Employee(empID,pw,fn,ln,email,active,posn,site,locked,userName,notes);
                    
                    AddUpdateUserForm au= new AddUpdateUserForm(emp);                  
                }
                else//if no row selected                
                    MessageBox.Show("Please select a user to update.", "Error - no user selection");            
            }
          
        }
    }
}
