<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="manage_sms.aspx.cs" Inherits="Alex.pages.manage_sms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script type="text/javascript">
        jQuery(function ($) {
            $('#<%=tbSmsRequired.ClientID %>').on('keyup', function () {

                 $('#<%=lblCost.ClientID %>').text(2.40 * $('#<%=tbSmsRequired.ClientID %>').val());

             });
         });
    </script>
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10 h1">
            <asp:Label ID="lblManageUsers" runat="server" Text="Manage SMS"></asp:Label>
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row">
            <div class="col-lg-12">

                <div class="col-md-7 ">
                    <div class="chat-discussion">
                        <h2 class="text-success">Number of SMS currently available to use:
                        <asp:Label ID="lblSmsAvaliable" runat="server"></asp:Label></h2>
                        <h2>Transaction History</h2>
                        <asp:GridView ID="GridViewTransaction" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example">
                            <Columns>
                                <asp:BoundField DataField="sms_acc_id" HeaderText="sms_acc_id" InsertVisible="False" ReadOnly="True" SortExpression="status_id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                                    <ItemStyle CssClass="hidden"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="credit_type" HeaderText="Credit Type" ReadOnly="true" />
                                <asp:BoundField DataField="date_loaded" HeaderText="Date of Transaction" ReadOnly="true" />
                                <asp:BoundField DataField="quantity" HeaderText="Quantity" ReadOnly="true" />

                            </Columns>
                        </asp:GridView>
                        <asp:Label ID="lblZeroTransaction" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-md-5">
                    <div class="chat-users">
                        <h4 class="text-success">Number of SMS credited on 1st of every month:
                        <asp:Label ID="lblInclusiveSms" runat="server"></asp:Label></h4>
                        <div class="users-list">
                            <div class="chat-user">
                                <h3>Birthday Alerts</h3>
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>
                                            <br />
                                            Birthday Alerts Phone No:</label>

                                    </div>
                                    <div class="col-md-2 pull-left">
                                        <asp:GridView ID="GridViewManageSms" runat="server" AutoGenerateColumns="False" CssClass="contact-box"
                                            OnRowEditing="GridViewManageSms_RowEditing"
                                            OnRowCancelingEdit="GridViewManageSms_RowCancelingEdit" OnRowUpdating="GridViewManageSms_RowUpdating" Width="100px" ShowHeader="False">
                                            <Columns>
                                                <asp:BoundField DataField="status_id" HeaderText="status_id" InsertVisible="False" ReadOnly="True" SortExpression="status_id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                                                    <ItemStyle CssClass="hidden"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="Edit" CssClass="btn-sm btn-primary m-t-n-xs" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="lnkUpdate" runat="server" Text="Update" CommandName="Update" />
                                                        <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel" CommandName="Cancel" CausesValidation="false" />
                                                    </EditItemTemplate>
                                                    <ControlStyle CssClass="btn btn-primary" Width="80px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Phone Number">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="tbMobileNo" runat="server" Text='<%# Bind("phone_no") %>'></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvMobileNo" ErrorMessage="*" ForeColor="Red" ControlToValidate="tbMobileNo"
                                                            runat="server" Dispaly="Dynamic" />
                                                        <asp:RegularExpressionValidator ID="revtbMobileNo" ControlToValidate="tbMobileNo" runat="server"
                                                            ErrorMessage="Phone Number should be in 11 digits " ForeColor="Red" ValidationExpression="\d{11}"></asp:RegularExpressionValidator>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPhoneNumber" runat="server" Text='<%# Bind("phone_no") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="phone_no" HeaderText="Phone Number" />--%>
                                            </Columns>
                                        </asp:GridView>

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Enable or Disable<asp:Label runat="server" ID="lblBDA"></asp:Label>
                                        </label>
                                    </div>
                                    <div class="col-md-2 pull-left">
                                        <asp:Button ID="btnBirthdayAlert" runat="server" OnClick="btnBirthdayAlert_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="hr-line-solid"></div>
                            <div class="chat-user">
                                <h3>Payment Alerts</h3>

                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Enable or Disable<asp:Label runat="server" ID="lblPA"></asp:Label>
                                        </label>
                                    </div>
                                    <div class="col-md-2 pull-left">
                                        <asp:Button ID="btnFeeAlert" runat="server" OnClick="btnFeeAlert_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="hr-line-solid"></div>
                            <div class="chat-user">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>
                                            <br />
                                            SMS Alert school Name As: </label>
                                    </div>
                                    <div class="col-md-2 pull-left">
                                        <asp:GridView ID="GridViewSchoolName" runat="server" AutoGenerateColumns="False" CssClass="ibox-content"
                                            OnRowEditing="GridViewSchoolName_RowEditing"
                                            OnRowCancelingEdit="GridViewSchoolName_RowCancelingEdit" OnRowUpdating="GridViewSchoolName_RowUpdating" Width="100px" ShowHeader="False">
                                            <Columns>
                                                <asp:BoundField DataField="status_id" HeaderText="status_id" InsertVisible="False" ReadOnly="True" SortExpression="status_id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                                                    <ItemStyle CssClass="hidden"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="Edit" CssClass="btn-sm btn-primary m-t-n-xs" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="lnkUpdate" runat="server" Text="Update" CommandName="Update" CssClass="btn-sm btn-primary m-t-n-xs" />
                                                        <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel" CommandName="Cancel" CausesValidation="false" CssClass="btn-sm btn-primary m-t-n-xs" />
                                                    </EditItemTemplate>
                                                    <ControlStyle CssClass="btn btn-primary" Width="80px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="School Name">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="tbSchoolName" runat="server" Text='<%# Bind("phone_no") %>'></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvtbSchoolName" ErrorMessage="*" ForeColor="Red" ControlToValidate="tbSchoolName"
                                                            runat="server" Dispaly="Dynamic" />
                                                        <asp:RegularExpressionValidator ID="revtbSchoolName" ControlToValidate="tbSchoolName" runat="server"
                                                            ErrorMessage="School Name should be in 11 characters " ForeColor="Red" ValidationExpression="^[\w\s\.]{2,11}$"></asp:RegularExpressionValidator>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSchoolName" runat="server" Text='<%# Bind("phone_no") %>' Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="phone_no" HeaderText="Phone Number" />--%>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>


                        </div>

                    </div>
                </div>
            </div>
        </div>

        <div class="">
            <h2>Request to purchase additional SMS (Minimum 200 SMS Units @ ₦2.40k each)</h2>
            <label>No of SMS Required :</label>
            <asp:TextBox ID="tbSmsRequired" runat="server" CssClass="form-control-static" ValidationGroup="SmsRequest">
            </asp:TextBox><asp:Button runat="server" ID="btnSmsRequiredSubmit" CssClass="btn alert-success" Text="Request Now" OnClick="btnSmsRequiredSubmit_Click" ValidationGroup="SmsRequest" />
             <asp:RequiredFieldValidator ID="RequiredFieldValidatorSmsRequired" runat="server" ForeColor="red" ValidationGroup="SmsRequest"
                ControlToValidate="tbSmsRequired" ErrorMessage="SMS required."></asp:RequiredFieldValidator>
            <asp:RangeValidator ID="RangeValidatorSmsRequired" runat="server"
                ControlToValidate="tbSmsRequired" ForeColor="red" ValidationGroup="SmsRequest"
                ErrorMessage="Invalid number. Please enter the number between 200 to 50000."
                MaximumValue="50000" MinimumValue="200" Type="Integer"></asp:RangeValidator>
           
           
        </div>
        <div class="text-danger">
            <label>SMS request cost : ₦
                <asp:Label ID="lblCost" runat="server" Text="0"></asp:Label></label>
        </div>



    </div>

    <style type="text/css">
        /*a {
            color: white;
        }*/
        .chat-user a {
            /* color: inherit; */
            color: white;
        }
    </style>
    <asp:Label ID="lblSchoolEmail" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblSchoolName" runat="server" Visible="false"></asp:Label>


</asp:Content>
