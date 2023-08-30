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
        ResQReference.RESQSERVICEClient sc = new ResQReference.RESQSERVICEClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            noLog.Visible = false;
        }

        protected void logButt_Click(object sender, EventArgs e)
        {
            noLog.Visible = false;
            Console.WriteLine("LOG_CLICKED");
            String GivenEmail = logEmail.Value;
            String GivenPassword = Secrecy.HashPassword(logPass.Value); // hashpassword, never gets stored
            int logSys = sc.loginUser(GivenEmail, GivenPassword); //send details to api?

            // Session["user"] = login(...);
            //Put this in site master
            //SysUser s = (SysUser)Session["user"] */
            switch (logSys)
            {
                case -1:
                    noLog.Visible = true;
                    break;
                case 0:
                    Server.Transfer("index.aspx");
                    break;
                case 1:
                    Server.Transfer("index.aspx");
                    break;
                case 2:
                    Server.Transfer("index.aspx");
                    break;

            }
        }
    }
}
