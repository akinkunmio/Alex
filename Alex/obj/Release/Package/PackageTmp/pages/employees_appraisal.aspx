<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="employees_appraisal.aspx.cs" Inherits="Alex.pages.employees_appraisal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
   
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10 h1">
            Employees Performance Appraisal
        </div>

        <div class="col-lg-2">
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
      <div class="row">
        <div class="col-lg-12">
           <div class="float-e-margins">
                 <div class="col-md-12">
                        <div class="col-lg-2">
                            <label>Academic Year</label><br />
                            <asp:DropDownList ID="ddlAcademicYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged" ></asp:DropDownList>
                        </div> 
                        <div class="col-lg-2">
                            <label>Term</label><br />
                            <asp:DropDownList ID="ddlTerm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTerm_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-lg-2">
                            <label>Appraisal No</label><br />
                            <asp:DropDownList ID="ddlApprisal" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="ddlApprisal_SelectedIndexChanged">
                                 <asp:ListItem Value="" Selected="True">Select Apprisal</asp:ListItem>
                                    <asp:ListItem>App1</asp:ListItem>
                                    <asp:ListItem>App2</asp:ListItem>
                                 </asp:DropDownList>
                        </div>
                        <%--<div class="col-lg-2">
                           <asp:Button ID="btnSearchFormClass" runat="server" Text="GO" type="search" CssClass="btn-primary" OnClick="btnSearchFormClass_Click" />
                        </div>--%>
                 </div>
          </div>
           
      </div>
    </div>  
             
       <br />
        <div id="divEditUpdate" runat="server">
            <div class="pull-right">
                <asp:Button ID="btnEdit" runat="server" Text="Click to mark" CssClass="btn-primary" OnClick="btnEdit_Click"/>
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn-primary"  Visible="false" OnClick="btnCancel_Click" />
                <asp:Button ID="btnUploadAppraosal" runat="server" Text="Save Appraisal" CssClass="btn-primary" OnClick="btnUploadAppraosal_Click"  />
            </div>
          
        </div>

        <asp:GridView ID="GridViewAppraisal" runat="server" AutoGenerateColumns="False"   DataKeyNames="emp_id"  OnRowDataBound="GridViewAppraisal_RowDataBound"
            CssClass="table table-striped table-bordered table-hover dataTables-example" UseAccessibleHeader="true">
            <Columns>
                <asp:BoundField DataField="pa_id" HeaderText="pa_id" InsertVisible="False" ReadOnly="True"  ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
               <asp:BoundField DataField="emp_id" HeaderText="emp_id" InsertVisible="False" ReadOnly="True"  ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="Name" HeaderStyle-Width="190px">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLinkPeople" runat="server" Text='<%# Eval("fullname") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Notes">
                    <ItemTemplate>
                        <asp:TextBox ID="tbNotes" runat="server"  TextMode="MultiLine" Text='<%# Bind("notes")  %>' width="100%" Enabled="false"></asp:TextBox>
                   </ItemTemplate>
                   <ItemStyle Width="800px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Rating">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlRating" runat="server" Text='<%# Bind("rating") %>' Enabled="false" SelectedValue='<%# Bind("rating") %>' AppendDataBoundItems="true">
                                <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                <asp:ListItem Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                </asp:TemplateField>

               
                
            </Columns>
        </asp:GridView>
        
        <asp:Label ID="lblZeroRecords" runat="server" Text=""></asp:Label>

    </div>



    <style type="text/css">
        .cssPager td {
            padding-left: 6px;
            padding-right: 6px;
        }

        .cssPager span {
            background-color: #4f6b72;
            font-size: 18px;
        }
    </style>

</asp:Content>
