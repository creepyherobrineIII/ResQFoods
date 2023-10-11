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
                        DateAdded = p.DateAdded
                    };
                prodRec.Add(pr);

                }
            return prodRec;
            }
            //Return null if no list found
            return null;
            }

        public List<Product> getFilteredList(String name, double P1, double P2, int filTag, int manu)
        {
            //Get list of all products
            List<Product> tempList = getAllProducts();
            //Filter if manufacturer!=-1
            if (manu != -1)
            {
                for (int i = tempList.Count - 1; i > -1; i--)
                {
                    //If the name does not match the searched name, remove the item from the list
                    if (tempList[i].UserId != manu)
                    {
                        tempList.RemoveAt(i);
                    }
                }
            }
            //Filter if name!=null
            if (name != "null")
            {
                for (int i = tempList.Count - 1; i > -1; i--)
                {
                    //If the name does not match the searched name, remove the item from the list
                    if (!tempList[i].Name.ToUpper().Contains(name.ToUpper()))
                    {
                        tempList.RemoveAt(i);
                    }
                }
            }

            if (P1 != -1 && P2 != -1)
            {
                for (int i = tempList.Count - 1; i > -1; i--)
                {
                    //Remove current value if it is greater or less than the bounds passed
                    if ((double)tempList[i].Price < P1 || (double)tempList[i].Price > P2)
                    {
                        tempList.RemoveAt(i);
                    }
                }
            }

            //In the remaining list
            if (filTag != -1)
            {
                for (int i = tempList.Count - 1; i > -1; i--)
                {
                    //Create current product instance
                    Product currProd = tempList[i];
                    //Search for all temp tags in bridging table ProductTags, where the product ID = the current product ID
                    bool cont = (from pt in db.ProductTags
                                 where pt.ProductId.Equals(currProd.ProductId)
                                 select pt).Any();
                    bool keep = false;
                    if (cont)
                    { 
                    dynamic tempTag = (from pt in db.ProductTags
                                       where pt.ProductId.Equals(currProd.ProductId)
                                       select pt).DefaultIfEmpty();
                    
                    //Create boolean keep, set to false
                    

                        if (tempTag != null)
                        {
                            List<ProductTag> tagList = new List<ProductTag>();
                            foreach (ProductTag t in tempTag)
                            {
                                if (t != null)
                                {
                                    ProductTag curr = new ProductTag
                                    {
                                        ProductId = t.ProductId,
                                        TagId = t.TagId
                                    };
                                    tagList.Add(curr);
                                }
                            }
                            //For each Prodcut tag in list
                            for (int x = 0; x < tagList.Count; x++)
                            {
                                //If they match one of the tags
                                if (tagList[x].TagId.Equals(filTag))
                                {
                                    //set keep to true
                                    keep = true;
                                }

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
            }
            //Return the list
            return tempList;
        }
            public UserTable getUser(string uEmail, int type)
            {
                //Get admin from usertable database
                var us = (from u in db.UserTables
                          where u.Email.Equals(uEmail) && u.Enabled == true && u.UserType == type
                          select u).FirstOrDefault();

                if (us != null)
                {
                    //Create usertable, return it
                    UserTable ur = new UserTable
                    {
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
            //Get admin from the userparent table, also used for getting userparent table values, as admin is a parent object
            public UserTable getAdmin(string uEmail)
            {
            //Get admin from usertable database
            UserTable user = getUser(uEmail, 0);
      
            if (user != null)
            {
                //Create usertable, return it
                return user;
            };
                
            return null;
            }

            //Get customer from customer table in database
            public Customer getCustomer(string uEmail)
            {
                UserTable inner = getUser(uEmail, 1);
                
                var cs = (from cus in db.Customers
                          where cus.UserId == inner.UserId
                          select cus).FirstOrDefault();
            if (cs != null)
            {
                //Create and return customer
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
            //Get store from storetable
            public Store getStore(string uEmail)
            {
                //Get sotres parent
                UserTable inner = getUser(uEmail, 2);
            
                var ss = (from str in db.Stores
                          where str.UserId == inner.UserId
                          select str).FirstOrDefault();
                if (ss != null)
                {
                //Create new store, return it
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
                        DateAdded = p.DateAdded
                    };
                    prodRec.Add(pr);
                }
                return prodRec;
            }
            return null;
            }

        public Product getProduct(int pID)
        {
            //Get product from the data base where ID matches the passed ID
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
                    Quantity = prods.Quantity,
                    NumSold = prods.NumSold
                    
                };

                return pr;
            }

            return null;
        }

        public bool AddProduct(int sID, string name, string desc, int quant, double price, string picPath, DateTime date, int tg, bool enabled)
            {
            //Check if product exists
                bool checkP = (from p in db.Products
                                  where p.Name.Equals(name)
                                  select p).Any();
                if(!checkP)
                {
                //If not, create new proiduct with passed values, insert into database
                    Product p = new Product
                    {
                        UserId = sID, 
                        Name = name, 
                        Description = desc, 
                        Picture = picPath, 
                        Price = (decimal)price, 
                        DateAdded = date, 
                        Enabled = enabled,
                        Quantity = quant,
                        NumSold = 0
                    };
                    try
                    {
                        db.Products.InsertOnSubmit(p);
                        db.SubmitChanges();

                    Product prd = (from prevProd in db.Products
                                    where prevProd.DateAdded.Equals(date)
                                    select prevProd).FirstOrDefault();

                    if(prd!=null)
                    {
                        if(AddProdTag(prd.ProductId, tg))
                        {
                            return true;
                        }
                    }
                
                    }
                    catch(Exception ex)
                    {
                        ex.GetBaseException();
                        return false;
                    }
                
                }
                return false;
            }

        public bool AddProdTag(int pID, int tID)
        {
            var pTag = (from pt in db.ProductTags
                        where pt.ProductId.Equals(pID)
                        select pt).FirstOrDefault();
            if(pTag==null)
            {
                ProductTag temp = new ProductTag
                {
                    ProductId = pID,
                    TagId = tID,
                };
                try
                {
                    db.ProductTags.InsertOnSubmit(temp);
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
                //If the cart item already exists
                var checkC = (from c in db.CartItems
                               where c.UserId.Equals(uID) && c.ProductId.Equals(pID)
                               select c).FirstOrDefault();

                if(checkC!=null)
                {
                    //Increase quantity of cart item if it exists, return true
                    checkC.Quantity += 1;
                    try
                    {
                        db.SubmitChanges();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        ex.GetBaseException();
                        return false;
                    }
                }
                //Else, create cart item, add to database
                CartItem cI = new CartItem
                {
                    UserId = uID,
                    ProductId = pID,
                    DateAdded = DateTime.Today,
                    Status = enabled,
                    Quantity = 1
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

        public List<CartItem> GetCart(int UID)
        {
            //Get all cart items from cart database associated with the userID
            List<CartItem> cartRec = new List<CartItem>();

            dynamic cart = (from c in db.CartItems
                            where c.UserId.Equals(c.UserId)
                            select c).DefaultIfEmpty();
            if (cart != null)
            {
                foreach (CartItem ic in cart)
                {

                    CartItem cr = new CartItem
                    {
                        UserId = ic.UserId,
                        ProductId = ic.UserId,
                        DateAdded = ic.DateAdded,
                        Status = ic.Status

                    };

                    cartRec.Add(cr);

                }

                return cartRec;
            }
            return null;
        }

        public bool editProduct(Product P, int pTag)
        {
            //Get product from the database 
            var prod = (from p in db.Products
                         where p.ProductId.Equals(P.ProductId)
                         select p).FirstOrDefault();

            if(prod!=null)
            {
                //Edit product attributes with passed product
                prod.ProductId = P.ProductId;
                prod.Picture = P.Picture;
                prod.DateAdded = P.DateAdded;
                prod.Description = P.Description;
                prod.Price = P.Price;
                prod.Quantity = P.Quantity;
                prod.Enabled = P.Enabled;
                prod.NumSold = P.NumSold;

                try
                {
                    //submit changes in db
                    db.SubmitChanges();
                    

                    if(pTag!=-1)
                    {
                        editProdTag(P.ProductId, pTag);
                    }
                    return true;
                }
                catch(SqlException ex)
                {
                    ex.GetBaseException();
                }
            }

            return false;
        }

        public bool decProduct(int prodID)
        {
            //Get product from the database 
            var prod = (from p in db.Products
                        where p.ProductId.Equals(prodID)
                        select p).FirstOrDefault();

            if (prod != null)
            {
                prod.Quantity -= 1;
                if (prod.Quantity <= 0)
                {
                    prod.Enabled = false; //disable to product
                }

                try
                {
                    //submit changes in db
                    db.SubmitChanges();

                    return true;
                }
                catch (SqlException ex)
                {
                    ex.GetBaseException();
                }
            }

            return false;
        }

        public bool editStore(Store S)
        {
            //Check if store and its parent exist
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
                user.Enabled = U.Enabled;

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
        private bool searchInvoiceUser(int UID)
        {
            bool inv = (from i in db.Invoices
                           where i.UserId.Equals(UID)
                           select i).Any();
            return inv;
        }
        public bool addInvoice(int UID, decimal price, DateTime TOS, List<int> prodIds, List<int> Quantities)
        {
            //Create invoice, 
            Invoice temp = new Invoice
            {
                UserId = UID,
                TotalPrice = price,
                DateAdded = TOS
            };
            try
            {
                //Add invoice to db
                db.Invoices.InsertOnSubmit(temp);
                db.SubmitChanges();
                
                //Get invoice just created
                var inv = (from i in db.Invoices
                           where i.UserId == UID && i.DateAdded.Equals(TOS)
                           select i).FirstOrDefault();
                if (inv != null)
                {
                    //Create invoice items for every cart item, add to invoice just added
                    for (int i = 0; i < prodIds.Count; i++)
                    {
                        addInvoiceItem(inv.InvoiceId, prodIds[i], Quantities[i]);

                    }
                }

                return true;

            }
            catch (SqlException ex)
            {
                ex.GetBaseException();
            }
            return false;

        }

        public bool addInvoiceItem(int ID, int ProdID, int Quantity)
        {
            //Get invoice from database
            var inv = (from i in db.Invoices
                       where i.InvoiceId == ID
                       select i).FirstOrDefault();
            if(inv!=null)
            {
                //Create a new invoice item associated with the invoice
                InvoiceItem ii = new InvoiceItem
                {
                    InvoiceId = inv.InvoiceId,
                    ProductId = ProdID,
                    Quantity = Quantity
                };
                //remove that number of rpoducts from the products table
                Product p = getProduct(ProdID);
                if(p!=null)
                {
                    p.Quantity = p.Quantity - Quantity;
                }
                editProduct(p, -1);
                try
                {
                    db.InvoiceItems.InsertOnSubmit(ii);
                    db.SubmitChanges();
                    return true;

                }
                catch (SqlException ex)
                {
                    ex.GetBaseException();
                }                 
            }
            return false;
        }

        public List<Invoice> getInvoices(int UID)
        {
            //Get all invoices for a client
            List <Invoice> tempList = new List<Invoice>();
            dynamic inv = (from i in db.Invoices
                           where i.UserId.Equals(UID)
                           select i).DefaultIfEmpty();
            if(inv!=null)
            {
                foreach(Invoice temp in inv)
                {
                    //Create invoice, add to list, return list
                    Invoice tempin = new Invoice
                    {
                        InvoiceId = temp.InvoiceId,
                        UserId = temp.UserId, 
                        TotalPrice = temp.TotalPrice
                    };

                    tempList.Add(tempin);
                }
            }
            return tempList;
        }

        public Invoice getInvoice(int IID)
        {
            //Get invoice from database, from passed database ID
            var temp = (from i in db.Invoices
                           where i.InvoiceId.Equals(IID)
                           select i).FirstOrDefault();
            
            if (temp != null)
            {
                    //Create invoice, return 
                    Invoice tempIn = new Invoice
                    {
                        InvoiceId = temp.InvoiceId,
                        UserId = temp.UserId,
                        TotalPrice = temp.TotalPrice
                    };
                return tempIn;
            }
            return null;
        }

        public List<InvoiceItem> getInvoiceItems(int IID)
        {
            //Create list of invoice items
            List<InvoiceItem> tempList = new List<InvoiceItem>();
            //Get all invoice items associated with the passed invoice ID
            dynamic temp = (from i in db.InvoiceItems
                        where i.InvoiceId.Equals(IID)
                        select i).DefaultIfEmpty();

            if(temp!=null)
            {
                //Populate the invoice item list with created invoice items, return the list
                foreach (InvoiceItem ii in temp)
                {
                    InvoiceItem tempI = new InvoiceItem
                    {
                        InvoiceId = ii.InvoiceId,
                        ProductId = ii.ProductId,
                        Quantity = ii.Quantity
                    };
                    tempList.Add(tempI);
                }
            }
            return tempList;

        }

        public decimal GetCartTotal(int UID)
        {
            dynamic cart = (from c in db.CartItems
                            where c.UserId.Equals(c.UserId)
                            select c).DefaultIfEmpty();

            decimal total = 0;

            if (cart != null)
            {
                foreach (CartItem c in cart)
                {

                    Product prod = new Product
                    {
                        ProductId = c.Product.ProductId,
                        Picture = c.Product.Picture,
                        DateAdded = c.Product.DateAdded,
                        Description = c.Product.Description,
                        Price = c.Product.Price,
                        ProductTags = c.Product.ProductTags,
                        Store = c.Product.Store

                    };

                    total += prod.Price;
                }

                return total;
            }
                                
            return 0;
        }

        public List<Store> getStores()
        {
            List<Store> stList = new List<Store>();
            dynamic user = (from u in db.UserTables
                          where u.Enabled == true && u.UserType==2
                          select u).DefaultIfEmpty();
            if (user != null)
            {
                foreach (UserTable temp in user)
                {
                    var st = (from s in db.Stores
                              where s.UserId.Equals(temp.UserId)
                              select s).FirstOrDefault();
                    if (st != null)
                    {
                        Store stor = new Store
                        {
                            UserId = st.UserId,
                            Name = st.Name,
                            Logo = st.Logo
                        };
                        stList.Add(stor);
                    }
                }
            }
            return stList;
        }

        public List<Tag> getTags()
        {
            List<Tag> tReturn = new List<Tag>();
            dynamic tg = (from t in db.Tags
                          select t).DefaultIfEmpty();
            if (tg != null)
            {
                foreach (Tag temp in tg)
                {
                    Tag tempTag = new Tag
                    {
                        TagID = temp.TagID, 
                        TagName = temp.TagName
                    };
                    tReturn.Add(tempTag);
                }
            }
            return tReturn;
        }

        

        public int getProdTag(int pID)
        {
            var pTag = (from pt in db.ProductTags
                        where pt.ProductId.Equals(pID)
                        select pt).FirstOrDefault();
            return pTag.TagId;
        }

        public String getTagName(int tID)
        {
            var pTag = (from pt in db.Tags
                        where pt.TagID.Equals(tID)
                        select pt).FirstOrDefault();
            if(pTag!=null)
            {
                return pTag.TagName;
            }
            return "null";
        }

        public bool editProdTag(int pID, int tID)
        {
            var pTag = (from pt in db.ProductTags
                        where pt.ProductId.Equals(pID) && pt.TagId.Equals(tID)
                        select pt).FirstOrDefault();
            if(pTag!=null)
            {
                pTag.TagId = tID;
                db.SubmitChanges();
                return true;
            }
            return false;
        }

        public int searchTag(string tagVal)
        {
            var pTag = (from t in db.Tags
                        where t.TagName.ToUpper().Equals(tagVal.ToUpper())
                        select t).FirstOrDefault();
            if(pTag!=null)
            {
                return pTag.TagID;
            }
            return -1;
        }

        public decimal getReportTotalSales()
        {
            dynamic invoice = (from i in db.Invoices
                               select i);


            //calculate total sales
            decimal totalSales = 0;
            foreach (Invoice inv in invoice)
            {
                totalSales += inv.TotalPrice;
            }

            return totalSales;
        }

        public Store getBestSellingStore()
        {
            dynamic store = (from s in db.Stores
                               select s);

            decimal tempHighesttotal = 0;

            Store temphighest = new Store();

            //add all products to list
            foreach (Store s in store)
            {
                decimal storeTotal = 0;

                dynamic products = (from p in db.Products where p.UserId == s.UserId select p); //get all store products

                //Calculate product total
                foreach(Product pr in products)
                {
                   storeTotal += (pr.Price * pr.NumSold); //get total store profit
                }

                if (tempHighesttotal < storeTotal)
                {
                    tempHighesttotal = storeTotal;
                    temphighest = s;

                }
            }

            Store highest = new Store()
            {
                UserId = temphighest.UserId,
                Company = temphighest.Company,
                Name = temphighest.Name,
                Logo = temphighest.Logo,
                Location = temphighest.Location,
                StoreType = temphighest.StoreType
            };

            return highest;
        }

        public Store getBestSellingStoreFromType(String Type)
        {
            dynamic store = (from s in db.Stores where s.StoreType.Equals(Type)
                             select s);

            decimal tempHighesttotal = 0;

            Store temphighest = new Store();

            //add all products to list
            foreach (Store s in store)
            {
                decimal storeTotal = 0;

                dynamic products = (from p in db.Products where p.UserId == s.UserId select p); //get all store products

                //Calculate product total
                foreach (Product pr in products)
                {
                    storeTotal += (pr.Price * pr.NumSold); //get total store profit
                }

                if (tempHighesttotal < storeTotal)
                {
                    tempHighesttotal = storeTotal;
                    temphighest = s;

                }
            }

            Store highest = new Store()
            {
                UserId = temphighest.UserId,
                Company = temphighest.Company,
                Name = temphighest.Name,
                Logo = temphighest.Logo,
                Location = temphighest.Location,
                StoreType = temphighest.StoreType
            };

            return highest;
        }

        public String getBestSellingStoreType()
        {
            Store s = getBestSellingStore();

            return s.StoreType;
        }

        public Tag getBestSellingProductTag()
        {
            dynamic tags = (from t in db.Tags select t);

            Tag returntag = new Tag();

            int tempHighestAmount = 0;
            
            foreach(Tag tag in tags)
            {
                dynamic products = (from pt in db.ProductTags
                                   where pt.TagId.Equals(tag.TagID)
                                   select pt.Product).DefaultIfEmpty();
                int totalProductsSoldWithTag = 0;
                foreach (Product pr in products)
                {
                    totalProductsSoldWithTag += pr.NumSold; 
                }

                if (tempHighestAmount< totalProductsSoldWithTag)
                {
                    tempHighestAmount = totalProductsSoldWithTag;
                    returntag = tag;

                }


            }

            return returntag;
        }

        public List<Product> getProductStock(int StoreID)
        {
            dynamic product = (from p in db.Products
                               where p.Enabled.Equals(true) && p.UserId.Equals(StoreID)
                               select p);

            if (product == null) 
            return null;

            List<Product> productreturn = new List<Product>();

            //add all products to list
            foreach (Product p in product)
            {
                Product sentproduct = new Product()
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Description = null, //dont need to have decription to admin
                    Quantity = p.Quantity,
                    NumSold = p.NumSold,
                    DateAdded = p.DateAdded,
                    Price = p.Price
                    
                };
                productreturn.Add(sentproduct);
            }

            return productreturn;
        }

        public int getNumRegUsers(DateTime date) //recieve date from admin to find specific users on that date
        {
            //gets invoice from DB
            dynamic Users = (from u in db.UserTables
                             where u.DateRegistered.Equals(date)
                             select u);


            //calculate total profit
            int totalRegistered = 0;

            foreach (UserTable u in Users)
            {
                totalRegistered++;
            }

            return totalRegistered;
        }

        public Tag getBestCategory() //Code from Richard
        {
            //Declare lists for products, and their totals
            List<int> prodSold = new List<int>();
            List<int> noSold = new List<int>();

            //Delcare list for tags and their totals
            List<Tag> tgs = getTags();
            List<int> noSoldTag = new List<int>();
            Tag t = null;

            //get all invoices from the invoice table
            dynamic inv = (from i in db.InvoiceItems
                           select i).DefaultIfEmpty();

            //Set count for items sold per tag to 0
            for (int x = 0; x < tgs.Count; x++)
            {
                noSoldTag.Add(0);
            }

            if (inv != null)
            {
                //For each invoice in the db
                foreach (Invoice curr in inv)
                {
                    //Get all lines associated with the invoice
                    List<InvoiceItem> iList = getInvoiceItems(curr.InvoiceId);
                    //For each item
                    foreach (InvoiceItem temp in iList)
                    {
                        //Get the product id of the invoice entry
                        int pId = temp.ProductId;
                        if (!prodSold.Contains(pId))
                        {
                            //If the product ID is not in the product sold array
                            //Add the product to the prodSold array
                            prodSold.Add(pId);
                            //Add the quantity to the corresponding index
                            noSold.Add(temp.Quantity);
                        }
                        else
                        {
                            //Get the index of the productID
                            int index = prodSold.IndexOf(pId);
                            //Add the quntity sold of the product to its total
                            noSold[index] += temp.Quantity;
                        }

                    }
                }


                //For each product that has ever been sold
                for (int i = 0; i < prodSold.Count; i++)
                {
                    //Get its tags
                    bool hasTag = (from prd in db.ProductTags
                                   where prd.ProductId.Equals(prodSold[i])
                                   select prd).Any();
                    if (hasTag)
                    {
                        //Get a list of its tags if it has any
                        dynamic productTag = (from prd in db.ProductTags
                                              where prd.ProductId.Equals(prodSold[i])
                                              select prd).DefaultIfEmpty();
                        //For each of its tags
                        foreach (ProductTag tempP in productTag)
                        {
                            //Loop through the tags array
                            foreach (Tag tempTag in tgs)
                            {
                                //If the product has a tag
                                if (tempP.TagId.Equals(tempTag.TagID))
                                {
                                    //Find the products tags index in the tags array
                                    int index = tgs.IndexOf(tempTag);
                                    //Add the number sold of that item to the tag count 
                                    noSoldTag[index] += noSold[i];
                                }
                            }
                        }
                    }
                }



                //Get the value of highest tag sales
                int highSale = noSoldTag.Max();
                //Get the index of that high value
                int indexOfSale = noSoldTag.IndexOf(highSale);
                //Get the tag at that index in the tgs table
                Tag tagToReturn = tgs[indexOfSale];

                t = tagToReturn;
            }
            return t; 
        }

        public decimal getReportTotalSalesBusiness(Store s)
        {

            dynamic items = (from i in db.InvoiceItems select i);


            //calculate total sales
            decimal totalSales = 0;
            foreach (InvoiceItem inv in items)
            {
               if(inv.Product.UserId == s.UserId)
                {
                    totalSales += (inv.Product.Price * inv.Product.NumSold);
                }
            }

            return totalSales;
        }
    }
    }

