<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="student_duplicate_profiles.aspx.cs" Inherits="Alex.pages.student_reports.student_duplicate_profiles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
    <div class="row wrapper white-bg">
        <div class="col-lg-9 h1">
            Student Reports: <small>Identical or Duplicate Profiles</small>
        </div>
        <div class="col-md-2 ">
            <a href="../reports.aspx">
                <br />
                <asp:Label runat="server" Text="&nbsp;Back to Reports" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"></asp:Label>
            </a>
        </div> <asp:Button ID="btnPrint" runat="server" Text="Print" Visible="false" class="btn btn-primary fa fa-print" OnClientClick="printDiv('print')" />
        <br />
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
       <div id="print">
            <div id="DivStudents" runat="server"  class="ibox float-e-margins panel panel-primary">
                <div class="ibox-title panel-heading">
                    <h5>Identical or Duplicate Profiles </h5>
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
                                <h2>Identical or Duplicate Profiles</h2>
                            </div>
                        </div>
                     </div>
                   <hr />
                    <asp:GridView ID="GridViewDuplicateStudents" runat="server" AutoGenerateColumns="False"
                        CssClass="table table-striped table-bordered table-hover dataTables-example">
                        <Columns>
                            <asp:BoundField DataField="Last_Name" HeaderText="Last Name" />
                            <asp:BoundField DataField="Middle_Name" HeaderText="Middle Name" />
                            <asp:BoundField DataField="First_Name" HeaderText="First Name" />
                            <asp:BoundField DataField="Date_of_Birth" HeaderText="Date of Birth" NullDisplayText="-" />
                            <asp:BoundField DataField="dupeCount" HeaderText="Duplicate Accounts" NullDisplayText="-" />
                            <asp:BoundField DataField="stu_id" HeaderText="Student ID" />
                       </Columns>
                    </asp:GridView>
                    <br />

                    <asp:Label ID="lblZeroRecords" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

