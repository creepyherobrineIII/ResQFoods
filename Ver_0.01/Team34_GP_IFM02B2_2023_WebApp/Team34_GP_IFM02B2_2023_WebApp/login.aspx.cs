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
            String userName;
                Session["user"] = sc.getAdmin(GivenEmail);
                switch (logSys)
                {
                    case -1:
                        noLog.Visible = true;
                        break;
                    case 0:
                        Session["user"] = sc.getAdmin(GivenEmail);
                        userName = ((ResQReference.UserRecord)Session["user"]).userEmail;
                        //Server.Transfer("index.aspx");
                        Response.Redirect("index.aspx?userName=" + userName);
                    break;
                    case 1:
                        Session["user"] = sc.getAdmin(GivenEmail);
                        ResQReference.CustomerRecord cust = sc.getCustomer(GivenEmail);
                        userName = cust.fName + " " + cust.lName;
                         //Server.Transfer("index.aspx?");
                        Response.Redirect("index.aspx?userName=" + userName);
                    break;
                    case 2:
                        Session["user"] = sc.getAdmin(GivenEmail);
                        ResQReference.StoreRecord store = sc.getStore(GivenEmail);
                        userName = store.name;
                        //Server.Transfer("index.aspx");
                        Response.Redirect("index.aspx?userName=" + userName);
                    break;

                }
        }
    }
}