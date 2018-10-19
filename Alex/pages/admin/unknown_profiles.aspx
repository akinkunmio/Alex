<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="unknown_profiles.aspx.cs" Inherits="Alex.pages.admin.unknown_profiles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-md-3 pull-right">
        <a href="../settings.aspx">
            <asp:Label runat="server" Text="&nbsp;Back to Settings" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"></asp:Label>
        </a>
    </div>
    <h2>List of Unknow Information Profiles</h2>
    <div class=" wrapper-content  animated fadeInRight">


        <asp:GridView ID="GridViewUnknownProfiles" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridViewUnknownProfiles_RowDataBound" ShowFooter="false"
            CssClass="table table-striped table-bordered table-hover dataTables-example" AllowPaging="false">
            <PagerStyle BackColor="#1ab394" ForeColor="white" HorizontalAlign="Center" CssClass="cssPager" />
            <Columns>
                <asp:BoundField DataField="title" HeaderText="Title" />
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLinkPeople" runat="server" Text='<%# Eval("fullname") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="dob" HeaderText="Date of Birth" />
                <asp:BoundField DataField="Gender" HeaderText="Gender" />
                <asp:BoundField DataField="Ethnicity" HeaderText="Ethnicity" />
                <asp:BoundField DataField="p_g_contact_no1" HeaderText="Contact Number" />

            </Columns>
        </asp:GridView>
        <asp:Label ID="lblZeroRecords" runat="server" Text=""></asp:Label>

    </div>


</asp:Content>
