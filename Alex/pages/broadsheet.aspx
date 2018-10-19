<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="broadsheet.aspx.cs" Inherits="Alex.pages.broadsheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">



    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10 h1">
            Broad Sheet
        </div>
    </div>

    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row">
            <div class="col-lg-12">
                <div class="float-e-margins">
                    <div class="col-md-12">

                        <div class=" col-lg-2">
                            <label>Academic Year</label><br />
                            <asp:DropDownList ID="ddlAcademicYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-lg-2">
                            <label>Term</label><br />
                            <asp:DropDownList ID="ddlTerm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTerm_SelectedIndexChanged"></asp:DropDownList>
                        </div>

                        <div class="col-lg-2">
                            <label>Class:</label><br />
                            <asp:DropDownList ID="ddlFormClass" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFormClass_SelectedIndexChanged"></asp:DropDownList>
                        </div>

                    </div>
                </div>

            </div>
        </div>
        <br />
        <asp:Label ID="lblSelectedText" runat="server"></asp:Label>
        <%--<div style="width: 100%; height: 400px; overflow: auto">--%>
        <asp:GridView ID="GridViewListOfStudents" runat="server"  CssClass="table table-striped table-bordered"
            AutoGenerateColumns="true" OnRowDataBound="GridViewListOfStudents_RowDataBound"   >
            <Columns>
                <%--<asp:BoundField DataField="person_id" HeaderText="PID" />
                <asp:BoundField DataField="fullname" HeaderText="Name" />
             
                 <asp:BoundField DataField="Biology" HeaderText="Biology" />
                 <asp:BoundField DataField="Health" HeaderText="Maths" />
                 <asp:BoundField DataField="ICT" HeaderText="ICT" />--%>
            </Columns>
      <HeaderStyle  Font-Size="XX-Small" CssClass="headerText" />
        </asp:GridView>

<%--</div>--%>
        <asp:Label ID="lblZeroRecords" runat="server" Text=""></asp:Label>
    </div>

    

<style>
   .headerText
{
     /*white-space: pre-wrap;
    word-wrap: break-word;
   width: 1px;
    line-height: 100%;*/
    writing-mode: vertical-lr;
}
</style>


</asp:Content>
