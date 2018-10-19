<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="reset_password.aspx.cs" Inherits="Alex.pages.admin.reset_password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%-- <div class="col-md-3 pull-right">
            <a href="../settings.aspx">
               <asp:Label runat="server" Text="&nbsp;Back to Settings" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"  ></asp:Label>
            </a>
    </div>--%>
   <h1></h1>
    <div class="row wrapper">
        <div class="col-lg-10 h3">
            Please reset your password below
        </div>
    </div>

    <div class=" wrapper-content  animated fadeInRight">
        <div class="row">
            <div class="col-lg-6">
                <div class="ibox">
                    <div role="form" id="form">
                        <div class="form-group">
                            <div class="editor-label">
                                <label>Old Password</label>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="tbOldPassword" runat="server" placeholder="Enter Old Password" type="password" CssClass="form-control"></asp:TextBox>
                                <div class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvtbOldPassword" ErrorMessage="old Password Required" ForeColor="Red" ControlToValidate="tbOldPassword"
                                        runat="server" Dispaly="Dynamic" />
                                </div>
                            </div>
                        </div>
                        <div id="compare">
                            <div class="form-group">
                                <div class="editor-label">
                                    <label>New Password</label>
                                </div>
                                <div class="editor-field">
                                    <asp:TextBox ID="tbpassword" runat="server" placeholder="Enter New Password" name="tbpassword" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                    <div class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvtbpassword" ErrorMessage=" Password Required" ForeColor="Red" ControlToValidate="tbpassword"
                                            runat="server" Dispaly="Dynamic" />
                                    </div>
                                    <div class="pull-right"><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                        ErrorMessage="Password must be minimum 8 characters and must include 1 uppercase letter and 1 numeric character." ControlToValidate="tbpassword"
                                        ValidationExpression="((?=.*\d)(?=.*[A-Z]).{8,20})" ForeColor="Red"
                                        Display="Dynamic">
                                    </asp:RegularExpressionValidator>
                                   </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="editor-label">
                                    <label>Confirm Password</label>
                                </div>
                                <div class="editor-field">
                                    <asp:TextBox ID="tbconfirmpassword" runat="server" placeholder="Confirm Password" name="tbconfirmpassword" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                    <div class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvtbconfirmpassword" ErrorMessage=" Confirm Password Required" ForeColor="Red" ControlToValidate="tbconfirmpassword"
                                            runat="server" Dispaly="Dynamic" />
                                    </div>

                                </div>
                            
                            <div class="pull-right">
                                <asp:CompareValidator ID="comparePasswords"
                                    runat="server"
                                    ControlToCompare="tbpassword"
                                    ControlToValidate="tbconfirmpassword"
                                    ErrorMessage="Your passwords do not match up!" ForeColor="Red"
                                    Display="Dynamic" />
                            </div></div>
                        </div>

                         <label><span class=" text-red">* </span>Password must be minimum 8 characters and must include 1 uppercase letter and 1 numeric character</label><br /><br />
                        <asp:Button ID="btnSetupNewPassword" type="button" runat="server" Text="Change" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" OnClick="btnSetupNewPassword_Click" />
                        <%--<asp:Button ID="BtnCancelSetupForm" runat="server" Text="Cancel" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" OnClick="BtnCancelSetupForm_Click" />--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
