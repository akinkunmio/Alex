<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="list_status_year_term.aspx.cs" Inherits="Alex.pages.application_reports.list_status_year_term" %>
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
    <div class="wrapper wrapper-content animated fadeInRight">
         <h2>List of Admissions Filtered by Status, Year and Term</h2>
        
         <div class="row">
            <div class="col-lg-12">
                <div class="float-e-margins">
                   <div class="col-md-12">
            <%--<div class="form-group">
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
                    <label>Term</label>
                </div>
                <div class="editor-field">
                    <asp:DropDownList ID="ddlTerm" runat="server">                   
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
            </div>--%>    <label>Academic Year</label>
                         <asp:DropDownList ID="ddlAcademicYear" runat="server">
                       </asp:DropDownList> <label>Term</label>
                       <asp:DropDownList ID="ddlTerm" runat="server">
                       </asp:DropDownList> <label>Status</label>
                       <asp:DropDownList ID="ddlStatus" runat="server">
                       </asp:DropDownList>
            <asp:Button ID="BtnListYearTermStatus" runat="server" Text="GO" type="search" CssClass="btn-primary" OnClick="BtnListYearTermStatus_Click"/>
        </div>
      </div>
    </div>
  </div><br />
    
    <asp:GridView ID="GridViewReportApplicationListByYearTermStatus" runat="server" AutoGenerateColumns="False"
                CssClass="table table-striped table-bordered table-hover dataTables-example">
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:BoundField DataField="dob" HeaderText="Date of Birth" />
                    <asp:BoundField DataField="app_date" HeaderText="Application date" />
                    <asp:BoundField DataField="form_name" HeaderText="Class" />
                    <asp:BoundField DataField="application_status" HeaderText="Admission Status" />

                </Columns>
            </asp:GridView><br />
     
    <asp:Label ID="lblZeroRecords" runat="server" Text=""></asp:Label>
    
  </div>
   
</asp:Content>