<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="purchases_summary_receipt.aspx.cs" Inherits="Alex.pages.purchases_summary_receipt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="iQ is a suite of integrated education management applications that facilitates the management of your school." />
   

    <link rel="shortcut icon" href="../images/favicon.png" />
    <title>iQ</title>

    <link href="../scripts/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../scripts/font-awesome/css/font-awesome.css" rel="stylesheet" />


    <link href="../scripts/css/animate.css" rel="stylesheet" />
    <link href="../scripts/css/style.css" rel="stylesheet" />

</head>
<body>
    <script type="text/javascript">
        function printDiv(print) {
            var printContents = document.getElementById(print).innerHTML;
            var originalContents = document.body.innerHTML;
            //document.body.innerHTML = printContents;
            document.body.innerHTML = "<html><head><title></title></head><body>" + printContents + "</body>";
            var ButtonControl = document.getElementById("btnPrint");
            var ButtonCloseWindow = document.getElementById("btnCloseWindow");
            ButtonControl.style.visibility = "hidden"; ButtonCloseWindow.style.visibility = "hidden";
            window.print();
            document.body.innerHTML = originalContents;
        }
    </script>
    <form id="form1" runat="server">
        <div id="print">
            <div class="ibox wrapper wrapper-content float-e-margins panel panel-primary">
                <div class="ibox-title panel-heading">
                    <h5>Purchases Summary PrintOut</h5>
                    <div class=" pull-right">
                        <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn btn-success fa fa-print" OnClientClick="printDiv('print')" />
                        <asp:Button ID="btnCloseWindow" runat="server" Text="Close Tab" CssClass="btn btn-danger fa fa-print" OnClick="btnCloseWindow_Click" />
                    </div>
                </div>

                <div class="ibox-content p-xl">
                    <div class="row">
                        <div class="col-md-7 col-lg-offset-1">
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
                            </div>
                        </div>

                        <div id="divPayInfo" runat="server" class="col-lg-6  col-lg-offset-1">
                            <hr class="hr-line-solid" />
                            <h3>Purchases Summary</h3>
                           <asp:GridView ID="GridViewPurchasesSummary" runat="server" AutoGenerateColumns="False" ShowFooter="false" DataKeyNames="purch_id"
                                CssClass="table table-striped table-bordered table-hover dataTables-example">
                                <Columns>
                                    <asp:BoundField DataField="item_name" HeaderText="Sale Item" ReadOnly="true" />
                                    <asp:BoundField DataField="Price#" HeaderText="Price" ReadOnly="true" />
                                    <asp:TemplateField HeaderText="Additional Charges">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="tbAdditionalfee" runat="server" CssClass="input-group" Text='<%# Bind("additional_fee") %>'></asp:TextBox>

                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAdditionalfee" runat="server" Text='<%# Bind("additional_fee") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Purchase Date">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="tbPurchaseDate" runat="server" rel="date" type="text" CssClass="datepick input-group date" Text='<%# Bind("Purchase_Date") %>'></asp:TextBox>

                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPurchaseDate" runat="server" Text='<%# Bind("Purchase_Date") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="tbquantity" runat="server" Text='<%# Bind("quantity") %>'></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="revtbquantity" ControlToValidate="tbquantity" runat="server"
                                                ErrorMessage="Only Numbers allowed" ForeColor="Red" ValidationExpression="^\d+([,\.]\d{1,2})?$"></asp:RegularExpressionValidator>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Bind("quantity") %>'></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Total Price Due#" HeaderText="Total Price Due" ReadOnly="true" />

                                    <asp:BoundField DataField="Total Amount_Paid" HeaderText="Total paid" ReadOnly="true" />
                                    <asp:TemplateField HeaderText="Total Item Amount Outstanding" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="tbBalance" runat="server" Text='<%# Bind("Balance") %>' ReadOnly="true"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblBalance" runat="server" Text='<%# Bind("Balance") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <div>
                                                <asp:Label ID="lblTotalBalance" runat="server" />
                                            </div>
                                        </FooterTemplate>
                                        <HeaderStyle CssClass="hidden"></HeaderStyle>
                                        <ItemStyle CssClass="hidden"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="SBalance" HeaderText="Balance" ReadOnly="true" />
                                    <asp:BoundField DataField="purch_id" InsertVisible="False" ReadOnly="True" SortExpression="purch_id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                        <HeaderStyle CssClass="hidden"></HeaderStyle>
                                        <ItemStyle CssClass="hidden"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="msi_id" HeaderText="Msi Id" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                        <HeaderStyle CssClass="hidden"></HeaderStyle>
                                        <ItemStyle CssClass="hidden"></ItemStyle>
                                    </asp:BoundField>
                               

                                </Columns>
                                <%-- <FooterStyle BackColor="#f3f3f4" ForeColor="Black" HorizontalAlign="Left" />--%>
                            </asp:GridView>
                            <asp:Label ID="lblZeroRecords" runat="server" Text="" CssClass="font-bold"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <!-- Mainly scripts -->
        <script src="../../scripts/js/jquery-2.1.1.js"></script>
        <script src="../../scripts/js/bootstrap.min.js"></script>
        <script src="../../scripts/js/plugins/metisMenu/jquery.metisMenu.js"></script>
        <script src="../../scripts/js/plugins/slimscroll/jquery.slimscroll.min.js"></script>
        <script src="../../scripts/js/plugins/jeditable/jquery.jeditable.js"></script>

        <!-- Flot -->
        <script src="../../scripts/js/plugins/flot/jquery.flot.js"></script>
        <script src="../../scripts/js/plugins/flot/jquery.flot.tooltip.min.js"></script>
        <script src="../../scripts/js/plugins/flot/jquery.flot.spline.js"></script>
        <script src="../../scripts/js/plugins/flot/jquery.flot.resize.js"></script>
        <script src="../../scripts/js/plugins/flot/jquery.flot.pie.js"></script>
        <script src="../../scripts/js/plugins/flot/jquery.flot.symbol.js"></script>
        <script src="../../scripts/js/plugins/flot/curvedLines.js"></script>

        <!-- Peity -->
        <script src="../../scripts/js/plugins/peity/jquery.peity.min.js"></script>
        <script src="../../scripts/js/demo/peity-demo.js"></script>

        <!-- Custom and plugin javascript -->
        <script src="../../scripts/js/custom.js"></script>
        <script src="../../scripts/js/plugins/pace/pace.min.js"></script>

        <!-- jQuery UI -->
        <script src="../../scripts/js/plugins/jquery-ui/jquery-ui.min.js"></script>

        <!-- GITTER -->
        <script src="../../scripts/js/plugins/gritter/jquery.gritter.min.js"></script>

        <!-- Sparkline -->
        <script src="../../scripts/js/plugins/sparkline/jquery.sparkline.min.js"></script>

        <!-- Sparkline demo data  -->
        <script src="../../scripts/js/demo/sparkline-demo.js"></script>

        <!-- ChartJS-->
        <script src="../../scripts/js/plugins/chartJs/Chart.min.js"></script>

        <!-- Toastr -->
        <script src="../../scripts/js/plugins/toastr/toastr.min.js"></script>

    </form>
</body>
</html>