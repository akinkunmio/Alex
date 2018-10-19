<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="profile.aspx.cs" Inherits="Alex.pages.profile" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function menuselection(arg) {
            $(arg).removeClass("btn btn-primary");
            $(arg).addClass("btn btn-active");
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../scripts/css/pagestyle.css" rel="stylesheet" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>

    <%-- <script src="scripts/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="scripts/jquery.blockUI.js" type="text/javascript"></script>--%>

    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <style>
        .ui-datepicker {
            margin-top: 7px;
            margin-left: -30px;
            margin-bottom: 0px;
            position: absolute;
            z-index: 1000;
        }
    </style>
    <script type="text/javascript">
        $(function () { $('.datepick').prop('readonly', true).datepicker({ changeMonth: true, changeYear: true, yearRange: '-100:' + new Date().getFullYear(), dateFormat: 'dd/mm/yy' }); });
        function PrintDetailsView() {
            var detailsview = document.getElementById('<%= DetailsViewiQKeyPassword.ClientID %>');
            var printWindow = window.open('', '', 'width=500,height=500,toolbar=0,scrollbars=1,status=0,resizable=1');
            printWindow.document.write(detailsview.outerHTML);
            printWindow.document.close();
            printWindow.focus();
            printWindow.print();
            printWindow.close();
        }
        $().ready(function () {
            $('.list').change(function () {
                if (this.value == 'Bank') {
                    $(".bank").show();
                    var validator = document.getElementById("<%=rfvddlBankName.ClientID %>");
                    validator.enabled = true;
                }
                else {
                    $(".bank").hide();
                    var validator = document.getElementById("<%=rfvddlBankName.ClientID %>");
                    validator.enabled = false;
                }
            });
        });

        function ValidateUploadPic() {
            var fuData = document.getElementById("<%=FileUpload1.ClientID %>");
            if (fuData.value == '') {
                return false;
            }
            else {
                document.getElementById("<%=Upload.ClientID %>").style.display = "";
            }
            return true;
        }

        function CheckAllEmp(Checkbox) {
            var GridViewBulk = document.getElementById("<%=GridViewBulkPurchases.ClientID %>");
            for (i = 1; i < GridViewBulkPurchases.rows.length; i++) {
                GridViewBulkPurchases.rows[i].cells[3].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
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
                alert("Please select at least one Sale Item");

                return false;
            }

            return true;
        }

        <%-- function CheckBoxSelectionValidation() {
            var gridView = document.getElementById("<%=GridViewBulkPurchases.ClientID %>");
            for (var i = 1; i < gridView.rows.length; i++) {
                var count = 0;
                var chkConfirm = gridView.rows[i].cells[0].getElementsByTagName('input')[0];
                var txtComment = gridView.rows[i].cells[9].getElementsByTagName('input')[0];
                var hasChecked = false;
                if (chkConfirm.checked && txtComment.value != "") {
                    hasChecked = true;
                    break;

                } else if (chkConfirm.checked && txtComment.value == "") {

                    alert("Please enter a Reference for Sale Item.");
                    return false;
                }
            }
            if (hasChecked == false) {
                alert("Please select at least one Sale Item");

                return false;
            }

            return true;
        }--%>

        //function ValidateAll() {
        //    var count = 0;
        //    $('.dummyClass').each(function (index, item) {
        //        if ($(this).val() != "") {
        //            count = 1;
        //        }
        //    }, 0);

        //    if (count == 0){ alert("fill atleast one");
        //        return false;
        //    } return true;
        //}


    </script>
    <script type="text/javascript">
        function SetTarget() {
            originalTarget = document.forms[0].target;
            document.forms[0].target = "_blank";
            window.setTimeout("document.forms[0].target=originalTarget;", 300);
            return true;
        }
    </script>

    <div class="col-md-12  dashboard-header">
        <div class="col-md-9">
            <asp:Label ID="LabelProfileName" runat="server" Text="Profile Name label" CssClass="h1"></asp:Label>
        </div>

        <div class="col-md-2 pull-right">
            <a href="../pages/people.aspx">
                <asp:Label runat="server" Text="Back to Profiles" CssClass="btn btn-sm btn-info m-t-n-xs" Font-Bold="True"></asp:Label>
            </a>
        </div>
    </div>
    <div class="wrapper wrapper-content  fadeInRight">
        <div class="ibox-content" style="padding: 10px 20px 20px 50px;">


            <asp:Button ID="BtnProfile" runat="server" Text="Profile" CssClass="btn  btn-primary" OnClick="BtnProfile_Click" CausesValidation="False" />
            <asp:Button ID="BtnAddress" runat="server" Text="Addresses" CssClass="btn  btn-primary" OnClick="BtnAddress_Click" CausesValidation="False" />
            <asp:Button ID="BtnApplication" runat="server" Text="Admissions" CssClass="btn   btn-primary " OnClick="BtnApplication_Click" CausesValidation="False" />
            <asp:Button ID="BtnRegistration" runat="server" Text="Registrations" CssClass="btn   btn-primary " OnClick="BtnRegistration_Click" CausesValidation="False" />
            <asp:Button ID="BtnStatmentofAccount" runat="server" Text="Statement of Account" CssClass="btn  btn-primary " OnClick="BtnStatmentofAccount_Click" CausesValidation="False" />
            <asp:Button ID="BtnAttendance" runat="server" Text="Attendance" CssClass="btn  btn-primary" OnClick="BtnAttendance_Click" CausesValidation="False" />
            <asp:Button ID="BtnAssessment" runat="server" Text="Assessments" CssClass="btn  btn-primary " OnClick="BtnAssessment_Click" CausesValidation="False" />
            <asp:Button ID="BtnPurchases" runat="server" Text="Purchases" CssClass="btn  btn-primary" OnClick="BtnPurchases_Click" CausesValidation="False" />
            <asp:Button ID="BtnNotes" runat="server" Text="Notes" CssClass="btn btn-primary" OnClick="BtnNotes_Click" CausesValidation="False" />



        </div>
        <asp:SqlDataSource ID="SqlDataSourceProfile" runat="server" ConnectionString="<%$ ConnectionStrings:MyStuConnectionString %>"
            SelectCommand="sp_ms_person_profile" SelectCommandType="StoredProcedure"
            UpdateCommand="sp_ms_person_edit" UpdateCommandType="StoredProcedure" OnUpdated="SqlDataSourceProfile_Updated"
            DeleteCommand="sp_ms_person_delete" DeleteCommandType="StoredProcedure" OnDeleting="SqlDataSourceProfile_Deleting" OnDeleted="SqlDataSourceProfile_Deleted">

            <DeleteParameters>
                <asp:Parameter Name="person_id" Type="Int32" />
                <asp:Parameter Name="from" Type="String" />
            </DeleteParameters>
            <SelectParameters>
                <asp:QueryStringParameter Name="person_id" QueryStringField="PersonId" Type="Int32" />
                <%--<asp:Parameter Name="person_id" Type="Int32" />--%>
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="person_id" Type="Int32" />
                <asp:Parameter Name="f_name" Type="String" />
                <asp:Parameter Name="m_name" Type="String" />
                <asp:Parameter Name="l_name" Type="String" />
                <asp:Parameter Name="title" Type="String" />
                <asp:Parameter DbType="String" Name="dob" />
                <asp:Parameter Name="gender" Type="String" />
                <asp:Parameter Name="nationality" Type="String" />
                <asp:Parameter Name="ethnicity" Type="String" />
                <asp:Parameter Name="religion" Type="String" />
                <asp:Parameter Name="parent_guardian_fname" Type="String" />
                <asp:Parameter Name="parent_guardian_lname" Type="String" />
                <asp:Parameter Name="p_g_relationship" Type="String" />
                <asp:Parameter Name="p_g_title" Type="String" />
                <asp:Parameter Name="p_g_contact_no1" Type="String" />
                <asp:Parameter Name="p_g_contact_no2" Type="String" />
                <asp:Parameter Name="p_g_email_add" Type="String" />
                <asp:Parameter Name="updated_by" Type="String" />
                <asp:Parameter Name="person_phone_no" Type="String" />
                <asp:Parameter Name="blood_group" Type="String" />
                <asp:Parameter Name="stu_id" Type="String" />
                <asp:Parameter Name="notes" Type="String" />

            </UpdateParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSourceAddress" runat="server" ConnectionString="<%$ ConnectionStrings:MyStuConnectionString %>"
            SelectCommand="sp_ms_person_address_current" SelectCommandType="StoredProcedure"
            UpdateCommand="sp_ms_person_address_edit" UpdateCommandType="StoredProcedure"
            DeleteCommand="sp_ms_person_address_delete" DeleteCommandType="StoredProcedure" OnDeleted="SqlDataSourceAddress_Deleted"
            InsertCommand="sp_ms_person_address_add" InsertCommandType="StoredProcedure">

            <DeleteParameters>
                <asp:Parameter Name="add_id" Type="Int32" />
            </DeleteParameters>

            <InsertParameters>
                <asp:QueryStringParameter Name="person_id" Type="Int32" QueryStringField="PersonId" />
                <asp:Parameter Name="address_line_1" Type="String" />
                <asp:Parameter Name="address_line_2" Type="String" />
                <asp:Parameter Name="address_line_3" Type="String" />
                <asp:Parameter Name="lga_city" Type="String" />
                <asp:Parameter Name="STATE" Type="String" />
                <asp:Parameter Name="zip_postal_code" Type="String" />
                <asp:Parameter Name="country" Type="String" />
                <%--<asp:Parameter Name="STATUS" Type="String" />--%>
                <asp:Parameter DbType="Date" Name="START_DATE" />
                <%--<asp:Parameter DbType="Date" Name="end_date" />--%>
                <asp:Parameter Name="created_by" Type="String" />
            </InsertParameters>

            <SelectParameters>
                <asp:QueryStringParameter Name="person_id" QueryStringField="PersonId" Type="Int32" />

            </SelectParameters>

            <UpdateParameters>
                <asp:Parameter Name="add_id" Type="Int32" />
                <asp:Parameter Name="person_id" Type="Int32" />
                <asp:Parameter Name="address_line_1" Type="String" />
                <asp:Parameter Name="address_line_2" Type="String" />
                <asp:Parameter Name="address_line_3" Type="String" />
                <asp:Parameter Name="lga_city" Type="String" />
                <asp:Parameter Name="state" Type="String" />
                <asp:Parameter Name="zip_postal_code" Type="String" />
                <asp:Parameter Name="country" Type="String" />
                <asp:Parameter Name="status" Type="String" />
                <asp:Parameter DbType="Date" Name="start_date" />
                <asp:Parameter DbType="Date" Name="end_date" />
                <asp:Parameter Name="updated_by" Type="String" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSourceApplication" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
            DeleteCommand="sp_ms_person_application_delete" DeleteCommandType="StoredProcedure" OnDeleted="SqlDataSourceApplication_Deleted"
            InsertCommand="sp_ms_person_application_add" InsertCommandType="StoredProcedure" OnInserted="SqlDataSourceApplication_Inserted" OnInserting="SqlDataSourceApplication_Inserting"
            SelectCommand="sp_ms_person_application_current" SelectCommandType="StoredProcedure"
            UpdateCommand="sp_ms_person_application_edit" UpdateCommandType="StoredProcedure" OnUpdated="SqlDataSourceApplication_Updated" OnUpdating="SqlDataSourceApplication_Updating">

            <DeleteParameters>
                <asp:Parameter Name="app_id" Type="Int32" />
            </DeleteParameters>

            <InsertParameters>
                <asp:QueryStringParameter Name="person_id" Type="Int32" QueryStringField="Personid" />
                <asp:Parameter Name="application_status" Type="String" />
                <asp:Parameter DbType="String" Name="app_date" />
                <asp:Parameter Name="created_by" Type="String" />
                <asp:Parameter Name="acad_year" Type="String" />
                <asp:Parameter Name="term_name" Type="String" />
                <asp:Parameter Name="form_name" Type="String" />
                <asp:Parameter Name="from" Type="String" />

            </InsertParameters>

            <SelectParameters>
                <asp:QueryStringParameter Name="person_id" QueryStringField="PersonId" Type="Int32" />
            </SelectParameters>

            <UpdateParameters>
                <asp:Parameter Name="term_name" Type="String" />
                <asp:Parameter DbType="String" Name="app_date" />
                <asp:Parameter Name="application_status" Type="String" />
                <asp:Parameter Name="form_name" Type="String" />
                <asp:Parameter Name="acad_year" Type="String" />
                <asp:Parameter Name="updated_by" Type="String" />
                <asp:Parameter Name="app_id" Type="Int32" />
                <asp:Parameter Name="from" Type="String" />
            </UpdateParameters>

        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSourceRegistration" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
            SelectCommand="sp_ms_person_registration_current" SelectCommandType="StoredProcedure"
            DeleteCommand="sp_ms_person_registration_delete" DeleteCommandType="StoredProcedure" OnDeleted="SqlDataSourceRegistration_Deleted" OnDeleting="SqlDataSourceRegistration_Deleting"
            InsertCommand="sp_ms_person_registration_add" InsertCommandType="StoredProcedure" OnInserted="SqlDataSourceRegistration_Inserted" OnInserting="SqlDataSourceRegistration_Inserting"
            UpdateCommand="sp_ms_person_registration_edit" UpdateCommandType="StoredProcedure" OnUpdated="SqlDataSourceRegistration_Updated" OnUpdating="SqlDataSourceRegistration_Updating">
            <DeleteParameters>
                <asp:Parameter Name="reg_id" Type="Int32" />
                <asp:Parameter Name="from" Type="String" />
            </DeleteParameters>
            <InsertParameters>
                <asp:QueryStringParameter Name="person_id" Type="Int32" QueryStringField="Personid" />
                <asp:Parameter Name="app_id" Type="Int32" />
                <asp:Parameter Name="reg_date" DbType="String" />
                <asp:Parameter Name="status" Type="String" />
                <asp:Parameter Name="created_by" Type="String" />
                <asp:Parameter Name="acad_year" Type="String" />
                <asp:Parameter Name="term_name" Type="String" />
                <asp:Parameter Name="class_name" Type="String" />
                <asp:Parameter Name="form_name" Type="String" />
                <asp:Parameter Name="type_description" Type="String" />
                <asp:Parameter Name="from" Type="String" />
            </InsertParameters>
            <SelectParameters>
                <asp:QueryStringParameter Name="person_id" QueryStringField="PersonId" Type="Int32" />
            </SelectParameters>
            <UpdateParameters>


                <asp:Parameter Name="reg_id" Type="Int32" />
                <asp:Parameter Name="term_name" Type="String" />
                <asp:Parameter Name="class_name" Type="String" />
                <asp:Parameter Name="form_name" Type="String" />
                <asp:Parameter Name="acad_year" Type="String" />
                <asp:Parameter DbType="String" Name="reg_date" />
                <asp:Parameter Name="status" Type="String" />
                <asp:Parameter Name="created_by" Type="String" />
                <asp:Parameter Name="type_description" Type="String" />
                <asp:Parameter Name="from" Type="String" />
            </UpdateParameters>
        </asp:SqlDataSource>

        <div id="Profiledropdown">
            <asp:SqlDataSource ID="SqlDataSourceNationalityDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
                SelectCommand="SELECT nationality FROM ms_dropdown_countries2"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSourceEthnicityDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
                SelectCommand="SELECT ethnicity FROM ms_dropdown_nigeria_ethnicity"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSourceReligionDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
                SelectCommand="SELECT status_name FROM ms_status Where category = 'religion'"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSourceCountryDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
                SelectCommand="sp_ms_countries_dropdown"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSourceCityDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
                SelectCommand="sp_ms_nigeria_lagos_state_lga_dropdown"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSourceTitleDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
                SelectCommand="SELECT status_name FROM ms_status Where category ='Title'"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSourceGenderDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
                SelectCommand="SELECT status_name FROM ms_status Where category = 'Gender'"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSourcePGTitleDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
                SelectCommand="SELECT status_name FROM ms_status Where category = 'PG Title'"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSourceRelationDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
                SelectCommand="SELECT status_name FROM ms_status Where category = 'PG Relationship'"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSourceBloodGroup" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
                SelectCommand="sp_ms_blood_group_dropdown"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSourceStateDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
                SelectCommand="sp_ms_nigerian_states_dropdown"></asp:SqlDataSource>
        </div>
        <div id="divProfile" runat="server">
            <div class="row">
                <div class="col-lg-12">
                    <div class="col-md-8">
                        <asp:DetailsView ID="DetailsViewProfile" runat="server" AutoGenerateRows="False" DataKeyNames="person_id" DataSourceID="SqlDataSourceProfile"
                            CssClass="table table-striped table-bordered table-hover dataTables-example" Height="16px" Width="700px">
                            <Fields>
                                <asp:TemplateField ShowHeader="False">
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                                        &nbsp;<asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="False" CommandName="Edit" Text=" Edit"></asp:LinkButton>
                                        &nbsp;<asp:LinkButton ID="LinkButtonDelete" runat="server" CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete ?');" Text=" Delete"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="btn btn-primary" Width="80px" />
                                    <HeaderStyle Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Title<span class="required">*</span>
                                    </HeaderTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlTitle" runat="server" DataValueField="status_name" DataTextField="status_name" AppendDataBoundItems="true" SelectedValue='<%# Bind("title") %>'
                                            Text='<%# Bind("title") %>' DataSourceID="SqlDataSourceTitleDropDown">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvTitle" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlTitle"
                                            runat="server" Dispaly="Dynamic" />
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbTitle" runat="server" Text='<%# Bind("title") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("title") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="person_id" HeaderText="person_id" SortExpression="person_id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                                    <ItemStyle CssClass="hidden"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Last Name" SortExpression="l_name">
                                    <HeaderTemplate>
                                        Last Name <span class="required">*</span>
                                    </HeaderTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbLName" runat="server" Text='<%# Bind("l_name") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvLName" ErrorMessage="*" ForeColor="Red" ControlToValidate="tbLName"
                                            runat="server" Dispaly="Dynamic" />
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="TtbLName" runat="server" Text='<%# Bind("l_name") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblLName" runat="server" Text='<%# Bind("l_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="First Name" SortExpression="f_name">
                                    <HeaderTemplate>
                                        First Name <span class="required">*</span>
                                    </HeaderTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbFName" runat="server" Text='<%# Bind("f_name") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvFName" ErrorMessage="*" ForeColor="Red" ControlToValidate="tbFName"
                                            runat="server" Dispaly="Dynamic" />
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbFName" runat="server" Text='<%# Bind("f_name") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblFName" runat="server" Text='<%# Bind("f_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="m_name" HeaderText="Middle Name" SortExpression="m_name" />

                                <asp:TemplateField HeaderText="Gender" SortExpression="Gender">
                                    <HeaderTemplate>
                                        Gender <span class="required">*</span>
                                    </HeaderTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlGender" runat="server" Text='<%# Bind("Gender") %>' DataValueField="status_name" DataTextField="status_name"
                                            AppendDataBoundItems="true" SelectedValue='<%# Bind("Gender") %>' DataSourceID="SqlDataSourceGenderDropDown">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlGender" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlGender"
                                            runat="server" Dispaly="Dynamic" />
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbGender" runat="server" Text='<%# Bind("Gender") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblGender" runat="server" Text='<%# Bind("Gender") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Blood Group">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlbloodGroup" runat="server" Text='<%# Bind("blood_group") %>' DataValueField="status_name" DataTextField="status_name"
                                            AppendDataBoundItems="true" SelectedValue='<%# Bind("blood_group") %>' DataSourceID="SqlDataSourceBloodGroup">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:DropDownList ID="ddlbloodGroup" runat="server" Text='<%# Bind("blood_group") %>' DataValueField="status_name" DataTextField="status_name"
                                            AppendDataBoundItems="true" SelectedValue='<%# Bind("blood_group") %>' DataSourceID="SqlDataSourceBloodGroup">
                                        </asp:DropDownList>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblBloodGroup" runat="server" Text='<%# Bind("blood_group") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Student Phone No" SortExpression="person_phone_no">
                                    <HeaderTemplate>
                                        Student Phone No 
                                    </HeaderTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbStudentMobileNo" runat="server" Text='<%# Bind("person_phone_no") %>'></asp:TextBox>

                                        <asp:RegularExpressionValidator ID="revtbStudentMobileNo" ControlToValidate="tbStudentMobileNo" runat="server"
                                            ErrorMessage="Phone Number should be in 11 digits " ForeColor="Red" ValidationExpression="\d{11}"></asp:RegularExpressionValidator>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbInsertStudentMobileNo" runat="server" Text='<%# Bind("person_phone_no") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStudentMobileNo" runat="server" Text='<%# Bind("person_phone_no") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date of Birth" SortExpression="Date of Birth">
                                    <HeaderTemplate>
                                        Date of Birth <span class="required">*</span>
                                    </HeaderTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbDOB" runat="server" rel="date" type="text" CssClass="datepick input-group date" Text='<%# Bind("dob") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDOB" ErrorMessage="*" ForeColor="Red" ControlToValidate="tbDOB"
                                            runat="server" Dispaly="Dynamic" />
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbDOB" runat="server" CssClass="datepick" Text='<%# Bind("dob") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDOB" runat="server" Text='<%# Bind("dob") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="AGE_NOW" HeaderText="Age" SortExpression="AGE_NOW" ReadOnly="true" />
                                <asp:TemplateField HeaderText="Nationality" SortExpression="nationality">
                                    <HeaderTemplate>
                                        Nationality <span class="required">*</span>
                                    </HeaderTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlNationality" runat="server" DataValueField="nationality" DataTextField="nationality" Text='<%# Bind("nationality") %>' DataSourceID="SqlDataSourceNationalityDropDown"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvNationality" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlNationality"
                                            runat="server" Dispaly="Dynamic" />
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:DropDownList ID="ddlNationality" runat="server" DataValueField="nationality" DataTextField="nationality" Text='<%# Bind("nationality") %>' DataSourceID="SqlDataSourceNationalityDropDown"></asp:DropDownList>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNationality" runat="server" Text='<%# Bind("nationality") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ethnicity" SortExpression="ethnicity">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlEthnicity" runat="server" DataValueField="ethnicity" DataTextField="ethnicity" Text='<%# Bind("ethnicity") %>' DataSourceID="SqlDataSourceEthnicityDropDown"
                                            SelectedValue='<%# Bind("ethnicity") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvEthnicity" ErrorMessage="*" ForeColor="Red" ControlToValidate="tbEthnicity"
                        runat="server" Dispaly="Dynamic" />--%>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:DropDownList ID="ddlEthnicity" runat="server" DataValueField="ethnicity" DataTextField="ethnicity" Text='<%# Bind("ethnicity") %>' DataSourceID="SqlDataSourceEthnicityDropDown"></asp:DropDownList>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblEthnicity" runat="server" Text='<%# Bind("ethnicity") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Religion" SortExpression="religion">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlReligion" runat="server" DataValueField="status_name" DataTextField="status_name" Text='<%# Bind("religion") %>' DataSourceID="SqlDataSourceReligionDropDown"
                                            SelectedValue='<%# Bind("religion") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:DropDownList ID="ddlReligion" runat="server" DataValueField="status_name" DataTextField="status_name" Text='<%# Bind("religion") %>' DataSourceID="SqlDataSourceReligionDropDown"></asp:DropDownList>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblReligion" runat="server" Text='<%# Bind("religion") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Parent Title" SortExpression="p_g_title">
                                    <HeaderTemplate>
                                        Parent Title <span class="required">*</span>
                                    </HeaderTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlParentTitle" runat="server" DataValueField="status_name" DataTextField="status_name" Text='<%# Bind("p_g_title") %>'
                                            AppendDataBoundItems="true" SelectedValue='<%# Bind("p_g_title") %>' DataSourceID="SqlDataSourcePGTitleDropDown">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvParentTitle" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlParentTitle"
                                            runat="server" Dispaly="Dynamic" />
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbParentTitle" runat="server" Text='<%# Bind("p_g_title") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblParentTitle" runat="server" Text='<%# Bind("p_g_title") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Parent Frist Name" SortExpression="parent_guardian_fname">
                                    <HeaderTemplate>
                                        Parent First Name
                                    </HeaderTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbParentFName" runat="server" Text='<%# Bind("parent_guardian_fname") %>'></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="rfvParentFName" ErrorMessage="*" ForeColor="Red" ControlToValidate="tbParentFName"
                                        runat="server" Dispaly="Dynamic" />--%>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbParentFName" runat="server" Text='<%# Bind("parent_guardian_fname") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblParentFName" runat="server" Text='<%# Bind("parent_guardian_fname") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Parent Last Name" SortExpression="parent_guardian_lname">
                                    <HeaderTemplate>
                                        Parent Last Name <span class="required">*</span>
                                    </HeaderTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbParentLName" runat="server" Text='<%# Bind("parent_guardian_lname") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvParentLName" ErrorMessage="*" ForeColor="Red" ControlToValidate="tbParentLName"
                                            runat="server" Dispaly="Dynamic" />
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbParentLName" runat="server" Text='<%# Bind("parent_guardian_lname") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblParentLName" runat="server" Text='<%# Bind("parent_guardian_lname") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Relationship " SortExpression="p_g_relationship">
                                    <HeaderTemplate>
                                        Relationship  <span class="required">*</span>
                                    </HeaderTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlRelationship" runat="server" Text='<%# Bind("p_g_relationship") %>' DataValueField="status_name" DataTextField="status_name"
                                            AppendDataBoundItems="true" SelectedValue='<%# Bind("p_g_relationship") %>' DataSourceID="SqlDataSourceRelationDropDown">
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="rfvRelationship" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlRelationship"
                                            runat="server" Dispaly="Dynamic" />
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbRelationship" runat="server" Text='<%# Bind("p_g_relationship") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblRelationship" runat="server" Text='<%# Bind("p_g_relationship") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mobile" SortExpression="p_g_contact_no1">
                                    <HeaderTemplate>
                                        Contact No Mobile <span class="required">*</span>
                                    </HeaderTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbMobileNo" runat="server" Text='<%# Bind("p_g_contact_no1") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvMobileNo" ErrorMessage="*" ForeColor="Red" ControlToValidate="tbMobileNo"
                                            runat="server" Dispaly="Dynamic" />
                                        <asp:RegularExpressionValidator ID="revtbMobileNo" ControlToValidate="tbMobileNo" runat="server"
                                            ErrorMessage="Phone Number should be in 11 digits " ForeColor="Red" ValidationExpression="\d{11}"></asp:RegularExpressionValidator>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbMobileNo" runat="server" Text='<%# Bind("p_g_contact_no1") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMobileNo" runat="server" Text='<%# Bind("p_g_contact_no1") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contact No Home/Office" SortExpression="p_g_contact_no2">

                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbHomeNo" runat="server" Text='<%# Bind("p_g_contact_no2") %>'></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revtbHomeNo" ControlToValidate="tbHomeNo" runat="server"
                                            ErrorMessage="Phone Number should be in 11 digits " ForeColor="Red" ValidationExpression="\d{11}"></asp:RegularExpressionValidator>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbHomeNo" runat="server" Text='<%# Bind("p_g_contact_no2") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblHomeNo" runat="server" Text='<%# Bind("p_g_contact_no2") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email" SortExpression="p_g_email_add">

                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbEmail" runat="server" Text='<%# Bind("p_g_email_add") %>'></asp:TextBox>
                                        <%-- <asp:RequiredFieldValidator ID="rfvEmail" ErrorMessage="*" ForeColor="Red" ControlToValidate="tbEmail"
                        runat="server" Dispaly="Dynamic" />--%>
                                        <asp:RegularExpressionValidator ID="revtbEmail" runat="server"
                                            ErrorMessage="Invalid Email" ControlToValidate="tbEmail" ForeColor="Red"
                                            SetFocusOnError="True" ValidationExpression="^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$" />
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbEmail" runat="server" Text='<%# Bind("p_g_email_add") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("p_g_email_add") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Student ID">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbStuId" runat="server" Text='<%# Bind("stu_id") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbStuId" runat="server" Text='<%# Bind("stu_id") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStuId" runat="server" Text='<%# Bind("stu_id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Notes">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbNotes" runat="server" Text='<%# Bind("notes") %>' MaxLength="500" Height="40px" Wrap="true"></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbNotes" runat="server" Text='<%# Bind("notes") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNotes" runat="server" Text='<%# Bind("notes") %>' Height="40px" Wrap="true"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Fields>
                        </asp:DetailsView>
                    </div>
                    <div class="col-md-4">
                        <div id="divProfilePic" runat="server" visible="false" style="width: 200px">
                            <div class="form-group">


                                <asp:Image ID="ProfilePicture" runat="server" length="200px" Width="200px" AlternateText="NO Image Found" CssClass="ibox-content" /><br />
                                <br />
                                <asp:ScriptManager ID="ScriptManagerUpload" runat="server"></asp:ScriptManager>
                                <asp:UpdatePanel ID="UpdatePanelUpload" runat="server" UpdateMode="conditional">
                                    <ContentTemplate>
                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                        <asp:Button ID="Upload" runat="server" Text="Upload" CssClass="btn btn-primary btn-xs" OnClick="Upload_Click" Style="display: none;" />
                                        <asp:CustomValidator ID="CustomValidatorPic" runat="server" ControlToValidate="FileUpload1" ClientValidationFunction="ValidateUploadPic" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="Upload" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <br />
                                <div class="ibox-content">
                                    <label>Student ID:</label><asp:Label runat="server" ID="lblStudentId"></asp:Label><br />
                                    <%--<asp:Button ID="btnIdCardPrint" runat="server" Text="Print ID Card" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" OnClick="btnIdCardPrint_Click" OnClientClick="SetTarget();" />--%>
                                    <label>Parent's iQ Access: </label>
                                    <asp:Button ID="btnParentEnable" runat="server" OnClick="btnParentEnable_Click" Text="N/A" />
                                </div>
                            </div>

                        </div>
                    </div>

                    <div class="col-md-4">
                        <div id="diviQOnlineKey" runat="server" visible="false" style="width: 240px">
                            <div class="form-group">
                                <div class="editor-label">
                                    <br />
                                    <br />
                                    <label>iQ Online Key credentials</label>
                                </div>
                                <br />
                                <asp:Button ID="BtnKeyGenerate" runat="server" Text="Generate Key" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" OnClick="BtnKeyGenerate_Click" />
                                <div class="ibox-content" id="DivDetailsView" runat="server" visible="false">
                                    <asp:DetailsView ID="DetailsViewiQKeyPassword" runat="server" CssClass="table table-striped table-bordered table-hover dataTables-example" AutoGenerateRows="False">
                                        <Fields>
                                            <asp:BoundField DataField="person_id" HeaderText="psersonId" InsertVisible="False" ReadOnly="True" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                <HeaderStyle CssClass="hidden"></HeaderStyle>
                                                <ItemStyle CssClass="hidden"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="p_key" HeaderText="Key" ReadOnly="True"></asp:BoundField>
                                            <asp:BoundField DataField="l_name" HeaderText="Username" ReadOnly="True"></asp:BoundField>
                                            <asp:BoundField DataField="password" HeaderText="Password" ReadOnly="True"></asp:BoundField>
                                        </Fields>
                                    </asp:DetailsView>
                                    <asp:Button ID="btnPrint" runat="server" Text="Print" Visible="false" OnClientClick="PrintDetailsView()" />
                                    <asp:Button ID="btniQPwdReset" runat="server" Text="Reset Password" Visible="false" OnClick="btniQPwdReset_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="divAddress" runat="server" visible="false">
            <div class="row">
                <div class="col-lg-12">
                    <div class="col-md-8">
                        <h1>Current Address</h1>
                        <asp:DetailsView ID="DetailsViewAddress" runat="server" AutoGenerateRows="False" DataKeyNames="add_id" DataSourceID="SqlDataSourceAddress"
                            CssClass="table table-striped table-bordered table-hover dataTables-example" Height="16px" Width="700px" OnItemCreated="DetailsViewAddress_ItemCreated">
                            <Fields>
                                <asp:BoundField DataField="person_id" HeaderText="person_id" SortExpression="person_id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                                    <ItemStyle CssClass="hidden"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="add_id" HeaderText="add_id" SortExpression="add_id" InsertVisible="False" ReadOnly="True" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                                    <ItemStyle CssClass="hidden"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Address line 1 <span class="required">*</span>
                                    </HeaderTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbAddressLine1" runat="server" Text='<%# Bind("address_line_1") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtbAddressLine1" ErrorMessage="Address Required" ForeColor="Red" ControlToValidate="tbAddressLine1"
                                            runat="server" Dispaly="Dynamic" />
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbAddressLine1" runat="server" Text='<%# Bind("address_line_1") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtbAddressLine1" ErrorMessage="Address Required" ForeColor="Red" ControlToValidate="tbAddressLine1"
                                            runat="server" Dispaly="Dynamic" />
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("address_line_1") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="address_line_2" HeaderText="Address line 2" SortExpression="address_line_2" />
                                <asp:BoundField DataField="address_line_3" HeaderText="Address line 3" SortExpression="address_line_3" />
                                <%--<asp:BoundField DataField="zip_postal_code" HeaderText="zip-postal code" SortExpression="zip_postal_code" />--%>
                                <%-- <asp:BoundField DataField="start_date" HeaderText="Address start date" SortExpression="Address_start_date" />--%>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        LGA <span class="required">*</span>
                                    </HeaderTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlCity" runat="server" DataValueField="lga" DataTextField="lga" Text='<%# Bind("lga_city") %>' DataSourceID="SqlDataSourceCityDropDown"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlCity" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlCity"
                                            runat="server" Dispaly="Dynamic" />
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:DropDownList ID="ddlCity" runat="server" DataValueField="lga" DataTextField="lga" Text='<%# Bind("lga_city") %>' DataSourceID="SqlDataSourceCityDropDown"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlCity" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlCity"
                                            runat="server" Dispaly="Dynamic" />
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("lga_city") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        State <span class="required">*</span>
                                    </HeaderTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlState" runat="server" DataValueField="state" DataTextField="state" Text='<%# Bind("state") %>' DataSourceID="SqlDataSourceStateDropDown"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvtbState" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlState"
                                            runat="server" Dispaly="Dynamic" />
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:DropDownList ID="ddlState" runat="server" DataValueField="state" DataTextField="state" Text='<%# Bind("state") %>' DataSourceID="SqlDataSourceStateDropDown"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvtbState" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlState"
                                            runat="server" Dispaly="Dynamic" />
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("state") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Country <span class="required">*</span>
                                    </HeaderTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlAddresCountry" runat="server" DataValueField="country" DataTextField="country" Text='<%# Bind("country") %>' DataSourceID="SqlDataSourceCountryDropDown"></asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="rfvddlAddressCountry" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlAddresCountry"
                                            runat="server" Dispaly="Dynamic" />
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:DropDownList ID="ddlAddresCountry" runat="server" DataValueField="country" DataTextField="country" Text='<%# Bind("country") %>' DataSourceID="SqlDataSourceCountryDropDown"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlAddressCountry" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlAddresCountry"
                                            runat="server" Dispaly="Dynamic" />
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblAddressCountry" runat="server" Text='<%# Bind("country") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ShowHeader="False">
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="lnkBtnAddUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                                        &nbsp;<asp:LinkButton ID="lnkBtnAddUpdateCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:LinkButton ID="lnkBtnAddInsert" runat="server" CausesValidation="True" CommandName="Insert" Text="Save"></asp:LinkButton>
                                        &nbsp;<asp:LinkButton ID="lnkBtnAddInsertCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" OnClick="lnkBtnAddInsertCancel_Click"></asp:LinkButton>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkBtnAddEdit" runat="server" CausesValidation="False" CommandName="Edit" Text=" Edit"></asp:LinkButton>
                                        &nbsp;<asp:LinkButton ID="lnkBtnAddNew" runat="server" CausesValidation="False" CommandName="New" Text="New"></asp:LinkButton>
                                        &nbsp;<asp:LinkButton ID="lnkBtnAddDelete" runat="server" CausesValidation="False" CommandName="Delete" Text=" Delete" OnClientClick="return confirm('Are you sure you want to delete ?');"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="btn btn-primary" Width="80px" />
                                    <HeaderStyle Width="80px" />
                                </asp:TemplateField>
                                <%-- <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowInsertButton="True" />--%>
                            </Fields>
                            <EmptyDataTemplate>
                                <h2>No Address Found </h2>
                                <asp:Button ID="InsertAddress" runat="server" CommandName="New" Text="Add Address" CssClass="btn btn-sm btn-primary" />
                            </EmptyDataTemplate>
                        </asp:DetailsView>
                    </div>
                    <div class="col-md-4">
                        <div id="divEndAddress" runat="server" visible="false" style="width: 200px">
                            <div class="form-group">
                                <div class="editor-label">
                                    <label>End Current Address when changed</label>
                                </div>
                                <div class="table table-striped table-bordered table-hover dataTables-example">
                                    <asp:TextBox ID="tbEndDate" runat="server" rel="date" type="text" CssClass="datepick" placeholder="dd/mm/yyyy"></asp:TextBox>
                                </div>
                            </div>
                            <div>
                                <asp:Button ID="BtnEndDate" runat="server" Text="Submit" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" OnClick="BtnEndDate_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divGridviewAddress" runat="server" class="col-lg-12">
                <h1>Previous Address(es)</h1>

                <asp:GridView ID="GridViewAddress" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example" OnRowDeleting="GridViewAddress_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="add_id" HeaderText="AddID" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                            <HeaderStyle CssClass="hidden"></HeaderStyle>
                            <ItemStyle CssClass="hidden"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="address_line_1" HeaderText="Address Line1" />
                        <asp:BoundField DataField="address_line_2" HeaderText="Address Line 2" />
                        <asp:BoundField DataField="address_line_3" HeaderText="Address Line 3" />
                        <asp:BoundField DataField="lga_city" HeaderText="LGA" />
                        <asp:BoundField DataField="state" HeaderText="State" />
                        <asp:BoundField DataField="country" HeaderText="Country" />
                        <%-- <asp:BoundField DataField="start_date" HeaderText="Start Date" />--%>
                        <asp:BoundField DataField="end_date" HeaderText="End Date" />
                        <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete" CssClass="btn blue-bg" OnClientClick="return confirm('Are you sure you want to delete ?');" Text="Delete"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <div>
                    <h4>
                        <asp:Label ID="lblZeroAddress" runat="server" Text=""></asp:Label></h4>
                </div>
            </div>
        </div>
        <div id="AppDropDown">
            <asp:SqlDataSource ID="SqlDataSourceAcademicDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
                SelectCommand="SELECT DISTINCT [acad_year], case status when 'Active' then '1' else '0' end as 'default'FROM [dbo].[ms_acad_year] order by 'default' DESC ;"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSourceTermDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
                SelectCommand="SELECT DISTINCT term_name from ms_acad_year_term"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSourceFormDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
                SelectCommand="sp_ms_rep_form_dropdown"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSourceAppStatusDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
                SelectCommand="SELECT status_name FROM ms_status Where category = 'Applications'  and status_name != 'All' "></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSourcAppBoarderTypeDD" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
                SelectCommand="sp_ms_boarder_type_drpdwn"></asp:SqlDataSource>
        </div>
        <div id="divApplication" runat="server" visible="false">
            <h1>Most Recent Application</h1>
            <div class="row">
                <div class="col-lg-12">
                    <div class="float-e-margins">
                        <div class="col-lg-6">
                            <asp:DetailsView ID="DetailsViewApplication" runat="server" Height="16px" AutoGenerateRows="False" DataKeyNames="app_id" DataSourceID="SqlDataSourceApplication"
                                CssClass="table table-striped table-bordered table-hover dataTables-example" OnDataBound="DetailsViewApplication_DataBound">

                                <Fields>
                                    <asp:BoundField DataField="person_id" HeaderText="person_id" InsertVisible="False" ReadOnly="True" SortExpression="person_id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                        <HeaderStyle CssClass="hidden"></HeaderStyle>
                                        <ItemStyle CssClass="hidden"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="app_id" HeaderText="app_id" InsertVisible="False" ReadOnly="True" SortExpression="app_id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                        <HeaderStyle CssClass="hidden"></HeaderStyle>
                                        <ItemStyle CssClass="hidden"></ItemStyle>
                                    </asp:BoundField>

                                    <asp:TemplateField HeaderText="Academic Year" SortExpression="acad_year">
                                        <HeaderTemplate>
                                            Academic Year <span class="required">*</span>
                                        </HeaderTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlAppAcademicYearEdit" runat="server" DataValueField="acad_year" DataTextField="acad_year" Text='<%# Bind("acad_year") %>'
                                                DataSourceID="SqlDataSourceAcademicDropDown"
                                                AppendDataBoundItems="true" OnSelectedIndexChanged="ddlAppAcademicYear_SelectedIndexChangedEdit" AutoPostBack="true">
                                                <asp:ListItem Text="Please Select Year" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlAppAcademicYear" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlAppAcademicYearEdit"
                                                runat="server" Dispaly="Dynamic" />
                                        </EditItemTemplate>
                                        <InsertItemTemplate>
                                            <asp:DropDownList ID="ddlAppAcademicYear" runat="server" DataValueField="acad_year" DataTextField="acad_year" Text='<%# Bind("acad_year") %>'
                                                DataSourceID="SqlDataSourceAcademicDropDown"
                                                AppendDataBoundItems="true" OnSelectedIndexChanged="ddlAppAcademicYear_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Text="Please Select Year" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlAppAcademicYear" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlAppAcademicYear"
                                                runat="server" Dispaly="Dynamic" />
                                        </InsertItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAppAcademicYear" runat="server" Text='<%# Bind("acad_year") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Term" SortExpression="term_name">
                                        <HeaderTemplate>
                                            Term <span class="required">*</span>
                                        </HeaderTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlAppTermEdit" runat="server"
                                                AppendDataBoundItems="true">
                                                <asp:ListItem Text="Please Select Term" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlAppTerm" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlAppTermEdit"
                                                runat="server" Dispaly="Dynamic" />
                                        </EditItemTemplate>
                                        <InsertItemTemplate>
                                            <asp:DropDownList ID="ddlAppTermInsert" runat="server" AppendDataBoundItems="true">
                                                <asp:ListItem Text="Please Select Term" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlAppTerm" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlAppTermInsert"
                                                runat="server" Dispaly="Dynamic" />
                                        </InsertItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAppTerm" runat="server" Text='<%# Bind("term_name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Class" SortExpression="form_name">
                                        <HeaderTemplate>
                                            Class <span class="required">*</span>
                                        </HeaderTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlAppForm" runat="server" DataValueField="form_name" DataTextField="form_name" Text='<%# Bind("form_name") %>' DataSourceID="SqlDataSourceFormDropDown"
                                                SelectedValue='<%# Bind("form_name") %>' AppendDataBoundItems="true">
                                                <asp:ListItem Text="Select Class" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlAppForm" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlAppForm"
                                                runat="server" Dispaly="Dynamic" />
                                        </EditItemTemplate>
                                        <InsertItemTemplate>
                                            <asp:DropDownList ID="ddlAppForm" runat="server" DataValueField="form_name" DataTextField="form_name" Text='<%# Bind("form_name") %>' DataSourceID="SqlDataSourceFormDropDown"
                                                SelectedValue='<%# Bind("form_name") %>' AppendDataBoundItems="true">
                                                <asp:ListItem Text="Select Class" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlAppForm" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlAppForm"
                                                runat="server" Dispaly="Dynamic" />
                                        </InsertItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAppForm" runat="server" Text='<%# Bind("form_name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Application Date" SortExpression="app_date">
                                        <HeaderTemplate>
                                            Application Date <span class="required">*</span>
                                        </HeaderTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="tbAppDate" runat="server" rel="date" type="text" CssClass="datepick input-group date" Text='<%# Bind("app_date") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtbAppDate" ErrorMessage="Date Required" ForeColor="Red" ControlToValidate="tbAppDate"
                                                runat="server" Dispaly="Dynamic" />
                                        </EditItemTemplate>
                                        <InsertItemTemplate>
                                            <asp:TextBox ID="tbAppDate" runat="server" CssClass="datepick" Text='<%# Bind("app_date") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtbAppDate" ErrorMessage="Date Required" ForeColor="Red" ControlToValidate="tbAppDate"
                                                runat="server" Dispaly="Dynamic" />
                                        </InsertItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAppDate" runat="server" Text='<%# Bind("app_date") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Application Status" SortExpression="application_status">
                                        <HeaderTemplate>
                                            Admission Status <span class="required">*</span>
                                        </HeaderTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlAppStatus" runat="server" DataValueField="status_name" DataTextField="status_name" SelectedValue='<%# Bind("application_status") %>' DataSourceID="SqlDataSourceAppStatusDropDown"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlAppStatus" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlAppStatus"
                                                runat="server" Dispaly="Dynamic" />
                                        </EditItemTemplate>
                                        <InsertItemTemplate>
                                            <asp:DropDownList ID="ddlAppStatus" runat="server" DataValueField="status_name" DataTextField="status_name" Text='<%# Bind("application_status") %>' DataSourceID="SqlDataSourceAppStatusDropDown"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlAppStatus" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlAppStatus"
                                                runat="server" Dispaly="Dynamic" />
                                        </InsertItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("application_status") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ShowHeader="False">
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                                            &nbsp;<asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                        </EditItemTemplate>
                                        <InsertItemTemplate>
                                            <asp:LinkButton ID="LinkButtonInsert" runat="server" CausesValidation="True" CommandName="Insert" Text="Save"></asp:LinkButton>
                                            &nbsp;<asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                        </InsertItemTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="False" CommandName="Edit" Text=" Edit"></asp:LinkButton>
                                            &nbsp;<asp:LinkButton ID="LinkButtonNew" runat="server" CausesValidation="False" CommandName="New" Text="New"></asp:LinkButton>
                                            &nbsp;<asp:LinkButton ID="LinkButtonDelete" runat="server" CausesValidation="False" OnClientClick="return confirm('Are you sure you want to delete ?');" CommandName="Delete" Text=" Delete"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ControlStyle CssClass="btn btn-primary" Width="80px" />
                                        <HeaderStyle Width="80px" />
                                    </asp:TemplateField>

                                </Fields>
                                <EmptyDataTemplate>
                                    <h2>No Admission Found </h2>
                                    <asp:Button ID="InsertApplication" runat="server" CommandName="New" Text="Add New Application " CssClass="btn btn-sm btn-primary" />
                                </EmptyDataTemplate>
                            </asp:DetailsView>
                        </div>
                        <div id="divAppRegistration" runat="server" class="col-lg-2" visible="false">
                            <label>Choose Arm and Register</label>
                            <asp:DropDownList ID="ddlCN" runat="server" AppendDataBoundItems="true" ValidationGroup="ddlCNGroup" CssClass="form-control">
                                <asp:ListItem Text="Select Arm" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlCN" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlCN" ValidationGroup="ddlCNGroup"
                                runat="server" Dispaly="Dynamic" /><br />
                            <label>Boarder Type</label>
                            <asp:DropDownList ID="ddlAppRegBoarderType" runat="server" DataValueField="boarder" DataTextField="boarder" DataSourceID="SqlDataSourcAppBoarderTypeDD"
                                AppendDataBoundItems="true" ValidationGroup="ddlCNGroup" CssClass="form-control">
                                <asp:ListItem Text="Not Boarder" Value="" />
                            </asp:DropDownList><br />
                            <asp:Button ID="btnApp2Reg" runat="server" Text="Register Now" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" ValidationGroup="ddlCNGroup" OnClick="btnApp2Reg_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <h1>Previous Application(s)</h1>
            <asp:GridView ID="GridViewApplication" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example">
                <Columns>
                    <asp:BoundField DataField="acad_year" HeaderText="Academic Year " />
                    <asp:BoundField DataField="term_name" HeaderText="Term " />
                    <asp:BoundField DataField="form_name" HeaderText="Class" />
                    <asp:BoundField DataField="app_date" HeaderText="Application Date" />
                    <asp:BoundField DataField="application_status" HeaderText="Application Status" />
                </Columns>
            </asp:GridView>
            <div>
                <h4>
                    <asp:Label ID="lblZeroApplications" runat="server" Text=""></asp:Label></h4>
            </div>
        </div>

        <div id="RegDropDown">
            <asp:SqlDataSource ID="SqlDataSourceClassDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
                SelectCommand="SELECT DISTINCT class_name FROM ms_classes"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSourceRegStatusDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
                SelectCommand="SELECT status_name FROM ms_status Where category = 'Registrations'"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSourceActiveAcademicYear" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
                SelectCommand="sp_ms_rep_active_acad_year_dropdown"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSourceBoarderTypedrpdwn" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
                SelectCommand="sp_ms_boarder_type_drpdwn"></asp:SqlDataSource>
        </div>

        <div id="divRegistration" runat="server" visible="false">
            <div class="row">
                <div class="col-lg-12">
                    <div class="float-e-margins">
                        <div class="col-lg-6">
                            <h1>
                                <asp:Label runat="server" ID="lblRegistrationSummaryCurrent" Text="Current Registration"></asp:Label><%--<h1>Current Registration</h1>--%></h1>
                            <asp:DetailsView ID="DetailsViewRegistration" runat="server" Height="16px" AutoGenerateRows="False" DataKeyNames="reg_id"
                                DataSourceID="SqlDataSourceRegistration"
                                CssClass="table table-striped table-bordered table-hover dataTables-example">
                                <Fields>
                                    <asp:BoundField DataField="person_id" HeaderText="person_id" InsertVisible="False" ReadOnly="True" SortExpression="person_id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                        <HeaderStyle CssClass="hidden"></HeaderStyle>
                                        <ItemStyle CssClass="hidden"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="reg_id" HeaderText="Reg Id" InsertVisible="False" ReadOnly="True" SortExpression="reg_id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                        <HeaderStyle CssClass="hidden"></HeaderStyle>
                                        <ItemStyle CssClass="hidden"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Academic Year" SortExpression="acad_year">
                                        <HeaderTemplate>
                                            Academic Year<span class="required">*</span>
                                        </HeaderTemplate>
                                        <%--   <EditItemTemplate>
                      <asp:DropDownList ID="ddlRegAcademicYear" runat="server" DataValueField="acad_year" DataTextField="acad_year" Text='<%# Bind("acad_year") %>' DataSourceID="SqlDataSourceActiveAcademicYear"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvddlRegAcademicYear" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlRegAcademicYear"
                            runat="server" Dispaly="Dynamic" />
                  </EditItemTemplate>--%>
                                        <InsertItemTemplate>
                                            <asp:DropDownList ID="ddlRegAcademicYear" runat="server" DataValueField="acad_year" DataTextField="acad_year" Text='<%# Bind("acad_year") %>'
                                                DataSourceID="SqlDataSourceActiveAcademicYear" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlRegAcademicYear_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Text="Please Select Year" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlRegAcademicYear" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlRegAcademicYear"
                                                runat="server" Dispaly="Dynamic" />
                                        </InsertItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRegAcademicYear" runat="server" Text='<%# Bind("acad_year") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Term" SortExpression="term_name">
                                        <HeaderTemplate>
                                            Term <span class="required">*</span>
                                        </HeaderTemplate>
                                        <%-- <EditItemTemplate>
                        <asp:DropDownList ID="ddlRegTerm" runat="server" DataValueField="term_name" DataTextField="term_name" Text='<%# Bind("term_name") %>' DataSourceID="SqlDataSourceTermDropDown"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvddlRegTerm" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlRegTerm"
                            runat="server" Dispaly="Dynamic" />
                    </EditItemTemplate>--%>
                                        <InsertItemTemplate>
                                            <asp:DropDownList ID="ddlRegTerm" runat="server" AppendDataBoundItems="true">
                                                <asp:ListItem Text="Select Term" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlRegTerm" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlRegTerm"
                                                runat="server" Dispaly="Dynamic" />
                                        </InsertItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRegTerm" runat="server" Text='<%# Bind("term_name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Class" SortExpression="form_name">
                                        <HeaderTemplate>
                                            Class <span class="required">*</span>
                                        </HeaderTemplate>
                                        <%-- <EditItemTemplate>
                        <asp:DropDownList ID="ddlRegForm" runat="server" DataValueField="form_name" DataTextField="form_name" Text='<%# Bind("form_name") %>' DataSourceID="SqlDataSourceFormDropDown"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvddlRegForm" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlRegForm"
                            runat="server" Dispaly="Dynamic" />
                    </EditItemTemplate>--%>
                                        <InsertItemTemplate>
                                            <asp:DropDownList ID="ddlRegForm" runat="server" DataValueField="form_name" DataTextField="form_name" Text='<%# Bind("form_name") %>' DataSourceID="SqlDataSourceFormDropDown"
                                                SelectedValue='<%# Bind("form_name") %>' AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlRegForm_SelectedIndexChanged">
                                                <asp:ListItem Text="Select Class" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlRegForm" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlRegForm"
                                                runat="server" Dispaly="Dynamic" />
                                        </InsertItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRegForm" runat="server" Text='<%# Bind("form_name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Arm" SortExpression="class_name">
                                        <HeaderTemplate>
                                            Arm <span class="required">*</span>
                                        </HeaderTemplate>
                                        <%--    <EditItemTemplate>
                        <asp:DropDownList ID="ddlRegClass" runat="server" DataValueField="class_name" DataTextField="class_name" Text='<%# Bind("class_name") %>' DataSourceID="SqlDataSourceClassDropDown"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvddlRegClass" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlRegClass"
                            runat="server" Dispaly="Dynamic" />
                    </EditItemTemplate>--%>
                                        <InsertItemTemplate>
                                            <asp:DropDownList ID="ddlRegClass" runat="server" AppendDataBoundItems="true">
                                                <asp:ListItem Text="Select Arm" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlRegClass" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlRegClass"
                                                runat="server" Dispaly="Dynamic" />
                                        </InsertItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="ddlRegClass" runat="server" Text='<%# Bind("class_name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Registration Date" SortExpression="reg_date">
                                        <HeaderTemplate>
                                            Registration Date <span class="required">*</span>
                                        </HeaderTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="tbRegDate" runat="server" rel="date" type="text" CssClass="datepick input-group date" Text='<%# Bind("reg_date") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtbRegDate" ErrorMessage="Date Required" ForeColor="Red" ControlToValidate="tbRegDate"
                                                runat="server" Dispaly="Dynamic" />
                                        </EditItemTemplate>
                                        <InsertItemTemplate>
                                            <asp:TextBox ID="tbRegDate" runat="server" CssClass="datepick" Text='<%# Bind("reg_date") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtbRegDate" ErrorMessage="Date Required" ForeColor="Red" ControlToValidate="tbRegDate"
                                                runat="server" Dispaly="Dynamic" />
                                        </InsertItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRegDate" runat="server" Text='<%# Bind("reg_date") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <HeaderTemplate>
                                            Status <span class="required">*</span>
                                        </HeaderTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlRegStatus" runat="server" DataValueField="status_name" DataTextField="status_name" Text='<%# Bind("status") %>' DataSourceID="SqlDataSourceRegStatusDropDown"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlRegStatus" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlRegStatus"
                                                runat="server" Dispaly="Dynamic" />
                                        </EditItemTemplate>
                                        <InsertItemTemplate>
                                            <asp:DropDownList ID="ddlRegStatus" runat="server" DataValueField="status_name" DataTextField="status_name" Text='<%# Bind("status") %>' DataSourceID="SqlDataSourceRegStatusDropDown"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlRegStatus" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlRegStatus"
                                                runat="server" Dispaly="Dynamic" />
                                        </InsertItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRegStatus" runat="server" Text='<%# Bind("status") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Boarder type">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlRegBoarderType" runat="server" DataValueField="boarder" DataTextField="boarder" Text='<%# Bind("type_description") %>' DataSourceID="SqlDataSourceBoarderTypedrpdwn"
                                                AppendDataBoundItems="true">
                                                <asp:ListItem Text="Not Boarder" Value="" />
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <InsertItemTemplate>
                                            <asp:DropDownList ID="ddlRegBoarderType" runat="server" DataValueField="boarder" DataTextField="boarder" Text='<%# Bind("type_description") %>' DataSourceID="SqlDataSourceBoarderTypedrpdwn"
                                                AppendDataBoundItems="true">
                                                <asp:ListItem Text="Not Boarder" Value="" />
                                            </asp:DropDownList>
                                        </InsertItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRegBoarderType" runat="server" Text='<%# Bind("type_description") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="False">
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                                            &nbsp;<asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                        </EditItemTemplate>
                                        <InsertItemTemplate>
                                            <asp:LinkButton ID="LinkButtonInsert" runat="server" CausesValidation="True" CommandName="Insert" Text="Save"></asp:LinkButton>
                                            &nbsp;<asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" OnClick="LinkButtonCancel_Click"></asp:LinkButton>
                                        </InsertItemTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="False" CommandName="Edit" Text=" Edit"></asp:LinkButton>
                                            &nbsp;<asp:LinkButton ID="LinkButtonNew" runat="server" CausesValidation="False" CommandName="New" Text="New" OnClick="LinkButtonNew_Click"></asp:LinkButton>
                                            &nbsp;<asp:LinkButton ID="LinkButtonDelete" runat="server" CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete ?');" Text=" Delete"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ControlStyle CssClass="btn btn-primary" Width="80px" />
                                        <HeaderStyle Width="80px" />
                                    </asp:TemplateField>

                                </Fields>
                                <EmptyDataTemplate>
                                    <h2>No Registration Found </h2>
                                    <asp:Button ID="InsertRegistration" runat="server" CommandName="New" Text="Add New Registration" CssClass="btn btn-sm btn-primary" OnClick="InsertRegistration_Click" />
                                </EmptyDataTemplate>
                            </asp:DetailsView>
                        </div>
                        <div class="col-lg-6">
                            <h1>
                                <asp:Label runat="server" ID="lblRegistrationSummaryPrevious" Text="Current Registration" Visible="false"></asp:Label></h1>
                            <asp:DetailsView ID="dvStaticRegistration" runat="server" AutoGenerateRows="False" DataKeyNames="reg_id"
                                DataSourceID="SqlDataSourceRegistration" Visible="false"
                                CssClass="table table-striped table-bordered table-hover dataTables-example">
                                <Fields>
                                    <asp:BoundField DataField="acad_year" HeaderText="Academic Year" />
                                    <asp:BoundField DataField="term_name" HeaderText="Term" />
                                    <asp:BoundField DataField="form_name" HeaderText="Class" />
                                    <asp:BoundField DataField="class_name" HeaderText="Arm" />
                                    <asp:BoundField DataField="reg_date" HeaderText="Registration Date" />
                                    <asp:BoundField DataField="status" HeaderText="Status" />
                                    <asp:BoundField DataField="type_description" HeaderText="Boarder Type" />
                                </Fields>
                            </asp:DetailsView>
                        </div>
                    </div>
                </div>
            </div>

            <h1>Previous Registration(s)</h1>
            <div>
                <h4>
                    <asp:Label ID="lblZeroRegistrations" runat="server" Text=""></asp:Label></h4>
            </div>
            <asp:GridView ID="GridViewRegistration" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example" OnRowEditing="GridViewRegistration_RowEditing"
                OnRowCancelingEdit="GridViewRegistration_RowCancelingEdit" OnRowUpdating="GridViewRegistration_RowUpdating" OnRowDeleting="GridViewRegistration_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="reg_id" HeaderText="Reg ID" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                        <HeaderStyle CssClass="hidden"></HeaderStyle>
                        <ItemStyle CssClass="hidden"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Academic Year">
                        <ItemTemplate>
                            <asp:Label ID="lblRegEditAcademicYear" runat="server" Text='<%# Bind("acad_year") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Term">
                        <ItemTemplate>
                            <asp:Label ID="lblRegEditTerm" runat="server" Text='<%# Bind("term_name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Class">
                        <ItemTemplate>
                            <asp:Label ID="lblRegEditClass" runat="server" Text='<%# Bind("form_name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Arm">
                        <ItemTemplate>
                            <asp:Label ID="lblRegEditClassName" runat="server" Text='<%# Bind("class_name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Registration Status">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlRegEditStatus" runat="server" Text='<%# Bind("status") %>' CssClass="input-group" DataValueField="status_name" DataTextField="status_name"
                                DataSourceID="SqlDataSourceRegStatusDropDown" SelectedValue='<%# Bind("status") %>' AppendDataBoundItems="true">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvtbRegEditStatus" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlRegEditStatus"
                                runat="server" Dispaly="Dynamic" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblRegEditStatus" runat="server" Text='<%# Bind("status") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Registration Date">
                        <EditItemTemplate>
                            <asp:TextBox ID="tbRegEditDate" runat="server" Text='<%# Bind("reg_date") %>' CssClass="datepick"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtbRegEditDate" ErrorMessage="*" ForeColor="Red" ControlToValidate="tbRegEditDate"
                                runat="server" Dispaly="Dynamic" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblRegEditDate" runat="server" Text='<%# Bind("reg_date") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Boarder Type">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlBoarderType" runat="server" Text='<%# Bind("type_description") %>' CssClass="input-group" DataValueField="boarder" DataTextField="boarder"
                                DataSourceID="SqlDataSourceBoarderTypedrpdwn" SelectedValue='<%# Bind("type_description") %>' AppendDataBoundItems="true">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblddlBoarderType" runat="server" Text='<%# Bind("type_description") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--  <asp:BoundField DataField="type_description" HeaderText="Boarder Type" NullDisplayText="N/A" />
                    <asp:BoundField DataField="acad_year" HeaderText="Academic Year " />
                <asp:BoundField DataField="term_name" HeaderText="Term " />
                <asp:BoundField DataField="form_name" HeaderText="Class" />
                <asp:BoundField DataField="class_name" HeaderText="Class Name " />
                <asp:BoundField DataField="status" HeaderText="Registration Status" />
                <asp:BoundField DataField="reg_date" HeaderText="Registration Date" />--%>
                    <asp:TemplateField ShowHeader="false" HeaderText="Edit">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CssClass="btn blue-bg" CommandName="Edit" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="lnkUpdate" runat="server" Text="Update" CssClass="btn blue-bg" CommandName="Update" />
                            <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel" CssClass="btn blue-bg" CommandName="Cancel" CausesValidation="False" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" CssClass="btn blue-bg" OnClientClick="return confirm('Are you sure you want to delete ?');" Text="Delete"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>
        <div id="divStatement" runat="server" visible="false">
            <h1>Account Summary</h1>
            <asp:Label ID="lblClickRecords" runat="server" Text=""></asp:Label>


            <asp:GridView ID="GridViewStatement" runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="reg_id"
                CssClass="table table-striped table-bordered table-hover dataTables-example" OnRowDataBound="GridViewStatement_RowDataBound"
                OnSelectedIndexChanging="GridViewStatement_SelectedIndexChanging">

                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="person_id,reg_id" DataNavigateUrlFormatString="profile.aspx?Personid={0}&amp;Regid={1}&amp;action=soa"
                        DataTextField="acad_year" HeaderText="Academic Year" ControlStyle-CssClass="hyperlink" />
                    <asp:BoundField DataField="term_name" HeaderText="Term" />
                    <asp:BoundField DataField="form_name" HeaderText="Class" />
                    <asp:BoundField DataField="Fees" HeaderText="Fee" />
                    <asp:BoundField DataField="Fees_Total_Amount_Paid" HeaderText="Total Fee Paid" />
                    <asp:TemplateField HeaderText="Total Fee Outstanding" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                        <EditItemTemplate>
                            <asp:TextBox ID="tbBalance" runat="server" Text='<%# Bind("Fee_Balance") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblBalance" runat="server" Text='<%# Bind("Fee_Balance") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <div>
                                <asp:Label ID="lblTotalBalance" runat="server" />
                            </div>
                        </FooterTemplate>
                        <HeaderStyle CssClass="hidden"></HeaderStyle>
                        <ItemStyle CssClass="hidden"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="SBalance" HeaderText="Fee Balance" />
                    <asp:BoundField DataField="SBoarder_fees" HeaderText="Boarder Fees" NullDisplayText="N/A" />
                    <asp:TemplateField HeaderText="Total BorderFee Outstanding" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                        <ItemTemplate>
                            <asp:Label ID="lblBorderBalance" runat="server" Text='<%# Bind("Boarder_Balance") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <div>
                                <asp:Label ID="lblTotalBoarderBalance" runat="server" />
                            </div>
                        </FooterTemplate>
                        <HeaderStyle CssClass="hidden"></HeaderStyle>
                        <ItemStyle CssClass="hidden"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Balance Outstanding" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                        <EditItemTemplate>
                            <asp:TextBox ID="tbBBalance" runat="server" Text='<%# Bind("Total_Balance_All_Fees") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblBBalance" runat="server" Text='<%# Bind("Total_Balance_All_Fees") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <div>
                                <asp:Label ID="lblTotalBBalance" runat="server" />
                            </div>
                        </FooterTemplate>
                        <HeaderStyle CssClass="hidden"></HeaderStyle>
                        <ItemStyle CssClass="hidden"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Boarder_Fees_Total_Amount_Paid" HeaderText="Boarder Total Fee Paid" />
                    <asp:BoundField DataField="SBoarder_Balance" HeaderText="Boarder Balance" />
                    <asp:BoundField DataField="STotal_Balance_All_fees" HeaderText="Total Balance" />
                    <asp:BoundField DataField="reg_id" InsertVisible="False" ReadOnly="True" SortExpression="reg_id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                        <HeaderStyle CssClass="hidden"></HeaderStyle>
                        <ItemStyle CssClass="hidden"></ItemStyle>
                        <FooterStyle CssClass="hidden" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Fee Payment">
                        <ItemTemplate>
                            <asp:Button ID="btnPayFee" runat="server" Text="Pay Tuition Fee" CommandArgument="Button1" OnClick="btnPayFee_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Sno" InsertVisible="False" ReadOnly="True" SortExpression="Sno" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                        <HeaderStyle CssClass="hidden"></HeaderStyle>
                        <ItemStyle CssClass="hidden"></ItemStyle>
                        <FooterStyle CssClass="hidden" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="boarder_count" InsertVisible="False" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                        <HeaderStyle CssClass="hidden"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblBoarderCount" runat="server" Text='<%# Bind("boarder_count") %>' CssClass="hidden"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle CssClass="hidden" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Boarder Payment">
                        <ItemTemplate>
                            <asp:Button ID="btnBoarderPayFee" runat="server" Text="Pay Boarder Fee" OnClick="btnBoarderPayFee_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#f3f3f4" ForeColor="Black" HorizontalAlign="Left" />
            </asp:GridView>

            <div id="print">
                <h1>Account Details</h1>
                <asp:Button ID="btnPrintStatement" runat="server" Text="Print Account Details" Visible="false" CssClass="col-lg-offset-9 btn btn-primary fa fa-print"
                    OnClick="btnPrintStatement_Click" OnClientClick="SetTarget();" />
                <div class="row" id="divAccountDetails" runat="server" visible="false">
                    <div class="col-lg-12">
                        <div class="col-xs-12 col-sm-6 col-md-8">
                            <strong>Academic Year:</strong><asp:Label ID="lblAcademicYear" runat="server" Text="" CssClass="p-xs"></asp:Label>
                            <strong>Term:</strong><asp:Label ID="lblTermName" runat="server" Text="" CssClass="p-xs"></asp:Label>
                            <strong>Class:</strong><asp:Label ID="lblFormName" runat="server" Text="" CssClass="p-xs"></asp:Label>
                        </div>
                        <div class="space-25"></div>
                        <div class="clearfix"></div>
                        <asp:GridView ID="GridViewStatementOfAccount" runat="server" AutoGenerateColumns="False" ShowFooter="True" Width="980px"
                            CssClass="table table-striped table-bordered table-hover dataTables-example table-fixed "
                            OnRowDataBound="GridViewStatementOfAccount_RowDataBound" Style="table-layout: auto;">
                            <Columns>
                                <asp:BoundField DataField="payment_method" HeaderText="Payment Method" HeaderStyle-Wrap="true" HeaderStyle-Width="10" ItemStyle-Wrap="true" />
                                <asp:BoundField DataField="bank_name" HeaderText="Bank Name" HeaderStyle-Wrap="true" HeaderStyle-Width="10" ItemStyle-Wrap="true" />
                                <asp:BoundField DataField="Receipt No" HeaderText="Receipt No" HeaderStyle-Wrap="true" HeaderStyle-Width="10" ItemStyle-Wrap="true" />
                                <asp:BoundField DataField="Reference No / Invoice No" HeaderText="Ref/Invoice No" HeaderStyle-Wrap="true" HeaderStyle-Width="10" ItemStyle-Wrap="true" />
                                <asp:BoundField DataField="Payment_Received_Date" HeaderText="Date" />
                                <asp:BoundField DataField="Fee_type" HeaderText="Fee Type" />
                                <asp:TemplateField HeaderText="Amount Paid">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmountPaid" runat="server" Text='<%# Bind("Amount_Paid") %>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lblFeeA" runat="server" CssClass="pull-right"></asp:Label>
                                    </HeaderTemplate>

                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalPaid" runat="server" CssClass="pull-right" /><br />
                                        <hr />
                                        <asp:Label ID="lblBalanceOwed" runat="server" CssClass="pull-right" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="payment_id" HeaderText="payment_id" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                                    <ItemStyle CssClass="hidden"></ItemStyle>
                                    <FooterStyle CssClass="hidden" />
                                </asp:BoundField>
                                <asp:BoundField DataField="reg_id" HeaderText="reg_id" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                                    <ItemStyle CssClass="hidden"></ItemStyle>
                                    <FooterStyle CssClass="hidden" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Receipt">
                                    <ItemTemplate>
                                        <asp:Button ID="btnReceipt" runat="server" Text="Receipt" OnClick="btnReceipt_Click" OnClientClick="SetTarget();" />
                                    </ItemTemplate>
                                    <FooterStyle CssClass="hidden" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <asp:Label ID="lblZeroRecords" runat="server" Text="" CssClass="font-bold"></asp:Label>
        </div>

        <div id="divAssessment" runat="server" visible="false" class="wrapper wrapper-content">
            <h1>Assessment Summary</h1>
            <asp:Label ID="lblClickAssemntsYear" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblZeroAssessments" runat="server" Text=""></asp:Label>
            <asp:GridView ID="GridViewAssesments" runat="server" AutoGenerateColumns="False" DataKeyNames="reg_id"
                CssClass="table table-striped table-bordered table-hover dataTables-example" OnRowDataBound="GridViewAssesments_RowDataBound">
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="person_id,reg_id" DataNavigateUrlFormatString="profile.aspx?Personid={0}&amp;Regid={1}&amp;action=ast"
                        DataTextField="acad_year" HeaderText="Academic Year" ControlStyle-CssClass="hyperlink" />
                    <%-- <asp:BoundField DataField="acad_year" HeaderText="Academic Year" />--%>
                    <asp:BoundField DataField="term_name" HeaderText="Term" />
                    <asp:BoundField DataField="Class" HeaderText="Class" />
                    <asp:BoundField DataField="Total No of Subjects taken" HeaderText="Total No of Subjects taken" />

                    <asp:BoundField DataField="Total Percentage" HeaderText="Total Percentage" />
                    <asp:TemplateField HeaderText="Report Card">
                        <ItemTemplate>
                            <asp:Button ID="btnReportCard" runat="server" Text="View" OnClick="btnReportCard_Click" OnClientClick="SetTarget();" />
                        </ItemTemplate>
                        <FooterStyle CssClass="hidden" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="Section2" HeaderText="Section" InsertVisible="False" ReadOnly="True" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                        <HeaderStyle CssClass="hidden"></HeaderStyle>
                        <ItemStyle CssClass="hidden"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="3rd Term Cumulative Report Card" Visible="false">
                        <ItemTemplate>
                            <asp:Button ID="btnCmRptCard" runat="server" Text="View" OnClick="btnCmRptCard_Click" OnClientClick="SetTarget();" />
                        </ItemTemplate>
                        <FooterStyle CssClass="hidden" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <div id="divAssessBreakdown" runat="server">
                <h1>Assessment Details</h1>
                <div id="divGrid" style="width: 100%;">
                    <strong>Academic Year:</strong><asp:Label ID="lblAcademicAssessment" runat="server" Text="" CssClass="p-xs"></asp:Label>
                    <strong>Term:</strong><asp:Label ID="lblTermAssessment" runat="server" Text="" CssClass="p-xs"></asp:Label>
                    <strong>Class:</strong><asp:Label ID="lblFormAssessment" runat="server" Text="" CssClass="p-xs"></asp:Label>

                    <asp:Label ID="lblZeroAssessmentsBreakDown" runat="server" Text=""></asp:Label>
                    <asp:GridView ID="GridViewAssessmentsBreakDown" runat="server" AutoGenerateColumns="False"
                        CssClass="table table-striped table-bordered table-hover dataTables-example ">
                        <Columns>
                            <asp:BoundField DataField="subject_name" HeaderText="Subject" HeaderStyle-Width="180px" />
                            <asp:BoundField DataField="assessment1" HeaderText="Assessment 1" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="120px" ItemStyle-Width="20px" />
                            <asp:BoundField DataField="assessment2" HeaderText="Assessment 2" HeaderStyle-Width="120px" />
                            <asp:BoundField DataField="assessment3" HeaderText="Assessment 3" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="120px" ItemStyle-Width="20px" />
                            <asp:BoundField DataField="assessment4" HeaderText="Assessment 4" HeaderStyle-Width="120px" />
                            <asp:BoundField DataField="assessment5" HeaderText="Exam" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="120px" ItemStyle-Width="20px" />
                            <asp:BoundField DataField="total_score" HeaderText="Total " HeaderStyle-Width="80px" />
                            <asp:BoundField DataField="grade" HeaderText="Grade" HeaderStyle-Width="80px" />
                            <asp:BoundField DataField="classification" HeaderText="Classification" HeaderStyle-Width="80px" />
                            <asp:BoundField DataField="remarks" HeaderText="Remarks" HeaderStyle-Width="80px" />

                        </Columns>
                    </asp:GridView>
                </div>
                <h1>Non-Academic Assessment Details</h1>
                <div class="col-md-12 row ">
                    <strong>Academic Year:</strong><asp:Label ID="lblAcademicAssessment2" runat="server" Text="" CssClass="p-xs"></asp:Label>
                    <strong>Term:</strong><asp:Label ID="lblTermAssessment2" runat="server" Text="" CssClass="p-xs"></asp:Label>
                    <strong>Class:</strong><asp:Label ID="lblFormAssessment2" runat="server" Text="" CssClass="p-xs"></asp:Label>
                    <div class="pull-right">

                        <label class="text-blue">4 =&nbsp;Excellent,</label>&nbsp;&nbsp;
                           
                            <label class="text-blue">3 =&nbsp;Good,</label>&nbsp;&nbsp;
                           
                            <label class="text-blue">2 =&nbsp;Average,</label>&nbsp;&nbsp;
                          
                            <label class="text-blue">1 =&nbsp;Poor</label>&nbsp;&nbsp;
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="float-e-margins">
                            <div class="col-lg-6">
                                <asp:DetailsView ID="DvAssessments2" runat="server" AutoGenerateRows="False" CssClass="table table-striped table-bordered table-hover dataTables-example">
                                    <Fields>
                                        <asp:BoundField DataField="attentiveness" HeaderText="Attentiveness" />
                                        <asp:BoundField DataField="honesty" HeaderText="Honesty" />
                                        <asp:BoundField DataField="neatness" HeaderText="Neatness" />
                                        <asp:BoundField DataField="politeness" HeaderText="Politeness" />
                                        <asp:BoundField DataField="punctuality" HeaderText="Punctuality" />
                                        <asp:BoundField DataField="relationship_with_others" HeaderText="Relationship with others" />
                                    </Fields>
                                </asp:DetailsView>
                            </div>
                            <div class="col-lg-6">
                                <asp:DetailsView ID="DvAssessments2_1" runat="server" AutoGenerateRows="False" CssClass="table table-striped table-bordered table-hover dataTables-example">
                                    <Fields>
                                        <asp:BoundField DataField="club_societies" HeaderText="Club Societies" />
                                        <asp:BoundField DataField="drawing_and_painting" HeaderText="Drawing and Painting" />
                                        <asp:BoundField DataField="hand_writing" HeaderText="Hand Writing" />
                                        <asp:BoundField DataField="hobbies" HeaderText="Hobbiess" />
                                        <asp:BoundField DataField="speech_fluency" HeaderText="Speech Fluency" />
                                        <asp:BoundField DataField="sports_and_games" HeaderText="Sports and Games" />
                                    </Fields>
                                </asp:DetailsView>
                            </div>
                        </div>
                    </div>
                </div>
                <label>Teacher Comment:</label><asp:Label ID="lblComment1" runat="server" Text="" CssClass="p-xs"></asp:Label><br />
                <label>Head Teacher Comment:</label><asp:Label ID="lblComment2" runat="server" Text="" CssClass="p-xs"></asp:Label>
            </div>
        </div>

        <div id="divAttendance" runat="server" visible="false">
            <h1>Attendance Summary</h1>
            <asp:Label ID="lblClickBelow" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblZeroAttendance" runat="server" Text=""></asp:Label>
            <asp:GridView ID="GridViewAttendance" runat="server" AutoGenerateColumns="False" DataKeyNames="reg_id"
                CssClass="table table-striped table-bordered table-hover dataTables-example">
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="person_id,reg_id" DataNavigateUrlFormatString="profile.aspx?Personid={0}&amp;Regid={1}&amp;action=att"
                        DataTextField="acad_year" HeaderText="Academic Year" ControlStyle-CssClass="hyperlink" />
                    <%-- <asp:BoundField DataField="acad_year" HeaderText="Academic Year" />--%>
                    <asp:BoundField DataField="term_name" HeaderText="Term" />
                    <asp:BoundField DataField="class" HeaderText="Class" />
                    <asp:BoundField DataField="TotalOpenDaysCount" HeaderText="Total Open Days" />
                    <asp:BoundField DataField="PresentCount" HeaderText="Present Days" />
                    <asp:BoundField DataField="AbsentCount" HeaderText="Absent Days" />
                    <asp:BoundField DataField="PresentCountPercent" HeaderText="Percentage" />
                </Columns>
            </asp:GridView>

            <div class="col-lg-12 row">

                <div class="col-lg-6">
                    <h1>Attendance Details</h1>
                </div>
                <div class="col-lg-6 h5 ">
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
            <div class="wrapper wrapper-content">
                <strong>Academic Year:</strong><asp:Label ID="lblAttAcademicYear" runat="server" Text="" CssClass="p-xs"></asp:Label>
                <strong>Term:</strong><asp:Label ID="lblAttTerm" runat="server" Text="" CssClass="p-xs"></asp:Label>
                <strong>Class:</strong><asp:Label ID="lblAttForm" runat="server" Text="" CssClass="p-xs"></asp:Label>
                <div class=" space-15"></div>
                <asp:Label ID="lblZeroAttendanceBreakDown" runat="server" Text="" CssClass="font-bold col-lg-offset-2"></asp:Label>
                <div id="divAttBreakDown" style="width: 100%; overflow: auto">
                    <asp:GridView ID="GridViewAttendanceBreakDown" runat="server" AutoGenerateColumns="False"
                        CssClass="table-striped table-bordered table-hover dataTables-example">
                        <Columns>
                            <asp:BoundField DataField="weekno" HeaderText="Week No" />
                            <asp:TemplateField HeaderText="Mon" HeaderStyle-Width="60px">
                                <ItemTemplate>
                                    <asp:TextBox ID="dd1" runat="server" Text='<%# Bind("1") %>' Enabled="false" Width="60px" Style="text-align: center"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tue" HeaderStyle-Width="60px">
                                <ItemTemplate>
                                    <asp:TextBox ID="dd2" runat="server" Text='<%# Bind("2") %>' Enabled="false" Width="60px" Style="text-align: center"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Wed" HeaderStyle-Width="60px">
                                <ItemTemplate>
                                    <asp:TextBox ID="dd3" runat="server" Text='<%# Bind("3") %>' Enabled="false" Width="60px" Style="text-align: center"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Thu" HeaderStyle-Width="60px">
                                <ItemTemplate>
                                    <asp:TextBox ID="dd4" runat="server" Text='<%# Bind("4") %>' Enabled="false" Width="60px" Style="text-align: center"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fri" HeaderStyle-Width="60px">
                                <ItemTemplate>
                                    <asp:TextBox ID="dd5" runat="server" Text='<%# Bind("5") %>' Enabled="false" Width="60px" Style="text-align: center"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="PresentCountPercent" HeaderText="Percentage">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>

        <div id="divPurchases" runat="server" visible="false">
            <div class=" wrapper-content  animated fadeInRight">
                <asp:Button ID="btnPurchase1" runat="server" Text="Add New Item" CssClass="btn btn-sm btn-primary m-t-n-xs" CausesValidation="true" Font-Bold="True" OnClick="btnPurchase1_Click" />
                <asp:Button ID="btnPurchase2" runat="server" Text=" Add and Pay for Item" CssClass="btn btn-sm btn-primary m-t-n-xs" CausesValidation="true" Font-Bold="True" OnClick="btnPurchase2_Click" />
                <asp:Button ID="btnPurchase3" runat="server" Text="Bulk Purchases" CssClass="btn btn-sm btn-primary m-t-n-xs" CausesValidation="true" Font-Bold="True" OnClick="btnPurchase3_Click" />
                <div id="PurchaseForm1" runat="server" visible="false">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="ibox">
                                <div role="form" id="form" runat="server">
                                    <div class="form-group">
                                        <div class="editor-label">
                                            <label>Item Name</label><span class="required">*</span>
                                        </div>
                                        <div class="editor-field">
                                            <asp:DropDownList ID="ddlSaleItem" runat="server" CssClass="form-control"></asp:DropDownList>
                                            <div class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvddlSaleItem" ErrorMessage="Sale Item Required" ForeColor="Red" ControlToValidate="ddlSaleItem"
                                                    runat="server" Dispaly="Dynamic" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="editor-label">
                                            <label>Quantity</label><span class="required">*</span>
                                        </div>
                                        <div class="editor-field">
                                            <asp:TextBox ID="tbQuantity" runat="server" placeholder="Enter Quantity" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                            <div class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvtbQuantity" ErrorMessage="Quantity Required" ForeColor="Red" ControlToValidate="tbQuantity"
                                                    runat="server" Dispaly="Dynamic" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="editor-label">
                                            <label>Additional Charges (If Any)</label>
                                        </div>
                                        <div class="editor-field">
                                            <asp:TextBox ID="tbAdditionalFee" runat="server" placeholder="Enter Addtional Fee" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="editor-label">
                                            <label>Purchase Date</label><span class="required">*</span>
                                        </div>
                                        <div class="editor-field">
                                            <asp:TextBox ID="tbPurchasedDate" runat="server" CssClass="table table-striped table-bordered table-hover dataTables-example datepick" placeholder="dd/mm/yyyy"></asp:TextBox>
                                            <div class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvtbPurchasedDate" ErrorMessage="Date Required" ForeColor="Red" ControlToValidate="tbPurchasedDate"
                                                    runat="server" Dispaly="Dynamic" />
                                            </div>
                                        </div>
                                    </div>



                                </div>
                                <asp:Button ID="BtnPurchaseForm1" runat="server" Text="Save" CssClass="btn btn-sm btn-primary m-t-n-xs" CausesValidation="true" Font-Bold="True" OnClick="BtnPurchaseForm1_Click" />
                                <asp:Button ID="BtnCancelPurchaseForm1" runat="server" Text="Cancel" CssClass="btn btn-sm btn-primary m-t-n-xs" CausesValidation="false" Font-Bold="True" OnClick="BtnCancelPurchaseForm1_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div id="PurchaseForm2" runat="server" visible="false">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="ibox">
                                <div role="form" id="form1" runat="server">
                                    <div class="form-group">
                                        <div class="editor-label">
                                            <label>Item Name</label><span class="required">*</span>
                                        </div>
                                        <div class="editor-field">
                                            <asp:DropDownList ID="ddlPayItemName" runat="server" CssClass="form-control"></asp:DropDownList>
                                            <div class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvddlPayItemName" ErrorMessage="Sale Item Required" ForeColor="Red" ControlToValidate="ddlPayItemName"
                                                    runat="server" Dispaly="Dynamic" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="editor-label">
                                            <label>Quantity</label><span class="required">*</span>
                                        </div>
                                        <div class="editor-field">
                                            <asp:TextBox ID="tbPayQty" runat="server" placeholder="Enter Quantity" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                            <div class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvtbPayQty" ErrorMessage="Quantity Required" ForeColor="Red" ControlToValidate="tbPayQty"
                                                    runat="server" Dispaly="Dynamic" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="editor-label">
                                            <label>Additional Charges (If Any)</label>
                                        </div>
                                        <div class="editor-field">
                                            <asp:TextBox ID="tbPayAddtionalFee" runat="server" placeholder="Enter Addtional Fee" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>


                                    <div class="form-group">
                                        <div class="editor-label">
                                            <label>Paying Amount<strong class="required">*</strong></label>
                                        </div>
                                        <div class="editor-field">
                                            <asp:TextBox ID="tbPayingAmount" runat="server" placeholder="Enter Amount" CssClass="form-control"></asp:TextBox>
                                            <div class="pull-right">
                                                <%--<asp:RegularExpressionValidator ID="revtbPayingAmount" ControlToValidate="tbPayingAmount" runat="server"
                                                ErrorMessage="Only Numbers allowed" ForeColor="Red" ValidationExpression="^(-)?(?=.*[1-9])(?:[1-9]\d*\.?|0?\.)\d*$"></asp:RegularExpressionValidator>--%>
                                                <asp:RequiredFieldValidator ID="rfvLName" ErrorMessage="Amount required" ForeColor="Red" ControlToValidate="tbPayingAmount"
                                                    runat="server" Display="Dynamic" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="editor-label">
                                            <label>Payment Method<strong class="required">*</strong></label>
                                        </div>
                                        <div class="editor-field">
                                            <asp:DropDownList ID="ddlPaymentMethod" runat="server" CssClass="form-control list">
                                                <asp:ListItem Value="">Please Select One</asp:ListItem>
                                                <asp:ListItem Value="Bank">Bank</asp:ListItem>
                                                <asp:ListItem Value="Cash">Cash</asp:ListItem>
                                                <asp:ListItem Value="Cheque">Cheque</asp:ListItem>
                                                <asp:ListItem Value="Scholarship/Discount">Scholarship/Discount</asp:ListItem>
                                                <asp:ListItem Value="Bad Debt/Write Off">Bad Debt/Write Off</asp:ListItem>
                                                <asp:ListItem Value="Other">Other</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvddlPaymentMethod" ErrorMessage="Payment Method Required" ForeColor="Red" ControlToValidate="ddlPaymentMethod"
                                                    runat="server" Dispaly="Dynamic" />
                                            </div>
                                        </div>

                                    </div>
                                    <div class="form-group bank">
                                        <div class="editor-label">
                                            <label>Bank Name<strong class="required">*</strong></label>
                                        </div>
                                        <div class="editor-field">
                                            <asp:DropDownList ID="ddlBankName" runat="server" CssClass="form-control"></asp:DropDownList>
                                            <div class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvddlBankName" ErrorMessage="Bank Name required" ForeColor="Red" ControlToValidate="ddlBankName"
                                                    runat="server" Dispaly="Dynamic" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="editor-label">
                                            <label>Payment Method Reference<strong class="required">*</strong></label>
                                        </div>
                                        <div class="editor-field">
                                            <asp:TextBox ID="tbPMReference" runat="server" placeholder="Enter Reference Number" CssClass="form-control"></asp:TextBox>
                                            <div class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvtbPMReference" ErrorMessage=" Reference required" ForeColor="Red" ControlToValidate="tbPMReference"
                                                    runat="server" Dispaly="Dynamic" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="editor-label">
                                            <label>Invoice Number</label>
                                        </div>
                                        <div class="editor-field">
                                            <asp:TextBox ID="tbInvoiceNumber" runat="server" placeholder="Enter Invoice Number" CssClass="form-control"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="editor-label">
                                            <label>Receipt Number</label>
                                        </div>
                                        <div class="editor-field">
                                            <asp:TextBox ID="tbReceiptNumber" runat="server" placeholder="Enter Receipt Number" CssClass="form-control"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="editor-label">
                                            <label>Purchase Date</label><span class="required">*</span>
                                        </div>
                                        <div class="editor-field">
                                            <asp:TextBox ID="tbPayPurchaseDate" runat="server" CssClass="table table-striped table-bordered table-hover dataTables-example datepick" placeholder="dd/mm/yyyy"></asp:TextBox>
                                            <div class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvtbPayPurchaseDate" ErrorMessage="Date Required" ForeColor="Red" ControlToValidate="tbPayPurchaseDate"
                                                    runat="server" Dispaly="Dynamic" />
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <asp:Button ID="BtnPurchaseForm2" runat="server" Text="Save" CssClass="btn btn-sm btn-primary m-t-n-xs" CausesValidation="true" Font-Bold="True" OnClick="BtnPurchaseForm2_Click" />
                                <asp:Button ID="BtnCancelPurchaseForm2" runat="server" Text="Cancel" CssClass="btn btn-sm btn-primary m-t-n-xs" CausesValidation="false" Font-Bold="True" OnClick="BtnCancelPurchaseForm2_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div id="PurchaseForm3" runat="server" visible="false">
                    <asp:SqlDataSource ID="SqlDataSourceBankBulkPur" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
                        SelectCommand="select status_name from ms_status where category = 'Bank'"></asp:SqlDataSource>
                    <h3>Bulk Purchases-List of Items</h3>
                    <asp:Button ID="BtnPurchaseForm3" runat="server" Text="Pay Now" CssClass=" pull-right btn-primary" OnClientClick="return Validate_Checkbox()" OnClick="BtnPurchaseForm3_Click" />
                    <asp:Button ID="BtnCancelPurchaseForm3" runat="server" Text="Cancel" CssClass="pull-right  btn-primary" CausesValidation="false" Font-Bold="True" OnClick="BtnCancelPurchaseForm3_Click" />
                    <asp:GridView ID="GridViewBulkPurchases" runat="server" CssClass="table table-striped table-bordered table-hover dataTables-example"
                        AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="40px">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkboxSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkboxSelectAll_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkStudent" runat="server" AutoPostBack="true" OnCheckedChanged="chkStudent_CheckedChanged"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="msi_id" HeaderText="msi_id" InsertVisible="False" ReadOnly="True" SortExpression="msi_id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                <HeaderStyle CssClass="hidden"></HeaderStyle>
                                <ItemStyle CssClass="hidden"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="item_name" HeaderText="Item Name" />
                            <asp:BoundField DataField="price" HeaderText="Price" />
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlBulkQuantity" runat="server">
                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="5" Value="5"></asp:ListItem>

                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Additional Charges (If Any)" ItemStyle-Width="20">
                                <ItemTemplate>
                                    <asp:TextBox ID="tbBulkAddCharges" runat="server" Text="" Width="60"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Paying Amount" ItemStyle-Width="20">
                                <ItemTemplate>
                                    <asp:TextBox ID="tbBulkPurAmount" runat="server" Text='<%# Bind("price") %>' Width="60"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Payment Method">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlPaymentMethod" runat="server" CssClass="form-control list" Width="100">
                                        <asp:ListItem Value="Cash">Cash</asp:ListItem>
                                        <asp:ListItem Value="Bank">Bank</asp:ListItem>
                                        <asp:ListItem Value="Cheque">Cheque</asp:ListItem>
                                        <asp:ListItem Value="Scholarship/Discount">Scholarship/Discount</asp:ListItem>
                                        <asp:ListItem Value="Bad Debt/Write Off">Bad Debt/Write Off</asp:ListItem>
                                        <asp:ListItem Value="Other">Other</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bank Name">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlBankBulkPur" runat="server" DataValueField="status_name" DataTextField="status_name" DataSourceID="SqlDataSourceBankBulkPur" AppendDataBoundItems="true">
                                        <asp:ListItem Text="Select" Value="" />
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reference" ItemStyle-Width="20">
                                <ItemTemplate>
                                    <asp:TextBox ID="tbBulkPurReference" runat="server" Text="" Width="60"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="rfvtbBulkPurReference" ErrorMessage="*" ForeColor="Red" ControlToValidate="tbBulkPurReference" Enabled="false"
                                                    runat="server" Dispaly="Dynamic" />--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Invoice Number" ItemStyle-Width="20">
                                <ItemTemplate>
                                    <asp:TextBox ID="tbBulkInvNo" runat="server" Text="" Width="60"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Receipt Number" ItemStyle-Width="20">
                                <ItemTemplate>
                                    <asp:TextBox ID="tbBulkRecNo" runat="server" Text="" Width="60"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- <asp:TemplateField HeaderText="Purchase Date*">
                                <ItemTemplate>
                                    <asp:TextBox ID="tbBulkPurDate" runat="server" Text='<%# Convert.ToDateTime(Eval("dateTime")).ToString("d") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                        </Columns>
                    </asp:GridView>
                </div>
                <asp:Label runat="server" ID="lblZeroBulkPurchases"></asp:Label>

                <div class="row">
                    <div class="col-lg-8">
                        <h1>Item(s) Summary</h1>
                        <asp:Label ID="lblPurchasesSummary" runat="server" Text=""></asp:Label>
                        <asp:Button ID="btnPrintPurchases" runat="server" Text="Print Item(s) Summary" Visible="false" CssClass="col-lg-offset-9 btn btn-primary fa fa-print"
                            OnClientClick="SetTarget();" OnClick="btnPrintPurchases_Click" />
                        <div id="divpurchasesflow" style="width: 100%; height: 100%; overflow: auto">
                            <asp:GridView ID="GridViewPurchasesSummary" runat="server" AutoGenerateColumns="False" ShowFooter="false" DataKeyNames="purch_id"
                                CssClass="table table-striped table-bordered table-hover dataTables-example"
                                OnRowEditing="GridViewPurchasesSummary_RowEditing" OnRowCancelingEdit="GridViewPurchasesSummary_RowCancelingEdit"
                                OnRowUpdating="GridViewPurchasesSummary_RowUpdating" OnRowDeleting="GridViewPurchasesSummary_RowDeleting" EditRowStyle-CssClass="GridViewEditRow">
                                <Columns>
                                    <asp:TemplateField HeaderText="Purchase Date">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="tbPurchaseDate" runat="server" rel="date" type="text" CssClass="datepick input-group date" Text='<%# Bind("Purchase_Date") %>'></asp:TextBox>

                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPurchaseDate" runat="server" Text='<%# Bind("Purchase_Date") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:HyperLinkField DataNavigateUrlFields="person_id,purch_id" DataNavigateUrlFormatString="profile.aspx?Personid={0}&amp;purch_id={1}&amp;action=3REe8GwY6X"
                                        DataTextField="item_name" HeaderText="Sale Item" ControlStyle-CssClass="hyperlink" />
                                    <asp:TemplateField HeaderText="Quantity">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="tbquantity" runat="server" Text='<%# Bind("quantity") %>'></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="revtbquantity" ControlToValidate="tbquantity" runat="server"
                                                ErrorMessage="Only Numbers allowed" ForeColor="Red" ValidationExpression="^\d+([,\.]\d{1,2})?$"></asp:RegularExpressionValidator>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Bind("quantity") %>'></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Price#" HeaderText="Price" ReadOnly="true" />
                                    <asp:TemplateField HeaderText="Additional Charges">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="tbAdditionalfee" runat="server" CssClass="input-group" Text='<%# Bind("additional_fee") %>'></asp:TextBox>

                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAdditionalfee" runat="server" Text='<%# Bind("additional_fee") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:BoundField DataField="Total Price Due#" HeaderText="Total Price Due" ReadOnly="true" />

                                    <asp:BoundField DataField="Total Amount_Paid" HeaderText="Total paid" ReadOnly="true" />
                                    <asp:TemplateField HeaderText="Total Item Amount Outstanding" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="tbBalance" runat="server" Text='<%# Bind("Balance") %>' ReadOnly="true"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblBalance" runat="server" Text='<%# Bind("Balance") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <div>
                                                <asp:Label ID="lblTotalBalance" runat="server" />
                                            </div>
                                        </FooterTemplate>
                                        <HeaderStyle CssClass="hidden"></HeaderStyle>
                                        <ItemStyle CssClass="hidden"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="SBalance" HeaderText="Balance" ReadOnly="true" />
                                    <asp:BoundField DataField="purch_id" InsertVisible="False" ReadOnly="True" SortExpression="purch_id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                        <HeaderStyle CssClass="hidden"></HeaderStyle>
                                        <ItemStyle CssClass="hidden"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="msi_id" HeaderText="Msi Id" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                        <HeaderStyle CssClass="hidden"></HeaderStyle>
                                        <ItemStyle CssClass="hidden"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="Edit" CssClass="small blue-bg" CausesValidation="False" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="lnkUpdate" runat="server" Text="Update" CommandName="Update" CssClass="small blue-bg" CausesValidation="false" />
                                            <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel" CommandName="Cancel" CssClass="small blue-bg" CausesValidation="False" />
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete" CssClass="small blue-bg" OnClientClick="return confirm('Are you sure you want to delete ?');" Text="Delete"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Payment">
                                        <ItemTemplate>
                                            <asp:Button ID="btnPayItems" runat="server" Text="Pay" CommandArgument="Button1" OnClick="btnPayItems_Click" CssClass="small blue-bg" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <%-- <FooterStyle BackColor="#f3f3f4" ForeColor="Black" HorizontalAlign="Left" />--%>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="col-lg-4">
                        <h1>Payment Details</h1>
                        <label class=" btn dim">Item Name:</label><asp:Label ID="lblItemName" runat="server" Text="" CssClass="btn   dim"></asp:Label>
                        <%-- <label class=" btn dim">Payment Date</label><asp:Label ID="lblPurchaseDate" runat="server" Text="" CssClass="btn   dim"></asp:Label>--%>
                        <br />
                        <asp:Label ID="lblItemDetails" runat="server" Text=""></asp:Label>


                        <div class="clearfix"></div>
                        <asp:GridView ID="GridViewPurchaseBreakDown" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            CssClass="table table-striped table-bordered table-hover dataTables-example table-fixed" OnRowDataBound="GridViewPurchaseBreakDown_RowDataBound"
                            Style="table-layout: auto;">
                            <Columns>
                                <asp:BoundField DataField="Payment_Received_Date" HeaderText="Payment Date" HeaderStyle-Wrap="true" HeaderStyle-Width="10" ItemStyle-Wrap="true" />
                                <asp:BoundField DataField="payment_method_ref" HeaderText="Payment Reference" HeaderStyle-Wrap="true" HeaderStyle-Width="10" ItemStyle-Wrap="true" />
                                <asp:TemplateField HeaderText="Amount Paid">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmountPaid" runat="server" Text='<%# Bind("Amount_Paid") %>' CssClass="pull-right"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalPaid" runat="server" CssClass="pull-right" />

                                    </FooterTemplate>

                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>

        <div id="divNotes" runat="server" visible="false">
            <h1>Notes</h1>
             <asp:GridView ID="GridViewNotes" runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="reg_id" PageIndex="20"
                CssClass="table table-striped table-bordered table-hover dataTables-example">
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="person_id,reg_id" DataNavigateUrlFormatString="profile.aspx?Personid={0}&amp;Regid={1}&amp;action=note"
                        DataTextField="acad_year" HeaderText="Academic Year" ControlStyle-CssClass="hyperlink" />
                    <asp:BoundField DataField="term_name" HeaderText="Term" />
                    <asp:BoundField DataField="form_name" HeaderText="Class" />
                    <asp:BoundField DataField="total_comment_1" HeaderText="Total Comments" />
                    <asp:TemplateField HeaderText="Add">
                        <ItemTemplate>
                            <asp:Button ID="btnAddNotes" runat="server" Text="Add Notes" CommandArgument="Button1" OnClick="btnAddNotes_Click"  />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="reg_id" InsertVisible="False" ReadOnly="True" SortExpression="reg_id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                        <HeaderStyle CssClass="hidden"></HeaderStyle>
                        <ItemStyle CssClass="hidden"></ItemStyle>
                        <FooterStyle CssClass="hidden" />
                   </asp:BoundField>
                </Columns>
                <FooterStyle BackColor="#f3f3f4" ForeColor="Black" HorizontalAlign="Left" />
            </asp:GridView>
            <asp:Label ID="lblZeroNotes" runat="server" Text="" CssClass="font-bold"></asp:Label>
            <h2>Notes Details</h2>
             <asp:Label runat="server" ID="lblZeroBrkDown"></asp:Label>
             <div class="col-xs-12 col-sm-6 col-md-6" runat="server" id="divNoteslabel" visible="false">
                            <strong>Academic Year:</strong><asp:Label ID="lblNoteAcademic" runat="server" Text="" CssClass="p-xs"></asp:Label>
                            <strong>Term:</strong><asp:Label ID="lblNoteTerm" runat="server" Text="" CssClass="p-xs"></asp:Label>
                            <strong>Class:</strong><asp:Label ID="lblNoteClass" runat="server" Text="" CssClass="p-xs"></asp:Label>
             </div>
           
            <asp:GridView ID="GridViewNotesBrkDown" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example" OnRowEditing="GridViewNotesBrkDown_RowEditing"
                OnRowCancelingEdit="GridViewNotesBrkDown_RowCancelingEdit" OnRowUpdating="GridViewNotesBrkDown_RowUpdating" OnRowDeleting="GridViewNotesBrkDown_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="reg_id" HeaderText="Reg ID" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                        <HeaderStyle CssClass="hidden"></HeaderStyle>
                        <ItemStyle CssClass="hidden"></ItemStyle>
                    </asp:BoundField>
                     <asp:BoundField DataField="note_id" HeaderText="Note ID" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                        <HeaderStyle CssClass="hidden"></HeaderStyle>
                        <ItemStyle CssClass="hidden"></ItemStyle>
                    </asp:BoundField>
                     <asp:TemplateField HeaderText="Comment">
                        <EditItemTemplate>
                            <asp:TextBox ID="tbComment1" runat="server" Text='<%# Bind("comment_1") %>' CssClass="input-group"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtbComment1" ErrorMessage="*" ForeColor="Red" ControlToValidate="tbComment1"
                                runat="server" Dispaly="Dynamic" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbltbComment1" runat="server" Text='<%# Bind("comment_1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                   <asp:TemplateField HeaderText="Date">
                        <EditItemTemplate>
                            <asp:TextBox ID="tbNoteDate" runat="server" Text='<%# Bind("note_date") %>' CssClass="datepick"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtbNoteDate" ErrorMessage="*" ForeColor="Red" ControlToValidate="tbNoteDate"
                                runat="server" Dispaly="Dynamic" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbltbNoteDate" runat="server" Text='<%# Bind("note_date") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Note Type">
                        <EditItemTemplate>
                             <asp:TextBox ID="tbNoteType" runat="server" Text='<%# Bind("note_type") %>' CssClass="input-group"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbltbNoteType" runat="server" Text='<%# Bind("note_type") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                  
                    <asp:TemplateField ShowHeader="false" HeaderText="Edit">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CssClass="btn blue-bg" CommandName="Edit" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="lnkUpdate" runat="server" Text="Update" CssClass="btn blue-bg" CommandName="Update" />
                            <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel" CssClass="btn blue-bg" CommandName="Cancel" CausesValidation="False" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" CssClass="btn blue-bg" OnClientClick="return confirm('Are you sure you want to delete ?');" Text="Delete"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

           
        </div> 
    </div>
    <%--  --Pages Styles--%>
    <style type="text/css">
        /*a {
            color: white;
        }*/

        .hyperlink {
            color: blue;
        }

        .required {
            color: #F00;
        }

        .GridViewEditRow input[type=text] {
            width: 80px;
        }
        /* size textboxes */
        .GridViewEditRow select {
            width: 70px;
        }

        .text-orange {
            color: #FF8200;
        }

        .text-blue {
            color: #0000FF;
        }

        .breakword {
            word-wrap: break-word;
            word-break: break-all;
        }
    </style>

</asp:Content>

