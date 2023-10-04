using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team34_GP_IFM02B2_2023_WebApp.ResQReference;

namespace Team34_GP_IFM02B2_2023_WebApp
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        ResQReference.RESQSERVICEClient rc = new ResQReference.RESQSERVICEClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["User"]!=null)
            {
                noLog.Visible = false;
                UserTable user = ((UserTable)Session["User"]);
                int type = user.UserType;
                switch(type)
                {
                    case 0:
                        adLog.Visible = true;
                        break;
                    case 1:
                        logCust.Visible = true;
                        break;
                    case 2:
                        storeLog.Visible = true;
                        break; 

                };
            }
        }
    }
}