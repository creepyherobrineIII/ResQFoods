using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Team34_GP_IFM02B2_2023_WebApp.ResQReference;

namespace Team34_GP_IFM02B2_2023_WebApp
{
    public partial class AddProduct : System.Web.UI.Page
    {
        private object lblMessage;

        protected void Page_Load(object sender, EventArgs e)
        {
            //not sure if i should add page logic here
        }

        protected bool btnAddProduct_Click(object sender, EventArgs e)
        {
            // Handle adding a new product to the shop
            // Retrieve input values from form controls
            string productName = txtProductName.Value;
            string productDescription = txtProductDescription.Value;
            // Get other input values

            // Perform validation, data processing, and add the product to the shop using the WCF service
            try
            {
                RESQSERVICEClient serviceClient = new RESQSERVICEClient();

                // Replace the following placeholders with actual values
                int storeId = 123; // Replace with the actual store ID
                double productPrice = 19.99; // Replace with the actual product price
                string picturePath = "path_to_image.jpg"; // Replace with the actual image path
                DateTime dateAdded = DateTime.Now; // Replace with the actual date

                bool success = serviceClient.AddProduct(storeId, productName, productDescription, 1, productPrice, picturePath, dateAdded, true);

                if (success)
                {
                    // Product added successfully, you can also show a success message if needed
                    Response.Redirect("Shop.aspx"); // Redirect to the shop page after adding the product
                }
                else
                {
                    //lblMessage. = "You were not able to successfully add the product";
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
   }