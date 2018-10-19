<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="student_debtors_year.aspx.cs" Inherits="Alex.pages.student_reports.student_debtors_year"   EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <script type="text/javascript">
        function printDiv(print) {
            var printContents = document.getElementById(print).innerHTML;
            var originalContents = document.body.innerHTML;
            //document.body.innerHTML = printContents;
            document.body.innerHTML = "<html><head><title></title></head><body>" + printContents + "</body>";
            window.print();
            document.body.innerHTML = originalContents;
        }
   </script>
    <div class="row wrapper white-bg">
        <div class="col-lg-9 h1">
            Accounts (Tuition Fee): <small>List of Debtors (Cummulative Debts)</small>
        </div>
        <div class="col-md-2 ">
            <a href="../reports.aspx">
                <br />
                <asp:Label runat="server" Text="&nbsp;Back to Reports" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"></asp:Label>
            </a>
        </div>
        <br />
    </div>
    <div class="wrapper wrapper-content fadeInRight">

        <div class="row">
            <div class="col-lg-12">
                <%--<div class="float-e-margins">
                    <h1> List of Students owing fees </h1>
                </div>--%>
                <div class="col-lg-offset-9">
                    <asp:Button ID="btnExportPDF" runat="server" Text="ExportToPDF" CssClass="fa fa-print btn btn-primary" OnClick="btnExportPDF_Click" />
                    <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="fa fa-print btn btn-primary " OnClientClick="printDiv('print')" />
                </div>
            </div>
        </div>
        <br />
        <asp:Label ID="lblZeroStudents" runat="server" Text=""></asp:Label>
        <div id="print">
            <div id="DivStudents" runat="server" class="ibox float-e-margins panel panel-primary">
                <div class="ibox-title panel-heading">
                    <h5>List of Debtors (Cummulative Debts)</h5>
                </div>
                <div class="ibox-content p-xl">
                    <div class="row">
                        <div class="col-md-7 col-lg-offset-3">
                            <div class="glyphicon" style="vertical-align: top">
                                <asp:Image ID="imgSchool" runat="server" height="120px" Width="120px" AlternateText="Image" />
                            </div>
                            <div class="text-center glyphicon">
                                <asp:Label ID="lblSchoolName" runat="server" Text="" CssClass="title h1"></asp:Label><br />
                                <asp:Label ID="lblAddress" runat="server" Text="" CssClass="h4"></asp:Label><br />
                                <asp:Label ID="lblCity" runat="server" Text="" CssClass="h4"></asp:Label>,
                                 <asp:Label ID="lblState" runat="server" Text="" CssClass="h4"></asp:Label><br />
                                <asp:Label ID="lblCountry" runat="server" Text="" CssClass="h4"></asp:Label>,
                                 <asp:Label ID="lblPostCode" runat="server" Text="" CssClass="h4"></asp:Label><br />
                                <i class="fa fa-envelope-square"></i>
                                <asp:Label ID="lblEmail" runat="server" Text="" CssClass="h4"></asp:Label>,
                                 <i class="fa fa-phone-square"></i>
                                <asp:Label ID="lblPhoneNo" runat="server" Text="" CssClass="h4"></asp:Label>


                                <br />
                                <br />
                                <h2>List of Debtors (Cummulative Debts)</h2>
                            </div>
                        </div>
                        <div class="col-lg-offset-10">
                            <asp:Button ID="btnSendDebSms" runat="server" CssClass=" btn-sm btn-danger m-t-n-xs" Text='Send SMS to Debtors' OnClick="btnSendDebSms_Click" />
                        </div>
                    </div>



                    <hr />
                    <asp:GridView ID="GridViewReportDebtors" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSourceStuDeb" ShowHeaderWhenEmpty="true"
                        CssClass="table table-striped table-bordered table-hover dataTables-example" AllowSorting="True" EmptyDataText="No Debtors Found">

                        <SortedAscendingHeaderStyle CssClass="sortasc" />
                        <SortedDescendingHeaderStyle CssClass="sortdesc" />
                        <Columns>
                            <asp:TemplateField HeaderText="S.no">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="name" HeaderText="Name" SortExpression="name" AccessibleHeaderText="Name">
                                <HeaderStyle BorderStyle="Inset" />
                            </asp:BoundField>
                            <asp:BoundField DataField="class" HeaderText="Class" ReadOnly="True" SortExpression="class" />
                            <asp:BoundField DataField="total_outstanding" HeaderText="Fee Outstanding" ReadOnly="True" SortExpression="total_outstanding" />

                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSourceStuDeb" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>" SelectCommand="sp_ms_rep_student_all_debtors_list" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                    <br />
                 </div>
            </div>
        </div>
      </div>
    <style>
        th.sortasc a {
            display: block;
            padding: 0 4px 0 15px;
            background: url("../../images/asc.gif") no-repeat;
        }

        th.sortdesc a {
            display: block;
            padding: 0 4px 0 15px;
            background: url("../../images/desc.gif") no-repeat;
        }
    </style>

</asp:Content>
