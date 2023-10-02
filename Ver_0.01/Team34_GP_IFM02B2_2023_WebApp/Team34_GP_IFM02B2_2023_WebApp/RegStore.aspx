<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegStore.aspx.cs" MasterPageFile="~/Site1.Master" Inherits="Team34_GP_IFM02B2_2023_WebApp.RegStore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content> 

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


  <div class="container">
	
	<div class="signup">
    <span class="signup">
	<p>I am a <br>
	<label><a href="/Register.aspx">Buyer</a></label> 
	<label><a href="/RegStore.aspx">Seller</a></label> 
	</span>
	</div>
    <div class="registration form">
      <header>Signup</header>
      <form id ="regForm" runat="server">
        <input type="text" id="uEmail" runat="server" placeholder="Enter your email">
        <input type="text" id="cName" runat="server" placeholder="Enter your Company Name">
	    <input type="text" id="sName" runat="server" placeholder="Enter your Branch Name">
        <input type="text" id="sLoc" runat="server" placeholder="Enter your Branch Location">
        <input type="text" id="sType" runat="server" placeholder="Enter your Store Type">
        <input type="password" id="uPass" runat="server" placeholder="Create a password">
        <input type="password" id="ucPass" runat="server" placeholder="Confirm your password">
        <asp:Button id="regButt" runat="server" CssClass="button" Text="Register" OnClick="logReg_Click" />
        <asp:Label id="regStat" runat="server" Text = "SUCCESS" CssClass="status"/>
      </form>
      <div class="signup">
        <span class="signup">Already have an account?
          <label><a href="/login.aspx">Login</a></label>
        </span>
      </div>
    </div>
   </div>

</asp:Content>
