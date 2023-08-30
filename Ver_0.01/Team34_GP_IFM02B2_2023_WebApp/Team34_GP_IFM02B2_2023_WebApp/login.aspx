<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="login.aspx.cs" Inherits="Team34_GP_IFM02B2_2023_WebApp.login" %>

<!DOCTYPE html>

<html lang="en">
<head>
  <title>Login User</title>
  <link rel="stylesheet" href="assets/css/logStyle.css">
</head>
<body>
  <div class="container">
    <div class="login form">
      <header>Login</header>
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
</body>
</html>

