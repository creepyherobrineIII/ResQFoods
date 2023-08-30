using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Team34_GP_IFM02B2_2023_WebApp
{
    public partial class AddProduct : System.Web.UI.Page
    {
        ResQReference.RESQSERVICEClient sc = new ResQReference.RESQSERVICEClient();
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void addProd_Click(object sender, EventArgs e)
        {
            //int sID, string name, string desc, double price, string picPath, DateTime date, bool enabled
            String pnm = pName.Value;
            String desc = pdesc.Value;
            Double price;
            Double.TryParse(pPrice.Value, out price);
            DateTime dAdd = DateTime.Today;
            bool enabled = true;

            bool adProd = sc.AddProduct(1, pnm, desc, price, "/assets/img/carousel-2.jpg", dAdd, true);
            if (adProd)
            {
                Server.Transfer("index.aspx");
            }
        }
    }
}