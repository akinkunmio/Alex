<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="student_attendance_report.aspx.cs" Inherits="Alex.pages.student_reports.student_attendance_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
        /*.ui-datepicker-calendar {
    display: none;
    }*/
    </style>
    <script type="text/javascript">
        $(function () { $('.datepick').prop('readonly', true).datepicker({ changeMonth: true, changeYear: true, yearRange: '-100:' + new Date().getFullYear(), dateFormat: 'dd/mm/yy' }); });
        $(function () {
            $('.DtPker').datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'MM yy',
                onClose: function (dateText, inst) {
                    var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
                    var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
                    $(this).datepicker('setDate', new Date(year, month, 1));
                }
            });
        });
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
            Student Reports: <small>Attendance</small>
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
        <div class="form-group">
            <label>Select Date or Month</label>
            <asp:DropDownList ID="ddlDateOrMonth" runat="server" CssClass="list" AutoPostBack="true" OnSelectedIndexChanged="ddlDateOrMonth_SelectedIndexChanged">
                <asp:ListItem Text="Please Select.." Value="" />
                <asp:ListItem Text="Date" Value="date" />
                <asp:ListItem Text="Month" Value="month" />
            </asp:DropDownList>
            <div class="pull-right">
                <asp:RequiredFieldValidator ID="rfvddlDateOrMonth" ErrorMessage="Required" ForeColor="Red" ControlToValidate="ddlDateOrMonth"
                    runat="server" Dispaly="Dynamic" />
            </div>
        </div>
        <div id="RowFields" runat="server" class="row" visible="false">
            <div class="col-lg-12">
                <div class="float-e-margins">

                    <label id="DivDate" runat="server">Date</label>
                    <asp:TextBox ID="tbAttDate" runat="server" type="text" Placeholder="dd/mm/yyyy" CssClass="datepick" AutoPostBack="true" OnTextChanged="tbAttDate_TextChanged" ></asp:TextBox>


                    <label id="DivMonth" runat="server" >Month</label>
                    <asp:TextBox ID="tbAttMonth" runat="server" type="text" placeholder="month/year" CssClass="DtPker" AutoPostBack="true" OnTextChanged="tbAttMonth_TextChanged" ></asp:TextBox>

                    <label>Status</label>
                    <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                        <asp:ListItem Text="All" Value="All" />
                        <asp:ListItem Text="Present" Value="P" />
                        <asp:ListItem Text="Absent" Value="A" />
                        <asp:ListItem Text="Late" Value="L" />
                        <asp:ListItem Text="Holiday" Value="H" />

                    </asp:DropDownList>
                    <label>Class</label>
                    <asp:DropDownList ID="ddlClass" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged">
                    </asp:DropDownList>

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
                    <h5>Attendance</h5>
                </div>
                <div class="ibox-content p-xl">
                    <div class="row">
                        <div class="col-lg-2">

                            <asp:Image ID="imgSchool" runat="server" length="160px" Width="160px" AlternateText="Image" />
                        </div>
                        <div class="col-lg-8 col-lg-offset-2">
                            <div class="title h1">
                                <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-lg-offset-1">
                                <h3>Attendance</h3>
                                <label>Status</label>:
                                <asp:Label ID="lblStatusSelected" runat="server" Text=""></asp:Label>
                                <label>Class</label>
                                :<asp:Label ID="lblClassSelected" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                    </div>
                    <hr />
                    <asp:GridView ID="GridViewAttendance" runat="server" AutoGenerateColumns="False"
                        CssClass="table table-striped table-bordered table-hover dataTables-example">
                        <Columns>
                            <asp:BoundField DataField="title" HeaderText="Title" />
                            <asp:BoundField DataField="fullname" HeaderText="Name" />
                            <asp:BoundField DataField="term_name" HeaderText="Term" />
                            <asp:BoundField DataField="weekno" HeaderText="Week No" />
                            <asp:BoundField DataField="att_date" HeaderText="Date" />
                        </Columns>
                    </asp:GridView>
                    <br />

                    <asp:Label ID="lblZeroRecords" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
