<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" MasterPageFile="~/Site1.Master" Inherits="Team34_GP_IFM02B2_2023_WebApp.AddProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Add Product</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <form id="form1" runat="server">
        <div class="container mt-5">
            <h2>Add Product</h2>
            <!-- Product Name -->
            <div class="form-group">
                <label for="txtProductName">Product Name:</label>
                <input type="text" id="txtProductName" runat="server" class="form-control" />
            </div>
            
            <!-- Product Description -->
            <div class="form-group">
                <label for="txtProductDescription">Product Description:</label>
                <textarea id="txtProductDescription" runat="server" class="form-control" rows="4"></textarea>
            </div>
            
            <!-- Product Price -->
            <div class="form-group">
                <label for="txtProductPrice">Product Price:</label>
                <input type="number" id="txtProductPrice" runat="server" class="form-control" />
            </div>
            
            <!-- Product Quantity -->
            <div class="form-group">
                <label for="txtProductQuantity">Product Quantity:</label>
                <input type="number" id="txtProductQuantity" runat="server" class="form-control" />
            </div>

            <!-- Product Image Upload -->
            <div class="form-group">
                <label for="fileProductImage">Product Image:</label>
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </div>


            <div class="form-group" id="catType">
            <select name="store_type"  id="cType" runat="server" placeholder="Choose Product Category">
            </select>
              </div>
            
            <!-- Add Product Button -->
            <div class="form-group">
                <asp:Button ID="btnAddProduct" runat="server" Text="Add Product" OnClick="btnAddProduct_Click" CssClass="btn btn-primary" />
            </div>
        </div>
    </form>
</asp:Content>

