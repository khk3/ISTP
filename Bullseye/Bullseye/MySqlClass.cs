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
                            reader.GetInt32(reader.GetOrdinal("employeeID")),
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

        public int UpdateEmployee(string userName)
        {
            OpenDb();
            string cmd = "update employee set locked = 1 where username ='" + userName + "'";
            MySqlCommand sqlCommand = new MySqlCommand(cmd, conn);


            int update = sqlCommand.ExecuteNonQuery();
            conn.Close();
            return update;
        }

    }//end of class
}
