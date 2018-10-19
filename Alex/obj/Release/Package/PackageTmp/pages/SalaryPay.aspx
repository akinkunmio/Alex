<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="SalaryPay.aspx.cs" Inherits="Alex.pages.SalaryPay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../scripts/css/pagestyle.css" rel="stylesheet" />
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
        $(function () { $('.datepick').datepicker({ changeMonth: true, changeYear: true, yearRange: '-100:' + new Date().getFullYear(), dateFormat: 'dd/mm/yy', maxDate: new Date() }); });
    </script>

    <h1>Payment Details</h1>
    <asp:DetailsView ID="DetailsViewPayment" runat="server" Height="50px" Width="400px" CssClass="table table-striped table-bordered table-hover dataTables-example" AutoGenerateRows="False">
        <Fields>
            <asp:BoundField DataField="Name" HeaderText="Employee Name" ReadOnly="True"></asp:BoundField>
            <asp:BoundField DataField="Salary" HeaderText="Net Pay" ReadOnly="True"></asp:BoundField>
            <asp:BoundField DataField="TotalSalaryPaid" HeaderText="Total Salary Paid" ReadOnly="True"></asp:BoundField>
            <asp:BoundField DataField="Balance" HeaderText="Outstanding Balance" ReadOnly="True"></asp:BoundField>
        </Fields>
    </asp:DetailsView>
    <asp:Label runat="server" ID="lblPayWarning" CssClass="h4 text-navy" Text="Paying individually would deprive you of the advantage to pay all employees at once with just a click on HR & Payroll Menu."></asp:Label>
    <br />  <br />
    <div id="divPayFields" class="form-group col-lg-8" runat="server">
      <div class="form-group">
            <div class="editor-label">
              <label>Paying Amount<strong class="required">*</strong></label>
            </div>
            <div class="editor-field">
               <asp:TextBox ID="tbPayingAmount" runat="server" placeholder="Enter Amount" CssClass="form-control"></asp:TextBox>
               <div class="pull-right">
                   <asp:RegularExpressionValidator ID="revtbPayingAmount" ControlToValidate="tbPayingAmount" runat="server"
                    ErrorMessage="Only numeric values allowed" ForeColor="Red" ValidationExpression="^\d+([,\.]\d{1,2})?$"></asp:RegularExpressionValidator>
                   <asp:RequiredFieldValidator ID="rfvPayingAmount" ErrorMessage="Amount required" ForeColor="Red" ControlToValidate="tbPayingAmount"
                    runat="server" Dispaly="Dynamic" />
                   <asp:RangeValidator ID="RangeValidatorAmount" runat="server" ErrorMessage="Amount must be greater than zero" ForeColor="Red" 
                        ControlToValidate="tbPayingAmount" MinimumValue="0.01" MaximumValue="999999999.99"></asp:RangeValidator>
              </div>
            </div>
      </div>
      <div class="form-group">
         <div class="editor-label">
            <label>Date<strong class="required">*</strong></label>
         </div>
         <div class="editor-field">
            <asp:TextBox ID="tbDate" runat="server" placeholder="dd/mm/yyyy" CssClass="form-control datepick"></asp:TextBox>
            <div class="pull-right">
                <asp:RequiredFieldValidator ID="rfvtbDate" ErrorMessage=" Date required" ForeColor="Red" ControlToValidate="tbDate"
                    runat="server" Dispaly="Dynamic" />
            </div>
         </div>
      </div>
      <div class="form-group">
            <div class="editor-label">
                <label>Payment Method<strong class="required">*</strong></label><br />(Check Bank details in Payroll)
            </div>
            <div class="editor-field">
                <asp:DropDownList ID="ddlPaymentMethod" runat="server" CssClass="form-control list">
                    <asp:ListItem Value="">Please Select One</asp:ListItem>
                    <asp:ListItem Value="Bank">Bank</asp:ListItem>
                    <asp:ListItem Value="Cash">Cash</asp:ListItem>
                    <asp:ListItem Value="Cheque">Cheque</asp:ListItem>
                    <asp:ListItem Value="Other">Other</asp:ListItem>
                </asp:DropDownList>
                <div class="pull-right">
                    <asp:RequiredFieldValidator ID="rfvddlPaymentMethod" ErrorMessage="Payment Method Required" ForeColor="Red" ControlToValidate="ddlPaymentMethod"
                        runat="server" Dispaly="Dynamic" />
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="editor-label">
                <label>Payment Method Reference</label>
            </div>
            <div class="editor-field">
                <asp:TextBox ID="tbPMReference" runat="server" placeholder="Enter Reference Number" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <asp:Button ID="btnSubmit" runat="server" Text="Pay" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" OnClick="FeePay_Click" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" OnClick="btnCancel_Click" CausesValidation="False" />
        <br />

    </div>

    <asp:Label runat="server" ID="lblResult"></asp:Label><br />
    <br />
    <asp:Button runat="server" ID="btnOK" CssClass="btn btn-sm btn-primary m-t-n-xs" Text="Back to Payroll Summary" OnClick="Result_Click" CausesValidation="False" />




    <style>
        .editor-label .required {
            color: #F00;
        }
    </style>
</asp:Content>
