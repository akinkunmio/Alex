<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="studentId_card.aspx.cs" Inherits="Alex.pages.studentId_card" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="iQ is a suite of integrated education management applications that facilitates the management of your school." />
    <meta name="description" content="iQ is a suite of integrated education management applications that facilitates the management of your school." />

    <link rel="shortcut icon" href="../images/favicon.png" />
    <title>iQ</title>

    <link href="../scripts/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../scripts/font-awesome/css/font-awesome.css" rel="stylesheet" />


    <link href="../scripts/css/animate.css" rel="stylesheet" />
    <link href="../scripts/css/style.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <div class="ibox wrapper wrapper-content">
          
                <div class="ibox-content p-xl" style="height:400px;width :750px">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
    <div>
    
        <rsweb:ReportViewer ID="RptViewerIdCard" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="384px" Width="620px" ShowPrintButton="true">
            <LocalReport ReportPath="Alex.Report\StudentIdCard.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="MyStuDataSet" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="Alex.MyStuDataSetTableAdapters.sp_ms_person_students_id_card"></asp:ObjectDataSource>
    
    </div>
                </div>
         
        </div>
    </form>
</body>
</html>
