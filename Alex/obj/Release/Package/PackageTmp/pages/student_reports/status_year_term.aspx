<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="status_year_term.aspx.cs" Inherits="Alex.pages.student_reports.status_year_term" %>
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
   
     <h1>List of Registrations Filtered by Status, Year and Term</h1>
    
        <div class="row">
          <div class="col-lg-12">
            <div class="float-e-margins">
            <div class="form-group">
                <div class="editor-label">
                    <label>Academic Year</label>
                </div>
                <div class="editor-field">
                    <asp:DropDownList ID="ddlAcademicYear" runat="server">
                   
                    </asp:DropDownList>

                </div>
            </div>
            <div class="form-group">
                <div class="editor-label">
                    <label>Status</label>
                </div>
                <div class="editor-field">
                    <asp:DropDownList ID="ddlStatus" runat="server">
                        
                   </asp:DropDownList>
                </div>
            </div>

            <div class="form-group">
                <div class="editor-label">
                    <label>Term</label>
                </div>
                <div class="editor-field">
                    <asp:DropDownList ID="ddlTerm" runat="server">
                      
                    </asp:DropDownList>
                </div>
            </div>
            <asp:Button ID="BtnTotalStatus" runat="server" Text="GO" type="search" CssClass="btn-primary" OnClick="BtnTotalStatus_Click" />
        </div>
    </div>
</div>
 
       
        <div class="wrapper wrapper-content animated fadeInRight">
            <asp:GridView ID="GridViewReportRegistrationsStatusByYear" runat="server" AutoGenerateColumns="False"
                CssClass="table table-striped table-bordered table-hover dataTables-example">
                <Columns>
                    <asp:BoundField DataField="acad_year" HeaderText="Academic Year" />
                    <asp:BoundField DataField="total_registrations" HeaderText="Total Registrations" />
                    <asp:BoundField DataField="term_name" HeaderText="Term" />
                    <asp:BoundField DataField="status" HeaderText="Registration Status" />

                </Columns>
            </asp:GridView>
            <asp:Label ID="lblZeroRecords" runat="server" Text=""></asp:Label>
        </div>
  
</asp:Content>
