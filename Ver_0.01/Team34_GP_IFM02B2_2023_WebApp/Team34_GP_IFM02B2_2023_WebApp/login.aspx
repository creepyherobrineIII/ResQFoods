<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" MasterPageFile="~/Site1.Master" Inherits="Team34_GP_IFM02B2_2023_WebApp.login" %>

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
            width: 70;
            text-align: center;
            font-family: 'Times New Roman', Times, serif;
            color: black;
            border-radius:20px;
            border: 2px solid seagreen;
            
        }

        a{
            color: black;
        }
        input[type="text"],
        input[type="password"],
        input[type="date"] {
            width: 70%;
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
            width: 70%;
        }
            </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="container">
    <div class="login form">
      <h1>Hello User <br > LOGIN HERE </h1>
      <form id ="logForm" runat="server">
        <input type="text" id = "logEmail" runat="server" placeholder="Enter your email">
        <input type="password" id ="logPass" runat="server" placeholder="Enter your password">
        <asp:Label id="noLog" runat="server" Text = "INVALID CREDENTIALS, PLEASE RE-ENTER" CssClass="warning"/>
        <asp:Button id="logButt" runat="server"  onlick="log_Click()" CssClass="button" Text="Login" OnClick="logButt_Click" />
       
      </form>
      <div class="signup" runat="server">
        <span class="signup">Don't have an account?
         <label><a href="/Register.aspx">Signup</a></label>
        </span>
      </div>
    </div>
  </div>

</asp:Content>
