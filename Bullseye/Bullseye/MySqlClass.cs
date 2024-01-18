using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
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
        static string connStr = ConfigurationManager.ConnectionStrings[ConstantsClass.DatabaseName].ConnectionString;

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
            try
            {
                string script = System.IO.File.ReadAllText(ConstantsClass.ScriptName);
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
                        int employeeID = reader.GetInt32(reader.GetOrdinal("employeeID"));
                        string password = reader.GetString(reader.GetOrdinal("password"));
                        //string asteriskPassword = new string('*', password.Length);

                        string firstName = reader.GetString(reader.GetOrdinal("firstName"));
                        string lastName = reader.GetString(reader.GetOrdinal("lastName"));
                        string email = reader.GetString(reader.GetOrdinal("email"));
                        bool active = reader.GetBoolean(reader.GetOrdinal("active"));
                        int positionID = reader.GetInt32(reader.GetOrdinal("PositionID"));
                        int siteID = reader.GetInt32(reader.GetOrdinal("siteID"));
                        bool locked = reader.GetBoolean(reader.GetOrdinal("locked"));
                        string username = reader.GetString(reader.GetOrdinal("username"));
                        string notes = reader.IsDBNull(reader.GetOrdinal("notes")) ? null : reader.GetString(reader.GetOrdinal("notes"));

                        Employee employee = new Employee(employeeID, password, firstName, lastName, email, active, positionID, siteID, locked, username, notes);
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
            string cmd = "Select locked from employee where username ='" + userName + "';";
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
                MessageBox.Show("Could not Check if user is locked. Error: " + sqlEx.Message, "Error- SQL Lock user");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not Check uf user is locked. Error: " + ex.Message, "Error- Lock user");
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

        public Employee[] GetAllEmployees()
        {
            OpenDb();
            string cmd = "select * from employee";

            MySqlCommand sqlCmd = new MySqlCommand(cmd, conn);
            List<Employee> employeesList = new List<Employee>();
            try
            {
                MySqlDataReader reader = sqlCmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader != null && reader.Read())
                    {
                        int employeeID = reader.GetInt32(reader.GetOrdinal("employeeID"));
                        string password = reader.GetString(reader.GetOrdinal("password"));
                        string firstName = reader.GetString(reader.GetOrdinal("firstName"));
                        string lastName = reader.GetString(reader.GetOrdinal("lastName"));
                        string email = reader.GetString(reader.GetOrdinal("email"));
                        bool active = reader.GetBoolean(reader.GetOrdinal("active"));
                        int position = reader.GetInt32(reader.GetOrdinal("positionID"));
                        int site = reader.GetInt32(reader.GetOrdinal("siteID"));
                        bool locked = reader.GetBoolean(reader.GetOrdinal("locked"));
                        string userName = reader.GetString(reader.GetOrdinal("username"));
                        string notes = reader.IsDBNull(reader.GetOrdinal("notes")) ? null : reader.GetString(reader.GetOrdinal("notes"));

                        Employee emp = new Employee(employeeID, password, firstName, lastName, email, active, position, site, locked, userName, notes);
                        employeesList.Add(emp);
                    }
                }
                //MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCmd);
                //DataTable dt = new DataTable();
                // adapter.Fill(dt);




                /*foreach (DataRow row in dt.Rows)
                {
                    if (dt.Columns.Contains("password"))
                    {
                        string password = row["password"].ToString();
                        int passwordLength = password.Length;
                        row["password"] = new string('*', passwordLength);
                    }
                }*/
                Employee[] empArray = employeesList.ToArray();
                conn.Close();
                return empArray;
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Could not retrieve employees. Error: " + sqlEx.Message, "Error - SQL Exception");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not retrieve employees. Error: " + ex.Message, "Error");
            }
            conn.Close();
            return null;
        }

        public bool isUserNameDuplicated(string userName)
        {
            OpenDb();
            string cmd = "select * from employee where username='" + userName + "'";
            MySqlCommand sqlCmd = new MySqlCommand(cmd, conn);

            bool isDuplicate = false;
            try
            {
                MySqlDataReader reader = sqlCmd.ExecuteReader();
                if (reader.HasRows)
                {
                    isDuplicate = true;
                }
                else
                {
                    isDuplicate = false;
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

        public int UpdateUser(Employee emp)
        {
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

        //RECOVER PASSWORD BTN
        public int UpdatePassword(string hashedPassword, string userName)
        {
            OpenDb();
            string updateQuery = "update employee set password = @password where username = @userName";
            MySqlCommand sqlCommand = new MySqlCommand(updateQuery, conn);
            sqlCommand.Parameters.AddWithValue("@Password", hashedPassword);

            sqlCommand.Parameters.AddWithValue("@UserName", userName);

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

        public int InactiveUser(int empId)
        {
            OpenDb();
            string cmd = "Update employee set active=0 where employeeID=@empId";

            MySqlCommand sqlCommand = new MySqlCommand(cmd, conn);
            sqlCommand.Parameters.AddWithValue("@empId", empId);

            try
            {
                int inactive = sqlCommand.ExecuteNonQuery();
                conn.Close();
                return inactive;
            }
            catch (MySqlException sqlEx)
            {
                MessageBox.Show("Could not Inactivate Employee. Error: " + sqlEx.Message, " Error - MySQL");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not Inactivate Employee. Error: " + ex.Message, " Error - Exception");
            }
            conn.Close();
            return 0;

        }

        public LocationClass[] GetAllLocations()
        {
            OpenDb();

            string cmd = "select * from site;";
            MySqlCommand sqlCmd = new MySqlCommand(cmd, conn);

            List<LocationClass> locationsList = new List<LocationClass>();
            try
            {
                MySqlDataReader reader = sqlCmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader != null && reader.Read())
                    {
                        int siteID = reader.GetInt32(reader.GetOrdinal("siteID"));
                        string name = reader.GetString(reader.GetOrdinal("name"));
                        string provinceID = reader.GetString(reader.GetOrdinal("provinceID"));
                        string address = reader.GetString(reader.GetOrdinal("address"));
                        string address2 = reader.IsDBNull(reader.GetOrdinal("address2")) ? null : reader.GetString(reader.GetOrdinal("address2"));
                        string city = reader.GetString(reader.GetOrdinal("city"));
                        string country = reader.GetString(reader.GetOrdinal("country"));
                        string postalCode = reader.GetString(reader.GetOrdinal("postalCode"));
                        string phone = reader.GetString(reader.GetOrdinal("phone"));
                        string dayOfWeek = reader.GetString(reader.GetOrdinal("dayOfWeek"));
                        int distanceFromWH = reader.GetInt32(reader.GetOrdinal("distanceFromWH"));
                        string notes = reader.IsDBNull(reader.GetOrdinal("notes")) ? null : reader.GetString(reader.GetOrdinal("notes"));

                        LocationClass location = new LocationClass(siteID, name, provinceID, address, address2, city, country, postalCode, phone, dayOfWeek, distanceFromWH, notes);

                        locationsList.Add(location);
                    }
                    reader.Close();
                }
                LocationClass[] locationsArray = locationsList.ToArray();
                conn.Close();
                return locationsArray;
            }
            catch (MySqlException sqlEx)
            {
                MessageBox.Show("Could not find Locations. System Error: " + sqlEx.Message, "Error - could not find Locations");
            }
            catch (Exception ex)
            {
                // Handle other general exceptions
                MessageBox.Show("Exception: " + ex.Message, "Error - Cannot find Locations");
            }
            conn.Close();
            return null;
        }

        public PositionClass[] GetAllPositions()
        {
            OpenDb();
            //create script and command
            string cmd = "select * from posn;";
            MySqlCommand sqlCmd = new MySqlCommand(cmd, conn);

            List<PositionClass> positionsList = new List<PositionClass>();
            try
            {
                MySqlDataReader reader = sqlCmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader != null && reader.Read())
                    {
                        int positionID = reader.GetInt32(reader.GetOrdinal("positionID"));
                        string permissionLevel = reader.GetString(reader.GetOrdinal("permissionLevel"));
                        PositionClass position = new PositionClass(positionID, permissionLevel);
                        positionsList.Add(position);
                    }
                    reader.Close();
                }
                PositionClass[] positionsArray = positionsList.ToArray();
                conn.Close();
                return positionsArray;
            }
            catch (MySqlException sqlEx)
            {
                MessageBox.Show("Could not find Positions. System Error: " + sqlEx.Message, "Error - could not find Positions");
            }
            catch (Exception ex)
            {
                // Handle other general exceptions
                MessageBox.Show("Exception: " + ex.Message, "Error - Cannot find Positions");
            }
            conn.Close();
            return null;
        }

        public string GetPosition(int positionID)
        {
            string permissionLevel = "";
            string getPositionQuery = "SELECT permissionLevel FROM posn WHERE positionID = @positionID";

            try
            {
                OpenDb();
                using (MySqlCommand cmd = new MySqlCommand(getPositionQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@positionID", positionID);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        permissionLevel = reader.GetString(reader.GetOrdinal("permissionLevel"));
                    }

                }
            }
            catch (MySqlException sqlEx)
            {
                MessageBox.Show("Could not get position. Error: " + sqlEx.Message, "Error - MySQL");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not get position. Error: " + ex.Message, "Error - Exception");
            }
            finally
            {
                conn.Close();
            }

            return permissionLevel;
        }


        public string GetLocation(int siteID)
        {
            string location = "";
            string getPositionQuery = "SELECT name FROM site WHERE siteID = @siteID";

            try
            {
                OpenDb();
                using (MySqlCommand cmd = new MySqlCommand(getPositionQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@siteID", siteID);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        location = reader.GetString(reader.GetOrdinal("name"));
                    }

                }
            }
            catch (MySqlException sqlEx)
            {
                MessageBox.Show("Could not get location. Error: " + sqlEx.Message, "Error - MySQL");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not get location. Error: " + ex.Message, "Error - Exception");
            }
            finally
            {
                conn.Close();
            }

            return location;
        }


        public ItemClass[] GetAllItems()
        {
            OpenDb();
            string cmd = "select * from item;";
            MySqlCommand sqlCmd = new MySqlCommand(cmd, conn);

            List<ItemClass> itemsList = new List<ItemClass>();
            try
            {
                MySqlDataReader reader = sqlCmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader != null && reader.Read())
                    {
                        int siteID = reader.GetInt32(reader.GetOrdinal("itemID"));
                        string name = reader.GetString(reader.GetOrdinal("name"));
                        int sku = reader.GetInt32(reader.GetOrdinal("sku"));
                        string description = reader.GetString(reader.GetOrdinal("description"));
                        string category = reader.GetString(reader.GetOrdinal("category"));
                        double weight = reader.GetDouble(reader.GetOrdinal("weight"));
                        int caseSize = reader.GetInt32(reader.GetOrdinal("caseSize"));
                        double costPrice = reader.GetDouble(reader.GetOrdinal("costPrice"));
                        double retailPrice = reader.GetDouble(reader.GetOrdinal("retailPrice"));
                        int supplierID = reader.GetInt32(reader.GetOrdinal("supplierID"));
                        int active = reader.GetInt32(reader.GetOrdinal("active"));
                        string notes = reader.IsDBNull(reader.GetOrdinal("notes")) ? null : reader.GetValue(reader.GetOrdinal("notes")).ToString();
                        ItemClass item = new ItemClass(siteID, name, sku, description, category, weight, caseSize, costPrice, retailPrice, supplierID, active, notes);

                        itemsList.Add(item);
                    }
                    reader.Close();
                }
                ItemClass[] ItemsArray = itemsList.ToArray();
                conn.Close();
                return ItemsArray;
            }
            catch (MySqlException sqlEx)
            {
                MessageBox.Show("Could not find Items. System Error: " + sqlEx.Message, "Error - could not find Items");
            }
            catch (Exception ex)
            {
                // Handle other general exceptions
                MessageBox.Show("Exception: " + ex.Message, "Error - Cannot find Items");
            }
            conn.Close();
            return null;
        }

        public string[] GetItemCategories()
        {
            OpenDb();
            string cmd = "select * from category";
            MySqlCommand sqlCmd = new MySqlCommand(cmd, conn);
            try
            {
                MySqlDataReader reader = sqlCmd.ExecuteReader();
                List<string> categoriesList = new List<string>();
                if (reader.HasRows)
                {
                    while (reader != null && reader.Read())
                    {

                        string category = reader.GetString(reader.GetOrdinal("categoryName"));
                        categoriesList.Add(category);

                    }
                    reader.Close();
                }
                string[] categoriesArray = categoriesList.ToArray();
                conn.Close();
                return categoriesArray;
            }
            catch (MySqlException sqlEx)
            {
                MessageBox.Show("Could not find Items. System Error: " + sqlEx.Message, "Error - could not find Items");
            }
            catch (Exception ex)
            {
                // Handle other general exceptions
                MessageBox.Show("Exception: " + ex.Message, "Error - Cannot find Items");
            }
            conn.Close();
            return null;

        }

        public int AddItem(ItemClass item)
        {
            string addItemQuery = "Insert into item values (@name, @sku, @desciption, @category, @weight," +
                " @caseSize,@costPrice, @retailPrice,@supplier,@active,@notes)";
            OpenDb();
            MySqlCommand cmd = new MySqlCommand(addItemQuery, conn);
            //cmd.Parameters.AddWithValue("@itemID", item.ItemID);
            cmd.Parameters.AddWithValue("@name", item.Name);
            cmd.Parameters.AddWithValue("@sku", item.Sku);
            cmd.Parameters.AddWithValue("@description", item.Description);
            cmd.Parameters.AddWithValue("@category", item.Category);
            cmd.Parameters.AddWithValue("@weight", item.Weight);
            cmd.Parameters.AddWithValue("@caseSize", item.CaseSize);
            cmd.Parameters.AddWithValue("@costPrice", item.CostPrice);
            cmd.Parameters.AddWithValue("@retailPrice", item.RetailPrice);
            cmd.Parameters.AddWithValue("@supplier", item.SupplierID);
            cmd.Parameters.AddWithValue("@active", item.Active);
            cmd.Parameters.AddWithValue("@notes", item.Notes);


            int result = 0;
            try
            {
                result = cmd.ExecuteNonQuery();
                conn.Close();
                return result;
            }
            catch (MySqlException sqlEx)
            {
                MessageBox.Show("Could not Add Item. Error: " + sqlEx.Message, " Error - MySQL");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not Add Item. Error: " + ex.Message, " Error - Exception");
            }
            conn.Close();
            return result;

        }

        public int EditItem(ItemClass item)
        {
            string addItemQuery = "Update item set name= @name, sku=@sku, description=@description, category=@category, weight=@weight," +
                " caseSize=@caseSize,costPrice=@costPrice, retailPrice=@retailPrice,supplierID=@supplier,active=@active,notes=@notes where itemID= @itemID";
            OpenDb();
            MySqlCommand cmd = new MySqlCommand(addItemQuery, conn);
            cmd.Parameters.AddWithValue("@itemID", item.ItemID);
            cmd.Parameters.AddWithValue("@name", item.Name);
            cmd.Parameters.AddWithValue("@sku", item.Sku);
            cmd.Parameters.AddWithValue("@description", item.Description);
            cmd.Parameters.AddWithValue("@category", item.Category);
            cmd.Parameters.AddWithValue("@weight", item.Weight);
            cmd.Parameters.AddWithValue("@caseSize", item.CaseSize);
            cmd.Parameters.AddWithValue("@costPrice", item.CostPrice);
            cmd.Parameters.AddWithValue("@retailPrice", item.RetailPrice);
            cmd.Parameters.AddWithValue("@supplier", item.SupplierID);
            cmd.Parameters.AddWithValue("@active", item.Active);
            cmd.Parameters.AddWithValue("@notes", item.Notes);


            int result = 0;
            try
            {
                result = cmd.ExecuteNonQuery();
                conn.Close();
                return result;
            }
            catch (MySqlException sqlEx)
            {
                MessageBox.Show("Could not Update Item. Error: " + sqlEx.Message, " Error - MySQL");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not Update Item. Error: " + ex.Message, " Error - Exception");
            }
            conn.Close();
            return result;
        }

        public SupplierClass[] GetAllSuppliers()
        {
            OpenDb();
            string cmd = "select * from supplier";
            MySqlCommand sqlCmd = new MySqlCommand(cmd, conn);

            List<SupplierClass> suppliersList = new List<SupplierClass>();
            try
            {
                MySqlDataReader reader = sqlCmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader != null && reader.Read())
                    {
                        int supplierID = reader.GetInt32(reader.GetOrdinal("supplierID"));
                        string name = reader.GetString(reader.GetOrdinal("name"));
                        string address1 = reader.GetString(reader.GetOrdinal("address1"));
                        string address2 = reader.IsDBNull(reader.GetOrdinal("address2")) ? null : reader.GetValue(reader.GetOrdinal("address2")).ToString();

                        string city = reader.GetString(reader.GetOrdinal("city"));
                        string country = reader.GetString(reader.GetOrdinal("country"));
                        string province = reader.GetString(reader.GetOrdinal("province"));
                        string postalCode = reader.GetString(reader.GetOrdinal("postalcode"));
                        string phone = reader.GetString(reader.GetOrdinal("phone"));
                        string contact = reader.GetString(reader.GetOrdinal("contact"));
                        string notes = reader.IsDBNull(reader.GetOrdinal("notes")) ? null : reader.GetValue(reader.GetOrdinal("notes")).ToString();

                        SupplierClass supplier = new SupplierClass(supplierID, name, address1, address2, city, country, province, postalCode, phone, contact, notes);


                        suppliersList.Add(supplier);
                    }
                    reader.Close();
                }
                SupplierClass[] suppliersArray = suppliersList.ToArray();
                conn.Close();
                return suppliersArray;
            }
            catch (MySqlException sqlEx)
            {
                MessageBox.Show("Could not find Items. System Error: " + sqlEx.Message, "Error - could not find Items");
            }
            catch (Exception ex)
            {
                // Handle other general exceptions
                MessageBox.Show("Exception: " + ex.Message, "Error - Cannot find Items");
            }
            conn.Close();
            return null;

        }//end of class
    }
}
