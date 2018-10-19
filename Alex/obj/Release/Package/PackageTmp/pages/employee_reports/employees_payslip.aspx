<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="employees_payslip.aspx.cs" Inherits="Alex.pages.employee_reports.employees_payslip" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../scripts/css/pagestyle.css" rel="stylesheet" />

    <div class="row wrapper white-bg">
        <div class="col-lg-9 h1">
            Employees Reports: <small>Payslip's</small>
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
            <div class="col-lg-4">
                <div class="float-e-margins">
                    <div class="form-group">
                        <div class="editor-label">
                            <label>Year</label>
                        </div>
                        <div class="editor-field">
                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"></asp:DropDownList>
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
                    <%--<asp:Button ID="BtnSearchPayslips" runat="server" Text="GO"  CssClass="btn-primary" OnClick="BtnSearchPayslips_Click" />--%>
                </div>
            </div>
        </div>
        <br />
    </div>
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        <br />
            <rsweb:ReportViewer ID="RptViewerPayslipAll" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana"
                WaitMessageFont-Size="14pt" Width="100%" Height="800px">
               <LocalReport ReportEmbeddedResource="Alex.Report\Payslips.rdlc" ReportPath="Report\Payslips.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="ObjectDSPayslip" Name="MyStuDataSet"/>
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
            <asp:ObjectDataSource ID="ObjectDSPayslip" runat="server" SelectMethod="GetData"
                TypeName="Alex.MyStuDataSetTableAdapters.sp_ms_hr_list_all_employee_payslipTableAdapter"></asp:ObjectDataSource>
            <br />
        </div>
   
</asp:Content>

