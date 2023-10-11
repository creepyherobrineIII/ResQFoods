using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
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
                List<Tag> tList = new List<Tag>(sc.getTags());
                foreach (Tag t in tList)
                {
                    cType.Items.Add(t.TagName);
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
            Product product = sc.getProduct(productID);

            if (product != null)
            {
                // Populate the form fields with product details
                txtProductName.Value = product.Name;
                txtProductDescription.Value= product.Description;
                txtProductPrice.Value = product.Price.ToString();
                txtProductQuantity.Value = product.Quantity.ToString();
                cType.Value = sc.getTagName(sc.getProdTag(productID));
            }
            else
            {
                // Handle the case where the product is not found
                Response.Redirect("shop.aspx");
            }
        }

        protected void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            Product product = sc.getProduct(productID);
                Product P = new Product
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Description = product.Description,
                    Quantity = product.Quantity,
                    Price = product.Price,
                    UserId = product.UserId,
                    Enabled = false
                };
                bool isUpdated = sc.editProduct(P, -1);
        }

        protected void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            // Get the updated values from the form fields
            string newName = txtProductName.Value;
            string newDescription = txtProductDescription.Value;
            double newPrice = Convert.ToDouble(txtProductPrice.Value);
            int newQuantity = Convert.ToInt32(txtProductQuantity.Value);
            int tag = sc.searchTag(cType.Value);
            if (FileUpload1.HasFile && newName != "" && newDescription != "" && newPrice > 0)
            {
                String imgPath = Server.MapPath("~/assets/img/");
                String fPath = imgPath + FileUpload1.FileName;
                if (!File.Exists(fPath))
                {
                    FileUpload1.PostedFile.SaveAs(fPath);
                }
                string picturePath = "/assets/img/" + FileUpload1.FileName;
                Product P = new Product
                {
                    ProductId = productID,
                    Name = newName,
                    Description = newDescription,
                    Quantity = newQuantity,
                    Price = (decimal)newPrice,
                    Enabled = true,
                    Picture = picturePath
                };
                bool isUpdated = sc.editProduct(P, tag);
                Response.Redirect("details.aspx?ID="+P.ProductId);
            }

        }
    }
}