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
            Store storeuser = new Store();


            if (user != null)
            {
                if (user.UserType == 0)
                {
                    //show everything  except total products
                    productstotal.Visible = false;
                }
                else if (user.UserType == 2)
                {
                    //show everything except users registered and options to look at other business
                    users.Visible = false;
                    sTypes.Visible = false;
                    bestsellingshop.Visible = false;
                    storeuser = sc.getStore(user.Email);

                }
                else Server.Transfer("index.aspx"); //transfer home if customer 

                //Tab 1
                 totalSales.InnerHtml = "<p><b>R" + (sc.getReportTotalSales()).ToString("#.#0") + "</b></p>";


                //Tab 2
                //Get  products for specific store
                //Display those produts
                  String Display = "";
                  List<Product> products = sc.getProductStock(storeuser.UserId).ToList();

                  foreach (Product p in products)
                  {
                      Display += "<tr>";
                      Display += "<td class='align-middle'>" + p.UserId + "</td>";
                      Display += " <td class='align-middle'>R" + p.Name + "</td>";
                      Display += "<td class='align-middle'>" + p.ProductTags + "</td>";
                      Display += "<td class='align-middle'>" + p.Quantity + "</td>";
                      Display += "<td class='align-middle'>" + p.NumSold + "</td>";
                      Display += "</tr>";
                  }

                  totals.InnerHtml = Display; //display to


                  //Tab 3
                  String stype = Select1.Value;
                  beststore.InnerHtml = "<p>" + sc.getBestSellingStoreFromType(stype) + "</p>"; 


                //Tab 4
                List<Tag> tags = new List<Tag>(sc.getTags());

                foreach (Tag t in tags)
                {
                    categories.Items.Add(t.TagName);
                }
                catdisplay.InnerHtml = "<p>" + sc.getBestCategory() + "</p>";
            }


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