<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="employee_payslip.aspx.cs" Inherits="Alex.pages.employee_payslip" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

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
     <div class="col-lg-offset-9">
                    <asp:Button ID="btnPrint" runat="server" Text="Print" class="btn btn-primary fa fa-print" OnClientClick="printDiv('print')" />
                    <asp:Button ID="btnBack" runat="server" Text="Back to Payroll" class="btn btn-primary fa fa-print" OnClick="btnBack_Click" />
     </div><br />
     <div id="print">
    <div  class="row col-lg-12">
        <div class="ibox float-e-margins panel panel-primary">
            <div class="ibox-title panel-heading">
                <div class="row">
                    <div class="col-lg-6">
                        <h3>Employee Name</h3>
                    </div>
                    <div class="col-lg-3">
                        <h3>Month</h3>
                    </div>
                    <div class="col-lg-3">
                        <h3>Year</h3>
                    </div>
                </div>

            </div>
            <div class="ibox-content" style="width: 100%; height: 40px; position: relative">

                <div class="row">
                    <div class="col-lg-6">
                        <h3>
                            <asp:Label ID="lblEmployeeName" runat="server" Text=""></asp:Label></h3>
                    </div>
                    <div class="col-lg-3">
                        <h3>
                            <asp:Label ID="lblMonth" runat="server" Text=""></asp:Label></h3>
                    </div>
                    <div class="col-lg-3">
                        <h3>
                            <asp:Label ID="lblYear" runat="server" Text=""></asp:Label></h3>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-lg-6">
            <div class="ibox float-e-margins panel panel-primary">
                <div class="ibox-title panel-heading">
                    <h5>Allowances</h5>
                </div>
                <div class="ibox-content" style="width: 100%; height: 250px;">
                    <asp:GridView ID="GridViewAllowances" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example">
                        <Columns>
                            <asp:BoundField DataField="housing_allow" HeaderText="Housing" />
                            <asp:BoundField DataField="transport_allow" HeaderText="Transport" />
                            <asp:BoundField DataField="meal_allow" HeaderText="Meal" />
                            <asp:BoundField DataField="utility_allow" HeaderText="Utility" />
                            <asp:BoundField DataField="other_allow" HeaderText="other" />
                            <%--<asp:BoundField DataField="Total Allowances" HeaderText="Total" />--%>

                        </Columns>
                    </asp:GridView>

                   <div class="col-lg-6 col-lg-offset-6">
                        <div class="ibox float-e-margins panel panel-primary">
                            <div class="ibox-title panel-heading" style="height: 60px; position: relative">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <h4>Total Allowances</h4>
                                    </div>

                                </div>

                            </div>
                            <div class="ibox-content" style="width: 100%; height: 40px;">

                                <div class="row">
                                    <div class="col-lg-6">
                                        <h3>
                                            <asp:Label ID="lblTotalAllowances" runat="server" Text=""></asp:Label></h3>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>

              
                </div>

            </div>
        </div>
        <div class="col-lg-6">
            <div class="ibox float-e-margins panel panel-primary">
                <div class="ibox-title panel-heading">
                    <h5>Deductions</h5>

                </div>
                <div class="ibox-content" style="width: 100%; height: 250px;">

                    <asp:GridView ID="GridViewDedcutions" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example">
                        <Columns>
                            <asp:BoundField DataField="staff_tax" HeaderText="Tax" />
                            <asp:BoundField DataField="staff_penssion_scheme" HeaderText="Penssion" />
                            <asp:BoundField DataField="staff_coop" HeaderText="Co-operative" />
                            <asp:BoundField DataField="staff_loan" HeaderText="Loan" />
                            <asp:BoundField DataField="Misc" HeaderText="Other" />
                           <%-- <asp:BoundField DataField="total_deductions" HeaderText="Total" />--%>

                        </Columns>
                    </asp:GridView>
                    <div class="col-lg-6 col-lg-offset-6">
                        <div class="ibox float-e-margins panel panel-primary">
                            <div class="ibox-title panel-heading" style="height: 60px; position: relative">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <h4>Total Deductions</h4>
                                    </div>

                                </div>

                            </div>
                               <div class="ibox-content" style="width: 100%; height: 40px;">

                                <div class="row">
                                    <div class="col-lg-6">
                                        <h3>
                                            <asp:Label ID="lblTotalDeductions" runat="server" Text=""></asp:Label></h3>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div  class="row col-lg-12">
        <div class="ibox float-e-margins panel panel-primary">
            <div class="ibox-title panel-heading">
                <div class="row">
                    <div class="col-lg-6">
                        <h3>Basic Pay</h3>
                    </div>
                    <div class="col-lg-3">
                        <h3>Gross Pay</h3>
                    </div>
                    <div class="col-lg-3">
                        <h3>Net Pay</h3>
                    </div>
                </div>

            </div>
            <div class="ibox-content" style="width: 100%; height: 40px; position: relative">

                <div class="row">
                    <div class="col-lg-6">
                        <h3>
                            <asp:Label ID="lblBasicPay" runat="server" Text=""></asp:Label></h3>
                    </div>
                    <div class="col-lg-3">
                        <h3>
                            <asp:Label ID="lblGrossPay" runat="server" Text=""></asp:Label></h3>
                    </div>
                    <div class="col-lg-3">
                        <h3>
                            <asp:Label ID="lblNetPay" runat="server" Text=""></asp:Label></h3>
                    </div>
                </div>
            </div>
        </div>
    </div>

    </div>
</asp:Content>
