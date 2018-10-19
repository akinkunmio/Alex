<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="setup_class.aspx.cs" Inherits="Alex.pages.admin.setup_class" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../scripts/css/pagestyle.css" rel="stylesheet" />
       <div class="col-md-3 pull-right">
            <a href="../settings.aspx">
               <asp:Label runat="server" Text="&nbsp;Back to Settings" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"  ></asp:Label>
            </a>
        </div>
<h2>Arm Setup</h2>
<div class=" wrapper-content  animated fadeInRight" >
   <div class="row">
     <div class="col-lg-6">
        <div class="ibox">
           <div role="form" id="form">
               <div class="form-group">
                  <div class="editor-label">
                       <label>Class</label><span class="required">*</span>
                   </div>
                       <div class="editor-field">
                           <asp:DropDownList ID="ddlFormName" runat="server" ValidationGroup="ClassSetup"  CssClass="form-control"></asp:DropDownList>
                            <div class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvddlFormName" ErrorMessage="Class Required" ValidationGroup="ClassSetup" ForeColor="Red" ControlToValidate="ddlFormName"
                                                runat="server" Dispaly="Dynamic" />
                            </div>
                       </div>
                  </div>
                  <div class="form-group">
                       <div class="editor-label">
                         <label>Arm</label><span class="required">*</span>
                      </div>
                      <div class="editor-field">
                            <asp:TextBox ID="tbSetupClass" runat="server" placeholder="Enter Arm"  ValidationGroup="ClassSetup" CssClass="form-control"></asp:TextBox>
                           <div class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvtbSetupClass" ErrorMessage="Arm Required" ValidationGroup="ClassSetup" ForeColor="Red" ControlToValidate="tbSetupClass"
                                                runat="server" Dispaly="Dynamic" />
                           </div>
                      </div>
                  </div>
               </div>
               <asp:Button ID="btnSaveClass" runat="server" Text="Save" type="search" CssClass="btn-primary" ValidationGroup="ClassSetup" OnClick="btnSaveClass_Click"/>
           </div>
         <asp:GridView ID="GridViewClass" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example" 
             OnRowEditing="GridViewClass_RowEditing" OnRowCancelingEdit="GridViewClass_RowCancelingEdit" 
             OnRowUpdating="GridViewClass_RowUpdating" OnRowDeleting="GridViewClass_RowDeleting">
             <Columns>
                
                 <asp:BoundField DataField="class_id" HeaderText="Class id" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                     <HeaderStyle CssClass="hidden"></HeaderStyle><ItemStyle CssClass="hidden"></ItemStyle> 
                 </asp:BoundField>
                <asp:BoundField DataField="form_name" HeaderText="Class" readonly="true"/>
                <%-- <asp:TemplateField HeaderText="Form Name">
                     <EditItemTemplate>
                         <asp:TextBox ID="tbForm" runat="server" Text='<%# Bind("form_name") %>' ReadOnly="true"></asp:TextBox>
                    </EditItemTemplate>
                     <ItemTemplate>
                         <asp:Label ID="lblForm" runat="server" Text='<%# Bind("form_name") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>--%>
                 <asp:TemplateField HeaderText="Arm">
                     <EditItemTemplate>
                         <asp:TextBox ID="tbClass" runat="server" Text='<%# Bind("class_name") %>'></asp:TextBox>
                         <asp:RequiredFieldValidator ID="rfvtbClass" ErrorMessage="Class name Required" ForeColor="Red" ControlToValidate="tbClass"
                                                runat="server" Dispaly="Dynamic" />
                     </EditItemTemplate>
                     <ItemTemplate>
                         <asp:Label ID="lblClass" runat="server" Text='<%# Bind("class_name") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField>
                      <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="Edit"/>
                      </ItemTemplate>
                      <EditItemTemplate>
                        <asp:LinkButton ID="lnkUpdate" runat="server" Text="Update" CommandName="Update"/>
                        <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel" CommandName="Cancel" CausesValidation="false"/>
                      </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                              <ItemTemplate>
                                  <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete ?');" Text="Delete"></asp:LinkButton>
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
