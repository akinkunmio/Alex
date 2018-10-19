<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="employees_salary_paid.aspx.cs" Inherits="Alex.pages.employee_reports.employees_salary_paid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../scripts/css/pagestyle.css" rel="stylesheet" />
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
   
    
    <div class="row wrapper white-bg">
        <div class="col-lg-9 h1">
           Employees Reports: <small>Salary Paid List</small>
        </div>
        <div class="col-md-2 ">
            <a href="../reports.aspx">
                <br />
                <asp:Label runat="server" Text="&nbsp;Back to Reports" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"></asp:Label>
            </a>
        </div>
        <%--<asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick = "ExportToExcel" />--%>
        <br />
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">

        <div class="row">
            <div class="col-lg-8">
                <div class="float-e-margins">
                    <div class="form-group">
                        <div class="editor-label">
                            <label>Year</label>
                        </div>
                        <div class="editor-field">
                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control"  AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="editor-label">
                            <label>Month</label>
                        </div>
                        <div class="editor-field">
                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                   
                    <%--<asp:Button ID="BtnSearchPaidPayments" runat="server" Text="GO" type="search" CssClass="btn-primary" OnClick="BtnSearchPaidPayments_Click" />--%>
                </div>
                <div class="col-lg-offset-12">
                    <asp:Button ID="btnPrint" runat="server" Text="Print" Visible="false" class="btn btn-primary fa fa-print" OnClientClick="printDiv('print')" />
                </div>
            </div>
        </div>
        <br />
      
        <div id="print">
            <div id="DivRptList" runat="server" visible="false" class="ibox float-e-margins panel panel-primary">
                <div class="ibox-title panel-heading">
                    <h5>Salary Paid List</h5>
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
                    <br /><h2>Salary Paid List</h2>
                                <label>Year</label>:
                                <asp:Label ID="lblYearSelected" runat="server" Text=""></asp:Label>
                                <label>,Month</label>
                                :<asp:Label ID="lblMonthSelected" runat="server" Text=""></asp:Label> </div>
                              </div>
                        </div>
                        <asp:Button ID="btnDownloadExcel" runat="server" Text="Download to Excel"  OnClick="btnDownloadExcel_Click"/>
                 
                    <br />
                    <br />
                    <h2>Total No of Employees Paid: <asp:Label ID="lblNoOfEmployees" runat="server"></asp:Label></h2>
                    <h2>Total Salary Paid for this Month: <asp:Label ID="lblTotalSalaryPaid4ThisMonth" runat="server"></asp:Label></h2>
                   
                   <hr />
                    <asp:GridView ID="GridViewBankPayments" runat="server" AutoGenerateColumns="False"
                        CssClass="table table-striped table-bordered table-hover dataTables-example">
                        <Columns>
                            <asp:BoundField DataField="name" HeaderText="Name" />
                            <asp:BoundField DataField="salary_at_run" HeaderText="Salary" />
                            <asp:BoundField DataField="total_deductions" HeaderText="Deductions" />
                            <asp:BoundField DataField="total_amount_paid" HeaderText="Salary Paid" />
                            <asp:BoundField DataField="balance" HeaderText="Balance" />
                        </Columns>
                    </asp:GridView>
                    <br />
                   
               </div>
            </div>
             <asp:Label ID="lblZeroRecords" runat="server" Text=""></asp:Label>
        </div>
       
    </div>

</asp:Content>

