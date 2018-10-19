<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="price_items.aspx.cs" Inherits="Alex.pages.price_items" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../scripts/css/pagestyle.css" rel="stylesheet" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <style>
        .ui-datepicker {
            margin-top: 7px;
            margin-left: -30px;
            margin-bottom: 0px;
            position: absolute;
            z-index: 1000;
        }
    </style>
    <script type="text/javascript">
        $(function () { $('.datepick').datepicker({ changeMonth: true, changeYear: true, yearRange: '-100:' + new Date().getFullYear(), dateFormat: 'dd/mm/yy', maxDate: new Date() }); });
    </script>
      <div class="col-md-3 pull-right">
            <a href="../admin/sales_items.aspx">
               <asp:Label runat="server" Text="&nbsp;Back to Sale Items" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"  ></asp:Label>
            </a>
        </div>
    <h2>Sale Items</h2>
    <div class=" wrapper-content  animated fadeInRight">
        <div class="row">
            <div class="col-lg-6">
                <div class="ibox">
                    <div role="form" id="form">
                        <div class="form-group">
                            <div class="editor-label">
                                <label>Item Name</label>
                            </div> 
                            <div class="editor-field">
                                <h4><asp:Label runat="server" ID="lblItemName"></asp:Label></h4>
                               <%-- <asp:DropDownList ID="ddlSaleItem" runat="server" CssClass="form-control" ValidationGroup="PriceSetup"></asp:DropDownList>
                                <div class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvddlSaleItem" ErrorMessage="Sale Item Required" ValidationGroup="PriceSetup" ForeColor="Red" ControlToValidate="ddlSaleItem"
                                        runat="server" Dispaly="Dynamic" />
                                </div>--%>
                            </div>
                        </div>
                       <%-- <div class="form-group">
                            <div class="editor-label">
                                <label>Academic Year</label><span class="required">*</span>
                            </div>
                            <div class="editor-field">
                                <asp:DropDownList ID="ddlAcademicYear" runat="server" CssClass="form-control" ValidationGroup="PriceSetup" AutoPostBack="true" OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged"></asp:DropDownList>
                                <div class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvddlAcademicYear" ErrorMessage="Academic Year Required" ValidationGroup="PriceSetup" ForeColor="Red" ControlToValidate="ddlAcademicYear"
                                        runat="server" Dispaly="Dynamic" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="editor-label">
                                <label>Term</label><span class="required">*</span>
                            </div>
                            <div class="editor-field">
                                <asp:DropDownList ID="ddlPriceTerm" runat="server" CssClass="form-control" ValidationGroup="PriceSetup">
                                </asp:DropDownList>
                                <div class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvddlPriceTerm"  ErrorMessage="Term Required" ValidationGroup="PriceSetup" ForeColor="Red" ControlToValidate="ddlPriceTerm"
                                        runat="server" Dispaly="Dynamic" />
                                </div>
                            </div>
                        </div>--%>
                        <div class="form-group">
                            <div class="editor-label">
                                <label>Current Price</label>
                            </div>
                            <div class="editor-field">
                                 <h4><asp:Label runat="server" ID="lblItemPrice"></asp:Label></h4>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="editor-label">
                                <label>New Price</label><span class="required">*</span>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="tbPrice" runat="server" placeholder="Enter Price"  ValidationGroup="PriceSetup" CssClass="form-control"></asp:TextBox>
                               <div class="pull-right">
                                    <asp:RegularExpressionValidator ID="revtbPrice" ControlToValidate="tbPrice" runat="server" ValidationGroup="PriceSetup"
                                        ErrorMessage="Invalid Amount" ForeColor="Red" ValidationExpression="^(-)?\d+([,\.]\d{1,2})?$"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="rfvPrice" ErrorMessage="Amount required" ForeColor="Red" ControlToValidate="tbPrice" ValidationGroup="PriceSetup"
                                        runat="server" Dispaly="Dynamic" />
                                </div>
                            </div>
                        </div>

                    </div>
                    <asp:Button ID="BtnSavePrice" runat="server" Text="Save" CssClass="btn btn-sm btn-primary m-t-n-xs" ValidationGroup="PriceSetup" CausesValidation="true" Font-Bold="True" OnClick="BtnSavePrice_Click" />
                </div>
            </div>

        </div>
        <h1>List of Old Price List</h1>
        <asp:GridView ID="GridViewPriceList" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example">
           
            <Columns>
                <asp:BoundField DataField="msi_pri_id" HeaderText="Price ID" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="msi_id" HeaderText="MSI ID" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="Price ">
                    <EditItemTemplate>
                         <asp:TextBox ID="tbEditPrice" runat="server" Text='<%# Bind("price") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblPrice" runat="server" Text='<%# Bind("price") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="end_date" HeaderText="Price End Date"/>
         </Columns>
        </asp:GridView>
        <asp:Label ID="lblZeroRecords" runat="server" Text=""></asp:Label>
    </div>
    <%--  --Pages Styles--%>
    <style>
        .required {
            color: #F00;
        }
    </style>
</asp:Content>
