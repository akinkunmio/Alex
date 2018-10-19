<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="people.aspx.cs" Inherits="Alex.pages.people" %>

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
        <div class="col-lg-10 h1">
             <asp:Label id="lblProfilePageHeading" runat="server" Text="List of Profiles"></asp:Label>
             <div class="col-md-2 pull-right" id="divBacktoProfiles" runat="server">
                    <a href="people.aspx">
                            <asp:Label runat="server" Text="Back to List of Profiles" CssClass="btn btn-primary"></asp:Label></a>
                    </div>
       </div>

        <div class="col-lg-2">
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row">
            <div class="col-lg-12">
                <div class="float-e-margins">


                    <div class="col-md-4">

                         <asp:Panel ID="panSearch" runat="server" DefaultButton="BtnSearch" Width="100%">
                            <asp:TextBox ID="TbSearch" runat="server" placeholder="Name or Date of Birth" onkeypress="return searchKeyPress(event);"></asp:TextBox>
                            <asp:Button ID="BtnSearch" runat="server" Text="Search" type="search" CssClass="btn-primary" OnClick="BtnSearch_Click1" OnClientClick="return CheckForm()" />
                         </asp:Panel>
                       
                    
                    </div>
                    <div class="col-md-2 pull-right">
                        <a href="add_new.aspx">
                            <asp:Label runat="server" Text="Add New Profile" CssClass="btn btn-primary"></asp:Label></a>
                    </div>

                </div>
            </div>
        </div>
        <div id="divAlpha" runat="server">
            <small>Alphabetic Search</small>
            <div class="AlphabetPager">
                <asp:Repeater ID="rptAlphabets" runat="server">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" Text='<%#Eval("Value")%>' Visible='<%# !Convert.ToBoolean(Eval("Selected"))%>'
                            OnClick="Alphabet_Click" />
                        <asp:Label runat="server" Text='<%#Eval("Value")%>' Visible='<%# Convert.ToBoolean(Eval("Selected"))%>' />
                    </ItemTemplate>
                </asp:Repeater>
            </div>
       </div>
   <br />    
 <asp:GridView ID="GridViewPeople" runat="server" AutoGenerateColumns="False" DataKeyNames="Full Name" OnRowDataBound="GridViewPeople_RowDataBound" ShowFooter="false"
      CssClass="table table-striped table-bordered table-hover dataTables-example" AllowPaging="false" OnPageIndexChanging="GridViewPeople_PageIndexChanging" PageSize="50">
   <PagerStyle BackColor="#1ab394" ForeColor="white" HorizontalAlign="Center" CssClass="cssPager"/>
     <Columns>
   <%--  <asp:BoundField DataField="title" HeaderText="Title" />--%>
     <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLinkPeople" runat="server" Text='<%# Eval("Full Name") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
   <asp:BoundField DataField="Date of Birth" HeaderText="Date of Birth" />

            </Columns>
        </asp:GridView>

    <%--<div class="col-md-2 pull-right">

        <asp:Button ID="BtnMorePeople" runat="server" Text="More" CssClass="btn btn-primary  full-width" OnClick="BtnMorePeople_Click" />
    </div>--%>

        <asp:Label ID="lblZeroRecords" runat="server" Text=""></asp:Label>
    </div>



<style type="text/css">
.cssPager td
 {
 padding-left: 6px;     
 padding-right: 6px;    
}
.cssPager span { background-color:#4f6b72; font-size:18px;} 

.AlphabetPager a, .AlphabetPager span {
            font-size: 11pt;
            display: inline-block;
            
            line-height: 15px;
            min-width: 22px;
            text-align: center;
            text-decoration: none;
            font-weight: bold;
            padding: 0px 1px 0px 1px;
        }

.AlphabetPager a {
            background-color: #f5f5f5;
            color: #969696;
            border: 1px solid #969696;
            width: 34px;
            height: 18px;
        }

.AlphabetPager span {
            background-color: #A1DCF2;
            color: #000;
            border: 1px solid #3AC0F2;
        }
</style>



</asp:Content>
