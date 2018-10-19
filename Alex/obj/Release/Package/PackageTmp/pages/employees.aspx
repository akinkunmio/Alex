<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="employees.aspx.cs" Inherits="Alex.pages.hr_payroll" %>

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
    <script>
        function searchKeyPress(e) {
            e = e || window.event;
            if (e.keyCode == 13) {
                document.getElementById('btnSearch').click();
                return false;
            }
            return true;
        }
    </script>
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10 h1">
              <asp:Label id="lblProfilePageHeading" runat="server" Text="List of Employees"></asp:Label>
              <div class="pull-right col-lg-offset-1" id="divBacktoProfiles" runat="server">
                    <a href="employees.aspx">
                            <asp:Label runat="server" Text="Back to Employees list" CssClass="btn btn-sm btn-info m-t-n-xs" Font-Bold="True"></asp:Label></a>
                    </div>
        </div>
        <div class="col-lg-2">
            <a href="add_new_employee.aspx">
                <asp:Label runat="server" Text="Add New Employee" CssClass="btn btn-primary block full-width m-b"></asp:Label></a>
        </div>

    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row">
            <div class="col-lg-12">
                <div class="float-e-margins">
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlEmployeeStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployeeStatus_SelectedIndexChanged">
                            <asp:ListItem Text="Choose Status" Value="0"></asp:ListItem>
                            <asp:ListItem>Current</asp:ListItem>
                            <asp:ListItem>Previous</asp:ListItem>
                        </asp:DropDownList>
                        <%--<asp:Button ID="btnEmployeeListByStatus" runat="server" Text="GO" type="search" CssClass="btn-primary" OnClick="btnEmployeeListByStatus_Click" />--%>
                    </div>
                    <div class="col-lg-4">

                        <asp:Panel ID="panSearch" runat="server" DefaultButton="BtnSearch">
                            <asp:TextBox ID="TbSearch" runat="server" placeholder="Name or Date of Birth" name="search" onkeypress="return searchKeyPress(event);"></asp:TextBox>
                            <asp:Button ID="BtnSearch" runat="server" Text="Search" type="search" CssClass="btn-primary" OnClick="BtnSearch_Click" OnClientClick="return CheckForm()" />
                        </asp:Panel>
                    </div>
                </div>
                <br />
            </div>
        </div>
        <br />
        <div id="divAlpha" runat="server">
            <small>Alphabetic Search</small>
            <div class="AlphabetPager">
                <asp:Repeater ID="rptAlphabets" runat="server">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" Text='<%#Eval("Value")%>' Visible='<%# !Convert.ToBoolean(Eval("Selected"))%>'
                            OnClick="Alphabet_Click" />
                        <asp:Label runat="server" Text='<%#Eval("Value")%>' Visible='<%# Convert.ToBoolean(Eval("Selected"))%>' />
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>


        <br />
        <asp:GridView ID="GridViewHr" runat="server" AutoGenerateColumns="False" DataKeyNames="emp_id" OnRowDataBound="GridViewHr_RowDataBound" ShowFooter="false"
            CssClass="table table-striped table-bordered table-hover dataTables-example" AllowPaging="false" OnPageIndexChanging="GridViewHr_PageIndexChanging" PageSize="30">
            <PagerStyle BackColor="#1ab394" ForeColor="white" HorizontalAlign="Center" CssClass="cssPager" />
            <Columns>
               <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLinkEmployee" runat="server" Text='<%# Eval("Full Name") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="Date of Birth" HeaderText="Date of Birth" />
                <asp:BoundField DataField="dept_name" HeaderText="Department" />
              
                <asp:BoundField DataField="Date Hired" HeaderText="Start Date" />
                <asp:BoundField DataField="end_date" HeaderText="End Date" />
            </Columns>
        </asp:GridView>
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

        .AlphabetPager a, .AlphabetPager span {
            font-size: 11pt;
            display: inline-block;
            line-height: 15px;
            min-width: 22px;
            text-align: center;
            text-decoration: none;
            font-weight: bold;
            padding: 0px 1px 0px 1px;
        }

        .AlphabetPager a {
            background-color: #f5f5f5;
            color: #969696;
            border: 1px solid #969696;
            width: 34px;
            height: 18px;
        }

        .AlphabetPager span {
            background-color: #A1DCF2;
            color: #000;
            border: 1px solid #3AC0F2;
        }
    </style>

</asp:Content>
