<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="total_year_term.aspx.cs" Inherits="Alex.pages.student_reports.made_year_term" %>

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
            Student Reports: <small>List of Historical No of Registered Students</small>
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
        <div class="col-lg-offset-10">
            <asp:Button ID="btnPrint" runat="server" Text="Print" class="btn btn-primary fa fa-print" OnClientClick="printDiv('print')" />
        </div>
        <br />
        <div id="print">
            <div class="ibox float-e-margins panel panel-primary">
                <div class="ibox-title panel-heading">
                    <h5>List of Historical No of Registered Students</h5>
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
                                <h3>Student Registrations Report</h3>
                            </div>
                        </div>

                    </div>
                    <hr />
                    <div class="row">
                       <div class="col-lg-4">
                         <div class="ibox-content" style="width: 100%; height: 100%; overflow: scroll">
                            <asp:GridView ID="GridViewReportRegistrationsTotalByYearTerm" runat="server" AutoGenerateColumns="False" 
                                CssClass="table table-striped table-bordered table-hover dataTables-example">
                                <Columns>
                                    <asp:BoundField DataField="acad_year" HeaderText="Academic Year" />
                                    <asp:BoundField DataField="total_registrations" HeaderText="Total no of Students" />

                                </Columns>
                            </asp:GridView>
                        </div>
                            </div>
                        <div class="col-lg-8">
                             <div class="ibox-content"  style="width: 100%; height: 100%;  overflow: scroll; position: relative">
                          <asp:SqlDataSource ID="SqlDataSourceGraph" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
                                    SelectCommand="sp_ms_rep_registrations_total_by_year_term" SelectCommandType="StoredProcedure"></asp:SqlDataSource>

                                <asp:Chart ID="chartRev" runat="server" DataSourceID="SqlDataSourceGraph"  BackColor="240, 240, 240" Height="760px" Width="700px" >
                                    <Series>
                                        <asp:Series Name="Series1" XValueMember="acad_year" YValueMembers="total_registrations" XValueType="Double" ChartType="Spline"></asp:Series>
                                    </Series>

                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1">
                                          
                                            <AxisY LineColor="Gray" Title="Total Registrations">
                                                <MajorGrid LineColor="LightGray" />
                                            </AxisY>
                                            <AxisX LineColor="Gray" Title="Academic Year">
                                                <MajorGrid LineColor="LightGray" />
                                            </AxisX>
                                        </asp:ChartArea>
                                    </ChartAreas>

                                </asp:Chart>
                            </div>
                        </div>
                    </div>


                    <br />
                    <asp:Label ID="lblZeroRecords" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>

    </div>

</asp:Content>
