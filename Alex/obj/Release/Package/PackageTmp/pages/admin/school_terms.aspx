<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="school_terms.aspx.cs" Inherits="Alex.pages.admin.school_terms" %>

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
        $(function () { $('.datepick').datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); });
    </script>
    <div class="col-md-3 pull-right">
        <a href="../settings.aspx">
            <asp:Label runat="server" Text="&nbsp;Back to Settings" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"></asp:Label>
        </a>
    </div>
    <h2>Term Setup</h2>
    <div class=" wrapper-content  animated fadeInRight">
        <div class="row">
            <div class="col-lg-6">
                <div class="ibox">
                    <div role="form" id="form">
                        <div class="form-group">
                            <div class="editor-label">
                                <label>Select Academic Year</label><span class="required">*</span>
                            </div>
                            <div class="editor-field">
                                <asp:DropDownList ID="ddlAcademicYear" runat="server" ValidationGroup="TermSetup" CssClass="form-control">
                                </asp:DropDownList>
                                <div class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvAcademicYear" ErrorMessage=" Academic Year Required" ValidationGroup="TermSetup" ForeColor="Red" ControlToValidate="ddlAcademicYear"
                                        runat="server" Dispaly="Dynamic" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="editor-label">
                                <label>Term</label><span class="required">*</span>
                            </div>
                            <div class="editor-field">
                                <asp:DropDownList runat="server" ID="ddlTermName" CssClass="form-control"   ValidationGroup="TermSetup">
                                    <asp:ListItem Value="1st" Text="1st"></asp:ListItem>
                                    <asp:ListItem Value="2nd" Text="2nd"></asp:ListItem>
                                    <asp:ListItem Value="3rd" Text="3rd"></asp:ListItem>
                                </asp:DropDownList>
                                <%--<asp:TextBox ID="tbTermName" runat="server" placeholder="Term" CssClass="form-control" ValidationGroup="TermSetup"></asp:TextBox>--%>
                                <div class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvtbTermName" ErrorMessage="Term Required" ValidationGroup="TermSetup" ForeColor="Red" ControlToValidate="ddlTermName"
                                        runat="server" Dispaly="Dynamic" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="editor-label">
                                <label>Start Date</label><span class="required">*</span>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="tbStartDate" runat="server" placeholder="dd/mm/yyyy" ValidationGroup="TermSetup" CssClass="form-control datepick"></asp:TextBox>
                                <div class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvtbStartDate" ErrorMessage="Start Required" ValidationGroup="TermSetup" ForeColor="Red" ControlToValidate="tbStartDate"
                                        runat="server" Dispaly="Dynamic" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="editor-label">
                                <label>End date</label><span class="required">*</span>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="tbEndDate" runat="server" placeholder="dd/mm/yyyy" CssClass="form-control datepick" ValidationGroup="TermSetup"></asp:TextBox>
                                <div class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvtbEndDate" ErrorMessage="End Required" ForeColor="Red" ValidationGroup="TermSetup" ControlToValidate="tbEndDate"
                                        runat="server" Dispaly="Dynamic" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:Button ID="BtnSaveTerm" runat="server" Text="Save" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" ValidationGroup="TermSetup" OnClick="BtnSaveTerm_Click" />
                </div>
                <a href="../admin/setup_academic_year.aspx">
                    <asp:Label runat="server" ID="lblSetupTerm" Text="&nbsp;Setup Academic Year" CssClass="fa fa-arrow-circle-right btn btn-sm btn-info m-t-n-xs" Font-Bold="True"></asp:Label>
                </a>
            </div>

        </div>
        <h1>List of Terms</h1>
        <asp:GridView ID="GridViewTerm" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example"
            OnRowEditing="GridViewTerm_RowEditing" OnRowCancelingEdit="GridViewTerm_RowCancelingEdit"
            OnRowUpdating="GridViewTerm_RowUpdating" OnRowDeleting="GridViewTerm_RowDeleting" OnRowDataBound="GridViewTerm_RowDataBound">
            <Columns>
                <asp:BoundField DataField="ay_term_id" HeaderText="Term ID" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="acad_year" HeaderText="Academic Year " ReadOnly="true" />
                <asp:TemplateField HeaderText="Term">
                    <EditItemTemplate>
                       <%-- <asp:TextBox ID="tbTerm" runat="server" Text='<%# Bind("term_name") %>'></asp:TextBox>--%>
                        <asp:DropDownList runat="server" ID="ddlEditTermName"   ValidationGroup="tbTerm"   SelectedValue='<%# Bind("term_name") %>' >
                                    <asp:ListItem Value="1st" Text="1st"></asp:ListItem>
                                    <asp:ListItem Value="2nd" Text="2nd"></asp:ListItem>
                                    <asp:ListItem Value="3rd" Text="3rd"></asp:ListItem>
                                </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvtbTerm" ErrorMessage="Term Required" ForeColor="Red" ControlToValidate="ddlEditTermName"
                            runat="server" Dispaly="Dynamic" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblTerm" runat="server" Text='<%# Bind("term_name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Start Date">
                    <EditItemTemplate>
                        <asp:TextBox ID="tbTermStartDate" runat="server" Text='<%# Bind("ay_term_start_date") %>' CssClass="datepick"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvtbTermStartDate" ErrorMessage="Date Required" ForeColor="Red" ControlToValidate="tbTermStartDate"
                            runat="server" Dispaly="Dynamic" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblTermStartDate" runat="server" Text='<%# Bind("ay_term_start_date") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="End Date ">
                    <EditItemTemplate>
                        <asp:TextBox ID="tbTermEndDate" runat="server" Text='<%# Bind("ay_term_end_date") %>' CssClass="datepick"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvtbTermEndDate" ErrorMessage="Date Required" ForeColor="Red" ControlToValidate="tbTermEndDate"
                            runat="server" Dispaly="Dynamic" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblTermEndDate" runat="server" Text='<%# Bind("ay_term_end_date") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="status" HeaderText="Status" ReadOnly="true"></asp:BoundField>
                <asp:TemplateField ShowHeader="false" HeaderText="Edit">
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
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Button ID="btnStatus" runat="server" CssClass="btn-sm btn-default bg-success m-t-n-xs" OnClick="btnStatus_Click" Text='Make Active' />
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
