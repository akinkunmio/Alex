<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="employee_profile.aspx.cs" Inherits="Alex.pages.employee_profile" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function menuselection(arg) {
            $(arg).removeClass("btn btn-primary");
            $(arg).addClass("btn btn-active");
        }
    </script>

    <script>
        function Validate() {
            alert("Please select anyone");
        }
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../scripts/css/pagestyle.css" rel="stylesheet" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
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
        // Joe 250118 1911 Make calendar show previous and future years $(function () { $('.datepick').prop('readonly', true).datepicker({ changeMonth: true, minDate: 0, changeYear: true, dateFormat: 'dd/mm/yy' }); });
        $(function () { $('.datepick').prop('readonly', true).datepicker({ changeMonth: true, changeYear: true, yearRange: '-100:+1', dateFormat: 'dd/mm/yy' }); });
        function CheckForm() {
            if ($('#<%=tbAddressEndDate.ClientID %>').val() == "") {
                alert('Please enter End Date ');
                return false;
            }
            return true;
        }

        function validateDate(sender, e) {

            //       // Split out the constituent parts (dd/mm/yyyy)    
            //       var dayfield = e.Value.split("/")[0];
            //       var monthfield = e.Value.split("/")[1];
            //       var yearfield = e.Value.split("/")[2];

            //       // Create a new date object based on the separate parts
            //       var dateValue = new Date(yearfield, monthfield - 1, dayfield)

            //       // Check that the date object's parts match the split out parts from the original string
            //       if ((dateValue.getMonth() + 1 != monthfield) || (dateValue.getDate() != dayfield) || (dateValue.getFullYear() != yearfield)) {
            //           e.IsValid = false;
            //       }

            //       // Check for future dates
            //       if (e.IsValid) {
            //           e.IsValid = dateValue <= new Date()
            //       }
        }
        function CheckBank(sender, args) {
         <%--var detailsview = document.getElementById('<%= DetailsViewSalaryPayroll.ClientID %>');
         var textBox = detailsview.getElementById("tbBankAccountNumber");
         var dropdown = detailsview.getElementById("ddlBankName").val();
         var textLength = textBox.value.length;
         if (textLength > 1 && dropdown == "")
         {
             args.IsValid = false;
             
         }--%>

     }
    </script>
    
    <div class="col-md-12  dashboard-header">
        <div class="col-md-9">
            <asp:Label ID="LabelProfileName" runat="server" Text="Profile Name label" CssClass="h1"></asp:Label>
        </div>

        <div class="col-md-2 pull-right">
            <a href="../pages/employees.aspx">
                <asp:Label runat="server" Text="Back to Employees list" CssClass="small fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs"></asp:Label>
            </a>
        </div>
    </div>
    <div class="ibox-content" style="padding: 15px 20px 20px 380px;">
        <asp:Button ID="BtnProfile" runat="server" Text="Profile" CssClass="btn  btn-primary" OnClick="BtnProfile_Click" CausesValidation="false" />
        <asp:Button ID="BtnAddress" runat="server" Text="Address" CssClass="btn  btn-primary" OnClick="BtnAddress_Click" CausesValidation="false" />
        <asp:Button ID="BtnPayroll" runat="server" Text="Payroll" CssClass="btn  btn-primary" OnClick="BtnPayroll_Click" CausesValidation="false" />
        <%--Joe 220118 Repaced this with above line<asp:Button ID="Button1" runat="server" Text="Payroll 1" CssClass="btn  btn-primary" OnClick="BtnPayroll_Click" CausesValidation="false" />--%>
        <%-- Joe 220118<asp:Button ID="BtnPayroll2" runat="server" Text="Payroll 2" CssClass="btn  btn-primary" OnClick="BtnPayroll2_Click"  CausesValidation="false" />--%>
        <asp:Button ID="BtnQualifications" runat="server" Text="Qualifications" CssClass="btn  btn-primary" OnClick="BtnQualifications_Click" CausesValidation="false" />

    </div>
    <div id="divEmployeeProfiledropdown">
        <asp:SqlDataSource ID="SqlDataSourceNationalityDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
            SelectCommand="SELECT nationality FROM ms_dropdown_countries2"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSourceEthnicityDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
            SelectCommand="SELECT ethnicity FROM ms_dropdown_nigeria_ethnicity"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSourceReligionDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
            SelectCommand="SELECT status_name FROM ms_status Where category like 'religion'"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSourceCountryDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
            SelectCommand="sp_ms_countries_dropdown"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSourceCityDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
            SelectCommand="sp_ms_nigeria_lagos_state_lga_dropdown"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSourceStateDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
            SelectCommand="sp_ms_nigerian_states_dropdown"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSourceBloodGroup" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
            SelectCommand="sp_ms_blood_group_dropdown"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSourceBankDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
            SelectCommand="SELECT status_name FROM ms_status Where category = 'Bank' "></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSourceDeptDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
            SelectCommand="sp_ms_hr_department_dropdown"></asp:SqlDataSource>
    </div>
    <asp:SqlDataSource ID="SqlDataSourceEmployeeProfile" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
        DeleteCommand="sp_ms_hr_employees_delete" DeleteCommandType="StoredProcedure" OnDeleted="SqlDataSourceEmployeeProfile_Deleted" OnDeleting="SqlDataSourceEmployeeProfile_Deleting"
        SelectCommand="sp_ms_hr_employee_profile" SelectCommandType="StoredProcedure"
        UpdateCommand="sp_ms_hr_employees_edit" UpdateCommandType="StoredProcedure" OnUpdated="SqlDataSourceEmployeeProfile_Updated">
        <DeleteParameters>
            <asp:Parameter Name="emp_id" Type="Int32" />
            <asp:Parameter Name="from" Type="String" />
        </DeleteParameters>
        <SelectParameters>
            <asp:QueryStringParameter Name="emp_id" QueryStringField="EmployeeId" Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="emp_id" Type="Int32" />
            <asp:Parameter Name="dept_name" Type="String" />
            <asp:Parameter Name="f_name" Type="String" />
            <asp:Parameter Name="m_name" Type="String" />
            <asp:Parameter Name="l_name" Type="String" />
            <asp:Parameter Name="title" Type="String" />
            <asp:Parameter DbType="String" Name="dob" />
            <asp:Parameter Name="gender" Type="String" />
            <asp:Parameter Name="nationality" Type="String" />
            <asp:Parameter Name="ethnicity" Type="String" />
            <asp:Parameter Name="religion" Type="String" />
            <asp:Parameter Name="email_add" Type="String" />
            <asp:Parameter Name="contact_no1" Type="String" />
            <asp:Parameter Name="contact_no2" Type="String" />
            <asp:Parameter Name="next_of_kin" Type="String" />
            <asp:Parameter Name="next_of_kin_contact_no" Type="String" />
            <asp:Parameter Name="next_of_kin_email_add" Type="String" />
              <asp:Parameter Name="bank" Type="String" />
            <asp:Parameter Name="bank_account_no" Type="String" />
            <asp:Parameter Name="blood_group" Type="String" />
            <asp:Parameter Name="notes" Type="String" />
            <asp:Parameter Name="updated_by" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <div id="ViewProfile" runat="server">
        <div class=" wrapper-content  animated fadeInRight">
            <div class="row">

                <div class="col-lg-12">
                    <div class="col-md-6">
                        <asp:DetailsView ID="DetailsViewProfile" runat="server"
                            CssClass="table table-striped table-bordered table-hover dataTables-example" Height="16px" Width="499px" AutoGenerateRows="False"
                            DataKeyNames="emp_id"
                            DataSourceID="SqlDataSourceEmployeeProfile">
                            <Fields>
                                <asp:BoundField DataField="emp_id" HeaderText="emp_id" InsertVisible="False" ReadOnly="True" SortExpression="emp_id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                                    <ItemStyle CssClass="hidden"></ItemStyle>
                                </asp:BoundField>
                               <%-- <asp:BoundField DataField="dept_id" HeaderText="dept_id" SortExpression="dept_id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                                    <ItemStyle CssClass="hidden"></ItemStyle>
                                </asp:BoundField>--%>
                                <%--<asp:BoundField DataField="dept_name" HeaderText="Department Name" SortExpression="dept_name" ReadOnly="true" />--%>
                                <asp:TemplateField HeaderText="Department Name">
                                    <HeaderTemplate>
                                        Department Name<span class="required">*</span>
                                    </HeaderTemplate>
                                    <EditItemTemplate>
                                       <asp:DropDownList ID="ddlDeptName" runat="server" Text='<%# Bind("dept_name") %>' DataValueField="dept_name" DataTextField="dept_name" CssClass="input-group"
                                            AppendDataBoundItems="true" SelectedValue='<%# Bind("dept_name") %>' DataSourceID="SqlDataSourceDeptDropDown">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvtbDeptName" ErrorMessage="Department Required" ForeColor="Red" ControlToValidate="ddlDeptName"
                                            runat="server" Dispaly="Dynamic" />
                                       
                                    </EditItemTemplate>
                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lblDeptName" runat="server" Text='<%# Bind("dept_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="hire_date" HeaderText="Start Date" SortExpression="hiredate" ReadOnly="true" />
                               <%--  <asp:TemplateField HeaderText="Start Date">
                                    <HeaderTemplate>
                                        Start Date
                                    </HeaderTemplate>
                                   <EditItemTemplate>
                                        <asp:TextBox ID="tbStartDate" runat="server" rel="date" type="text" CssClass="datepick input-group date" Text='<%# Bind("hire_date") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtbStartDate" ErrorMessage="Date Required" ForeColor="Red" ControlToValidate="tbStartDate"
                                            runat="server" Dispaly="Dynamic" />
                                        <asp:CustomValidator runat="server" ID="valDate" ControlToValidate="tbStartDate" ForeColor="Red" ErrorMessage="Enter valid date" ClientValidationFunction="validateDate" />
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbStartDate" runat="server" Text='<%# Bind("hire_date") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStartDate" runat="server" Text='<%# Bind("hire_date") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:BoundField DataField="end_date" HeaderText="End Date" SortExpression="end_date" ReadOnly="true" />
                               <%-- <asp:TemplateField HeaderText="End Date" SortExpression="END Date">
                                    <HeaderTemplate>
                                        End Date
                                    </HeaderTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbEndDate" runat="server" rel="date" type="text" CssClass="datepick input-group date" Text='<%# Bind("end_date") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbEndDate" runat="server" Text='<%# Bind("end_date") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblEndDate" runat="server" Text='<%# Bind("end_date") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Title<span class="required">*</span>
                                    </HeaderTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlTitle" runat="server" Text='<%# Bind("title") %>' AppendDataBoundItems="true" SelectedValue='<%# Bind("title") %>'>
                                            <asp:ListItem>Mr</asp:ListItem>
                                            <asp:ListItem>Mrs</asp:ListItem>
                                            <asp:ListItem>Ms</asp:ListItem>
                                            <asp:ListItem>Miss</asp:ListItem>
                                            <asp:ListItem>Sir</asp:ListItem>
                                            <asp:ListItem>Dr</asp:ListItem>
                                            <asp:ListItem>Madam</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvTitle" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlTitle"
                                            runat="server" Dispaly="Dynamic" />
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbTitle" runat="server" Text='<%# Bind("title") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbltitle" runat="server" Text='<%# Bind("title") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Last Name" SortExpression="l_name">
                                    <HeaderTemplate>
                                        Last Name <span class="required">*</span>
                                    </HeaderTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbLastName" runat="server" Text='<%# Bind("l_name") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvLName" ErrorMessage="*" ForeColor="Red" ControlToValidate="tbLastName"
                                            runat="server" Dispaly="Dynamic" />
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbLastName" runat="server" Text='<%# Bind("l_name") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbLastName" runat="server" Text='<%# Bind("l_name") %>'></asp:Label>
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
                                <asp:TemplateField HeaderText="Date Of Birth" SortExpression="dob">
                                    <HeaderTemplate>
                                        Date of Birth <span class="required">*</span>
                                    </HeaderTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbDOB" runat="server" rel="date" type="text" CssClass="datepick input-group date" Text='<%# Bind("dob") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDOB" ErrorMessage="*" ForeColor="Red" ControlToValidate="tbDOB"
                                            runat="server" Dispaly="Dynamic" />
                                        <asp:CustomValidator runat="server" ID="valDateRange" ControlToValidate="tbDOB" ForeColor="Red" ErrorMessage="Enter valid date" ClientValidationFunction="validateDate" />
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbDOB" runat="server" Text='<%# Bind("dob") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDOB" runat="server" Text='<%# Bind("dob") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Gender" SortExpression="gender">
                                    <HeaderTemplate>
                                        Gender <span class="required">*</span>
                                    </HeaderTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlGender" runat="server" Text='<%# Bind("gender") %>' AppendDataBoundItems="true" SelectedValue='<%# Bind("gender") %>'>
                                            <asp:ListItem>Male</asp:ListItem>
                                            <asp:ListItem>Female</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="rfvddlGender" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlGender"
                                            runat="server" Dispaly="Dynamic" />
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbGender" runat="server" Text='<%# Bind("gender") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblGender" runat="server" Text='<%# Bind("gender") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
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
                                <asp:TemplateField HeaderText="Email" SortExpression="email_add">
                                    <HeaderTemplate>
                                        Email<span class="required">*</span>
                                    </HeaderTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbEmail" runat="server" Text='<%# Bind("email_add") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvEmail" ErrorMessage="*" ForeColor="Red" ControlToValidate="tbEmail"
                                            runat="server" Dispaly="Dynamic" />
                                        <asp:RegularExpressionValidator ID="revtbEmail" runat="server"
                                            ErrorMessage="Invalid Email" ControlToValidate="tbEmail" ForeColor="Red"
                                            SetFocusOnError="True" ValidationExpression="^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$" />
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbEmail" runat="server" Text='<%# Bind("email_add") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("email_add") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mobile Number" SortExpression="contact_no1">
                                    <HeaderTemplate>
                                        Mobile Number<span class="required">*</span>
                                    </HeaderTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbMobileNo" runat="server" Text='<%# Bind("contact_no1") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvMobileNo" ErrorMessage="*" ForeColor="Red" ControlToValidate="tbMobileNo"
                                            runat="server" Dispaly="Dynamic" />
                                        <asp:RegularExpressionValidator ID="revtbMobileNo" ControlToValidate="tbMobileNo" runat="server"
                                            ErrorMessage="Phone Number should be in 11 digits " ForeColor="Red" ValidationExpression="\d{11}"></asp:RegularExpressionValidator>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbMobileNo" runat="server" Text='<%# Bind("contact_no1") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMobileNo" runat="server" Text='<%# Bind("contact_no1") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Landline Number" SortExpression="contact_no2">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbContactNo2" runat="server" Text='<%# Bind("contact_no2") %>'></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revtbContactNo2" ControlToValidate="tbContactNo2" runat="server"
                                            ErrorMessage="Phone Number should be in 11 digits" ForeColor="Red" ValidationExpression="\d{11}"></asp:RegularExpressionValidator>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbContactNo2" runat="server" Text='<%# Bind("contact_no2") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("contact_no2") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Next of Kin" SortExpression="next_of_kin">
                                    <HeaderTemplate>
                                        Next of Kin<span class="required">*</span>
                                    </HeaderTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbNextOfKin" runat="server" Text='<%# Bind("next_of_kin") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtbNextOfKin" ErrorMessage="*" ForeColor="Red" ControlToValidate="tbNextOfKin"
                                            runat="server" Dispaly="Dynamic" />
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbNextOfKin" runat="server" Text='<%# Bind("next_of_kin") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNextOfKin" runat="server" Text='<%# Bind("next_of_kin") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Next of Kin Contact no" SortExpression="next_of_kin_contact_no">
                                    <HeaderTemplate>
                                        Next of Kin Contact no <span class="required">*</span>
                                    </HeaderTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbNextOfKinContactNo" runat="server" Text='<%# Bind("next_of_kin_contact_no") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtbNextOfKinContactNo" ErrorMessage="*" ForeColor="Red" ControlToValidate="tbNextOfKinContactNo"
                                            runat="server" Dispaly="Dynamic" />
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbNextOfKinContactNo" runat="server" Text='<%# Bind("next_of_kin_contact_no") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbltbNextOfKinContactNo" runat="server" Text='<%# Bind("next_of_kin_contact_no") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Next of Kin Email" SortExpression="next_of_kin_email_add">
                                    <HeaderTemplate>
                                        Next of Kin Email
                                    </HeaderTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbNextOfKinEmail" runat="server" Text='<%# Bind("next_of_kin_email_add") %>'></asp:TextBox>
                                        <%-- <asp:RequiredFieldValidator ID="rfvtbNextOfKinEmail" ErrorMessage="*" ForeColor="Red" ControlToValidate="tbNextOfKinEmail"
                            runat="server" Dispaly="Dynamic" />--%>
                                        <asp:RegularExpressionValidator ID="revtbNextOfKinEmail" runat="server"
                                            ErrorMessage="Invalid Email" ControlToValidate="tbNextOfKinEmail" ForeColor="Red"
                                            SetFocusOnError="True" ValidationExpression="^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$" />
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbNextOfKinEmail" runat="server" Text='<%# Bind("next_of_kin_email_add") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbltbNextOfKinEmail" runat="server" Text='<%# Bind("next_of_kin_email_add") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank" Visible="false">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlBankName" runat="server" Text='<%# Bind("bank") %>' CssClass="input-group" DataValueField="status_name" DataTextField="status_name"
                                            DataSourceID="SqlDataSourceBankDropDown" SelectedValue='<%# Bind("bank") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblBank" runat="server" Text='<%# Bind("bank") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Bank Account No" Visible="false">
                                    <HeaderTemplate>
                                        Bank Account No
                                    </HeaderTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbBankAccountNo" runat="server" Text='<%# Bind("bank_account_no") %>'></asp:TextBox>

                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbBankAccountNo" runat="server" Text='<%# Bind("bank_account_no") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblBankAccountNo" runat="server" Text='<%# Bind("bank_account_no") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Notes">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbNotes" runat="server" Text='<%# Bind("notes") %>' MaxLength="500" Height="80px" Wrap="true"></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbNotes" runat="server" Text='<%# Bind("notes") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNotes" runat="server" Text='<%# Bind("notes") %>' Height="80px" Wrap="true"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                                        &nbsp;<asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="False" CommandName="Edit" Text=" Edit"></asp:LinkButton>
                                        &nbsp;<asp:LinkButton ID="LinkButtonDelete" runat="server" CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete ?');" Text=" Delete"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="btn btn-primary" Width="80px" />
                                    <HeaderStyle Width="80px" />
                                </asp:TemplateField>

                            </Fields>
                        </asp:DetailsView>
                    </div>
                    <div class="col-md-4">
                     <div id="divProfilePic" runat="server" visible="true" style="width: 200px">
                        <div class="form-group">
                          <asp:Image ID="ProfilePicture" runat="server" length="200px" Width="200px" AlternateText="NO Image Found" /><br />
                            <br />
                            <asp:ScriptManager ID="ScriptManagerUpload" runat="server"></asp:ScriptManager>
                            <asp:UpdatePanel ID="UpdatePanelUpload" runat="server" UpdateMode="conditional">
                                <ContentTemplate>
                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                    <asp:Button ID="Upload" runat="server" Text="Upload" CssClass="btn btn-primary btn-xs"  OnClick="Upload_Click" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="Upload" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <br />
                            <div class="ibox-content">
                                
                                <asp:label runat="server" ID="lblEndEmployee" CssClass="sidebar-title"></asp:label>
                                <asp:Button ID="btnEndEmployee" runat="server" Text="" CssClass="btn-primary" OnClick="btnEndEmployee_Click" />
                            </div>
                        </div>

                     </div>
                  </div>

                   <%-- <div class="col-md-offset-6">
                        <asp:Button ID="btnEndEmployee" runat="server" Text="End Employee" CssClass="btn-primary" OnClick="btnEndEmployee_Click" />
                    </div>--%>
                </div>
            </div>
        </div>
    </div>
    <div id="divEndDateEmployee" runat="server" visible="false">
        <div class=" wrapper-content  animated fadeInRight">
            <div class="row">
                <div class="col-lg-6">
                    <div class="ibox">
                        <div role="form">
                            <div class="form-group">
                                <div class="editor-label">
                                    <label>Date<strong class="required">*</strong></label>
                                </div>
                                <div class="editor-field">
                                    <asp:TextBox ID="tbEndDateEmployee" runat="server" placeholder="Enter Date" CssClass="datepick"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtbEndDateEmployee" ErrorMessage="Date Required" ForeColor="Red" CssClass="pull-right" ControlToValidate="tbEndDateEmployee"
                                        runat="server" Dispaly="Dynamic" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:Button ID="btnEndDateEmployee" runat="server" Text="Submit" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" OnClick="btnEndDateEmployee_Click" />
        </div>

    </div>

    <div id="ViewAddress" runat="server">
        <div class="row">
            <div class="col-lg-12">
                <div class="col-md-8">
                    <h1>Current Address</h1>
                    <asp:DetailsView ID="DetailsViewAddress" runat="server"
                        CssClass="table table-striped table-bordered table-hover dataTables-example" Height="16px" Width="799px" DataKeyNames="emp_id" AutoGenerateRows="False"
                        DataSourceID="SqlDataSourceAddress" OnItemCreated="DetailsViewAddress_ItemCreated">
                        <Fields>
                            <asp:BoundField DataField="emp_id" HeaderText="emp_id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                <HeaderStyle CssClass="hidden"></HeaderStyle>
                                <ItemStyle CssClass="hidden"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="emp_add_id" HeaderText="emp_add_id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" InsertVisible="False" ReadOnly="True" SortExpression="emp_add_id">
                                <HeaderStyle CssClass="hidden"></HeaderStyle>

                                <ItemStyle CssClass="hidden"></ItemStyle>
                            </asp:BoundField>

                            <asp:TemplateField>
                                <HeaderTemplate>
                                    Address line 1 <span class="required">*</span>
                                </HeaderTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="tbAddressLine1" runat="server" Text='<%# Bind("address_line1") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtbAddressLine1" ErrorMessage="*" ForeColor="Red" ControlToValidate="tbAddressLine1"
                                        runat="server" Dispaly="Dynamic" />
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <asp:TextBox ID="tbAddressLine1" runat="server" Text='<%# Bind("address_line1") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtbAddressLine1" ErrorMessage="*" ForeColor="Red" ControlToValidate="tbAddressLine1"
                                        runat="server" Dispaly="Dynamic" />
                                </InsertItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblAddressLine1" runat="server" Text='<%# Bind("address_line1") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="address_line2" HeaderText="Address Line 2 " SortExpression="address_line2" />
                            <%--<asp:BoundField DataField="lga_city" HeaderText="City" SortExpression="lga_city" />
                <asp:BoundField DataField="state" HeaderText="State" SortExpression="state" />--%>
                            <%--<asp:BoundField DataField="zip_postal_code" HeaderText="zip postal code" SortExpression="zip_postal_code" />--%>
                            <%-- <asp:BoundField DataField="country" HeaderText="Country" SortExpression="country" />--%>
                            <%-- <asp:BoundField DataField="created_by" HeaderText="Created By" SortExpression="start_date" />--%>
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
                                    &nbsp;<asp:LinkButton ID="LinkButtonDelete" runat="server" CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete ?');" Text=" Delete"></asp:LinkButton>
                                </ItemTemplate>
                                <ControlStyle CssClass="btn btn-primary" Width="80px" />
                                <HeaderStyle Width="80px" />
                            </asp:TemplateField>
                        </Fields>
                        <EmptyDataTemplate>
                            <h2>No Address Found </h2>
                            <asp:Button ID="InsertAddress" runat="server" CommandName="New" Text="New" CssClass="btn btn-sm btn-primary" />
                        </EmptyDataTemplate>
                    </asp:DetailsView>
                </div>
                <div class="col-md-4">
                    <div id="divEndAddress" runat="server" visible="false" style="width: 200px">
                        <div class="form-group">
                            <div class="editor-label">
                                <label>End This Address</label>
                            </div>
                            <div class="">
                                <asp:TextBox ID="tbAddressEndDate" runat="server" rel="date" type="text" CssClass="datepick" placeholder="dd/mm/yyyy"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="rfvtbAddressEndDate" ErrorMessage="Date Required" ForeColor="Red" CssClass="pull-right" ControlToValidate="tbAddressEndDate"
                                    runat="server" Dispaly="Dynamic" />--%>
                            </div>
                        </div>
                        <div>
                            <asp:Button ID="BtnAddressEndDate" runat="server" Text="Submit" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" OnClick="BtnAddressEndDate_Click" OnClientClick="return CheckForm()" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <h1>Previous Address(es)</h1>
        <div>
            <h4>
                <asp:Label ID="lblZeroAddress" runat="server" Text=""></asp:Label>

            </h4>
        </div>

        <asp:GridView ID="GridViewEmployeeAddress" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example">
            <Columns>
                <asp:BoundField DataField="address_line1" HeaderText="Address Line" />
                <asp:BoundField DataField="address_line2" HeaderText="Address Line" />
                <asp:BoundField DataField="lga_city" HeaderText="Lga" />
                <asp:BoundField DataField="state" HeaderText="State" />
                <asp:BoundField DataField="country" HeaderText="Country" />
                <asp:BoundField DataField="start_date" HeaderText="Start Date" />
                <asp:BoundField DataField="end_date" HeaderText="End  Date" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSourceAddress" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
            InsertCommand="sp_ms_hr_employee_address_add" InsertCommandType="StoredProcedure"
            SelectCommand="sp_ms_hr_employee_address_current" SelectCommandType="StoredProcedure"
            UpdateCommand="sp_ms_hr_emp_address_edit" UpdateCommandType="StoredProcedure"
            DeleteCommand="sp_ms_hr_employees_address_delete" DeleteCommandType="StoredProcedure" OnDeleted="SqlDataSourceAddress_Deleted">

            <DeleteParameters>
                <asp:Parameter Name="emp_id" Type="Int32" />
            </DeleteParameters>

            <InsertParameters>
                <asp:QueryStringParameter Name="emp_id" QueryStringField="EmployeeId" Type="Int32" />
                <asp:Parameter Name="address_line1" Type="String" />
                <asp:Parameter Name="address_line2" Type="String" />
                <asp:Parameter Name="lga_city" Type="String" />
                <asp:Parameter Name="state" Type="String" />
                <%-- <asp:Parameter Name="zip_postal_code" Type="String" />--%>
                <asp:Parameter Name="country" Type="String" />
                <asp:Parameter Name="status" Type="String" />
                <asp:Parameter DbType="Date" Name="start_date" />
                <asp:Parameter Name="created_by" Type="String" />
            </InsertParameters>
            <SelectParameters>
                <asp:QueryStringParameter Name="emp_id" QueryStringField="EmployeeId" Type="Int32" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="emp_id" Type="Int32" />
                <asp:Parameter Name="address_line1" Type="String" />
                <asp:Parameter Name="address_line2" Type="String" />
                <asp:Parameter Name="lga_city" Type="String" />
                <%--<asp:Parameter Name="zip_postal_code" Type="String" />--%>
                <asp:Parameter Name="state" Type="String" />
                <asp:Parameter Name="country" Type="String" />
                <asp:Parameter Name="status" Type="String" />
                <asp:Parameter DbType="Date" Name="start_date" />
                <asp:Parameter Name="end_date" DbType="Date" />
                <asp:Parameter Name="updated_by" Type="String" />
                <%-- <asp:Parameter Name="created_by" Type="String" />--%>
            </UpdateParameters>
        </asp:SqlDataSource>
    </div>
    <div id="ViewPayroll" runat="server">
       <div class=" wrapper-content  animated fadeInRight">
        <h1>Current Salary</h1>
        <asp:Label ID="lblNoCurrentSalary" runat="server" Text=""></asp:Label>
        <asp:DetailsView ID="DetailsViewCurrentSalary" runat="server" Height="50px" Width="400px" CssClass="table table-striped table-bordered table-hover dataTables-example" AutoGenerateRows="False">
            <Fields>
                <asp:BoundField DataField="sal_id" HeaderText="SalID" InsertVisible="False" ReadOnly="True" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="amount" HeaderText="Gross Pay" DataFormatString="₦{0:n}"></asp:BoundField>
            </Fields>
        </asp:DetailsView>
        <asp:Button ID="btnSetSalary" runat="server" Text="" CssClass="btn  btn-primary" OnClick="btnSetSalary_Click" />
        <h1>Payroll Summary</h1>
        <div class="">
            <div class="">
                <asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList>
                <asp:Button ID="btnPayrollByYear" runat="server" Text="GO" type="search" CssClass="btn-primary" OnClick="btnPayrollByYear_Click" />
            </div>
        </div>
        <asp:Label ID="lblClickRecords" runat="server" Text=""></asp:Label>
        <asp:GridView ID="GridViewPayroll" runat="server" AutoGenerateColumns="False" DataKeyNames="emp_id" CssClass="table table-striped table-bordered table-hover dataTables-example">
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="emp_id,month,year" DataNavigateUrlFormatString="employee_profile.aspx?EmployeeId={0}&amp;Month={1}&amp;year={2}&amp;action=payroll"
                    DataTextField="month" HeaderText="Month" ControlStyle-CssClass="hyperlink" />
                <asp:BoundField DataField="emp_id" HeaderText="EmpID" InsertVisible="False" ReadOnly="True" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="month" HeaderText="Month" InsertVisible="False" ReadOnly="True" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="year" HeaderText="Year" InsertVisible="False" ReadOnly="True" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="payroll_id" HeaderText="PayrollId" InsertVisible="False" ReadOnly="True" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="basic_pay" HeaderText="Basic Pay" />
                <asp:BoundField DataField="Total Allowances" HeaderText="Total Allowances" />
                <asp:BoundField DataField="salary_at_run" HeaderText="Gross Pay" />
                <asp:BoundField DataField="total_deductions" HeaderText="Total Deductions" />
                <asp:BoundField DataField="Net Pay" HeaderText="Net Pay" />
                <asp:BoundField DataField="total_amount_paid" HeaderText="Total Salary Paid" />
                <asp:BoundField DataField="balance" HeaderText="Balance" />
                <asp:TemplateField HeaderText="Payment">
                    <ItemTemplate>
                        <asp:Button ID="btnPaySalary" runat="server" Text="Pay Salary" OnClick="btnPaySalary_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Payslip">
                    <ItemTemplate>
                        <asp:Button ID="btnViewPayslip" runat="server" Text="View Payslip" CommandArgument='<%#Eval("payroll_id")%>' OnClick="btnViewPayslip_Click" />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
        <h2>Payments</h2>
        <asp:Label ID="lblZeroRecords" runat="server" Text=""></asp:Label>
        <asp:GridView ID="GridViewPayrollBreakDown" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example"
            OnRowDeleting="GridViewPayrollBreakDown_RowDeleting">
            <Columns>
                <asp:BoundField DataField="hr_pay_id" HeaderText="Hr_pay_ID" InsertVisible="False" ReadOnly="True" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="year" HeaderText="Year" />
                <asp:BoundField DataField="month" HeaderText="Month" />
                <asp:BoundField DataField="amount" HeaderText="Amount" />
                <asp:BoundField DataField="payment date" HeaderText="Payment Date" />
                <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDelete" runat="server" ControlStyle-CssClass="hyperlink" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete ?');" Text="Delete"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Label ID="lblZeroBreakDown" runat="server" Text=""></asp:Label>
      </div>
   </div>
    <div id="divSetSalary" runat="server" visible="false">
        <h2>Update Salary and Payroll</h2>
        <div class=" wrapper-content  animated fadeInRight">
            <div class="row">
                <div class="col-lg-6">
                    <div class="ibox">
                        <asp:DetailsView ID="DetailsViewSalaryPayroll" runat="server" CssClass="table table-striped table-bordered table-hover dataTables-example"
                            Width="599px" AutoGenerateRows="False" DataKeyNames="emp_id,sal_id" AutoGenerateColumns="False" DataSourceID="SqlDataSourceSalaryPayroll"
                            OnItemCreated="DetailsViewSalaryPayroll_ItemCreated" OnItemUpdated="DetailsViewSalaryPayroll_ItemUpdated" OnItemCommand="DetailsViewSalaryPayroll_ItemCommand"
                            OnDataBound="DetailsViewSalaryPayroll_DataBound" OnItemInserted="DetailsViewSalaryPayroll_ItemInserted" OnItemUpdating="DetailsViewSalaryPayroll_ItemUpdating" OnItemInserting="DetailsViewSalaryPayroll_ItemInserting">
                            <Fields>
                                <asp:BoundField DataField="sal_id" HeaderText="SalID" InsertVisible="False" ReadOnly="True" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                                    <ItemStyle CssClass="hidden"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="emp_id" HeaderText="EmpID" InsertVisible="False" ReadOnly="True" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                                    <ItemStyle CssClass="hidden"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Gross Pay">
                                    <HeaderTemplate>
                                        Gross Pay<span class="required">*</span>
                                    </HeaderTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbSalary" runat="server" Text='<%# Bind("amount") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtbSalary" ErrorMessage="Salary Required" ForeColor="Red" ControlToValidate="tbSalary"
                                            runat="server" Dispaly="Dynamic" />
                                        <asp:RegularExpressionValidator ID="regularfvtbSalary" ControlToValidate="tbSalary" runat="server"
                                            ErrorMessage="Only Numbers Allowed " ForeColor="Red" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbSalary" runat="server" Text='<%# Bind("amount") %>' TextMode="Number"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtbSalary" ErrorMessage="Salary Required" ForeColor="Red" ControlToValidate="tbSalary"
                                            runat="server" Dispaly="Dynamic" />
                                        <asp:RegularExpressionValidator ID="regularfvtbSalary" ControlToValidate="tbSalary" runat="server"
                                            ErrorMessage="Only Numbers Allowed " ForeColor="Red" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbltbSalary" runat="server" Text='<%# Bind("amount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlBankName" runat="server" Text='<%# Bind("bank") %>' CssClass="input-group" DataValueField="status_name" DataTextField="status_name"
                                            DataSourceID="SqlDataSourceBankDropDown" SelectedValue='<%# Bind("bank") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:DropDownList ID="ddlBankName" runat="server" Text='<%# Bind("bank") %>' CssClass="input-group" DataValueField="status_name" DataTextField="status_name"
                                            DataSourceID="SqlDataSourceBankDropDown" SelectedValue='<%# Bind("bank") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblBank" runat="server" Text='<%# Bind("bank") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank Account Number">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbBankAccountNumber" runat="server" Text='<%# Bind("bank_account_no") %>'></asp:TextBox>
                                       <asp:CustomValidator runat="server" ID="valac" ControlToValidate="tbBankAccountNumber" ForeColor="Red" ErrorMessage="Enter back acc" Display="Dynamic"  ClientValidationFunction="CheckBank" />
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbBankAccountNumber" runat="server" Text='<%# Bind("bank_account_no") %>'></asp:TextBox>
                                        <asp:CustomValidator runat="server" ID="valacc" ControlToValidate="tbBankAccountNumber" ForeColor="Red" ErrorMessage="Enter back acc" Display="Dynamic" ClientValidationFunction="CheckBank" />
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbltbBankAccountNumber" runat="server" Text='<%# Bind("bank_account_no") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Housing Allowance %" SortExpression="housing_allow">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbHousingAllowance" runat="server" Text='<%# Bind("housing_allow") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbHousingAllowance" runat="server" Text='<%# Bind("housing_allow") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbltbHousingAllowance" runat="server" Text='<%# Bind("housing_allow") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Transport Allowance %" SortExpression="transport_allow">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbTransportAllowance" runat="server" Text='<%# Bind("transport_allow") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbTransportAllowance" runat="server" Text='<%# Bind("transport_allow") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbltbTransportAllowance" runat="server" Text='<%# Bind("transport_allow") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Medical Allowance %" SortExpression="medical_allow">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbMedicalAllowance" runat="server" Text='<%# Bind("medical_allow") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbMedicalAllowance" runat="server" Text='<%# Bind("medical_allow") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMedicalAllowance" runat="server" Text='<%# Bind("medical_allow") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Meal Allowance %" SortExpression="meal_allow">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbMealAllowance" runat="server" Text='<%# Bind("meal_allow") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbMealAllowance" runat="server" Text='<%# Bind("meal_allow") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMealAllowance" runat="server" Text='<%# Bind("meal_allow") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Utility Allowance %" SortExpression="utility_allow">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbUtilityAllowance" runat="server" Text='<%# Bind("utility_allow") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbUtilityAllowance" runat="server" Text='<%# Bind("utility_allow") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblUtilityAllowance" runat="server" Text='<%# Bind("utility_allow") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Other Allowance" SortExpression="other_allow">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbOtherAllowance" runat="server" Text='<%# Bind("other_allow") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbOtherAllowance" runat="server" Text='<%# Bind("other_allow") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblOtherAllowance" runat="server" Text='<%# Bind("other_allow") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tax Deduction %" SortExpression="tax_deduct">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbTaxDeduction" runat="server" Text='<%# Bind("tax_deduct") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbTaxDeduction" runat="server" Text='<%# Bind("tax_deduct") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTaxDeduction" runat="server" Text='<%# Bind("tax_deduct") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pension Deduction %" SortExpression="pension_deduct">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbPensionDeduction" runat="server" Text='<%# Bind("pension_deduct") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbPensionDeduction" runat="server" Text='<%# Bind("pension_deduct") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPensionDeduction" runat="server" Text='<%# Bind("pension_deduct") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Welfare Deduction" SortExpression="welfare_deduct">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbWelfareDeduction" runat="server" Text='<%# Bind("welfare_deduct") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbWelfareDeduction" runat="server" Text='<%# Bind("welfare_deduct") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblWelfareDeduction" runat="server" Text='<%# Bind("welfare_deduct") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Co-operative Deduction" SortExpression="coop_deduct">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbCooperativeDeduction" runat="server" Text='<%# Bind("coop_deduct") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbCooperativeDeduction" runat="server" Text='<%# Bind("coop_deduct") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCooperativeDeduction" runat="server" Text='<%# Bind("coop_deduct") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Loan Deduction" SortExpression="loan_deduct">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbLoanDeduction" runat="server" Text='<%# Bind("loan_deduct") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbLoanDeduction" runat="server" Text='<%# Bind("loan_deduct") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbltbLoanDeduction" runat="server" Text='<%# Bind("loan_deduct") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Other  Deduction" SortExpression="other_deduct">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbOtherDeduction" runat="server" Text='<%# Bind("other_deduct") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbOtherDeduction" runat="server" Text='<%# Bind("other_deduct") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblOtherDeduction" runat="server" Text='<%# Bind("other_deduct") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="True" EditText=" Edit" ShowInsertButton="True" InsertText="Save" ShowCancelButton="true" 
                                    ControlStyle-CssClass="btn btn-primary" HeaderStyle-Width="80px" >
                                    <HeaderStyle Width="80px"></HeaderStyle>
                                    <ControlStyle CssClass="btn btn-primary" Width="80px"></ControlStyle>
                                </asp:CommandField>
                            </Fields>
                            <%-- <EmptyDataTemplate>
                                <h1></h1>
                                <asp:Button ID="InsertSalary" runat="server" CommandName="New" InsertText="Save" Text="Click to Add" CssClass="btn btn-sm btn-primary" />
                            </EmptyDataTemplate>--%>
                        </asp:DetailsView>
                        <asp:SqlDataSource ID="SqlDataSourceSalaryPayroll" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
                            InsertCommand="sp_ms_hr_employee_update_salary" InsertCommandType="StoredProcedure"
                            SelectCommand=" sp_ms_hr_employee_salary_payroll" SelectCommandType="StoredProcedure"
                            UpdateCommand="sp_ms_hr_employee_update_salary" UpdateCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="emp_id" QueryStringField="EmployeeId" Type="Int32" />
                            </SelectParameters>
                            <InsertParameters>
                                <asp:Parameter Name="sal_id" Type="Int32" />
                                <asp:QueryStringParameter Name="emp_id" QueryStringField="EmployeeId" Type="Int32" />
                                <asp:Parameter Name="amount" Type="Int32" />
                                <asp:Parameter Name="housing_allow" Type="Decimal" />
                                <asp:Parameter Name="transport_allow" Type="Decimal" />
                                <asp:Parameter Name="medical_allow" Type="Decimal" />
                                <asp:Parameter Name="meal_allow" Type="Decimal" />
                                <asp:Parameter Name="utility_allow" Type="Decimal" />
                                <asp:Parameter Name="other_allow" Type="Decimal" />
                                <asp:Parameter Name="tax_deduct" Type="Decimal" />
                                <asp:Parameter Name="pension_deduct" Type="Decimal" />
                                <asp:Parameter Name="welfare_deduct" Type="Decimal" />
                                <asp:Parameter Name="coop_deduct" Type="Decimal" />
                                <asp:Parameter Name="loan_deduct" Type="Decimal" />
                                <asp:Parameter Name="other_deduct" Type="Decimal" />
                                <asp:Parameter Name="bank_account_no" Type="Int64" />
                                <asp:Parameter Name="bank" Type="String" />

                            </InsertParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="sal_id" Type="Int32" />
                                <asp:Parameter Name="emp_id" Type="Int32" />
                                <asp:Parameter Name="amount" Type="Int32" />
                                <asp:Parameter Name="housing_allow" Type="Decimal" />
                                <asp:Parameter Name="transport_allow" Type="Decimal" />
                                <asp:Parameter Name="medical_allow" Type="Decimal" />
                                <asp:Parameter Name="meal_allow" Type="Decimal" />
                                <asp:Parameter Name="utility_allow" Type="Decimal" />
                                <asp:Parameter Name="other_allow" Type="Decimal" />
                                <asp:Parameter Name="tax_deduct" Type="Decimal" />
                                <asp:Parameter Name="pension_deduct" Type="Decimal" />
                                <asp:Parameter Name="welfare_deduct" Type="Decimal" />
                                <asp:Parameter Name="coop_deduct" Type="Decimal" />
                                <asp:Parameter Name="loan_deduct" Type="Decimal" />
                                <asp:Parameter Name="other_deduct" Type="Decimal" />
                                <asp:Parameter Name="bank_account_no" Type="Int64" />
                                <asp:Parameter Name="bank" Type="String" />
                            </UpdateParameters>
                        </asp:SqlDataSource>
                    </div>
                </div>
                <div class="col-lg-2 col-lg-offset-2">
                    <asp:Button ID="btnfill" runat="server" Text="Set to default payroll" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" CausesValidation="false" OnClick="btnfill_Click" />
                </div>
            </div>
            <%--<asp:Button ID="BtnSalarySave" runat="server" Text="Save" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" OnClick="BtnSalarySave_Click" />--%>
            <%-- <asp:Button ID="btnCancelSave" runat="server" Text="Cancel" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" OnClick="btnCancelSave_Click" CausesValidation="false" />--%>
        </div>
    </div>
    <div id="ViewPayroll2" runat="server">
      <div class=" wrapper-content  animated fadeInRight">
        <h1>Current Salary</h1>
        <asp:Label ID="lblNoCurrentSalary2" runat="server" Text=""></asp:Label>
        <asp:DetailsView ID="DetailsViewCurrentSalary2" runat="server" Height="50px" Width="400px" CssClass="table table-striped table-bordered table-hover dataTables-example" AutoGenerateRows="False">
            <Fields>
                <asp:BoundField DataField="sal_id" HeaderText="SalID" InsertVisible="False" ReadOnly="True" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="amount" HeaderText="Gross Pay" DataFormatString="₦{0:n}"></asp:BoundField>
            </Fields>
        </asp:DetailsView>
        <asp:Button ID="btnSetSalary2" runat="server" Text="" CssClass="btn  btn-primary" OnClick="btnSetSalary2_Click" />
        <h1>Payroll Summary</h1>
        <div>
            <div class="">
                <asp:DropDownList ID="ddlYear2" runat="server"></asp:DropDownList>
                <asp:Button ID="btnPayrollByYear2" runat="server" Text="GO" type="search" CssClass="btn-primary" OnClick="btnPayrollByYear2_Click" />
            </div>
        </div>
        <asp:Label ID="lblClickRecords2" runat="server" Text=""></asp:Label>
        <asp:GridView ID="GridViewPayroll2" runat="server" AutoGenerateColumns="False" DataKeyNames="emp_id" CssClass="table table-striped table-bordered table-hover dataTables-example">
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="emp_id,month,year" DataNavigateUrlFormatString="employee_profile.aspx?EmployeeId={0}&amp;Month={1}&amp;year={2}&amp;action=payroll"
                    DataTextField="month" HeaderText="Month" ControlStyle-CssClass="hyperlink" />
                <asp:BoundField DataField="emp_id" HeaderText="EmpID" InsertVisible="False" ReadOnly="True" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="month" HeaderText="Month" InsertVisible="False" ReadOnly="True" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="year" HeaderText="Year" InsertVisible="False" ReadOnly="True" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="payroll_id" HeaderText="PayrollId" InsertVisible="False" ReadOnly="True" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="basic_pay" HeaderText="Basic Pay" />
                <asp:BoundField DataField="Total Allowances" HeaderText="Total Allowances" />
                <asp:BoundField DataField="salary_at_run" HeaderText="Gross Pay" />
                <asp:BoundField DataField="total_deductions" HeaderText="Total Deductions" />
                <asp:BoundField DataField="Net Pay" HeaderText="Net Pay" />
                <asp:BoundField DataField="total_amount_paid" HeaderText="Total Salary Paid" />
                <asp:BoundField DataField="balance" HeaderText="Balance" />
                <asp:TemplateField HeaderText="Payment">
                    <ItemTemplate>
                        <asp:Button ID="btnPaySalary2" runat="server" Text="Pay Salary" OnClick="btnPaySalary_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Payslip">
                    <ItemTemplate>
                        <asp:Button ID="btnViewPayslip2" runat="server" Text="View Payslip" CommandArgument='<%#Eval("payroll_id")%>' OnClick="btnViewPayslip2_Click" />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
        <h2>Payments</h2>
        <asp:Label ID="lblZeroRecords2" runat="server" Text=""></asp:Label>
        <asp:GridView ID="GridViewPayrollBreakDown2" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example"
            OnRowDeleting="GridViewPayrollBreakDown2_RowDeleting">
            <Columns>
                <asp:BoundField DataField="hr_pay_id" HeaderText="Hr_pay_ID" InsertVisible="False" ReadOnly="True" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="year" HeaderText="Year" />
                <asp:BoundField DataField="month" HeaderText="Month" />
                <asp:BoundField DataField="amount" HeaderText="Amount" />
                <asp:BoundField DataField="payment date" HeaderText="Payment Date" />
                <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDelete" runat="server" ControlStyle-CssClass="hyperlink" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete ?');" Text="Delete"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Label ID="lblZeroBreakDown2" runat="server" Text=""></asp:Label>
      </div>
   </div>
    <div id="divSetSalary2" runat="server" visible="false">
        <h2>Update Salary and Payroll</h2>
        <div class="wrapper  wrapper-content  animated fadeInRight">
            <div class="row">
                <div class="col-lg-6">
                    <div class="ibox">
                        <asp:DetailsView ID="DetailsViewSalaryPayroll2" runat="server" CssClass="table table-striped table-bordered table-hover dataTables-example"
                            Width="599px" AutoGenerateRows="False" DataKeyNames="sal_id" AutoGenerateColumns="False" DataSourceID="SqlDataSourceSalaryPayroll2"
                            OnItemCreated="DetailsViewSalaryPayroll2_ItemCreated" OnItemUpdated="DetailsViewSalaryPayroll2_ItemUpdated" OnItemCommand="DetailsViewSalaryPayroll2_ItemCommand"
                            OnDataBound="DetailsViewSalaryPayroll2_DataBound" OnItemInserted="DetailsViewSalaryPayroll2_ItemInserted" OnItemUpdating="DetailsViewSalaryPayroll2_ItemUpdating" OnItemInserting="DetailsViewSalaryPayroll2_ItemInserting">
                            <Fields>
                                <asp:BoundField DataField="sal_id" HeaderText="sal_id" InsertVisible="False" ReadOnly="True" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" SortExpression="sal_id">
                                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                                    <ItemStyle CssClass="hidden"></ItemStyle>
                                </asp:BoundField>
                               
                                <asp:BoundField DataField="emp_id" HeaderText="emp_id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" SortExpression="emp_id">
                                <HeaderStyle CssClass="hidden"></HeaderStyle>
                                    <ItemStyle CssClass="hidden"></ItemStyle>
                                </asp:BoundField>

                                <asp:TemplateField HeaderText="Salary" SortExpression="amount">
                                     <HeaderTemplate>
                                        Salary<span class="required">*</span>
                                    </HeaderTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbSalary" runat="server" Text='<%# Bind("amount") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtbSalary" ErrorMessage="Salary Required" ForeColor="Red" ControlToValidate="tbSalary"
                                            runat="server" Dispaly="Dynamic" />
                                        <asp:RegularExpressionValidator ID="regularfvtbSalary" ControlToValidate="tbSalary" runat="server"
                                            ErrorMessage="Only Numbers Allowed " ForeColor="Red" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbSalary" runat="server" Text='<%# Bind("amount") %>' TextMode="Number"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtbSalary" ErrorMessage="Salary Required" ForeColor="Red" ControlToValidate="tbSalary"
                                            runat="server" Dispaly="Dynamic" />
                                        <asp:RegularExpressionValidator ID="regularfvtbSalary" ControlToValidate="tbSalary" runat="server"
                                            ErrorMessage="Only Numbers Allowed " ForeColor="Red" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbltbSalary" runat="server" Text='<%# Bind("amount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank Account Number" SortExpression="bank_account_no">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbBankAccountNumber" runat="server" Text='<%# Bind("bank_account_no") %>'></asp:TextBox>
                                       <asp:CustomValidator runat="server" ID="valac" ControlToValidate="tbBankAccountNumber" ForeColor="Red" ErrorMessage="Enter back acc" Display="Dynamic"  ClientValidationFunction="CheckBank" />
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbBankAccountNumber" runat="server" Text='<%# Bind("bank_account_no") %>'></asp:TextBox>
                                        <asp:CustomValidator runat="server" ID="valacc" ControlToValidate="tbBankAccountNumber" ForeColor="Red" ErrorMessage="Enter back acc" Display="Dynamic" ClientValidationFunction="CheckBank" />
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbltbBankAccountNumber" runat="server" Text='<%# Bind("bank_account_no") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank" SortExpression="bank">
                                   <EditItemTemplate>
                                        <asp:DropDownList ID="ddlBankName" runat="server" Text='<%# Bind("bank") %>' CssClass="input-group" DataValueField="status_name" DataTextField="status_name"
                                            DataSourceID="SqlDataSourceBankDropDown" SelectedValue='<%# Bind("bank") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:DropDownList ID="ddlBankName" runat="server" Text='<%# Bind("bank") %>' CssClass="input-group" DataValueField="status_name" DataTextField="status_name"
                                            DataSourceID="SqlDataSourceBankDropDown" SelectedValue='<%# Bind("bank") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblBank" runat="server" Text='<%# Bind("bank") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <%-- <asp:TemplateField HeaderText="Gross Pay" SortExpression="add_gross">
                                     <EditItemTemplate>
                                        <asp:TextBox ID="tbGross" runat="server" Text='<%# Bind("add_gross") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtbGross" ErrorMessage="Gross Required" ForeColor="Red" ControlToValidate="tbGross"
                                            runat="server" Dispaly="Dynamic" />
                                        <asp:RegularExpressionValidator ID="regularfvtbGross" ControlToValidate="tbGross" runat="server"
                                            ErrorMessage="Only Numbers Allowed " ForeColor="Red" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbGross" runat="server" Text='<%# Bind("add_gross") %>' TextMode="Number"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtbGross" ErrorMessage="Gross Required" ForeColor="Red" ControlToValidate="tbGross"
                                            runat="server" Dispaly="Dynamic" />
                                        <asp:RegularExpressionValidator ID="regularfvtbGross" ControlToValidate="tbGross" runat="server"
                                            ErrorMessage="Only Numbers Allowed " ForeColor="Red" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblGross" runat="server" Text='<%# Bind("add_gross") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Basic Salary" SortExpression="add_basic_salary">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbBasic" runat="server" Text='<%# Bind("add_basic_salary") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtbBasic" ErrorMessage="Basic Salary Required" ForeColor="Red" ControlToValidate="tbBasic"
                                            runat="server" Dispaly="Dynamic" />
                                        <asp:RegularExpressionValidator ID="regularfvtbBasic" ControlToValidate="tbBasic" runat="server"
                                            ErrorMessage="Only Numbers Allowed " ForeColor="Red" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbBasic" runat="server" Text='<%# Bind("add_basic_salary") %>' TextMode="Number"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtbBasic" ErrorMessage="Basic Salary Required" ForeColor="Red" ControlToValidate="tbBasic"
                                            runat="server" Dispaly="Dynamic" />
                                        <asp:RegularExpressionValidator ID="regularfvtbBasic" ControlToValidate="tbBasic" runat="server"
                                            ErrorMessage="Only Numbers Allowed " ForeColor="Red" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblBasicSalary" runat="server" Text='<%# Bind("add_basic_salary") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Cost of Living Allowance" SortExpression="add_cost_of_living_allowance">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbCostOfLiving" runat="server" Text='<%# Bind("add_cost_of_living_allowance") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbCostOfLiving" runat="server" Text='<%# Bind("add_cost_of_living_allowance") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("add_cost_of_living_allowance") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cola Allowance" SortExpression="add_cola">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbCola" runat="server" Text='<%# Bind("add_cola") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbCola" runat="server" Text='<%# Bind("add_cola") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("add_cola") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Enhance Allowance" SortExpression="add_enhance">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbEnhance" runat="server" Text='<%# Bind("add_enhance") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbEnhance" runat="server" Text='<%# Bind("add_enhance") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("add_enhance") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Transport Allowance" SortExpression="add_transport">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbTransport" runat="server" Text='<%# Bind("add_transport") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbTransport" runat="server" Text='<%# Bind("add_transport") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("add_transport") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Productivity Allowance" SortExpression="add_productivity">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbProductivity" runat="server" Text='<%# Bind("add_productivity") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbProductivity" runat="server" Text='<%# Bind("add_productivity") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label10" runat="server" Text='<%# Bind("add_productivity") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Responsibility Allowance" SortExpression="add_responsibility">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbResponsibility" runat="server" Text='<%# Bind("add_responsibility") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbResponsibility" runat="server" Text='<%# Bind("add_responsibility") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label11" runat="server" Text='<%# Bind("add_responsibility") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Housing Allowance" SortExpression="add_housing">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbHousing" runat="server" Text='<%# Bind("add_housing") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbHousing" runat="server" Text='<%# Bind("add_housing") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label12" runat="server" Text='<%# Bind("add_housing") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Personalized Allowance" SortExpression="add_personalized">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbPersonalized" runat="server" Text='<%# Bind("add_personalized") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbPersonalized" runat="server" Text='<%# Bind("add_personalized") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label13" runat="server" Text='<%# Bind("add_personalized") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Child Allowance" SortExpression="add_child_allowance">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbChild" runat="server" Text='<%# Bind("add_child_allowance") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbChild" runat="server" Text='<%# Bind("add_child_allowance") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label14" runat="server" Text='<%# Bind("add_child_allowance") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Utility Allowance" SortExpression="add_utility">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbUtility" runat="server" Text='<%# Bind("add_utility") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbUtility" runat="server" Text='<%# Bind("add_utility") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label15" runat="server" Text='<%# Bind("add_utility") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Other Allowance" SortExpression="add_others">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbOtherAllowance" runat="server" Text='<%# Bind("add_others") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbOtherAllowance" runat="server" Text='<%# Bind("add_others") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label16" runat="server" Text='<%# Bind("add_others") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Paye Deduction" SortExpression="deduct_paye">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbPayee" runat="server" Text='<%# Bind("deduct_paye") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbPayee" runat="server" Text='<%# Bind("deduct_paye") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label17" runat="server" Text='<%# Bind("deduct_paye") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tithe Deduction" SortExpression="deduct_tithe">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbTithe" runat="server" Text='<%# Bind("deduct_tithe") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbTithe" runat="server" Text='<%# Bind("deduct_tithe") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label18" runat="server" Text='<%# Bind("deduct_tithe") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pencom Deduction" SortExpression="deduct_pencom">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbPencom" runat="server" Text='<%# Bind("deduct_pencom") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbPencom" runat="server" Text='<%# Bind("deduct_pencom") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label19" runat="server" Text='<%# Bind("deduct_pencom") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cooperative Deduction" SortExpression="deduct_cooperative">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbCooperative" runat="server" Text='<%# Bind("deduct_cooperative") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbCooperative" runat="server" Text='<%# Bind("deduct_cooperative") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label20" runat="server" Text='<%# Bind("deduct_cooperative") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Socials Deduction" SortExpression="deduct_socials">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbSocial" runat="server" Text='<%# Bind("deduct_socials") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbSocial" runat="server" Text='<%# Bind("deduct_socials") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label21" runat="server" Text='<%# Bind("deduct_socials") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Personal Account Deduction" SortExpression="deduct_personal_account">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbPersonalAccount" runat="server" Text='<%# Bind("deduct_personal_account") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbPersonalAccount" runat="server" Text='<%# Bind("deduct_personal_account") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label22" runat="server" Text='<%# Bind("deduct_personal_account") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rent Comeback Deduction" SortExpression="deduct_rent_comeback">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbRent" runat="server" Text='<%# Bind("deduct_rent_comeback") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbRent" runat="server" Text='<%# Bind("deduct_rent_comeback") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label23" runat="server" Text='<%# Bind("deduct_rent_comeback") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Other Deduction" SortExpression="deduct_others">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tbOtherDeductions" runat="server" Text='<%# Bind("deduct_others") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="tbOtherDeductions" runat="server" Text='<%# Bind("deduct_others") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label24" runat="server" Text='<%# Bind("deduct_others") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="True" EditText=" Edit" ShowInsertButton="True" InsertText="Save" ShowCancelButton="true" 
                                    ControlStyle-CssClass="btn btn-primary" HeaderStyle-Width="80px" >
                                    <HeaderStyle Width="80px"></HeaderStyle>
                                    <ControlStyle CssClass="btn btn-primary" Width="80px"></ControlStyle>
                               </asp:CommandField>
                            </Fields>
                        </asp:DetailsView>
                        <asp:SqlDataSource ID="SqlDataSourceSalaryPayroll2" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
                            InsertCommand="sp_ms_hr_bab_employee_update_salary" InsertCommandType="StoredProcedure"
                            SelectCommand=" sp_ms_hr_bab_employee_salary_payroll" SelectCommandType="StoredProcedure"
                            UpdateCommand="sp_ms_hr_bab_employee_update_salary" UpdateCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="emp_id" QueryStringField="EmployeeId" Type="Int32" />
                            </SelectParameters>
                            <InsertParameters>
                                <asp:Parameter Name="sal_id" Type="Int32" />
                                <asp:QueryStringParameter Name="emp_id" QueryStringField="EmployeeId" Type="Int32" />
                                <asp:Parameter Name="amount" Type="Int32" />
                                <asp:Parameter Name="bank_account_no" Type="Int64" />
                                <asp:Parameter Name="bank" Type="String" />

                    <%--            <asp:Parameter Name="add_gross" Type="Decimal" />
                                <asp:Parameter Name="add_basic_salary" Type="Decimal" />--%>
                                <asp:Parameter Name="add_cost_of_living_allowance" Type="Decimal" />
                                <asp:Parameter Name="add_cola" Type="Decimal" />
                                <asp:Parameter Name="add_enhance" Type="Decimal" />
                                <asp:Parameter Name="add_transport" Type="Decimal" />
                                <asp:Parameter Name="add_productivity" Type="Decimal" />
                                <asp:Parameter Name="add_responsibility" Type="Decimal" />
                                <asp:Parameter Name="add_housing" Type="Decimal" />
                                <asp:Parameter Name="add_personalized" Type="Decimal" />
                                <asp:Parameter Name="add_child_allowance" Type="Decimal" />
                                <asp:Parameter Name="add_utility" Type="Decimal" />
                                <asp:Parameter Name="add_others" Type="Decimal" />
                                <asp:Parameter Name="deduct_paye" Type="Decimal" />
                                <asp:Parameter Name="deduct_tithe" Type="Decimal" />
                                <asp:Parameter Name="deduct_pencom" Type="Decimal" />
                                <asp:Parameter Name="deduct_cooperative" Type="Decimal" />
                                <asp:Parameter Name="deduct_socials" Type="Decimal" />
                                <asp:Parameter Name="deduct_personal_account" Type="Decimal" />
                                <asp:Parameter Name="deduct_rent_comeback" Type="Decimal" />
                                <asp:Parameter Name="deduct_others" Type="Decimal" />
                               
                            </InsertParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="sal_id" Type="Int32" />
                                <asp:Parameter Name="emp_id" Type="Int32" />
                                <asp:Parameter Name="amount" Type="Int32" />
                                <asp:Parameter Name="bank_account_no" Type="Int64" />
                                <asp:Parameter Name="bank" Type="String" />
                              <%--  <asp:Parameter Name="add_gross" Type="Decimal" />
                                <asp:Parameter Name="add_basic_salary" Type="Decimal" />--%>
                                <asp:Parameter Name="add_cost_of_living_allowance" Type="Decimal" />
                                <asp:Parameter Name="add_cola" Type="Decimal" />
                                <asp:Parameter Name="add_enhance" Type="Decimal" />
                                <asp:Parameter Name="add_transport" Type="Decimal" />
                                <asp:Parameter Name="add_productivity" Type="Decimal" />
                                <asp:Parameter Name="add_responsibility" Type="Decimal" />
                                <asp:Parameter Name="add_housing" Type="Decimal" />
                                <asp:Parameter Name="add_personalized" Type="Decimal" />
                                <asp:Parameter Name="add_child_allowance" Type="Decimal" />
                                <asp:Parameter Name="add_utility" Type="Decimal" />
                                <asp:Parameter Name="add_others" Type="Decimal" />
                                <asp:Parameter Name="deduct_paye" Type="Decimal" />
                                <asp:Parameter Name="deduct_tithe" Type="Decimal" />
                                <asp:Parameter Name="deduct_pencom" Type="Decimal" />
                                <asp:Parameter Name="deduct_cooperative" Type="Decimal" />
                                <asp:Parameter Name="deduct_socials" Type="Decimal" />
                                <asp:Parameter Name="deduct_personal_account" Type="Decimal" />
                                <asp:Parameter Name="deduct_rent_comeback" Type="Decimal" />
                                <asp:Parameter Name="deduct_others" Type="Decimal" />
                              
                            </UpdateParameters>
                        </asp:SqlDataSource>
                    </div>
                </div>
                <div class="col-lg-2 col-lg-offset-2">
                    <asp:Button ID="btnfill2" runat="server" Text="Set to default payroll" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" CausesValidation="false" OnClick="btnfill2_Click" />
                </div>
            </div>
        </div>
    </div>
    <div id="divQualifications" runat="server" visible="false">
        <asp:Button ID="btnAddShow" runat="server" Text="Add Qualification" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" OnClick="btnAddShow_Click" />
        <div id="divAddQualification" runat="server" visible="false">
            <h2>Add Qualification</h2>
            <div class="row">
                <div class="col-lg-6">
                    <div class="ibox">
                        <div role="form" id="form">
                            <div>
                                <div class="form-group">
                                    <div class="editor-label">
                                        <label>Qualifications</label><span class="required">*</span>
                                    </div>
                                    <div class="editor-field">
                                        <asp:TextBox ID="tbQualification" runat="server" placeholder="Enter Qualification" CssClass=""></asp:TextBox>
                                        <div class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvtbQualification" ErrorMessage="Qualification Required" ForeColor="Red" ControlToValidate="tbQualification"
                                                runat="server" Dispaly="Dynamic" />
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="editor-label">
                                        <label>Date Attained</label><span class="required">*</span>
                                    </div>
                                    <div class="editor-field">
                                        <asp:TextBox ID="tbDateAttained" runat="server" placeholder="dd/mm/yyyy" CssClass="datepick"></asp:TextBox>
                                        <div class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvtbDateAttained" ErrorMessage="Date Attained Required" ForeColor="Red" ControlToValidate="tbDateAttained"
                                                runat="server" Dispaly="Dynamic" />
                                        </div>
                                    </div>
                                </div>

                                <%-- <div class="form-group">
                                    <div class="editor-label">
                                        <label>Created Date</label><span class="required">*</span>
                                    </div>
                                    <div class="editor-field">
                                        <asp:TextBox ID="tbCreatedDate" runat="server" placeholder="dd/mm/yyyy" CssClass="form-control datepick"></asp:TextBox>
                                        <div class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvtbCreatedDate" ErrorMessage="Created Date Required" ForeColor="Red" ControlToValidate="tbCreatedDate"
                                                runat="server" Dispaly="Dynamic" />
                                        </div>
                                    </div>
                                </div>--%>
                                <asp:Button ID="BtnAddQualification" runat="server" Text="Save" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" OnClick="BtnAddQualification_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" OnClick="btnCancel_Click" CausesValidation="false" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <h1>List of Qualifications</h1>
        <asp:GridView ID="GridViewQualifications" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example"
            OnRowEditing="GridViewQualifications_RowEditing"
            OnRowCancelingEdit="GridViewQualifications_RowCancelingEdit"
            OnRowUpdating="GridViewQualifications_RowUpdating"
            OnRowDeleting="GridViewQualifications_RowDeleting">
            <Columns>
                <asp:BoundField DataField="q_id" HeaderText="q_id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" InsertVisible="False" ReadOnly="True" SortExpression="q_id">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="emp_id" HeaderText="emp_id" SortExpression="emp_id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" InsertVisible="False" ReadOnly="True">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="Qualification" SortExpression="qualification">
                    <EditItemTemplate>
                        <asp:TextBox ID="tbQualification" runat="server" Text='<%# Bind("qualification") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvtbQualification" ErrorMessage="Qualification Required" ForeColor="Red" ControlToValidate="tbQualification"
                            runat="server" Dispaly="Dynamic" />
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox ID="tbQualification" runat="server" Text='<%# Bind("qualification") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvtbQualification" ErrorMessage="Qualification Required" ForeColor="Red" ControlToValidate="tbQualification"
                            runat="server" Dispaly="Dynamic" />
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblQualification" runat="server" Text='<%# Bind("qualification") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date Attained">
                    <EditItemTemplate>
                        <asp:TextBox ID="tbDateAttained" runat="server" rel="date" type="text" CssClass="datepick input-group date" Text='<%# Bind("date_attained") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvtbDateAttained" ErrorMessage="Date Attained Required" ForeColor="Red" ControlToValidate="tbDateAttained"
                            runat="server" Dispaly="Dynamic" />
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox ID="tbDateAttained" runat="server" CssClass="datepick" Text='<%# Bind("date_attained") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvtbDateAttained" ErrorMessage="Date Attained Required" ForeColor="Red" ControlToValidate="tbDateAttained"
                            runat="server" Dispaly="Dynamic" />
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDateAttained" runat="server" Text='<%# Bind("date_attained") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--  <asp:TemplateField HeaderText="Created Date">
                   <EditItemTemplate>
                        <asp:TextBox ID="tbCreatedDate" runat="server" rel="date" type="text" CssClass="datepick input-group date" Text='<%# Bind("created_date") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvtbCreatedDate" ErrorMessage="Date Required" ForeColor="Red" ControlToValidate="tbCreatedDate"
                            runat="server" Dispaly="Dynamic" />
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox ID="tbCreatedDate" runat="server" CssClass="datepick" Text='<%# Bind("created_date") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvtbCreatedDate" ErrorMessage="Date Required" ForeColor="Red" ControlToValidate="tbCreatedDate"
                            runat="server" Dispaly="Dynamic" />
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblCreatedDate" runat="server" Text='<%# Bind("created_date") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Edit">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="Edit" CssClass="text-navy" CausesValidation="false" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="lnkUpdate" runat="server" Text="Update" CommandName="Update" CssClass="text-navy" CausesValidation="false" />
                        <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel" CommandName="Cancel" CssClass="text-navy" CausesValidation="false" />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CssClass="text-navy" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete ?');" Text="Delete"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
        <asp:Label ID="lblZeroQualifications" runat="server" Text=""></asp:Label>
    </div>
   

    <%--  --Pages Styles--%>
    <style>
        a {
            color: white;
        }

        .required {
            color: #F00;
        }

        .editor-label .required {
            color: #F00;
        }
    </style>
</asp:Content>
