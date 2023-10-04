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
            if (Session["User"] != null)
            {
                int type = ((UserTable)Session["user"]).UserType;
                List<Product> prod = new List<Product>(sc.getAllProducts());
            }
        }
    }
}