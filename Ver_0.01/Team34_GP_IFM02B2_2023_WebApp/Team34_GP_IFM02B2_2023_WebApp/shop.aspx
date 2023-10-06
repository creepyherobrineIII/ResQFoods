<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="shop.aspx.cs" MasterPageFile="~/Site1.Master" Inherits="Team34_GP_IFM02B2_2023_WebApp.shop" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content> 

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
                    <form action="">
                    <div class="input-group">
                        <input type="text" class="form-control" placeholder="Search for products">
                        <div class="input-group-append">
                            <span class="input-group-text bg-transparent text-primary">
                                <i class="fa fa-search"></i>
                            </span>
                        </div>
                    </div>
                </form>
                </nav>
            </div>
        </div>
    </div>
    <!-- Navbar End -->


    <!-- Breadcrumb Start -->
    <div class="container-fluid">
        <div class="row px-xl-5">
            <div class="col-12">
                <nav class="breadcrumb bg-light mb-30">
                    <a class="breadcrumb-item text-dark" href="index.aspx">Home</a>
                    <a class="breadcrumb-item text-dark" href="shop.aspx">Shop</a>
                    <span class="breadcrumb-item active">Shop List</span>
                </nav>
            </div>
        </div>
    </div>
    <!-- Breadcrumb End -->


    <!-- Shop Start -->
    <div class="container-fluid">
        <div class="row px-xl-5">
            <!-- Shop Sidebar Start -->
            <div class="col-lg-3 col-md-4">
                <!-- Price Start -->
                <h5 class="section-title position-relative text-uppercase mb-3"><span class="bg-secondary pr-3">Filter by price</span></h5>
                <div class="bg-light p-4 mb-30">
                        <div class="custom-control custom-checkbox d-flex align-items-center justify-content-between mb-3">
                            <a href="shop.aspx?Filter=P&upper=-1&lower=-1"><label for="price-all">All Prices</label></a>
                        </div>
                        <div class="custom-control custom-checkbox d-flex align-items-center justify-content-between mb-3">
                            <a href="shop.aspx?Filter=P&upper=50&lower=0"><label  for="price-1">R0 - R50</label></a>
                        </div>
                        <div class="custom-control custom-checkbox d-flex align-items-center justify-content-between mb-3">
                            <a href="shop.aspx?Filter=P&upper=100&lower=50"><label  for="price-2">R50 - R100</label></a>
                        </div>
                        <div class="custom-control custom-checkbox d-flex align-items-center justify-content-between mb-3">
                            <a href="shop.aspx?Filter=P&upper=150&lower=100"><label  for="price-3">R100 - R150</label></a>
                        </div>
                        <div class="custom-control custom-checkbox d-flex align-items-center justify-content-between mb-3">
                            <a href="shop.aspx?Filter=P&upper=200&lower=-150"><label  for="price-4">R150 - R200</label></a>
                        </div>
                        <div class="custom-control custom-checkbox d-flex align-items-center justify-content-between">
                            <a href="shop.aspx?Filter=P&upper=99999999&lower=200"><label  for="price-5">R200+</label></a>
                        </div>
                </div>
                <!-- Price End -->
               

                <!-- Shop Start -->
                <!-- Added shops by alphabetical order, thus the ids also follow alphabetical order -->
                <h5 class="section-title position-relative text-uppercase mb-3" id="shopHead" runat="server" visible="true"><span class="bg-secondary pr-3">Filter by shop</span></h5>
                <div class="bg-light p-4 mb-30" id ="stBlock" runat="server"  visible="true">

                </div>
                <!-- Shops End -->
            </div>
            <!-- Shop Sidebar End -->


            <!-- Shop Product Start -->
            <div class="col-lg-9 col-md-8">
                <div class="col-12 pb-1">
                        <div class="d-flex align-items-center justify-content-between mb-4">
                            <div class="ml-2">
                                <div class="btn-group">
                                    <button type="button" class="btn btn-sm btn-light dropdown-toggle" data-toggle="dropdown">Sorting</button>
                                    <div class="dropdown-menu dropdown-menu-right">
                                        <a class="dropdown-item" href="shop.aspx?Order=cO">Chronological (old-new)</a>
                                        <a class="dropdown-item" href="shop.aspx?Order=cN">Chronological (new-old)</a>
                                        <a class="dropdown-item" href="shop.aspx?Order=pL">Price (low-high)</a>
                                        <a class="dropdown-item" href="shop.aspx?Order=pH">Price (high-low)</a>
                                        <a class="dropdown-item" href="shop.aspx?Order=nA">Name (a-z)</a>
                                        <a class="dropdown-item" href="shop.aspx?Order=nZ">Name (z-a)</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                <div class="row pb-3" id="prodSpace" runat="server">
                    
                 </div>
            </div>
            <!-- Shop Product End -->
        </div>
    </div>
    <!-- Shop End -->

    <!-- Back to Top -->
    <a href="#" class="btn btn-primary back-to-top"><i class="fa fa-angle-double-up"></i></a>



</asp:Content>
