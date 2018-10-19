<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="setup_boarder_fees.aspx.cs" Inherits="Alex.pages.admin.setup_boarder_fees" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <link href="../../scripts/css/pagestyle.css" rel="stylesheet" />
    <div class="col-md-3 pull-right">
        <a href="../settings.aspx">
            <asp:Label runat="server" Text="&nbsp;Back to Settings" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"></asp:Label>
        </a>
    </div>
    <h2>Boarder Fee Setup</h2>
 <div class=" wrapper-content  animated fadeInRight">
        <div class="row">
            <div class="col-lg-7">
                <div class="ibox">
                    <div role="form" id="form">
                        <div>
                            <div class="form-group">
                                <div class="editor-label">
                                    <label>Academic Year</label>
                                </div>
                                <div class="editor-field">
                                    <asp:DropDownList ID="ddlBoarderFeeSetupYear" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlBoarderFeeSetupYear_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <div class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvddlBoarderFeeSetupYear" ValidationGroup="BoarderFeeSetupGroup" ErrorMessage="Year Required" ForeColor="Red" ControlToValidate="ddlBoarderFeeSetupYear"
                                            runat="server" Dispaly="Dynamic" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="editor-label">
                                    <label>Term</label>
                                </div>
                                <div class="editor-field">
                                    <asp:DropDownList ID="ddlBoarderFeeSetupTerm" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlBoarderFeeSetupTerm_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <div class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvddlBoarderFeeSetupTerm" ValidationGroup="BoarderFeeSetupGroup" ErrorMessage="Term Required" ForeColor="Red" ControlToValidate="ddlBoarderFeeSetupTerm"
                                            runat="server" Dispaly="Dynamic" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="editor-label">
                                    <label>Boarder Type</label>
                                </div>
                                <div class="editor-field">
                                    <asp:DropDownList ID="ddlBoarderFeeSetupBrType" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                    <div class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvddlBoarderFeeSetupBrType" ValidationGroup="BoarderFeeSetupGroup" ErrorMessage="Class Required" ForeColor="Red" ControlToValidate="ddlBoarderFeeSetupBrType"
                                            runat="server" Dispaly="Dynamic" />
                                    </div>
                                </div>
                          </div>
                          <div class="form-group">
                                <div class="editor-label">
                                    <label>Fee Amount</label>
                                </div>
                                <div class="editor-field">
                                    <asp:TextBox ID="tbBoarderFeeSetupAmount" runat="server" placeholder="Enter Fee" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                    <div class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvtbBoarderFeeSetupAmount" ValidationGroup="BoarderFeeSetupGroup" ErrorMessage="Fee Required" ForeColor="Red" ControlToValidate="tbBoarderFeeSetupAmount"
                                            runat="server" Dispaly="Dynamic" />
                                    </div>
                                </div>
                            </div>


                            <asp:Button ID="BtnSaveBoarderFeSetup" runat="server" Text="Save" ValidationGroup="BoarderFeeSetupGroup" CausesValidation="true" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" OnClick="BtnSaveBoarderFeSetup_Click" />

                        </div>
                    </div>
                </div>
            </div>
           
        </div>
        <h1>List of Current Term Boarder Fees:</h1>
      <%--  <div class="row">
            <div class="col-lg-12">
                <div class="float-e-margins">
                    <div class="col-md-6">
                        <asp:DropDownList ID="ddlAcademicYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlTerm" runat="server">
                        </asp:DropDownList>
                        <asp:Button ID="btnSearchAcadmicYear" runat="server" Text="GO" type="search" CssClass="btn-primary" OnClick="btnSearchAcadmicYear_Click" />
                    </div>
                </div>
            </div>
        </div>--%>

        <asp:GridView ID="GridViewBoarderFee" runat="server" DataKeyNames="type_description" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example"
            OnRowEditing="GridViewBoarderFee_RowEditing"
            OnRowCancelingEdit="GridViewBoarderFee_RowCancelingEdit"
            OnRowUpdating="GridViewBoarderFee_RowUpdating"
            OnRowDeleting="GridViewBoarderFee_RowDeleting">
            <Columns>
                <asp:BoundField DataField="acad_year" HeaderText="Acad Year" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="term_name" HeaderText="Term" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="type_description" HeaderText="Boarder Type" ReadOnly="true" />
                <asp:BoundField DataField="board_fee_id" HeaderText="Boarder Fee Id" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="boarder_type_id" HeaderText="BoarderTypeId" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="acad_y_term_id" HeaderText="acad_y_term_id" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="Boarder Fee ₦">
                    <EditItemTemplate>
                        <asp:TextBox ID="tbAmount" runat="server" Text='<%# Bind("amount") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvtbAmount" ErrorMessage="Fee Required" ForeColor="Red" ControlToValidate="tbAmount"
                            runat="server" Dispaly="Dynamic" />
                        <asp:RegularExpressionValidator ID="revtbAmount" ControlToValidate="tbAmount" runat="server"
                            ErrorMessage="Only Numbers allowed" ForeColor="Red" ValidationExpression="^\d+([,\.]\d{1,2})?$"></asp:RegularExpressionValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("amount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Edit">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="Edit" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="lnkUpdate" runat="server" Text="Update" CommandName="Update" />
                        <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel" CommandName="Cancel" CausesValidation="False" />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete ?');" Text="Delete"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
          <asp:Label ID="lblZeroRecords" runat="server" Text=""></asp:Label>
        
        <br />


    </div>
</asp:Content>
