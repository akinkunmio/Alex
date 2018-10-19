<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="setup_expenses_category.aspx.cs" Inherits="Alex.pages.admin.setup_expenses_category" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<link href="../../scripts/css/pagestyle.css" rel="stylesheet" />
  <div class="col-md-3 pull-right">
            <a href="../settings.aspx">
               <asp:Label runat="server" Text="&nbsp;Back to Settings" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"  ></asp:Label>
            </a>
        </div>
    <h2>Add Expenses Category</h2>
    <div class=" wrapper-content  animated fadeInRight">
        <div class="row">
            <div class="col-lg-6">
                <div class="ibox">
                    <div role="form" id="form">
                        <div class="form-group">
                            <div class="editor-label">
                                <label>Category</label><span class="required">*</span>
                            </div>
                            <div class="editor-field">
                                 <asp:TextBox ID="tbCategory" runat="server" placeholder="Enter Expenses Category" ValidationGroup="ExpensesSetup" CssClass="form-control"></asp:TextBox>
                                <div class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvtbCategory" ErrorMessage="Expenses Category Required" ForeColor="Red" ValidationGroup="ExpensesSetup" ControlToValidate="tbCategory"
                                        runat="server" Dispaly="Dynamic" />
                                </div>
                            </div>
                        </div>
                  
                     </div>
                    <asp:Button ID="BtnSaveCategory" runat="server" Text="Save" CssClass="btn btn-sm btn-primary m-t-n-xs" CausesValidation="true" ValidationGroup="ExpensesSetup" Font-Bold="True" OnClick="BtnSaveCategory_Click" />
                </div>
            </div>

        </div>
        <h1>List of Customized Expenses Category</h1>
        <asp:GridView ID="GridViewCategory" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example"
            OnRowEditing="GridViewCategory_RowEditing" OnRowCancelingEdit="GridViewCategory_RowCancelingEdit"
            OnRowUpdating="GridViewCategory_RowUpdating" OnRowDeleting="GridViewCategory_RowDeleting">
            <Columns>
                <asp:BoundField DataField="status_id" HeaderText="Status ID" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
          
              
               <asp:TemplateField HeaderText="Category">
                    <EditItemTemplate>
                         <asp:TextBox ID="tbCategory" runat="server" Text='<%# Bind("status_name") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblCategorye" runat="server" Text='<%# Bind("status_name") %>'></asp:Label>
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
