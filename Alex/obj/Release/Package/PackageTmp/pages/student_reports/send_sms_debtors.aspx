<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="send_sms_debtors.aspx.cs" Inherits="Alex.pages.student_reports.send_sms_debtors" %>
<%@ Register TagPrefix="asp" Namespace="Saplin.Controls" Assembly="DropDownCheckBoxes" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="iQ is a suite of integrated education management applications that facilitates the management of your school." />
    <meta name="description" content="iQ is a suite of integrated education management applications that facilitates the management of your school." />
    <link rel="shortcut icon" href="../../images/favicon.png" />
    <title>iQ</title>
    <link href="../../scripts/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../scripts/font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link href="../../scripts/css/animate.css" rel="stylesheet" />
    <link href="../../scripts/css/style.css" rel="stylesheet" />
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
     <script src="http://code.jquery.com/jquery-1.11.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type='text/javascript'>
        $('#spnCharLeft').css('display', 'none');
        var maxLimit = 155;
        $(document).ready(function () {
            $('#<%= tbSmsMessage.ClientID %>').keyup(function () {
                var lengthCount = this.value.length;
                if (lengthCount > maxLimit) {
                    this.value = this.value.substring(0, maxLimit);
                    var charactersLeft = maxLimit - lengthCount + 1;
                }
                else {
                    var charactersLeft = maxLimit - lengthCount;
                }
                $('#spnCharLeft').css('display', 'block');
                $('#spnCharLeft').text(charactersLeft + ' Characters left');
            });
        });
     </script>
</head>
<body>
    <form id="form1" runat="server">
      <div class="btn-sm btn-danger m-t-n-xs"><h1>Send SMS to Debtors</h1></div>




    <div class="col-lg-offset-1 col-lg-9 animated fadeInRight" style="height:500px">
        <div class="mail-box-header">
            <h2>Compose SMS
            </h2>
            <div class="col-md-3 pull-right">
               <asp:Label  id="lblSmsAvaliable" runat="server"   Font-Bold="True"></asp:Label>
            </div>
        </div>
        <div class="mail-box">


            <div class="mail-body">

                <div class="form-horizontal">
                    <div class="form-group">
                        <label class="col-sm-2 control-label">Debtors:</label>

                        <div class="col-sm-4">
                             <asp:DropDownCheckBoxes ID="ddcbDebtors" runat="server" CssClass="form-control list" Width="100%"  UseSelectAllNode="true">
                                 <Style SelectBoxWidth="300" DropDownBoxBoxWidth="350" DropDownBoxBoxHeight="230" />
                                 <Texts SelectBoxCaption="Select Debtors" />
                             </asp:DropDownCheckBoxes>
                        </div>
                    </div>
                   
                    <div class="form-group">
                        <label class="col-sm-2 control-label">Message:</label>

                        <div class="col-sm-10">
                            <asp:TextBox ID="tbSmsMessage" runat="server" TextMode="MultiLine" Rows="4" Columns="50"
                                placeholder="Maximum limit: 155 characters"></asp:TextBox><br />
                            <span id="spnCharLeft"></span>

                        </div>

                    </div>
                </div>

            </div>

            
            <div class="mail-body text-right tooltip-demo">
                <asp:Button ID="btnSubmit" runat="server" Text="Send" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" 
                    OnClientClick="return confirm('Please,be aware that all text messages are chargeable.');" OnClick="btnSubmit_Click" />
                 <a href="../../pages/student_reports/student_debtors_year.aspx">
                                <asp:Label runat="server" Text="Cancel" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True"></asp:Label></a>
            </div>
            <div class="clearfix"></div>

              <asp:Label runat="server" ID="lblResponce" Text="" />
              <asp:Label runat="server" ID="lblName" Text="" Visible="false" />
              <asp:Label runat="server" ID="lblPhoneNumbers" Text="" Visible="false" />
              <asp:Label runat="server" ID="lblCount" Text=""  Visible="false" />

        </div>
    </div>
    </form>
</body>
</html>
