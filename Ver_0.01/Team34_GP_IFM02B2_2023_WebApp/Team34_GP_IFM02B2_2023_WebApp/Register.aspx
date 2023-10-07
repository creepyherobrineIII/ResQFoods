<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" MasterPageFile="~/Site1.Master" Inherits="Team34_GP_IFM02B2_2023_WebApp.WebForm1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style> 
        body {
            background-color: white;
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
        .buttonr{
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
        
        .h2{
                text-align: center;
           }
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	 <div class="container">
	
         <div class="signup">
             <div class="signup"> 
                 <br />
                     <h2>Create an Account</h2>                   
                     <img class="rounded-image" src="/assets/img/buyer.png" alt="User Rounded Image">
                     <p>Not a buyer? Click below if you are  a Seller</p>
                     <p>I am a
                         <label><a href="/RegStore.aspx">Seller</a></label>
                        <!-- / an
                         <label><a href="/RegAdmin.aspx">Admin</a></label>--> 
                     </p>
                 
             </div>
         </div>
         <div class="registration form">
      
      <form id ="regForm" runat="server">
	  <input type="text" id="fName" runat="server" placeholder="Enter your First Name">
	  <input type="text" id="lName" runat="server" placeholder="Enter your Last Name">
        <input type="text" id="uEmail" runat="server" placeholder="Enter your Email">
        <input type="password" id="uPass" runat="server" placeholder="Create a Password">
        <input type="password" id="ucPass" runat="server" placeholder="Confirm your Password">
          Please Enter Date of Birth:
        <input type="date"  id="bDate" runat="server">
          <br />
        <asp:Button id="regButt" runat="server" CssClass="buttonr" Text="Register" OnClick="logReg_Click" />
          <br /> <br />
       <b><asp:Label id="regStat" runat="server" Text = "" CssClass="warning"/></b> 
      </form>
      <div class="signup">
        <span class="signup">Already have an account?
          <label><a href="/login.aspx">Login</a></label>
        </span>
      </div>
    </div>
   </div>
</asp:Content>
