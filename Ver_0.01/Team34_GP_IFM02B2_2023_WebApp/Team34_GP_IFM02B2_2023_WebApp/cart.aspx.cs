using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team34_GP_IFM02B2_2023_WebApp.ResQReference;

namespace Team34_GP_IFM02B2_2023_WebApp
{
    public partial class cart : System.Web.UI.Page
    {
        ResQReference.RESQSERVICEClient sc = new ResQReference.RESQSERVICEClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            //COMMENTED OUT FOR TESTING OF EASE
            //user must be logged in
            //if (Session["UID"] == null)
            // {
            //     Response.Redirect("index.aspx");
            //  }

            //Increase Quantity if user wishes to
            if (Request.QueryString["dec"] != null)
            {
                int prodID = Convert.ToInt32(Request.QueryString["dec"]);

                List<CartItem> cart = (List<CartItem>)Session["CartList"];

                foreach (CartItem c in cart)
                {
                    if (prodID == c.ProductId) //find the item
                    {
                        c.Quantity = c.Quantity - 1;
                        break;//leave loop
                    }
                }
            }

            if (Request.QueryString["inc"] != null)
            {
                int prodID = Convert.ToInt32(Request.QueryString["inc"]);

                List<CartItem> cart = (List<CartItem>)Session["CartList"];

                foreach (CartItem c in cart)
                {
                    if (prodID == c.ProductId) //find the item
                    {
                        c.Quantity = c.Quantity + 1;
                        break;//leave loop
                    }
                }
            }

            //If the user wants to remove from cart, reload page and do this
            if (Request.QueryString["RemoveID"] != null)
                {
                    List<CartItem> cart = (List<CartItem>)Session["CartList"]; //the cart must have been filled with an item, to be able to delete it
                    int remID = Convert.ToInt32(Request.QueryString["RemoveID"]);

                    CartItem remove = null;
                    //find product in list and remove it
                    foreach (CartItem c in cart)
                    {

                        if (remID == c.ProductId) //find the item
                        {
                            remove = c;
                            break;//leave loop
                        }
                    }

                    cart.Remove(remove); //remove from session
                }

                //Make sure to populate the session variable on add to cart button
                if (Session["CartList"] != null)
                {
                    List<CartItem> cart = (List<CartItem>)Session["CartList"];

                String Display = "";
                foreach (CartItem c in cart)
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
                    cartitem.InnerHtml = Display; //display to 


                string Display2 = "";
                    Display2 += "<div class='border-bottom pb-2'>";
                    Display2 += "<div class='d-flex justify-content-between mb-3'>";
                    Display2 += "<h6>Subtotal</h6> <h6>R" + 20 + "</h6></div>"; //NEEDS A BACKEND FUNCTION TO CALC TOTAL PRICE OF CART
                    Display2 += "<div class='d-flex justify-content-between'><h6 class='font-weight-medium'>VAT</h6><h6 class='font-weight-medium'>R10</h6></div><br/>";
                    Display2 += "<div class='d-flex justify-content-between'> <h6 class='font-weight-medium'>Shipping</h6><h6 class='font-weight-medium'>R10</h6></div></div>";
                    Display2 += "<div class='pt-2'>";
                    Display2 += "<div class='d-flex justify-content-between mt-2'> <h5>Total</h5><h5>$160</h5></div>";
                    Display2 += "<button class='btn btn-block btn-primary font-weight-bold my-3 py-3'>Proceed To Checkout</button>";

                     cartsum.InnerHtml = Display; //display to

            }

                                                   
            }
    }
}

