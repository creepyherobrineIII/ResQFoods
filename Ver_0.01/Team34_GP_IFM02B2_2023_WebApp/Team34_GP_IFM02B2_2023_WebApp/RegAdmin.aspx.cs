using HashPass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Team34_GP_IFM02B2_2023_WebApp.ResQReference;

namespace Team34_GP_IFM02B2_2023_WebApp
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        ResQReference.RESQSERVICEClient sc = new ResQReference.RESQSERVICEClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            regStat.Visible = false;
            
        }

        protected void logReg_Click(object sender, EventArgs e)
        {
            regStat.Visible = false;
            String email = uEmail.Value;
            String pass = Secrecy.HashPassword(uPass.Value);
            String cps = Secrecy.HashPassword(ucPass.Value);
            if (pass.Equals(cps))
            {
                
                var regstr = sc.regAdmin(email, pass);
                if (regstr)
                {
                    UserTable u = sc.getUser(email, 0);
                    Session["user"] = u;
                    regStat.Visible = true;
                    Response.Redirect("index.aspx");
                }
            }

        }
    }
}
