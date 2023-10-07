<%@ Page Title="AminRegistration" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RegAdmin.aspx.cs" Inherits="Team34_GP_IFM02B2_2023_WebApp.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style> 
        body {
            background-color: white;/* Replace 'your-background-image.jpg' with your image file path */
            background-size: cover;
            background-repeat: no-repeat;
            background-attachment: fixed;
            font-family: Arial, sans-serif;
            margin: 0;
        }

        .container
        {
            
            text-align: center;
            font-family: 'Times New Roman', Times, serif;
            color: black;
            border-radius:20px;
            border: 2px solid seagreen;
            
        }
        .container img 
        {

        }
        a{
            color: black;
        }
        input[type="text"],
        input[type="password"],
        input[type="date"] {
            width: 100%;
            padding: 10px;
            margin-bottom: 10px;
            border: none;
            border-radius: 20px; /* Adjust the value for the desired roundness */
            font-size: 16px;
            background-color: rgba(46, 139, 87, 0.3); /* Opacity set to 50% */
            text-align: center;
        }

        /* Style the registration button with rounded corners */
        .button {
            background-color: rgb(46, 139, 87);
            color: #fff;
            font-weight: bold;
            border: none;
            border-radius: 20px; /* Adjust the value for the desired roundness */
            cursor: pointer;
            width: 100%;
        }
        .rounded-image {
            border-radius: 50%;
            width: 200px; /* Set the desired width for the image */
            height: 200px; /* Set the desired height for the image */
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container">
	
	<div class="signup">
    <span class="signup">
	<h2>Hello Admin Signup Here </h2><br>
        <img class="rounded-image" src="/assets/img/regbackground.png" alt="User Rounded Image">
        <p>Not an Admin? Click below if you are either a Buyer or Seller</p>
	<p>I am a <label><a href="/Register.aspx">Buyer</a></label> / an <label><a href ="/RegStore.aspx">Seller</a></label> </p> <br> 
	
	</span>
	</div>
    <div class="registration form">
      
      <form id ="regForm" runat="server">
        <input type="text" id="uEmail" runat="server" placeholder="Enter your email">
        <input type="password" id="uPass" runat="server" placeholder="Create a password">
        <input type="password" id="ucPass" runat="server" placeholder="Confirm your password">
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
</asp:Content>
