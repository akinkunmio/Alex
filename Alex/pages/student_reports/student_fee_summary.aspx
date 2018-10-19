<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="student_fee_summary.aspx.cs" Inherits="Alex.pages.student_reports.student_fee_summary" %>

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
    <div class="row wrapper white-bg">
        <div class="col-lg-9 h1">
            Accounts (Tuition Fee): <small>Tuition Fee Accounts Summary</small>
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
                    <label>Academic Year</label>
                    <asp:DropDownList ID="ddlAcademicYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged">
                    </asp:DropDownList>
                    <label>Term</label>
                    <asp:DropDownList ID="ddlTerm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTerm_SelectedIndexChanged">
                    </asp:DropDownList>
                   <%-- <asp:Button ID="BtnStudentStats" runat="server" Text="GO" type="search" CssClass="btn-primary" OnClick="BtnStudentStats_Click" />--%>
                </div>
                <div class="col-lg-offset-9">
                    <asp:Button ID="btnPrint" runat="server" Text="Print" Visible="false" class="btn btn-primary fa fa-print" OnClientClick="printDiv('print')" />
                </div>
            </div>
        </div>
        <br />
        <asp:Label ID="lblZeroStudents" runat="server" Text=""></asp:Label>
        <div id="print">
            <div id="DivStudents" runat="server" visible="false" class="ibox float-e-margins panel panel-primary">
                <div class="ibox-title panel-heading">
                    <h5>Tuition Fee Accounts Summary</h5>
                </div>
                <div class="ibox-content p-xl">
                    <div class="row">
                        <div class="col-lg-2">

                            <asp:Image ID="imgSchool" runat="server" length="160px" Width="160px" AlternateText="Image" />
                        </div>
                        <div class="col-lg-8 col-lg-offset-2">
                            <div class="title h1">
                                <asp:Label ID="lblName" runat="server" Text=""></asp:Label></div>
                            <div class="col-lg-offset-1">
                                <h3>Tuition Fee Accounts Summary</h3>
                                <label>AcademicYear</label>:
                                <asp:Label ID="lblyearSelected" runat="server" Text=""></asp:Label>
                                <label>Term</label>
                                :<asp:Label ID="lblTermSelected" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                    </div>
                   
                    
                        <div class="list-group">
                            <h2 class="text-success">Total Outstanding Debts of all Academic Years: <asp:Label ID="lblAcademicTotalOutstanding" runat="server"></asp:Label></h2>
                            <h2 class="text-success">Total Amount Fee Expected: <asp:Label ID="lblExpected" runat="server"></asp:Label></h2>
                            <h2 class="text-success">Total Amount Fee Received: <asp:Label ID="lblReceived" runat="server"></asp:Label></h2>
                            <h2 class="text-success">Total Amount Fee Outstanding: <asp:Label ID="lblOutstanding" runat="server"></asp:Label></h2>
                         </div>

                    <hr />
                    <asp:GridView ID="GridViewFeeReportsSummary1" runat="server" AutoGenerateColumns="False"
                        CssClass="table table-striped table-bordered table-hover dataTables-example">
                        <Columns>
                            <asp:BoundField DataField="form_name" HeaderText="Class" />
                            <asp:BoundField DataField="School Fees" HeaderText="School Fees" />
                            <asp:BoundField DataField="Total_no_of_students" HeaderText="Total No of Students" />
                            <asp:BoundField DataField="Total_Amount_of_Fees_Expected" HeaderText="Total Amount Fee Expected" />
                            <asp:BoundField DataField="Total_Amount_of_Fees_Received" HeaderText="Total Amount Fee Received" />
                            <asp:BoundField DataField="Total_Amount_of_Fees_Outstanding" HeaderText="Total Amount Fee Outstanding" />


                        </Columns>
                    </asp:GridView>
                    <br />

                    <asp:Label ID="lblZeroRecords" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
