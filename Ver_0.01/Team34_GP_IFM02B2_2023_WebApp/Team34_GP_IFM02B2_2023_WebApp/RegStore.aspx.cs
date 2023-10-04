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
    public partial class RegStore : System.Web.UI.Page
    {
        RESQSERVICEClient sc = new RESQSERVICEClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            regStat.Visible = false;
        }

        protected void logReg_Click(object sender, EventArgs e)
        {
            regStat.Visible = false;
            String snm = sName.Value;
            String cnm = cName.Value;
            String email = uEmail.Value;
            String pass = Secrecy.HashPassword(uPass.Value);
            String cps = Secrecy.HashPassword(ucPass.Value);
            String loc = sLoc.Value;
            String type = sType.Value;
            if (pass.Equals(cps))
            {
                //use to lower and find the picture (some of the pictures arent jpg atm, need to fix that 
                var regstr = sc.regStore(email, pass, cnm, snm, "/assets/img/" + cnm.ToLower() +".jpg", loc, type);
                if (regstr)
                {
                    regStat.Visible = true;
                    Server.Transfer("index.aspx");
                }
            }

        }
    }
}