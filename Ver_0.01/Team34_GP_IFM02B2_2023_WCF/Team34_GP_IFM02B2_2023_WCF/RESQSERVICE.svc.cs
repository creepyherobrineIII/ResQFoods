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

            public List<UserTable> GetEmployeeRecords(String uEmail)
            {
                throw new NotImplementedException();
            }
            //This mehthod checks if a user exists in the usertables database
            private bool checkUserValid(String email)
            {

            bool exists = (from u in db.UserTables
                         where u.Email.Equals(email)
                         select u).Any();
            //Return whether or not user exists (search done by email)
            return exists;
            }

            //Look for user record type, return that type
            //Make 3 methods, one for each type of user
            public int loginUser(String user, String pass)
            {
            //If the users exists
            if(checkUserValid(user))
            {
                //Search for user where email and hashed password match
                var userT = (from u in db.UserTables
                              where u.Email.Equals(user) &&
                              u.HashedPassword.Equals(pass)
                              select u).FirstOrDefault(); 
                //Return users type
                int t = userT.UserType;
                return t;
            }
            //If user does not exist, return -1
            return -1;
            }
            
            //This method registers a user of into the parent table
            private bool regUser(String uEmail, String uPass, int type)
            {
            //If the user does NOT exist
            if (!checkUserValid(uEmail))
            {
                //Create new User, initialize with the passed values
                UserTable a = new UserTable
                {
                    Email = uEmail,
                    UserType = type,
                    HashedPassword = uPass,
                    DateRegistered = System.DateTime.Today,
                    Enabled = true
                };
                //Insert user into database
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
            //If user exists, return false
            return false;
        }
            //Method to register a sysAdmin
            public bool regAdmin(String uEmail, String uPass)
            {
            //If the admin is added, return true
            if(regUser(uEmail, uPass, 0))
            {
                return true;
            }
            return false;
            }

            public bool regCust(String uEmail, String uPass, String fName, String lName, DateTime BDate, bool grant)
            {
            //If the customers parent has been added to the database
            if (regUser(uEmail, uPass, 1))
            {
                //Get user that was just added to the database
                var check = (from u in db.UserTables
                             where u.Email.Equals(uEmail)
                             select u).FirstOrDefault();
                        
                        //If the user has been successfully retrieved
                        if (check != null)
                        {
                            //Create new customer, using the parent ID, and passed values
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
                            //Submit new user
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
            //If parent not added, return false
            return false;
        }

            //Method to register a store
            public bool regStore(String uEmail, String uPass, String comp, String name, String icoPath, String loc, String type)
            {
            //If the parent of the store has been added
            if (regUser(uEmail, uPass, 2))
            {
                //Get the parent from the database
                var check = (from u in db.UserTables
                             where u.Email.Equals(uEmail)
                             select u).FirstOrDefault();

                if (check != null)
                {
                    //If the parent exists, create a store, using the parents ID
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
                        //Insert store into database
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
            //If parent was not added, return false
            return false;

            }
            
            //Method to return all products from database
            public List<Product> getAllProducts()
            {
                //Create new lists, one for display, one for the data from the database
                List<Product> prodRec = new List<Product>();
                List<Product> prods = (from p in db.Products
                                       where p.Enabled == true
                                        select p).ToList();
            if (prods.Any())
            {
                //Create a new product, returning only what will be displayed, add this to the list that will be returned
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
                        Enabled = p.Enabled,
                    };
                prodRec.Add(pr);

                }
            return prodRec;
            }
            //Return null if no list found
            return null;
            }

            public List<Product> getFilteredList(String name, double P1, double P2, List<int> tags)
            {
            //Get list of all products
            List<Product> tempList = getAllProducts();
            //Filter if name not = null
            if (name != "null")
            {
                for (int i = tempList.Count - 1; i > -1; i--)
                {
                    //If the name does not match the searched name, remove the item from the list
                    if (tempList[i].Name != name)
                    {
                        tempList.RemoveAt(i);
                    }
                }
            }

            for (int i = tempList.Count - 1; i > -1; i--)
            {
                //Remove current value if it is greater or less than the bounds passed
                if ((double)tempList[i].Price < P1 || (double)tempList[i].Price > P2)
                {
                    tempList.RemoveAt(i);
                }
            }
            
            //In the remaining list
            for (int i = tempList.Count - 1; i > -1; i--)
            {
                //Create current product instance
                Product currProd = tempList[i];
                //Search for all temp tags in bridging table ProductTags, where the product ID = the current product ID
                dynamic tempTag = (from pt in db.ProductTags
                                where pt.ProductId.Equals(currProd.ProductId)
                                select pt).DefaultIfEmpty();
                //Create boolean keep, set to false
                bool keep = false;

                //For each Prodcut tag in list
                foreach(Tag t in tempTag)
                {
                    //Loop through tag list passed, and check if they match
                    foreach(int val in tags)
                    {
                        //If they match one of the tags
                        if(val == t.TagID)
                        {
                            //set keep to true
                            keep = true;
                        }
                    }
                }
                //If the product does not have the tag
                if (!keep)
                {
                    //remove the product from the list
                    tempList.RemoveAt(i);
                }
            }
            //Return the list
            return tempList;
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
            if (prods!=null)
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
            if (cart != null)
            {
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
            return null;
        }

        public bool editProduct(Product P)
        {
            var prod = (from p in db.Products
                         where p.ProductId.Equals(P.ProductId)
                         select p).FirstOrDefault();

            if(prod!=null)
            {
                prod.ProductId = P.ProductId;
                prod.Picture = P.Picture;
                prod.DateAdded = P.DateAdded;
                prod.Description = P.Description;
                prod.Price = P.Price;
                prod.ProductTags = P.ProductTags;
                prod.Store = P.Store;

                try
                {
                    db.SubmitChanges();
                    return true;
                }
                catch(SqlException ex)
                {
                    ex.GetBaseException();
                }
            }

            return false;
        }

        public bool editStore(Store S)
        {

            var user = (from u in db.UserTables
                        where u.UserId.Equals(S.UserId)
                        select u).FirstOrDefault();

            var store = (from s in db.Stores
                        where s.UserId.Equals(S.UserId)
                        select s).FirstOrDefault();

            if (user!=null && store != null)
            {
                if (editUser(getAdmin(user.Email)))
                    {
                    store.UserId = S.UserId;
                    store.Logo = S.Logo;
                    store.Name = S.Name;
                    store.Location = S.Location;
                    store.StoreType = S.StoreType;

                    try
                    {
                        db.SubmitChanges();
                        return true;
                    }
                    catch (SqlException ex)
                    {
                        ex.GetBaseException();
                    }
                }
            }

            return false;
        }

        public bool editCustomer(Customer C)
        {

            var user = (from u in db.UserTables
                        where u.UserId.Equals(C.UserId)
                        select u).FirstOrDefault();

            var cust = (from c in db.Customers
                         where c.UserId.Equals(C.UserId)
                         select c).FirstOrDefault();

            if (user != null && cust != null)
            {
                if (editUser(getAdmin(user.Email)))
                {
                    cust.UserId = C.UserId;
                    cust.FirstName = C.FirstName;
                    cust.LastName = C.LastName;
                    cust.GrantRecipient = C.GrantRecipient;

                    try
                    {
                        db.SubmitChanges();
                        return true;
                    }
                    catch (SqlException ex)
                    {
                        ex.GetBaseException();
                    }
                }
            }

            return false;
        }

        public bool editUser(UserTable U)
        {
            var user = (from u in db.UserTables
                         where u.UserId.Equals(U.UserId)
                         select u).FirstOrDefault();

            if (user != null)
            {
                user.UserId = U.UserId;
                user.Email = U.Email;
                user.HashedPassword = U.HashedPassword;

                try
                {
                    db.SubmitChanges();
                    return true;
                }
                catch (SqlException ex)
                {
                    ex.GetBaseException();
                }
            };

            return false;
        }

        /*public List<Product> getProductsFiltered(double Price1, double Price2, int SID, String cat) 
        {
            decimal temp1 = (decimal)Price1;
            decimal temp2 = (decimal)Price2;
            List<Product> returnList = new List<Product>();
            dynamic prodList = (from p in db.Products
                                where p.Price > temp1 && p.Price < temp2 && p.UserId == SID && p.ProductTags.Equals(cat)
                                select p).DefaultIfEmpty();
            if (prodList != null)
            {
                foreach (Product temp in prodList)
                {
                    Product newP = new Product
                    {
                        ProductId = temp.ProductId,
                        Name = temp.Name,
                        Picture = temp.Picture,
                        Price = temp.Price,
                    };
                    returnList.Add(newP);

                }
                return returnList;
            }
            return null;
        }*/

        /*public List<Product> searchFiltered(double Price1, double Price2, int SID, String cat)
        {
            decimal temp1 = (decimal)Price1;
            decimal temp2 = (decimal)Price2;
            List<Product> returnList = new List<Product>();
            dynamic prodList = (from p in db.Products
                                where p.Price > temp1 && p.Price < temp2 && p.UserId == SID && p.ProductTags.con(cat)
                                select p).DefaultIfEmpty();
            if (prodList != null)
            {
                foreach (Product temp in prodList)
                {
                    Product newP = new Product
                    {
                        ProductId = temp.ProductId,
                        Name = temp.Name,
                        Picture = temp.Picture,
                        Price = temp.Price,
                    };
                    returnList.Add(newP);

                }
                return returnList;
            }
            return null;
        }*/

        public string DeleteUser(string uEmail)
        {
            throw new NotImplementedException();
        }
    }
    }

