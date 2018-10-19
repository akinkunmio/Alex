<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="FeePay.aspx.cs" Inherits="Alex.pages.FeePay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../scripts/css/pagestyle.css" rel="stylesheet" />
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
        function printDiv(print) {
            var printContents = document.getElementById(print).innerHTML;
            var originalContents = document.body.innerHTML;
            //document.body.innerHTML = printContents;
            document.body.innerHTML = "<html><head><title></title></head><body>" + printContents + "</body>";
            window.print();
            document.body.innerHTML = originalContents;
        }
    </script>
    <script type="text/javascript">
        $(function () { $('.datepick').datepicker({ changeMonth: true, changeYear: true, yearRange: '-100:' + new Date().getFullYear(), dateFormat: 'dd/mm/yy', maxDate: new Date() }); });
        $().ready(function () {
            $('.list').change(function () {
                if (this.value == 'Bank') {
                    $(".bank").show();
                    var validator = document.getElementById("<%=rfvddlBankName.ClientID %>");
                    validator.enabled = true;
                    var Verifyvalidator = document.getElementById("<%=rfvddlBankVerify.ClientID %>");
                    Verifyvalidator.enabled = true;
                }
                else {
                    $(".bank").hide();
                    var validator = document.getElementById("<%=rfvddlBankName.ClientID %>");
                    validator.enabled = false;
                    var Verifyvalidator = document.getElementById("<%=rfvddlBankVerify.ClientID %>");
                    Verifyvalidator.enabled = false;
                }
            });
        });
        function validateDate(sender, e) {

            // Split out the constituent parts (dd/mm/yyyy)    
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
            }
        }

      function enableButton() {
          document.getElementById("<%=btnSubmit.ClientID %>").disabled = false;
      }

    </script>

    <div class="wrapper wrapper-content fadeInRight">
        <asp:Label ID="lblPhoneNumber" runat="server" Text="" Visible="false"></asp:Label>

        <div class="col-md-3 pull-right">
            <br />
            <asp:Button runat="server" ID="btnOK" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Text="&nbsp;Back to Account(s) Summary" OnClick="Result_Click" CausesValidation="False" />
        </div>

        <div id="divDetails" class="row" runat="server">
            <div class="col-lg-6">
                <h1>Fee Details</h1>
                <asp:DetailsView ID="DetailsViewPayment" runat="server" Height="50px" Width="400px" CssClass="table table-striped table-bordered table-hover dataTables-example" AutoGenerateRows="False">
                    <Fields>
                        <asp:BoundField DataField="Name" HeaderText="Student Name" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="Fees" HeaderText="Fees" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="Total" HeaderText="Total Fees Paid" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="SBalance" HeaderText="Balance" ReadOnly="True"></asp:BoundField>

                    </Fields>
                </asp:DetailsView>
            </div>
            <%--<div id="divPayInfo" runat="server" class="col-lg-6">
            <h1>Payment Details</h1>
            <asp:DetailsView ID="DetailsViewPaymentInfo" runat="server" Height="50px" Width="400px" CssClass="table table-striped table-bordered table-hover dataTables-example" AutoGenerateRows="False">
                <Fields>
                    <asp:BoundField DataField="amount" HeaderText="Amount Paid now" ReadOnly="True"></asp:BoundField>
                    <asp:BoundField DataField="teller_no" HeaderText="Receipt No" ReadOnly="True"></asp:BoundField>
                    <asp:BoundField DataField="receive_date" HeaderText="Date Received" ReadOnly="True"></asp:BoundField>
                    <asp:BoundField DataField="payment_method" HeaderText="Payment Method" ReadOnly="True"></asp:BoundField>
                    <asp:BoundField DataField="payment_method_ref" HeaderText="Payment Reference No" ReadOnly="True"></asp:BoundField>
                    <asp:BoundField DataField="bank_name" HeaderText="Bank Name" ReadOnly="True"></asp:BoundField>
                    <asp:BoundField DataField="invoice_no" HeaderText="Invoivce No" ReadOnly="True"></asp:BoundField>
                </Fields>
            </asp:DetailsView>
        </div>--%>
        </div>
        <div id="divPayFields" class="form-group row" runat="server">
            <div class="col-lg-6">
                <div class="form-group">
                    <div class="editor-label">
                        <label>Paying Amount ₦ <strong class="required">*</strong></label>
                    </div>
                    <div class="editor-field ">
                        <asp:TextBox ID="tbPayingAmount" runat="server" placeholder="Enter Amount" CssClass="form-control" ></asp:TextBox>
                        <div class="pull-right">
                            <asp:RegularExpressionValidator ID="revtbPayingAmount" ControlToValidate="tbPayingAmount" runat="server"
                                ErrorMessage="Invalid Amount" ForeColor="Red" ValidationExpression="^-?([\d\,]+(\.\d{1,2})?|\.\d{1,2})\s*$"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="rfvLName" ErrorMessage="Amount required" ForeColor="Red" ControlToValidate="tbPayingAmount"
                                runat="server" Dispaly="Dynamic" />

                        </div>

                    </div>
                </div>
                <div class="form-group">
                    <div class="editor-label">
                        <label>Date<strong class="required">*</strong></label>
                    </div>
                    <div class="editor-field">
                        <asp:TextBox ID="tbDate" runat="server" placeholder="dd/mm/yyyy" CssClass="form-control datepick "></asp:TextBox>
                        <div class="pull-right">
                            <asp:RequiredFieldValidator ID="rfvtbDate" ErrorMessage=" Date required" ForeColor="Red" ControlToValidate="tbDate"
                                runat="server" Dispaly="Dynamic" />
                            <asp:CustomValidator runat="server" ID="valDateRange" ControlToValidate="tbDate" ForeColor="Red" ErrorMessage="enter valid date" ClientValidationFunction="validateDate" />
                        </div>


                    </div>
                </div>
                <div class="form-group">
                    <div class="editor-label">
                        <label>Payment Method<strong class="required">*</strong></label>
                    </div>
                    <div class="editor-field">
                        <asp:DropDownList ID="ddlPaymentMethod" runat="server" CssClass="form-control list">
                            <asp:ListItem Value="">Please Select One</asp:ListItem>
                            <asp:ListItem Value="Bank">Bank</asp:ListItem>
                            <asp:ListItem Value="Cash">Cash</asp:ListItem>
                            <asp:ListItem Value="Cheque">Cheque</asp:ListItem>
                            <asp:ListItem Value="Scholarship/Discount">Scholarship/Discount</asp:ListItem>
                            <asp:ListItem Value="Bad Debt/Write Off">Bad Debt/Write Off</asp:ListItem>
                            <asp:ListItem Value="Other">Other</asp:ListItem>
                        </asp:DropDownList>
                        <div class="pull-right">
                            <asp:RequiredFieldValidator ID="rfvddlPaymentMethod" ErrorMessage="Payment Method Required" ForeColor="Red" ControlToValidate="ddlPaymentMethod"
                                runat="server" Dispaly="Dynamic" />
                        </div>
                    </div>

                </div>
                <div class="form-group bank">
                    <div class="editor-label">
                        <label>Bank Name<strong class="required">*</strong></label>
                    </div>
                    <div class="editor-field">
                        <asp:DropDownList ID="ddlBankName" runat="server" CssClass="form-control"></asp:DropDownList>
                        <div class="pull-right">
                            <asp:RequiredFieldValidator ID="rfvddlBankName" ErrorMessage=" Bank Name required" ForeColor="Red" ControlToValidate="ddlBankName"
                                runat="server" Dispaly="Dynamic" />
                        </div>
                    </div>
                </div>

                <div class="form-group bank">
                    <div class="editor-label">
                        <label>Bank Payment status<strong class="required">*</strong></label>
                    </div>
                    <div class="editor-field">
                        <asp:DropDownList ID="ddlBankVerify" runat="server" CssClass="form-control">
                            <asp:ListItem Value="">Please Select One</asp:ListItem>
                            <asp:ListItem Value="Verified" Selected="True">Verified</asp:ListItem>
                            <asp:ListItem Value="Not Verified">Not Verified</asp:ListItem>
                        </asp:DropDownList>
                        <div class="pull-right">
                            <asp:RequiredFieldValidator ID="rfvddlBankVerify" ErrorMessage="status required" ForeColor="Red" ControlToValidate="ddlBankVerify"
                                runat="server" Dispaly="Dynamic" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="editor-label">
                        <label>Payment Method Reference<strong class="required">*</strong></label>
                    </div>
                    <div class="editor-field">
                        <asp:TextBox ID="tbPMReference" runat="server" placeholder="Enter Reference Number" CssClass="form-control"></asp:TextBox>
                        <div class="pull-right">
                            <asp:RequiredFieldValidator ID="rfvtbPMReference" ErrorMessage=" Reference required" ForeColor="Red" ControlToValidate="tbPMReference"
                                runat="server" Dispaly="Dynamic" />
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <div class="editor-label">
                        <label>Invoice Number</label>
                    </div>
                    <div class="editor-field">
                        <asp:TextBox ID="tbInvoiceNumber" runat="server" placeholder="Enter Invoice Number" CssClass="form-control"></asp:TextBox>

                    </div>
                </div>
                <div class="form-group">
                    <div class="editor-label">
                        <label>Receipt Number</label>
                    </div>
                    <div class="editor-field">
                        <asp:TextBox ID="tbReceiptNumber" runat="server" placeholder="Enter Receipt Number" CssClass="form-control"></asp:TextBox>

                    </div>
                </div>

                <asp:Button ID="btnSubmit" runat="server" Text="Pay" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" OnClick="FeePay_Click"
                    OnClientClick="this.disabled = true; setTimeout('enableButton()', 1600);"  UseSubmitBehavior="false" ClientIDMode="Static" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" OnClick="btnCancel_Click" CausesValidation="False" />
                <br />
            </div>
        </div>

        <asp:Label runat="server" ID="lblResult"></asp:Label><br />
        <br />

        <div id="DivToPrint" runat="server" visible="false">

            <div class="row">
                <div class="col-lg-12">
                    <div class="col-lg-offset-9">
                        <asp:Button ID="btnPrint" runat="server" Text="Print" Visible="false" class="btn btn-primary fa fa-print" OnClientClick="printDiv('print')" />
                    </div>
                </div>
            </div>
            <br />
            <div id="print">
                <div class="ibox float-e-margins panel panel-primary">
                    <div class="ibox-title panel-heading">
                        <h5>Pay Fee Details</h5>
                    </div>
                    <div class="ibox-content p-xl">
                        <div class="row">
                            <div class="col-md-7 col-lg-offset-2">
                                <div class="glyphicon" style="vertical-align: top">
                                    <asp:Image ID="imgSchool" runat="server" length="120px" Width="120px" AlternateText="Image" />
                                </div>
                                <div class="text-center glyphicon">
                                    <asp:Label ID="lblSchoolName" runat="server" Text="" CssClass="title h1"></asp:Label><br />
                                    <asp:Label ID="lblAddress" runat="server" Text="" CssClass="h4"></asp:Label><br />
                                    <asp:Label ID="lblCity" runat="server" Text="" CssClass="h4"></asp:Label>,
                                 <asp:Label ID="lblState" runat="server" Text="" CssClass="h4"></asp:Label><br />
                                    <asp:Label ID="lblCountry" runat="server" Text="" CssClass="h4"></asp:Label>,
                                 <asp:Label ID="lblPostCode" runat="server" Text="" CssClass="h4"></asp:Label><br />
                                    <i class="fa fa-envelope-square"></i>
                                    <asp:Label ID="lblEmail" runat="server" Text="" CssClass="h4"></asp:Label>,
                                 <i class="fa fa-phone-square"></i>
                                    <asp:Label ID="lblPhoneNo" runat="server" Text="" CssClass="h4"></asp:Label>
                                </div>
                            </div>
                        </div>


                        <hr />

                        <div class="row">
                            <div class="col-lg-6">
                                <h1>Fee Details</h1>
                                <asp:DetailsView ID="DetailsViewPayment1" runat="server" Height="50px" Width="400px" CssClass="table table-striped table-bordered table-hover dataTables-example" AutoGenerateRows="False">
                                    <Fields>
                                        <asp:BoundField DataField="Name" HeaderText="Student Name" ReadOnly="True"></asp:BoundField>
                                        <asp:BoundField DataField="Fees" HeaderText="Fees" ReadOnly="True"></asp:BoundField>
                                        <asp:BoundField DataField="Total" HeaderText="Total Fees Paid" ReadOnly="True"></asp:BoundField>
                                        <asp:BoundField DataField="SBalance" HeaderText="Balance" ReadOnly="True"></asp:BoundField>

                                    </Fields>
                                </asp:DetailsView>
                                <asp:Label ID="lblResponce" runat="server"></asp:Label>
                            </div>
                            <div id="divPayInfo" runat="server" class="col-lg-6">
                                <h1>Payment Details</h1>
                                <asp:DetailsView ID="DetailsViewPaymentInfo" runat="server" Height="50px" Width="400px" OnDataBound="DetailsViewPaymentInfo_OnDataBound"
                                    CssClass="table table-striped table-bordered table-hover dataTables-example" AutoGenerateRows="False">
                                    <Fields>
                                        <asp:BoundField DataField="amount" HeaderText="Amount Paid now" ReadOnly="True"></asp:BoundField>
                                        <asp:BoundField DataField="teller_no" HeaderText="Receipt No" ReadOnly="True"></asp:BoundField>
                                        <asp:BoundField DataField="receive_date" HeaderText="Date Received" ReadOnly="True"></asp:BoundField>
                                        <asp:BoundField DataField="payment_method" HeaderText="Payment Method" ReadOnly="True"></asp:BoundField>
                                        <asp:BoundField DataField="payment_method_ref" HeaderText="Payment Reference No" ReadOnly="True"></asp:BoundField>
                                        <asp:BoundField DataField="bank_name" HeaderText="Bank Name" ReadOnly="True"></asp:BoundField>
                                        <asp:BoundField DataField="invoice_no" HeaderText="Invoivce No" ReadOnly="True"></asp:BoundField>
                                    </Fields>
                                </asp:DetailsView>
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
        </div>

        <asp:Label runat="server" ID="lblSmsSchoolName" Visible="false"></asp:Label>
    </div>
    <style>
        .editor-label .required {
            color: #F00;
        }
    </style>

</asp:Content>
