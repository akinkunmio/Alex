<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="fees.aspx.cs" Inherits="Alex.pages.fees" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10 h1">
            Fee
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
                        </div><br />
                       <%--<asp:Button ID="btnSearchAcadmicYear" runat="server" Text="GO" type="search" CssClass="btn-primary" OnClick="btnSearchAcadmicYear_Click" />--%>
               </div>
                  <%--<div class="pull-right">
                   <a href="admin/setup_fee.aspx" class="btn btn-primary">SETUP FEE</a>
                 </div>--%>
         </div>
      </div>
   </div><br />
     <asp:Label ID="lblZeroRecords" runat="server" Text=""></asp:Label>

     <asp:GridView ID="GridViewFee" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example" style="width:699px; ">
               <Columns>
                   <asp:BoundField DataField="form_name" HeaderText="Class" />
                    <asp:BoundField DataField="amount" HeaderText="Fee" />
               </Columns>
    </asp:GridView>

 </div>
</asp:Content>
