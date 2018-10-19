<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="setup_academic_year.aspx.cs" Inherits="Alex.pages.admin.setup_academic_year" %>

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
       // $(function () { $('.datepick').datepicker({ changeMonth: true, changeYear: true, yearRange: '-100:' + new Date().getFullYear(), dateFormat: 'dd/mm/yy', maxDate: new Date() }); });
    </script>
    <div class="col-md-3 pull-right">
        <a href="../settings.aspx">
            <asp:Label runat="server" Text="&nbsp;Back to Settings" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"></asp:Label>
        </a>
    </div>
    <h2>Academic Year Setup</h2>
    <div>
        <asp:Label ID="lblSetupAcademicYear" runat="server" Text=""></asp:Label></div>
    <div class=" wrapper-content  animated fadeInRight">
        <div class="row">
            <div class="col-lg-6">
                <div class="ibox">
                    <div role="form" id="form">
                        <div>
                            <div class="form-group">
                                <div class="editor-label">
                                    <label>Academic Year</label><span class="required">*</span>
                                </div>
                                <div class="editor-field">
                                    <asp:TextBox ID="tbAcademicYear" runat="server" placeholder="Enter Academic year" ValidationGroup="YearSetup" CssClass="form-control"></asp:TextBox>
                                    <div class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvAcademicYear" ErrorMessage=" Academic Year Required" ForeColor="Red" ValidationGroup="YearSetup" ControlToValidate="tbAcademicYear"
                                            runat="server" Dispaly="Dynamic" />
                                    </div>
                                </div>
                               
                            </div>

                            <div class="form-group">
                                <div class="editor-label">
                                    <label>Academic Start Date</label><span class="required">*</span>
                                </div>
                                <div class="editor-field">
                                    <asp:TextBox ID="tbAcademicStartDate" runat="server" placeholder="dd/mm/yyyy" ValidationGroup="YearSetup" CssClass="form-control datepick"></asp:TextBox>
                                    <div class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvAcademicStartDate" ErrorMessage=" Academic Start Date Required" ForeColor="Red" ControlToValidate="tbAcademicStartDate"
                                            runat="server" Dispaly="Dynamic" ValidationGroup="YearSetup" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="editor-label">
                                    <label>Academic End Date</label><span class="required">*</span>
                                </div>
                                <div class="editor-field">
                                    <asp:TextBox ID="tbAcademicEndDate" runat="server" placeholder="dd/mm/yyyy" CssClass="form-control datepick" ValidationGroup="YearSetup"></asp:TextBox>
                                    <div class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvAcademicEndDate" ErrorMessage=" Academic End Date Required" ForeColor="Red" ControlToValidate="tbAcademicEndDate"
                                            runat="server" Dispaly="Dynamic" ValidationGroup="YearSetup" />
                                    </div>
                                </div>
                            </div>
                            <%-- <div class="form-group">
                                    <div class="editor-label">
                                        <label>Created By</label>
                                    </div>
                                    <div class="editor-field">
                                        <asp:TextBox ID="tbCreatedBy" runat="server" placeholder="Enter your Name" CssClass="form-control"></asp:TextBox>
                                        <div class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvCreatedBy" ErrorMessage=" Your Name Required" ForeColor="Red" ControlToValidate="tbCreatedBy"
                                                runat="server" Dispaly="Dynamic" />
                                        </div>
                                    </div>
                                </div>--%>

                            <asp:Button ID="BtnSaveAcademicYear" runat="server" Text="Save" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" ValidationGroup="YearSetup" OnClick="BtnSaveAcademicYear_Click" />

                        </div>

                        
                    </div>
                </div>
                 <a href="../admin/school_terms.aspx">
                   <asp:Label runat="server" ID="lblSetupTerm" Text="&nbsp;Setup Term" CssClass="fa fa-arrow-circle-right btn btn-sm btn-info m-t-n-xs" Font-Bold="True"></asp:Label>
                 </a>
            </div>
        </div>
        <h1>List of Academic Years</h1>
        <asp:GridView ID="GridViewAcademicYear" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example"
            OnRowEditing="GridViewAcademicYear_RowEditing"
            OnRowCancelingEdit="GridViewAcademicYear_RowCancelingEdit"
            OnRowUpdating="GridViewAcademicYear_RowUpdating"
            OnRowDeleting="GridViewAcademicYear_RowDeleting" OnRowDataBound="GridViewAcademicYear_RowDataBound">
            <Columns>
                <asp:BoundField DataField="acad_year_id" HeaderText="AcademicYearId" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>

                <asp:TemplateField HeaderText="Academic Year">
                    <EditItemTemplate>
                        <asp:TextBox ID="tbEditAcademicYear" runat="server" Text='<%# Bind("acad_year") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvtbEditAcademicYear" ErrorMessage="Academic Year Required" ForeColor="Red" ControlToValidate="tbEditAcademicYear"
                                            runat="server" Dispaly="Dynamic" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("acad_year") %>' ></asp:Label> 
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Start Date">
                    <EditItemTemplate>
                        <asp:TextBox ID="tbAcademicYearSD" runat="server" Text='<%# Bind("acad_y_start_date") %>' CssClass="datepick input-group date"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="rfvtbAcademicYearSD" ErrorMessage="Date Required" ForeColor="Red" ControlToValidate="tbAcademicYearSD"
                                            runat="server" Dispaly="Dynamic" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("acad_y_start_date") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="End Date ">
                    <EditItemTemplate>
                        <asp:TextBox ID="tbAcademicYearED" runat="server" Text='<%# Bind("acad_y_end_date") %>' CssClass="datepick input-group date"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvtbAcademicYearED" ErrorMessage="Date Required" ForeColor="Red" ControlToValidate="tbAcademicYearED"
                                            runat="server" Dispaly="Dynamic" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblAcademicYearED" runat="server" Text='<%# Bind("acad_y_end_date") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:BoundField DataField="status" HeaderText="Status" ReadOnly="true"></asp:BoundField>
                <asp:TemplateField HeaderText="Edit">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="Edit" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="lnkUpdate" runat="server" Text="Update" CommandName="Update"/>
                        <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel" CommandName="Cancel" CausesValidation="false" />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete ?');" Text="Delete"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                <%--<asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Button ID="btnStatus" runat="server"  CssClass=" btn-sm btn-default m-t-n-xs" OnClick="btnStatus_Click" CausesValidation="False"  Text='Make Active'/>
                    </ItemTemplate>
                </asp:TemplateField>--%>
            </Columns>
        </asp:GridView>
        <asp:Label ID="lblZeroRecords" runat="server" Text=""></asp:Label>
    </div>

<%--  --Pages Styles--%>
<style>
.required {
color: #F00;
}
</style>
</asp:Content>
