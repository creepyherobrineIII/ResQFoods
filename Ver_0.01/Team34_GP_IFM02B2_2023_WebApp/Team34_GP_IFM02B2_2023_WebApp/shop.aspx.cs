using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team34_GP_IFM02B2_2023_WebApp.ResQReference;

namespace Team34_GP_IFM02B2_2023_WebApp
{
    public partial class shop : System.Web.UI.Page
    {
        RESQSERVICEClient rc = new RESQSERVICEClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Store> st = new List<Store>(rc.getStores());
            String name = "noSearch";
            double upper = -1;
            double lower = -1;
            int man = -1;
            int cat = -1;


            if (Request.QueryString["Filter"]!=null)
            {
                String filt = Request.QueryString["Filter"];
                if (filt.Contains("N"))
                {
                    String n = Request.QueryString["Name"];
                    name = n;
                }
                else if(filt.Contains("S"))
                {
                    int SID = Convert.ToInt32(Request.QueryString["SID"]);
                    man = SID;
                }
                else if (filt.Contains("P"))
                {
                    double u = Convert.ToDouble(Request.QueryString["upper"]);
                    double l = Convert.ToDouble(Request.QueryString["lower"]);
                    upper = u;
                    lower = l;
                }
                else if (filt.Contains("T"))
                {
                    int TID = Convert.ToInt32(Request.QueryString["Tag"]);
                    cat = TID;
                }
            }
            List<Product> p = new List<Product>(rc.getFilteredList(name, upper, lower, cat, man));

        }
    }
}