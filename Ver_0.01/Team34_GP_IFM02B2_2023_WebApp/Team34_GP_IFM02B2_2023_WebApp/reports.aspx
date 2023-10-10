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
                <a class="nav-link" href="#tabs-1-2" role="tab" data-toggle="tab" id="productstotal" runat="server">Total Products Sold and Product Quanities</a>
            </li>

            <li class="nav-item">
                <a class="nav-link" href="#tabs-1-3" role="tab" data-toggle="tab" id="bestsellingshop" runat="server">Best Selling Shop</a>
            </li>

            <li class="nav-item">
                <a class="nav-link" href="#tabs-1-4" role="tab" data-toggle="tab" id="sTypes" runat="server">Best Selling Product Category</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="#tabs-1-5" role="tab" data-toggle="tab" id="users" runat="server">Total Users Registered</a>
            </li>
        </ul>

        <div class="tab-content">

            <div role="tabpanel" class="tab-pane fade show active" id="tabs-1-1">
                <div class="nk-gap"></div>
                <!-- Total amount of sales -->
                <div>
                    <div class="nk-gap-1"></div>
                    <br />
                    <table>
                        <tr>
                            <td>
                                <div class="nk-post-title h4 ml-3" style="text-align: left">Total Sales: </div>
                            </td>
                            <td>
                                <div class="nk-post-title h5 ml-2 mt-2" id="totalSales" runat="server">
                                    <!--Dynamic loading-->
                                </div>
                                <div class="nk-gap"></div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>

            <div role="tabpanel" class="tab-pane fade" id="tabs-1-2">
                <div class="nk-gap"></div>

                <!-- Total Products -->
                <div>
                    <div class="nk-gap-1"></div>
                    <br />

                    <h2 class="nk-post-title h4 ml-3">Total Product Stock and Sold: </h2>

                    <div class="nk-post-text">
                        <div class="container-fluid">
                            <div class="row px-xl-1">
                                <div class="col-lg-10 table-responsive mb-5">
                                    <table class="table table-light table-borderless table-hover text-center mb-0">
                                        <thead class="thead-dark">
                                            <tr>
                                                <th>ID</th>
                                                <th>Name</th>
                                                <th>Product Tags</th>
                                                <th>Stock Left</th>
                                                <th>Total Sold</th>
                                            </tr>
                                        </thead>
                                        <tbody class="align-middle" id="totals" runat="server">
                                            <!--Dynamic Product Display-->
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="nk-gap"></div>
            </div>

            <!-- Best Selling Store-->
            <div role="tabpanel" class="tab-pane fade" id="tabs-1-3">
                <div class="nk-gap"></div>
                <div>
                    <div class="nk-gap-1"></div>
                    <br />
                    <table>
                        <tr>
                            <td>
                                <h2 class="nk-post-title h4 ml-3">Best Selling Shop: </h2>
                                <select class="h5 ml-3" name="store_type" id="Select1" runat="server" placeholder="Choose Store Type">
                                    <option value="grocery">Grocery</option>
                                    <option value="bakery">Bakery</option>
                                    <option value="restaurant">Restaurant</option>
                                    <option value="misc">Misc</option>
                                    <option value="overall">Overall</option>
                                </select>
                            </td>
                            <td>
                                <div class="nk-post-title h5 ml-2 mt-2" id="beststore" runat="server">
                                    <!--Dynamic loading-->
                                </div>
                                <div class="nk-gap"></div>
                            </td>
                        </tr>
                    </table>

                </div>
                <div class="nk-gap"></div>
            </div>

            <!-- Best Selliing Product Category NOT DONE-->
             <div role="tabpanel" class="tab-pane fade" id="tabs-1-4">
                <div class="nk-gap"></div>
                <div>
                    <div class="nk-gap-1"></div>
                    <br />
                    <table>
                        <tr>
                            <td>
                                <h2 class="nk-post-title h4 ml-3">Best Selling Category: </h2>
                                <select class="h5 ml-3" id="categories" runat="server" placeholder="Choose Store Type">

                                </select>
                            </td>
                            <td>
                                <div class="nk-post-title h5 ml-2 mt-2" id="catdisplay" runat="server">
                                    <!--Dynamic loading-->
                                </div>
                                <div class="nk-gap"></div>
                            </td>
                        </tr>
                    </table>

                </div>
                <div class="nk-gap"></div>
            </div>

            <!-- Total Users Registered-->
            <div role="tabpanel" class="tab-pane fade" id="tabs-1-5">
                <div class="nk-gap"></div>
                <br />
                <h2 class="nk-post-title h4 ml-3">Number of Users Registered: </h2>
                <div class="nk-blog-post nk-blog-post-border-bottom">
                    <div class="col-md-6">
                        <div class="nk-product-cat" runat="server" id="Div2">

                            <div class="row vertical-gap">
                                <div class="col-lg-6">
                                    <form runat="server">
                                        <div class="row vertical-gap">
                                            <div class="col-sm-6">
                                                <label for="sDate3">Start Date: <span class="text-main-1"></span></label>
                                                <input id="bDate" type="date" runat="server">
                                            </div>
                                        </div>
                                        <div class="nk-gap-2"></div>

                                        <br />

                                        <asp:Button class="nk-btn nk-btn-rounded nk-btn-color-main-1" ID="btnUsers" runat="server" Text="Generate Num Users" OnClick="btnUsers_Click"></asp:Button>

                                        <label runat="server" id="usersTotal">Total Users: N/A<span class="text-main-1"></span></label>

                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="nk-gap">
                </div>

            </div>
        </div>
    </div>
</asp:Content>
