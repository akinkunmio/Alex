<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="setup_fee.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="Alex.pages.admin.setup_fee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../scripts/css/pagestyle.css" rel="stylesheet" />
    <div class="col-md-3 pull-right">
        <a href="../settings.aspx">
            <asp:Label runat="server" Text="&nbsp;Back to Settings" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"></asp:Label>
        </a>
    </div>
    <h2>Fee Setup</h2>

    <div class=" wrapper-content  animated fadeInRight">
        <div class="row">
            <div class="col-lg-6">
                <div class="ibox">
                    <div role="form" id="form">
                        <div>
                            <div class="form-group">
                                <div class="editor-label">
                                    <label>Academic Year</label>
                                </div>
                                <div class="editor-field">
                                    <asp:DropDownList ID="ddlFeeSetupYear" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFeeSetupYear_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <div class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvddlFeeSetupYear" ValidationGroup="FeeSetupGroup" ErrorMessage="Year Required" ForeColor="Red" ControlToValidate="ddlFeeSetupYear"
                                            runat="server" Dispaly="Dynamic" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="editor-label">
                                    <label>Term</label>
                                </div>
                                <div class="editor-field">
                                    <asp:DropDownList ID="ddlFeeSetupTerm" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFeeSetupTerm_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <div class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvddlFeeSetupTerm" ValidationGroup="FeeSetupGroup" ErrorMessage="Term Required" ForeColor="Red" ControlToValidate="ddlFeeSetupTerm"
                                            runat="server" Dispaly="Dynamic" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="editor-label">
                                    <label>Class</label>
                                </div>
                                <div class="editor-field">
                                    <asp:DropDownList ID="ddlFeeSetupForm" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                    <div class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvddlFeeSetupForm" ValidationGroup="FeeSetupGroup" ErrorMessage="Class Required" ForeColor="Red" ControlToValidate="ddlFeeSetupForm"
                                            runat="server" Dispaly="Dynamic" />
                                    </div>
                                </div>
                          </div>
                          <div class="form-group">
                                <div class="editor-label">
                                    <label>Fee Amount</label>
                                </div>
                                <div class="editor-field">
                                    <asp:TextBox ID="tbFeeSetupAmount" runat="server" placeholder="Enter Fee" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                    <div class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvtbFeeSetupAmount" ValidationGroup="FeeSetupGroup" ErrorMessage="Fee Required" ForeColor="Red" ControlToValidate="tbFeeSetupAmount"
                                            runat="server" Dispaly="Dynamic" />
                                    </div>
                                </div>
                            </div>


                            <asp:Button ID="BtnSaveFeSetup" runat="server" Text="Save" ValidationGroup="FeeSetupGroup" CausesValidation="true" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" OnClick="BtnSaveFeeSetup_Click" />

                        </div>
                    </div>
                </div>
            </div>
            <div class="dashboard-header col-md-4 pull-right">
               <a class="btn btn-primary btn-rounded btn-block" href="../admin/batch_fee_setup.aspx"><i class="fa fa-copy"></i> Copy Fee from pervious Term</a>
            </div>
        </div>
        <h1>List of Current Term Fee's</h1>
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

        <asp:GridView ID="GridViewFee" runat="server" DataKeyNames="form_name" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example"
            OnRowEditing="GridViewFee_RowEditing"
            OnRowCancelingEdit="GridViewFee_RowCancelingEdit"
            OnRowUpdating="GridViewFee_RowUpdating"
            OnRowDeleting="GridViewFee_RowDeleting">
            <Columns>
                <asp:BoundField DataField="acad_year" HeaderText="Acad Year" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="term_name" HeaderText="Term" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="form_name" HeaderText="Class" ReadOnly="true" />
                <asp:BoundField DataField="fee_id" HeaderText="Fee Id" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="form_id" HeaderText="FormId" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="acad_y_term_id" HeaderText="acad_y_term_id" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="Fee ₦">
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
                <asp:TemplateField HeaderText="Fee Breakdown">
                                <ItemTemplate>
                                    <asp:Button ID="btnFeeBreakDown" runat="server" Text="Add/View" OnClick="btnFeeBreakDown_Click" />
                                </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
          <asp:Label ID="lblZeroRecords" runat="server" Text=""></asp:Label>
        <%--<asp:CommandField ShowEditButton="True"/>--%>
        <br />


    </div>
</asp:Content>
