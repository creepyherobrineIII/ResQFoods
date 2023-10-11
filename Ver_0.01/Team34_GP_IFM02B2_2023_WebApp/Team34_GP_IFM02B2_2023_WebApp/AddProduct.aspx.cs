using System;
using System.Collections.Generic;
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
    public partial class AddProduct : System.Web.UI.Page
    {
        RESQSERVICEClient serviceClient = new RESQSERVICEClient();
        int storeId = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                UserTable st = (UserTable)Session["user"];
                storeId = st.UserId;
            }
            List <Tag> tList = new List<Tag>(serviceClient.getTags());
            String tagVal = "";
            foreach(Tag t in tList)
            {
                tagVal+= "<option value='"+t.TagID+"'>"+t.TagName+"</option>";
            }
            cType.InnerHtml = tagVal;
        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {

            // Handle adding a new product to the shop
            // Retrieve input values from form controls
            string productName = txtProductName.Value;
            string productDescription = txtProductDescription.Value;
            double prc = Convert.ToDouble(txtProductPrice.Value);
            int quantity = Convert.ToInt32(txtProductQuantity.Value);

            // Get other input values

            // Perform validation, data processing, and add the product to the shop using the WCF service
            try
            {

                // Replace the following placeholders with actual values
                if (FileUpload1.HasFile && storeId != -1)
                {
                    String imgPath = Server.MapPath("~/assets/img/");
                    String fPath = imgPath + FileUpload1.FileName;
                    if (!File.Exists(fPath))
                    {
                        FileUpload1.PostedFile.SaveAs(fPath);
                    }
                    string picturePath = "/assets/img/" + FileUpload1.FileName;
                    DateTime dateAdded = DateTime.Now;

                    bool success = serviceClient.AddProduct(storeId, productName, productDescription, quantity, prc, picturePath, dateAdded, true);

                    if (success)
                    {
                        // Product added successfully, you can also show a success message if needed
                        Response.Redirect("shop.aspx"); // Redirect to the shop page after adding the product
                    }
                    else
                    {
                        //lblMessage. = "You were not able to successfully add the product";
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
