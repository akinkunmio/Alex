<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="student_payments.aspx.cs" Inherits="Alex.pages.student_reports.student_payments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function printDiv(print) {
            var printContents = document.getElementById(print).innerHTML;
            var originalContents = document.body.innerHTML;
            document.body.innerHTML = "<html><head><title></title></head><body>" + printContents + "</body>";
            window.print();
            document.body.innerHTML = originalContents;
        }
    </script>
    <div class="row wrapper white-bg">
        <div class="col-lg-9 h1">
            Accounts (Tuition Fee): <small>Recent Tuition Fee Payments</small>
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
            <div class="col-lg-12">
                <div class="float-e-margins">
                    <div class="col-lg-4">
                        <label>View</label><br />
                        <asp:DropDownList ID="ddlView" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlView_SelectedIndexChanged">
                            <asp:ListItem Text="Tuition Fee Payments Received Today" Value="Tuition Fee Payments Received Today" />
                            <asp:ListItem Text="Tuition Fee Payments Received This Week" Value="Tuition Fee Payments Received This Week" />
                            <asp:ListItem Text="Tuition Fee Payments Received This Month" Value="Tuition Fee Payments Received This Month" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-2">
                        <label>Class:</label><br />
                        <asp:DropDownList ID="ddlFormClass" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFormClass_SelectedIndexChanged"></asp:DropDownList>
                    </div>
              </div>
                <div class="col-lg-offset-9">
                    <asp:Button ID="btnPrint" runat="server" Text="Print" Visible="false" CssClass="btn btn-primary fa fa-print" OnClientClick="printDiv('print')" />
                </div>
            </div>
        </div>
        <br />
        <asp:Label ID="lblZeroStudents" runat="server" Text=""></asp:Label>
        <div id="print">
            <div id="DivStudents" runat="server" visible="false" class="ibox float-e-margins panel panel-primary">
                <div class="ibox-title panel-heading">
                    <h5>Recent Tuition Fee Payments</h5>
                </div>
                <div class="ibox-content p-xl">
                   <div class="row">
                        <div class="col-md-7 col-lg-offset-3">
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
                                <br />
                                <br />
                                <h2>Recent Tuition Fee Payments</h2>
                                 &nbsp;&nbsp;
                                <asp:Label ID="lblViewSelected" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                     </div>
                    <br />
                    <br />
                    <h2>Total Amount Recieved:
                        <asp:Label ID="lblTotalAmountPaid" runat="server"></asp:Label></h2>
                    <h2>Bank:
                        <asp:Label ID="lblBank" runat="server"></asp:Label>
                        Cash:
                        <asp:Label ID="lblCash" runat="server"></asp:Label>
                        Cheque:
                        <asp:Label ID="lblCheque" runat="server"></asp:Label>
                        Other:
                        <asp:Label ID="lblOther" runat="server"></asp:Label></h2>
                    <%--Completed:<asp:Label ID="lblCompleted" runat="server"></asp:Label>--%>

                    <hr />
                    <asp:GridView ID="GridViewStudentPayments" runat="server" AutoGenerateColumns="False"
                        CssClass="table table-striped table-bordered table-hover dataTables-example">
                        <Columns>
                            <asp:BoundField DataField="Payment_Received_Date" HeaderText="Payment Received Date" />
                            <asp:BoundField DataField="name" HeaderText="Student Name" />
                            <asp:BoundField DataField="className" HeaderText="Class" />
                            <asp:BoundField DataField="Amount_Paid" HeaderText="Amount Paid" />
                            <asp:BoundField DataField="payment_method" HeaderText="Payment Method" />
                        </Columns>
                    </asp:GridView>
                    <br />

                    <asp:Label ID="lblZeroRecords" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
