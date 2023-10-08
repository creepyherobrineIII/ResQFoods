using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team34_GP_IFM02B2_2023_WebApp.ResQReference;

namespace Team34_GP_IFM02B2_2023_WebApp
{
    public partial class reports : System.Web.UI.Page
    {
        ResQReference.RESQSERVICEClient sc = new ResQReference.RESQSERVICEClient();
        protected void Page_Load(object sender, EventArgs e)
        {

            UserTable user = (UserTable)Session["user"];


            if (user != null)
            {
                if (user.UserType == 0)
                {
                    //show everything  except total products
                    products.Visible = false;
                }
                else if (user.UserType == 2)
                {
                    //show everything except users registered and options to look at other business
                    users.Visible = false;
                    sType.Visible = false;
                    Store storeuser = sc.getStore(user.Email);

                }
                else Server.Transfer("index.aspx"); //transfer home if customer 
            }


            //Tab 1
             totalSales.InnerHtml = "<p><b>R" + (sc.getReportTotalSales()).ToString("#.#0") + "</b></p>";


            //Tab 2

            //Get  products for specific store
            //Display those produts
            //List<Product> products = sc.getPro

            /*foreach (Product p in products)
            {
                Display += "<tr>";
                Display += "<td class='align-middle'><img src='" + c.Product.Picture + "' alt='' style='width: 50px;'>" + c.Product.Name + "</td>";
                Display += " <td class='align-middle'>R" + c.Product.Price + "</td>";
                Display += "<td class='align-middle'>";
                Display += "<div class='input-group quantity mx-auto' style='width: 100px;'>";
                Display += "<div class='input-group-btn'>";
                Display += "<button class='btn btn-sm btn-primary btn-minus'>";
                Display += "<i class='fa fa-minus'></i> </button> </div>";
                Display += "<input type='text' class='form-control form-control-sm bg-secondary border-0 text-center' value='1'>";
                Display += "<div class='input-group-btn'>";
                Display += "<button class='btn btn-sm btn-primary btn-plus'>";
                Display += "<i class='fa fa-plus'></i>";
                Display += "</button> </div> </div> </td> ";
                Display += "<button class='btn btn-sm btn-primary btn-plus'>";
                Display += "<td class='align-middle'>R" + (c.Product.Price * c.Quantity) + "</td>";
                Display += "<td class='align-middle'><button class='btn btn-sm btn-danger'><i class='fa fa-times'></i></button></td> </tr>";
            }
            //cartitem.InnerHtml = Display; //display to
            */

            //Tab 3

         
        }

        //Tab 5
        protected void btnUsers_Click(object sender, EventArgs e)
        {
            string date = bDate.Value;
            if (date != "")
            {
                DateTime date2 = DateTime.Parse(date);
                int p = sc.getNumRegUsers(date2);
                usersTotal.InnerHtml = "Total Users: " + p;
            }
           

        }
    }
}