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

            public List<UserTable> GetEmployeeRecords(String uEmail)
            {
                throw new NotImplementedException();
            }

            private bool checkUserValid(String email)
            {

            bool exists = (from u in db.UserTables
                         where u.Email.Equals(email)
                         select u).Any();
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
          
            if(checkUserValid(user))
            {
                var userT = (from u in db.UserTables
                              where u.Email.Equals(user) &&
                              u.HashedPassword.Equals(pass)
                              select u).FirstOrDefault(); 

                int t = userT.UserType;
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
            

            public List<UserTable> SearchUser(string uEmail)
            {
                throw new NotImplementedException();
            }

            public string UpdateUser(String uEmail)
            {
                throw new NotImplementedException();
            }

            public List<Product> getAllProducts()
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
                List<Product> prodRec = new List<Product>();
                List<Product> prods = (from p in db.Products 
                                        select p).ToList();
            if (prods.Any())
            {
                foreach (Product p in prods)
                {
                    Product pr = new Product
                    {
                        UserId = p.UserId,
                        ProductId = p.ProductId,
                        Name = p.Name,
                        Picture = p.Picture,
                        Price = p.Price,
                        Description = p.Description,
                        DateAdded = p.DateAdded,
                    };
                prodRec.Add(pr);

                }

            return prodRec;
            }
            return null;
            }

            public UserTable getAdmin(string uEmail)
            {
                
                var us = (from u in db.UserTables
                                where u.Email.Equals(uEmail)
                                select u).FirstOrDefault();
            if (us != null)
            {
                UserTable ur = new UserTable {
                    UserId = us.UserId,
                    Email = us.Email,
                    DateRegistered = us.DateRegistered,
                    UserType = us.UserType,
                    Enabled = us.Enabled,
            };
                return ur;
            }
            return null;
            }

            public Customer getCustomer(string uEmail)
            {
                UserTable inner = getAdmin(uEmail);
                
                var cs = (from cus in db.Customers
                          where cus.UserId == inner.UserId
                          select cus).FirstOrDefault();
            if (cs != null)
            {
                Customer c = new Customer {
                FirstName = cs.FirstName,
                LastName = cs.LastName,
                Birthdate = cs.Birthdate,
                GrantRecipient = cs.GrantRecipient,
                };
                return c;

            }
            return null;
        }

            public Store getStore(string uEmail)
            {
                UserTable inner = getAdmin(uEmail);
            
                var ss = (from str in db.Stores
                          where str.UserId == inner.UserId
                          select str).FirstOrDefault();
                if (ss != null)
                {
                Store s = new Store { 
                Company = ss.Company,
                Location = ss.Location,
                Logo = ss.Logo,
                Name = ss.Name,
                StoreType = ss.StoreType,
                };
                
                return s;
            }
            return null;
                
            }

            public List<Product> SearchProducts(string name)
            {
                List<Product> prodRec = new List<Product>();
                dynamic prods = (from p in db.Products
                                       where p.Name.ToLower().Contains(name.ToLower())
                                       select p).DefaultIfEmpty();
            if (prods.Any())
            {
                foreach (Product p in prods)
                {
                    Product pr = new Product
                    {
                        UserId = p.UserId,
                        ProductId = p.ProductId,
                        Name = p.Name,
                        Picture = p.Picture,
                        Price = p.Price,
                        Description = p.Description,
                        DateAdded = p.DateAdded,
                    };
                    prodRec.Add(pr);
                }
                return prodRec;
            }
            return null;
            }

        public Product GetProduct(int pID)
        {
            
            Product prods = (from p in db.Products
                                   where p.ProductId == (pID)
                                   select p).FirstOrDefault();
            if (prods != null)
            {
                Product pr = new Product
                {
                    UserId = prods.UserId,
                    ProductId = prods.ProductId,
                    Name = prods.Name,
                    Picture = prods.Picture,
                    Price = prods.Price,
                    Description = prods.Description,
                    DateAdded = prods.DateAdded,
                };

                return pr;
            }

            return null;
        }

        public bool AddProduct(int sID, string name, string desc, double price, string picPath, DateTime date, bool enabled)
            {
                bool checkP = (from p in db.Products
                                  where p.Name.Equals(name)
                                  select p).Any();
                if(!checkP)
                {
                    Product p = new Product
                    {
                        UserId = sID, 
                        Name = name, 
                        Description = desc, 
                        Picture = picPath, 
                        Price = (decimal)price, 
                        DateAdded = date, 
                        Enabled = enabled
                    };
                    try
                    {
                        db.Products.InsertOnSubmit(p);
                        db.SubmitChanges();
                        return true;
                    }
                    catch(Exception ex)
                    {
                        ex.GetBaseException();
                        return false;
                    }
                
                }
                return false;
            }

        public bool AddToCart(int pID, int uID, DateTime added, bool enabled)
        {
            bool checkP = (from p in db.Products
                           where p.ProductId.Equals(pID)
                           select p).Any();

            bool checkU = (from u in db.Products
                           where u.UserId.Equals(uID)
                           select u).Any();

            if (checkP && checkU)
            {
                CartItem cI = new CartItem
                {
                    UserId = uID,
                    ProductId = pID,
                    DateAdded = DateTime.Today,
                    Status = enabled
                };
                try
                {
                    db.CartItems.InsertOnSubmit(cI);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    ex.GetBaseException();
                    return false;
                }

            }
            return false;
        }

        public List<CartItem> GetCart()
        {
            List<CartItem> cartRec = new List<CartItem>();
            dynamic cart = (from c in db.CartItems
                                   select c).DefaultIfEmpty();
            
            foreach (CartItem c in cart)
            {
                Product pr = GetProduct(c.ProductId);

                UserTable user = (from u in db.UserTables
                                  where u.UserId.Equals(c.UserId)
                                  select u).FirstOrDefault();

                UserTable us = getAdmin(user.Email);

                CartItem cr = new CartItem
                {
                    UserId = user.UserId,
                    ProductId = pr.UserId,
                    DateAdded = c.DateAdded,
                    Status = c.Status

                };

                cartRec.Add(cr);
       
            }

            return cartRec;
        }
        
    }
    }

