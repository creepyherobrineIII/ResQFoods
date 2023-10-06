<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="invoices.aspx.cs" Inherits="Team34_GP_IFM02B2_2023_WebApp.invoices" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="container">
    <div class="nk-store nk-store-checkout">
        <h3 class="nk-decorated-h-2"><span><span class="text-main-1">View</span> Invoices</span></h3>

        <!-- START: Billing Details -->
        <div class="nk-gap"></div>
        <div action="#" class="nk-form">
            <div class="row vertical-gap">
                <div class="col-lg-6">
                     <!--invoices inserted here-->
                    <div id="InvoiceDisplayArea" runat="server"></div>
                </div>
            </div>
        </div>
      

        <div class="nk-gap-2"></div>
        
    </div>
    </div>
        

<div class="nk-gap-2"></div>
</asp:Content>
