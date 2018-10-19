<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="school_details.aspx.cs" Inherits="Alex.pages.admin.school_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
        $(function () { $('.datepick').datepicker({ changeMonth: true, changeYear: true, yearRange: '-100:' + new Date().getFullYear(), dateFormat: 'dd/mm/yy', maxDate: new Date() }); });
    </script>


    <div class="col-md-3 pull-right">
            <a href="../settings.aspx">
               <asp:Label runat="server" Text="&nbsp;Back to Settings" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"  ></asp:Label>
            </a>
    </div>
    <h2>School Details</h2>
    
    <asp:SqlDataSource ID="SqlDataSourceStateDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
        SelectCommand="sp_ms_nigerian_states_dropdown"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSourceCountryDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
        SelectCommand="sp_ms_countries_dropdown"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSourceCityDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
        SelectCommand="sp_ms_nigeria_lagos_state_lga_dropdown"></asp:SqlDataSource>
    <div class=" wrapper-content  animated fadeInRight">
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
                    <asp:Label ID="lblHeadteacherTitle" runat="server" Text='<%# Bind("headteacher_title") %>'></asp:Label>
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
                    <asp:Label ID="lblCountry" runat="server" Text='<%# Bind("country") %>'></asp:Label>
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
                    <asp:Label ID="lblContactNo2" runat="server" Text='<%# Bind("contact_no2") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
           



            <asp:CommandField ShowEditButton="True" EditText=" Edit" ShowInsertButton="True" InsertText="Save" ControlStyle-CssClass="btn btn-primary" HeaderStyle-Width="80px">
                <HeaderStyle Width="80px"></HeaderStyle>
                <ControlStyle CssClass="btn btn-primary" Width="80px"></ControlStyle>
            </asp:CommandField>
        </Fields>
        <EmptyDataTemplate>
            <h1>Setup School</h1>
            <asp:Button ID="InsertSchoolDetails" runat="server" CommandName="New" InsertText="Add" Text="Setup School Now" CssClass="btn btn-sm btn-primary" />
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
    <%--  --Pages Styles--%>
    <style>
        .required {
            color: #F00;
        }
    </style>
</asp:Content>

