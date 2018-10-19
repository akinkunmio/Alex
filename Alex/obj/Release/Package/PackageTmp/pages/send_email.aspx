<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="send_email.aspx.cs" Inherits="Alex.pages.send_email" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <link href="../scripts/css/bootstrap.min.css" rel="stylesheet" />

    <link href="../scripts/font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link href="../scripts/css/plugins/iCheck/custom.css" rel="stylesheet" />

    <link href="../scripts/css/plugins/summernote/summernote.css" rel="stylesheet" />
    <link href="../scripts/css/plugins/summernote/summernote-bs3.css" rel="stylesheet" />
    <link href="../scripts/css/animate.css" rel="stylesheet" />
    <link href="../scripts/css/style.css" rel="stylesheet" />
    <script src="http://code.jquery.com/jquery-1.11.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type='text/javascript'>
       
        $().ready(function () {
            $('.list').change(function () {
                if (this.value == 'Other Email') {
                    $(".phone").show();
                    var validator = document.getElementById("<%=rfvtbPhoneNumbers.ClientID %>");
                validator.enabled = true;
            }
            else {
                $(".phone").hide();
                var validator = document.getElementById("<%=rfvtbPhoneNumbers.ClientID %>");
                    validator.enabled = false;
                }
            });
        });
    </script>


    <div class="col-lg-12 animated fadeInRight">
        <div class="mail-box-header">
            <h2>Compose Email</h2>
        </div>
        <div class="mail-box">


            <div class="mail-body">

                <div class="">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label class="col-sm-2 control-label">To:</label>

                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddlEmailGroup" runat="server" CssClass="form-control list" AppendDataBoundItems="true">
                                    <asp:ListItem Value="Other Email">Other Email</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group phone">
                            <div class="col-sm-2 control-label">
                                <label>Email<strong class="required">*</strong></label>
                            </div>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbPhoneNumbers" runat="server" CssClass="form-control"
                                    placeholder="Eg: xxxx@gmail.com,xxx@yahoomail.com"></asp:TextBox>

                                <div class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvtbPhoneNumbers" ErrorMessage="Phone Number required" ForeColor="Red" ControlToValidate="tbPhoneNumbers"
                                        runat="server" Dispaly="Dynamic" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-2 control-label">
                                <label>Subject</label>
                            </div>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbSubject" runat="server" CssClass="form-control"
                                    placeholder=""></asp:TextBox>

                                <div class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvtbSubject" ErrorMessage="Subject required" ForeColor="Red" ControlToValidate="tbSubject"
                                        runat="server" Dispaly="Dynamic" />
                                </div>
                            </div>
                        </div>

                    </div>
                </div>


            </div>
            <div class="mail-text h-200">
                <div class="col-sm-10">
                    <asp:TextBox ID="tbBody" runat="server" TextMode="MultiLine" Rows="10" Columns="140"
                        placeholder="Type your Email Message here"></asp:TextBox><br />
                    <span id="spnCharLeft"></span>

                </div>

                <div class="clearfix"></div>
            </div>
            <div class="mail-body text-right tooltip-demo">
                <asp:Button ID="BtnSend" runat="server" Text="Send" CssClass="btn  btn-primary" OnClick="BtnSend_Click" />

            </div>
            <div class="clearfix"></div>



        </div>
        <asp:Label runat="server" ID="lblEmailNumbers" Text="" Visible="false" />
         <asp:Label runat="server" ID="lblName" Text="" Visible="false" />
    </div>

    <script src="../scripts/js/jquery-2.1.1.js"></script>
    <script src="../scripts/js/bootstrap.min.js"></script>
    <script src="../scripts/js/plugins/metisMenu/jquery.metisMenu.js"></script>
    <script src="../scriptsjs/plugins/slimscroll/jquery.slimscroll.min.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="../scripts/js/custom.js"></script>
    <script src="../scripts/js/plugins/pace/pace.min.js"></script>

    <!-- iCheck -->
    <script src="../scripts/js/plugins/iCheck/icheck.min.js"></script>


    <!-- SUMMERNOTE -->

    <script src="../scripts/js/plugins/summernote/summernote.min.js"></script>
    <script>
        $(document).ready(function () {

            $('.summernote').summernote();

        });

    </script>
</asp:Content>
