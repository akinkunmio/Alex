<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="attendance.aspx.cs" Inherits="Alex.pages.attendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10 h1">
            Mark Attendance
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row">
            <div class="col-lg-16">
                <div class="float-e-margins">
                    <div class="col-md-16">
                        <%--<div class="col-lg-2">
                            <label>Year</label><br />
                            <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-lg-2">
                            <label>Month</label><br />
                            <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged"></asp:DropDownList>
                        </div>--%>
                        <div class="col-lg-3 ">
                            <label>Week No</label><br />
                            <asp:DropDownList ID="ddlWeek" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="ddlWeek_SelectedIndexChanged"></asp:DropDownList>
                        </div>&nbsp;&nbsp;
                        <div class="col-lg-2 ">
                            <label>Class</label><br />
                            <asp:DropDownList ID="ddlFormClass" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFormClass_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <br />
                       <%-- <asp:Button ID="btnFilterAttendance" runat="server" Text="GO" type="search" CssClass="btn-primary" OnClick="btnFilterAttendance_Click" />--%>
                    </div>
                </div>
                <div class="col-lg-6 h5 col-lg-offset-7">
                    <i class="fa fa-square text-green"></i>
                    <label>[P]&nbsp;Present</label>&nbsp;&nbsp;
                     <i class="fa fa-square text-red"></i>
                    <label>[A]&nbsp;Absent</label>&nbsp;&nbsp;
                     <i class="fa fa-square text-orange"></i>
                    <label>[L]&nbsp;Late</label>&nbsp;&nbsp;
                     <i class="fa fa-square text-blue"></i>
                    <label>[H]&nbsp;Holiday</label>
                </div>
            </div>
        </div>


        <br />
        <div class="pull-right" id="divEditUpdate" runat="server">

            <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn-primary" OnClick="btnEdit_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn-primary" OnClick="btnCancel_Click" Visible="false" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn-primary" OnClick="btnUpdate_Click" Visible="false" />

        </div>
        <br />
        <%--     <asp:DropDownList ID="ddlMultiColor" runat="server">
            <asp:ListItem Value="Red" Text="L"></asp:ListItem>
            <asp:ListItem Value="Green" Text="A"></asp:ListItem>
            <asp:ListItem Value="Black" Text="H"></asp:ListItem>
        </asp:DropDownList>--%>
        <div id="divEditRow" runat="server" class="col-md-offset-5" visible="false">
            <label>Select Day</label>
            <asp:DropDownList ID="ddlOnTopDay" runat="server">
                <asp:ListItem Value="M" Text="Monday"></asp:ListItem>
                <asp:ListItem Value="T" Text="Tuesday"></asp:ListItem>
                <asp:ListItem Value="W" Text="Wednesday"></asp:ListItem>
                <asp:ListItem Value="TH" Text="Thursday"></asp:ListItem>
                <asp:ListItem Value="F" Text="Friday"></asp:ListItem>
            </asp:DropDownList>
            <label>Select Attendance </label>
            <asp:DropDownList ID="ddlOnTopStatus" runat="server">
                <asp:ListItem Value="P" Text="Present"></asp:ListItem>
                <asp:ListItem Value="L" Text="Late"></asp:ListItem>
                <asp:ListItem Value="A" Text="Absent"></asp:ListItem>
                <asp:ListItem Value="H" Text="Holiday"></asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="btnFill" runat="server" Text="Fill" CssClass="btn-primary" OnClick="btnFill_Click" />
        </div>
        <div id="divGridAttendance">

            <asp:GridView ID="GridViewAttendance" runat="server" CssClass="table table-striped table-bordered table-hover dataTables-example"
                AutoGenerateColumns="False" OnRowDataBound="GridViewAttendance_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="reg_id" HeaderText="reg_id" SortExpression="reg_id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                        <HeaderStyle CssClass="hidden"></HeaderStyle>
                        <ItemStyle CssClass="hidden"></ItemStyle>
                    </asp:BoundField>
                   
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLinkPeople" runat="server" Text='<%# Eval("fullname") %>'></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Mon">
                        <ItemTemplate>
                            <%--<asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("1") %>' Enabled="false"></asp:TextBox>--%>
                            <asp:DropDownList ID="dd1" runat="server" Text='<%# Bind("1") %>' Enabled="false" SelectedValue='<%# Bind("1") %>' AppendDataBoundItems="true">
                                <asp:ListItem Value="P" Text="P"></asp:ListItem>
                                <asp:ListItem Value="L" Text="L"></asp:ListItem>
                                <asp:ListItem Value="A" Text="A"></asp:ListItem>
                                <asp:ListItem Value="H" Text="H"></asp:ListItem>
                                <asp:ListItem Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <%--   <ItemStyle HorizontalAlign="Center" Width="1px" />--%>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tue">
                        <ItemTemplate>
                            <%-- <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("2") %>' Enabled="false"></asp:TextBox>--%>
                            <asp:DropDownList ID="dd2" runat="server" Text='<%# Bind("2") %>' Enabled="false" SelectedValue='<%# Bind("2") %>' AppendDataBoundItems="true">
                                <asp:ListItem Value="P" Text="P"></asp:ListItem>
                                <asp:ListItem Value="L" Text="L"></asp:ListItem>
                                <asp:ListItem Value="A" Text="A"></asp:ListItem>
                                <asp:ListItem Value="H" Text="H"></asp:ListItem>
                                <asp:ListItem Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Wed">
                        <ItemTemplate>
                            <%--  <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("3") %>' Enabled="false"></asp:TextBox>--%>
                            <asp:DropDownList ID="dd3" runat="server" Text='<%# Bind("3") %>' Enabled="false" SelectedValue='<%# Bind("3") %>' AppendDataBoundItems="true">
                                <asp:ListItem Value="P" Text="P"></asp:ListItem>
                                <asp:ListItem Value="L" Text="L"></asp:ListItem>
                                <asp:ListItem Value="A" Text="A"></asp:ListItem>
                                <asp:ListItem Value="H" Text="H"></asp:ListItem>
                                <asp:ListItem Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Thu">
                        <ItemTemplate>
                            <%--<asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("4") %>' Enabled="false"></asp:TextBox>--%>
                            <asp:DropDownList ID="dd4" runat="server" Text='<%# Bind("4") %>' Enabled="false" SelectedValue='<%# Bind("4") %>' AppendDataBoundItems="true">
                                <asp:ListItem Value="P" Text="P"></asp:ListItem>
                                <asp:ListItem Value="L" Text="L"></asp:ListItem>
                                <asp:ListItem Value="A" Text="A"></asp:ListItem>
                                <asp:ListItem Value="H" Text="H"></asp:ListItem>
                                <asp:ListItem Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fri">
                        <ItemTemplate>
                            <%--<asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("5") %>' Enabled="false"></asp:TextBox>--%>
                            <asp:DropDownList ID="dd5" runat="server" Text='<%# Bind("5") %>' Enabled="false" SelectedValue='<%# Bind("5") %>' AppendDataBoundItems="true">
                                <asp:ListItem Value="P" Text="P"></asp:ListItem>
                                <asp:ListItem Value="L" Text="L"></asp:ListItem>
                                <asp:ListItem Value="A" Text="A"></asp:ListItem>
                                <asp:ListItem Value="H" Text="H"></asp:ListItem>
                                <asp:ListItem Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <%--<asp:BoundField DataField="3" HeaderText="3" />
                    <asp:BoundField DataField="4" HeaderText="4" />
                    <asp:BoundField DataField="5" HeaderText="5" />
                    <asp:BoundField DataField="6" HeaderText="6" />
                    <asp:BoundField DataField="7" HeaderText="7" />
                    <asp:BoundField DataField="8" HeaderText="8" />
                    <asp:BoundField DataField="9" HeaderText="9" />
                    <asp:BoundField DataField="10" HeaderText="10" />
                    <asp:BoundField DataField="11" HeaderText="11" />
                    <asp:BoundField DataField="12" HeaderText="12" />
                    <asp:BoundField DataField="13" HeaderText="13" />
                    <asp:BoundField DataField="14" HeaderText="14" />
                    <asp:BoundField DataField="15" HeaderText="15" />
                    <asp:BoundField DataField="16" HeaderText="16" />
                    <asp:BoundField DataField="17" HeaderText="17" />
                    <asp:BoundField DataField="18" HeaderText="18" />
                    <asp:BoundField DataField="19" HeaderText="19" />
                    <asp:BoundField DataField="20" HeaderText="20" />
                    <asp:BoundField DataField="21" HeaderText="21" />
                    <asp:BoundField DataField="22" HeaderText="22" />
                    <asp:BoundField DataField="23" HeaderText="23" />
                    <asp:BoundField DataField="24" HeaderText="24" />
                    <asp:BoundField DataField="25" HeaderText="25" />
                    <asp:BoundField DataField="26" HeaderText="26" />
                    <asp:BoundField DataField="27" HeaderText="27" />
                    <asp:BoundField DataField="28" HeaderText="28" />
                    <asp:BoundField DataField="29" HeaderText="29" />
                    <asp:BoundField DataField="30" HeaderText="30" />
                    <asp:BoundField DataField="31" HeaderText="31" />--%>
                </Columns>
            </asp:GridView>
        </div>
        <asp:Label ID="lblZeroRecords" runat="server" Text=""></asp:Label>
    </div>

    <style type="text/css">
        .cssPager td {
            padding-left: 6px;
            padding-right: 6px;
        }

        .cssPager span {
            background-color: #4f6b72;
            font-size: 18px;
        }

        .text-orange {
            color: #FF8200;
        }

        .text-green {
            color: #008000;
        }

        .text-darkGreen {
            color: #006400;
        }

        .text-red {
            color: #FF0000;
        }

        .text-yellow {
            color: #FDDA1C;
        }
        .text-blue {
            color: #0000FF;
        }
    </style>

</asp:Content>
