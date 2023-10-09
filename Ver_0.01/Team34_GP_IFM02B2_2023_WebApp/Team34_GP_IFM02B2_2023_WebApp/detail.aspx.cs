using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Team34_GP_IFM02B2_2023_WebApp
{
    public partial class detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ProductList"] == null)
        {
            Session["ProductList"] = productList;
        }
        else
        {
            productList = (List<Product>)Session["ProductList"];
        }

        if (!IsPostBack)
        {
            // Load the product data from a database or other data source
            productList = LoadProductData();
        }
        }
    }

     protected void AddProduct(object sender, EventArgs e)
    {
        // Get product data from form fields
        string name = productName.Value;
        string description = productDescription.Value;
        decimal price;

        if (!decimal.TryParse(productPrice.Value, out price))
        {
           //invalid price handling (can do it dont have to)
            return;
        }

        // Create a new product and add it to the list
        Product product = new Product
        {
            Name = name,
            Description = description,
            Price = price
        };

        productList.Add(product);

        // Optionally, you can save the productList to a database or file for persistence

        // Clear form fields
        productName.Value = "";
        productDescription.Value = "";
        productPrice.Value = "";

    
        successLabel.Text = "Product added successfully!";
    }

    protected void UpdateProduct(object sender, EventArgs e)
    {
        // Get the product ID from the form field
        int productId;
        if (!int.TryParse(productId.Value, out productId))
        {
            // Handle invalid product ID input (optional)
            return;
        }

        // Find the product to update based on the product ID
        Product productToUpdate = productList.Find(p => p.Id == productId);

        if (productToUpdate == null)
        {
           //may have to handle error code 
            return;
        }

        // Update product details from form fields
        productToUpdate.Name = productName.Value;
        productToUpdate.Description = productDescription.Value;
        decimal price;
        if (decimal.TryParse(productPrice.Value, out price))
        {
            productToUpdate.Price = price;
        }
        
        // Optionally, save the updated product data to a database or file for persistence

        successLabel.Text = "Product updated successfully!";
    }

    private List<Product> LoadProductData()
    {
        
        return new List<Product>
        {
            new Product { Id = 1, Name = "Product 1", Description = "Description 1", Price = 10.99m },
            new Product { Id = 2, Name = "Product 2", Description = "Description 2", Price = 19.99m },
            // Add more products as needed
        };
    }
}
