<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="sales_items.aspx.cs" Inherits="Alex.pages.sales_items" %>

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
        <a href="../settings.aspx">
            <asp:Label runat="server" Text="&nbsp;Back to Settings" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"></asp:Label>
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
                                <label>Item Name</label><span class="required">*</span>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="tbItemName" runat="server" placeholder="Item Name" ValidationGroup="SalesSetup" CssClass="form-control"></asp:TextBox>
                                <div class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvtbItemName" ErrorMessage="Item Required" ForeColor="Red" ValidationGroup="SalesSetup" ControlToValidate="tbItemName"
                                        runat="server" Dispaly="Dynamic" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="editor-label">
                                <label>Item Price</label><span class="required">*</span>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="tbPrice" runat="server" placeholder="Enter Price"  ValidationGroup="SalesSetup" CssClass="form-control"></asp:TextBox>
                                <div class="pull-right">
                                    <asp:RegularExpressionValidator ID="revtbPrice" ControlToValidate="tbPrice" runat="server" ValidationGroup="SalesSetup"
                                        ErrorMessage="Invalid Amount" ForeColor="Red" ValidationExpression="^(-)?\d+([,\.]\d{1,2})?$"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="rfvPrice" ErrorMessage="Amount required" ForeColor="Red" ControlToValidate="tbPrice" ValidationGroup="SalesSetup"
                                        runat="server" Dispaly="Dynamic" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="editor-label">
                                <label>Description</label><span class="required">*</span>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="tbDescription" runat="server" placeholder="Description" CssClass="form-control" ValidationGroup="SalesSetup"></asp:TextBox>
                                <div class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvtbDescription" ErrorMessage="Description Required" ValidationGroup="SalesSetup" ForeColor="Red" ControlToValidate="tbDescription"
                                        runat="server" Dispaly="Dynamic" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="editor-label">
                                <label>Status</label><span class="required">*</span>
                            </div>
                            <div class="editor-field">
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" ValidationGroup="SalesSetup">
                                    <asp:ListItem Text="Choose Status" Value=""></asp:ListItem>
                                    <asp:ListItem>Active</asp:ListItem>
                                    <asp:ListItem>Inactive</asp:ListItem>
                                </asp:DropDownList>

                                <div class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvddlStatus" ErrorMessage="Status Required" ValidationGroup="SalesSetup" ForeColor="Red" ControlToValidate="ddlStatus"
                                        runat="server" Dispaly="Dynamic" />
                                </div>
                            </div>
                        </div>

                    </div>
                    <asp:Button ID="BtnSaveItem" runat="server" Text="Save" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" ValidationGroup="SalesSetup" OnClick="BtnSaveItem_Click" />
                </div>
            </div>

        </div>
        <h1>List of Sale Items</h1>
        <asp:GridView ID="GridViewItem" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example"
            OnRowEditing="GridViewItem_RowEditing" OnRowCancelingEdit="GridViewItem_RowCancelingEdit"
            OnRowUpdating="GridViewItem_RowUpdating" OnRowDeleting="GridViewItem_RowDeleting">
            <Columns>
                <asp:BoundField DataField="msi_id" HeaderText="MSI ID" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>

                <asp:TemplateField HeaderText="Item Name">
                    <EditItemTemplate>
                        <asp:TextBox ID="tbItem" runat="server" Text='<%# Bind("item_name") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblItem" runat="server" Text='<%# Bind("item_name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description">
                    <EditItemTemplate>
                        <asp:TextBox ID="tbEditDescription" runat="server" Text='<%# Bind("description") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="price" HeaderText="Price" ReadOnly="true"/>
                <asp:TemplateField HeaderText="status ">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlEditStatus" runat="server" Text='<%# Bind("status") %>'>
                            <asp:ListItem>Active</asp:ListItem>
                            <asp:ListItem>Inactive</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("status") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false" HeaderText="Edit">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="Edit" CausesValidation="False" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="lnkUpdate" runat="server" Text="Update" CommandName="Update" CausesValidation="False" />
                        <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel" CommandName="Cancel" CausesValidation="False" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete ?');" Text="Delete"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Price">
                    <ItemTemplate>
                        <asp:Button ID="btnSetPrice" runat="server" CssClass=" btn-sm btn-primary m-t-n-xs" Text='Set/View Price' OnClick="btnSetPrice_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <%--  --Pages Styles--%>
    <style>
        .required {
            color: #F00;
        }
    </style>
</asp:Content>
