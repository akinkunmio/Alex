<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="profit_loss.aspx.cs" Inherits="Alex.pages.student_reports.profit_loss" %>

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
    <div class="row wrapper white-bg">
        <div class="col-lg-9 h1">
            Account Reports: <small>Profit & Loss</small>
        </div>
        <div class="col-md-2 ">
            <a href="../reports.aspx">
                <br />
                <asp:Label runat="server" Text="&nbsp;Back to Reports" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"></asp:Label>
            </a>
        </div>
        <br />
    </div>
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
                    <asp:Button ID="BtnSearchProfitLoss" runat="server" Text="GO" type="search" CssClass="btn-primary" OnClick="BtnSearchProfitLoss_Click" />
                </div>
                <div class="col-lg-offset-9">
                    <asp:Button ID="btnPrint" runat="server" Text="Print" Visible="false" cssClass="btn btn-primary fa fa-print" OnClientClick="printDiv('print')" />
                </div>
            </div>
        </div>
        <br />
       
        <div id="print">
            <div class="ibox float-e-margins panel panel-primary">
                <div class="ibox-title panel-heading">
                    <h5>Profit & Loss</h5>
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
                                <h2>Profit & Loss</h2>
                               <%-- <label>Your business's income less its day-to-day running costs over a given period of time</label><br />--%>
                                <label>From</label>
                                :<asp:Label ID="lblDateSelected" runat="server" Text=""></asp:Label>
                                <label>To</label>
                                :<asp:Label ID="lblEndDateSelected" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                     </div>
                   
                    <br />
                    <br />
                    <div class="col-lg-12">

                       <%-- <div style="width:840px;">
                             <strong>Turnover</strong>
                            <strong><asp:Label ID="lblTurnover" runat="server" CssClass="col-lg-offset-10" ></asp:Label></strong>
                        </div>--%>
                        <asp:GridView ID="GridViewTrunOverSummary" runat="server" AutoGenerateColumns="False"  Width="840px"
                        CssClass="" ShowHeader="false" GridLines="None" Font-Bold="true">
                        <Columns>
                            <asp:BoundField DataField="column_name" HeaderText="source" />
                            <asp:BoundField DataField="turnover" HeaderText="Total" ItemStyle-Width="300px"  ItemStyle-HorizontalAlign="Right" />
                       </Columns>
                       </asp:GridView>
                        <asp:GridView ID="GridViewTrunOver" runat="server" AutoGenerateColumns="False" Width="840px"
                        CssClass="table table-striped table-bordered table-hover dataTables-example" ShowHeader="false">
                        <Columns>
                            <asp:BoundField DataField="source" HeaderText="Source"/>
                            <asp:BoundField DataField="total_amount" HeaderText="Total" ItemStyle-Width="300px"   ItemStyle-HorizontalAlign="Right"    />
                        </Columns>
                    </asp:GridView>
                    </div>

                    <hr />

                    <div class="col-lg-12">
                      
                        <%--<div style="width:840px;">
                            <strong>Running Costs / Expenses</strong>
                            <strong><asp:Label ID="lblRunningCost" runat="server" CssClass="col-lg-offset-6" ></asp:Label></strong>
                        </div>--%>
                      <asp:GridView ID="GridViewExpensesSummary" runat="server" AutoGenerateColumns="False"  Width="840px"
                        CssClass="" ShowHeader="false" GridLines="None" Font-Bold="true">
                        <Columns>
                            <asp:BoundField DataField="cost_type" HeaderText="cost" />
                            <asp:BoundField DataField="sum_total" HeaderText="Total" ItemStyle-Width="300px"/>
                       </Columns>
                       </asp:GridView>

                      <asp:GridView ID="GridViewExpenses" runat="server" AutoGenerateColumns="False"  Width="840px"
                        CssClass="table table-striped table-bordered table-hover dataTables-example" ShowHeader="false">
                        <Columns>
                            <asp:BoundField DataField="expenses_type" HeaderText="Expenses Category" />
                            <asp:BoundField DataField="sum_total" HeaderText="Total" ItemStyle-Width="300px"/>
                        </Columns>
                    </asp:GridView>
                    <asp:Label ID="lblZeroRecords" runat="server" Text=""></asp:Label>
                    </div>

                    
                    <hr />


                   <div class="col-lg-12">
                     
                       <%--<div style="width:840px;">
                             <strong>Staff Costs</strong>
                            <strong><asp:Label ID="lblStaffCost" runat="server" CssClass="col-lg-offset-7" Text="₦0"></asp:Label></strong>
                        </div>--%>
                       <asp:GridView ID="GridViewStaffCostTotal" runat="server" AutoGenerateColumns="False"  Width="840px"
                        CssClass="" ShowHeader="false" GridLines="None" Font-Bold="true">
                        <Columns>
                            <asp:BoundField DataField="cost_type" HeaderText="Cost Category"/>
                            <asp:BoundField DataField="total_amount" HeaderText="Total"  ItemStyle-Width="300px"/>
                       </Columns>
                       </asp:GridView>
                        <asp:GridView ID="GridViewStaffCost" runat="server" AutoGenerateColumns="False" Width="840px"
                        CssClass="table table-striped table-bordered table-hover dataTables-example" ShowHeader="false">
                        <Columns>
                            <asp:BoundField DataField="cost_type" HeaderText="Cost Category"/>
                            <asp:BoundField DataField="total_amount" HeaderText="Total"  ItemStyle-Width="300px"/>
                        </Columns>
                    </asp:GridView>
                    </div>


                   
                     <hr class="line_break"/>
                  <div style="width:896px;">
                   <strong>Operating Profit/loss for this period:<asp:Label ID="lblProfit" runat="server" CssClass="col-lg-offset-7"></asp:Label></strong>
                  </div>





                </div>
            </div>
 </div>
   </div>

<style>
    .line_break {
    
  border: 0;
  clear:both;
  display:block;
  width: 96%;               
  background-color:rgba(0, 0, 0, 0.23);
  height: 1px;
}
</style>
   

</asp:Content>
