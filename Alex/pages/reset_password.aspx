<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reset_password.aspx.cs" Inherits="Alex.pages.reset_password" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="iQ is a suite of integrated education management applications that facilitates the management of your school." />
    <link rel="shortcut icon" href="../images/favicon.png"/>
    <title>iQ</title>

    <link href="../scripts/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../scripts/font-awesome/css/font-awesome.css" rel="stylesheet"/>

    <link href="../scripts/css/animate.css" rel="stylesheet"/>
     <link href="../scripts/css/style.css" rel="stylesheet"/>
   
</head>
<body class="gray-bg"">
    <form id="form1" runat="server">
    <div class="col-md-8">
       
   
   <h1></h1>
    <div class="row wrapper">
        <div class="col-lg-10 h1">
            Please reset your password here for New Password
        </div>
    </div>

    <div class=" wrapper-content  animated fadeInRight">
        <div class="row">
            <div class="col-lg-6">
                <div class="ibox">
                    <div role="form" id="form">
                       <%-- <div class="form-group">
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
                        </div>--%>
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
                                        ErrorMessage="Password must be minimum 5 characters and must include 1 numeric character." ControlToValidate="tbpassword"
                                        ValidationExpression="((?=.*\d)(?=.*[a-z]).{5,20})" ForeColor="Red"
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

                        <label><span class=" text-red">* </span>Password must be minimum 5 characters and must include 1 numeric character</label><br /><br />
                        <asp:Button ID="btnSetupNewPassword" type="button" runat="server" Text="Change" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" OnClick="btnSetupNewPassword_Click" />
                        <%--<asp:Button ID="BtnCancelSetupForm" runat="server" Text="Cancel" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" OnClick="BtnCancelSetupForm_Click" />--%>
                    </div>
                </div>
            </div>
        </div>
    </div>

    </div>
         <!-- Mainly scripts -->
        <script src="../scripts/js/jquery-2.1.1.js"></script>
        <script src="../scripts/js/bootstrap.min.js"></script>
        <script src="../scripts/js/plugins/metisMenu/jquery.metisMenu.js"></script>
        <script src="../scripts/js/plugins/slimscroll/jquery.slimscroll.min.js"></script>
        <script src="../scripts/js/plugins/jeditable/jquery.jeditable.js"></script>

        <!-- Flot -->
        <script src="../scripts/js/plugins/flot/jquery.flot.js"></script>
        <script src="../scripts/js/plugins/flot/jquery.flot.tooltip.min.js"></script>
        <script src="../scripts/js/plugins/flot/jquery.flot.spline.js"></script>
        <script src="../scripts/js/plugins/flot/jquery.flot.resize.js"></script>
        <script src="../scripts/js/plugins/flot/jquery.flot.pie.js"></script>
        <script src="../scripts/js/plugins/flot/jquery.flot.symbol.js"></script>
        <script src="../scripts/js/plugins/flot/curvedLines.js"></script>

        <!-- Peity -->
        <script src="../scripts/js/plugins/peity/jquery.peity.min.js"></script>
        <script src="../scripts/js/demo/peity-demo.js"></script>

        <!-- Custom and plugin javascript -->
        <script src="../scripts/js/custom.js"></script>
        <script src="../scripts/js/plugins/pace/pace.min.js"></script>

        <!-- jQuery UI -->
        <script src="../scripts/js/plugins/jquery-ui/jquery-ui.min.js"></script>

        <!-- GITTER -->
        <script src="../scripts/js/plugins/gritter/jquery.gritter.min.js"></script>

        <!-- Sparkline -->
        <script src="../scripts/js/plugins/sparkline/jquery.sparkline.min.js"></script>

        <!-- Sparkline demo data  -->
        <script src="../scripts/js/demo/sparkline-demo.js"></script>

        <!-- ChartJS-->
        <script src="../scripts/js/plugins/chartJs/Chart.min.js"></script>

        <!-- Toastr -->
        <script src="../scripts/js/plugins/toastr/toastr.min.js"></script>

    </form>
</body>
</html>
