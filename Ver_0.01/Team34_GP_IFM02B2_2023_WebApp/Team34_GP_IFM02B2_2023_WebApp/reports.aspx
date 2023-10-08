<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="reports.aspx.cs" Inherits="Team34_GP_IFM02B2_2023_WebApp.reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="nk-gap-3"></div>
    <br />
    <h3 class="nk-decorated-h-2" style="text-align: center"><span><span class="text-main-1">Reports</span>:</span></h3>
    <div class="nk-gap"></div>
    <div class="nk-tabs">

        <ul class="nav nav-tabs nav-tabs-fill" role="tablist">
            <li class="nav-item">
                <a class="nav-link active" href="#tabs-1-1" role="tab" data-toggle="tab">Total Amount of Sales</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="#tabs-1-2" role="tab" data-toggle="tab">Total Revenue Made</a>
            </li>

            <li class="nav-item">
                <a class="nav-link" href="#tabs-1-5" role="tab" data-toggle="tab">Total Products Sold</a>
            </li>

            <li class="nav-item">
                <a class="nav-link" href="#tabs-1-8" role="tab" data-toggle="tab">Total Users Registered</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="#tabs-1-3" role="tab" data-toggle="tab">Best Selling Shop</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="#tabs-1-4" role="tab" data-toggle="tab">Best Selling Product Category</a>
            </li>
        </ul>

        <div class="tab-content">
            <div role="tabpanel" class="tab-pane fade show active" id="tabs-1-1">

                <div class="nk-gap"></div>
                <!-- Total amount of sales -->
                <div class="nk-blog-post nk-blog-post-border-bottom">

                    <div class="nk-gap-1"></div>
                    <br />
                    <h2 class="nk-post-title h4" style="text-align: left">Total Sales: </h2>

                    <div class="nk-post-text" id="totalSales" runat="server">
                        <!--Dynamic loading-->
                    </div>
                </div>
               
            </div>     
            </div>
               
    </div>
</asp:Content>
