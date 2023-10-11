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
            if (Session["user"] == null)
             {
                 Response.Redirect("index.aspx");
             }

            UserTable user = (UserTable)Session["user"];

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
                    Product p = new Product();
                        p = sc.getProduct(c.ProductId);
                        Display += "<tr>";
                        Display += "<td class='align-middle'><img src='" + p.Picture + "' alt='' style='width: 50px;'>" + p.Name + "</td>";
                        Display += " <td class='align-middle'>R" + p.Price + "</td>";
                        Display += "<td class='align-middle'>";
                        Display += "<div class='input-group quantity mx-auto' style='width: 100px;'>";
                        Display += "<div class='input-group-btn'>";
                        Display += "<a href='cart.aspx?dec=" + p.ProductId + "'><button class='btn btn-sm btn-primary btn-minus'>";
                        Display += "<i class='fa fa-minus'></i> </button></a> </div>";
                        Display += "<input type='text' class='form-control form-control-sm bg-secondary border-0 text-center' value='" + c.Quantity +"'>";
                        Display += "<div class='input-group-btn'>";
                        Display += "<a href='cart.aspx?inc=" + p.ProductId + "'<button class='btn btn-sm btn-primary btn-plus'>";
                        Display += "<i class='fa fa-plus'></i>";
                        Display += "</button></a> </div> </div> </td> ";
                        Display += "<button class='btn btn-sm btn-primary btn-plus'>";
                        Display += "<td class='align-middle'>R" + (p.Price * c.Quantity) + "</td>";
                        Display += "<td class='align-middle'><a href='cart.aspx?REMOVEID=" + p.ProductId+ "'><button class='btn btn-sm btn-danger'><i class='fa fa-times'></i></button></td> </tr>";
                }
                    cartitem.InnerHtml = Display; //display to 


                string Display2 = "";
                    Display2 += "<div class='border-bottom pb-2'>";
                    Display2 += "<div class='d-flex justify-content-between mb-3'>";
                    Display2 += "<div class='pt-2'>";
                    Display2 += "<div class='d-flex justify-content-between mt-2'><h5>Total: R" + GetCartTotal(cart) + "</h5></div>";
                    Display2 +=  "";
               

                     cartsum.InnerHtml = Display2; //display to

            }

                                                   
            }

        protected decimal GetCartTotal(List<CartItem> l)
        {
            decimal total = 0;

            foreach (CartItem c in l)
            {
                Product p = sc.getProduct(c.ProductId);

                Product prod = new Product
                {
                    ProductId = p.ProductId,
                    Picture = p.Picture,
                    DateAdded = p.DateAdded,
                    Description = p.Description,
                    Price = p.Price,
                    ProductTags = p.ProductTags,
                    Store = p.Store

                };

                total += prod.Price * c.Quantity;
            }

            return total;
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        { 
                Response.Redirect("checkout.aspx");
        }

    }
}

