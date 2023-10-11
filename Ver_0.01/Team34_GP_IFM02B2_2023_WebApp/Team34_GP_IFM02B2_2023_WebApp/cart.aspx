<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cart.aspx.cs" MasterPageFile="~/Site1.Master" Inherits="Team34_GP_IFM02B2_2023_WebApp.cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <!-- Breadcrumb Start -->
    <div class="container-fluid">
        <div class="row px-xl-5">
            <div class="col-12">
                <nav class="breadcrumb bg-light mb-30">
                    <a class="breadcrumb-item text-dark" href="index.aspx">Home</a>
                    <a class="breadcrumb-item text-dark" href="shop.aspx">Shop</a>
                    <span class="breadcrumb-item active">Shopping Cart</span>
                </nav>
            </div>
        </div>
    </div>
    <!-- Breadcrumb End -->

        <!-- Cart Start -->
    <div class="container-fluid">
        <div class="row px-xl-5">
            <div class="col-lg-8 table-responsive mb-5">
                <table class="table table-light table-borderless table-hover text-center mb-0">
                    <thead class="thead-dark">
                        <tr>
                            <th>Products</th>
                            <th>Price</th>
                            <th>Quantity</th>
                            <th>Total</th>
                            <th>Remove</th>
                        </tr>
                    </thead>
                    <tbody class="align-middle" id="cartitem" runat="server"> <!--Dynamic Cart Display-->                   
                    </tbody>
                </table>
            </div>
            <div class="col-lg-4">
                <form class="mb-30" action="" runat="server">
                    <h5 class="section-title position-relative text-uppercase mb-3"><span class="bg-secondary pr-3">Cart Summary</span></h5>
                    <div class="bg-light p-30 mb-5" id="cartsum" runat="server"> <!--Dynamic Cart Summary Display-->
                    </div>
                    <div id="btnCheck" runat="server" visible="false">
                    <asp:Button id='checkout' runat='server' Text='Checkout' OnClick='btnCheckout_Click' class='btn btn-sm btn-primary'/>
                    </div>
                        </form>
               
            </div>
        </div>
    </div>
    <!-- Cart End -->

</asp:Content>
