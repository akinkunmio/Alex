<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="add_new_student.aspx.cs" Inherits="Alex.pages.add_new_student" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function CheckForm() {
            if ($('#<%=TbSearch.ClientID %>').val() == "") {
                 alert('Please enter Name or Date of Birth');
                 return false;
             }
             return true;
        }
    </script> 
    <script>
         function searchKeyPress(e) {// look for window.event in case event isn't passed in
         e = e || window.event;
           if (e.keyCode == 13) {
               document.getElementById('btnSearch').click();
               return false;
             }
             return true;
            }
    </script>
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10 h1">Search For Profiles to Enroll Student</div>
    </div>

    <div class="wrapper wrapper-content animated fadeInRight">
        <div id="DivNotFound" runat="server" class="col-md-8 col-lg-offset-3" visible="false">
            <h3>If you can not find the profile, please add a new profile.
            <a href="add_new.aspx">
                <asp:Label runat="server" Text="Add New Profile" CssClass="btn btn-primary"></asp:Label></a></h3>
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <div class="search-form">
            <div class="input-group">
                <asp:Panel ID="panSearch" runat="server" DefaultButton="BtnSearch" Width="100%">
                    <asp:TextBox ID="TbSearch" runat="server" placeholder="Search by Name or Date of Birth" name="search" CssClass="form-control input-lg" onkeypress="return searchKeyPress(event);"></asp:TextBox>
                </asp:Panel>
               
                <div class="input-group-btn">
                    <asp:Button ID="BtnSearch" runat="server" Text="Search" type="search" CssClass="btn btn-lg btn-primary" OnClick="BtnSearch_Click" OnClientClick="return CheckForm()" />
                </div>
            </div>
        </div>
        <br />
        <br />
        <div id="DivAddProfile" runat="server" class="col-md-6 col-lg-offset-3" visible="false">
            <h3>No Profile found, please add a profile 
            <a href="add_new.aspx">
               <asp:Label runat="server" Text="Add New Profile" CssClass="btn btn-primary"></asp:Label></a></h3>
        </div>


        <asp:GridView ID="GridViewAddNewStudent" runat="server" AutoGenerateColumns="False"
            CssClass="table table-striped table-bordered table-hover dataTables-example" OnRowDataBound="GridViewAddNewStudent_RowDataBound">
            <Columns>
                <asp:BoundField DataField="person_id" HeaderText="Person ID" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
              <%--  <asp:BoundField DataField="title" HeaderText="Title" />--%>
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLinkPeople" runat="server" Text='<%# Eval("Full Name") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Gender" HeaderText="Gender" />
                <asp:BoundField DataField="Date of Birth" HeaderText="Date of Birth" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
