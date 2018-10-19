<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="export_data.aspx.cs" Inherits="Alex.pages.admin.export_data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-md-3 pull-right">
        <a href="../settings.aspx">
            <asp:Label runat="server" Text="&nbsp;Back to Settings" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"></asp:Label>
        </a>
    </div>
    <h2>Export School Data</h2>
    <div class="col-lg-8">
        <div class="ibox float-e-margins">
            <div class="ibox-title">
                <h5>Avaliable Downlaod Data</h5>

            </div>
            <div class="ibox-content no-padding">
                <ul class="list-group">
                    <li class="list-group-item toast-bottom-full-width">All profiles with address
                        <span class=" pull-right">
                            <asp:Button ID="btnProfile" runat="server" CssClass="btn btn-outline btn-primary" Text="Downlaod" OnClick="btnProfile_Click" />
                        </span>
                    </li>
                    <li class="list-group-item toast-bottom-full-width">All Current profiles with address
                        <span class=" pull-right">
                            <asp:Button ID="btnCurrentProfile" runat="server" CssClass="btn btn-outline btn-primary" Text="Downlaod" OnClick="btnCurrentProfile_Click" />
                        </span>
                    </li>
                    <li class="list-group-item toast-bottom-full-width">All Registrations
                        <span class="pull-right">
                            <asp:Button ID="btnReg" runat="server" CssClass="btn btn-outline btn-primary" Text="Downlaod" OnClick="btnReg_Click" />
                        </span>
                    </li>
                    <li class="list-group-item toast-bottom-full-width">All Admissions
                        <span class="pull-right">
                            <asp:Button ID="btnApp" runat="server" CssClass="btn btn-outline btn-primary" Text="Downlaod" OnClick="btnApp_Click" />
                        </span>
                    </li>
                    <li class="list-group-item toast-bottom-full-width">All Expenses
                        <span class="pull-right">
                            <asp:Button ID="btnExp" runat="server" CssClass="btn btn-outline btn-primary" Text="Downlaod" OnClick="btnExp_Click" />
                        </span>
                    </li>
                    <li class="list-group-item toast-bottom-full-width">All Purchases/Sold Items
                        <span class="pull-right">
                            <asp:Button ID="btnPurchases" runat="server" CssClass="btn btn-outline btn-primary" Text="Downlaod" OnClick="btnPurchases_Click" />
                        </span>
                    </li>
                    <li class="list-group-item toast-bottom-full-width">All School Fees Payments
                        <span class="pull-right">
                            <asp:Button ID="btnFee" runat="server" CssClass="btn btn-outline btn-primary" Text="Downlaod" OnClick="btnFee_Click" />
                        </span>
                    </li>

                    <li class="list-group-item toast-bottom-full-width">All Employees Salary
                        <span class="pull-right">
                            <asp:Button ID="btnSalary" runat="server" CssClass="btn btn-outline btn-primary" Text="Downlaod" OnClick="btnSalary_Click" />
                        </span>
                    </li>
                    <li class="list-group-item toast-bottom-full-width">All Attendance
                        <span class="pull-right">
                            <asp:Button ID="btnAttendance" runat="server" CssClass="btn btn-outline btn-primary" Text="Downlaod" OnClick="btnAttendance_Click" />
                        </span>
                    </li>
                    <li class="list-group-item toast-bottom-full-width">All Assessments
                        <span class="pull-right">
                            <asp:Button ID="btnAssess" runat="server" CssClass="btn btn-outline btn-primary" Text="Downlaod" OnClick="btnAssess_Click" />
                        </span>
                    </li>
                    <li class="list-group-item toast-bottom-full-width">All Employees and Address
                        <span class="pull-right">
                            <asp:Button ID="btnEmp" runat="server" CssClass="btn btn-outline btn-primary" Text="Downlaod" OnClick="btnEmp_Click" />
                        </span>
                    </li>
                    <li class="list-group-item toast-bottom-full-width">All payroll
                        <span class="pull-right">
                            <asp:Button ID="btnPayroll" runat="server" CssClass="btn btn-outline btn-primary" Text="Downlaod" OnClick="btnPayroll_Click" />
                        </span><br /><br />
                    </li>
                   </ul>
            </div>
        </div>
    </div>
   <%-- <asp:GridView ID="GridViewProfile" runat="server" AutoGenerateColumns="true" CssClass="table table-striped table-bordered table-hover dataTables-example"></asp:GridView>
    <asp:GridView ID="GridViewReg" runat="server" AutoGenerateColumns="true" CssClass="table table-striped table-bordered table-hover dataTables-example"></asp:GridView>
    <asp:GridView ID="GridViewApp" runat="server" AutoGenerateColumns="true" CssClass="table table-striped table-bordered table-hover dataTables-example"></asp:GridView>
    <asp:GridView ID="GridViewExp" runat="server" AutoGenerateColumns="true" CssClass="table table-striped table-bordered table-hover dataTables-example"></asp:GridView>
    <asp:GridView ID="GridViewPurchases" runat="server" AutoGenerateColumns="true" CssClass="table table-striped table-bordered table-hover dataTables-example"></asp:GridView>
    <asp:GridView ID="GridViewFee" runat="server" AutoGenerateColumns="true" CssClass="table table-striped table-bordered table-hover dataTables-example"></asp:GridView>
    <asp:GridView ID="GridViewSalary" runat="server" AutoGenerateColumns="true" CssClass="table table-striped table-bordered table-hover dataTables-example"></asp:GridView>
    <asp:GridView ID="GridViewAttendance" runat="server" AutoGenerateColumns="true" CssClass="table table-striped table-bordered table-hover dataTables-example"></asp:GridView>
    <asp:GridView ID="GridViewAssessments" runat="server" AutoGenerateColumns="true" CssClass="table table-striped table-bordered table-hover dataTables-example"></asp:GridView>
    <asp:GridView ID="GridViewEmp" runat="server" AutoGenerateColumns="true" CssClass="table table-striped table-bordered table-hover dataTables-example"></asp:GridView>
    <asp:GridView ID="GridViewPayroll" runat="server" AutoGenerateColumns="true" CssClass="table table-striped table-bordered table-hover dataTables-example"></asp:GridView>--%>



</asp:Content>
