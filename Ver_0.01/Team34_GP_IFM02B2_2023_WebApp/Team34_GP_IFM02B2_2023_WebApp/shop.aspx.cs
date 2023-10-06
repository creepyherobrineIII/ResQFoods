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
            storeText += "<a href = 'shop.aspx?Filter=S&ID="+s.UserId+"' >< label class='custom-control-label' for='shop-1'>"+s.Name+"</label></a>";
            storeText += "</div>";
            }
            stBlock.InnerHtml = storeText;

            if (Request.QueryString["Edit"] != null)
            {
                Request.QueryString["Edit"].Equals("true");
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
                    int SID = Convert.ToInt32(Request.QueryString["SID"]);
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
                List<Product> products = new List<Product>(rc.getFilteredList(name, upper, lower, cat, man));
                if(Request.QueryString["Order"]!=null)
                {
                    List<Product> ordered = null;
                    String ot = Request.QueryString["Order"];
                    switch(ot)
                    {
                        case "cO":
                            ordered = (List<Product>)products.OrderBy(prod=>prod.DateAdded);
                            break;
                        case "cN":
                            ordered = (List<Product>)products.OrderByDescending(prod => prod.DateAdded);
                            break;
                        case "pL":
                            ordered = (List<Product>)products.OrderBy(prod => prod.Price);
                            break;
                        case "pH":
                            ordered = (List<Product>)products.OrderByDescending(prod => prod.Price);
                            break;
                        case "nA":
                            ordered = (List<Product>)products.OrderBy(prod => prod.Name);
                            break;
                        case "nZ":
                            ordered = (List<Product>)products.OrderByDescending(prod => prod.Name);
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
                 pBlock += "<img class='img-fluid w-100' src='"+curr.Picture+"' alt=''>";                
                 pBlock += "<div class='product-action'>";                 
                 pBlock += " <a class='btn btn-outline-dark btn-square' href='shop.aspx?CartAdd=" + curr.ProductId + "'><i class='fa fa-shopping-cart'></i></a>";                   
                 pBlock += " <a class='btn btn-outline-dark btn-square' href='shop.aspx?WishAdd=" + curr.ProductId + "'><i class='far fa-heart'></i></a>";                                     
                 pBlock += "</div>";                
                 pBlock += "</div>";           
                 pBlock += " <div class='text-center py-4'>";         
                 pBlock += "<a class='h6 text-decoration-none text-truncate' href='"+direct+".aspx?ID="+curr.ProductId+"'>Rustic Potatoes Bag(7kg)</a>";                
                 pBlock += "<div class='d-flex align-items-center justify-content-center mt-2'>";               
                 pBlock += "<h5>R30.00</h5><h6 class='text-muted ml-2'><del>R50.00</del></h6>";                   
                 pBlock += "</div>";                
                 pBlock += "</div>";            
                 pBlock += "</div>";       
                 pBlock += "</div>";    
            }
            prodSpace.InnerHtml = pBlock;
            
            

            

            

        }
    }
}