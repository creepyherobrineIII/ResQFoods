<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Team34_GP_IFM02B2_2023_WebApp.index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <div class="col-lg-8 col-6 text-left">
            <div class="nav-item dropdown">
                <!--<a href="#" class="nav-link dropdown-toggle" data-toggle="dropdown">Account</i></a>
                        <div class="dropdown-menu bg-primary rounded-0 border-0 m-0">
                            <a href="cart.html" class="dropdown-item">Shopping Cart</a>
                            <a href="checkout.html" class="dropdown-item">Checkout</a>
                        </div>
                <div class="navbar-nav ml-auto py-0 d-none d-lg-block">
                            <a href="" class="btn px-0">
                                 <i class="fas fa-heart text-primary"></i>
                            </a>
                            <a href="" class="btn px-0 ml-3">
                                <i class="fas fa-shopping-cart text-primary"></i>
                                <span class="badge circle-color border text-black-50 rounded-circle " style="padding-bottom: 4px;">0</span>
                            </a>
                </div> -->


            </div>
        </div>
    <!-- Topbar End -->


    <!-- Navbar Start -->
    <div class="container-fluid bg-dark mb-30">
        <div class="row px-xl-5">
            <div class="col-lg-3 d-none d-lg-block">
                <a class="btn d-flex align-items-center justify-content-between bg-primary w-100" data-toggle="collapse" href="#navbar-vertical" style="height: 65px; padding: 0 30px;">
                    <h6 class="text-light m-0"><i class="fa fa-bars mr-2"></i>Categories</h6>
                    <i class="fa fa-angle-down text-dark"></i>
                </a>
                <nav class="collapse position-absolute navbar navbar-vertical navbar-light align-items-start p-0 bg-light" id="navbar-vertical" style="width: calc(100% - 30px); z-index: 999;">
                    <div class="navbar-nav w-100" id="pTags" runat="server">
                      
                    </div>
                </nav>
            </div>
               <div class="col-lg-9">
                <nav class="navbar navbar-expand-lg bg-dark navbar-dark py-3 py-lg-0 px-0">
                    <button type="button" class="navbar-toggler" data-toggle="collapse" data-target="#navbarCollapse">
                        <span class="navbar-toggler-icon"></span>
                    </button>
                    <div class="collapse navbar-collapse justify-content-between" id="navbarCollapse">
                    </div>
                    <form id ="serachForm" runat="server">
                    <div class="input-group">
                        <input type="text" class="form-control" id = "txtSearch" runat="server" placeholder="Search for products">
                             <asp:Button ID="btnSearch" class="btn btn-primary" runat="server" Text="Search" OnClick="btnSearch_Click"></asp:Button>
                    </div>
                </form>
                </nav>
            </div>
        </div>
    </div>
    <!-- Navbar End -->


    <!-- Carousel Start -->
    <div class="container-fluid mb-3">
        <div class="row px-xl-5">
            <div class="col-lg-8">
                <div id="header-carousel" class="carousel slide carousel-fade mb-30 mb-lg-0" data-ride="carousel">
                    <ol class="carousel-indicators">
                        <li data-target="#header-carousel" data-slide-to="0" class="active"></li>
                        <li data-target="#header-carousel" data-slide-to="1"></li>
                    </ol>
                    <div class="carousel-inner">
                        <div class="carousel-item position-relative active" style="height: 430px;">
                            <img class="position-absolute w-100 h-100" src="assets/img/carousel-1.jpg" style="object-fit: cover;">
                            <div class="carousel-caption d-flex flex-column align-items-center justify-content-center">
                                <div class="p-3" style="max-width: 700px;">
                                    <h1 class="display-4 text-white mb-3 animate__animated animate__fadeInDown"></h1>
                                    <p class="mx-md-5 px-5 animate__animated animate__bounceIn">Baked Goods</p>
                                    <a class="btn btn-outline-light py-2 px-4 mt-3 animate__animated animate__fadeInUp" href="shop.aspx?Filter=T&TID=2">Shop Now</a>
                                </div>
                            </div>
                        </div>
                        <div class="carousel-item position-relative" style="height: 430px;">
                            <img class="position-absolute w-100 h-100" src="assets/img/carousel-2.jpg" style="object-fit: cover;">
                            <div class="carousel-caption d-flex flex-column align-items-center justify-content-center">
                                <div class="p-3" style="max-width: 700px;">
                                    <h1 class="display-4 text-white mb-3 animate__animated animate__fadeInDown"></h1>
                                    <p class="mx-md-5 px-5 animate__animated animate__bounceIn">Meals</p>
                                    <a class="btn btn-outline-light py-2 px-4 mt-3 animate__animated animate__fadeInUp" href="shop.aspx?Filter=T&TID=5">Shop Now</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-4">
                <div class="product-offer mb-30" style="height: 200px;">
                    <img class="img-fluid" src="assets/img/sustainability.jpg" alt="">
                </div>
                <a href="shop.aspx"><div class="product-offer mb-30" style="height: 200px;">
                    <img class="img-fluid" src="assets/img/offer-2.jpg" alt="">

                </div></a>
            </div>
        </div>
    </div>
    <!-- Carousel End -->

    <!-- Products Start -->
    <div class="container-fluid pt-5 pb-3">
        <h2 class="section-title position-relative text-uppercase mx-xl-5 mb-4"><span class="bg-secondary pr-3">Shop from one of our partner stores</span></h2>
        <div class="row px-xl-5" id ="featStores" runat="server">
           
        </div>
    </div>
          
    <!-- Products End -->


    <!-- Products Start -->
    <div class="container-fluid pt-5 pb-3">
        <h2 class="section-title position-relative text-uppercase mx-xl-5 mb-4"><span class="bg-secondary pr-3">Recent Products</span></h2>
        <div class="row px-xl-5" id="featProd" runat="server">
            
            
        </div>
        </div>

    <!-- Products End -->

       <!-- Vendor Start -->
    <div class="container-fluid py-5">
        <div class="row px-xl-5">
            <div class="col">
                <div class="owl-carousel vendor-carousel" id ="stCarousel" runat="server">
                    
                </div>
            </div>
        </div>
    </div>
    <!-- Vendor End -->
   
</asp:Content>
