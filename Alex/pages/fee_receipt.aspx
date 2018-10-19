<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="fee_receipt.aspx.cs" Inherits="Alex.pages.fee_receipt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="iQ is a suite of integrated education management applications that facilitates the management of your school." />
    
    <link rel="shortcut icon" href="../images/favicon.png" />
    <title>iQ</title>

    <link href="../scripts/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../scripts/font-awesome/css/font-awesome.css" rel="stylesheet" />


    <link href="../scripts/css/animate.css" rel="stylesheet" />
    <link href="../scripts/css/style.css" rel="stylesheet" />

</head>
<body>
    <script type="text/javascript">
        function printDiv(print) {
            var printContents = document.getElementById(print).innerHTML;
            var originalContents = document.body.innerHTML;
            //document.body.innerHTML = printContents;
            document.body.innerHTML = "<html><head><title></title></head><body>" + printContents + "</body>";
            var ButtonControl = document.getElementById("btnPrint");
            var ButtonCloseWindow = document.getElementById("btnCloseWindow");
            ButtonControl.style.visibility = "hidden"; ButtonCloseWindow.style.visibility = "hidden";
            window.print();
            document.body.innerHTML = originalContents;
        }
    </script>
    <form id="form1" runat="server">

        <div id="print">
            <div class="ibox wrapper wrapper-content float-e-margins panel panel-primary">
                <div class="ibox-title panel-heading">
                    <h5>Fee Receipt</h5>
                    <div class=" pull-right">
                        <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn btn-success fa fa-print" OnClientClick="printDiv('print')" />
                        <asp:Button ID="btnCloseWindow" runat="server" Text="Close Tab" CssClass="btn btn-danger fa fa-print" OnClick="btnCloseWindow_Click" />
                    </div>
                </div>

                <div class="ibox-content p-xl">
                    <div class="row">
                        <div class="col-md-7 col-lg-offset-1">
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

                        <div id="divPayInfo" runat="server" class="col-lg-6  col-lg-offset-1">
                            <hr class="hr-line-solid" />
                            <h3>Payment Details</h3>
                            <div class=" ">
                                <label class="btn dim">Student Name</label><asp:Label ID="lblStuName" runat="server" Text="" CssClass="btn   dim"></asp:Label>
                                <label class="btn dim ">Academic Year:</label><asp:Label ID="lblAcademicYear" runat="server" Text="" CssClass="btn   dim"></asp:Label><br />
                                <label class="btn dim">Term:</label><asp:Label ID="lblTermName" runat="server" Text="" CssClass="btn   dim"></asp:Label>
                                <label class=" btn dim">Class:</label><asp:Label ID="lblFormName" runat="server" Text="" CssClass="btn   dim"></asp:Label>
                                <label class=" btn dim">Fee Type:</label><asp:Label ID="lblFeeType" runat="server" Text="" CssClass="btn   dim"></asp:Label>

                                <br />
                                <br />
                            </div>
                            <asp:DetailsView ID="DetailsViewPaymentInfo" runat="server" Height="50px" Width="400px"
                                CssClass="table table-striped table-bordered table-hover dataTables-example" AutoGenerateRows="False"
                                OnDataBound="DetailsViewPaymentInfo_DataBound">
                                <Fields>
                                    <%-- <asp:BoundField DataField="fullname" HeaderText="Student Name" ReadOnly="True"></asp:BoundField>
                                <asp:BoundField DataField="acad_year" HeaderText="Academic Year" ReadOnly="True"></asp:BoundField>
                                <asp:BoundField DataField="form_name" HeaderText="Class" ReadOnly="True"></asp:BoundField>
                                <asp:BoundField DataField="School Fees" HeaderText="Fees" ReadOnly="True"></asp:BoundField>--%>
                                    <asp:BoundField DataField="payment_method" HeaderText="Payment Method" ReadOnly="True"></asp:BoundField>
                                    <asp:BoundField DataField="bank_name" HeaderText="Bank Name" ReadOnly="True"></asp:BoundField>
                                    <asp:BoundField DataField="teller_no" HeaderText="Receipt No" ReadOnly="True"></asp:BoundField>
                                    <asp:BoundField DataField="Reference No / Invoice No" HeaderText="Reference No / Invoice No" ReadOnly="True"></asp:BoundField>
                                    <asp:BoundField DataField="Payment_Received_Date" HeaderText="Date Received" ReadOnly="True"></asp:BoundField>
                                    <asp:BoundField DataField="Amount_Paid" HeaderText="Amount Paid" ReadOnly="True"></asp:BoundField>
                                </Fields>
                            </asp:DetailsView>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <!-- Mainly scripts -->
        <script src="../../scripts/js/jquery-2.1.1.js"></script>
        <script src="../../scripts/js/bootstrap.min.js"></script>
        <script src="../../scripts/js/plugins/metisMenu/jquery.metisMenu.js"></script>
        <script src="../../scripts/js/plugins/slimscroll/jquery.slimscroll.min.js"></script>
        <script src="../../scripts/js/plugins/jeditable/jquery.jeditable.js"></script>

        <!-- Flot -->
        <script src="../../scripts/js/plugins/flot/jquery.flot.js"></script>
        <script src="../../scripts/js/plugins/flot/jquery.flot.tooltip.min.js"></script>
        <script src="../../scripts/js/plugins/flot/jquery.flot.spline.js"></script>
        <script src="../../scripts/js/plugins/flot/jquery.flot.resize.js"></script>
        <script src="../../scripts/js/plugins/flot/jquery.flot.pie.js"></script>
        <script src="../../scripts/js/plugins/flot/jquery.flot.symbol.js"></script>
        <script src="../../scripts/js/plugins/flot/curvedLines.js"></script>

        <!-- Peity -->
        <script src="../../scripts/js/plugins/peity/jquery.peity.min.js"></script>
        <script src="../../scripts/js/demo/peity-demo.js"></script>

        <!-- Custom and plugin javascript -->
        <script src="../../scripts/js/custom.js"></script>
        <script src="../../scripts/js/plugins/pace/pace.min.js"></script>

        <!-- jQuery UI -->
        <script src="../../scripts/js/plugins/jquery-ui/jquery-ui.min.js"></script>

        <!-- GITTER -->
        <script src="../../scripts/js/plugins/gritter/jquery.gritter.min.js"></script>

        <!-- Sparkline -->
        <script src="../../scripts/js/plugins/sparkline/jquery.sparkline.min.js"></script>

        <!-- Sparkline demo data  -->
        <script src="../../scripts/js/demo/sparkline-demo.js"></script>

        <!-- ChartJS-->
        <script src="../../scripts/js/plugins/chartJs/Chart.min.js"></script>

        <!-- Toastr -->
        <script src="../../scripts/js/plugins/toastr/toastr.min.js"></script>

    </form>
</body>
</html>
