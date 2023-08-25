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
        Team34_ServiceReference.ServiceClient sc = new Team34_ServiceReference.ServiceClient();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            String GivenEmail= EmailAddress.Text;
            String GivenPassword = Secrecy.HashPassword(Password.Text); // hashpassword, never gets stored
            sc.Login(GivenEmail, GivenPassword); //send details to api? 
        }

    }
}