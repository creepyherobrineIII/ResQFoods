using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team34_GP_IFM02B2_2023_WebApp.ResQReference;

namespace Team34_GP_IFM02B2_2023_WebApp
{
    public partial class detail : System.Web.UI.Page
    {
        RESQSERVICEClient rc = new RESQSERVICEClient();
        protected void Page_Load(object sender, EventArgs e) //Display product from URL Parameters
        {
            Product dispProd = null;

            if (Request.QueryString["ID"] != null) //Determine whether URL Parameters exist
            {
                int prodID = Convert.ToInt32(Request.QueryString["ID"]);

                dispProd = rc.getProduct(prodID);

                string prodImage = "<img class='mw-100 mh-100 px-auto' src=" + dispProd.Picture + " style='width:480px; height:400px; object-fit:cover;' alt='Image'>";

                //Display product
                prodImage1.InnerHtml = prodImage;
                prodHead.InnerText = dispProd.Name;
                prodShortDesc.InnerText = dispProd.Description.Substring(0, 20) + "..."; //Short Description of product
                prodFullDesc.InnerText = dispProd.Description; //Full Description
                prodPrice.InnerText = "R" + dispProd.Price.ToString();
            }

        }

        protected void addToCart_Click(object sender, EventArgs e) //On Click event for button
        {

            if (Session["User"] != null)
            {
                UserTable tempUser = (UserTable)Session["User"]; //Find out user details
                int prodID = int.Parse(Request.QueryString["ID"]);

                CartItem c = new CartItem
                {
                    UserId = tempUser.UserId,
                    ProductId = prodID
                };

                if (Session["CartList"] != null)
                {
                    List<CartItem> cList = (List<CartItem>)Session["CartList"];

                    if (cList.Contains(c))
                    {
                        int i = cList.IndexOf(c);
                    } else
                    {
                        cList.Add(c);
                        Session["CartList"] = cList;
                        Response.Redirect("detail.aspx?ID=" +prodID);
                    }
                } else
                {
                    List<CartItem> cList = new List<CartItem>();
                    cList.Add(c);
                    Session["CartList"] = cList;
                    Response.Redirect("detail.aspx?ID=" + prodID);
                }

            }
            else
            {
                Response.Redirect("login.aspx");
            }
        }
    }

}