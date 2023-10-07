using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team34_GP_IFM02B2_2023_WebApp.ResQReference;

namespace Team34_GP_IFM02B2_2023_WebApp
{
    public partial class checkout : System.Web.UI.Page
    {
        ResQReference.RESQSERVICEClient sc = new ResQReference.RESQSERVICEClient();

        //Made global so that onclick can access
        private decimal finalamount = 0;
        private decimal shippingtotal = 50; //default shipping is 50, free if total is over 200
        List<CartItem> cart = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            Customer customer = null; //get customer

            if (Session["user"] == null)
             {
                customer = (Customer)Session["user"];
              }

            string products = "";
            string totals = "";
            string shippingdisplay = "";
            string final = "";

            decimal totalcartitems = 0;
           

            if (Session["CartList"] != null)
            {
                 cart = (List<CartItem>)Session["CartList"];


                foreach (CartItem c in cart)
                {
                    products += "<p>" + c.Product.Name + "</p>";
                    products += "<p>R" + c.Product.Price + "</p> ";

                    //Calculate session cart total
                    totalcartitems += c.Product.Price;
                }

                order.InnerHtml = products;


                totals += "<h6>" + "Cart Subtotal" + "</h6>";
                totals += "<h6>" + totalcartitems + "</h6>";

                ordertotalbreakdown.InnerHtml = totals;

                if((totalcartitems >= 200) || customer.GrantRecipient) // if total over 200, or grant recipient
                {
                    shippingtotal = 0; //make free shipping
                }

                shippingdisplay += "<h6 class='font-weight-medium'>Shipping</h6>";
                shippingdisplay += "<h6 class='font-weight-medium''>R" + shippingtotal +"</h6>";


                shipping.InnerHtml = shippingdisplay;

                finalamount = shippingtotal + totalcartitems;

                if (customer.GrantRecipient)
                {

                    finalamount = finalamount - (finalamount * 15 / 100); //15 percent discount
                    final = "<h5>Total</h5> <h5>R" + finalamount + "</h5>";
                    //  final += "<h5>Total</h5> <h5>R" + finalamount + "</h5>"; //add a message to tell use a discout was added
                }
                else { final = "<h5>Total</h5> <h5>R" + finalamount + "</h5>"; 
                }

                finaltotal.InnerHtml = final;

            }
              
            
    }
        //Onlick update stock and clear cart, and send to success page
        protected void pay_Click(object sender, EventArgs e)
        {
            UserTable currentuser = (UserTable)Session["user"];
            int userID = currentuser.UserId;

           // sc.addInvoice(userID, finalamount, DateTime.Now, cart);
            decreaseStock();
            Response.Redirect("success.aspx");
        }

        private void decreaseStock()
        {
            List<CartItem> cartProducts = (List<CartItem>)Session["CartList"];

            foreach (CartItem c in cartProducts)
            {

                int prodQuantity = (int)c.Product.Quantity -1;
                Product p = c.Product;
                p.Quantity = prodQuantity; //update quantity

                if(p.Quantity == 0)
                {
                    p.Enabled = false; //disable to product
                }
                sc.editProduct(p);//update stock  of specific product //***Add a counter for how many items sold? idk (can do it by checking unenabled stock 

                //Delete Products from Cart
                Session["CartList"] = new List<CartItem>(); //make a new empty cart
            }

            
        }
    }

    }

            