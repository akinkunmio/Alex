<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="verify_payments.aspx.cs" Inherits="Alex.pages.verify_payments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function CheckAllEmp(Checkbox) {
            var GridViewProcessPayments = document.getElementById("<%=GridViewBnkPayVerify.ClientID %>");
            for (i = 1; i < GridViewBnkPayVerify.rows.length; i++) {
                GridViewBnkPayVerify.rows[i].cells[3].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
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
        $(function () { $('.datepick').datepicker({ changeMonth: true, changeYear: true, yearRange: '-100:' + new Date().getFullYear(), dateFormat: 'dd/mm/yy' }); });
        
        $().ready(function () {
            $('.list').change(function () {
                if (this.value == 'Bank') {
                    $(".bank").show();
                    var validator = document.getElementById("<%=rfvddlBankName.ClientID %>");
                    validator.enabled = true;
                }
                else {
                    $(".bank").hide();
                    var validator = document.getElementById("<%=rfvddlBankName.ClientID %>");
                    validator.enabled = false;
                }
            });
     });

    </script>

    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10 h1">
            Verify Payments
        </div>
   </div>
    <div class="wrapper wrapper-content animated fadeInRight">
       <div class="row">
            <%--<div class="col-lg-8">
                <div class="float-e-margins">
                    <div class="col-md-14">
                        <div class=" col-lg-2">
                            <label>Bank</label><br />
                            <asp:DropDownList ID="ddlBankName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBankName_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                     </div>
                </div>

            </div>--%>
      
            <div class="col-lg-8">
                <div class="float-e-margins">
                     <div class="form-group">
                        <div class="editor-label">
                            <label>Status</label>
                        </div>
                        <div class="editor-field">
                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="">Please Select</asp:ListItem>
                                <asp:ListItem Value="All" Selected="True">All</asp:ListItem>
                                <asp:ListItem Value="Verified">Verified</asp:ListItem>
                                <asp:ListItem Value="Not Verified">Not Verified</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="editor-label">
                            <label>Payment Method</label>
                        </div>
                        <div class="editor-field">
                            <asp:DropDownList ID="ddlPaymentMethod" runat="server" CssClass="form-control list" OnSelectedIndexChanged="ddlPaymentMethod_SelectedIndexChanged" AutoPostBack="True" >
                                <asp:ListItem Value="">Please Select One</asp:ListItem>
                                <asp:ListItem Value="Bank">Bank</asp:ListItem>
                                <asp:ListItem Value="Cash">Cash</asp:ListItem>
                                <asp:ListItem Value="Cheque">Cheque</asp:ListItem>
                                <asp:ListItem Value="Scholarship/Discount">Scholarship/Discount</asp:ListItem>
                                <asp:ListItem Value="Bad Debt/Write Off">Bad Debt/Write Off</asp:ListItem>
                                <asp:ListItem Value="Other">Other</asp:ListItem>
                                <asp:ListItem Value="All">All</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                      </div>
                        <div class="form-group bank" id="bank" runat="server">
                            <div class="editor-label">
                                <label>Bank Name<strong class="required">*</strong></label>
                            </div>
                            <div class="editor-field">
                                <asp:DropDownList ID="ddlBankName" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlBankName_SelectedIndexChanged1"></asp:DropDownList>
                                <div class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvddlBankName" ErrorMessage="Bank Name required for Bank Payment Method " ForeColor="Red" ControlToValidate="ddlBankName"
                                        runat="server"  />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="editor-label">
                                <label>From Date</label>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="tbStartDate" runat="server" CssClass="form-control datepick" placeholder="dd/mm/yyyy" AutoPostBack="true" OnTextChanged="tbStartDate_TextChanged"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="editor-label">
                                <label>To Date</label>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="tbEndDate" runat="server" CssClass="form-control datepick" placeholder="dd/mm/yyyy" AutoPostBack="true" OnTextChanged="tbEndDate_TextChanged"></asp:TextBox>
                            </div>
                        </div>
                       <%-- <asp:Button ID="BtnSearchPayments" runat="server" Text="GO" type="search" CssClass="btn-primary" OnClick="BtnSearchPayments_Click"  />--%>
                    </div>
                   
                </div>
       
        <div id="divProcessNow" runat="server" visible="false"  class="col-lg-4">

                <asp:Button ID="btnBatchPayVerify" runat="server" Text="Verify Now" CssClass="col-lg-offset-1 btn-primary" OnClick="btnBatchPayVerify_Click"  OnClientClick="return Validate_Checkbox()" />
        </div>
   </div>    
    
        <asp:GridView ID="GridViewBnkPayVerify" runat="server" CssClass="table table-striped table-bordered table-hover dataTables-example"
            AutoGenerateColumns="False" OnRowDataBound="GridViewBnkPayVerify_RowDataBound"
             ShowFooter="false">
           
            <Columns>
                <asp:TemplateField ItemStyle-Width="40px">
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkboxSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkboxSelectAll_CheckedChanged" />
                    </HeaderTemplate>

                    <ItemTemplate>
                        <asp:CheckBox ID="chkStudent" runat="server"></asp:CheckBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="person_id" HeaderText="person_id" InsertVisible="False" ReadOnly="True" SortExpression="person_id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="pid_fees" HeaderText="pid_fees" InsertVisible="False" ReadOnly="True" SortExpression="pid_fees" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="pid_purch" HeaderText="pid_purch" InsertVisible="False" ReadOnly="True" SortExpression="pid_purch" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="received_date" HeaderText="Received Date" />
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLinkPeople" runat="server" Text='<%# Eval("fullName") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="amount" HeaderText="Paid Amount" />
                <asp:BoundField DataField="payment_method" HeaderText="Payment Method" />
                <asp:BoundField DataField="bank_name" HeaderText="Bank Name" />
                <asp:BoundField DataField="receipt_no" HeaderText="Receipt No" />
                <asp:BoundField DataField="status" HeaderText="Status" />

            </Columns>
        </asp:GridView>



        <br />

    </div>
    <br />
    <div class="col-md-6">
        <asp:Label ID="lblZeroRecords" runat="server" Text=""></asp:Label>
    </div>
     <style type="text/css">
        .required {
            color: #F00;
        }
    </style>

</asp:Content>
