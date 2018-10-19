﻿<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="student_stats.aspx.cs" Inherits="Alex.pages.student_reports.student_stats" %>

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
            Student Reports: <small>Student Stats</small>
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
                    <h5>Student Statistics</h5>
                </div>
                <div class="ibox-content p-xl">
                    <div class="row">
                        <div class="col-lg-2">
                           
                                <asp:Image ID="imgSchool" runat="server" length="160px" Width="160px" AlternateText="Image" />
                           </div>
                            <div class="col-lg-8 col-lg-offset-2">
                              <div class="title h1"><asp:Label ID="lblName" runat="server" Text=""></asp:Label></div> 
                                <div class="col-lg-offset-1"> <h3>Student Statistics</h3>
                                <label>AcademicYear</label>:
                                <asp:Label ID="lblyearSelected" runat="server" Text=""></asp:Label>
                                <label>Term</label>
                                :<asp:Label ID="lblTermSelected" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                    
                </div><br /><br />
                <h2>Total No of Students:<asp:Label ID="lblNoOfStudents" runat="server"></asp:Label></h2>
                <h2>Boys:<asp:Label ID="lblBoys" runat="server"></asp:Label>
                    Girls:
                        <asp:Label ID="lblGirls" runat="server"></asp:Label></h2>
                <h2>Active:<asp:Label ID="lblActive" runat="server"></asp:Label>
                    Withdrawn:<asp:Label ID="lblWithdrawn" runat="server"></asp:Label>
                    Completed:<asp:Label ID="lblCompleted" runat="server"></asp:Label></h2>

                <hr />
                <asp:GridView ID="GridViewReportStat2" runat="server" AutoGenerateColumns="False"
                    CssClass="table table-striped table-bordered table-hover dataTables-example">
                    <Columns>
                        <asp:BoundField DataField="class" HeaderText="Class" />
                        <asp:BoundField DataField="total_students" HeaderText="Total Students" />
                        <asp:BoundField DataField="Male" HeaderText="Boys" />
                        <asp:BoundField DataField="Female" HeaderText="Girls" />
                        <asp:BoundField DataField="Active" HeaderText="Active" />
                        <asp:BoundField DataField="Withdrawn" HeaderText="Withdrawn" />
                        <asp:BoundField DataField="Completed" HeaderText="Completed" />

                    </Columns>
                </asp:GridView>
                <br />

                <asp:Label ID="lblZeroRecords" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
    </div>
  
</asp:Content>