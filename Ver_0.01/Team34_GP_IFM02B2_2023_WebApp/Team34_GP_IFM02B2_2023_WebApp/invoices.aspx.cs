﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team34_GP_IFM02B2_2023_WebApp.ResQReference;

namespace Team34_GP_IFM02B2_2023_WebApp
{
    public partial class invoices : System.Web.UI.Page
    {
        ResQReference.RESQSERVICEClient sc = new ResQReference.RESQSERVICEClient();
        int user_id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            //get user
              if (Session["user"] == null)
              {
                Response.Redirect("index.aspx");
              }
              else
             //only display information if user is logged in
            {
                UserTable user = (UserTable)Session["user"];
                user_id = user.UserId;
                //get user invoices 
                List<Invoice> invoices = sc.getInvoices(user_id).ToList();
                
                string display = DisplayInvoices(invoices);
                InvoiceDisplay.InnerHtml = display;
            }

            
        }

        //Display Invoice Items (Gets Called in Display Invoices)
        private String DisplayInvoiceItem(InvoiceItem item)
        {
            var p = sc.getProduct(item.ProductId);

            String display_items = "<tr><td class='nk-product-cart-thumb'>";
            display_items += "<a href='product.aspx?ID=" + p.ProductId + "' class='nk-image-box-1 nk-post-image'>";
            display_items += "<img src='" + p.Picture + "' alt==" + p.Name + " width='115'></a></td>";
            display_items += "<td class='nk-product-cart-title'><h5 class='h6'>Product:</h5><div class='nk-gap-1'></div>";
            //no link to products already sold out (page shouldn't exist anymore)
            if (p.Enabled)
                display_items += "<h2 class='nk-post-title h4'><a href='detail.aspx?ID=" + p.ProductId + "'>" + p.Name + "</a></h2></td>";
            else
                display_items += "<h2 class='nk-post-title h4'>" + p.Name + "</h2></td>";
            display_items += "<td class='nk-product-cart-price'><h5 class='h6'>Price:</h5><div class='nk-gap-1'>R" + p.Price.ToString("#.##") + "</div>";

            display_items += "<td class='nk-product-cart-remove'><a href='#'><span class='ion-android-close'></span></a></td>";

            display_items += "<td class='nk-product-cart-price'><h5 class='h6'>Quantity:</h5><div class='nk-gap-1'></div>";
            display_items += "<strong>" + item.Quantity + "</strong></td>";
            display_items += "<td class='nk-product-cart-remove'><a href='#'><span class='ion-android-close'></span></a></td></tr>";

            return display_items;
        }

        private String DisplayInvoice(Invoice invoice)
        {
            String display = "<table class='table nk-store-cart-products'><tbody>";
            display += "<tr><td class='nk-product-cart-title'><h5 class='h6'>Invoice: " + invoice.InvoiceId + "</h5><div class='nk-gap-1'></div>"
                    + "<h5 class='nk-post-title h4'>Date Created: " + invoice.DateAdded + "</h5></td>";

            List<InvoiceItem> items = sc.getInvoiceItems(invoice.InvoiceId).ToList();
            foreach (InvoiceItem i in items)
            {
                display += DisplayInvoiceItem(i);
            }

            display += "</tbody></table>";



            display += "<h3 class='nk-title h4'>Cart Totals</h3>";
            display += "<table class='nk-table nk-table-sm'><tbody><tr class='nk-store-cart-totals-subtotal'>";
            display += "<td>Subtotal</td><td> R" + invoice.TotalPrice.ToString("#.##") + "</td>";
            display += "<tr class='nk-store-cart-totals-shipping'>";
            display += "<tr class='nk-store-cart-totals-total'>";
            display += "<td>Total</td><td> R" + invoice.TotalPrice.ToString("#.##") + "</td></tr></tbody></table>";

            return display;
        }

        private String DisplayInvoices(List<Invoice> invoices)
        {
            String display = "";
            foreach (Invoice i in invoices)
                display += DisplayInvoice(i);
            return display;
        }

    }

}
