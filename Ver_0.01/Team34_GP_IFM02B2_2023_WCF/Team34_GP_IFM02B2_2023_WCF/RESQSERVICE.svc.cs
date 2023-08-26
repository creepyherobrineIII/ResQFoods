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

            private bool checkUserValid(String email)
            {

            /*String con = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\USER\\Source\\Repos\\ResQFoods\\Ver_0.01\\Team34_GP_IFM02B2_2023_WCF\\Team34_GP_IFM02B2_2023_WCF\\App_Data\\ResQFoods_DB.mdf; Integrated Security = True";
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
            return valid;*/


            bool exists = false;
            var check = (from u in db.UserTables
                         where u.Email.Equals(email)
                         select u).FirstOrDefault();
            if (check != null)
            {
                exists = true;
            }
            return exists;


        }

            //Look for user record type, return that type
            //Make 3 methods, one for each type of user
            public int loginUser(String user, String pass)
            {
            /* var syslog = (from u in db.UserTables
                           where u.Email.Equals(user) &&
                           u.HashedPassword.Equals(pass)
                           select u).FirstOrDefault();*/

            /*var u = new UserTable
            {
                Email = 90;
            };*/


                /*bool valid = false;
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
                return valid;*/

            var check = (from u in db.UserTables
                             where u.Email.Equals(user) &&
                             u.HashedPassword.Equals(pass)
                             select u).FirstOrDefault(); 
            if(check!=null)
            {
                int t = check.UserType;
                return t;
            }
            return -1;
            }
            //Add an insert for the type of user registering (USERTYPE)

            private bool regUser(String uEmail, String uPass, int type)
            {
            if (!checkUserValid(uEmail))
            {
                UserTable a = new UserTable
                {
                    Email = uEmail,
                    UserType = type,
                    HashedPassword = uPass,
                    DateRegistered = System.DateTime.Today,
                    Enabled = true
                };
                try
                {
                    db.UserTables.InsertOnSubmit(a);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception e)
                {
                    e.GetBaseException();
                }
            }
            return false;
        }
            public bool regAdmin(String uEmail, String uPass)
            {
                /*if (checkValid(uEmail))
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
                return false;*/

            if(regUser(uEmail, uPass, 0))
            {
                return true;
            }
            return false;
            }

            public bool regCust(String uEmail, String uPass, String fName, String lName, DateTime BDate, bool grant)
            {
            if (regUser(uEmail, uPass, 1))
            {
               
                var check = (from u in db.UserTables
                             where u.Email.Equals(uEmail)
                             select u).FirstOrDefault();

                        if (check != null)
                        {
                            Customer c = new Customer
                            {
                                UserId = check.UserId,
                                FirstName = fName,
                                LastName = lName,
                                Birthdate = BDate,
                                GrantRecipient = grant
                            };
                        try
                        {
                            db.Customers.InsertOnSubmit(c);
                            db.SubmitChanges();
                             return true;
                         }
                        catch (Exception ex)
                        {
                            ex.GetBaseException();
                            return false;
                        }
                        }
            }
            return false;
        }

            public bool regStore(String uEmail, String uPass, String comp, String name, String icoPath, String loc, String type)
        {
            if (regUser(uEmail, uPass, 2))
            {
                var check = (from u in db.UserTables
                             where u.Email.Equals(uEmail)
                             select u).FirstOrDefault();

                if (check != null)
                {
                    Store s = new Store
                    {
                        UserId = check.UserId,
                        Company = comp,
                        Name = name,
                        Logo = icoPath,
                        Location = loc,
                        StoreType = type
                    };
                    try
                    {
                        db.Stores.InsertOnSubmit(s);
                        db.SubmitChanges();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        ex.GetBaseException();
                        return false;
                    }
                }
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
            /*String conn = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\USER\\Source\\Repos\\ResQFoods\\Ver_0.01\\Team34_GP_IFM02B2_2023_WCF\\Team34_GP_IFM02B2_2023_WCF\\App_Data\\ResQFoods_DB.mdf; Integrated Security = True";
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
            throw new NotImplementedException();*/
            List<ProductRecord> prodRec = new List<ProductRecord>();
            List<Product> prods = (from p in db.Products 
                                    select p).ToList();
            foreach(Product p in prods)
            {
                ProductRecord pr = new ProductRecord();
                pr.storeId = p.UserId;
                pr.prodId = p.ProductId;
                pr.prodName = p.Name;
                pr.prodPic = p.Picture;
                pr.prodPrice = (double)p.Price;
                pr.prodDesc = p.Description;
                pr.prodDate = p.DateAdded;
                prodRec.Add(pr);
            }

            return prodRec;
            
        }

        public UserRecord getAdmin(string uEmail)
        {
            UserRecord ur = new UserRecord();
            var us = (from u in db.UserTables
                            where u.Email.Equals(uEmail)
                            select u).FirstOrDefault();
            if(us!=null)
            {
                ur.userId = us.UserId;
                ur.userEmail = us.Email;
                ur.userReg = us.DateRegistered;
                ur.userType = us.UserType;
                ur.enabled = us.Enabled;
            }
            return ur;
        }

        public CustomerRecord getCustomer(string uEmail)
        {
            UserRecord inner = getAdmin(uEmail);
            CustomerRecord c = new CustomerRecord();
            var cs = (from cus in db.Customers
                      where cus.UserId == inner.userId
                      select cus).FirstOrDefault();
            if (cs != null)
            {
                c.u = inner;
                c.fName = cs.FirstName;
                c.lName = cs.LastName;
                c.birthDate = cs.Birthdate;
                c.grantRec = cs.GrantRecipient;
            }

            return c;

        }

        public StoreRecord getStore(string uEmail)
        {
            UserRecord inner = getAdmin(uEmail);
            StoreRecord s = new StoreRecord();
            var ss = (from str in db.Stores
                      where str.UserId == inner.userId
                      select str).FirstOrDefault();
            if (ss != null)
            {
                s.u = inner;
                s.company = ss.Company;
                s.location = ss.Location;
                s.logo = ss.Location;
                s.name = ss.Name;
                s.sType = ss.StoreType;
            }

            return s;
        }
    }
    }

