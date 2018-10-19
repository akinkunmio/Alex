<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="process_applications.aspx.cs" Inherits="Alex.pages.process_applications" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function CheckAllEmp(Checkbox) {
            var GridViewProcessApplications = document.getElementById("<%=GridViewProcessApplications.ClientID %>");
            for (i = 1; i < GridViewProcessApplications.rows.length; i++) {
                GridViewProcessApplications.rows[i].cells[3].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
        function Validate_Checkbox() {

            var chks = document.getElementsByTagName('input');
            var hasChecked = false;
            for (var i = 0; i < chks.length; i++) {
                if (chks[i].checked) {
                    hasChecked = true;
                    break;
                }
            }
            if (hasChecked == false) {
                alert("Please select at least one Student");

                return false;
            }

            return true;
        }
    </script>


    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10 h1">
            Process Applications
        </div>

        <div class="col-lg-2">
            <a href="add_new_applicant.aspx">
                <asp:Label runat="server" Text="Add New Application" CssClass="btn btn-primary block full-width m-b"></asp:Label></a>
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row">
            <div class="col-lg-12">
                <div class="float-e-margins">
                    <div class="col-md-12">
                        <div class=" col-lg-2">
                            <label>Academic Year</label><br />
                            <asp:DropDownList ID="ddlAcademicYear" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-lg-2">
                            <label>Term</label><br />
                            <asp:DropDownList ID="ddlTerm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTerm_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-lg-2">
                            <label>Class</label><br />
                            <asp:DropDownList ID="ddlForm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlForm_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-lg-2">
                            <label>Status</label><br />
                            <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <%--<br />
                        <div class="col-lg-2">
                            <asp:Button ID="btnSearchAcadmicYear" runat="server" Text="GO" type="search" CssClass="btn-primary" OnClick="btnSearchAcadmicYear_Click" />
                        </div>--%>
                    </div>
                </div>
            </div>
        </div>


        <%--      <h2><small>Alphabetic Search</small><asp:DataList ID="DataListAlpha"
            OnItemCommand="DataListAlpha_ItemCommand"
            OnItemDataBound="DataListAlpha_ItemDataBound" RepeatDirection="Horizontal" runat="server" Width="753px">
            <SeparatorTemplate>
            </SeparatorTemplate>
            <ItemTemplate>
                <asp:LinkButton ID="lnkbtnPaging" runat="server" CommandArgument='<%# Bind("PageIndex") %>'
                    Text='<%# Bind("PageText") %>'></asp:LinkButton>
            </ItemTemplate>
        </asp:DataList>
        </h2>--%>
        <br />
        <div class="pull-right">
            <asp:DropDownList runat="server" ID="ddlStatusConvert">
            </asp:DropDownList>
            <asp:Button ID="GridConvert" runat="server" Text="Process Now" OnClick="GridConvert_click" OnClientClick="return Validate_Checkbox()" />
        </div>
        <asp:GridView ID="GridViewProcessApplications" runat="server" CssClass="table table-striped table-bordered table-hover dataTables-example"
            AutoGenerateColumns="False" OnRowDataBound="GridViewProcessApplications_RowDataBound"
            OnPageIndexChanging="GridViewProcessApplications_PageIndexChanging"
            AllowPaging="false" PageSize="50" ShowFooter="false">
            <PagerStyle BackColor="#1ab394" ForeColor="white" HorizontalAlign="Center" CssClass="cssPager" />
            <Columns>
                <asp:TemplateField ItemStyle-Width="40px">
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkboxSelectAll1" runat="server" AutoPostBack="true" OnCheckedChanged="chkboxSelectAll_CheckedChanged1" />
                    </HeaderTemplate>

                    <ItemTemplate>
                        <asp:CheckBox ID="chkStudent" runat="server"></asp:CheckBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="app_id" HeaderText="app_id" InsertVisible="False" ReadOnly="True" SortExpression="app_id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLinkPeople" runat="server" Text='<%# Eval("Name") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="dob" HeaderText="Date Of Birth" />
                <asp:BoundField DataField="form_name" HeaderText="Class" />
                <asp:BoundField DataField="app_date" HeaderText="Application Date" />
                <asp:BoundField DataField="application_status" HeaderText="Application Status" />

            </Columns>
        </asp:GridView>

        <asp:Label ID="lblZeroRecords" runat="server" Text=""></asp:Label>

        <br />

    </div>
    <br />
    <div>
        <div>
        </div>
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
    </style>

</asp:Content>
