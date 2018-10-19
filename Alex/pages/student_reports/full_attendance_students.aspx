<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="full_attendance_students.aspx.cs" Inherits="Alex.pages.student_reports.full_attendance_students" %>
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
            Student Reports: <small>List of 100 Percentage Attendance Students</small>
        </div>
        <div class="col-md-2 ">
            <a href="../reports.aspx">
                <br/>
                <asp:Label runat="server" Text="&nbsp;Back to Reports" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"></asp:Label>
            </a>
        </div>
        <br />
    </div>
        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="col-lg-offset-10">
                    <asp:Button ID="btnPrint" runat="server" Text="Print" class="btn btn-primary fa fa-print" OnClientClick="printDiv('print')" />
            </div><br />
            <div id="print">
            <div class="ibox float-e-margins panel panel-primary">
                <div class="ibox-title panel-heading">
                    <h5>List of 100 Percentage Attendance Students</h5>
                </div>
                <div class="ibox-content p-xl">
                    <div class="row">
                        <div class="col-lg-2">
                           <asp:Image ID="imgSchool" runat="server" length="160px" Width="160px" AlternateText="Image" />
                           </div>
                            <div class="col-lg-8 col-lg-offset-2">
                              <div class="title h1"><asp:Label ID="lblName" runat="server" Text=""></asp:Label></div> 
                                <div class=""> <h3>100 Percentage Attendance Students Report</h3>
                                </div>
                            </div>
                    
                </div><hr />
                <asp:GridView ID="GridViewReportFullAttendance" runat="server" AutoGenerateColumns="False"
                CssClass="table table-striped table-bordered table-hover dataTables-example">
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:BoundField DataField="acad_year" HeaderText="Academic Year" />
                    <asp:BoundField DataField="term_name" HeaderText="Term" />
                    <asp:BoundField DataField="class" HeaderText="Class" />
                    <asp:BoundField DataField="PresentCountPercentB" HeaderText="Percentage" />

                </Columns>
            </asp:GridView>
                <br />
            <asp:Label ID="lblZeroRecords" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
            
  </div>

</asp:Content>