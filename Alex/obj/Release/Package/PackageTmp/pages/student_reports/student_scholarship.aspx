<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="student_scholarship.aspx.cs" Inherits="Alex.pages.student_reports.student_scholarship" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
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
            Accounts (Tuition Fee): <small>Students on Scholarship/Discount</small>
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
                    <label>Academic Year</label>
                    <asp:DropDownList ID="ddlAcademicYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged">
                    </asp:DropDownList>
                    <label>Term</label>
                    <asp:DropDownList ID="ddlTerm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTerm_SelectedIndexChanged">
                    </asp:DropDownList>
                  <%--  <asp:Button ID="BtnDebtors" runat="server" Text="GO" type="search" CssClass="btn-primary"  OnClick="BtnDebtors_Click" />--%>
                </div>
                <div class="col-lg-offset-9">
                    <asp:Button ID="btnPrint" runat="server" Text="Print" Visible="false" class="btn btn-primary fa fa-print" OnClientClick="printDiv('print')" />
                </div>
            </div>
        </div>
        <br />
        <asp:Label ID="lblZeroStudents" runat="server" Text=""></asp:Label>
        <div id="print">
            <div id="DivStudents" runat="server" visible="false" class="ibox float-e-margins panel panel-primary">
                <div class="ibox-title panel-heading">
                    <h5>Students on Scholarship/Discount</h5>
                </div>
                <div class="ibox-content p-xl">
                    <div class="row">
                        <div class="col-lg-2">

                            <asp:Image ID="imgSchool" runat="server" length="160px" Width="160px" AlternateText="Image" />
                        </div>
                        <div class="col-lg-8 col-lg-offset-2">
                              <div class="title h1"><asp:Label ID="lblName" runat="server" Text=""></asp:Label></div> 
                                <div class="col-lg-offset-1"> <h3>Students on Scholarship/Discount</h3>
                                <label>AcademicYear</label>:
                                <asp:Label ID="lblyearSelected" runat="server" Text=""></asp:Label>
                                <label>Term</label>
                                :<asp:Label ID="lblTermSelected" runat="server" Text=""></asp:Label>
                                </div>
                            </div>

                    </div>


                    <hr />
                    <asp:GridView ID="GridViewScholarship" runat="server" AutoGenerateColumns="False"
                        CssClass="table table-striped table-bordered table-hover dataTables-example">
                        <Columns>
                            <asp:BoundField DataField="fullName" HeaderText="Name" />
                            <asp:BoundField DataField="Form_Class" HeaderText="Class" />
                            <asp:BoundField DataField="School_Fees" HeaderText="School Fee" />
                            <asp:BoundField DataField="Amount_Paid" HeaderText="Scholarship Amount" />
                            <asp:BoundField DataField="Payment_Received_Date" HeaderText="Payment Date" />
                            <asp:BoundField DataField="Payment_ref_receipt" HeaderText="Reference & Receipt" />
                       </Columns>
                    </asp:GridView>
                    <br />

                    <asp:Label ID="lblZeroRecords" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
    </div>
    
</asp:Content>
