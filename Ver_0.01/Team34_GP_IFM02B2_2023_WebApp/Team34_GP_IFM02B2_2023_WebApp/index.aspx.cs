using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//add service reference using
using Team34_GP_IFM02B2_2023_WebApp.Team34_ServiceReference;

namespace Team34_GP_IFM02B2_2023_WebApp
{
    public partial class index : System.Web.UI.Page
    {
        ServiceClient sc = new ServiceClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    }
}