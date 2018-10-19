<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="total_year.aspx.cs" Inherits="Alex.pages.application_reports.total_year" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10 h1">
           Admission Reports
        </div>
         <div class="col-md-3 pull-right">
            <a href="../reports.aspx">
               <asp:Label runat="server" Text="&nbsp;Back to Reports" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"  ></asp:Label>
            </a>
        </div>
    </div>
 

    <div id="ReportsApplicationByYear" runat="server">
        <div class="wrapper wrapper-content animated fadeInRight">
            <h2>Total Admissions by Year</h2>
           <asp:GridView ID="GridViewReportApplicationByYear" runat="server" AutoGenerateColumns="False"
            CssClass="table table-striped table-bordered table-hover dataTables-example">
            <Columns>
                <asp:BoundField DataField="acad_year" HeaderText="Academic Year" />
                <asp:BoundField DataField="Total" HeaderText="Total Admissions" />
            </Columns>
        </asp:GridView>
    </div>
  </div>
</asp:Content>
