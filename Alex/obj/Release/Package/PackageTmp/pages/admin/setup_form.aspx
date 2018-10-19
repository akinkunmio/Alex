<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="setup_form.aspx.cs" Inherits="Alex.pages.admin.setup_form" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../scripts/css/pagestyle.css" rel="stylesheet" />
       <div class="col-md-3 pull-right">
            <a href="../settings.aspx">
               <asp:Label runat="server" Text="&nbsp;Back to Settings" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"  ></asp:Label>
            </a>
        </div>
    <h2>Class Setup</h2>

    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row">
            <div class="col-lg-6">
                <div class="ibox">
                    <div role="form" id="form">
                        <div class="form-group">
                            <div class="editor-label">
                                <label>Class</label><span class="required">*</span>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="tbSetupForm" runat="server" placeholder="Enter Class" ValidationGroup="SetupForm" CssClass="form-control"></asp:TextBox>
                                 <div class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvtbSetupForm" ErrorMessage="Class Required" ValidationGroup="SetupForm" ForeColor="Red" ControlToValidate="tbSetupForm"
                                                runat="server" Dispaly="Dynamic" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="editor-label">
                                <label>Section</label><span class="required">*</span>
                            </div>
                            <div class="editor-field">
                                <asp:DropDownList runat="server" ID="ddlSection" ValidationGroup="SetupForm" CssClass="form-control" ></asp:DropDownList>
                                <div class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvtbSection" ErrorMessage="Section Required" ValidationGroup="SetupForm" ForeColor="Red" ControlToValidate="ddlSection"
                                                runat="server" Dispaly="Dynamic" />
                                </div>
                            </div>
                        </div>
                        <asp:Button ID="BtnSaveSetupForm" runat="server" Text="Save" CssClass="btn btn-sm btn-primary m-t-n-xs" ValidationGroup="SetupForm" Font-Bold="True" OnClick="BtnSaveSetupForm_Click" />
                    </div>
                </div>
                <h1>List of Class </h1>

                <asp:SqlDataSource ID="SqlDataSourceSectionDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>" SelectCommand="sp_ms_section_dropdown" ></asp:SqlDataSource>
                <asp:GridView ID="GridViewForm" runat="server" AutoGenerateColumns="False"
                    Visible="False" CssClass="table table-striped table-bordered table-hover dataTables-example"
                    OnRowEditing="GridViewForm_RowEditing" OnRowCancelingEdit="GridViewForm_RowCancelingEdit" 
                    OnRowUpdating="GridViewForm_RowUpdating" 
                    OnRowDeleting="GridViewForm_RowDeleting"
                    OnRowDataBound="GridViewForm_RowDataBound">
                    
                    <Columns>
                        <asp:BoundField DataField="form_id" HeaderText="Form Id" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"><HeaderStyle CssClass="hidden"></HeaderStyle>
                        <ItemStyle CssClass="hidden"></ItemStyle> </asp:BoundField>
                      
                        <asp:TemplateField HeaderText="Class">
                            <ItemTemplate>
                                <%#Eval("form_name")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtValue" runat="server" Text='<%# Eval("form_name") %>'></asp:TextBox>
                                <div class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvtxtValue" ErrorMessage="Class Required" ForeColor="Red" ControlToValidate="txtValue"
                                                runat="server" Dispaly="Dynamic" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Section">
                            <ItemTemplate>
                                <asp:Label ID="lblSection" runat="server" Text='<%# Eval("section2")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                               <asp:DropDownList ID = "ddlEditSection" runat="server"  >
                                           </asp:DropDownList>
                                           
                                <div class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvtbSection" ErrorMessage="Section Required" ForeColor="Red" ControlToValidate="ddlEditSection"
                                                runat="server" Dispaly="Dynamic" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                   

                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="Edit"  />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="lnkUpdate" runat="server" Text="Update" CommandName="Update"   />
                                <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel" CommandName="Cancel" CausesValidation="false"  />
                            </EditItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                              <ItemTemplate>
                                  <asp:LinkButton ID="lnkDelete" runat="server"  CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete ?');" Text="Delete"></asp:LinkButton>
                              </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

<%--  --Pages Styles--%>
 <style>
.required {
color: #F00;
}
</style>

</asp:Content>
