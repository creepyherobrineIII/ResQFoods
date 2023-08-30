<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" Inherits="Team34_GP_IFM02B2_2023_WebApp.AddProduct" %>

<!DOCTYPE html>

<html lang="en">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta http-equiv="X-UA-Compatible" content="ie=edge">
  <title>Registration Form</title>
  <!---Custom CSS File--->
  <link rel="stylesheet" href="assets/css/logStyle.css">
</head>
<body>
  <div class="container">
    <div class="registration form">
      <header>Add a Product Form</header>
      <form id ="regForm" runat="server">
	  <input type="text" id="pName" runat="server" placeholder="Enter your Product Name">
	  <input type="text" id="pdesc" runat="server" placeholder="Enter short description">
        <input type="text" id="pPrice" runat="server" placeholder="Enter the price">
        <asp:Button id="addProd" runat="server" CssClass="button" Text="Add Product" OnClick="addProd_Click" />
        <asp:Label id="regStat" runat="server" Text = "SUCCESS" CssClass="status"/>
      </form>
      <div class="signup">
        <span class="signup">Already have an account?
          <label><a href="/login.aspx">Login</a></label>
        </span>
      </div>
    </div>
   </div>
</body>
</html>
