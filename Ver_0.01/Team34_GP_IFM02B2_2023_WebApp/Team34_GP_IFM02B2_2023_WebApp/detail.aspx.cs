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
        private List<Product> productList = new List<Product>();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Load the product data from a database or other data source
            productList = LoadProductData();
        }
    }

    protected void SaveProduct(object sender, EventArgs e)
    {
        // Get the product ID from the form field
        int productId;
        if (!int.TryParse(productId.Value, out productId))
        {
            // ???handling of product invalid id (optional)
            return;
        }

        // Check if the product ID is valid for editing (non-negative ID indicates a new product)
        if (productId >= 0)
        {
            // Find the product to update based on the product ID
            Product productToUpdate = productList.Find(p => p.Id == productId);

            if (productToUpdate == null)
            {
                // Handle product not found (optional)
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

            // Provide user feedback (e.g., display a success message)
            successLabel.Text = "Product updated successfully!";
        }
        else
        {
            // Adding a new product
            Product newProduct = new Product
            {
                Id = GenerateNewProductId(),
                Name = productName.Value,
                Description = productDescription.Value,
                Price = Convert.ToDecimal(productPrice.Value)
            };

            productList.Add(newProduct);

            // Optionally, save the new product data to a database or file for persistence(maybe?)

            //feedback to manager
            successLabel.Text = "Product added successfully!";
        }
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

    private int GenerateNewProductId()
    {
        
        return productList.Count + 1; //increment the count of existing products
    }
 }    
}
