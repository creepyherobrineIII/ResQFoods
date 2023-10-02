using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Team34_GP_IFM02B2_2023_WebApp
{
    
    public partial class index : System.Web.UI.Page
    {

        ResQReference.RESQSERVICEClient sc = new ResQReference.RESQSERVICEClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            /*int type = ((ResQReference.UserRecord)Session["user"]).userType;
            String id = Request.QueryString["username"];
            //IDLabel.Text = id;
            sc.getAllProducts();*/
        }
    }
}