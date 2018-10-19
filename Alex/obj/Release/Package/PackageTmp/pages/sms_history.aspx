<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="sms_history.aspx.cs" Inherits="Alex.pages.student_reports.sms_history" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../scripts/css/pagestyle.css" rel="stylesheet" />
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
        $(function () { $('.datepick').datepicker({ changeMonth: true, changeYear: true, yearRange: '-100:' + new Date().getFullYear(), dateFormat: 'dd/mm/yy' }); });
        function printDiv(print) {
            var printContents = document.getElementById(print).innerHTML;
            var originalContents = document.body.innerHTML;
            //document.body.innerHTML = printContents;
            document.body.innerHTML = "<html><head><title></title></head><body>" + printContents + "</body>";
            window.print();
            document.body.innerHTML = originalContents;
        }

    </script>
    <%-- <div class="row wrapper white-bg">
        <div class="col-lg-9 h1">
           Accounts: <small>SMS history</small>
        </div>
        <div class="col-md-2 ">
            <a href="../reports.aspx">
                <br />
                <asp:Label runat="server" Text="&nbsp;Back to Reports" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"></asp:Label>
            </a>
        </div>
        <br />
    </div>--%>
    <div class="wrapper wrapper-content fadeInRight">

        <div class="row">
            <div class="col-lg-8">
                <div class="float-e-margins">

                    <div class="form-group">
                        <div class="editor-label">
                            <label>From Date</label>
                        </div>
                        <div class="editor-field">
                            <asp:TextBox ID="tbStartDate" runat="server" CssClass="form-control datepick" placeholder="dd/mm/yyyy"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="editor-label">
                            <label>To Date</label>
                        </div>
                        <div class="editor-field">
                            <asp:TextBox ID="tbEndDate" runat="server" CssClass="form-control datepick" placeholder="dd/mm/yyyy"></asp:TextBox>
                        </div>
                    </div>
                    <asp:Button ID="BtnSearchSmsHistory" runat="server" Text="GO" type="search" CssClass="btn-primary" OnClick="BtnSearchSmsHistory_Click" />
                </div>
                <div class="col-lg-offset-9">
                    <asp:Button ID="btnPrint" runat="server" Text="Print" Visible="false" class="btn btn-primary fa fa-print" OnClientClick="printDiv('print')" />
                </div>
            </div>
        </div>
        <br />

        <div id="print">
            <div id="DivStudents" runat="server" visible="false" class="ibox float-e-margins panel panel-primary">
                <div class="ibox-title panel-heading">
                    <h5>SMS History</h5>
                </div>
                <div class="ibox-content p-xl">
                    <div class="row">
                        <div class="col-md-7 col-lg-offset-3">
                            <div class="glyphicon" style="vertical-align: top">
                                <asp:Image ID="imgSchool" runat="server" length="120px" Width="120px" AlternateText="Image" />
                            </div>
                            <div class="text-center glyphicon">
                                <asp:Label ID="lblSchoolName" runat="server" Text="" CssClass="title h1"></asp:Label><br />
                                <asp:Label ID="lblAddress" runat="server" Text="" CssClass="h4"></asp:Label><br />
                                <asp:Label ID="lblCity" runat="server" Text="" CssClass="h4"></asp:Label>,
                                 <asp:Label ID="lblState" runat="server" Text="" CssClass="h4"></asp:Label><br />
                                <asp:Label ID="lblCountry" runat="server" Text="" CssClass="h4"></asp:Label>,
                                 <asp:Label ID="lblPostCode" runat="server" Text="" CssClass="h4"></asp:Label><br />
                                <i class="fa fa-envelope-square"></i>
                                <asp:Label ID="lblEmail" runat="server" Text="" CssClass="h4"></asp:Label>,
                                 <i class="fa fa-phone-square"></i>
                                <asp:Label ID="lblPhoneNo" runat="server" Text="" CssClass="h4"></asp:Label>


                                <br />
                                <br />
                                <h2>SMS History</h2>
                                <label>From</label>
                                :<asp:Label ID="lblDateSelected" runat="server" Text=""></asp:Label>
                                <label>To</label>
                                :<asp:Label ID="lblEndDateSelected" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                    </div>
                   
                    <br />
                    <br />


                    <asp:GridView ID="GridViewSmsHistory" runat="server" AutoGenerateColumns="False" ShowFooter="True"  Width="980px"
                        CssClass="table table-striped table-bordered table-hover dataTables-example" OnRowDataBound="GridViewSmsHistory_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="recipient" HeaderText="Recipient"  />
                            <asp:BoundField DataField="message" HeaderText="Message" ItemStyle-CssClass="breakword" ItemStyle-Wrap="true"  />
                            <asp:BoundField DataField="phone_number" HeaderText="Phone Number" NullDisplayText=" -" ItemStyle-CssClass="breakword" />
                            <asp:TemplateField HeaderText="Total SMS" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                <EditItemTemplate>
                                    <asp:TextBox ID="tbBBalance" runat="server" Text='<%# Bind("count_sms") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblBBalance" runat="server" Text='<%# Bind("count_sms") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div>
                                        <asp:Label ID="lblTotalBBalance" runat="server" />
                                    </div>
                                </FooterTemplate>
                                <HeaderStyle CssClass="hidden"></HeaderStyle>
                                <ItemStyle CssClass="hidden"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="totalCost" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">

                                <ItemTemplate>
                                    <asp:Label ID="lblBalance" runat="server" Text='<%# Bind("sms_total_cost") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div>
                                        <asp:Label ID="lblTotalBalance" runat="server" />
                                    </div>
                                </FooterTemplate>
                                <HeaderStyle CssClass="hidden"></HeaderStyle>
                                <ItemStyle CssClass="hidden"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="count_sms" HeaderText="No of SMS" FooterStyle-CssClass="hidden" />
                            <asp:BoundField DataField="sms_total_cost" HeaderText="Cost" FooterStyle-CssClass="hidden" />
                            <asp:BoundField DataField="created_date" HeaderText="Send Date" />
                            <asp:BoundField DataField="created_by" HeaderText="Sender" ItemStyle-CssClass="breakword" />

                        </Columns>
                        <FooterStyle BackColor="#f3f3f4" ForeColor="Black" HorizontalAlign="Left" />
                    </asp:GridView>
                    <br />

                </div>
            </div>
            <asp:Label ID="lblZeroRecords" runat="server" Text=""></asp:Label>
        </div>
    </div>
    <style>
        .breakword {
            word-wrap: break-word;
            word-break: break-all;
        }
    </style>
</asp:Content>
