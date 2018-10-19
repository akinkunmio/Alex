<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="advanced_search.aspx.cs" Inherits="Alex.pages.advanced_search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row wrapper border-bottom white-bg page-heading">
   <div class="col-lg-10 h1">Advanced Search</div>
</div>

<div class="wrapper wrapper-content animated fadeInRight">
  <div role="form" id="form">
    <div class="row form-group">
      <div class="col-md-6">
          <label class="control-label">First Name</label>
          <asp:TextBox ID="tbAdvancedSearchFName" runat="server" CssClass="form-control"></asp:TextBox>
      </div>
      <div class="col-md-6">
         <label class="control-label">Last Name</label>
         <asp:TextBox ID="tbAdvancedSearchLName" runat="server" CssClass="form-control"></asp:TextBox>
      </div>
    </div>
    <div class="row form-group">
         <div class="col-md-6">
              <label class="control-label">Date Of Birth</label>
              <asp:TextBox ID="tbAdvancedSearchDOB" runat="server" CssClass="form-control"></asp:TextBox>
         </div>
         <div class="col-md-6">
              <label class="control-label">Gender</label>
              <asp:TextBox ID="tbAdvancedSearchGender" runat="server" CssClass="form-control"></asp:TextBox>
         </div>
    </div>
    <div class="row form-group">
        <div class="col-md-2">
              <label class="control-label">Academic Year</label>
              <asp:TextBox ID="tbAdvancedSearchAcademicYear" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-2">
              <label class="control-label">Class Name</label>
              <asp:TextBox ID="tbAdvancedSearchClassName" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
       <div class="col-md-2">
              <label class="control-label">Form Name</label>
              <asp:TextBox ID="tbAdvancedSearchFormName" runat="server" CssClass="form-control"></asp:TextBox>
       </div>

       <div class="col-md-2">
              <label class="control-label">City</label>
              <asp:TextBox ID="tbAdvancedSearchCity" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
   </div>
   <div class="row form-group">
        <div class="col-md-2">
              <label class="control-label">Post Code</label>
              <asp:TextBox ID="tbAdvancedSearchPostCode" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
       <div class="col-md-2">
              <label class="control-label">State</label>
              <asp:TextBox ID="tbAdvancedSearchState" runat="server" CssClass="form-control"></asp:TextBox>
       </div>

       <div class="col-md-2">
              <label class="control-label">Status</label>
              <asp:TextBox ID="tbAdvancedSearchStatus" runat="server" CssClass="form-control"></asp:TextBox>
       </div>
       <div class="col-md-2">
              <label class="control-label">country</label>
              <asp:TextBox ID="tbAdvancedSearchCountry" runat="server" CssClass="form-control"></asp:TextBox>
       </div>

        <div class="col-md-4"><asp:Button ID="BtnAdvancedSearch" runat="server" Text="Search" type="search" CssClass="btn-primary" OnClick="BtnAdvancedSearch_Click" /></div>
  </div>
 </div>

    <asp:GridView ID="GridViewAdvancedSearch" runat="server" AutoGenerateColumns="False"   CssClass="table table-striped table-bordered table-hover dataTables-example" >
        <Columns>
            <asp:BoundField DataField="f_name" HeaderText="First Name" />
            <asp:BoundField DataField="l_name" HeaderText="Last Name" />
            <asp:BoundField DataField="dob" HeaderText="Date Of Birth" />
            <asp:BoundField DataField="gender" HeaderText="Gender" />
            <asp:BoundField DataField="acad_year" HeaderText="Academic Year" />
            <asp:BoundField DataField="class_name" HeaderText="Class Name" />
            <asp:BoundField DataField="form_name" HeaderText="Form Name" />
            <asp:BoundField DataField="lga_city" HeaderText="City" />
            <asp:BoundField DataField="zip_postal_code" HeaderText="Zip Code" />
            <asp:BoundField DataField="state" HeaderText="State" />
            <asp:BoundField DataField="status" HeaderText="Status" />
            <asp:BoundField DataField="country" HeaderText="Country" />
        </Columns>
    </asp:GridView>
</div>
</asp:Content>
