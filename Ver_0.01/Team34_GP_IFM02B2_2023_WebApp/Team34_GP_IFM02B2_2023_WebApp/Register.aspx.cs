using HashPass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Team34_GP_IFM02B2_2023_WebApp
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        ResQReference.RESQSERVICEClient sc = new ResQReference.RESQSERVICEClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            regStat.Visible = true; 
        }

        protected void logReg_Click(object sender, EventArgs e)
        {
            regStat.Visible = true;

            //Make sure nothing is incomplete!
            if (fName.Value.Equals("") || lName.Value.Equals("") || uPass.Value.Equals("") || uEmail.Value.Equals("") || ucPass.Value.Equals("") || bDate.Value.Equals(""))
            {
                regStat.Text = "One of the enteries is incomplete! Please fill in all information.";
                Server.Transfer("Register.aspx");
            }
            else
            {

                String fnm = fName.Value;
                String lnm = lName.Value;
                String email = uEmail.Value;
                String pass = Secrecy.HashPassword(uPass.Value);
                String cps = Secrecy.HashPassword(ucPass.Value);
                String date = bDate.Value;
                String date2 = bDate.ToString();
                DateTime dt = Convert.ToDateTime(bDate.Value);
                if (pass.Equals(cps))
                {
                    var regcus = sc.regCust(email, pass, fnm, lnm, dt, true);
                    if (regcus)
                    {
                        regStat.Visible = true;
                        regStat.Text = "Register Success!";
                        Server.Transfer("login.aspx");
                    }
                }
                else
                {
                    regStat.Text = "Register Unsuccessful. Passwords Do Not Match";
                    Server.Transfer("Register.aspx");
                }
            }

        }
    }
}