<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="batch_fee_payments.aspx.cs" Inherits="Alex.pages.batch_fee_payments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
        function CheckAllEmp(Checkbox) {
            var GridViewProcess = document.getElementById("<%=GridViewBatchPayments.ClientID %>");
            for (i = 1; i < GridViewBatchPayments.rows.length; i++) {
                GridViewBatchPayments.rows[i].cells[3].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
        function Validate_Checkbox() {
            var chks = document.getElementsByTagName('input');
            var hasChecked = false;
            for (var i = 0; i < chks.length; i++) {
                if (chks[i].checked) {
                    hasChecked = true;
                    break;
                }
            }
            if (hasChecked == false) {
                alert("Please select at least one Student");
                return false;
            }

            return true;
        }
    </script>

    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10 h1">
            Batch Fee Payments
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row">
            <div class="col-lg-12">
                <div class="float-e-margins">
                    <div class="col-md-12">

                        <div class=" col-lg-2">
                            <label>Academic Year</label><br />
                            <asp:DropDownList ID="ddlAcademicYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-lg-2">
                            <label>Term</label><br />
                            <asp:DropDownList ID="ddlTerm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTerm_SelectedIndexChanged"></asp:DropDownList>
                        </div>

                        <div class="col-lg-2">
                            <label>Class:</label><br />
                            <asp:DropDownList ID="ddlFormClass" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFormClass_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                </div>

            </div>
        </div>
        <asp:Button ID="BtnBlukPayFee" runat="server" Text="Pay Now" CssClass=" pull-right btn-primary" OnClientClick="return Validate_Checkbox()" OnClick="BtnBlukPayFee_Click"  />
         <asp:SqlDataSource ID="SqlDataSourceBankBulkPur" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
                      SelectCommand="select status_name from ms_status where category = 'Bank'"></asp:SqlDataSource>
        <asp:GridView ID="GridViewBatchPayments" runat="server" CssClass="table table-striped table-bordered table-hover dataTables-example"
            AutoGenerateColumns="False">
            <PagerStyle BackColor="#1ab394" ForeColor="white" HorizontalAlign="Center" CssClass="cssPager" />
            <Columns>
                <asp:TemplateField ItemStyle-Width="40px">
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkboxSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkboxSelectAll_CheckedChanged" />
                    </HeaderTemplate>

                    <ItemTemplate>
                        <asp:CheckBox ID="chkStudent" runat="server" AutoPostBack="true" OnCheckedChanged="chkStudent_CheckedChanged"></asp:CheckBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="person_id" HeaderText="person_id" InsertVisible="False" ReadOnly="True" SortExpression="person_id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="reg_id" HeaderText="reg_id" InsertVisible="False" ReadOnly="True" SortExpression="reg_id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField DataField="Fees" HeaderText="Fee" />
                <asp:BoundField DataField="Fees_Total_Amount_Paid" HeaderText="Total Paid" />
                <asp:BoundField DataField="SBalance" HeaderText="Balance" />
                <asp:TemplateField HeaderText="Paying Amount" ItemStyle-Width="20">
                    <ItemTemplate>
                        <asp:TextBox ID="tbBatchFeeAmount" runat="server" Text='<%# Bind("Fee_Balance") %>' Width="60"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date" ItemStyle-Width="20">
                    <ItemTemplate>
                        <asp:TextBox ID="tbBatchFeeDate" runat="server" Text='<%# Bind("TodayDate") %>' Width="80"  CssClass="datepick"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Payment Method">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlPaymentMethod" runat="server"  Width="100">
                            <asp:ListItem Value="Cash">Cash</asp:ListItem>
                            <asp:ListItem Value="Bank">Bank</asp:ListItem>
                            <asp:ListItem Value="Cheque">Cheque</asp:ListItem>
                            <asp:ListItem Value="Scholarship/Discount">Scholarship/Discount</asp:ListItem>
                            <asp:ListItem Value="Bad Debt/Write Off">Bad Debt/Write Off</asp:ListItem>
                            <asp:ListItem Value="Other">Other</asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Bank Name">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlBankBulkFeePay" runat="server" DataValueField="status_name" DataTextField="status_name" DataSourceID="SqlDataSourceBankBulkPur" AppendDataBoundItems="true">
                            <asp:ListItem Text="Select" Value="" />
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Payment status">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlBankVerify" runat="server">
                            <asp:ListItem Value="Verified" Selected="True">Verified</asp:ListItem>
                            <asp:ListItem Value="Not Verified">Not Verified</asp:ListItem>
                        </asp:DropDownList>

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reference" ItemStyle-Width="20">
                    <ItemTemplate>
                        <asp:TextBox ID="tbBulkFeePayReference" runat="server" Text="" Width="60"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Invoice Number" ItemStyle-Width="20">
                    <ItemTemplate>
                        <asp:TextBox ID="tbBulkInvNo" runat="server" Text="" Width="60"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Receipt Number" ItemStyle-Width="20">
                    <ItemTemplate>
                        <asp:TextBox ID="tbBulkRecNo" runat="server" Text="" Width="60"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>


            </Columns>
        </asp:GridView>
        <asp:Label ID="lblResponce" runat="server"></asp:Label>
        <asp:Label runat="server" ID="lblZeroRecords"></asp:Label>
        <asp:Label runat="server" ID="lblSmsSchoolName" Visible="false"></asp:Label>
        <asp:Label ID="lblPhoneNumber" runat="server" Text="" Visible="false"></asp:Label>
        </div>
    
</asp:Content>
