<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="registration_profile_delete.aspx.cs" Inherits="Alex.pages.admin.registration_profile_delete" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function CheckAllEmp(Checkbox) {
            var GridViewProcessApplications = document.getElementById("<%=GvBatchPrOrReg.ClientID %>");
            for (i = 1; i < GvBatchPrOrReg.rows.length; i++) {
                GvBatchPrOrReg.rows[i].cells[3].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
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
            Delete Non Active Registered Students
        </div>

        <div class="col-lg-2">
            <a href="../settings.aspx">
            <asp:Label runat="server" Text="&nbsp;Back to Settings" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"></asp:Label>
        </a>
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <div id="divProcessNow" runat="server" visible="false">
         
            <div class=" col-lg-6">
                <label>Choose</label><br />
                <asp:DropDownList ID="ddlRegOrProf" runat="server" >
                     <asp:ListItem Text="Remove Registration as a Student Entirely" Value="1" />
                     <asp:ListItem Text="Delete/Remove Student Profile Entirely" Value="2" />
                </asp:DropDownList>
            </div>
                      
            <br />

            <asp:Button ID="btnProfOrRegDelete" runat="server" Text="Delete Now" CssClass="col-lg-offset-1 btn-primary" OnClick="btnProfOrRegDelete_Click"  OnClientClick="return Validate_Checkbox()" />
       
        <div class="col-lg-14 col-lg-offset-1">
            
            <asp:RequiredFieldValidator ID="rfvddlRegOrProf" ErrorMessage="Academic Year required" ForeColor="Red" ControlToValidate="ddlRegOrProf"
                runat="server" Dispaly="Dynamic" />
           
        </div>
     </div>
        <asp:GridView ID="GvBatchPrOrReg" runat="server" CssClass="table table-striped table-bordered table-hover dataTables-example"
            AutoGenerateColumns="False"
            OnRowDataBound="GvBatchPrOrReg_RowDataBound"
            OnPageIndexChanging="GvBatchPrOrReg_PageIndexChanging"
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
                <asp:BoundField DataField="class" HeaderText="Class" />
                <asp:BoundField DataField="acad_year" HeaderText="Academic Year" />
                <asp:BoundField DataField="term_name" HeaderText="Term" />
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
