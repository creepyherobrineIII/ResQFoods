using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team34_GP_IFM02B2_2023_WebApp.ResQReference;

namespace Team34_GP_IFM02B2_2023_WebApp
{
    public partial class checkout : System.Web.UI.Page
    {
        ResQReference.RESQSERVICEClient sc = new ResQReference.RESQSERVICEClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UID"] == null)
             {
                int ID = Convert.ToInt32(Session["UID"]); //get user id
              }

            string products = "";
            string totals = "";

            List<CartItem> cart = (List<CartItem>)Session["CartList"];
           
            foreach(CartItem c in cart)
            {
                products += "<p>" + c.Product.Name+ "</p>";
                products += "<p>R" + c.Product.Price + "</p> ";
            }

            order.InnerHtml = products;

            totals += "<h6>"+ sc.getCat +"</h6>";
            totals += "<h6>$150</h6>";
           
                            

        }
    }
}

            
                           