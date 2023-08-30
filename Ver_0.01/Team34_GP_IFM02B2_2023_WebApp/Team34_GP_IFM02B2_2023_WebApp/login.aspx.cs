using HashPass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Team34_GP_IFM02B2_2023_WebApp
{
    public partial class login : System.Web.UI.Page
    {
        Team34_ServiceReference.RESQSERVICEClient sc = new Team34_ServiceReference.RESQSERVICEClient();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void logButt_Click(object sender, EventArgs e)
        {
           /* String GivenEmail= logEmail.;
            String GivenPassword = Secrecy.HashPassword(Password.Text); // hashpassword, never gets stored
            sc.loginUser(GivenEmail, GivenPassword); //send details to api?
            // Session["user"] = login(...);
            //Put this in site master
            //SysUser s = (SysUser)Session["user"] */
        }

    }
}