using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    public partial class EditProduct : System.Web.UI.Page
    {
        private readonly RESQSERVICEClient sc = new RESQSERVICEClient();
        int productID; // Define a variable to hold the product ID
        private object txtName;
        private object txtDescription;
        private object lblMessage;

        public object txtPrice { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if a product ID is provided in the query string
            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                if (int.TryParse(Request.QueryString["ID"], out productID))
                {
                    if (!IsPostBack)
                    {
                        // Load the product details for editing
                        LoadProductDetails(productID);
                    }
                }
            }
            else
            {
                // Handle the case where no product ID is provided
                Response.Redirect("shop.aspx");
            }
        }

        protected void LoadProductDetails(int productID)
        {
            // Retrieve product details from the database
            Product product = sc.GetProduct(productID);

            if (product != null)
            {
                // Populate the form fields with product details
                txtName.Value = product.Name;
                txtDescription.Value = product.Description;
                txtPrice.Value = product.Price.ToString();
                txtQuantity.Value = product.Quantity.ToString();
            }
            else
            {
                // Handle the case where the product is not found
                Response.Redirect("shop.aspx");
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            // Get the updated values from the form fields
            string newName = txtName.Text;
            string newDescription = txtDescription.Text;
            double newPrice;
            int newQuantity;

            if (double.TryParse(txtPrice.Text, out newPrice) && int.TryParse(txtQuantity.Text, out newQuantity))
            {
                // Update the product in the database
                bool isUpdated = UpdateProduct(productID, newName, newDescription, newPrice, newQuantity);

                if (isUpdated)
                {
                    // Handle successful update (e.g., show a success message)
                    lblMessage.Text = "Product updated successfully!";
                }
                else
                {
                    // Handle the case where the product update fails
                    lblMessage.Text = "Product update failed!";
                }
            }
            else
            {
                // Handle the case where price or quantity parsing fails
                lblMessage.Text = "Invalid price or quantity!";
            }
        }

        protected bool UpdateProduct(int productID, string newName, string newDescription, double newPrice, int newQuantity)
        {
            // Implement the product update logic using your data access layer
            // You can use ADO.NET or Entity Framework to update the product in the database
            // Return true if the update is successful, or false if it fails

            // Example ADO.NET update logic
            // Replace with your actual database update code
            try
            {
                // Define your connection string
                string connectionString = "your_connection_string_here";

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Implement your SQL update query
                    string updateQuery = "UPDATE Products SET Name = @NewName, Description = @NewDescription, Price = @NewPrice, Quantity = @NewQuantity WHERE ProductID = @ProductID";

                    using (var command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@ProductID", productID);
                        command.Parameters.AddWithValue("@NewName", newName);
                        command.Parameters.AddWithValue("@NewDescription", newDescription);
                        command.Parameters.AddWithValue("@NewPrice", newPrice);
                        command.Parameters.AddWithValue("@NewQuantity", newQuantity);

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
                return false;
            }
        }

    }
}