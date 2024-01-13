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
        public AddUpdateUserForm() { }
        public AddUpdateUserForm(string action)
        {
            InitializeComponent();
            lblEmpID.Text = "Automatic";
            LoadComboboxes();
        }

        public AddUpdateUserForm(Employee emp)
        {
            InitializeComponent();

            lblEmpID.Text = emp.EmployeeID.ToString();
            txtFName.Text= emp.FirstName.ToString();
            txtLName.Text= emp.LastName.ToString();
            txtEmail.Text= emp.Email.ToString();
            txtPassword.Text= emp.Password.ToString();
            
        }

        private void LoadComboboxes()
        {
            cboPosn.DataSource = new BindingSource(PositionClass.PositionNames, null);
            cboPosn.DisplayMember = "Value";
            cboPosn.ValueMember = "Key";


        }//end of LoadBomboboxes

      
    }
}
