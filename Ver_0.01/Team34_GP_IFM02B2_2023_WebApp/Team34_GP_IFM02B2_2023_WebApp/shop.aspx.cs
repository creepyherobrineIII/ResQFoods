using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team34_GP_IFM02B2_2023_WebApp.ResQReference;

namespace Team34_GP_IFM02B2_2023_WebApp
{
    public partial class shop : System.Web.UI.Page
    {
        RESQSERVICEClient rc = new RESQSERVICEClient();
        protected void Page_Load(object sender, EventArgs e)
        {
           List<Store> st = new List<Store>(rc.getStores());
            List<Tag> t = new List<Tag>(rc.getTags());
            String name = "null";
            double upper = -1;
            double lower = -1;
            int man = -1;
            int cat = -1;
            String direct = "detail";


            String storeText = "";
            foreach(Store s in st)
            {
            storeText += "<div class='custom-control custom-checkbox d-flex align-items-center justify-content-between mb-3'>";
            storeText += "<a href = 'shop.aspx?Filter=S&ID="+s.UserId+"' ><label for='shop-1'>"+s.Name+"</label></a>";
            storeText += "</div>";
            }
            stBlock.InnerHtml = storeText;


            if (Request.QueryString["Edit"] != null)
            {
                if (Request.QueryString["Edit"].Equals("true"))
                {
                    if (Session["User"] != null)
                    {
                        UserTable tempUser = (UserTable)Session["User"];
                        if (tempUser.UserType == 2)
                        {
                            shopHead.Visible = false;
                            stBlock.Visible = false;
                            direct = "edit-product";
                        }
                    }
                }
            }

            if (Request.QueryString["CartAdd"] != null)
            {
                if (Session["User"] != null)
                {
                    int PID = Convert.ToInt32(Request.QueryString["CartAdd"]);
                    UserTable tempUser = (UserTable)Session["User"];
                    CartItem c = new CartItem
                    {
                        UserId = tempUser.UserId,
                        ProductId = PID,
                        Quantity = 1
                    };

                    if (Session["CartList"]!=null)
                    {
                        bool add = false;
                        List<CartItem> cList = (List<CartItem>)Session["CartList"];
                       for(int i = 0;i<cList.Count-1;i++)
                        {
                            if((cList[i].ProductId==c.ProductId)&&(cList[i].UserId==c.UserId))
                            {
                                add = false;
                                cList[i].Quantity += 1;
                                break;
                            }
                            else
                            {
                                add = true;
                            }
                        }
                        if(add)
                        {
                            cList.Add(c);
                        }

                        Session["CartList"] = cList;
                        
                    }
                }
            }

            if (Request.QueryString["WishAdd"] != null)
            {
                if (Session["User"] != null)
                {
                    int PID = Convert.ToInt32(Request.QueryString["WishAdd"]);
                    UserTable tempUser = (UserTable)Session["User"];
                    CartItem c = new CartItem
                    {
                        UserId = tempUser.UserId,
                        ProductId = PID,
                        Quantity = 1
                    };

                    if (Session["WishList"] != null)
                    {
                        bool add = false;
                        List<CartItem> wList = (List<CartItem>)Session["WishList"];
                        for (int i = 0; i < wList.Count - 1; i++)
                        {
                            if ((wList[i].ProductId == c.ProductId) && (wList[i].UserId == c.UserId))
                            {
                                add = false;
                                wList[i].Quantity += 1;
                                break;
                            }
                            else
                            {
                                add = true;
                            }
                        }
                        if (add)
                        {
                            wList.Add(c);
                        }

                        Session["CartList"] = wList;

                    }
                }
            }


            String tBlock = "";

            foreach(Tag temp in t)
            {
                tBlock += "<a href='shop.aspx?Filter=T&TID="+temp.TagID+"' class='nav-item nav-link'>"+temp.TagName+"</a>";
            }
            pTags.InnerHtml = tBlock;


            if (Request.QueryString["Filter"] != null)
            {
                String filt = Request.QueryString["Filter"];
                if (filt.Contains("N"))
                {
                    String n = Request.QueryString["Name"];
                    name = n;
                }
                else if (filt.Contains("S"))
                {
                    int SID = Convert.ToInt32(Request.QueryString["ID"]);
                    man = SID;
                }
                else if (filt.Contains("P"))
                {
                    double u = Convert.ToDouble(Request.QueryString["upper"]);
                    double l = Convert.ToDouble(Request.QueryString["lower"]);
                    upper = u;
                    lower = l;
                }
                else if (filt.Contains("T"))
                {
                    int TID = Convert.ToInt32(Request.QueryString["TID"]);
                    cat = TID;
                }
            }
                List<Product> products = new List<Product>(rc.getFilteredList(name, lower, upper, cat, man));
                if(Request.QueryString["Order"]!=null)
                {
                    List<Product> ordered = null;
                    String ot = Request.QueryString["Order"];
                    switch(ot)
                    {
                        case "cO":
                            ordered = (from p in products
                                       orderby p.DateAdded descending
                                       select p).ToList();
                            break;
                        case "cN":
                        ordered = (from p in products
                                   orderby p.DateAdded 
                                   select p).ToList();
                        break;
                        case "pL":
                        ordered = (from p in products
                                   orderby p.Price 
                                   select p).ToList();
                        break;
                        case "pH":
                            ordered = (from p in products
                                       orderby p.Price descending
                                       select p).ToList();
                        break;
                        case "nA":
                            ordered = (from p in products
                                       orderby p.Name
                                       select p).ToList();
                        break;
                        case "nZ":
                            ordered = (from p in products
                                       orderby p.Name descending
                                       select p).ToList();
                        break;
                    }
                    if (ordered != null)
                    {
                        products = ordered;
                    }
                    
                }
            String aOe = "";
            String pBlock = "";
            foreach(Product curr in products)
            {
                 pBlock += "<div class='col-lg-4 col-md-6 col-sm-6 pb-1'>";
                 pBlock += "<div class='product-item bg-light mb-4'>";        
                 pBlock += "<div class='product-img position-relative overflow-hidden'>";           
                 pBlock += "<img class='img-fluid w-100' src='"+curr.Picture+ "' style='width: 300px; height: 300px; object-fit:cover;' alt=''>";                
                 pBlock += "<div class='product-action'>";                 
                 pBlock += " <a class='btn btn-outline-dark btn-square' href='shop.aspx?CartAdd=" + curr.ProductId + "'><i class='fa fa-shopping-cart'></i></a>";                   
                 pBlock += " <a class='btn btn-outline-dark btn-square' href='shop.aspx?WishAdd=" + curr.ProductId + "'><i class='far fa-heart'></i></a>";                                     
                 pBlock += "</div>";                
                 pBlock += "</div>";           
                 pBlock += " <div class='text-center py-4'>";         
                 pBlock += "<a class='h6 text-decoration-none text-truncate' href='"+direct+".aspx?ID="+curr.ProductId+"'>"+curr.Name+"</a>";                
                 pBlock += "<div class='d-flex align-items-center justify-content-center mt-2'>";               
                 pBlock += "<h5>R"+curr.Price+"</h5><h6 class='text-muted ml-2'></h6>";                   
                 pBlock += "</div>";                
                 pBlock += "</div>";            
                 pBlock += "</div>";       
                 pBlock += "</div>";    
            }
            prodSpace.InnerHtml = pBlock;
            


        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Value != null)
            {
                String n = txtSearch.Value;
                Response.Redirect("shop.aspx?Filter=N&Name=" + n);
            }
        }
    }
}