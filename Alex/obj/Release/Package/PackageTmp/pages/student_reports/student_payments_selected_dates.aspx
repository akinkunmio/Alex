<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="student_payments_selected_dates.aspx.cs" Inherits="Alex.pages.student_reports.student_payments_selected_dates" %>
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
           Accounts (Tuition Fee): <small>Tuition Fee Payments</small>
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
                            <label>Payment Method</label>
                        </div>
                        <div class="editor-field">
                            <asp:DropDownList ID="ddlPaymentMethod" runat="server" CssClass="form-control list">
                               
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
                        <asp:Button ID="BtnSearchFeePayments" runat="server" Text="GO" type="search" CssClass="btn-primary" OnClick="BtnSearchFeePayments_Click" CausesValidation="false" />
                    </div>
                    <div class="col-lg-offset-9">
                        <asp:Button ID="btnPrint" runat="server" Text="Print" Visible="false" CssClass="btn btn-primary fa fa-print" OnClientClick="printDiv('print')" />
                        <asp:Button ID="btnExcelDownload" runat="server" CssClass="btn btn-outline btn-primary" Text="Downlaod" OnClick="btnExcelDownload_Click" CausesValidation="false"  />
                    </div>
                </div>
            </div>
        <br />
        
        <div id="print">
            <div id="DivStudents" runat="server" visible="false" class="ibox float-e-margins panel panel-primary">
                <div class="ibox-title panel-heading">
                    <h5>Tuition Fee Payments </h5>
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
                                <h3>Tuition Fee Payments</h3>
                                 <label>PaymentMethod</label>:
                                <asp:Label ID="lblMethodSelected" runat="server" Text=""></asp:Label>
                                 <label>From</label>
                                :<asp:Label ID="lblDateSelected" runat="server" Text=""></asp:Label>
                                 <label>To</label>
                                :<asp:Label ID="lblEndDateSelected" runat="server" Text=""></asp:Label></div>
                            
                        </div>

                    </div>
                    <br />
                   
                   <hr />
                    <asp:GridView ID="GridViewFeePayments" runat="server" AutoGenerateColumns="False"
                        CssClass="table table-striped table-bordered table-hover dataTables-example">
                        <Columns>
                            <asp:BoundField DataField="fullname" HeaderText="Name" />
                            <asp:BoundField DataField="gender" HeaderText="Gender" />
                            <asp:BoundField DataField="acad_year" HeaderText="Academic Year" />
                            <asp:BoundField DataField="term_name" HeaderText="Term" />
                            <asp:BoundField DataField="Form_class" HeaderText="Class" />
                            <asp:BoundField DataField="Amount_Paid" HeaderText="Amount Paid" />
                            <asp:BoundField DataField="Payment_Received_Date" HeaderText="Date" />
                            <asp:BoundField DataField="payment_method_ref" HeaderText="Payment Reference" />
                            <asp:BoundField DataField="invoice_no" HeaderText="Invoice" />
                            <asp:BoundField DataField="receipt_no" HeaderText="Receipt" />
                            <asp:BoundField DataField="payment_method" HeaderText="Payment Method" />
                            <asp:BoundField DataField="bank_name" HeaderText="Bank" />
                        </Columns>
                    </asp:GridView>
                    <br />

                   
                </div>
            </div>
        </div>
         <asp:Label ID="lblZeroRecords" runat="server" Text=""></asp:Label>
    </div>

</asp:Content>