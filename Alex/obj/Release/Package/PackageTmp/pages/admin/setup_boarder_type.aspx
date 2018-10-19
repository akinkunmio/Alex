<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="setup_boarder_type.aspx.cs" Inherits="Alex.pages.admin.setup_boarder_type" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <%-- <link href="../../scripts/css/pagestyle.css" rel="stylesheet" />--%>

     <div class="col-md-3 pull-right">
            <a href="../settings.aspx">
               <asp:Label runat="server" Text="&nbsp;Back to Settings" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"  ></asp:Label>
            </a>
     </div>
    <h2>Boarder Type Setup</h2>
     <div class=" wrapper-content  animated fadeInRight">
        <div class="row">
            <div class="col-lg-6">
                <div class="ibox">
                    <div role="form" id="form">
                        <div class="form-group">
                            <div class="editor-label">
                                <label>Boarder Type</label><span class="required">*</span>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="tbBoarderType" runat="server" placeholder="Enter Boarder type" ValidationGroup="BoarderForm" CssClass="form-control"></asp:TextBox>
                                 <div class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvtbBoarderType" ErrorMessage="Class Required" ValidationGroup="BoarderForm" ForeColor="Red" ControlToValidate="tbBoarderType"
                                                runat="server" Dispaly="Dynamic" />
                                </div>
                            </div>
                        </div>
                        <asp:Button ID="BtnSaveBoarderType" runat="server" Text="Save" CssClass="btn btn-sm btn-primary m-t-n-xs" ValidationGroup="BoarderForm" Font-Bold="True" OnClick="BtnSaveBoarderType_Click" />
                   </div>
                </div>
                <h1>List of Boarder Types</h1>

                  <asp:GridView ID="GridViewBoarderType" runat="server" AutoGenerateColumns="False"
                   CssClass="table table-striped table-bordered table-hover dataTables-example"
                    OnRowEditing="GridViewBoarderType_RowEditing" OnRowCancelingEdit="GridViewBoarderType_RowCancelingEdit"
                    OnRowUpdating="GridViewBoarderType_RowUpdating" OnRowDeleting="GridViewBoarderType_RowDeleting">
                        <Columns>
                        <asp:BoundField DataField="boarder_type_id" HeaderText="boarder_type_id" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"><HeaderStyle CssClass="hidden"></HeaderStyle>
                        <ItemStyle CssClass="hidden"></ItemStyle> </asp:BoundField>
                      
                        <asp:TemplateField HeaderText="Boarder Type">
                            <ItemTemplate>
                                <%#Eval("type_description")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="tbBoarderDescription" runat="server" Text='<%# Eval("type_description") %>'></asp:TextBox>
                                <div class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvtbBoarderDescription" ErrorMessage="Type Required" ForeColor="Red" ControlToValidate="tbBoarderDescription"
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
                </asp:GridView><asp:Label runat="server" ID="lblZeroRecords" ></asp:Label>
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