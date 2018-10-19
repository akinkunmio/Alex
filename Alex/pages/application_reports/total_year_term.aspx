<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="total_year_term.aspx.cs" Inherits="Alex.pages.application_reports.made_year_term" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10 h1">
           Admissions Reports
        </div>
         <div class="col-md-3 pull-right">
            <a href="../reports.aspx">
               <asp:Label runat="server" Text="&nbsp;Back to Reports" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"  ></asp:Label>
            </a>
        </div>
    </div>
<div>
        <h1>Total Admissions by Year and Term</h1>
        <div class="wrapper wrapper-content animated fadeInRight">
            <asp:GridView ID="GridViewReportApplicationTotalByYearTerm" runat="server" AutoGenerateColumns="False"
                CssClass="table table-striped table-bordered table-hover dataTables-example">
                <Columns>
                    <asp:BoundField DataField="acad_year" HeaderText="Academic Year" />
                    <asp:BoundField DataField="term_name" HeaderText="Term" />
                    <asp:BoundField DataField="Total" HeaderText="Total" />
                </Columns>
            </asp:GridView>
  </div>
</div>
</asp:Content>
