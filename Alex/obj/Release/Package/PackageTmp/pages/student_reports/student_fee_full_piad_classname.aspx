<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="student_fee_full_piad_classname.aspx.cs" Inherits="Alex.pages.student_reports.student_fee_full_piad_classname" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ibox wrapper wrapper-content float-e-margins panel panel-primary">
        <div class="ibox-title panel-heading">
            <h5>Students Outstanding Tuition Fee</h5>
            <div class=" pull-right">
                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-danger fa fa-print" OnClick="btnBack_Click" />
            </div>
        </div>

        <asp:GridView ID="GridViewClassStudents" runat="server" AutoGenerateColumns="False"
            CssClass="table table-striped table-bordered table-hover dataTables-example">
            <Columns>

                <asp:BoundField DataField="name" HeaderText="Name" />
                <asp:BoundField DataField="gender" HeaderText="Gender" />
                <asp:BoundField DataField="total_outstanding" HeaderText="Total Outstanding Fee" />
                <asp:BoundField DataField="class" HeaderText="Class" />


            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
