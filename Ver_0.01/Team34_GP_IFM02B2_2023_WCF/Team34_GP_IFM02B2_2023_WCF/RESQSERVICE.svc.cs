using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Team34_GP_IFM02B2_2023_WCF
{
        
        public class RESQSERVICE : IRESQSERVICE
        {
            ResQFoods_DCSDataContext db = new ResQFoods_DCSDataContext();
            public string DeleteUser(String uEmail)
            {
                throw new NotImplementedException();
            }

            public string GetData(int value)
            {
                return string.Format("You entered: {0}", value);
            }

            public List<UserRecord> GetEmployeeRecords(String uEmail)
            {
                throw new NotImplementedException();
            }

            private bool checkValid(String email)
            {
                bool valid = true;
                String con = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\USER\\Source\\Repos\\ResQFoods\\Ver_0.01\\Team34_GP_IFM02B2_2023_WCF\\Team34_GP_IFM02B2_2023_WCF\\App_Data\\ResQFoods_DB.mdf; Integrated Security = True";
                String query = "SELECT Count(Email) FROM UserTable WHERE Email = @userEmail";
                SqlConnection conn = new SqlConnection(con);
                SqlCommand sCom = new SqlCommand(query, conn);
                sCom.Parameters.AddWithValue("@userEmail", email);
                try
                {
                    conn.Open();
                    Object count = sCom.ExecuteScalar();
                    Int32 var;
                    if (Int32.TryParse(count.ToString(), out var))
                    {
                        if (var != 0)
                        {
                            valid = false;
                        }
                    }
                }
                catch (SqlException)
                {
                    Console.WriteLine("SQL ERROR");
                }
                finally
                {
                    conn.Close();
                }
                return valid;
            }
            //Look for user record type, return that type
            //Make 3 methods, one for each type of user
            public bool loginUser(String user, String pass)
            {
            /* var syslog = (from u in db.UserTables
                           where u.Email.Equals(user) &&
                           u.HashedPassword.Equals(pass)
                           select u).FirstOrDefault();*/

            /*var u = new UserTable
            {
                Email = 90;
            };*/


        bool valid = false;
                String con = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\USER\\Source\\Repos\\ResQFoods\\Ver_0.01\\Team34_GP_IFM02B2_2023_WCF\\Team34_GP_IFM02B2_2023_WCF\\App_Data\\ResQFoods_DB.mdf; Integrated Security = True";
                String query = "SELECT Count(Email) FROM UserTable WHERE Email = @userEmail AND HashedPassword = @userPass";
                SqlConnection conn = new SqlConnection(con);
                SqlCommand sCom = new SqlCommand(query, conn);
                sCom.Parameters.AddWithValue("@userEmail", user);
                sCom.Parameters.AddWithValue("@userPass", pass);
                try
                {
                
                    conn.Open();
                    Object count = sCom.ExecuteScalar();
                    Int32 var;
                    if (Int32.TryParse(count.ToString(), out var))
                    {
                        if (var == 1)
                        {
                            valid = true;
                        }
                    }
                }
                catch (SqlException)
                {
                    Console.WriteLine("SQL ERROR");
                }
                finally
                {
                    conn.Close();
                }
                return valid;
            }
            //Add an insert for the type of user registering (USERTYPE)
            public bool RegUser(String uEmail, char uType, String uPass, String uReg)
            {
                if (checkValid(uEmail))
                {
                String con = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\USER\\Source\\Repos\\ResQFoods\\Ver_0.01\\Team34_GP_IFM02B2_2023_WCF\\Team34_GP_IFM02B2_2023_WCF\\App_Data\\ResQFoods_DB.mdf; Integrated Security = True";
                String query = "INSERT INTO UserTable(UserType, HashedPassword, Email, DateRegistered, Enabled) VALUES(@param1, @param2, @param3, @param4, @param5 )";
                SqlConnection conn = new SqlConnection(con);
                    SqlCommand sCom = new SqlCommand(query, conn);
                    sCom.Parameters.AddWithValue("@param1", uType);
                    sCom.Parameters.AddWithValue("@param2", uPass);
                    sCom.Parameters.AddWithValue("@param3", uEmail);
                    sCom.Parameters.AddWithValue("@param4", uReg);
                    sCom.Parameters.AddWithValue("@param5", true);
                try
                    {
                        conn.Open();
                        int recordsAffected = sCom.ExecuteNonQuery();
                    }
                    catch (SqlException)
                    {
                        // error here
                    }
                    finally
                    {
                        conn.Close();
                    }

                    return true;
                }
                return false;
            }

            public List<UserRecord> SearchUser(string uEmail)
            {
                throw new NotImplementedException();
            }

            public string UpdateUser(String uEmail)
            {
                throw new NotImplementedException();
            }

        public List<ProductRecord> getAllProducts()
        {
            String conn = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\USER\\Source\\Repos\\ResQFoods\\Ver_0.01\\Team34_GP_IFM02B2_2023_WCF\\Team34_GP_IFM02B2_2023_WCF\\App_Data\\ResQFoods_DB.mdf; Integrated Security = True";
            String comm = "SELECT * FROM Products";
            SqlConnection SqlConn = new SqlConnection(conn);
            SqlCommand SqlComm = new SqlCommand(comm, SqlConn);
            try
            {
                SqlConn.Open();
                DbDataReader dbr = SqlComm.ExecuteReader();
                while(dbr.Read())
                {
                    byte[] tempImg = null;
                    ProductRecord temp = new ProductRecord();
                    temp.prodID = dbr.GetInt32(0);
                    temp.userID = dbr.GetInt32(1);
                    temp.prodName = dbr.GetString(2);
                    temp.prodDesc = dbr.GetString(3);
                    

                }
            }
            catch(SqlException s)
            {
                //Error handling logic here
            }
            finally
            {
                SqlConn.Close();
            }
            throw new NotImplementedException();
        }

        public bool RegUser(string fName, string lName, string uEmail, int uType, string uPass, string phone, string uReg)
        {
            throw new NotImplementedException();
        }

        UserRecord IRESQSERVICE.loginUser(string uEmail, string pass)
        {
            throw new NotImplementedException();
        }
    }
    }

