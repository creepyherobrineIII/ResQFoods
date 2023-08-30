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
            regStat.Visible = false;
        }

        protected void logReg_Click(object sender, EventArgs e)
        {
            regStat.Visible = false;
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
                if(regcus)
                {
                    regStat.Visible = true;
                    Server.Transfer("index.aspx");
                }
            }

        }
    }
}