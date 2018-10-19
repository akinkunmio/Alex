<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="report_card.aspx.cs" Inherits="Alex.pages.report_card" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div id="form1" runat="server">
        <div class="ibox wrapper wrapper-content">
          
                <div class="ibox-content p-xl"style="height: 1000px; width: 1050px">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
    <div>
    
        <rsweb:ReportViewer ID="RptViewerRptCard" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" 
             WaitMessageFont-Size="14pt" Height="900px" Width="950px" ShowPrintButton="true" >
            <LocalReport ReportPath="Alex.Report\ReportCard.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="MyStuDataSet" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="Alex.MyStuDataSetTableAdapters.sp_ms_person_report_card_breakdown"></asp:ObjectDataSource>
    
    </div>
                </div>
         
        </div>
    </div>
</asp:Content>
