using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team34_GP_IFM02B2_2023_WebApp.ResQReference;

namespace Team34_GP_IFM02B2_2023_WebApp
{
    
    public partial class index : System.Web.UI.Page
    {

        RESQSERVICEClient sc = new RESQSERVICEClient();

        protected void Page_Load(object sender, EventArgs e)
        {
         List<Store> st = new List<Store>(sc.getStores());
         List<Product> prod = new List<Product>(sc.getAllProducts());
         List<Tag> t = new List<Tag>(sc.getTags());
         Store[] feat = new Store[4];
         Product[] pFeat = new Product[7];

         for(int i = 0; i<4;i++)
            {
                bool inList = true;

                while (inList == true)
                {
                    Random r = new Random();
                    int ft = r.Next(0, st.Count);
                    Store temp = st[ft];
                    if (!feat.Contains(temp))
                    {
                        feat[i] = st[ft];
                        inList = false;
                    }
                }
            }

            for (int i = 0; i < 7; i++)
            {
                bool inList = true;

                while (inList == true)
                {
                    Random r = new Random();
                    int ft = r.Next(0, prod.Count);
                    Product temp = prod[ft];
                    if (!pFeat.Contains(temp))
                    {
                        pFeat[i] = temp;
                        inList = false;
                    }
                }
            }

            if (Request.QueryString["CartAdd"] != null)
            {
                if (Session["user"] != null)
                {
                    int PID = Convert.ToInt32(Request.QueryString["CartAdd"]);
                    UserTable tempUser = (UserTable)Session["user"];
                    CartItem c = new CartItem
                    {
                        UserId = tempUser.UserId,
                        ProductId = PID,
                        Quantity = 1
                    };

                    //if (Session["CartList"] != null)
                    //{
                        bool add = false;
                        List<CartItem> cList = (List<CartItem>)Session["CartList"];
                        for (int i = 0; i < cList.Count - 1; i++)
                        {
                            if ((cList[i].ProductId == c.ProductId) && (cList[i].UserId == c.UserId))
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
                        if (add)
                        {
                            cList.Add(c);
                        }

                        Session["CartList"] = cList;

                    //}
                }
            }

            String stBlock = "";
            String stCar = "";
            String pBlock = "";

            String tBlock = "";

            foreach (Tag temp in t)
            {
                tBlock += "<a href='shop.aspx?Filter=T&TID=" + temp.TagID + "' class='nav-item nav-link'>" + temp.TagName + "</a>";
            }
            pTags.InnerHtml = tBlock;

            foreach (Store s in feat)
            {
                stBlock+=  "<div class='col-lg-3 col-md- 4 col-sm-6 pb-1'>";
                stBlock+=  "<div class='product-item bg-light mb-4'>";
                 stBlock+= "<div class='product-img position-relative overflow-hidden' >";
                stBlock += "<a  href='shop.aspx?Filter=S&ID=" + s.UserId + "'><img class='img-fluid w-100' src='" + s.Logo+"' style='width:300px; height:300px; object-fit:cover;' alt=''>";
                stBlock += "</div>";
                stBlock+= "<div class='text-center py-4'>";
                stBlock+= "<a class='h6 text-decoration-none text-truncate' href='shop.aspx?Filter=S&ID="+s.UserId+"'>"+s.Name+"</a>";
                stBlock+= "<div class='d-flex align-items-center justify-content-center mt-2'>";
                stBlock+= " </div>";
                stBlock+= "</div>";
                stBlock+= " </div>";
                stBlock+= "</div>";
            }

            foreach(Store s in st)
            {
                stCar += "<div class='bg-light p-4'>";
                stCar += "<a href = 'shop.aspx?Filter=S&ID="+s.UserId+"' ><img src='"+s.Logo+"' alt=''></a>";
                stCar += "</div>";
            }

            foreach(Product p in pFeat)
            {
                pBlock+= "<div class='col-lg-3 col-md-4 col-sm-6 pb-1'>";
                pBlock+= "<div class='product-item bg-light mb-4'>";
                pBlock+= "<div class='product-img position-relative overflow-hidden'>";
                pBlock+= "<a  href='detail.aspx?ID="+p.ProductId+"'><img class='img-fluid w-100' src='"+p.Picture+ "' style='width: 300px; height: 300px; object-fit: cover;' alt=''></a>";
                if (Session["user"] != null)
                {
                    pBlock += "<div class='product-action'>";
                    pBlock += " <a class='btn btn-outline-dark btn-square' href='index.aspx?CartAdd=" + p.ProductId + "'><i class='fa fa-shopping-cart'></i></a>";                    pBlock += "</div>";
                }
                pBlock += "</div>";
                pBlock+= "<div class='text-center py-4'>";
                pBlock+= " <a class='h6 text-decoration-none text-truncate' href='detail.aspx?ID="+p.ProductId+"'>"+p.Name+"</a>";
                pBlock+= "<div class='d-flex align-items-center justify-content-center mt-2'>";
                pBlock+= "<h5>R"+p.Price+"</h5>";
                pBlock+= "</div>";                       
                pBlock+= "</div>";
                pBlock+= "</div>";
                pBlock+= "</div>";
            }

            pBlock += "<div class='col-lg-3 col-md-4 col-sm-6 pb-1'>";
            pBlock += "<a class='h6 text-decoration-none text-truncate' href='shop.aspx'><div class='product-item bg-light mb-4'>";
            pBlock += "<div class='text-center py-4'>";
            pBlock += " <a class='h6 text-decoration-none text-truncate' href='shop.aspx'><button class='btn btn-primary'>Shop All</button></a>";
            pBlock += "</div>";
            pBlock += "</div></a>";
            pBlock += "</div>";


            featStores.InnerHtml = stBlock;
            stCarousel.InnerHtml = stCar;
            featProd.InnerHtml = pBlock;



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