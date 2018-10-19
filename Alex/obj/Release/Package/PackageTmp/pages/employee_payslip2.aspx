<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="employee_payslip2.aspx.cs" Inherits="Alex.pages.employee_payslip2" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--<script type="text/javascript">
        function printDiv(print) {
            var printContents = document.getElementById(print).innerHTML;
            var originalContents = document.body.innerHTML;
            //document.body.innerHTML = printContents;
            document.body.innerHTML = "<html><head><title></title></head><body>" + printContents + "</body>";
            window.print();
            document.body.innerHTML = originalContents;
        }
    </script>--%>

    <div class="col-lg-offset-9">
        <%--<asp:Button ID="btnPrint" runat="server" Text="Print" class="btn btn-primary fa fa-print" />--%>
        <asp:Button ID="btnBack" runat="server" Text="Back to Payroll" CssClass="btn btn-primary fa fa-print" OnClick="btnBack_Click"  />
    </div>
    <%-- <div class="row">
            <div class="col-lg-12">
                
                <div class="col-lg-offset-9">
                    <asp:Button ID="btnPrint" runat="server" Text="Print" class="btn btn-primary fa fa-print" OnClientClick="printDiv('print')" />
                </div>
            </div>
        </div>--%>
    <div id="print">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <br />

        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%" Height="800px"
            ShowPrintButton="true" ShowRefreshButton="false" ShowPageNavigationControls="false" ShowFindControls="false" ShowBackButton="false">
            <LocalReport ReportEmbeddedResource="Alex.Report\Employee_Payslip2.rdlc" ReportPath="Report\Employee_Payslip2.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="MyStuDataSet" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="Alex.MyStuDataSetTableAdapters.sp_ms_hr_bab_employee_payroll_payslipTableAdapter"></asp:ObjectDataSource>
        <br />

    </div>

</asp:Content>
