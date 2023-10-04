using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Team34_GP_IFM02B2_2023_WebApp
{
    public partial class cart : System.Web.UI.Page
    {
        ResQReference.RESQSERVICEClient sc = new ResQReference.RESQSERVICEClient();
        protected void Page_Load(object sender, EventArgs e)
        {

            //if (Session["UID"] == null)
            // {
            //     Response.Redirect("index.aspx");
            //  }
            // int UserID = Convert.ToInt32(Session["UID"]);

            //Get a product id if added to cart 

            //Display cart items
            String Display = "";



               
            }
        }
    }
