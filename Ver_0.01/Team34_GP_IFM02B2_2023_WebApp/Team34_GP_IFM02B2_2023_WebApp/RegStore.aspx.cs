using HashPass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
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

            //Make sure nothing is incomplete!
            if (sName.Value.Equals("") || cName.Value.Equals("") || uPass.Value.Equals("") || uEmail.Value.Equals("") || ucPass.Value.Equals("") || sloc.Value.Equals("") || sType.Value.Equals("") || !FileUpload1.HasFile)
            {
                regStat.Text = "One of the enteries is incomplete! Please fill in all information.";
                Server.Transfer("RegStore.aspx");
            }
            else
            {

                String snm = sName.Value;
                String cnm = cName.Value;
                String email = uEmail.Value;
                String pass = Secrecy.HashPassword(uPass.Value);
                String cps = Secrecy.HashPassword(ucPass.Value);
                String loc = sloc.Value;
                String type = sType.Value;
                String logoPath = Server.MapPath("~/assets/img/");
                String fPath = logoPath + FileUpload1.FileName;

                if(!File.Exists(fPath))
                {
                    FileUpload1.PostedFile.SaveAs(fPath);
                }

                if (pass.Equals(cps))
                {
                    //use to lower and find the picture (some of the pictures arent jpg atm, need to fix that 
                    var regstr = sc.regStore(email, pass, cnm, snm, "/assets/img/"+ FileUpload1.FileName, loc, type);
                    if (regstr)
                    {
                        UserTable curr = sc.getUser(email, 2);
                        Session["user"] = curr;
                        regStat.Visible = true;
                        Response.Redirect("index.aspx");
                    }
                }
                else
                {
                    regStat.Text = "Register Unsuccessful. Passwords Do Not Match";
                    Response.Redirect("RegStore.aspx");
                }
            }

        }
    }
}