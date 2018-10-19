<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="list_of_students.aspx.cs" Inherits="Alex.pages.list_of_students" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   <div class="row wrapper border-bottom white-bg page-heading">
       <div class="col-lg-10 h1">
           List of Registered Students by Year
       </div>
       <div class="col-lg-2">
          <a href="add_new_student.aspx">
             <asp:Label runat="server" Text="Register Student" CssClass="btn btn-primary block full-width m-b"></asp:Label></a>
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
                        <label>Class</label><br />
                        <asp:DropDownList ID="ddlFormClass" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFormClass_SelectedIndexChanged"></asp:DropDownList>
                      </div>
                     <%--  <br />
                     <asp:Button ID="btnSearchAcadmicYear" runat="server" Text="GO" type="search" CssClass="btn-primary" OnClick="btnSearchAcadmicYear_Click" />--%>
                </div>          
           </div>
                 
        </div>
    </div><br />
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
   <br />    
  
    

 <asp:GridView ID="GridViewListOfStudents" runat="server"  CssClass="table table-striped table-bordered table-hover dataTables-example"
          AutoGenerateColumns="False" OnRowDataBound="GridViewListOfStudents_RowDataBound" OnPageIndexChanging="GridViewListOfStudents_PageIndexChanging" AllowPaging="false"
           PageSize="50" ShowFooter="false" >
     <PagerStyle BackColor="#1ab394" ForeColor="white" HorizontalAlign="Center" CssClass="cssPager"/>
         <Columns>
            <%-- <asp:BoundField DataField="title" HeaderText="Title" />--%>
               <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLinkPeople" runat="server" Text='<%# Eval("fullName") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
             
             <asp:BoundField DataField="dob" HeaderText="Date of Birth" />
             <asp:BoundField DataField="form_class" HeaderText="Class" />
             <asp:BoundField DataField="reg_date" HeaderText="Registration Date" />
             <asp:BoundField DataField="status" HeaderText="Registration Status" />
         </Columns>
    </asp:GridView>
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
