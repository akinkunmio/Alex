<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="setup_department.aspx.cs" Inherits="Alex.pages.admin.setup_department" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<link href="../../scripts/css/pagestyle.css" rel="stylesheet" />
       <div class="col-md-3 pull-right">
            <a href="../settings.aspx">
               <asp:Label runat="server" Text="&nbsp;Back to Settings" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"  ></asp:Label>
            </a>
        </div>
    <h2>Department Setup</h2>
    <div class=" wrapper-content  animated fadeInRight">
        <div class="row">
            <div class="col-lg-6">
                <div class="ibox">
                    <div role="form" id="form">
                        <div>
                            <div class="form-group">
                                <div class="editor-label">
                                    <label>Department name</label><span class="required">*</span>
                                </div>
                                 <div class="editor-field">
                                        <asp:TextBox ID="tbDepartment" runat="server" placeholder="Enter Department Name" ValidationGroup="Department" CssClass="form-control"></asp:TextBox>
                                        <div class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvtbDepartment" ErrorMessage="Department Name Required" ValidationGroup="Department" ForeColor="Red" ControlToValidate="tbDepartment"
                                            runat="server" Dispaly="Dynamic"/>
                                    </div>
                                 </div>
                             </div>
                                    
                             <asp:Button ID="BtnSaveDepartment" runat="server" Text="Save" CssClass="btn btn-sm btn-primary m-t-n-xs" ValidationGroup="Department" Font-Bold="True" OnClick="BtnSaveDepartment_Click" />
                            
                        </div>
                    </div>
                </div>
            </div>
        </div>
       <h1>List of Department Names</h1>
        <asp:GridView ID="GridViewDepartment" runat="server" AutoGenerateColumns="False"  CssClass="table table-striped table-bordered table-hover dataTables-example" 
            OnRowEditing="GridViewDepartment_RowEditing"
            OnRowCancelingEdit="GridViewDepartment_RowCancelingEdit"
            OnRowUpdating="GridViewDepartment_RowUpdating"
            OnRowDeleting="GridViewDepartment_RowDeleting">
            <Columns>
                 <asp:BoundField DataField="dept_id" HeaderText="Department Id" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                     <HeaderStyle CssClass="hidden"></HeaderStyle>
                     <ItemStyle CssClass="hidden"></ItemStyle> 
                 </asp:BoundField>
                <asp:TemplateField HeaderText="Department Name">
                    <EditItemTemplate>
                        <asp:TextBox ID="tbDepartment" runat="server" Text='<%# Bind("dept_name") %>'></asp:TextBox>
                         <div class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvtbDepartment" ErrorMessage="Department Required" ForeColor="Red" ControlToValidate="tbDepartment"
                                                runat="server" Dispaly="Dynamic" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDepartment" runat="server" Text='<%# Bind("dept_name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

               
                <asp:TemplateField HeaderText="Edit">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="Edit" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="lnkUpdate" runat="server" Text="Update" CommandName="Update"  />
                        <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel" CommandName="Cancel" CausesValidation="False" />
                    </EditItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                              <ItemTemplate>
                                  <asp:LinkButton ID="lnkDelete" runat="server"  CommandName="Delete" Text="Delete"  OnClientClick="return confirm('Are you sure you want to delete ?');" ></asp:LinkButton>
                              </ItemTemplate>
                        </asp:TemplateField>
           </Columns>
        </asp:GridView>
  </div>
<style>
.required {
color: #F00;
}
</style>
</asp:Content>
