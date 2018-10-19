<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="manage_users.aspx.cs" Inherits="Alex.pages.admin.manage_users" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      
     <div class="col-md-4 pull-right">
            <a href="../settings.aspx">
               <asp:Label runat="server" Text="&nbsp;Back to Settings" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"  ></asp:Label>
            </a>
             <a href="../admin/setup_new_user.aspx">
               <asp:Label runat="server" Text="Create New User" CssClass="btn btn-sm btn-warning m-t-n-xs" Font-Bold="True"  ></asp:Label>
            </a>
        </div>
    <h2>List of Users</h2>
    <asp:SqlDataSource ID="SqlDataSourceLevelDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>" SelectCommand="sp_ms_level_of_access_dropdown" ></asp:SqlDataSource>
    <div class=" wrapper-content  animated fadeInRight">
      <asp:GridView ID="GridViewManageUsers" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example"
            OnRowEditing="GridViewManageUsers_RowEditing" 
            OnRowCancelingEdit="GridViewManageUsers_RowCancelingEdit1" 
            OnRowUpdating="GridViewManageUsers_RowUpdating" 
            OnRowDeleting="GridViewManageUsers_RowDeleting" OnRowDataBound="GridViewManageUsers_RowDataBound">

            <Columns>
                <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" ReadOnly="True" SortExpression="id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
             
                <asp:TemplateField HeaderText="First Name">
                    <EditItemTemplate>
                        <asp:TextBox ID="tbFirstName" runat="server" Text='<%# Bind("f_name") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvtbFirstName" ErrorMessage="First Name Required" ForeColor="Red" ControlToValidate="tbFirstName"
                                                runat="server" Dispaly="Dynamic" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblFirstName" runat="server" Text='<%# Bind("f_name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Last Name">
                    <EditItemTemplate>
                        <asp:TextBox ID="tbLastName" runat="server" Text='<%# Bind("l_name") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvtbLastName" ErrorMessage="Last Name Required" ForeColor="Red" ControlToValidate="tbLastName"
                                                runat="server" Dispaly="Dynamic" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblLastName" runat="server" Text='<%# Bind("l_name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Email">
                    <EditItemTemplate>
                        <asp:TextBox ID="tbEmail" runat="server" Text='<%# Bind("email_add") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvtbEmail" ErrorMessage="Email Required" ForeColor="Red" ControlToValidate="tbEmail"
                                                runat="server" Dispaly="Dynamic" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("email_add") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Level of Access">
                   <EditItemTemplate>
                        <asp:DropDownList ID = "ddlLevelOfAccess" runat="server"  DataValueField="status_name" DataTextField="status_name" Text= '<%# Bind("actual_level_of_access") %>' 
                            DataSourceID="SqlDataSourceLevelDropDown"></asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="ddlLevelOfAccess" runat="server" Text='<%# Bind("actual_level_of_access") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="last_active" HeaderText="LastTime Login" ReadOnly="true"  NullDisplayText="N/A"/>
                <asp:TemplateField HeaderText="Expiry Date">
                    <ItemTemplate>
                        <asp:Label ID="lblExDate" runat="server" Text='<%# Bind("PasswordExpiryDate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="PwdExpired" >
                   <ItemTemplate>
                        <asp:Label ID="lblPwdExp" runat="server" Text='<%# Bind("PwdExpired") %>'></asp:Label>
                    </ItemTemplate>
                      <HeaderStyle CssClass="hidden"></HeaderStyle>
                     <ItemStyle CssClass="hidden"></ItemStyle>
                 </asp:TemplateField>
              
               
                <asp:TemplateField HeaderText="Edit">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="Edit" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="lnkUpdate" runat="server" Text="Update" CommandName="Update" />
                        <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel" CommandName="Cancel" CausesValidation="false" />
                    </EditItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete ?');" Text="Delete"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Access">
                    <ItemTemplate>
                        <asp:Button ID="btnAccess" runat="server" OnClick="btnAccess_Click"  Text='<%# Bind("level_of_access") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Password">
                    <ItemTemplate>
                        <asp:Button ID="btnChangePassword" runat="server"  CssClass=" btn-sm btn-danger m-t-n-xs" OnClick="btnChangePassword_Click"  Text='Change Password'/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

    </div>
</asp:Content>
