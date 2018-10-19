<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="setup_new_user.aspx.cs" Inherits="Alex.pages.admin.setup_new_user" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../scripts/css/pagestyle.css" rel="stylesheet" />
   <%--   <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
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
    </script>--%>
     <div class="col-md-3 pull-right">
            <a href="../settings.aspx">
               <asp:Label runat="server" Text="&nbsp;Back to Settings" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"  ></asp:Label>
            </a>
        </div>
    <h2>New User</h2>

    <div class=" wrapper-content  animated fadeInRight">
        <div class="row">
            <div class="col-lg-6">
                <div class="ibox">
                    <div role="form" id="form">
                        <div class="form-group">
                            <div class="editor-label">
                                <label>Email</label><span class="required">*</span>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="tbSetupUserEmail" runat="server" placeholder="Enter Email" type="email" ValidationGroup="UserSetup" CssClass="form-control"></asp:TextBox>
                                <div class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvtbSetupUserEmail" ErrorMessage=" Email required" ForeColor="Red" ValidationGroup="UserSetup"  ControlToValidate="tbSetupUserEmail"
                                        runat="server" Dispaly="Dynamic" />
                                    <asp:RegularExpressionValidator ID="revtbSetupUserEmail" runat="server"
                                        ErrorMessage="Invalid Email" ControlToValidate="tbSetupUserEmail" ForeColor="Red" ValidationGroup="UserSetup" 
                                        SetFocusOnError="True" ValidationExpression="^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$" />
                                </div>
                            </div>
                        </div>
                      <%--  <div class="form-group">
                            <div class="editor-label">
                                <label>Password</label><span class="required">*</span>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="tbSetupUserPassword" runat="server" placeholder="Enter Password" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                <div class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvtbSetupUserPassword" ErrorMessage="Password required" ForeColor="Red" ControlToValidate="tbSetupUserPassword"
                                        runat="server" Dispaly="Dynamic" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="editor-label">
                                <label>Confirm Password</label>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="tbSetupUserConfirmPassword" runat="server" placeholder="Confirm Password" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                <div class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvtbSetupUserConfirmPassword" ErrorMessage="Confirm Password required" ForeColor="Red" ControlToValidate="tbSetupUserConfirmPassword"
                                        runat="server" Dispaly="Dynamic" />
                                </div>
                                <div class="pull-right">
                                    <asp:CompareValidator ID="comparePasswords"
                                        runat="server"
                                        ControlToCompare="tbSetupUserPassword"
                                        ControlToValidate="tbSetupUserConfirmPassword"
                                        ErrorMessage="Your passwords do not match up!" ForeColor="Red"
                                        Display="Dynamic" />
                                </div>
                            </div>
                        </div>--%>
                        <div class="form-group">
                            <div class="editor-label">
                                <label>First Name</label><span class="required">*</span>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="tbSetupUserFName" runat="server" placeholder="Enter First Name" ValidationGroup="UserSetup"  CssClass="form-control"></asp:TextBox>
                                <div class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvtbSetupUserFName" ErrorMessage=" First Name required" ForeColor="Red" ValidationGroup="UserSetup"  ControlToValidate="tbSetupUserFName"
                                        runat="server" Dispaly="Dynamic" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="editor-label">
                                <label>Last Name</label><span class="required">*</span>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="tbSetupUserLName" runat="server" placeholder="Enter Last Name" ValidationGroup="UserSetup"  CssClass="form-control"></asp:TextBox>
                                <div class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvtbSetupUserLName" ErrorMessage=" Last Name required" ValidationGroup="UserSetup"  ForeColor="Red" ControlToValidate="tbSetupUserLName"
                                        runat="server" Dispaly="Dynamic" />
                                </div>
                            </div>
                        </div>

<%--                        <div class="form-group">
                            <div class="editor-label">
                                <label>Date Of Birth</label><span class="required">*</span>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="DOB" runat="server" placeholder="dd/mm/yyyy" ValidationGroup="UserSetup"  CssClass="form-control datepick"></asp:TextBox>
                                <div class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvDOB" ErrorMessage="Date of Birth required" ValidationGroup="UserSetup"  ForeColor="Red" ControlToValidate="DOB"
                                        runat="server" Dispaly="Dynamic" />
                                </div>

                            </div>
                        </div>--%>
                        <div class="form-group">
                            <div class="editor-label">
                                <label>Level Of Access</label><span class="required">*</span>
                            </div>
                            <div class="editor-field">
                                <asp:DropDownList ID="ddlLevelOfAccess" runat="server" ValidationGroup="UserSetup"  CssClass="form-control" >
                                    <asp:ListItem Value="" Selected="True">Select Level of Access</asp:ListItem></asp:DropDownList>
                                <div class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvddlLevelOfAccess" ErrorMessage="Level of Access required" ValidationGroup="UserSetup"  ForeColor="Red" ControlToValidate="ddlLevelOfAccess"
                                        runat="server" Dispaly="Dynamic" />
                                </div>
                            </div>
                        </div>
                        <%-- <div class="form-group">
                            <div class="editor-label">
                                <label>Position</label><span class="required">*</span>
                            </div>
                           <div class="editor-field">
                                <asp:DropDownList ID="ddlSetupUserPosition" runat="server" ValidationGroup="UserSetup">
                                    <asp:ListItem Value="" Selected="True">Select Position</asp:ListItem>
                                    <asp:ListItem>Administrator</asp:ListItem>
                                    <asp:ListItem>Employee</asp:ListItem>
                                    <asp:ListItem>Secretary</asp:ListItem>
                                </asp:DropDownList>
                                <div class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvddlSetupUserPosition" ErrorMessage="Position required" ValidationGroup="UserSetup"  ForeColor="Red" ControlToValidate="ddlSetupUserPosition"
                                        runat="server" Dispaly="Dynamic" />
                                </div>
                            </div>
                        </div>--%>

                        <asp:Button ID="btnCreateSetupUser" runat="server" Text="Create" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" ValidationGroup="UserSetup"  OnClick="btnCreateSetupUser_Click" />
                        <a href="../settings.aspx">
                           <asp:Label runat="server" Text="Cancel" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" ></asp:Label>
                        </a>
                    </div>
                </div>
            </div> <div class="col-md-8"><asp:Label ID="lblPassword" runat="server" Text=""  CssClass="text-green font-bold h3"></asp:Label></div>
        </div>
    </div>
    
      
                    
               
<style>
.required {
color: #F00;
}
.text-green {
  color:#008000;
}
</style> 
</asp:Content>
