<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="fee_breakdown.aspx.cs" Inherits="Alex.pages.admin.fee_breakdown" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../scripts/css/pagestyle.css" rel="stylesheet" />
       <div class="col-md-3 pull-right">
            <a href="../admin/setup_fee.aspx">
               <asp:Label runat="server" Text="&nbsp;Back to Fee's" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"  ></asp:Label>
            </a>
        </div>
    <h2>Fee BreakDown Setup</h2>

    <div class=" wrapper-content  animated fadeInRight">
        
        <div class="row">
            <div class="col-lg-6">
                <div class="ibox">
                    <div role="form" id="form">
                         <div class="form-group">
                            <div class="editor-label">
                                <label>Class:</label>
                            </div>
                            <div class="editor-field">
                                <asp:Label ID="lblClass" runat="server" Text="" ></asp:Label>
                            </div>
                        </div>
                         <div class="form-group">
                            <div class="editor-label">
                                <label>Total Fee:</label>
                            </div>
                            <div class="editor-field">
                                <asp:Label ID="lblFee" runat="server" Text="" ></asp:Label>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="editor-label">
                                <label>Item</label><span class="required">*</span>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="tbSetupFeeItem" runat="server" placeholder="Enter Item" ValidationGroup="SetupFeeBrkDwn" CssClass="form-control"></asp:TextBox>
                                 <div class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvtbSetupForm" ErrorMessage="Item Required" ValidationGroup="SetupFeeBrkDwn" ForeColor="Red" ControlToValidate="tbSetupFeeItem"
                                                runat="server" Dispaly="Dynamic" />
                                     <asp:RegularExpressionValidator ID="revtbSetupFeeItem" ControlToValidate="tbSetupFeeItem" runat="server" ValidationGroup="SetupFeeBrkDwn"
                                     ErrorMessage="Valid characters Only" ForeColor="Red" ValidationExpression="[a-zA-Z ]*$"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="editor-label">
                                <label>Amount</label><span class="required">*</span>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="tbAmount" runat="server" placeholder="Enter Amount" ValidationGroup="SetupFeeBrkDwn" CssClass="form-control"></asp:TextBox>
                                 <div class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvtbSection" ErrorMessage="Amount Required" ValidationGroup="SetupFeeBrkDwn" ForeColor="Red" ControlToValidate="tbAmount"
                                                runat="server" Dispaly="Dynamic" />
                                      <asp:RegularExpressionValidator ID="revtbAmount" ControlToValidate="tbAmount" runat="server" ValidationGroup="SetupFeeBrkDwn"
                                     ErrorMessage="Only Numbers allowed" ForeColor="Red" ValidationExpression="^\d+([,\.]\d{1,2})?$"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <asp:Button ID="BtnSaveSetupFeeBrkDwn" runat="server" Text="Save" CssClass="btn btn-sm btn-primary m-t-n-xs" ValidationGroup="SetupFeeBrkDwn" OnClick="BtnSaveSetupFeeBrkDwn_Click" Font-Bold="True"  />
                    </div>
                </div>
                <h1>BreakDown Fee For the Class:<asp:Label ID="lblBrkDwnFeeClass" runat="server" ></asp:Label> </h1>


                <asp:GridView ID="GridViewSetupFeeBrkDwn" runat="server" AutoGenerateColumns="False"
                    Visible="False" CssClass="table table-striped table-bordered table-hover dataTables-example"
                     OnRowEditing="GridViewSetupFeeBrkDwn_RowEditing" OnRowCancelingEdit="GridViewSetupFeeBrkDwn_RowCancelingEdit"
                      OnRowDeleting="GridViewSetupFeeBrkDwn_RowDeleting" OnRowUpdating="GridViewSetupFeeBrkDwn_RowUpdating" OnRowDataBound="GridViewSetupFeeBrkDwn_RowDataBound" ShowFooter="True">
                    
                    <Columns>
                        <asp:BoundField DataField="fb_id" HeaderText="FB Id" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"><HeaderStyle CssClass="hidden"></HeaderStyle>
                        <ItemStyle CssClass="hidden"></ItemStyle> </asp:BoundField>
                      
                        <asp:TemplateField HeaderText="Item">
                            <ItemTemplate>
                                <%#Eval("item")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtItem" runat="server" Text='<%# Eval("item") %>'></asp:TextBox>
                                <div class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvtxtItem" ErrorMessage="Class Required" ForeColor="Red" ControlToValidate="txtItem"
                                                runat="server" Dispaly="Dynamic" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Fee BreakDowns">
                            <ItemTemplate>
                                <%#Eval("amount")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="tbFeeAmount" runat="server" Text='<%# Eval("amount") %>'></asp:TextBox>
                                <div class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvtbSection" ErrorMessage="Fee Required" ForeColor="Red" ControlToValidate="tbFeeAmount"
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
                 <asp:Label ID="lblZeroRecords" runat="server" Text=""></asp:Label>
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
