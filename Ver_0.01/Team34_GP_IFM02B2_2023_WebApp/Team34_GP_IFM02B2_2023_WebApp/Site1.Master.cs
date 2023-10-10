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
            noLog.Visible = true;

            if (Session["user"]!=null)
            {
                noLog.Visible = false;
                UserTable user = ((UserTable)Session["user"]);
                int type = user.UserType;
                switch (type)
                {
                    case 0:
                        adLog.Visible = true;
                        break;
                    case 1:
                        logCust.Visible = true;
                        String cartCust = "";
                        String noCi = "0";
                        if (Session["CartList"] != null)
                        {
                            noCi = ((List<CartItem>)Session["Cartlist"]).Count + "";
                        }
                        cartCust +="<a href = 'cart.aspx' class='btn px-0 ml-2'>";
                        cartCust += "<i class='fas fa-shopping-cart text-dark'></i>";
                        cartCust+= "<span class='badge text-dark border border-dark rounded-circle' style='padding-bottom: 2px;'>"+noCi+"</span>";
                        cartCust += "</a>";
                        WishCust.InnerHtml = cartCust;
                        break;
                    case 2:
                        storeLog.Visible = true;
                        String cStore = "";
                        String cSn = "0";
                        if (Session["CartList"] != null)
                        {
                            cSn = ((List<CartItem>)Session["Cartlist"]).Count + "";
                        }
                        cStore += "<a href = 'cart.aspx' class='btn px-0 ml-2'>";
                        cStore += "<i class='fas fa-shopping-cart text-dark'></i>";
                        cStore += "<span class='badge text-dark border border-dark rounded-circle' style='padding-bottom: 2px;'>" + cSn + "</span>";
                        cStore += "</a>";
                        WishCust.InnerHtml = cStore;
                        break; 

                };
            }
        }
    }
}