<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wizard.aspx.cs" Inherits="Alex.pages.wizard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="iQ is a suite of integrated education management applications that facilitates the management of your school." />
    <meta name="description" content="iQ is a suite of integrated education management applications that facilitates the management of your school." />
    <link rel="shortcut icon" href="../images/favicon.png" />
    <title>iQ</title>
    <link href="../scripts/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../scripts/font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link href="../scripts/css/animate.css" rel="stylesheet" />
    <link href="../scripts/css/style.css" rel="stylesheet" />
    <script src="../../scripts/js/jquery-2.1.1.js"></script>
    <script src="../../scripts/js/bootstrap.min.js"></script>
    <script src="../../scripts/js/plugins/metisMenu/jquery.metisMenu.js"></script>
    <script src="../../scripts/js/plugins/slimscroll/jquery.slimscroll.min.js"></script>
    <script src="../../scripts/js/plugins/jeditable/jquery.jeditable.js"></script>
    <link href="../../scripts/css/pagestyle.css" rel="stylesheet" />
    <script src="../../scripts/js/custom.js"></script>
    <script src="../../scripts/js/plugins/pace/pace.min.js"></script>

    <script src="../../scripts/js/plugins/metisMenu/jquery.metisMenu.js"></script>
    <script src="../../scripts/js/plugins/slimscroll/jquery.slimscroll.min.js"></script>






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
        $(function () { $('.datepick').datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); });
        function validate() {

            if (document.getElementById('<%= CheckBox1.ClientID %>').checked == false) {
                alert("Please agree to the terms and conditions");
                return false;
            }

            return true;
        }
        function ValidateCheckBox(sender, args) {
            if (document.getElementById("<%=CheckBox1.ClientID %>").checked == true) {
                args.IsValid = true;
            } else {
                args.IsValid = false;
            }
        }
    </script>



</head>
<body class="gray-bg">
    <form id="form1" runat="server">
        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="col-lg-10 col-lg-offset-4 h1">
                Welcome to iQ, let's get started
                <div class="h4 col-lg-offset-8">

                    <i class="fa fa-sign-out"></i><a href="~/../logout.aspx">Logout</a>

                </div>
            </div>
        </div>
        <asp:SqlDataSource ID="SqlDataSourceAcademicDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
            SelectCommand="SELECT DISTINCT [acad_year] FROM [dbo].[ms_acad_year] ;"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSourceStateDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
            SelectCommand="sp_ms_nigerian_states_dropdown"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSourceCountryDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
            SelectCommand="sp_ms_countries_dropdown"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSourceCityDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
            SelectCommand="sp_ms_nigeria_lagos_state_lga_dropdown"></asp:SqlDataSource>
        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="row">
                <div class="col-lg-12">
                    <div class="float-e-margins">
                        <div class="col-md-12 col-lg-offset-1">
                            <asp:Wizard ID="Wizard1" runat="server" DisplayCancelButton="false" OnCancelButtonClick="Wizard1_CancelButtonClick" OnFinishButtonClick="Wizard1_FinishButtonClick"
                                HeaderText="Setup School Details" ActiveStepIndex="0" CausesValidation="false" OnNextButtonClick="Wizard1_NextButtonClick">
                                <HeaderStyle BackColor="White" BorderColor="#FFCC66" Font-Size="Large" ForeColor="#CC9900" HorizontalAlign="Center" Font-Bold="True" />
                                <SideBarTemplate>
                                    <asp:DataList ID="SideBarList" runat="server" HorizontalAlign="Justify" RepeatDirection="Horizontal">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="SideBarButton" runat="server" ForeColor="green" OnClientClick="return false" CssClass="btn"></asp:LinkButton>
                                        </ItemTemplate>
                                        <SelectedItemStyle Font-Bold="True" CssClass=" yellow-bg" />
                                    </asp:DataList>
                                    </tr><tr>
                                </SideBarTemplate>
                                <StartNavigationTemplate>
                                    <div class="col-md-9">
                                        <asp:Button ID="btnNext" runat="server" CssClass="btn-group btn-success btn-lg" Text="Next" CommandName="MoveNext" CausesValidation="true" />
                                    </div>

                                </StartNavigationTemplate>
                                <StepNavigationTemplate>
                                    <div class="">
                                        <asp:Button ID="btnPrevious" runat="server"
                                            CssClass=" btn-danger btn-group btn-lg"
                                            Text="Previous"
                                            CommandName="MovePrevious" />

                                        <asp:Button ID="btnNext" runat="server"
                                            CssClass="btn-group btn-success btn-lg"
                                            Text="Next"
                                            CommandName="MoveNext"
                                            CausesValidation="true" />
                                    </div>

                                </StepNavigationTemplate>
                                <FinishNavigationTemplate>
                                    <asp:Button ID="btnFinishPrevious" runat="server" Text="Previous" CssClass="btn-group btn-danger btn-lg" CommandName="MovePrevious" CausesValidation="false" />
                                    <asp:Button ID="btnFinishFinish" runat="server" Text="Finish" CssClass="btn-group btn-primary btn-lg" CommandName="MoveComplete" OnClientClick="validate()" />
                                </FinishNavigationTemplate>

                                <%--   <NavigationButtonStyle BackColor="#CCCCCC" BorderColor="#CCFF66" BorderStyle="Ridge" />--%>
                                <WizardSteps>
                                    <asp:WizardStep ID="WizardStep1" runat="server" Title="1.Setup School Details">
                                        <div class="col-lg-12">
                                            <div class="row">
                                                <div class="col-lg-8">
                                                    <asp:DetailsView ID="DetailsViewSchoolDetails" runat="server" AutoGenerateRows="False" DataSourceID="SqlDataSourceSchoolDetails"
                                                        OnItemCreated="DetailsViewSchoolDetails_ItemCreated"
                                                        CssClass="table table-striped table-bordered table-hover dataTables-example" Width="799px">
                                                        <Fields>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    School Name<span class="required">*</span>
                                                                </HeaderTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="tbSchoolName" runat="server" Text='<%# Bind("school_name") %>'></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvSchoolName" ErrorMessage="School Name Required" ForeColor="Red" ControlToValidate="tbSchoolName"
                                                                        runat="server" Dispaly="Dynamic" />
                                                                </EditItemTemplate>
                                                                <InsertItemTemplate>
                                                                    <asp:TextBox ID="tbSchoolName" runat="server" Text='<%# Bind("school_name") %>'></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvSchoolName" ErrorMessage="School Name Required" ForeColor="Red" ControlToValidate="tbSchoolName"
                                                                        runat="server" Dispaly="Dynamic" />
                                                                </InsertItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSchoolName" runat="server" Text='<%# Bind("school_name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Proprietor/Proprietress Title<span class="required">*</span>
                                                                </HeaderTemplate>
                                                                <EditItemTemplate>

                                                                    <asp:DropDownList ID="tbProprietorTitle" runat="server" Text='<%# Bind("proprietor_title") %>'>
                                                                        <asp:ListItem Text="Choose Title" Value=""></asp:ListItem>
                                                                        <asp:ListItem>Mr</asp:ListItem>
                                                                        <asp:ListItem>Mrs</asp:ListItem>
                                                                        <asp:ListItem>Miss</asp:ListItem>
                                                                        <asp:ListItem>Ms</asp:ListItem>
                                                                        <asp:ListItem>Sir</asp:ListItem>
                                                                        <asp:ListItem>Madam</asp:ListItem>
                                                                        <asp:ListItem>Dr</asp:ListItem>
                                                                        <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvProprietorTitle" ErrorMessage="Title Required" ForeColor="Red" ControlToValidate="tbProprietorTitle"
                                                                        runat="server" Dispaly="Dynamic" />
                                                                </EditItemTemplate>
                                                                <InsertItemTemplate>
                                                                    <%-- <asp:TextBox ID="tbProprietorTitle" runat="server" Text='<%# Bind("proprietor_title") %>'></asp:TextBox>--%>
                                                                    <asp:DropDownList ID="tbProprietorTitle" runat="server" Text='<%# Bind("proprietor_title") %>'>
                                                                        <asp:ListItem Text="Choose Title" Value=""></asp:ListItem>
                                                                        <asp:ListItem>Mr</asp:ListItem>
                                                                        <asp:ListItem>Mrs</asp:ListItem>
                                                                        <asp:ListItem>Miss</asp:ListItem>
                                                                        <asp:ListItem>Ms</asp:ListItem>
                                                                        <asp:ListItem>Sir</asp:ListItem>
                                                                        <asp:ListItem>Madam</asp:ListItem>
                                                                        <asp:ListItem>Dr</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvProprietorTitle" ErrorMessage="Title Required" ForeColor="Red" ControlToValidate="tbProprietorTitle"
                                                                        runat="server" Dispaly="Dynamic" />
                                                                </InsertItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblProprietorTitle" runat="server" Text='<%# Bind("proprietor_title") %>'></asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Proprietor/Proprietress First Name<span class="required">*</span>
                                                                </HeaderTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="tbFirstName" runat="server" Text='<%# Bind("proprietor_f_name") %>'></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvFirstName" ErrorMessage="First Name Required" ForeColor="Red" ControlToValidate="tbFirstName"
                                                                        runat="server" Dispaly="Dynamic" />
                                                                </EditItemTemplate>
                                                                <InsertItemTemplate>
                                                                    <asp:TextBox ID="tbFirstName" runat="server" Text='<%# Bind("proprietor_f_name") %>'></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvFirstName" ErrorMessage="First Name Required" ForeColor="Red" ControlToValidate="tbFirstName"
                                                                        runat="server" Dispaly="Dynamic" />
                                                                </InsertItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFirstName" runat="server" Text='<%# Bind("proprietor_f_name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="proprietor_m_name" HeaderText="Proprietor/Proprietress Middle Name" SortExpression="proprietor_m_name" />
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Proprietor/Proprietress Last Name<span class="required">*</span>
                                                                </HeaderTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="tbLastName" runat="server" Text='<%# Bind("proprietor_l_name") %>'></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvLastName" ErrorMessage="Last Name Required" ForeColor="Red" ControlToValidate="tbLastName"
                                                                        runat="server" Dispaly="Dynamic" />
                                                                </EditItemTemplate>
                                                                <InsertItemTemplate>
                                                                    <asp:TextBox ID="tbLastName" runat="server" Text='<%# Bind("proprietor_l_name") %>'></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvLastName" ErrorMessage="Last Name Required" ForeColor="Red" ControlToValidate="tbLastName"
                                                                        runat="server" Dispaly="Dynamic" />
                                                                </InsertItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLastName" runat="server" Text='<%# Bind("proprietor_l_name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Headteacher Title" SortExpression="headteacher_title">
                                                                <EditItemTemplate>
                                                                    <%-- <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("headteacher_title") %>'></asp:TextBox>--%>
                                                                    <asp:DropDownList ID="tbHeadteacherTitle" runat="server" Text='<%# Bind("headteacher_title") %>' AppendDataBoundItems="true" SelectedValue='<%# Bind("headteacher_title") %>'>
                                                                        <asp:ListItem Text="Choose Title" Value=""></asp:ListItem>
                                                                        <asp:ListItem>Mr</asp:ListItem>
                                                                        <asp:ListItem>Mrs</asp:ListItem>
                                                                        <asp:ListItem>Miss</asp:ListItem>
                                                                        <asp:ListItem>Ms</asp:ListItem>
                                                                        <asp:ListItem>Sir</asp:ListItem>
                                                                        <asp:ListItem>Madam</asp:ListItem>
                                                                        <asp:ListItem>Dr</asp:ListItem>
                                                                        <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </EditItemTemplate>
                                                                <InsertItemTemplate>
                                                                    <%--<asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("headteacher_title") %>'></asp:TextBox>--%>
                                                                    <asp:DropDownList ID="tbHeadteacherTitle" runat="server" Text='<%# Bind("headteacher_title") %>'>
                                                                        <asp:ListItem Text="Choose Title" Value=""></asp:ListItem>
                                                                        <asp:ListItem>Mr</asp:ListItem>
                                                                        <asp:ListItem>Mrs</asp:ListItem>
                                                                        <asp:ListItem>Miss</asp:ListItem>
                                                                        <asp:ListItem>Ms</asp:ListItem>
                                                                        <asp:ListItem>Sir</asp:ListItem>
                                                                        <asp:ListItem>Madam</asp:ListItem>
                                                                        <asp:ListItem>Dr</asp:ListItem>

                                                                    </asp:DropDownList>
                                                                </InsertItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("headteacher_title") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="headteacher_f_name" HeaderText="Headteacher First Name" SortExpression="headteacher_f_name" />
                                                            <asp:BoundField DataField="headteacher_l_name" HeaderText="Headteacher Last Name" SortExpression="headteacher_l_name" />
                                                            <asp:TemplateField HeaderText="Deputy Headteacher Title" SortExpression="deputy_headteacher_title">
                                                                <EditItemTemplate>
                                                                    <%-- <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("deputy_headteacher_title") %>'></asp:TextBox>--%>
                                                                    <asp:DropDownList ID="tbDHeadteacherTitle" runat="server" Text='<%# Bind("deputy_headteacher_title") %>' AppendDataBoundItems="true" SelectedValue='<%# Bind("deputy_headteacher_title") %>'>
                                                                        <asp:ListItem Text="Choose Title" Value=""></asp:ListItem>
                                                                        <asp:ListItem>Mr</asp:ListItem>
                                                                        <asp:ListItem>Mrs</asp:ListItem>
                                                                        <asp:ListItem>Miss</asp:ListItem>
                                                                        <asp:ListItem>Ms</asp:ListItem>
                                                                        <asp:ListItem>Sir</asp:ListItem>
                                                                        <asp:ListItem>Madam</asp:ListItem>
                                                                        <asp:ListItem>Dr</asp:ListItem>
                                                                        <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </EditItemTemplate>
                                                                <InsertItemTemplate>
                                                                    <%-- <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("deputy_headteacher_title") %>'></asp:TextBox>--%>
                                                                    <asp:DropDownList ID="tbDHeadteacherTitle" runat="server" Text='<%# Bind("deputy_headteacher_title") %>'>
                                                                        <asp:ListItem Text="Choose Title" Value=""></asp:ListItem>
                                                                        <asp:ListItem>Mr</asp:ListItem>
                                                                        <asp:ListItem>Mrs</asp:ListItem>
                                                                        <asp:ListItem>Miss</asp:ListItem>
                                                                        <asp:ListItem>Ms</asp:ListItem>
                                                                        <asp:ListItem>Sir</asp:ListItem>
                                                                        <asp:ListItem>Madam</asp:ListItem>
                                                                        <asp:ListItem>Dr</asp:ListItem>

                                                                    </asp:DropDownList>
                                                                </InsertItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("deputy_headteacher_title") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="deputy_headteacher_f_name" HeaderText="Deputy Headteacher First Name" SortExpression="deputy_headteacher_f_name" />
                                                            <asp:BoundField DataField="deputy_headteacher_l_name" HeaderText="Deputy Headteacher Last Name" SortExpression="deputy_headteacher_l_name" />
                                                            <asp:TemplateField HeaderText="Established Date" SortExpression="established_date">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="tbEstablishedDate" runat="server" Text='<%# Bind("established_date") %>' CssClass="datepick"></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <InsertItemTemplate>
                                                                    <asp:TextBox ID="tbEstablishedDate" runat="server" Text='<%# Bind("established_date") %>' CssClass="datepick"></asp:TextBox>
                                                                </InsertItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEstablishedDate" runat="server" Text='<%# Bind("established_date") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Address Line1<span class="required">*</span>
                                                                </HeaderTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="tbAddressLine1" runat="server" Text='<%# Bind("address_line1") %>'></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvAddressLine1" ErrorMessage="Address Required" ForeColor="Red" ControlToValidate="tbAddressLine1"
                                                                        runat="server" Dispaly="Dynamic" />
                                                                </EditItemTemplate>
                                                                <InsertItemTemplate>
                                                                    <asp:TextBox ID="tbAddressLine1" runat="server" Text='<%# Bind("address_line1") %>'></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvAddressLine1" ErrorMessage="Address Required" ForeColor="Red" ControlToValidate="tbAddressLine1"
                                                                        runat="server" Dispaly="Dynamic" />
                                                                </InsertItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("address_line1") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="address_line2" HeaderText="Address Line2" SortExpression="address_line2" />
                                                            <asp:BoundField DataField="address_line3" HeaderText="Address Line3" SortExpression="address_line3" />
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    LGA<span class="required">*</span>
                                                                </HeaderTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddlCity" runat="server" DataValueField="lga" DataTextField="lga" Text='<%# Bind("lga_city") %>' DataSourceID="SqlDataSourceCityDropDown"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvddlCity" ErrorMessage="LGA Required" ForeColor="Red" ControlToValidate="ddlCity"
                                                                        runat="server" Dispaly="Dynamic" />
                                                                </EditItemTemplate>
                                                                <InsertItemTemplate>
                                                                    <asp:DropDownList ID="ddlCity" runat="server" DataValueField="lga" DataTextField="lga" Text='<%# Bind("lga_city") %>' DataSourceID="SqlDataSourceCityDropDown"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvddlCity" ErrorMessage="LGA Required" ForeColor="Red" ControlToValidate="ddlCity"
                                                                        runat="server" Dispaly="Dynamic" />
                                                                </InsertItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLga" runat="server" Text='<%# Bind("lga_city") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    State<span class="required">*</span>
                                                                </HeaderTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddlState" runat="server" DataValueField="state" DataTextField="state" Text='<%# Bind("state") %>' DataSourceID="SqlDataSourceStateDropDown"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvtbState" ErrorMessage="State Required" ForeColor="Red" ControlToValidate="ddlState"
                                                                        runat="server" Dispaly="Dynamic" />
                                                                </EditItemTemplate>
                                                                <InsertItemTemplate>
                                                                    <asp:DropDownList ID="ddlState" runat="server" DataValueField="state" DataTextField="state" Text='<%# Bind("state") %>' DataSourceID="SqlDataSourceStateDropDown"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvtbState" ErrorMessage="State Required" ForeColor="Red" ControlToValidate="ddlState"
                                                                        runat="server" Dispaly="Dynamic" />
                                                                </InsertItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblState" runat="server" Text='<%# Bind("state") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Country<span class="required">*</span>
                                                                </HeaderTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddlAddresCountry" runat="server" DataValueField="country" DataTextField="country" Text='<%# Bind("country") %>' DataSourceID="SqlDataSourceCountryDropDown"></asp:DropDownList>

                                                                    <asp:RequiredFieldValidator ID="rfvddlAddressCountry" ErrorMessage="Country Required" ForeColor="Red" ControlToValidate="ddlAddresCountry"
                                                                        runat="server" Dispaly="Dynamic" />
                                                                </EditItemTemplate>
                                                                <InsertItemTemplate>
                                                                    <asp:DropDownList ID="ddlAddresCountry" runat="server" DataValueField="country" DataTextField="country" Text='<%# Bind("country") %>' DataSourceID="SqlDataSourceCountryDropDown"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvddlAddressCountry" ErrorMessage="Country Required" ForeColor="Red" ControlToValidate="ddlAddresCountry"
                                                                        runat="server" Dispaly="Dynamic" />
                                                                </InsertItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblState" runat="server" Text='<%# Bind("country") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="website_url" HeaderText="Website URL" SortExpression="website_url" />
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Contact Email<span class="required">*</span>
                                                                </HeaderTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="tbContactEmail" runat="server" Text='<%# Bind("contact_email") %>'></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvContactEmail" ErrorMessage="Contact Email Required" ForeColor="Red" ControlToValidate="tbContactEmail"
                                                                        runat="server" Dispaly="Dynamic" />
                                                                    <asp:RegularExpressionValidator ID="revtbContactEmail" runat="server"
                                                                        ErrorMessage="Invalid Email" ControlToValidate="tbContactEmail" ForeColor="Red"
                                                                        SetFocusOnError="True" ValidationExpression="^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$" />
                                                                </EditItemTemplate>
                                                                <InsertItemTemplate>
                                                                    <asp:TextBox ID="tbContactEmail" runat="server" Text='<%# Bind("contact_email") %>'></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvContactEmail" ErrorMessage="Contact Email Required" ForeColor="Red" ControlToValidate="tbContactEmail"
                                                                        runat="server" Dispaly="Dynamic" />
                                                                    <asp:RegularExpressionValidator ID="revtbContactEmail" runat="server"
                                                                        ErrorMessage="Invalid Email" ControlToValidate="tbContactEmail" ForeColor="Red"
                                                                        SetFocusOnError="True" ValidationExpression="^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$" />
                                                                </InsertItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblContactEmail" runat="server" Text='<%# Bind("contact_email") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    School Email<span class="required">*</span>
                                                                </HeaderTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="tbEmail" runat="server" Text='<%# Bind("email_add") %>'></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvEmail" ErrorMessage="Email Required" ForeColor="Red" ControlToValidate="tbEmail"
                                                                        runat="server" Dispaly="Dynamic" />
                                                                    <asp:RegularExpressionValidator ID="revtbEmail" runat="server"
                                                                        ErrorMessage="Invalid Email" ControlToValidate="tbEmail" ForeColor="Red"
                                                                        SetFocusOnError="True" ValidationExpression="^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$" />
                                                                </EditItemTemplate>
                                                                <InsertItemTemplate>
                                                                    <asp:TextBox ID="tbEmail" runat="server" Text='<%# Bind("email_add") %>'></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvEmail" ErrorMessage="Email Required" ForeColor="Red" ControlToValidate="tbEmail"
                                                                        runat="server" Dispaly="Dynamic" />
                                                                    <asp:RegularExpressionValidator ID="revtbEmail" runat="server"
                                                                        ErrorMessage="Invalid Email" ControlToValidate="tbEmail" ForeColor="Red"
                                                                        SetFocusOnError="True" ValidationExpression="^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$" />
                                                                </InsertItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("email_add") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Contact Number1<span class="required">*</span>
                                                                </HeaderTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="tbContactNo1" runat="server" Text='<%# Bind("contact_no1") %>'></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvContactNo1" ErrorMessage="Contact No Required" ForeColor="Red" ControlToValidate="tbContactNo1"
                                                                        runat="server" Dispaly="Dynamic" />
                                                                    <asp:RegularExpressionValidator ID="revtbContactNo1" ControlToValidate="tbContactNo1" runat="server"
                                                                        ErrorMessage="Phone Number should be in 11 digits" ForeColor="Red" ValidationExpression="\d{11}"></asp:RegularExpressionValidator>
                                                                </EditItemTemplate>
                                                                <InsertItemTemplate>
                                                                    <asp:TextBox ID="tbContactNo1" runat="server" Text='<%# Bind("contact_no1") %>'></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvContactNo1" ErrorMessage="Contact No Required" ForeColor="Red" ControlToValidate="tbContactNo1"
                                                                        runat="server" Dispaly="Dynamic" />
                                                                    <asp:RegularExpressionValidator ID="revtbContactNo1" ControlToValidate="tbContactNo1" runat="server"
                                                                        ErrorMessage="Phone Number should be in 11 digits" ForeColor="Red" ValidationExpression="\d{11}"></asp:RegularExpressionValidator>
                                                                </InsertItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("contact_no1") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Contact Number2" SortExpression="contact_no2">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="tbContactNo2" runat="server" Text='<%# Bind("contact_no2") %>'></asp:TextBox>
                                                                    <asp:RegularExpressionValidator ID="revtbContactNo2" ControlToValidate="tbContactNo2" runat="server"
                                                                        ErrorMessage="Phone Number should be in 11 digits" ForeColor="Red" ValidationExpression="\d{11}"></asp:RegularExpressionValidator>
                                                                </EditItemTemplate>
                                                                <InsertItemTemplate>
                                                                    <asp:TextBox ID="tbContactNo2" runat="server" Text='<%# Bind("contact_no2") %>'></asp:TextBox>
                                                                    <asp:RegularExpressionValidator ID="revtbContactNo2" ControlToValidate="tbContactNo2" runat="server"
                                                                        ErrorMessage="Phone Number should be in 11 digits" ForeColor="Red" ValidationExpression="\d{11}"></asp:RegularExpressionValidator>
                                                                </InsertItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("contact_no2") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:CommandField ShowEditButton="True" EditText=" Edit" ShowInsertButton="True" InsertText="Save" ControlStyle-CssClass="btn btn-primary" HeaderStyle-Width="80px">
                                                                <HeaderStyle Width="80px"></HeaderStyle>
                                                                <ControlStyle CssClass="btn btn-primary" Width="80px"></ControlStyle>
                                                            </asp:CommandField>
                                                        </Fields>
                                                        <EmptyDataTemplate>
                                                            <h1>Setup School</h1>
                                                            <asp:Button ID="InsertSchoolDetails" runat="server" CommandName="New" InsertText="Save" Text="Setup School Now" CssClass="btn btn-sm btn-primary" />
                                                        </EmptyDataTemplate>

                                                    </asp:DetailsView>

                                                    <asp:SqlDataSource ID="SqlDataSourceSchoolDetails" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
                                                        InsertCommand="sp_ms_settings_school_add" InsertCommandType="StoredProcedure"
                                                        SelectCommand="sp_ms_settings_school_detail" SelectCommandType="StoredProcedure"
                                                        UpdateCommand="sp_ms_settings_school_edit" UpdateCommandType="StoredProcedure">
                                                        <InsertParameters>
                                                            <asp:Parameter Name="school_name" Type="String" />
                                                            <asp:Parameter Name="proprietor_title" Type="String" />
                                                            <asp:Parameter Name="proprietor_f_name" Type="String" />
                                                            <asp:Parameter Name="proprietor_m_name" Type="String" />
                                                            <asp:Parameter Name="proprietor_l_name" Type="String" />
                                                            <asp:Parameter Name="headteacher_title" Type="String" />
                                                            <asp:Parameter Name="headteacher_f_name" Type="String" />
                                                            <asp:Parameter Name="headteacher_l_name" Type="String" />
                                                            <asp:Parameter Name="deputy_headteacher_title" Type="String" />
                                                            <asp:Parameter Name="deputy_headteacher_f_name" Type="String" />
                                                            <asp:Parameter Name="deputy_headteacher_l_name" Type="String" />
                                                            <asp:Parameter DbType="String" Name="established_date" />
                                                            <asp:Parameter Name="address_line1" Type="String" />
                                                            <asp:Parameter Name="address_line2" Type="String" />
                                                            <asp:Parameter Name="address_line3" Type="String" />
                                                            <asp:Parameter Name="lga_city" Type="String" />
                                                            <asp:Parameter Name="state" Type="String" />
                                                            <asp:Parameter Name="country" Type="String" />
                                                            <asp:Parameter Name="zip_postal_code" Type="String" />
                                                            <asp:Parameter Name="website_url" Type="String" />
                                                            <asp:Parameter Name="email_add" Type="String" />
                                                            <asp:Parameter Name="contact_email" Type="String" />
                                                            <asp:Parameter Name="contact_no1" Type="String" />
                                                            <asp:Parameter Name="contact_no2" Type="String" />
                                                            <asp:Parameter Name="licence_id" Type="String" />
                                                            <asp:Parameter Name="created_by" Type="String" />
                                                        </InsertParameters>
                                                        <UpdateParameters>
                                                            <asp:Parameter Name="school_name" Type="String" />
                                                            <asp:Parameter Name="proprietor_title" Type="String" />
                                                            <asp:Parameter Name="proprietor_f_name" Type="String" />
                                                            <asp:Parameter Name="proprietor_m_name" Type="String" />
                                                            <asp:Parameter Name="proprietor_l_name" Type="String" />
                                                            <asp:Parameter Name="headteacher_title" Type="String" />
                                                            <asp:Parameter Name="headteacher_f_name" Type="String" />
                                                            <asp:Parameter Name="headteacher_l_name" Type="String" />
                                                            <asp:Parameter Name="deputy_headteacher_title" Type="String" />
                                                            <asp:Parameter Name="deputy_headteacher_f_name" Type="String" />
                                                            <asp:Parameter Name="deputy_headteacher_l_name" Type="String" />
                                                            <asp:Parameter DbType="String" Name="established_date" />
                                                            <asp:Parameter Name="address_line1" Type="String" />
                                                            <asp:Parameter Name="address_line2" Type="String" />
                                                            <asp:Parameter Name="address_line3" Type="String" />
                                                            <asp:Parameter Name="lga_city" Type="String" />
                                                            <asp:Parameter Name="state" Type="String" />
                                                            <asp:Parameter Name="country" Type="String" />
                                                            <asp:Parameter Name="zip_postal_code" Type="String" />
                                                            <asp:Parameter Name="website_url" Type="String" />
                                                            <asp:Parameter Name="email_add" Type="String" />
                                                            <asp:Parameter Name="contact_email" Type="String" />
                                                            <asp:Parameter Name="contact_no1" Type="String" />
                                                            <asp:Parameter Name="contact_no2" Type="String" />
                                                            <asp:Parameter Name="updated_by" Type="String" />
                                                        </UpdateParameters>
                                                    </asp:SqlDataSource>
                                                </div>
                                                <div class=" col-md-2">
                                                    <div class="wrapper wrapper-content animated fadeInUp">
                                                        <ul class="notes">
                                                            <li>
                                                                <div>
                                                                    <small>School Details</small>
                                                                    <%--<h4>Where a * is present is a required field and you will not be able to proceed without it.</h4>--%>
                                                                    <h4>School details is the first step which needs to be filled in before proceeding, add all your information and then hit save and then proceed by clicking next. </h4>
                                                                </div>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </asp:WizardStep>
                                    <asp:WizardStep ID="WizardStep2" runat="server" Title="2.Setup Academic Year">

                                        <div class=" wrapper-content  animated fadeInRight">
                                            <div class="row" id="divAcademicYear" runat="server">
                                                <div class="col-lg-8">
                                                    <div class="ibox">
                                                        <div role="form" id="">
                                                            <div>
                                                                <div class="form-group">
                                                                    <div class="editor-label">
                                                                        <label>Academic Year</label><span class="required">*</span>
                                                                    </div>
                                                                    <div class="editor-field">
                                                                        <asp:TextBox ID="tbAcademicYear" runat="server" placeholder="Enter Academic year" CssClass="form-control" ValidationGroup="AcademicYear"></asp:TextBox>
                                                                        <div class="pull-right">
                                                                            <asp:RequiredFieldValidator ID="rfvAcademicYear" ErrorMessage=" Academic Year Required" ForeColor="Red" ControlToValidate="tbAcademicYear" ValidationGroup="AcademicYear"
                                                                                runat="server" Dispaly="Dynamic" />
                                                                        </div>
                                                                    </div>

                                                                </div>

                                                                <div class="form-group">
                                                                    <div class="editor-label">
                                                                        <label>Academic Start Date</label><span class="required">*</span>
                                                                    </div>
                                                                    <div class="editor-field">
                                                                        <asp:TextBox ID="tbAcademicStartDate" runat="server" placeholder="dd/mm/yyyy" ValidationGroup="AcademicYear" CssClass="form-control datepick"></asp:TextBox>
                                                                        <div class="pull-right">
                                                                            <asp:RequiredFieldValidator ID="rfvAcademicStartDate" ErrorMessage=" Academic Start Date Required" ValidationGroup="AcademicYear"
                                                                                ForeColor="Red" ControlToValidate="tbAcademicStartDate"
                                                                                runat="server" Dispaly="Dynamic" />
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group">
                                                                    <div class="editor-label">
                                                                        <label>Academic End Date</label><span class="required">*</span>
                                                                    </div>
                                                                    <div class="editor-field">
                                                                        <asp:TextBox ID="tbAcademicEndDate" runat="server" placeholder="dd/mm/yyyy" ValidationGroup="AcademicYear" CssClass="form-control datepick"></asp:TextBox>
                                                                        <div class="pull-right">
                                                                            <asp:RequiredFieldValidator ID="rfvAcademicEndDate" ValidationGroup="AcademicYear" ErrorMessage=" Academic End Date Required"
                                                                                ForeColor="Red" ControlToValidate="tbAcademicEndDate"
                                                                                runat="server" Dispaly="Dynamic" />
                                                                        </div>
                                                                    </div>
                                                                </div>


                                                                <asp:Button ID="BtnSaveAcademicYear" runat="server" Text="Save" ValidationGroup="AcademicYear" CssClass="btn btn-sm btn-primary m-t-n-xs"
                                                                    Font-Bold="True" OnClick="BtnSaveAcademicYear_Click" />

                                                            </div>


                                                        </div>
                                                    </div>

                                                </div>
                                                <div class=" col-md-2">
                                                    <div class="wrapper wrapper-content animated fadeInUp">
                                                        <ul class="notes">
                                                            <li>
                                                                <div>
                                                                    <small>Academic Year</small>
                                                                    <h4>You need to fill this step in correctly, failing to do so will not allow you to proceed correctly.</h4>
                                                                    <p>Please setup your academic year<strong>(eg:2015/16 or 2015/2016)</strong> and both start &amp end date, hit save and then proceed by clicking next.</p>
                                                                </div>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                            <h1>List of Academic Years</h1>
                                            <asp:GridView ID="GridViewAcademicYear" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example"
                                                OnRowEditing="GridViewAcademicYear_RowEditing"
                                                OnRowCancelingEdit="GridViewAcademicYear_RowCancelingEdit"
                                                OnRowUpdating="GridViewAcademicYear_RowUpdating"
                                                OnRowDeleting="GridViewAcademicYear_RowDeleting">
                                                <Columns>
                                                    <asp:BoundField DataField="acad_year_id" HeaderText="AcademicYearId" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                        <HeaderStyle CssClass="hidden"></HeaderStyle>
                                                        <ItemStyle CssClass="hidden"></ItemStyle>
                                                    </asp:BoundField>

                                                    <asp:TemplateField HeaderText="Academic Year">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="tbEditAcademicYear" runat="server" Text='<%# Bind("acad_year") %>'></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtbEditAcademicYear" ErrorMessage="Academic Year Required" ForeColor="Red" ControlToValidate="tbEditAcademicYear"
                                                                runat="server" Dispaly="Dynamic" />
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("acad_year") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Start Date">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="tbAcademicYearSD" runat="server" Text='<%# Bind("acad_y_start_date") %>' CssClass="datepick input-group date"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtbAcademicYearSD" ErrorMessage="Date Required" ForeColor="Red" ControlToValidate="tbAcademicYearSD"
                                                                runat="server" Dispaly="Dynamic" />
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("acad_y_start_date") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="End Date ">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="tbAcademicYearED" runat="server" Text='<%# Bind("acad_y_end_date") %>' CssClass="datepick input-group date"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtbAcademicYearED" ErrorMessage="Date Required" ForeColor="Red" ControlToValidate="tbAcademicYearED"
                                                                runat="server" Dispaly="Dynamic" />
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAcademicYearED" runat="server" Text='<%# Bind("acad_y_end_date") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="status" HeaderText="Status" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                        <HeaderStyle CssClass="hidden"></HeaderStyle>
                                                        <ItemStyle CssClass="hidden"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="Edit" />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:LinkButton ID="lnkUpdate" runat="server" Text="Update" CommandName="Update" />
                                                            <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel" CommandName="Cancel" CausesValidation="false" />
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete ?');" Text="Delete"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                </Columns>
                                            </asp:GridView>
                                            <asp:Label ID="lblZeroRecords" runat="server" Text=""></asp:Label>
                                        </div>

                                    </asp:WizardStep>
                                    <asp:WizardStep ID="WizardStep3" runat="server" Title="3.Setup Academic Term">
                                        <div class=" wrapper-content  animated fadeInRight">
                                            <div class="row" id="divTerms" runat="server">
                                                <div class="col-lg-8">
                                                    <div class="ibox">
                                                        <div role="form" id="">
                                                            <div class="form-group">
                                                                <div class="editor-label">
                                                                    <label>Select Academic Year</label><span class="required">*</span>
                                                                </div>
                                                                <div class="editor-field">
                                                                    <asp:DropDownList ID="ddlTermAcademicYear" runat="server" ValidationGroup="AcademicTerm" CssClass="form-control">
                                                                    </asp:DropDownList>
                                                                    <div class="pull-right">
                                                                        <asp:RequiredFieldValidator ID="rfvddlTermAcademicYear" ErrorMessage=" Academic Year Required" ValidationGroup="AcademicTerm"
                                                                            ForeColor="Red" ControlToValidate="ddlTermAcademicYear"
                                                                            runat="server" Dispaly="Dynamic" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="editor-label">
                                                                    <label>Term</label><span class="required">*</span>
                                                                </div>
                                                                <div class="editor-field">
                                                                    <asp:DropDownList runat="server" ID="ddlTermName" CssClass="form-control" ValidationGroup="AcademicTerm">
                                                                        <asp:ListItem Value="1st" Text="1st"></asp:ListItem>
                                                                        <asp:ListItem Value="2nd" Text="2nd"></asp:ListItem>
                                                                        <asp:ListItem Value="3rd" Text="3rd"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <div class="pull-right">
                                                                        <asp:RequiredFieldValidator ID="rfvtbTermName" ErrorMessage="Term Required" ForeColor="Red" ControlToValidate="ddlTermName"
                                                                            runat="server" Dispaly="Dynamic" ValidationGroup="AcademicTerm" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="editor-label">
                                                                    <label>Start Date</label><span class="required">*</span>
                                                                </div>
                                                                <div class="editor-field">
                                                                    <asp:TextBox ID="tbStartDate" runat="server" placeholder="dd/mm/yyyy" CssClass="form-control datepick" ValidationGroup="AcademicTerm"></asp:TextBox>
                                                                    <div class="pull-right">
                                                                        <asp:RequiredFieldValidator ID="rfvtbStartDate" ErrorMessage="Start Required" ForeColor="Red" ControlToValidate="tbStartDate"
                                                                            runat="server" Dispaly="Dynamic" ValidationGroup="AcademicTerm" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="editor-label">
                                                                    <label>End date</label><span class="required">*</span>
                                                                </div>
                                                                <div class="editor-field">
                                                                    <asp:TextBox ID="tbEndDate" runat="server" placeholder="dd/mm/yyyy" CssClass="form-control datepick" ValidationGroup="AcademicTerm"></asp:TextBox>
                                                                    <div class="pull-right">
                                                                        <asp:RequiredFieldValidator ID="rfvtbEndDate" ErrorMessage="End Required" ForeColor="Red" ControlToValidate="tbEndDate"
                                                                            runat="server" Dispaly="Dynamic" ValidationGroup="AcademicTerm" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <asp:Button ID="BtnSaveTerm" runat="server" Text="Save" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" OnClick="BtnSaveTerm_Click" ValidationGroup="AcademicTerm" />
                                                    </div>

                                                </div>
                                                <div class=" col-md-2">
                                                    <div class="wrapper wrapper-content animated fadeInUp">
                                                        <ul class="notes">
                                                            <li>
                                                                <div>
                                                                    <small>Academic Term</small>
                                                                    <h4>If your academic year does not appear in the drop down you haven't set up step two correctly, please click previous to go back.</h4>
                                                                    <p>Select your academic year, input your term name<strong>(e.g. 1 or Term1)</strong>, start and end date.Once this information has been entered hit save and proceed by clicking next. </p>
                                                                </div>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                            <h1>Term</h1>
                                            <asp:GridView ID="GridViewTerm" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example"
                                                OnRowEditing="GridViewTerm_RowEditing" OnRowCancelingEdit="GridViewTerm_RowCancelingEdit"
                                                OnRowUpdating="GridViewTerm_RowUpdating" OnRowDeleting="GridViewTerm_RowDeleting">
                                                <Columns>
                                                    <asp:BoundField DataField="ay_term_id" HeaderText="Term ID" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                        <HeaderStyle CssClass="hidden"></HeaderStyle>
                                                        <ItemStyle CssClass="hidden"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="acad_year" HeaderText="Academic Year " ReadOnly="true" />
                                                    <asp:TemplateField HeaderText="Term">
                                                        <EditItemTemplate>
                                                            <asp:DropDownList runat="server" ID="ddlEditTermName"  SelectedValue='<%# Bind("term_name") %>'>
                                                                <asp:ListItem Value="1st" Text="1st"></asp:ListItem>
                                                                <asp:ListItem Value="2nd" Text="2nd"></asp:ListItem>
                                                                <asp:ListItem Value="3rd" Text="3rd"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvtbTerm" ErrorMessage="Term Required" ForeColor="Red" ControlToValidate="ddlEditTermName"
                                                                runat="server" Dispaly="Dynamic" />
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTerm" runat="server" Text='<%# Bind("term_name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Start Date">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="tbTermStartDate" runat="server" Text='<%# Bind("ay_term_start_date") %>' CssClass="datepick"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtbTermStartDate" ErrorMessage="Date Required" ForeColor="Red" ControlToValidate="tbTermStartDate"
                                                                runat="server" Dispaly="Dynamic" />
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTermStartDate" runat="server" Text='<%# Bind("ay_term_start_date") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="End Date ">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="tbTermEndDate" runat="server" Text='<%# Bind("ay_term_end_date") %>' CssClass="datepick"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtbTermEndDate" ErrorMessage="Date Required" ForeColor="Red" ControlToValidate="tbTermEndDate"
                                                                runat="server" Dispaly="Dynamic" />
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTermEndDate" runat="server" Text='<%# Bind("ay_term_end_date") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="status" HeaderText="Status" ReadOnly="true"></asp:BoundField>
                                                    <asp:TemplateField ShowHeader="false" HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="Edit" />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:LinkButton ID="lnkUpdate" runat="server" Text="Update" CommandName="Update" />
                                                            <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel" CommandName="Cancel" CausesValidation="False" />
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete ?');" Text="Delete"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                            <asp:Label ID="lblZeroTerm" runat="server" Text=""></asp:Label>
                                        </div>

                                    </asp:WizardStep>
                                    <asp:WizardStep ID="WizardStep4" runat="server" Title="4.Setup Class">
                                        <div style="width: 799px;">
                                            <div class="col-lg-8">
                                                <div class="ibox">
                                                    <div role="form">
                                                        <br />
                                                        <div class="form-group">
                                                            <div class="editor-label">
                                                                <label>Class</label><span class="required">*</span>
                                                            </div>
                                                            <div class="editor-field">
                                                                <asp:TextBox ID="tbSetupForm" runat="server" placeholder="Enter Class" CssClass="form-control" ValidationGroup="ClassGroup"></asp:TextBox>
                                                                <div class="pull-right">
                                                                    <asp:RequiredFieldValidator ID="rfvtbSetupForm" ErrorMessage="Class Required" ForeColor="Red" ControlToValidate="tbSetupForm"
                                                                        runat="server" Dispaly="Dynamic" ValidationGroup="ClassGroup" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="editor-label">
                                                                <label>Section</label><span class="required">*</span>
                                                            </div>
                                                            <div class="editor-field">
                                                                <asp:DropDownList runat="server" ID="ddlSection" ValidationGroup="ClassGroup" CssClass="form-control"></asp:DropDownList>
                                                                <div class="pull-right">
                                                                    <asp:RequiredFieldValidator ID="rfvtbSection" ErrorMessage="Section Required" ValidationGroup="ClassGroup" ForeColor="Red" ControlToValidate="ddlSection"
                                                                        runat="server" Dispaly="Dynamic" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <asp:Button ID="BtnSaveSetupForm" runat="server" Text="Save" CssClass="btn btn-sm btn-primary m-t-n-xs" ValidationGroup="ClassGroup" Font-Bold="True" OnClick="BtnSaveSetupForm_Click" />
                                                    </div>
                                                </div>
                                                <h1>List of Class </h1>

                                                <asp:SqlDataSource ID="SqlDataSourceSectionDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>" SelectCommand="sp_ms_section_dropdown"></asp:SqlDataSource>
                                                <asp:GridView ID="GridViewForm" runat="server" AutoGenerateColumns="False"
                                                    Visible="False" CssClass="table table-striped table-bordered table-hover dataTables-example"
                                                    OnRowEditing="GridViewForm_RowEditing" OnRowCancelingEdit="GridViewForm_RowCancelingEdit"
                                                    OnRowUpdating="GridViewForm_RowUpdating"
                                                    OnRowDeleting="GridViewForm_RowDeleting">

                                                    <Columns>
                                                        <asp:BoundField DataField="form_id" HeaderText="Form Id" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                            <HeaderStyle CssClass="hidden"></HeaderStyle>
                                                            <ItemStyle CssClass="hidden"></ItemStyle>
                                                        </asp:BoundField>
                                                        <%--<asp:BoundField DataField="form_name" HeaderText="Form Name" />--%>
                                                        <asp:TemplateField HeaderText="Class">
                                                            <ItemTemplate>
                                                                <%#Eval("form_name")%>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtValue" runat="server" Text='<%# Eval("form_name") %>'></asp:TextBox>
                                                                <div class="">
                                                                    <asp:RequiredFieldValidator ID="rfvtxtValue" ErrorMessage="Class Required" ForeColor="Red" ControlToValidate="txtValue"
                                                                        runat="server" Dispaly="Dynamic" />
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Section">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSection" runat="server" Text='<%# Eval("section2")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>

                                                                <asp:DropDownList ID="ddlEditSection" runat="server"></asp:DropDownList>

                                                                <div class="pull-right">
                                                                    <asp:RequiredFieldValidator ID="rfvtbSection" ErrorMessage="Section Required" ForeColor="Red" ControlToValidate="ddlEditSection"
                                                                        runat="server" Dispaly="Dynamic" />
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Edit">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="Edit" CausesValidation="False" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:LinkButton ID="lnkUpdate" runat="server" Text="Update" CommandName="Update" CausesValidation="False" />
                                                                <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel" CommandName="Cancel" CausesValidation="False" />
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete ?');" Text="Delete"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class=" col-md-2">
                                                <div class="wrapper wrapper-content animated fadeInUp">
                                                    <ul class="notes">
                                                        <li>
                                                            <div>
                                                                <small>Setup Class</small>
                                                                <h5>Please input the list of classes for your school, this part of the setup allows you to enter more than one class.</h5>
                                                                <p>
                                                                    After entering each class click on save to enter a new one and proceed to step five by clicking next when fully completed.
                                                                    <br />
                                                                    <strong>E.g. Class: 1 or Primary 1 or Basic 1
                                                                        <br />
                                                                        &nbsp;&nbsp;&nbsp; Section: Primary or Secondary</strong><br />
                                                                    No worries if you want to make changes later on or add additional classes - You can do so in the Settings page later on (But try to add as much as you can now).
                                                                </p>
                                                            </div>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:WizardStep>
                                    <asp:WizardStep ID="WizardStep5" runat="server" Title="5.Setup Class Name">
                                        <div style="width: 799px;">
                                            <div class="col-lg-8">
                                                <div class="ibox">
                                                    <div>
                                                        <br />
                                                        <div class="form-group">
                                                            <div class="editor-label">
                                                                <label>Class</label><span class="required">*</span>
                                                            </div>
                                                            <div class="editor-field">
                                                                <asp:DropDownList ID="ddlFormName" runat="server" CssClass="form-control" ValidationGroup="ClassNameGroup"></asp:DropDownList>
                                                                <div class="pull-right">
                                                                    <asp:RequiredFieldValidator ID="rfvddlFormName" ErrorMessage="Class Required" ForeColor="Red" ControlToValidate="ddlFormName"
                                                                        runat="server" Dispaly="Dynamic" ValidationGroup="ClassNameGroup" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="editor-label">
                                                                <label>Class Name</label><span class="required">*</span>
                                                            </div>
                                                            <div class="editor-field">
                                                                <asp:TextBox ID="tbSetupClass" runat="server" placeholder="Enter Class Name" CssClass="form-control" ValidationGroup="ClassNameGroup"></asp:TextBox>
                                                                <div class="pull-right">
                                                                    <asp:RequiredFieldValidator ID="rfvtbSetupClass" ErrorMessage="Class name Required" ForeColor="Red" ControlToValidate="tbSetupClass"
                                                                        runat="server" Dispaly="Dynamic" ValidationGroup="ClassNameGroup" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <asp:Button ID="btnSaveClass" runat="server" Text="Save" type="search" CssClass="btn-primary" ValidationGroup="ClassNameGroup" OnClick="btnSaveClass_Click" />
                                                </div>
                                                <asp:GridView ID="GridViewClass" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example"
                                                    OnRowEditing="GridViewClass_RowEditing" OnRowCancelingEdit="GridViewClass_RowCancelingEdit"
                                                    OnRowUpdating="GridViewClass_RowUpdating" OnRowDeleting="GridViewClass_RowDeleting">
                                                    <Columns>

                                                        <asp:BoundField DataField="class_id" HeaderText="Class id" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                            <HeaderStyle CssClass="hidden"></HeaderStyle>
                                                            <ItemStyle CssClass="hidden"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="form_name" HeaderText="Class" ReadOnly="true" />

                                                        <asp:TemplateField HeaderText="Class Name">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="tbClass" runat="server" Text='<%# Bind("class_name") %>' Width="80px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvtbClass" ErrorMessage="Class Name Required" ForeColor="Red" ControlToValidate="tbClass"
                                                                    runat="server" Dispaly="Dynamic" />
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblClass" runat="server" Text='<%# Bind("class_name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="Edit" CausesValidation="False" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:LinkButton ID="lnkUpdate" runat="server" Text="Update" CommandName="Update" CausesValidation="False" />
                                                                <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel" CommandName="Cancel" CausesValidation="False" />
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete ?');" Text="Delete"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class=" col-md-2">
                                                <div class="wrapper wrapper-content animated fadeInUp">
                                                    <ul class="notes">
                                                        <li>
                                                            <div>
                                                                <small>Setup Class Name</small>
                                                                <h4>If step 4 has not been setup correctly then the drop down list for class will be empty.</h4>
                                                                <p>
                                                                    Create your class groupings  here - Assign the class names to the classes you created earlier.<br />
                                                                    E.g <strong>Class</strong> = Primary 1 and <strong>Class Name</strong>  = B or Red<br />
                                                                    It is mandatory to create class names for ALL classes you have created.<br />
                                                                    <%-- Input a class name for each class and hit save to create another one.--%> After all class name groupings have been done proceed to step 6 by clicking next.
                                                                </p>
                                                            </div>
                                                        </li>
                                                    </ul>

                                                    <div class=" lightBoxGallery">
                                                        <h5>Example</h5>
                                                        <a href="../images/ClassScreenShot.png" target="_blank">
                                                            <img src="../images/ClassSmall.png" /></a>


                                                    </div>
                                                </div>

                                            </div>

                                        </div>

                                    </asp:WizardStep>
                                    <asp:WizardStep ID="WizardStep6" runat="server" Title="6.Setup Fee ">
                                        <div style="width: 799px;" class="row">
                                            <div class="col-lg-8">
                                                <br />
                                                <div class="ibox">
                                                    <div role="form" id="form">
                                                        <div>
                                                            <div class="form-group">
                                                                <div class="editor-label">
                                                                    <label>Academic Year</label>
                                                                </div>
                                                                <div class="editor-field">
                                                                    <asp:DropDownList ID="ddlFeeSetupYear" runat="server" CssClass=" form-control" ValidationGroup="FeeGroup">
                                                                    </asp:DropDownList>
                                                                    <div class="pull-right">
                                                                        <asp:RequiredFieldValidator ID="rfvddlFeeSetupYear" ErrorMessage="Year Required" ValidationGroup="FeeGroup" ForeColor="Red" ControlToValidate="ddlFeeSetupYear"
                                                                            runat="server" Dispaly="Dynamic" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="form-group">
                                                                <div class="editor-label">
                                                                    <label>Term</label>
                                                                </div>
                                                                <div class="editor-field">
                                                                    <asp:DropDownList ID="ddlFeeSetupTerm" runat="server" CssClass=" form-control">
                                                                    </asp:DropDownList>
                                                                    <div class="pull-right">
                                                                        <asp:RequiredFieldValidator ID="rfvddlFeeSetupTerm" ErrorMessage="Term Required" ValidationGroup="FeeGroup" ForeColor="Red" ControlToValidate="ddlFeeSetupTerm"
                                                                            runat="server" Dispaly="Dynamic" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="editor-label">
                                                                    <label>Class</label>
                                                                </div>
                                                                <div class="editor-field">
                                                                    <asp:DropDownList ID="ddlFeeSetupForm" runat="server" CssClass=" form-control">
                                                                    </asp:DropDownList>
                                                                    <div class="pull-right">
                                                                        <asp:RequiredFieldValidator ID="rfvddlFeeSetupForm" ErrorMessage="Class Required" ValidationGroup="FeeGroup" ForeColor="Red" ControlToValidate="ddlFeeSetupForm"
                                                                            runat="server" Dispaly="Dynamic" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="editor-label">
                                                                    <label>Fee Amount</label>
                                                                </div>
                                                                <div class="editor-field">
                                                                    <asp:TextBox ID="tbFeeSetupAmount" runat="server" placeholder="Enter Fee" TextMode="Number" CssClass="form-control" ValidationGroup="FeeGroup"></asp:TextBox>
                                                                    <div class="pull-right">
                                                                        <asp:RequiredFieldValidator ID="rfvtbFeeSetupAmount" ErrorMessage="Year Required" ValidationGroup="FeeGroup" ForeColor="Red" ControlToValidate="tbFeeSetupAmount"
                                                                            runat="server" Dispaly="Dynamic" />
                                                                    </div>
                                                                </div>
                                                            </div>


                                                            <asp:Button ID="BtnSaveFeSetup" runat="server" Text="Save" ValidationGroup="FeeGroup" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" OnClick="BtnSaveFeeSetup_Click" />

                                                        </div>
                                                    </div>
                                                </div>
                                                <h1>Fee List</h1>
                                                <asp:GridView ID="GridViewFee" runat="server" DataKeyNames="form_name" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example"
                                                    OnRowEditing="GridViewFee_RowEditing"
                                                    OnRowCancelingEdit="GridViewFee_RowCancelingEdit"
                                                    OnRowUpdating="GridViewFee_RowUpdating"
                                                    OnRowDeleting="GridViewFee_RowDeleting">
                                                    <Columns>
                                                        <asp:BoundField DataField="acad_year" HeaderText="Acad Year" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                            <HeaderStyle CssClass="hidden"></HeaderStyle>
                                                            <ItemStyle CssClass="hidden"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="term_name" HeaderText="Term" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                            <HeaderStyle CssClass="hidden"></HeaderStyle>
                                                            <ItemStyle CssClass="hidden"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="form_name" HeaderText="Class" ReadOnly="true" />
                                                        <asp:BoundField DataField="fee_id" HeaderText="Fee Id" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                            <HeaderStyle CssClass="hidden"></HeaderStyle>
                                                            <ItemStyle CssClass="hidden"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Fee">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="tbAmount" runat="server" Text='<%# Bind("amount") %>'></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="revtbAmount" ControlToValidate="tbAmount" runat="server"
                                                                    ErrorMessage="Only Numbers allowed" ForeColor="Red" ValidationExpression="^\d+([,\.]\d{1,2})?$"></asp:RegularExpressionValidator>
                                                                <asp:RequiredFieldValidator ID="rfvtbAmount" ErrorMessage="Amount Required" ForeColor="Red" ControlToValidate="tbAmount"
                                                                    runat="server" Dispaly="Dynamic" />
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <%#Eval("amount")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="Edit" CausesValidation="False" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:LinkButton ID="lnkUpdate" runat="server" Text="Update" CommandName="Update" />
                                                                <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel" CommandName="Cancel" CausesValidation="False" />
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete ?');" Text="Delete"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class=" col-md-2">
                                                <div class="wrapper wrapper-content animated fadeInUp">
                                                    <ul class="notes">
                                                        <li>
                                                            <div>
                                                                <small>Setup Fee</small>
                                                                <h4>Input the school fees for each of the classes you created in step 4</h4>
                                                                <p>
                                                                    The drop downs will only display data if steps 2,3 and 4 have been set up correctly. 
                                                                    No worries if you want to make changes later on- You can do so in the Settings page at any time.<br />
                                                                    <strong>Note:Fees is the total of all mandatory fees (tuition,lesson etc)</strong>
                                                                </p>
                                                            </div>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>




                                    </asp:WizardStep>
                                    <asp:WizardStep ID="WizardStep7" runat="server" Title="7.Setup Subject">
                                        <div style="width: 799px;">
                                            <div class="col-lg-8">
                                                <br />
                                                <div class="ibox">
                                                    <div role="form">
                                                        <div class="form-group">
                                                            <div class="editor-label">
                                                                <label>Subject</label><span class="required">*</span>
                                                            </div>
                                                            <div class="editor-field">
                                                                <asp:TextBox ID="tbSetupSubject" runat="server" placeholder="Enter subject name" ValidationGroup="VgSubjectSetup" CssClass="form-control"></asp:TextBox>
                                                                <div class="pull-right">
                                                                    <asp:RequiredFieldValidator ID="rfvtbSetupSubject" ValidationGroup="VgSubjectSetup" ErrorMessage="Subject name Required" ForeColor="Red" ControlToValidate="tbSetupSubject"
                                                                        runat="server" Dispaly="Dynamic" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <asp:Button ID="BtnSaveSetupSubject" runat="server" Text="Save" ValidationGroup="VgSubjectSetup" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" OnClick="BtnSaveSetupSubject_Click" />
                                                    </div>
                                                </div>
                                                <h1>List of Subjects </h1>


                                                <asp:GridView ID="GridViewSubjects" runat="server" AutoGenerateColumns="False"
                                                    Visible="False" CssClass="table table-striped table-bordered table-hover dataTables-example"
                                                    OnRowEditing="GridViewSubjects_RowEditing" OnRowCancelingEdit="GridViewSubjects_RowCancelingEdit"
                                                    OnRowUpdating="GridViewSubjects_RowUpdating" OnRowDeleting="GridViewSubjects_RowDeleting">

                                                    <Columns>
                                                        <asp:BoundField DataField="subject_id" HeaderText="Subject Id" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                            <HeaderStyle CssClass="hidden"></HeaderStyle>
                                                            <ItemStyle CssClass="hidden"></ItemStyle>
                                                        </asp:BoundField>

                                                        <asp:TemplateField HeaderText="Class">
                                                            <ItemTemplate>
                                                                <%#Eval("subject_name")%>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="tbSubjectName" runat="server" Text='<%# Eval("subject_name") %>'></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvtbSubjectName" ErrorMessage="Subject Required" ForeColor="Red" ControlToValidate="tbSubjectName"
                                                                    runat="server" Dispaly="Dynamic" />
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Edit">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="Edit" CausesValidation="False" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:LinkButton ID="lnkUpdate" runat="server" Text="Update" CommandName="Update" CausesValidation="False" />
                                                                <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel" CommandName="Cancel" CausesValidation="False" />
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete ?');" Text="Delete"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class=" col-md-2">
                                                <div class="wrapper wrapper-content animated fadeInUp">
                                                    <ul class="notes">
                                                        <li>
                                                            <div>
                                                                <small>Setup Subject</small>
                                                                <h4>Once you have inputted all your needed subjects hit save and then proceed to finish, this will complete the setup wizard and take you to your iQ dashboard.</h4>
                                                                <p>
                                                                    The final stage of the setup wizard is to input your school subjects, this page allows for more than one entry, once you have entered subject name hit save and enter as many subjects as you need
                                                                    <br />
                                                                    <strong>E.g. Mathematics, Science etc. </strong>
                                                                </p>
                                                            </div>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>

                                        </div>
                                    </asp:WizardStep>
                                    <asp:WizardStep ID="WizardStep8" runat="server" Title="8.Payment Card Details">
                                        <div style="width: 1200px;">
                                            <div class="col-lg-8">
                                                <br />
                                                <div class="ibox">
                                                    <div class="row">

                                                        <div class="col-lg-12">

                                                            <div class="ibox">
                                                                <div class="ibox-title">
                                                                    Card Payment Details
                                                                </div>
                                                                <div class="ibox-content">

                                                                    <div class="panel-group payments-method" id="accordion">

                                                                        <div class="panel panel-default">
                                                                            <div class="panel-heading">
                                                                                <div class="pull-right">
                                                                                    <asp:DropDownList ID="ddlCardType" runat="server" CssClass="form-control">
                                                                                        <asp:ListItem Text="Choose.." Value=""></asp:ListItem>
                                                                                        <asp:ListItem>Visa</asp:ListItem>
                                                                                        <asp:ListItem>MasterCard</asp:ListItem>
                                                                                        <asp:ListItem>Verve</asp:ListItem>
                                                                                        <asp:ListItem>Genesis</asp:ListItem>

                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="rfvddlCardType" ErrorMessage="Account Type Required" ForeColor="Red" ControlToValidate="ddlCardType"
                                                                                        runat="server" Dispaly="Dynamic" />
                                                                                </div>
                                                                                <h5 class="panel-title">
                                                                                    <label>Account Type</label>
                                                                                </h5>
                                                                            </div>
                                                                            <div id="collapseTwo" class="panel-collapse collapse in">
                                                                                <div class="panel-body">

                                                                                    <div class="row">

                                                                                        <div class="col-md-8">

                                                                                            <div role="form" id="payment-form">
                                                                                                <div class="row">
                                                                                                    <div class="col-xs-12">
                                                                                                        <div class="form-group">
                                                                                                            <label>NAME ON CARD</label>
                                                                                                            <asp:TextBox ID="tbNameOnCard" runat="server" CssClass="form-control" placeholder="Cardholder Name "></asp:TextBox>
                                                                                                            <asp:RequiredFieldValidator ID="rfvtbNameOnCard" ErrorMessage="Name Required" ForeColor="Red" ControlToValidate="tbNameOnCard"
                                                                                                                runat="server" Dispaly="Dynamic" />
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>

                                                                                                <div class="row">
                                                                                                    <div class="col-xs-12">
                                                                                                        <div class="form-group">
                                                                                                            <label>CARD NUMBER</label>
                                                                                                            <div class="input-group">
                                                                                                                <asp:TextBox ID="tbCardNumber" runat="server" CssClass="form-control" placeholder="Valid Card Number"></asp:TextBox>
                                                                                                                <span class="input-group-addon"><i class="fa fa-credit-card"></i></span>
                                                                                                                <asp:RequiredFieldValidator ID="rfvtbCardNumber" ErrorMessage="card number Required" ForeColor="Red" ControlToValidate="tbCardNumber"
                                                                                                                    runat="server" Dispaly="Dynamic" />
                                                                                                                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="tbCardNumber" ID="RegularExpressionValidator3" ForeColor="Red" CssClass="pull-right"
                                                                                                                    ValidationExpression="^[\s\S]{15,16}$" runat="server" ErrorMessage="Enter Valid <br />Card Number."></asp:RegularExpressionValidator>

                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-xs-7 col-md-7">
                                                                                                        <div class="form-group">
                                                                                                            <label>EXPIRATION DATE</label>
                                                                                                            <asp:TextBox ID="tbExpireDate" name="Expiry" runat="server" CssClass="form-control" placeholder="MM / YY"></asp:TextBox>
                                                                                                            <asp:RequiredFieldValidator ID="rfvtbExpireDate" ErrorMessage="MM / YY Required" ForeColor="Red" ControlToValidate="tbExpireDate"
                                                                                                                runat="server" Dispaly="Dynamic" />
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="col-xs-5 col-md-5 pull-right">
                                                                                                        <div class="form-group">
                                                                                                            <label>CV CODE</label>
                                                                                                            <asp:TextBox ID="tbCVCode" runat="server" CssClass="form-control" name="CVC" placeholder="CVC"></asp:TextBox>
                                                                                                            <asp:RequiredFieldValidator ID="rfvtbCVCode" ErrorMessage="CVC Required" ForeColor="Red" ControlToValidate="tbCVCode"
                                                                                                                runat="server" Dispaly="Dynamic" />
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>

                                                                                            </div>

                                                                                        </div>

                                                                                    </div>






                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                    <div class="panel-group payments-method" id="accordio">

                                                                        <div class="panel panel-default">
                                                                            <div class="panel-heading">

                                                                                <h5 class="panel-title">
                                                                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseTwo">Billing Address</a>
                                                                                </h5>
                                                                            </div>
                                                                            <div id="collapse2" class="panel-collapse collapse in">
                                                                                <div class="panel-body">

                                                                                    <div class="row">

                                                                                        <div class="col-md-12">

                                                                                            <div role="form" id="billing-form">
                                                                                                <div class="row">
                                                                                                    <div class="col-xs-6  col-md-6">
                                                                                                        <div class="form-group">
                                                                                                            <label>FIRST NAME</label>
                                                                                                            <asp:TextBox ID="tbBillFName" runat="server" CssClass="form-control" placeholder="Enter First Name"></asp:TextBox>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="col-xs-6 col-md-6 pull-right">
                                                                                                        <div class="form-group">
                                                                                                            <label>LAST NAMEE</label>
                                                                                                            <asp:TextBox ID="tbBillLastName" runat="server" CssClass="form-control" placeholder="Enter Last Name"></asp:TextBox>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-xs-6 col-md-6">
                                                                                                        <div class="form-group">
                                                                                                            <label>EMAIL</label>
                                                                                                            <asp:TextBox ID="tbBillEmail" runat="server" CssClass="form-control" placeholder="Enter Email"></asp:TextBox>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="col-xs-6 col-md-6 pull-right">
                                                                                                        <div class="form-group">
                                                                                                            <label>PHONE</label>
                                                                                                            <asp:TextBox ID="tbBillPhone" runat="server" CssClass="form-control" placeholder="Enter Phone number"></asp:TextBox>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-xs-12">
                                                                                                        <div class="form-group">
                                                                                                            <label>ADDRESS</label>
                                                                                                            <asp:TextBox ID="tbBillAddress" runat="server" CssClass="form-control" placeholder="Enter Address "></asp:TextBox>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>


                                                                                                <div class="row">
                                                                                                    <div class="col-xs-4 col-md-4">
                                                                                                        <div class="form-group">
                                                                                                            <label>LGA</label>
                                                                                                            <asp:TextBox ID="tbBillCity" runat="server" CssClass="form-control" placeholder="Enter LGA"></asp:TextBox>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="col-xs-4 col-md-4">
                                                                                                        <div class="form-group">
                                                                                                            <label>STATE</label>
                                                                                                            <asp:TextBox ID="tbBillState" runat="server" CssClass="form-control" placeholder="Enter State"></asp:TextBox>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="col-xs-4 col-md-4 pull-right">
                                                                                                        <div class="form-group">
                                                                                                            <label>ZIP</label>
                                                                                                            <asp:TextBox ID="tbBillZip" runat="server" CssClass="form-control" placeholder="Enter Xip Code"></asp:TextBox>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>

                                                                                                <div class="row">
                                                                                                    <div class="col-xs-12">
                                                                                                        <asp:CheckBox ID="CheckBox1" onclick="validate()" runat="server" />
                                                                                                        I accept the <a href="../pages/terms_conditions.aspx" target="_blank"><u>Terms and Conditions</u> </a>
                                                                                                        <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="" ClientValidationFunction="ValidateCheckBox"></asp:CustomValidator>
                                                                                                        <small>and I authorize the above named business to charge the credit card indicated in this authorization form according to the terms outlined above. 
                                                                                                            This payment authorization is for the services described above, for the amount indicated above only. I certify that I am an authorized user of this credit card and that 
                                                                                                            I will not dispute the payment with my credit card company; so long as the transaction corresponds to the terms indicated in this form.
                                                                                                        </small><%--<button class="btn btn-primary" type="submit">Save Detials</button>--%>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>

                                                                                        </div>

                                                                                    </div>






                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    </div>

                                                                </div>

                                                            </div>

                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class=" col-md-2">
                                                <div class="wrapper wrapper-content animated fadeInUp">
                                                    <ul class="notes">
                                                        <li>
                                                            <div>
                                                                <small>Card Details</small>
                                                                <h4>we will notify 7 days before processing your payment.</h4>
                                                                <p>
                                                                    Thank you for registering for iQ by Torilo, the smart software for smart schools. To complete your activation process, kindly fill the payment form, your card will not be charged until after your 30 days free trial.
                                                                    <br />
                                                                    <strong>Payments will be taken after your 30 days free trial. </strong>
                                                                </p>
                                                            </div>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>

                                        </div>
                                    </asp:WizardStep>

                                </WizardSteps>
                            </asp:Wizard>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <style>
        .lightBoxGallery {
            text-align: center;
        }

            .lightBoxGallery img {
                margin: 5px;
            }
    </style>
</body>
</html>
