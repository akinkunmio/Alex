<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="admin_password_change.aspx.cs" Inherits="Alex.pages.admin.admin_password_change" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <div class="row wrapper">
        <div class="col-lg-10 h1">
            Please Enter a new password for the user
        </div>
    </div>
 <div class=" wrapper-content  animated fadeInRight">
        <div class="row">
            <div class="col-lg-6">
                <div class="ibox">
                    <div role="form" id="form">
                        <div class="form-group">
                            <div class="editor-label">
                                <label>Email</label>
                            </div>
                            <div class="editor-field">
                                       <asp:Label ID="lblEmail" runat="server" Text="" CssClass="h3"></asp:Label>
                            </div>
                        </div>
                        <div id="compare">
                        <div class="form-group">
                            <div class="editor-label">
                                <label>Password</label>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="tbPass" runat="server" placeholder="Enter Password"  name="tbPass" textmode="Password" CssClass="form-control"></asp:TextBox>
                                <label id="Pass"></label>
                                <%-- <div class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvtbSetupUserPassword" ErrorMessage="Password required" ForeColor="Red" ControlToValidate="tbSetupUserPassword"
                                            runat="server" Dispaly="Dynamic" />
                                    </div>--%>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="editor-label">
                                <label>Confirm Password</label>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="tbPass2" runat="server"  placeholder="Confirm Password" name="tbPass2" textmode="Password" CssClass="form-control"></asp:TextBox>
                                 <label id="Pass2"></label>
                                <%-- <div class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvtbSetupUserConfirmPassword" ErrorMessage="Confirm Password required" ForeColor="Red" ControlToValidate="tbSetupUserConfirmPassword"
                                            runat="server" Dispaly="Dynamic" />
                                    </div>--%>
                                
                            </div>
                              <div class="pull-right">
                                <asp:CompareValidator ID="comparePasswords"
                                    runat="server"
                                    ControlToCompare="tbPass"
                                    ControlToValidate="tbPass2"
                                    ErrorMessage="Your passwords do not match up!" ForeColor="Red"
                                    Display="Dynamic" />
                            </div>
                        </div></div>
                       

                     <asp:Button ID="btnSetupPassword" type="submit"  runat="server" Text="Change" CssClass="btn btn-sm btn-primary m-t-n-xs" OnClick="btnSetupPassword_Click" Font-Bold="True"  />
                     <%--<asp:Button ID="BtnCancelSetupForm" runat="server" Text="Cancel" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" OnClick="BtnCancelSetupForm_Click" />--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
