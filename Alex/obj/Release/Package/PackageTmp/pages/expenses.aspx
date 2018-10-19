<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="expenses.aspx.cs" Inherits="Alex.pages.expenses" %>

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
        $(function () { $('.datepick').datepicker({ changeMonth: true, changeYear: true, yearRange: '-100:' + new Date().getFullYear(), dateFormat: 'dd/mm/yy', maxDate: new Date() }); });
    </script>



    <div class=" wrapper-content  animated fadeInRight">
        <asp:Button ID="btnAddExpenses" runat="server" Text="Add New Expense" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" OnClick="btnAddExpenses_Click" />
        <div class="row" id="divAddExpenses" runat="server" visible="false">
            <h2>Add New Expense</h2>
            <div class="col-lg-8">
                <div class="ibox">
                    <div role="form" id="form">
                        <div>
                            <div class="form-group">
                                <div class="editor-label">
                                    <label>Expense Type</label><span class="required">*</span>
                                </div>
                                <div class="editor-field">
                                    <asp:DropDownList ID="ddlPaymentType" runat="server" CssClass="form-control" ValidationGroup="SchoolExpenses">
                                        <asp:ListItem Text="Choose Type" Value=""></asp:ListItem>
                                        <asp:ListItem>Payment</asp:ListItem>
                                        <asp:ListItem>Refund</asp:ListItem>
                                    </asp:DropDownList>
                                    <div class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvddlPaymentType" ErrorMessage="Type of Refund Required" ForeColor="Red" ControlToValidate="ddlPaymentType" ValidationGroup="SchoolExpenses"
                                            runat="server" Dispaly="Dynamic" />
                                    </div>
                                </div>

                            </div>

                            <div class="form-group">
                                <div class="editor-label">
                                    <label>Category</label><span class="required">*</span>
                                </div>
                                <div class="editor-field">
                                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" ValidationGroup="SchoolExpenses"></asp:DropDownList>
                                    <div class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvddlCategory" ErrorMessage="Category Required" ForeColor="Red" ControlToValidate="ddlCategory" ValidationGroup="SchoolExpenses"
                                            runat="server" Dispaly="Dynamic" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="editor-label">
                                    <label>Expense Date</label><span class="required">*</span>
                                </div>
                                <div class="editor-field">
                                    <asp:TextBox ID="tbExpensesDate" runat="server" placeholder="dd/mm/yyyy" CssClass="form-control datepick" ValidationGroup="SchoolExpenses"></asp:TextBox>
                                    <div class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvtbExpensesDate" ErrorMessage="Date Required" ForeColor="Red" ControlToValidate="tbExpensesDate" ValidationGroup="SchoolExpenses"
                                            runat="server" Dispaly="Dynamic" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="editor-label">
                                    <label>Amount</label><span class="required">*</span>
                                </div>
                                <div class="editor-field">
                                    <asp:TextBox ID="tbAmount" runat="server" placeholder="Enter Amount" CssClass="form-control" ValidationGroup="SchoolExpenses"></asp:TextBox>
                                    <div class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvtbAmount" ErrorMessage="Amount Required" ForeColor="Red" ControlToValidate="tbAmount" ValidationGroup="SchoolExpenses"
                                            runat="server" Dispaly="Dynamic" />
                                        <asp:RegularExpressionValidator ID="revtbAmount" ControlToValidate="tbAmount" runat="server" ValidationGroup="SchoolExpenses"
                                            ErrorMessage="Only Numbers allowed" ForeColor="Red" ValidationExpression="^\d+([,\.]\d{1,2})?$"></asp:RegularExpressionValidator>
                                        <asp:RangeValidator ID="RangeValidatorAmount" runat="server" ErrorMessage="Amount must be greater than zero" ForeColor="Red" ValidationGroup="SchoolExpenses"
                                            ControlToValidate="tbAmount" MinimumValue="0.01" MaximumValue="999999999.99"></asp:RangeValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="editor-label">
                                    <label>Description</label><span class="required">*</span>
                                </div>
                                <div class="editor-field">
                                    <asp:TextBox ID="tbDescription" runat="server" placeholder="Enter Description" CssClass="form-control " ValidationGroup="SchoolExpenses"></asp:TextBox>
                                    <div class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvtbDescription" ErrorMessage="Description Required" ForeColor="Red" ControlToValidate="tbDescription" ValidationGroup="SchoolExpenses"
                                            runat="server" Dispaly="Dynamic" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="editor-label">
                                    <label>Reciept Reference</label>
                                </div>
                                <div class="editor-field">
                                    <asp:TextBox ID="tbRecieptRef" runat="server" placeholder="Enter Reciept Reference" CssClass="form-control"></asp:TextBox>
                                    <%--  <div class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvtbRecieptRef" ErrorMessage="Reciept Reference Required" ForeColor="Red" ControlToValidate="tbRecieptRef"
                                            runat="server" Dispaly="Dynamic" />
                                    </div>--%>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="editor-label">
                                    <label>Academic Year</label>
                                </div>
                                <div class="editor-field">
                                    <asp:DropDownList ID="ddlAcademicYear" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="editor-label">
                                    <label>Term</label>
                                </div>
                                <div class="editor-field">
                                    <asp:DropDownList ID="ddlTerm" runat="server" CssClass="form-control">
                                    </asp:DropDownList>

                                </div>
                            </div>

                            <asp:Button ID="BtnSaveExpenses" runat="server" Text="Save" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" ValidationGroup="SchoolExpenses" OnClick="BtnSaveExpenses_Click" />
                            <asp:Button ID="BtnSaveNAddExp" runat="server" Text="Save & Add Another" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" ValidationGroup="SchoolExpenses" OnClick="BtnSaveNAddExp_Click" />
                            <asp:Button ID="BtnCancelExpenses" runat="server" Text="Cancel" CssClass="btn btn-sm btn-primary m-t-n-xs" CausesValidation="false" Font-Bold="True" OnClick="BtnCancelExpenses_Click" />
                        </div>


                    </div>
                </div>

            </div>
        </div>
        <asp:SqlDataSource ID="SqlDataSourceCategory" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
            SelectCommand="SELECT status_name FROM ms_status Where category = 'Expenses_type'"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSourceEthnicityDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
            SelectCommand="SELECT ethnicity FROM ms_dropdown_nigeria_ethnicity"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSourceAcademicDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
            SelectCommand="SELECT DISTINCT [acad_year] FROM [dbo].[ms_acad_year]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSourceTermDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
            SelectCommand="SELECT DISTINCT term_name from ms_acad_year_term"></asp:SqlDataSource>
        <h1>List of School Expenses</h1>
        <div class="row">
            <div class="col-lg-12">
                <div class="float-e-margins">
                    <div class="col-md-8">
                        <div class=" col-lg-2">
                            <label>Year</label><br />
                            <asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList>
                        </div>
                        <div class=" col-lg-2">
                            <label>Month</label><br />
                            <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                       <%-- <br />
                        <div class=" col-lg-2">
                            <asp:Button ID="btnSearchExpenses" runat="server" Text="GO" type="search" CssClass="btn-primary" OnClick="btnSearchExpenses_Click" />
                        </div>--%>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <asp:GridView ID="GridViewExpenses" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example"
            OnRowEditing="GridViewExpenses_RowEditing"
            OnRowCancelingEdit="GridViewExpenses_RowCancelingEdit"
            OnRowUpdating="GridViewExpenses_RowUpdating"
            OnRowDeleting="GridViewExpenses_RowDeleting" EditRowStyle-CssClass="GridViewEditRow">
            <Columns>
                <asp:BoundField DataField="exp_id" HeaderText="Exp Id" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>

                <asp:TemplateField HeaderText="Expense Type">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlPaymentType" runat="server" CssClass="input-group" Text='<%# Bind("type_refund_payment") %>'>

                            <asp:ListItem>Payment</asp:ListItem>
                            <asp:ListItem>Refund</asp:ListItem>
                        </asp:DropDownList>

                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblPaymentRefundType" runat="server" Text='<%# Bind("type_refund_payment") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Category">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="input-group" Text='<%# Bind("category") %>' DataValueField="status_name" DataTextField="status_name" DataSourceID="SqlDataSourceCategory"></asp:DropDownList>

                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbltbCategory" runat="server" Text='<%# Bind("category") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Expense Date ">
                    <EditItemTemplate>
                        <asp:TextBox ID="tbExpensesDate" runat="server" Text='<%# Bind("exp_date") %>' CssClass="datepick input-group date"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblExpensesDate" runat="server" Text='<%# Bind("exp_date") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Amount">
                    <EditItemTemplate>
                        <asp:TextBox ID="tbAmount" runat="server" Text='<%# Bind("amount") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvtbAmount" ErrorMessage="*" ForeColor="Red" ControlToValidate="tbAmount"
                            runat="server" Dispaly="Dynamic" />
                        <asp:RegularExpressionValidator ID="revtbAmount" ControlToValidate="tbAmount" runat="server"
                            ErrorMessage="*" ForeColor="Red" ValidationExpression="^\d+([,\.]\d{1,2})?$"></asp:RegularExpressionValidator>
                        <asp:RangeValidator ID="RangeValidatorAmount" runat="server" ErrorMessage="Amount must be greater than zero" ForeColor="Red"
                            ControlToValidate="tbAmount" MinimumValue="0.01" MaximumValue="999999999.99"></asp:RangeValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("amount") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description">
                    <EditItemTemplate>
                        <asp:TextBox ID="tbdescription" runat="server" Text='<%# Bind("description") %>' CssClass="input-group "></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbltbdescription" runat="server" Text='<%# Bind("description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reciept Reference">
                    <EditItemTemplate>
                        <asp:TextBox ID="tbRecieptReference" runat="server" Text='<%# Bind("reciept_ref") %>' CssClass="input-group "></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbltbRecieptReference" runat="server" Text='<%# Bind("reciept_ref") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Acadamic Year">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlAcadamicYear" runat="server" CssClass="input-group" DataValueField="acad_year" DataTextField="acad_year"
                            Text='<%# Bind("acad_year") %>' DataSourceID="SqlDataSourceAcademicDropDown" AppendDataBoundItems="true">
                            <asp:ListItem Text="Select" Value="" />
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblAcadamicYear" runat="server" Text='<%# Bind("acad_year") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Term">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlTerm" runat="server" Text='<%# Bind("term") %>' DataValueField="term_name" DataTextField="term_name"
                            DataSourceID="SqlDataSourceTermDropDown" CssClass="input-group" AppendDataBoundItems="true">
                            <asp:ListItem Text="Select" Value="" />
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblTerm" runat="server" Text='<%# Bind("term") %>'></asp:Label>
                    </ItemTemplate>
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

                <%--<asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Button ID="btnStatus" runat="server"  CssClass=" btn-sm btn-default m-t-n-xs" OnClick="btnStatus_Click" CausesValidation="False"  Text='Make Active'/>
                    </ItemTemplate>
                </asp:TemplateField>--%>
            </Columns>
        </asp:GridView>
        <br />
        <div class="col-lg-offset-1"><asp:Label ID="lblZeroRecords" runat="server" Text=""></asp:Label></div>
    </div>

    <%--  --Pages Styles--%>
    <style>
        .required {
            color: #F00;
        }

        .GridViewEditRow input[type=text] {
            width: 90px;
        }
        /* size textboxes */
        .GridViewEditRow select {
            width: 100px;
        }
    </style>


</asp:Content>
