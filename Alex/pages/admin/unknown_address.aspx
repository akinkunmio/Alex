<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="unknown_address.aspx.cs" Inherits="Alex.pages.admin.unknown_address" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-md-3 pull-right">
        <a href="../settings.aspx">
            <asp:Label runat="server" Text="&nbsp;Back to Settings" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"></asp:Label>
        </a>
    </div>
    <h2>List of Unknow Information On Profile Address</h2>

    <div class=" wrapper-content  animated fadeInRight">

        <asp:GridView ID="GridViewUnknownAddress" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridViewUnknownAddress_RowDataBound" ShowFooter="false"
            CssClass="table table-striped table-bordered table-hover dataTables-example" AllowPaging="false">
            <PagerStyle BackColor="#1ab394" ForeColor="white" HorizontalAlign="Center" CssClass="cssPager" />
            <Columns>
                <asp:BoundField DataField="title" HeaderText="Title" />
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLinkPeople" runat="server" Text='<%# Eval("fullname") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="address_line_1" HeaderText="Address" />
                <asp:BoundField DataField="lga_city" HeaderText="LGA" />
                <asp:BoundField DataField="state" HeaderText="State" />
                <asp:BoundField DataField="country" HeaderText="Country" />

            </Columns>
        </asp:GridView>
        <asp:Label ID="lblZeroRecords" runat="server" Text=""></asp:Label>

    </div>
</asp:Content>
