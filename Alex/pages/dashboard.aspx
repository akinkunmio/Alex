<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="Alex.pages.dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function CheckForm() {
            if ($('#<%=TbSearch.ClientID %>').val() == "") {
                alert('Please enter Name or Date of Birth');
                return false;
            }
            return true;
        }
    </script>
    <div id="divDashboard" class="wrapper wrapper-content animated fadeInRight" runat="server">
        <div class="row">
            <div class="col-lg-8">
                <div class="ibox float-e-margins panel panel-warning">
                    <div class="ibox-title panel-heading" style="width: 100%; height: 90px; position: relative">
                        <div class="btn-group-sm  h5">Quick Links:
                            <a href="add_new.aspx" class="btn btn-danger">Add New Profile</a>
                            <a href="add_new_employee.aspx" class="btn btn-success ">Add New Employee</a>  <a href="add_new_student.aspx" class="btn btn-info">Register Student</a>
                            <a href="add_new_applicant.aspx" class="btn btn-primary">New Applicant</a> <a href="add_event_log.aspx" class="btn btn-success">Log an Event</a> 
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-4">
                <div class="ibox float-e-margins panel panel-info">
                    <div class="ibox-title panel-heading" style="width: 100%; height: 90px; position: relative">
                        <h5>Quick Profile Search: &nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="TbSearch" runat="server" placeholder="Name or Date of Birth" Style="background-color: #A5AFA4"></asp:TextBox>
                            <asp:Button ID="BtnSearch" runat="server" Text="Search" type="search" CssClass="btn-primary" OnClick="BtnSearch_Click" OnClientClick="return CheckForm()" /></h5>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-8">
                <div class="ibox float-e-margins panel panel-primary">
                    <div class="ibox-title panel-heading">
                        <h5>Last 6 Months Tuition Fee Inflow</h5>
                    </div>
                    <%--SelectCommand="sp_ms_dashboard_total_rev_by_month_to_use" SelectCommandType="StoredProcedure"></asp:SqlDataSource>--%>
                    <div class="ibox-content" style="width: 100%; height: 480px; overflow: scroll; position: relative">

                        <asp:SqlDataSource ID="SqlDataSourceGraph" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
                            SelectCommand="sp_ms_dashboard_total_rev_by_month_to_use" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <br />

                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Chart ID="chartRev" runat="server" DataSourceID="SqlDataSourceGraph" Height="376px" Width="684px" BackColor="#f0f0f0">
                            <Series>
                                <asp:Series Name="Series1" XValueMember="Month" YValueMembers="Total Amount Received" XValueType="Double"></asp:Series>

                            </Series>

                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                    <%-- <Area3DStyle Enable3D="True" />--%>
                                    <AxisY LineColor="Gray" Title="Total Amount Received">
                                        <MajorGrid LineColor="LightGray" />
                                    </AxisY>
                                    <AxisX LineColor="Gray" Title="Month">
                                        <MajorGrid LineColor="LightGray" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>

                        </asp:Chart>
                    </div>
                </div>
            </div>
            <div class="col-lg-4">
                <div class="ibox float-e-margins panel panel-danger">
                    <div class="ibox-title panel-heading">
                        <h5>Total Number of Students By Class</h5>

                    </div>
                    <div class="ibox-content" style="width: 100%; height: 480px; overflow: scroll">
                        <asp:SqlDataSource ID="SqlDataSourcePieChart" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
                            SelectCommand="sp_ms_dashboard_total_students_by_form_pie_chart"
                            SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                        <asp:Chart ID="Chart1" runat="server" DataSourceID="SqlDataSourcePieChart" Height="450px" Width="350px">
                            <Titles>
                                <asp:Title Font="Verdana, 12pt, style=Bold" Name="Title1"
                                    Text="Number of Students">
                                </asp:Title>
                            </Titles>
                            <Series>
                                <asp:Series Name="Series1" ChartType="Funnel" XValueMember="form_name" YValueMembers="total_registrations" IsVisibleInLegend="true" IsValueShownAsLabel="true" Font="Microsoft Sans Serif, 12pt"></asp:Series>
                            </Series>

                            <Legends>
                                <asp:Legend Alignment="Near" Docking="Right" Title="Class"
                                    IsTextAutoFit="True" Name="Default"
                                    LegendStyle="Column" />

                            </Legends>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartAreaPie" Area3DStyle-Enable3D="true">
                                    <Area3DStyle Enable3D="True"></Area3DStyle>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </div>
                </div>
            </div>
            <%--   <div class="col-lg-4">
                <div class="ibox float-e-margins panel panel-warning">
                    <div class="ibox-title panel-heading">
                        <h5>Quick Links</h5>
                    </div>
                    <div class="ibox-content" style=" width:100%; height: 250px"><br /><br /><br />
                        <table style="border-spacing: 10px; border-collapse: separate;">
                            <tbody>
                                <tr>
                                    <td>
                                        <a href="add_new.aspx" class="btn btn-block btn-danger">Add New Profile</a>
                                    </td>
                                   
                                    <td>
                                         <a href="add_new_employee.aspx" class="btn btn-block btn-success ">Add New Employee</a>
                                    </td>
                                 </tr>
                                <tr>
                                    <td>
                                        <a href="add_new_student.aspx" class="btn btn-block btn-info">Register Student</a>

                                    </td>
                                    
                                    <td  style="width:100px";>
                                       <a href="add_new_applicant.aspx" class="btn btn-block btn-primary">Enlist Applicant</a>

                                    </td>

                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>--%>
            <%--   <div class="col-lg-4">
                <div class="ibox float-e-margins panel panel-info">
                    <div class="ibox-title panel-heading">
                        <h5>Quick Profile Search </h5>
                    </div>
                    <div class="ibox-content" style="height:140px";><br /><br />
                        <asp:TextBox ID="TbSearch" runat="server" placeholder="Name or Date of Birth"></asp:TextBox>
                        <asp:Button ID="BtnSearch" runat="server" Text="Search" type="search" CssClass="btn-primary" OnClick="BtnSearch_Click" />
                    </div>
                </div>
            </div>--%>
        </div>

        <div class="row">
            <div class="col-lg-4">
                <div class="ibox float-e-margins panel panel-success">
                    <div class="ibox-title panel-heading">
                        <h5>School Calendar </h5>
                    </div>
                    <div class="ibox-content" style="width: 100%; height: 450px; overflow: scroll;">
                        <asp:Repeater ID="repEvents" runat="server">
                            <HeaderTemplate>
                                <div class="list-group">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="list-group-item">
                                    <div class="media">
                                        <div class="media-body">
                                            <div class="pull-left">
                                                <b><%# Eval("DateMonth").ToString() %><br />
                                                    &nbsp;</b>
                                            </div>
                                            <h5 class="media-heading text-center">
                                                <%# Eval("Event") %>
                                            </h5>
                                            <p class="text-center">
                                                <%# Eval("Description").ToString() %>
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
                                </div>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
            <div class="col-lg-4">
                <div class="ibox float-e-margins panel panel-primary">
                    <div class="ibox-title panel-heading ">
                        <h5>Students Upcoming Birthdays</h5>

                    </div>
                    <div class="ibox-content" style="width: 100%; height: 450px; overflow: scroll">
                        <asp:Repeater ID="repStudentsBirthdays" runat="server">
                            <HeaderTemplate>
                                <div class="list-group">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="list-group-item">
                                    <div class="media">
                                        <div class="media-body">
                                            <div class="pull-left">
                                                <b><%# Eval("BIRTHDAY").ToString() %><br />
                                                    Age:<%# Eval("AGE_ONE_WEEK_FROM_NOW").ToString() %></b>
                                            </div>
                                            <h5 class="media-heading text-center">
                                                <%# Eval("fullname") %>
                                            </h5>
                                            <p class="text-center">
                                                <%# Eval("class").ToString() %>
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
                                </div>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
            <div class="col-lg-4">
                <div class="ibox float-e-margins panel panel-warning">
                    <div class="ibox-title panel-heading">
                        <h5>Employees Upcoming Birthdays </h5>
                    </div>
                    <div class="ibox-content" style="width: 100%; height: 450px; overflow: scroll">
                        <asp:Repeater ID="repEmployeesBirthday" runat="server">
                            <HeaderTemplate>
                                <div class="list-group">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="list-group-item">
                                    <div class="media">
                                        <div class="media-body">
                                            <div class="pull-left">
                                                <b><%# Eval("BIRTHDAY").ToString() %><br />
                                                </b>
                                                <%-- Age:<%# Eval("AGE_ONE_WEEK_FROM_NOW").ToString() %>--%>
                                            </div>
                                            <h5 class="media-heading text-center">
                                                <%# Eval("Full Name") %>
                                            </h5>
                                            <p class="text-center">
                                                <%# Eval("dept_name").ToString() %>
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
                                </div>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>

        </div>
        <div class="row">
            <div class="col-lg-4">
                <div class="ibox float-e-margins panel panel-success">
                    <div class="ibox-title panel-heading">
                        <h5>Most Recent Inflow (Tuition Fee) </h5>

                    </div>
                    <div class="ibox-content" style="width: 100%; height: 410px; overflow: scroll">

                        <asp:GridView ID="GridViewPayments" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example">
                            <Columns>
                                <asp:BoundField DataField="fullname" HeaderText="Student Name" />
                                <asp:BoundField DataField="amount" HeaderText="Fee" />
                                <asp:BoundField DataField="received_date" HeaderText="Date Paid" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="col-lg-4">
                <div class="ibox float-e-margins panel panel-primary">
                    <div class="ibox-title panel-heading ">
                        <h5>New Students</h5>

                    </div>
                    <div class="ibox-content" style="width: 100%; height: 410px; overflow: scroll">


                        <asp:GridView ID="GridViewRegistrations" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example">
                            <Columns>
                                <asp:BoundField DataField="fullname" HeaderText="Student Name " />
                                <asp:BoundField DataField="formclass" HeaderText="Class" />
                                <asp:BoundField DataField="reg_date" HeaderText="Registration Date" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="col-lg-4">
                <div class="ibox float-e-margins panel panel-danger">
                    <div class="ibox-title panel-heading">
                        <h5>Most Recent Applications (Admissions) </h5>
                    </div>
                    <div class="ibox-content" style="width: 100%; height: 410px; overflow: scroll">

                        <asp:GridView ID="GridViewApplications" runat="server" AutoGenerateColumns="False"
                            CssClass="table table-striped table-bordered table-hover dataTables-example">
                            <Columns>
                                <asp:BoundField DataField="Name" HeaderText="Applicant Name" />
                                <asp:BoundField DataField="form_name" HeaderText="Class Applied For" />
                                <asp:BoundField DataField="application_status" HeaderText="Status" />
                                <asp:BoundField DataField="app_date" HeaderText="Application Date" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <%----%>
        </div>

        <div class="row">
        </div>
    </div>
    <!-- BEGIN JIVOSITE CODE {literal} -->
    <script type='text/javascript'>
        (function () {
            var widget_id = 'rj4nHrNnkb';
            var s = document.createElement('script'); s.type = 'text/javascript'; s.async = true; s.src = '//code.jivosite.com/script/widget/' + widget_id; var ss = document.getElementsByTagName('script')[0]; ss.parentNode.insertBefore(s, ss);
        })();</script>
    <!-- {/literal} END JIVOSITE CODE -->
</asp:Content>
