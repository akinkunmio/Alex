<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="total_year.aspx.cs" Inherits="Alex.pages.student_reports.total_year" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10 h1">
            Student Reports
        </div>
          <div class="col-md-3 pull-right">
            <a href="../reports.aspx">
               <asp:Label runat="server" Text="&nbsp;Back to Reports" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"  ></asp:Label>
            </a>
        </div>
    </div>
     

   
        <h1>Total Registrations by Year</h1>
        <div class="wrapper wrapper-content animated fadeInRight" id="TotalRegistrationByYear" runat="server">
            <asp:GridView ID="GridViewReportRegistrationByYear" runat="server" AutoGenerateColumns="False"
                CssClass="table table-striped table-bordered table-hover dataTables-example">
                <Columns>
                    <asp:BoundField DataField="acad_year" HeaderText="Academic Year" />
                    <asp:BoundField DataField="Total" HeaderText="Total Applications" />
                </Columns>
            </asp:GridView>
        </div>
  
</asp:Content>
