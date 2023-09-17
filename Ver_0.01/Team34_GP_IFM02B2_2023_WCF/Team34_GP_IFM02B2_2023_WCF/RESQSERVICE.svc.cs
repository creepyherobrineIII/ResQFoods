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


        //*************************GET IMPLEMENTATION************************
        public string GetData(dynamic value)//Changed from Int to Dynamic for more versatility 
        {
            return string.Format("You entered: {0}", value);
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
            
            List<Product> prodReturn = new List<Product>();
            List<Product> prods = (from p in db.Products where p.Enabled == true
                                   select p).ToList();
            if (prods.Any())
            {
                foreach (Product p in prods)
                {
                    Product pr = new Product();
                    pr.UserId = p.UserId;
                    pr.ProductId = p.ProductId;
                    pr.Name = p.Name;
                    pr.Picture = p.Picture;
                    pr.Price = (decimal)(double)p.Price;
                    pr.Description = p.Description;
                    pr.DateAdded = p.DateAdded;
                    prodReturn.Add(pr);
                }

                return prodReturn;
            }
            return null;
        }

        public UserTable getUser(string uEmail)
        {
            UserTable uReturn = null; 

            var uQuery = (from u in db.UserTables
                             where u.Email == uEmail && u.Enabled == true
                             select u).FirstOrDefault();
           
            if( uQuery != null)
            {
                uReturn = new UserTable
                {
                    UserId = uQuery.UserId,
                    UserType = uQuery.UserType,
                    Email = uQuery.Email,
                    HashedPassword = uQuery.HashedPassword,
                    DateRegistered = uQuery.DateRegistered,
                    Enabled = true,
                };

                return uReturn;

            }
            return null;
        }

        public UserTable getAdmin(string uEmail)
        {
            UserTable aReturn = null;

            //Make sure email is equal, that the usertype is an admin, and it is still active. 
            var aQuery = (from a in db.UserTables
                              where a.Email.Equals(uEmail) && a.UserType == 2 && a.Enabled == true
                              select a).FirstOrDefault();
           
            if (aQuery != null)
            {
                aReturn = new UserTable
                {
                    UserId = aQuery.UserId,
                    UserType = aQuery.UserType,
                    Email = aQuery.Email,
                    HashedPassword = aQuery.HashedPassword,
                    DateRegistered = aQuery.DateRegistered,
                    Enabled = true,
                };
               
                return aReturn;
            }
            return null;
        }

        public Customer getCustomer(string uEmail)
        {
            Customer cReturn = null;

            var cQuery = (from c in db.Customers
                          where c.UserTable.Email == uEmail  && c.UserTable.Enabled == true                                //CHECK IF WORKS!!!
                          select c).FirstOrDefault();
            
            if (cQuery != null)
            {
                cReturn = new Customer
                {
                    UserId = cQuery.UserId,
                    FirstName = cQuery.FirstName,
                    LastName = cQuery.LastName,
                    Birthdate = cQuery.Birthdate,
                    GrantRecipient = cQuery.GrantRecipient,
                };

                return cReturn;
            }

            return null;
        }

        public Store getStore(string uEmail)
        {
            Store sReturn = null;

            var sQuery = (from s in db.Stores
                          where s.UserTable.Email == uEmail && s.UserTable.Enabled == true                                //CHECK IF WORKS!!!
                          select s).FirstOrDefault();

            if (sQuery != null)
            {
                sReturn = new Store
                {
                    UserId = sQuery.UserId,
                    Company = sQuery.Company,
                    Name = sQuery.Name,
                    Logo = sQuery.Logo,
                    Location = sQuery.Location,
                    StoreType = sQuery.StoreType,
                };

                return sReturn;
            }

            return null;
        }


        public Product getProduct(int pId)
        {

            Product prodGet = (from p in db.Products
                             where p.ProductId == (pId) && p.Enabled == true
                             select p).FirstOrDefault();
            if (prodGet != null)
            {
                Product productReturn = new Product();

                productReturn.UserId = prodGet.UserId;
                productReturn.ProductId = prodGet.ProductId;
                productReturn.Name = prodGet.Name;
                productReturn.Picture = prodGet.Picture;
                productReturn.Price =  (decimal)(double)prodGet.Price;
                productReturn.Description = prodGet.Description;
                productReturn.DateAdded = prodGet.DateAdded;

                return productReturn;
            }

            return null;
        }

        public List<CartItem> getCartItems(int uId)
        {
            List<CartItem> returnList = new List<CartItem>();
            List<CartItem> cartQuery = (from c in db.CartItems
                                        where c.UserId == uId && c.Status == true //Select only cart items of user and that are enabled
                                        select c).ToList();

            foreach (CartItem c in cartQuery)
            {
                CartItem cr = new CartItem
                {
                    ProductId = c.ProductId,
                    CartId = c.CartId,
                    UserId = c.UserId,
                    DateAdded = c.DateAdded,
                    Status = c.Status,

                };       
                returnList.Add(cr);
            }

            return returnList;
        }

        public List<Product> getCartProducts(int uId)
        {
            List<Product> returnList = new List<Product>();
            List<CartItem> cartList = getCartItems(uId);

            foreach (CartItem c in cartList)
            {
                Product pAdd = getProduct(c.ProductId); //get the product id with cart item, and then search it with getProduct function
                if(pAdd != null)
                {
                    returnList.Add(pAdd);
                }
            }

            return returnList;
        }

        //Have an empty and populated-via-search product list and then add only the necessary info to 
        public List<Product> searchProduct(string name)
        {
            List<Product> prodSend = new List<Product>();
            List<Product> prodSearch = (from p in db.Products
                                        where p.Name.ToLower().Contains(name.ToLower()) && p.Enabled == true
                                        select p).ToList();
            if (prodSearch.Any())
            {
                foreach (Product p in prodSearch)
                {
                    Product pr = new Product();
                    pr.UserId = p.UserId;
                    pr.ProductId = p.ProductId;
                    pr.Name = p.Name;
                    pr.Picture = p.Picture;
                    pr.Price = (decimal)(double)p.Price;
                    pr.Description = p.Description;
                    pr.DateAdded = p.DateAdded;

                    prodSend.Add(pr);
                }
                return prodSend;

            }

            return null;

        }



        //**********************UPDATE IMPLEMENTATION***************************
        public string updateUser(String uEmail)
        {
            throw new NotImplementedException();
        }

        public bool updateCart(int CartID)
        {
            throw new NotImplementedException();
        }
        


        //*********************ADD IMPLEMENTATION************************
        public bool addProduct(int sID, string name, string desc, double price, string picPath, DateTime date, bool enabled)
        {
            throw new NotImplementedException();
        }

        public bool addToCart(int pID, int uID, DateTime added, bool enabled)
        {
            throw new NotImplementedException();
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

        public bool AddProduct(int sID, string name, string desc, double price, string picPath, DateTime date, bool enabled)
        {
            bool checkP = (from p in db.Products
                           where p.Name.Equals(name)
                           select p).Any();
            if (!checkP)
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
                catch (Exception ex)
                {
                    ex.GetBaseException();
                    return false;
                }

            }
            return false;
        }
        public bool addToCart(int pID, int uID, DateTime added)
        {
            throw new NotImplementedException();
        }




        //***********************LOGIN IMPLEMENTATION******************************************
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


            bool exists = (from u in db.UserTables
                           where u.Email.Equals(email)
                           select u).Any();
            return exists;


        }

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

            if (checkUserValid(user))
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
          
       
    }
    }

