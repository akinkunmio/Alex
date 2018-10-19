<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="payments.aspx.cs" Inherits="Alex.pages.payments" MaintainScrollPositionOnPostback="true" %>

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
        $(function () { $('.datepick').prop('readonly', true).datepicker({ changeMonth: true, changeYear: true, yearRange: '-100:' + new Date().getFullYear(), dateFormat: 'dd/mm/yy', maxDate: new Date() }); });
        function CheckForm() {
            if ($('#<%=TbSearch.ClientID %>').val() == "") {
                alert('Please enter Name');
                return false;
            }
            return true;
        }
        function searchKeyPress(e) {
            e = e || window.event;
            if (e.keyCode == 13) {
                document.getElementById('btnSearch').click();
                return false;
            }
            return true;
        }
        function validateDate(sender, e) {

            <%-- // Split out the constituent parts (dd/mm/yyyy)    
            var dayfield = e.Value.split("/")[0];
            var monthfield = e.Value.split("/")[1];
            var yearfield = e.Value.split("/")[2];

            // Create a new date object based on the separate parts
            var dateValue = new Date(yearfield, monthfield - 1, dayfield)

            // Check that the date object's parts match the split out parts from the original string
            if ((dateValue.getMonth() + 1 != monthfield) || (dateValue.getDate() != dayfield) || (dateValue.getFullYear() != yearfield)) {
                e.IsValid = false;
            }

            // Check for future dates
            if (e.IsValid) {
                e.IsValid = dateValue <= new Date()
            }--%>
        }
    </script>


    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10 h1">
            Tuition Fee Payments
        </div>

        <div class="col-lg-2">
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row">
            <div class="col-lg-12">
                <div class="float-e-margins">
                    <div class="col-md-8">
                        <div class=" col-lg-2">
                            <label>Year</label><br />
                            <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class=" col-lg-2">
                            <label>Month</label><br />
                            <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-lg-4">
                            <label>Payment Method</label><br />
                            <asp:DropDownList ID="ddlPaymentMethod" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPaymentMethod_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <%--<br />
                        <div class=" col-lg-2">
                            <asp:Button ID="btnSearchPayments" runat="server" Text="GO" type="search" CssClass="btn-primary" OnClick="btnSearchPayments_Click" />
                        </div>--%>
                    </div>
                    <div class="col-md-4" runat="server">
                        <asp:Panel ID="panSearch" runat="server" DefaultButton="BtnSearch">
                            <asp:TextBox ID="TbSearch" runat="server" placeholder="Name or Date of Birth" name="search" onkeypress="return searchKeyPress(event);"></asp:TextBox>
                            <asp:Button ID="BtnSearch" runat="server" Text="Search" type="search" CssClass="btn-primary" OnClick="BtnSearch_Click" OnClientClick="return CheckForm()" />
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <br />

        <div id="divNumber" runat="server">
            <small>Date Search</small>
            <div class="NumberPager">
                <asp:Repeater ID="rptNumbers" runat="server">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" Text='<%#Eval("Value")%>' Visible='<%# !Convert.ToBoolean(Eval("Selected"))%>' OnClick="Number_Click" />
                        <asp:Label runat="server" Text='<%#Eval("Value")%>' Visible='<%# Convert.ToBoolean(Eval("Selected"))%>' />
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

        <br />

        <asp:SqlDataSource ID="SqlDataSourceBankDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
            SelectCommand="SELECT status_name FROM ms_status Where category = 'Bank' "></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSourcePaymentDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
            SelectCommand="sp_ms_payment_method_dropdown"></asp:SqlDataSource>
        <div id="divGrid" style="width: 100%; overflow: scroll">
            <label>Selected PaymentDate : </label>
            <asp:Label ID="lblDateSelected" runat="server"></asp:Label>
            <asp:GridView ID="GridViewPayments" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example"
                OnRowDataBound="GridViewPayments_RowDataBound"
                OnRowEditing="GridViewPayments_RowEditing"
                OnRowCancelingEdit="GridViewPayments_RowCancelingEdit"
                OnRowUpdating="GridViewPayments_RowUpdating"
                OnRowDeleting="GridViewPayments_RowDeleting" EditRowStyle-CssClass="GridViewEditRow"
                AllowPaging="true" OnPageIndexChanging="GridViewPayments_PageIndexChanging" PageSize="12"
                PagerSettings-Mode="NumericFirstLast" PagerSettings-PageButtonCount="10" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last">
                <PagerStyle BackColor="#1ab394" ForeColor="white" HorizontalAlign="Center" CssClass="cssPager" />
                <Columns>
                    <asp:BoundField DataField="person_id" HeaderText="PersonId" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                        <HeaderStyle CssClass="hidden"></HeaderStyle>
                        <ItemStyle CssClass="hidden"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Payment_id">
                        <EditItemTemplate>
                            <asp:Label ID="lblPaymentId" runat="server" Text='<%# Eval("payment_id") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblPaymentId" runat="server" Text='<%# Bind("payment_id") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="hidden" />
                        <ItemStyle CssClass="hidden" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLinkPeople" runat="server" Text='<%# Eval("fullName") %>'></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="Form_Class" HeaderText="Class" ReadOnly="true" />
                    <asp:BoundField DataField="fee_type" HeaderText="Fee Type" ReadOnly="true" />
                    <asp:TemplateField HeaderText="Amount Paid">
                        <EditItemTemplate>
                            <asp:TextBox ID="tbAmount" runat="server" Text='<%# Bind("Amount_Paid") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtbAmount" ErrorMessage="*" ForeColor="Red" ControlToValidate="tbAmount"
                                runat="server" Dispaly="Dynamic" />
                            <asp:RegularExpressionValidator ID="revtbAmount" ControlToValidate="tbAmount" runat="server"
                                ErrorMessage="*" ForeColor="Red" ValidationExpression="^(-)?(?=.*[1-9])(?:[1-9]\d*\.?|0?\.)\d*$"></asp:RegularExpressionValidator>

                        </EditItemTemplate>
                        <ItemTemplate>
                            <%#Eval("Amount_Paid")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Payment Received Date">
                        <EditItemTemplate>
                            <asp:TextBox ID="tbPaymentReceivedDate" runat="server" Text='<%# Bind("Payment_Received_Date") %>' CssClass="datepick input-group date"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtbDate" ErrorMessage=" Date required" ForeColor="Red" ControlToValidate="tbPaymentReceivedDate"
                                runat="server" Dispaly="Dynamic" />
                            <asp:CustomValidator runat="server" ID="valDateRange" ControlToValidate="tbPaymentReceivedDate" ForeColor="Red" ErrorMessage="enter valid date" ClientValidationFunction="validateDate" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <%#Eval("Payment_Received_Date")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Payment Method">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlPaymentMethod" runat="server" Text='<%# Bind("Payment_method") %>' CssClass="input-group" DataValueField="Payment_method" DataTextField="Payment_method"
                                DataSourceID="SqlDataSourcePaymentDropDown" SelectedValue='<%# Bind("Payment_method") %>' AppendDataBoundItems="true">
                            </asp:DropDownList>

                        </EditItemTemplate>
                        <ItemTemplate>
                            <%#Eval("Payment_method")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Payment Reference">
                        <EditItemTemplate>
                            <asp:TextBox ID="tbPaymentReference" runat="server" Text='<%# Bind("Payment_method_ref") %>' CssClass="input-group"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <%#Eval("Payment_method_ref")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bank Name">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlBankName" runat="server" Text='<%# Bind("bank_name") %>' CssClass="input-group" DataValueField="status_name" DataTextField="status_name"
                                DataSourceID="SqlDataSourceBankDropDown" SelectedValue='<%# Bind("bank_name") %>' AppendDataBoundItems="true">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <%#Eval("bank_name")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Invoice No">
                        <EditItemTemplate>
                            <asp:TextBox ID="tbInvoiceNo" runat="server" Text='<%# Bind("invoice_no") %>' CssClass="input-group"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <%#Eval("invoice_no")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Receipt No">
                        <EditItemTemplate>
                            <asp:TextBox ID="tbReceiptNo" runat="server" Text='<%# Bind("receipt_no") %>' CssClass="input-group"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <%#Eval("receipt_no")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>

                            <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="Edit" CausesValidation="False" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="lnkUpdate" runat="server" Text="Update" CommandName="Update" />
                            <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel" CommandName="Cancel" CausesValidation="False" />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete ?');" Text="Delete"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>

                <EditRowStyle CssClass="GridViewEditRow"></EditRowStyle>
            </asp:GridView>
        </div>
        <asp:Label ID="lblZeroRecords" runat="server" Text=""></asp:Label>
    </div>

    <style>
        .GridViewEditRow input[type=text] {
            width: 80px;
        }
        /* size textboxes 
       .GridViewEditRow select {width:80px;} */
        .GridViewEditRow select {
            width: 100px;
        }

        .cssPager td {
            padding-left: 6px;
            padding-right: 6px;
        }

        .cssPager span {
            background-color: #8A7F7E;
            font-size: 18px;
            color: #ddd;
            border: 1px solid #35383A;
        }

        .NumberPager a, .NumberPager span {
            font-size: 11pt;
            display: inline-block;
            line-height: 15px;
            min-width: 22px;
            text-align: center;
            text-decoration: none;
            font-weight: bold;
            padding: 0px 1px 0px 1px;
        }

        .NumberPager a {
            background-color: #f5f5f5;
            color: #969696;
            border: 1px solid #969696;
            width: 34px;
            height: 18px;
        }

        .NumberPager span {
            background-color: #A1DCF2;
            color: #000;
            border: 1px solid #3AC0F2;
        }
    </style>
</asp:Content>
