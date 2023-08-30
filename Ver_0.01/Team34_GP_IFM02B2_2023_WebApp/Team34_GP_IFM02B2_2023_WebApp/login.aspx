<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Team34_GP_IFM02B2_2023_WebApp.login" %>

<!DOCTYPE html>

<html lang="en">
<head>
  <title>Login & Registration Form | CoderGirl</title>
  <!---Custom CSS File--->
  <link rel="stylesheet" href="logStyle.css">
</head>
<body>
  <div class="container">
    <div class="login form">
      <header>Login</header>
      <form id ="logForm" runat="server">
        <input type="text" id = "logEmail" runat="server" placeholder="Enter your email">
        <input type="password" id ="logPass" runat="server" placeholder="Enter your password">
        <asp:Button id="logButt" runat="server"  onlick="logButt_Click" Text="Login" />
      </form>
      <div class="signup">
        <span class="signup">Don't have an account?
         <label>Signup</label>
        </span>
      </div>
    </div>
  </div>
</body>
</html>

