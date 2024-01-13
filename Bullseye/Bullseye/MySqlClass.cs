using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Bullseye
{
    public class MySqlClass
    {
        public MySqlClass() { }

        //class=level config to connection string
        static string connStr = ConfigurationManager.ConnectionStrings["bullseyedb"].ConnectionString;

        //create connection
        MySqlConnection conn = new MySqlConnection(connStr);

        public void OpenDb()
        {
            //open the connection - needs a try catch
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error - Closing form");               
            }

        }//end of OpenDB

        public void RunScript()
        {
            //
            try
            {
                string script = System.IO.File.ReadAllText("Bullseye_DB2024_1.3.sql");
                MySqlCommand cmd = new MySqlCommand(script, conn);
                //Run the script
                int num = cmd.ExecuteNonQuery();
            }
            catch (MySqlException sqlEx)
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

        public List<Employee> LoadEmployees()
        {
            //create script and command
            string cmd = "select * from employee ";
            MySqlCommand sqlCmd = new MySqlCommand(cmd, conn);

            List<Employee> employees = new List<Employee>();
            try
            {
                MySqlDataReader reader = sqlCmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Employee employee = new Employee(
                            //reader.GetInt32(reader.GetOrdinal("employeeID")),
                            reader.GetString(reader.GetOrdinal("password")),
                            reader.GetString(reader.GetOrdinal("firstName")),
                            reader.GetString(reader.GetOrdinal("lastName")),
                            reader.GetString(reader.GetOrdinal("email")),
                            reader.GetBoolean(reader.GetOrdinal("active")),
                            reader.GetInt32(reader.GetOrdinal("PositionID")),
                            reader.GetInt32(reader.GetOrdinal("siteID")),
                            reader.GetBoolean(reader.GetOrdinal("locked")),
                            reader.GetString(reader.GetOrdinal("username")),
                            reader.IsDBNull(reader.GetOrdinal("notes")) ? null : reader.GetString(reader.GetOrdinal("notes"))
                        );

                        // Add the Employee object to the list
                        employees.Add(employee);
                    }

                    reader.Close();
                   
                }
                return employees;
            }
            catch (MySqlException sqlEx)
            {
                MessageBox.Show("Could not find Employees. System Error: " + sqlEx.Message, "Error - could not find Employees");
            }
            catch (Exception ex)
            {
                // Handle other general exceptions
                MessageBox.Show("Exception: " + ex.Message, "Error - Cannot run script");
            }
            return new List<Employee>();
        }

        public bool CheckLocked(string userName)
        {
            OpenDb();
            string cmd = "Select locked from employee where username ='" + userName + "'";
            MySqlCommand sqlCmd = new MySqlCommand(cmd, conn);
            try
            {
                MySqlDataReader reader = sqlCmd.ExecuteReader();
                reader.Read();

                bool isLocked = reader.GetBoolean(reader.GetOrdinal("locked"));
                reader.Close();
              
                conn.Close();
                return isLocked;
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Could not Lock the user. Error: " + sqlEx.Message, "Error- SQL Lock user");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not Lock the user. Error: " + ex.Message, "Error- Lock user");
            }
            conn.Close();
            return false;
        }

        public int LockUser(string userName)
        {
            OpenDb();
            string cmd = "update employee set locked = 1 where username = @userName";

            MySqlCommand sqlCommand = new MySqlCommand(cmd, conn);
            sqlCommand.Parameters.AddWithValue("@userName", userName);

            try
            {
                int update = sqlCommand.ExecuteNonQuery();
                conn.Close();
                return update;
            }
            catch (MySqlException sqlEx)
            {
                MessageBox.Show("Could not Lock Employee. Error: " + sqlEx.Message, " Error - MySQL");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not Lock Employee. Error: " + ex.Message, " Error - Exception");
            }
            conn.Close();
            return 0;
        }
        public bool isUserNameDuplicated(string userName)
        {
            OpenDb();
            string cmd= "select * from employee where username='"+userName + "'";
            MySqlCommand sqlCmd = new MySqlCommand(cmd, conn);

            bool isDuplicate = false;
            try
            {
                MySqlDataReader reader = sqlCmd.ExecuteReader();
                if (reader.HasRows)
                {             
                    isDuplicate= true;
                }
                else
                {               
                    isDuplicate= false;
                }
            }
            catch (MySqlException sqlEx)
            {
                MessageBox.Show("Could not Update Employee. Error: " + sqlEx.Message, " Error - MySQL");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not Update Employee. Error: " + ex.Message, " Error - Exception");
            }
            conn.Close();
            return isDuplicate;
        }


        public int AddUser(Employee emp)
        {
            string addUserQuery = $"INSERT INTO employee (Password, FirstName, LastName, Email, active, PositionID, siteID, locked, username, notes) VALUES " +
                   $"('{emp.Password}', '{emp.FirstName}', '{emp.LastName}', '{emp.Email}', {Convert.ToInt32(emp.Active)}, {emp.PositionID}, {emp.SiteID}, {Convert.ToInt32(emp.Locked)}, '{emp.UserName}', '{emp.Notes}')";
            OpenDb();
            MySqlCommand cmd = new MySqlCommand(addUserQuery, conn);
            int result = 0;
            try
            {
                result = cmd.ExecuteNonQuery();

            }
            catch (MySqlException sqlEx)
            {
                MessageBox.Show("Could not Add Employee. Error: " + sqlEx.Message, " Error - MySQL");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not Add Employee. Error: " + ex.Message, " Error - Exception");
            }
            return result;

        }

        public int UpdateUser(Employee emp) {
            OpenDb();
            string updateQuery = "UPDATE employee SET Password = @Password, FirstName = @FirstName, LastName = @LastName, Email = @Email, active = @Active, PositionID = @PositionID, " +
                         "siteID = @SiteID, locked = @Locked, username = @UserName, notes = @Notes WHERE employeeID = @EmployeeID";
            MySqlCommand sqlCommand = new MySqlCommand(updateQuery, conn);
            sqlCommand.Parameters.AddWithValue("@Password", emp.Password);
            sqlCommand.Parameters.AddWithValue("@FirstName", emp.FirstName);
            sqlCommand.Parameters.AddWithValue("@LastName", emp.LastName);
            sqlCommand.Parameters.AddWithValue("@Email", emp.Email);
            sqlCommand.Parameters.AddWithValue("@Active", emp.Active ? 1 : 0);
            sqlCommand.Parameters.AddWithValue("@PositionID", emp.PositionID);
            sqlCommand.Parameters.AddWithValue("@SiteID", emp.SiteID);
            sqlCommand.Parameters.AddWithValue("@Locked", emp.Locked ? 1 : 0);
            sqlCommand.Parameters.AddWithValue("@UserName", emp.UserName);
            sqlCommand.Parameters.AddWithValue("@Notes", emp.Notes);
            sqlCommand.Parameters.AddWithValue("@EmployeeID", emp.EmployeeID);
            try
            {
                int update = sqlCommand.ExecuteNonQuery();
                conn.Close();
                return update;
            }
            catch (MySqlException sqlEx)
            {
                MessageBox.Show("Could not Update Employee. Error: " + sqlEx.Message, " Error - MySQL");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not Update Employee. Error: " + ex.Message, " Error - Exception");
            }
            conn.Close();
            return 0;

        }

        public int DeleteUser(int empId)
        {
            OpenDb();
            string cmd = "DELETE from employee where employeeID=@empId";

            MySqlCommand sqlCommand = new MySqlCommand(cmd, conn);
            sqlCommand.Parameters.AddWithValue("@empId", empId);

            try
            {
                int delete = sqlCommand.ExecuteNonQuery();
                conn.Close();
                return delete;
            }
            catch (MySqlException sqlEx)
            {
                MessageBox.Show("Could not Delete Employee. Error: " + sqlEx.Message, " Error - MySQL");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not Delete Employee. Error: " + ex.Message, " Error - Exception");
            }
            conn.Close();
            return 0;

        }



    }//end of class
}
