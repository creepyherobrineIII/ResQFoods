<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Team34_GP_IFM02B2_2023_WebApp.WebForm1" %>

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
	
	<div class="signup">
    <span class="signup">
	<p>I am a <br>
	<p>I am a <br>
	<label><a href="/Register.aspx">Buyer</a></label> 
	<label><a href="/RegStore.aspx">Seller</a></label> 
	</span>
	</div>
    <div class="registration form">
      <header>Signup</header>
      <form id ="regForm" runat="server">
	  <input type="text" id="fName" runat="server" placeholder="Enter your First Name">
	  <input type="text" id="lName" runat="server" placeholder="Enter your Last Name">
        <input type="text" id="uEmail" runat="server" placeholder="Enter your email">
        <input type="password" id="uPass" runat="server" placeholder="Create a password">
        <input type="password" id="ucPass" runat="server" placeholder="Confirm your password">
        <input type="date"  id="bDate" runat="server">
        <asp:Button id="regButt" runat="server" CssClass="button" Text="Register" OnClick="logReg_Click" />
        <asp:Label id="regStat" runat="server" Text = "SUCCESS" CssClass="warning"/>
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
