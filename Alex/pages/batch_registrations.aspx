<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="batch_registrations.aspx.cs" Inherits="Alex.pages.batch_registrations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function CheckAllEmp(Checkbox) {
            var GridViewProcessApplications = document.getElementById("<%=GridViewBatchRegistrations.ClientID %>");
            for (i = 1; i < GridViewBatchRegistrations.rows.length; i++) {
                GridViewBatchRegistrations.rows[i].cells[3].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
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
            Batch Registration
        </div>

        <div class="col-lg-2">
            <a href="add_new_student.aspx">
                <asp:Label runat="server" Text="Register Student" CssClass="btn btn-primary block full-width m-b"></asp:Label></a>
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <h4>From</h4>
        <div class="row">
            <div class="col-lg-12">
                <div class="float-e-margins">
                    <div class="col-md-14">
                        <div class=" col-lg-2">
                            <label>Academic Year</label><br />
                            <asp:DropDownList ID="ddlFromBRYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFromBRYear_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-lg-2">
                            <label>Term</label><br />
                            <asp:DropDownList ID="ddlFromBRTerm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFromBRTerm_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                       <%-- <div class="col-lg-2">
                            <label>Class</label><br />
                            <asp:DropDownList ID="ddlForm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlForm_SelectedIndexChanged"></asp:DropDownList>
                        </div>--%>
                        <div class="col-lg-2">
                            <label>Class</label><br />
                            <asp:DropDownList ID="ddlClass" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                      <%--    <div class="col-lg-2">
                            <label>Status</label><br />
                            <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                      <br />

                        <asp:Button ID="btnSearchFilters" runat="server" Text="GO" type="search" CssClass="btn-primary" OnClick="btnSearchFilters_Click" CausesValidation="False" />--%>
                    </div>
                </div>

            </div>
        </div>

        <%-- <h2><small>Alphabetic Search</small><asp:DataList ID="DataListAlpha"
            OnItemCommand="DataListAlpha_ItemCommand"
            OnItemDataBound="DataListAlpha_ItemDataBound" RepeatDirection="Horizontal" runat="server" Width="753px">
            <SeparatorTemplate>
            </SeparatorTemplate>
            <ItemTemplate>
                <asp:LinkButton ID="lnkbtnPaging" runat="server" CommandArgument='<%# Bind("PageIndex") %>'
                    Text='<%# Bind("PageText") %>' CausesValidation="False"></asp:LinkButton>
            </ItemTemplate>
        </asp:DataList>
        </h2>--%>
        <div id="divProcessNow" runat="server" visible="false">
        <br />
        <h4>To</h4>
        <div class="col-md-14">
            <div class=" col-lg-2">
                <label>Academic Year</label><br />
                <asp:DropDownList ID="ddlYearBReg" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlYearBReg_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="col-lg-2">
                <label>Term</label><br />
                <asp:DropDownList ID="ddlTermBReg" runat="server"></asp:DropDownList>
            </div>
           <%-- <div class="col-lg-2">
                <label>Class</label><br />
                <asp:DropDownList ID="ddlFormBReg" runat="server"></asp:DropDownList>
            </div>--%>
            <div class="col-lg-2">
                <label>Class</label><br />
                <asp:DropDownList ID="ddlClassBReg" runat="server"></asp:DropDownList>
            </div>
            <div class="col-lg-2">
              <%--  <label>Status</label><br />
                <asp:DropDownList ID="ddlStatusBReg" runat="server"></asp:DropDownList>--%>
            </div>
            <br />

            <asp:Button ID="btnBatchRegistrations" runat="server" Text="Process Now" CssClass="col-lg-offset-1 btn-primary" OnClick="btnBatchRegistrations_Click"  OnClientClick="return Validate_Checkbox()" />
        </div>
        <div class="col-lg-14 col-lg-offset-1">
            <br />
            <asp:RequiredFieldValidator ID="rfvddlYearBReg" ErrorMessage="Academic Year required" ForeColor="Red" ControlToValidate="ddlYearBReg"
                runat="server" Dispaly="Dynamic" />
            &nbsp;&nbsp;&nbsp;
            <asp:RequiredFieldValidator ID="rfvddlTermBReg" ErrorMessage="Term field required" ForeColor="Red" ControlToValidate="ddlTermBReg"
                runat="server" Dispaly="Dynamic" />
            &nbsp;&nbsp;&nbsp;
          <%--  <asp:RequiredFieldValidator ID="rfvddlFormBReg" ErrorMessage="Class field required" ForeColor="Red" ControlToValidate="ddlFormBReg"
                runat="server" Dispaly="Dynamic" />
            &nbsp; &nbsp;&nbsp;&nbsp;--%>
            <asp:RequiredFieldValidator ID="rfvddlClassBReg" ErrorMessage="Arm field required " ForeColor="Red" ControlToValidate="ddlClassBReg"
                runat="server" Dispaly="Dynamic" />
           <%-- &nbsp; &nbsp;&nbsp;
            <asp:RequiredFieldValidator ID="rfvddlStatusBReg" ErrorMessage="Status field required" ForeColor="Red" ControlToValidate="ddlStatusBReg"
                runat="server" Dispaly="Dynamic" />--%>
        </div>
     </div>
        <asp:GridView ID="GridViewBatchRegistrations" runat="server" CssClass="table table-striped table-bordered table-hover dataTables-example"
            AutoGenerateColumns="False"
            OnRowDataBound="GridViewBatchRegistrations_RowDataBound"
            OnPageIndexChanging="GridViewBatchRegistrations_PageIndexChanging"
            AllowPaging="false" PageSize="50" ShowFooter="false">
            <PagerStyle BackColor="#1ab394" ForeColor="white" HorizontalAlign="Center" CssClass="cssPager" />
            <Columns>
                <asp:TemplateField ItemStyle-Width="40px">
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkboxSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkboxSelectAll_CheckedChanged" />
                    </HeaderTemplate>

                    <ItemTemplate>
                        <asp:CheckBox ID="chkStudent" runat="server"></asp:CheckBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="person_id" HeaderText="person_id" InsertVisible="False" ReadOnly="True" SortExpression="person_id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="reg_id" HeaderText="reg_id" InsertVisible="False" ReadOnly="True" SortExpression="reg_id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLinkPeople" runat="server" Text='<%# Eval("fullname") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="dob" HeaderText="Date Of Birth" />
                <asp:BoundField DataField="form_name" HeaderText="Class" />
                <asp:BoundField DataField="Class_name" HeaderText="Arm" />
                <asp:BoundField DataField="reg_date" HeaderText="Registration Date" />
                <asp:BoundField DataField="status" HeaderText="Registration Status" />

            </Columns>
        </asp:GridView>



        <br />

    </div>
    <br />
    <div class="col-md-6">
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
    </style>

</asp:Content>
