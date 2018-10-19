<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="add_new.aspx.cs" Inherits="Alex.pages.add_new" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
        $(function () { $("[id$=DOB]").datepicker({ changeMonth: true, changeYear: true, yearRange: '-100:' + new Date().getFullYear(), dateFormat: 'dd/mm/yy' }); });
        function validateDate(sender, e) {

            // Split out the constituent parts (dd/mm/yyyy)    
            var dayfield = e.Value.split("/")[0];
            var monthfield = e.Value.split("/")[1];
            var yearfield = e.Value.split("/")[2];

            // Create a new date object based on the separate parts
            var dateValue = new Date(yearfield, monthfield - 1, dayfield)

            // Check that the date object's parts match the split out parts from the original string
            if ((dateValue.getMonth() + 1 != monthfield) || (dateValue.getDate() != dayfield) || (dateValue.getFullYear() != yearfield)) {
                e.IsValid = false;
            }

            // Check for future dates
            if (e.IsValid) {
                e.IsValid = dateValue <= new Date()
            }
        }
        function copy_data(val) {
            var UI = document.getElementById("<%=tbLName.ClientID %>").value;
            document.getElementById("<%=tbP_g_LName.ClientID %>").value = UI;
        }

    </script>
    <h2>Add New Profile</h2>
    <div class=" wrapper-content  animated fadeInRight">
        <div class="row">
            <div class="col-lg-6">
                <div class="ibox">
                    <div role="form" id="form">
                        <div>
                            <div class="form-group">
                                <div class="editor-label">
                                    <label>Title <strong class="required">*</strong></label>
                                </div>
                                <div class="editor-field">
                                    <asp:DropDownList ID="ddlTitle" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlTitle_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <div class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvddlTitle" ErrorMessage="Please Choose Title" ForeColor="Red" ControlToValidate="ddlTitle"
                                            runat="server" Dispaly="Dynamic" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="editor-label">
                                    <label>Last Name<strong class="required">*</strong></label>
                                </div>
                                <div class="editor-field">
                                    <asp:TextBox ID="tbLName" runat="server" placeholder="Enter Last Name" CssClass="form-control" onKeyUp="copy_data(this)"></asp:TextBox>
                                    <div class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvLName" ErrorMessage=" Last Name Required" ForeColor="Red" ControlToValidate="tbLName"
                                            runat="server" Dispaly="Dynamic" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="editor-label">
                                    <label>First Name<strong class="required">*</strong></label>
                                </div>
                                <div class="editor-field">
                                    <asp:TextBox ID="tbFName" runat="server" placeholder="Enter First Name" CssClass="form-control" type="text"></asp:TextBox>
                                    <div class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvFName" ErrorMessage="First Name Required" ForeColor="Red" ControlToValidate="tbFName"
                                            runat="server" Dispaly="Dynamic" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="editor-label">
                                    <label>Middle Name</label>
                                </div>
                                <div class="editor-field">
                                    <asp:TextBox ID="tbMName" runat="server" placeholder="Enter Middle Name" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>


                            <div class="form-group">
                                <div class="editor-label">
                                    <label>Date of Birth<strong class="required">*</strong></label>
                                </div>
                                <div class="editor-field input-group date">
                                    <asp:TextBox ID="DOB" runat="server" rel="date" type="text" CssClass="form-control" placeholder="dd/mm/yyyy"></asp:TextBox>
                                    <div class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvDOB" ErrorMessage=" Date of Birth Required" ForeColor="Red" ControlToValidate="DOB"
                                            runat="server" Dispaly="Dynamic" />
                                        <asp:CustomValidator runat="server" ID="valDateRange" ControlToValidate="DOB" ForeColor="Red" ErrorMessage="enter valid date" ClientValidationFunction="validateDate" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="editor-label">
                                    <label>Gender<strong class="required">*</strong></label>
                                </div>
                                <div class="editor-field">
                                    <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                    <div class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvGender" ErrorMessage=" Gender Required" ForeColor="Red" ControlToValidate="ddlGender"
                                            runat="server" Dispaly="Dynamic" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="editor-label">
                                    <label>Blood Group</label>
                                </div>
                                <div class="editor-field">
                                    <asp:DropDownList ID="ddlBloodGroup" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="editor-label">
                                    <label>Student Phone No</label>
                                </div>
                                <div class="editor-field">
                                    <asp:TextBox ID="tbStudentPhoneNo" runat="server" placeholder="Enter Phone Number" CssClass="form-control"></asp:TextBox>
                                </div>

                                <div class="pull-right">
                                    <asp:RegularExpressionValidator ID="RevStudentPh" ControlToValidate="tbStudentPhoneNo" runat="server"
                                        ErrorMessage="Phone Number should be in 11 digits " ForeColor="Red" ValidationExpression="\d{11}"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <hr />


                        <div class="form-group">
                            <div class="editor-label">
                                <label>Nationality<strong class="required">*</strong></label>
                            </div>
                            <div class="editor-field">
                                <asp:DropDownList ID="ddlNationality" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="pull-right">
                                <asp:RequiredFieldValidator ID="rfvNationality" ErrorMessage=" Select Nationality" ForeColor="Red" ControlToValidate="ddlNationality"
                                    runat="server" Dispaly="Dynamic" InitialValue="NA" />
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="editor-label">
                                <label>Ethnicity</label>
                            </div>
                            <div class="editor-field">
                                <asp:DropDownList ID="ddlEthnicity" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="editor-label">
                                <label>Religion</label>
                            </div>
                            <div class="editor-field">
                                <asp:DropDownList ID="ddlReligion" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>

                        <hr />

                        <div class="form-group">
                            <div class="editor-label">
                                <label>Address<strong class="required">*</strong></label>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="TbAddressLine1" runat="server" placeholder="Enter Address" CssClass="form-control" type="text"></asp:TextBox>
                                <div class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorAddressLine1" ErrorMessage="Address Required" ForeColor="Red" ControlToValidate="TbAddressLine1"
                                        runat="server" Dispaly="Dynamic" />
                                </div>
                            </div>
                        </div>


                        <div class="form-group">
                            <div class="editor-label">
                                <%-- <label>Address Line2</label>--%>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="TbAddressLine2" runat="server" placeholder="Enter Address" CssClass="form-control" type="text"></asp:TextBox>
                                <%--<div class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorTbAddressLine2" ErrorMessage="Address required" ForeColor="Red" ControlToValidate="TbAddressLine2"
                                        runat="server" Dispaly="Dynamic" />
                                </div>--%>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="editor-label">
                                <%--<label>Address Line 3</label>--%>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="TbAddressLine3" runat="server" placeholder="Enter Address" CssClass="form-control" type="text"></asp:TextBox>
                                <%-- <div class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorTbAddressLine3" ErrorMessage="First Name required" ForeColor="Red" ControlToValidate="TbAddressLine3"
                                        runat="server" Dispaly="Dynamic" />
                                </div>--%>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="editor-label">
                                <label>State<strong class="required">*</strong></label>
                            </div>
                            <div class="editor-field">
                                <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                <div class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvddlState" ErrorMessage="State Required" ForeColor="Red" ControlToValidate="ddlState"
                                        runat="server" Dispaly="Dynamic" />
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="editor-label">
                                <label>LGA<strong class="required">*</strong></label>
                            </div>
                            <div class="editor-field">
                                <%--<asp:TextBox ID="TbCity" runat="server" placeholder="Enter City" CssClass="form-control"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlLGA" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                <div class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvddlLGA" ErrorMessage="LGA Required" ForeColor="Red" ControlToValidate="ddlLGA"
                                        runat="server" Dispaly="Dynamic" />
                                </div>
                            </div>
                        </div>



                        <%-- <div class="form-group">
                            <div class="editor-label">
                                <label>Zip Code<strong class="required">*</strong></label></div>
                            <div class="editor-field">
                                <asp:TextBox ID="TbZipCode" runat="server" placeholder="Enter Zip Code" CssClass="form-control"></asp:TextBox></div>
                        </div>--%>

                        <div class="form-group">
                            <div class="editor-label">
                                <label>Country<strong class="required">*</strong></label>
                            </div>
                            <div class="editor-field">
                                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control"></asp:DropDownList>
                                <div class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvddlCountry" ErrorMessage="Country Required" ForeColor="Red" ControlToValidate="ddlCountry"
                                        runat="server" Dispaly="Dynamic" />
                                </div>
                            </div>
                        </div>

                        <hr />

                        <div class="form-group">
                            <div class="editor-label">
                                <label>Title<strong class="required">*</strong></label>
                            </div>
                            <div class="editor-field">
                                <asp:DropDownList ID="ddlP_g_title" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="pull-right">
                                <asp:RequiredFieldValidator ID="rfvP_g_title" ErrorMessage="Title Required" ForeColor="Red" ControlToValidate="ddlP_g_title"
                                    runat="server" Dispaly="Dynamic" />
                            </div>
                        </div>


                        <div class="form-group">
                            <div class="editor-label">
                                <label>Parent/Guardian Last Name<strong class="required">*</strong></label>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="tbP_g_LName" runat="server" placeholder="Enter last Name" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="pull-right">
                                <asp:RequiredFieldValidator ID="rfvP_g_LName" ErrorMessage=" Last Name Required" ForeColor="Red" ControlToValidate="tbP_g_LName"
                                    runat="server" Dispaly="Dynamic" />
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="editor-label">
                                <label>Parent/Guardian First Name</label>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="tbP_g_FName" runat="server" placeholder="Enter first Name" CssClass="form-control"></asp:TextBox>
                            </div>
                            <%--<div class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvP_g_FName" ErrorMessage=" First Name Required" ForeColor="Red" ControlToValidate="tbP_g_FName"
                                            runat="server" Dispaly="Dynamic" />
                                    </div>--%>
                        </div>

                        <div class="form-group">
                            <div class="editor-label">
                                <label>Relationship to Student<strong class="required">*</strong></label>
                            </div>
                            <div class="editor-field">
                                <asp:DropDownList ID="ddlRelation" runat="server" CssClass="form-control">
                                </asp:DropDownList>

                                <div class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvddlRelation" ErrorMessage=" Relationship Required" ForeColor="Red" ControlToValidate="ddlRelation"
                                        runat="server" Dispaly="Dynamic" />
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="editor-label">
                                <label>Contact No Mobile<strong class="required">*</strong></label>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="MobileTb" runat="server" placeholder="Enter Mobile Number" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="pull-right">
                                <asp:RequiredFieldValidator ID="rfvMobileTb" ErrorMessage=" Mobile No Required" ForeColor="Red" ControlToValidate="MobileTb"
                                    runat="server" Dispaly="Dynamic" />
                            </div>
                            <div class="pull-right">
                                <asp:RegularExpressionValidator ID="revMobileTb" ControlToValidate="MobileTb" runat="server"
                                    ErrorMessage="Phone Number should be in 11 digits " ForeColor="Red" ValidationExpression="\d{11}"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="editor-label">
                                <label>Contact No Home/Office</label>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="HomeNoTb" runat="server" placeholder="Enter Home/Office Number" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="pull-right">
                                <asp:RegularExpressionValidator ID="revHomeNoTb" ControlToValidate="HomeNoTb" runat="server"
                                    ErrorMessage="Phone Number should be in 11 digits " ForeColor="Red" ValidationExpression="\d{11}"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="editor-label">
                                <label>Email</label>
                            </div>

                            <div class="editor-field">
                                <asp:TextBox ID="EmailTb" runat="server" placeholder="Enter Email" TextMode="Email" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="pull-right">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                    ErrorMessage="Invalid Email" ControlToValidate="EmailTb" ForeColor="Red"
                                    SetFocusOnError="True"
                                    ValidationExpression="^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$">
                                </asp:RegularExpressionValidator>
                            </div>
                        </div>

                        <hr />
                        <div class="form-group">
                            <div class="editor-label">
                                <label>Emergency Contact First Name</label>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="tbEmergencyContactFName" runat="server" placeholder="Enter First Name" CssClass="form-control" type="text"></asp:TextBox>
                                <div class="pull-right">
                                    <%--<asp:RequiredFieldValidator ID="rfvEmergencyContact" ErrorMessage="First Name Required" ForeColor="Red" ControlToValidate="tbEmergencyContactFName"
                                        runat="server" Dispaly="Dynamic" />--%>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="editor-label">
                                <label>Emergency Contact Last Name</label>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="tbEmergencyContactLName" runat="server" placeholder="Enter Last Name" CssClass="form-control" type="text"></asp:TextBox>
                                <div class="pull-right">
                                    <%--<asp:RequiredFieldValidator ID="rfvEmergencyContactLName" ErrorMessage="Last Name Required" ForeColor="Red" ControlToValidate="tbEmergencyContactLName"
                                        runat="server" Dispaly="Dynamic" />--%>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="editor-label">
                                <label>Emergency Contact Number</label>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="tbEmergencyContactNumber" runat="server" placeholder="Enter Number" CssClass="form-control" type="text"></asp:TextBox>
                                <div class="pull-right">
                                    <%-- <asp:RequiredFieldValidator ID="rfvEmergencyContactNumber" ErrorMessage="Phone number Required" ForeColor="Red" ControlToValidate="tbEmergencyContactNumber"
                                        runat="server" Dispaly="Dynamic" />--%>
                                    <asp:RegularExpressionValidator ID="revtbEmergencyContactNumber" ControlToValidate="tbEmergencyContactNumber" runat="server"
                                        ErrorMessage="Phone Number should be in 11 digits " ForeColor="Red" ValidationExpression="\d{11}"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>


                        <div class="form-group">
                            <div class="editor-label">
                                <label>Notes</label>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="tbNotes" runat="server" placeholder="Enter Notes" CssClass="form-control" MaxLength="500"></asp:TextBox>
                            </div>
                        </div>
                        <div>
                            <asp:Button ID="BtnSubmit" runat="server" Text="Submit" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" OnClick="BtnSubmit_Click" />
                            <a href="people.aspx">
                                <asp:Label runat="server" Text="Cancel" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True"></asp:Label></a>
                        </div>
                    </div>
                </div>
            </div>

        </div>

    </div>
    <style>
        .editor-label .required {
            color: #F00;
        }
    </style>
</asp:Content>
