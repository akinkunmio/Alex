<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="ict.aspx.cs" Inherits="Alex.pages.student_reports.ict" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../scripts/css/pagestyle.css" rel="stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(DocReady);
            function DocReady() {
                $('option[value = PDF]').remove();
            }
           
        });
    </script>

    <div class="row wrapper white-bg">
        <div class="col-lg-9 h1">
            Student Reports: <small>ICT</small>
        </div>
        <div class="col-md-2 ">
            <a href="../reports.aspx">
                <br />
                <asp:Label runat="server" Text="&nbsp;Back to Reports" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"></asp:Label>
            </a>
        </div>

        <br />
    </div>
    <div class="wrapper wrapper-content  fadeInRight">
        <div class="col-md-3 pull-right ">
           <asp:Button ID="btnExportExcel" runat="server" Text="Export to Excel" OnClick="btnExportExcel_Click"  CssClass=" fa fa-file-excel-o btn btn-danger"/>
        </div>
         <br />
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        <br />
             <rsweb:ReportViewer ID="RptViewerICTCard" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" 
                 WaitMessageFont-Size="14pt" Height="484px" Width="720px"  ShowCredentialPrompts="False" ShowDocumentMapButton="False"
        ShowParameterPrompts="False" ShowWaitControlCancelLink="False" EnableTheming="False"
        ExportContentDisposition="AlwaysAttachment" >
            <LocalReport ReportPath="Alex.Report\Ict.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="MyStuDataSet" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="Alex.MyStuDataSetTableAdapters.sp_ms_ict_all"></asp:ObjectDataSource>
    
            <br />
       
    </div>
       
      
   
</asp:Content>
