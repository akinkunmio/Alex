<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="report_cards_comments.aspx.cs" Inherits="Alex.pages.student_reports.report_cards_comments" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="form1" runat="server">
        <div class="row wrapper white-bg">
            <div class="col-lg-9 h1">
                Student Reports: <small>Report Sheet</small>
            </div>
            <div class="col-md-2 ">
                <a href="../student_reports/all_report_cards.aspx">
                    <br />
                    <asp:Label runat="server" Text="&nbsp;Back to Report Cards" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"></asp:Label>
                </a>
            </div>

            <br />
        </div>
        <div class="ibox wrapper wrapper-content">
            <div class="row">
                <div class="col-lg-12">
                    <div class="float-e-margins">
                        <div class="col-md-12">

                            <div class=" col-lg-2">
                                <label>Academic Year</label><br />
                                <asp:DropDownList ID="ddlAcademicYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="col-lg-2">
                                <label>Term</label><br />
                                <asp:DropDownList ID="ddlTerm" runat="server"></asp:DropDownList>
                            </div>

                            <div class="col-lg-4">
                                <label>Class:</label><br />
                                <asp:DropDownList ID="ddlFormClass" runat="server"></asp:DropDownList>&nbsp;<asp:Button ID="btnReportCard" runat="server" Text="GO" CssClass="btn-primary" OnClick="btnReportCard_Click" />
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <br />
            <div class="ibox-content p-xl" style="height: 1000px; width: 1050px">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <div>

                    <rsweb:ReportViewer ID="RptViewerRptCard" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana"
                        WaitMessageFont-Size="14pt" Height="900px" Width="950px" ShowPrintButton="true">
                        <LocalReport ReportEmbeddedResource="Alex.Report\ReportCards_Students_Comments.rdlc" ReportPath="Report\ReportCards_Students_Comments.rdlc">
                            <DataSources>
                                <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="MyStuDataSet" />
                            </DataSources>
                        </LocalReport>
                    </rsweb:ReportViewer>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="Alex.MyStuDataSetTableAdapters.sp_ms_person_assessment_breakdown_v4TableAdapter"></asp:ObjectDataSource>

                </div>
            </div>

        </div>
    </div>   <asp:Label ID="lblFindClassSection" runat="server" Text="" Visible="false"></asp:Label>
</asp:Content>
