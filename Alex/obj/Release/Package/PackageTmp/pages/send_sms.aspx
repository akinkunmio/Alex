<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="send_sms.aspx.cs" Inherits="Alex.pages.admin.send_sms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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

        $().ready(function () {
            $('.list').change(function () {
                if (this.value == 'Other Number') {
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

   <%-- <div class="col-md-3 pull-right">
        <a href="../settings.aspx">
            <asp:Label runat="server" Text="&nbsp;Back to Settings" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"></asp:Label>
        </a>
    </div>--%>

    <h1>Send SMS</h1>




    <div class="col-lg-9 animated fadeInRight">
         
        <div class="mail-box-header">
            <h2>Compose SMS</h2>
            <div class="col-md-3 pull-right">
               <asp:Label  id="lblSmsAvaliable" runat="server"   Font-Bold="True"></asp:Label>
            </div>
        </div>
        <div class="mail-box">


            <div class="mail-body">

                <div class="form-horizontal">
                    <div class="form-group">
                        <label class="col-sm-2 control-label">To:</label>

                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlSmsGroup" runat="server" CssClass="form-control list"  AppendDataBoundItems="true"  ValidationGroup="SMSGroup" >
                                  <asp:ListItem Value="Other Number">Other Number</asp:ListItem>
                             </asp:DropDownList>
                            <div class="pull-right">
                                <asp:RequiredFieldValidator ID="rfvddlSmsGroup" ErrorMessage="Select Group" ForeColor="Red" ControlToValidate="ddlSmsGroup"  ValidationGroup="SMSGroup"
                                    runat="server" Dispaly="Dynamic" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group phone">
                        <div class="col-sm-2 control-label">
                            <label>Phone Number<strong class="required">*</strong></label>
                        </div>
                        <div class="col-sm-6">
                              <asp:TextBox ID="tbPhoneNumbers" runat="server" CssClass="form-control"  ValidationGroup="SMSGroup"
                                placeholder="Eg: 08xxxxxxxx,08xxxxxxxx"></asp:TextBox>
                            
                            <div class="pull-right">
                                <asp:RequiredFieldValidator ID="rfvtbPhoneNumbers" ErrorMessage="Phone Number required" ForeColor="Red" ControlToValidate="tbPhoneNumbers"  ValidationGroup="SMSGroup"
                                    runat="server" Dispaly="Dynamic" />
                            </div>
                            <div class="pull-right">
                                <asp:RegularExpressionValidator ID="revMobileTb" ControlToValidate="tbPhoneNumbers" runat="server"  ValidationGroup="SMSGroup"
                                    ErrorMessage="Phone Number should be in 11 digits " ForeColor="Red" ValidationExpression="^(([0-9]{11,11},)|([0-9]{11,11}))+$"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">Message:</label>

                        <div class="col-sm-6">
                            <asp:TextBox ID="tbSmsMessage" runat="server" TextMode="MultiLine" Rows="4" Columns="50"
                                placeholder="Maximum limit: 155 characters"></asp:TextBox><br />
                            <span id="spnCharLeft"></span>
                            <div class="pull-right">
                                <asp:RequiredFieldValidator ID="rfvtbSmsMsg" ErrorMessage="Message Required" ForeColor="Red" ControlToValidate="tbSmsMessage" ValidationGroup="SMSGroup"
                                    runat="server" Dispaly="Dynamic" />
                            </div>
                        </div>

                    </div>
                </div>

            </div>


            <div class="mail-body text-right tooltip-demo">
                <div class="col-lg-10">
                 <label class="text-center  text-uppercase red-bg  font-bold">Please,be aware that all text messages are chargeable.</label></div>
                 <asp:Button ID="btnSubmit" runat="server" Text="Send" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True"  ValidationGroup="SMSGroup" OnClick="btnSubmit_Click" />
            </div>
            <div class="clearfix"></div>
        </div>
    </div>

    <asp:Label runat="server" ID="lblResponce" Text="" />
    <asp:Label runat="server" ID="lblName" Text="" Visible="false" />
    <asp:Label runat="server" ID="lblPhoneNumbers" Text="" Visible="false" />
   <asp:Label runat="server" ID="lblCount" Text=""  Visible="false" />
</asp:Content>





