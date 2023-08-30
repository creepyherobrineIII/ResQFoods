<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Team34_GP_IFM02B2_2023_WebApp.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lbl1" runat="server">Email address</asp:Label>
            <asp:TextBox ID="EmailAddress" runat="server" ToolTip="Enter Email Address:"></asp:TextBox>

             <asp:Label ID="lbl2" runat="server">Password</asp:Label>
            <asp:TextBox ID="Password" runat="server" ToolTip="Enter Password:"></asp:TextBox>
        </div>
        <p>
            <asp:Button ID="LoginButton" runat="server" Text="Submit" OnClick="LoginButton_Click" />
        </p>
        <br />

    </form>
    <asp:Label ID="userInput" runat="server"></asp:Label>
</body>
</html>
