<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Team34_GP_IFM02B2_2023_WebApp.WebForm1" %>

<!DOCTYPE html>

<html lang="en">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta http-equiv="X-UA-Compatible" content="ie=edge">
  <title>Login & Registration Form | CoderGirl</title>
  <!---Custom CSS File--->
  <link rel="stylesheet" href="style.css">
</head>
<body>
  <div class="container">
	
	<div class="signup">
    <span class="signup">
	<p>I am a <br>
	<input type="radio" name="userType" id="buy" hidden="true"></input>
	<label for="buy">Buyer</label> 
	<input type="radio" name="userType" id="sell" hidden="true"></input>
	<label for="sell">Seller</label> 
	<input type="radio" name="userType" id="admin" hidden="true"></input>
	<label for="admin">Admin</label>
	</span>
	</div>
    <div class="registration form">
      <header>Signup</header>
      <form action="#">
	  <input type="text" placeholder="Enter your First Name">
	  <input type="text" placeholder="Enter your Last Name">
        <input type="text" placeholder="Enter your email">
        <input type="password" placeholder="Create a password">
        <input type="password" placeholder="Confirm your password">
        <input type="button" class="button" value="Signup">
      </form>
      <div class="signup">
        <span class="signup">Already have an account?
         <label id ="lblLogin">Login</label>
        </span>
      </div>
    </div>
  </div>
</body>
</html>
