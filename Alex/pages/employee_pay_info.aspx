<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="employee_pay_info.aspx.cs" Inherits="Alex.pages.employee_pay_info" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
            var tbl = document.getElementById(gridId);
            if (tbl) {
                var DivHR = document.getElementById('DivHeaderRow');
                var DivMC = document.getElementById('DivMainContent');
                var DivFR = document.getElementById('DivFooterRow');

                //*** Set divheaderRow Properties ****
                DivHR.style.height = headerHeight + 'px';
                DivHR.style.width = (parseInt(width) - 16) + 'px';
                DivHR.style.position = 'relative';
                DivHR.style.top = '0px';
                DivHR.style.zIndex = '10';
                DivHR.style.verticalAlign = 'top';

                //*** Set divMainContent Properties ****
                DivMC.style.width = width + 'px';
                DivMC.style.height = height + 'px';
                DivMC.style.position = 'relative';
                DivMC.style.top = -headerHeight + 'px';
                DivMC.style.zIndex = '1';

                //*** Set divFooterRow Properties ****
                DivFR.style.width = (parseInt(width) - 16) + 'px';
                DivFR.style.position = 'relative';
                DivFR.style.top = -headerHeight + 'px';
                DivFR.style.verticalAlign = 'top';
                DivFR.style.paddingtop = '2px';

                if (isFooter) {
                    var tblfr = tbl.cloneNode(true);
                    tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
                    var tblBody = document.createElement('tbody');
                    tblfr.style.width = '100%';
                    tblfr.cellSpacing = "0";
                    tblfr.border = "0px";
                    tblfr.rules = "none";
                    //*****In the case of Footer Row *******
                    tblBody.appendChild(tbl.rows[tbl.rows.length - 1]);
                    tblfr.appendChild(tblBody);
                    DivFR.appendChild(tblfr);
                }
                //****Copy Header in divHeaderRow****
                DivHR.appendChild(tbl.cloneNode(true));
            }
        }



        function OnScrollDiv(Scrollablediv) {
            document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
            document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
        }


    </script>

    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-md-4 h1">
            Monthly Payroll
        </div>
        <br />

        <div id="divPayDeleteSalaries" class="col-lg-2 pull-right" runat="server">
            <asp:Button ID="btnPaySalaries" runat="server" Text="Pay All Salary's" CssClass="btn btn-primary block full-width m-b"
                OnClientClick="return confirm('Are you sure you want to Pay Salarys ?');" OnClick="btnPaySalaries_Click"></asp:Button>
            <asp:Button ID="btnDeleteSalaries" runat="server" Text="Delete All Salary's" CssClass="btn btn-primary block full-width m-b"
                OnClientClick="return confirm('Are you sure you want to Delete Salarys ?');" OnClick="btnDeleteSalaries_Click"></asp:Button>
            &nbsp;	&nbsp;	&nbsp;	&nbsp;
        </div>

    </div>

    <div class="row wrapper border-bottom page-heading animated fadeInRight">

        <div id="divdrop" class="col-md-6" runat="server">
            <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
            </asp:DropDownList>
            <%--<asp:Button ID="btnSearchYear" runat="server" Text="GO" type="search" CssClass="btn-primary" OnClick="btnSearchYear_Click" />--%>
        </div>
        <div id="btnRunDelete" class=" col-lg-2 pull-right" runat="server">
            <asp:Button ID="btnRunPayroll" runat="server" Text="Run Payroll" OnClientClick="return confirm('Are you sure you want to Run Payroll ?');" CssClass="btn btn-primary block full-width m-b"
                OnClick="btnRunPayroll_Click"></asp:Button>
            <asp:Button ID="btnDeletePayroll" runat="server" Text="Delete Payroll" CssClass="btn btn-primary block full-width m-b"
                OnClientClick="return confirm('Are you sure you want to Delete Payroll ?');" OnClick="btnDeletePayroll_Click"></asp:Button>

        </div>
        <div id="divBackPayroll" class="col-lg-2 pull-right" runat="server" visible="false">
            <a href="employee_pay_info.aspx">
                <asp:Label runat="server" Text="&nbsp;Back to Payroll" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"></asp:Label>
            </a>
        </div>

    </div>
    <div class="h3">
        <asp:Label ID="lblZeroRecords" runat="server" Text=""></asp:Label></div>
    <asp:Button ID="btnDownload" runat="server" CssClass="btn btn-outline btn-primary btn-xs" Text="Downlaod" OnClick="btnDownload_Click" />
    <div class="wrapper-content">
        <div id="divGrid" runat="server">
           <%-- <div style="overflow: hidden;" id="DivHeaderRow">
            </div>
            <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">--%>
                <asp:GridView ID="GridViewEmployee_payrollList" runat="server" CssClass="table table-striped table-bordered table-hover dataTables-example" Width="100%" ShowFooter="True"
                    OnRowDataBound="GridViewEmployee_payrollList_RowDataBound" AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLinkEmployee" runat="server" Text='<%# Eval("Full Name") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="basic_pay" HeaderText="Basic Pay" />
                        <asp:BoundField DataField="housing_allow" HeaderText="Housing" />
                        <asp:BoundField DataField="transport_allow" HeaderText="Transport" />
                        <asp:BoundField DataField="meal_allow" HeaderText="Meal" />
                        <asp:BoundField DataField="medical_allow" HeaderText="Medical" />
                        <asp:BoundField DataField="utility_allow" HeaderText="Utility" />
                        <asp:BoundField DataField="other_allow" HeaderText="Others" />
                        <asp:BoundField DataField="total_allowances" HeaderText="Total Allowances" />
                        <asp:BoundField DataField="Gross Pay" HeaderText="Gross Pay" />
                        <asp:BoundField DataField="staff_tax" HeaderText="Tax" />
                        <asp:BoundField DataField="staff_penssion_scheme" HeaderText="Pension Scheme" />
                        <asp:BoundField DataField="staff_welfare" HeaderText="Welfare" />
                        <asp:BoundField DataField="staff_coop" HeaderText="Coop" />
                        <asp:BoundField DataField="staff_loan" HeaderText="Loan" />
                        <asp:BoundField DataField="Misc" HeaderText="Other" />
                        <asp:BoundField DataField="total_deductions" HeaderText="Total Deductions" />
                        <asp:BoundField DataField="Net Pay" HeaderText="Net Pay" />
                    </Columns>
                </asp:GridView>
            </div>
          <%--  <div id="DivFooterRow" style="overflow: hidden">
            </div>
        </div>--%>

        <h1>
            <asp:Label ID="lblDelete" runat="server" Text=""></asp:Label></h1>
        <asp:GridView ID="GridViewSalaryPaid" runat="server" CssClass="table table-striped table-bordered table-hover dataTables-example"
            AutoGenerateColumns="False" OnRowDataBound="GridViewSalaryPaid_RowDataBound">

            <Columns>

                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <asp:HyperLink ID="HLEmployee" runat="server" Text='<%# Eval("name") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="amount" HeaderText="Salary paid" />
                <asp:BoundField DataField="year" HeaderText="Year" />
                <asp:BoundField DataField="month" HeaderText="Month" />

            </Columns>

        </asp:GridView>
    </div>
</asp:Content>
