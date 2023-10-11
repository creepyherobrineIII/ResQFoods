﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EditProduct.aspx.cs" Inherits="Team34_GP_IFM02B2_2023_WebApp.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title> Edit product </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <div class="container mt-5">
            <h2>Edit Product</h2>
            <!-- Product ID (Hidden Field) -->
            <input type="hidden" id="hiddenProductId" runat="server" />

            <!-- Product Name -->
            <div class="form-group">
                <label for="txtProductName">Product Name:</label>
              <!-- <input type="text" id="txtProductName" runat="server" class="form-control" /> -->
             <!--   <asp:TextBox id="txtProductName" runat="server" CssClass="form-control"></asp:TextBox> -->
            <!--   <input type="text" id="sName" runat="server" placeholder="Enter Your Store Name"> -->
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

            <!-- Product Image Upload (Optional) -->
            <div class="form-group">
                <label for="fileProductImage">Product Image (Optional):</label>
                <input type="file" id="fileProductImage" runat="server" accept="image/*" class="form-control-file" />
            </div>

            <!-- Update Product Button -->
            <div class="form-group">
                <asp:Button ID="btnUpdateProduct" runat="server" Text="Update Product" OnClick="btnUpdateProduct_Click" CssClass="btn btn-primary" />
            </div>

            <!-- Delete Product Button -->
            <div class="form-group">
                <asp:Button ID="btnDeleteProduct" runat="server" Text="Delete Product" OnClick="btnDeleteProduct_Click" CssClass="btn btn-danger" />
            </div>
        </div>
    </form>
</asp:Content>