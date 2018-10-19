<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="saleItem_paymentmethod.aspx.cs" Inherits="Alex.pages.student_reports.saleItem_paymentmethod" %>

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
        $(function () { $('.datepick').datepicker({ changeMonth: true, changeYear: true, yearRange: '-100:' + new Date().getFullYear(), dateFormat: 'dd/mm/yy' }); });
        function printDiv(print) {
            var printContents = document.getElementById(print).innerHTML;
            var originalContents = document.body.innerHTML;
            //document.body.innerHTML = printContents;
            document.body.innerHTML = "<html><head><title></title></head><body>" + printContents + "</body>";
            window.print();
            document.body.innerHTML = originalContents;
        }
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
    <div class="row wrapper white-bg">
        <div class="col-lg-9 h1">
            Accounts (Sales): <small>Sold Items Payment List</small>
        </div>
        <div class="col-md-2 ">
            <a href="../reports.aspx">
                <br />
                <asp:Label runat="server" Text="&nbsp;Back to Reports" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"></asp:Label>
            </a>
        </div>
        <br />
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">

        <div class="row">
            <div class="col-lg-8">
                <div class="float-e-margins">
                     <div class="form-group">
                        <div class="editor-label">
                            <label>Item Name</label>
                        </div>
                        <div class="editor-field">
                            <asp:DropDownList ID="ddlItemName" runat="server" CssClass="form-control"></asp:DropDownList>
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
                                <asp:DropDownList ID="ddlBankName" runat="server" CssClass="form-control"></asp:DropDownList>
                                <div class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvddlBankName" ErrorMessage="Bank Name required for Bank Payment Method " ForeColor="Red" ControlToValidate="ddlBankName"
                                        runat="server" Dispaly="Dynamic" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="editor-label">
                                <label>From Date</label>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="tbStartDate" runat="server" CssClass="form-control datepick" placeholder="dd/mm/yyyy"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="editor-label">
                                <label>To Date</label>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="tbEndDate" runat="server" CssClass="form-control datepick" placeholder="dd/mm/yyyy"></asp:TextBox>
                            </div>
                        </div>
                        <asp:Button ID="BtnSearchPurchases" runat="server" Text="GO" type="search" CssClass="btn-primary" OnClick="BtnSearchPurchases_Click" />
                    </div>
                    <div class="col-lg-offset-9">
                        <asp:Button ID="btnPrint" runat="server" Text="Print" Visible="false" CssClass="btn btn-primary fa fa-print" OnClientClick="printDiv('print')" />
                    </div>
                </div>
            </div>
            <br />

            <div id="print">
                <div id="DivStudents" runat="server" visible="false" class="ibox float-e-margins panel panel-primary">
                    <div class="ibox-title panel-heading">
                        <h5>Sold Items Payment List</h5>
                    </div>
                    <div class="ibox-content p-xl">
                        <div class="row">
                            <div class="col-lg-2">

                                <asp:Image ID="imgSchool" runat="server" length="160px" Width="160px" AlternateText="Image" />
                            </div>
                            <div class="col-lg-8 col-lg-offset-2">
                                <div class="title h1">
                                    <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                                </div>
                               <div class="col-lg-offset-1">
                                    <h3>Sold Items List of Payments</h3>
                                    <label>PaymentMethod</label>:
                                <asp:Label ID="lblItemSelected" runat="server" Text=""></asp:Label>
                                 
                                    <label>From</label>
                                    :<asp:Label ID="lblDateSelected" runat="server" Text=""></asp:Label>
                                    <label>To</label>
                                    :<asp:Label ID="lblEndDateSelected" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            
                        </div>
                        <br />
                        <br />
                       

                        <hr />
                        <asp:GridView ID="GridViewPurchases" runat="server" AutoGenerateColumns="False"
                            CssClass="table table-striped table-bordered table-hover dataTables-example">
                            <Columns>
                                <asp:BoundField DataField="Payment_Received_Date" HeaderText="Received Date" />
                                <asp:BoundField DataField="fullname" HeaderText="Name" />
                                <asp:BoundField DataField="item_name" HeaderText="Item Name" />
                                <asp:BoundField DataField="Amount_Paid#" HeaderText="Amount Paid" />
                                <asp:BoundField DataField="invoice_no" HeaderText="Invoice No" />
                                <asp:BoundField DataField="receipt_no" HeaderText="Receipt No" />
                                <asp:BoundField DataField="bank_name" HeaderText="Bank Name" />
                                <asp:BoundField DataField="payment_method" HeaderText="Payment Method" />

                            </Columns>
                        </asp:GridView>
                        <br />
                       
                    </div>
                </div>

            </div> <asp:Label ID="lblZeroRecords" runat="server" Text=""></asp:Label>
        </div>
</asp:Content>
