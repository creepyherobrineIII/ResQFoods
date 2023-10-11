using HashPass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team34_GP_IFM02B2_2023_WebApp.ResQReference;

namespace Team34_GP_IFM02B2_2023_WebApp
{
    public partial class login : System.Web.UI.Page
    {
        RESQSERVICEClient sc = new RESQSERVICEClient();
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

                String userName;
                //Session["user"] = sc.getAdmin(GivenEmail);
                
                switch (logSys)
                {
                    case -1:
                        noLog.Visible = true;
                    Response.Redirect("login.aspx");
                    break;
                    case 0:
                        Session["user"] = sc.getUser(GivenEmail, 0);
                        //userName = ((UserTable)Session["user"]).Email;
                    //Server.Transfer("index.aspx");
                    //Response.Redirect("index.aspx?userName=" + userName);
                    break;
                    case 1:
                        Session["user"] = sc.getUser(GivenEmail, 1);
                        Customer cust = sc.getCustomer(GivenEmail);
                        //userName = cust.FirstName + " " + cust.LastName;
                        //Server.Transfer("index.aspx?");
                        //Response.Redirect("index.aspx?userName=" + userName);
                    break;
                    case 2:
                        Session["user"] = sc.getUser(GivenEmail, 2);
                        Store store = sc.getStore(GivenEmail);
                        //userName = store.Name;
                        //Server.Transfer("index.aspx");
                        //Response.Redirect("index.aspx?userName=" + userName);
                    break;  
                }

            //Create a cart session variable 
            List<CartItem> itemList = new List<CartItem>();
            Session["CartList"] = itemList;
            Response.Redirect("index.aspx");
        }
    }
}