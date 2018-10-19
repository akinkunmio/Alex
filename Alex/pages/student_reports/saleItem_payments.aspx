<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="saleItem_payments.aspx.cs" Inherits="Alex.pages.student_reports.saleItem_payments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function printDiv(print) {
            var printContents = document.getElementById(print).innerHTML;
            var originalContents = document.body.innerHTML;
            document.body.innerHTML = "<html><head><title></title></head><body>" + printContents + "</body>";
            window.print();
            document.body.innerHTML = originalContents;
        }
    </script>
    <div class="row wrapper white-bg">
        <div class="col-lg-9 h1">
            Accounts (Sales): <small>Recent Sale Item Payments</small>
        </div>
        <div class="col-md-2 ">
            <a href="../reports.aspx">
                <br />
                <asp:Label runat="server" Text="&nbsp;Back to Reports" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"></asp:Label>
            </a>
        </div>
        <br />
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">

        <div class="row">
            <div class="col-lg-12">
                <div class="float-e-margins">
                    <div class="col-lg-4"><label>View</label><br />
                    <asp:DropDownList ID="ddlView" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlView_SelectedIndexChanged">
                        <asp:ListItem Text="Item Sales Payments Received Today" Value="Item Sales Payments Received Today" />
                        <asp:ListItem Text="Item Sales Payments Received This Week" Value="Item Sales Payments Received This Week" />
                        <asp:ListItem Text="Item Sales Payments Received This Month" Value="Item Sales Payments Received This Month"/>
                   </asp:DropDownList>
                         </div>
                   <div class="col-lg-2">
                        <label>Class:</label><br />
                        <asp:DropDownList ID="ddlFormClass" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFormClass_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                   
                    <%--<asp:Button ID="BtnSaleItemPayments" runat="server" Text="GO" type="search" CssClass="btn-primary" OnClick="BtnSaleItemPayments_Click" />--%>
                </div>
                <div class="col-lg-offset-9">
                    <asp:Button ID="btnPrint" runat="server" Text="Print" Visible="false" Cssclass="btn btn-primary fa fa-print" OnClientClick="printDiv('print')" />
                </div>
            </div>
        </div>
        <br />
        <asp:Label ID="lblZeroStudents" runat="server" Text=""></asp:Label>
        <div id="print">
            <div id="DivStudents" runat="server" visible="false" class="ibox float-e-margins panel panel-primary">
                <div class="ibox-title panel-heading">
                    <h5>Recent Sale Item Payments</h5>
                </div>
                <div class="ibox-content p-xl">
                    <div class="row">
                        <div class="col-lg-2">
                           
                                <asp:Image ID="imgSchool" runat="server" length="160px" Width="160px" AlternateText="Image" />
                           </div>
                            <div class="col-lg-8 col-lg-offset-2">
                              <div class="title h1"><asp:Label ID="lblName" runat="server" Text=""></asp:Label></div> 
                                <div class="col-lg-offset-1"> <h3>Recent Sale Item Payments</h3>
                                &nbsp;
                                <asp:Label ID="lblViewSelected" runat="server" Text="" CssClass="font-bold"></asp:Label>
                               
                                </div>
                            </div>
                    
                </div><br /><br />
                <h2>Total Amount Recieved: <asp:Label ID="lblTotalAmountPaid" runat="server"></asp:Label></h2>
                <h2>Bank: <asp:Label ID="lblBank" runat="server"></asp:Label>
                    Cash: <asp:Label ID="lblCash" runat="server"></asp:Label>
                    Cheque: <asp:Label ID="lblCheque" runat="server"></asp:Label>
                    Other: <asp:Label ID="lblOther" runat="server"></asp:Label></h2>
                    <%--Completed:<asp:Label ID="lblCompleted" runat="server"></asp:Label>--%>
              
                <hr />
                <asp:GridView ID="GridViewSaleItemPayments" runat="server" AutoGenerateColumns="False"
                    CssClass="table table-striped table-bordered table-hover dataTables-example">
                    <Columns>
                        <asp:BoundField DataField="Received_Date1" HeaderText="Payment Received Date"/>
                        <asp:BoundField DataField="fullname" HeaderText="Name" />
                        <asp:BoundField DataField="item_name" HeaderText="Item Name" />
                        <asp:BoundField DataField="Price#" HeaderText="Price" />
                        <asp:BoundField DataField="quantity" HeaderText="Quantity" />
                        <asp:BoundField DataField="Total Price Due#" HeaderText="Due" />
                        <asp:BoundField DataField="Total_Amount_Paid" HeaderText="Paid" />
                      <%--  <asp:BoundField DataField="SBalance" HeaderText="Balance" />--%>
                        <asp:BoundField DataField="payment_method" HeaderText="Payment Method" />
                 </Columns>
                </asp:GridView>
                <br />

                <asp:Label ID="lblZeroRecords" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
    </div>
  
</asp:Content>
