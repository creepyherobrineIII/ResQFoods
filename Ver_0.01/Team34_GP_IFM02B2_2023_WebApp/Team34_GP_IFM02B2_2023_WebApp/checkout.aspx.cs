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
            UserTable user = null; //get customer

            if (Session["user"] != null)
             {
                user = (UserTable)Session["user"];
            }
            else
            {
                Response.Redirect("index.aspx");
            }

            Customer customer = null;
            if (user.UserType==1)
            {
                customer = sc.getCustomer(user.Email);


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
                        c.Product = sc.getProduct(c.ProductId);

                        products += "<p>" + c.Product.Name + "</p>";
                        products += "<p>R" + c.Product.Price + "</p> ";
                        products += "<p>" + c.Quantity + "</p> ";

                        //Calculate session cart total
                        totalcartitems += c.Product.Price * c.Quantity;
                    }

                    order.InnerHtml = products;


                    totals += "<h6>" + "Cart Subtotal" + "</h6>";
                    totals += "<h6>" + totalcartitems + "</h6>";

                    ordertotalbreakdown.InnerHtml = totals;

                    if ((totalcartitems >= 200) || customer.GrantRecipient) // if total over 200, or grant recipient
                    {
                        shippingtotal = 0; //make free shipping
                    }

                    shippingdisplay += "<h6 class='font-weight-medium'>Shipping</h6>";
                    shippingdisplay += "<h6 class='font-weight-medium''>R" + shippingtotal.ToString("#.##") + "</h6>";


                    shipping.InnerHtml = shippingdisplay;

                    finalamount = shippingtotal + totalcartitems;

                    if (customer.GrantRecipient)
                    {

                        finalamount = finalamount - (finalamount * 15 / 100); //15 percent discount
                        final += "<h6 class='font-weight-medium'>Grant Added</h6></br>";
                        final += "<h5>Total</h5> <h5>R" + finalamount.ToString("#.##") + "</h5>";
                        //  final += "<h5>Total</h5> <h5>R" + finalamount + "</h5>"; //add a message to tell use a discout was added
                    }
                    else
                    {
                        final = "<h5>Total</h5> <h5>R" + finalamount.ToString("#.##") + "</h5>";
                    }

                    finaltotal.InnerHtml = final;


                }

            }
              
            
    }
        //Onlick update stock and clear cart, and send to success page
        protected void pay_Click(object sender, EventArgs e)
        {
            UserTable currentuser = (UserTable)Session["user"];
            int userID = currentuser.UserId;

            List<int> Quanities = new List<int>();
            List<int> ProdIDs = new List<int>(); 

            foreach(CartItem c in cart)
            {
                Quanities.Add(c.Quantity);
                ProdIDs.Add(c.ProductId);
            }

            bool success = sc.addInvoice(userID, finalamount, DateTime.Now, ProdIDs.ToArray(), Quanities.ToArray() );
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

                
                sc.decProduct(c.ProductId);//update stock  of specific product //***Add a counter for how many items sold? idk (can do it by checking unenabled stock 

                //Delete Products from Cart
                Session["CartList"] = new List<CartItem>(); //make a new empty cart
            }

            
        }
    }

    }

            