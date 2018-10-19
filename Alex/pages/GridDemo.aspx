<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="GridDemo.aspx.cs" Inherits="Alex.pages.GridDemo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <asp:GridView ID="gdvauthors" runat="server" AutoGenerateColumns="False"
            BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px"
            CellPadding="4">
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <RowStyle BackColor="White" ForeColor="#003399" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <SortedAscendingCellStyle BackColor="#EDF6F6" />
            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
            <SortedDescendingCellStyle BackColor="#D6DFDF" />
            <SortedDescendingHeaderStyle BackColor="#002876" />
            <Columns>
                <asp:BoundField HeaderText="AuthorId" DataField="AuthorId" />
                <asp:BoundField HeaderText="Author Name" DataField="Name" />
                <asp:BoundField HeaderText="Points Earned" DataField="Points" />
                <asp:TemplateField ShowHeader="false">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtndetails" runat="server" ImageUrl="~/edit.jpg" Height="30px" Width="30px" OnClick="imgbtn_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnlpopup"
            CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="269px" Width="400px" Style="display: none">
            <table width="100%" style="border: Solid 3px #D55500; width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                <tr style="background-color: #D55500">
                    <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger" align="center">Update Author</td>
                </tr>
                <tr style="background-color: #008080; color: #FFFFFF; font-size: large; font-weight: bold">
                    <td align="right" style="width: 45%">Author Id : </td>
                    <td>
                        <asp:Label ID="lblID" runat="server"></asp:Label>
                    </td>
                </tr>

                <tr style="background-color: #33CCFF; color: #FF0000; font-size: large; font-weight: bold">
                    <td align="right">Author Name : </td>
                    <td>
                        <asp:TextBox ID="txtname" runat="server" />
                    </td>
                </tr>
                <tr style="background-color: #FFCCFF; color: #000066; font-size: large; font-weight: bold">
                    <td align="right">Points : 
                    </td>
                    <td>
                        <asp:TextBox ID="txtpoints" runat="server" /></td>
                </tr>


                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnUpdate" CommandName="Update" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
