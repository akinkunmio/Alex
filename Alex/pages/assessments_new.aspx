<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="assessments_new.aspx.cs" Inherits="Alex.pages.assessments_new" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10 h1">
           New Mark Assessments
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
                             <label>Class</label><br />
                             <asp:DropDownList ID="ddlFormClass" runat="server"></asp:DropDownList>
                        </div>
                        <div class="col-lg-2">
                             <label>Subject</label><br />
                             <asp:DropDownList ID="ddlSubject" runat="server"></asp:DropDownList>
                        </div>
                        <div class="col-lg-2">
                             <label></label><br />&nbsp;&nbsp;
                             <asp:Button ID="btnSearchFormClass" runat="server" Text="GO" type="search" CssClass="btn-primary" OnClick="btnSearchFormClass_Click" />
                        </div>
                      
                   
                        
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div id="divEdit" class="pull-right">
            <asp:Button ID="btnEdit" runat="server" Text="Click to mark" CssClass="btn-primary" OnClick="btnEdit_Click" />
            <asp:Button ID="btnUploadAssessments" runat="server" Text="Update" CssClass="btn-primary" OnClick="btnUploadAssessments_Click"/>
        </div>
        <asp:GridView ID="GridViewAssessments" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridViewAssessments_RowDataBound"
            CssClass="table table-striped table-bordered table-hover dataTables-example">
            <Columns>
                <asp:BoundField DataField="assessment_id" HeaderText="assessment_id" InsertVisible="False" ReadOnly="True" SortExpression="assessment_id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="title" HeaderText="Title" />
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLinkPeople" runat="server" Text='<%# Eval("fullname") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Assessment1" ControlStyle-Width="80px">
                    <ItemTemplate>
                       <asp:TextBox ID="tbAssessment1" runat="server" Text='<%# Bind("assessment1")  %>'  Enabled="false"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
               
                <asp:TemplateField HeaderText="Assessment2" ControlStyle-Width="80px">
                    <ItemTemplate>
                       <asp:TextBox ID="tbAssessment2" runat="server" Text='<%# Bind("assessment2") %>' Enabled="false"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Assessment3" ControlStyle-Width="80px">
                    <ItemTemplate>
                        <asp:TextBox ID="tbAssessment3" runat="server" Text='<%# Bind("assessment3") %>' Enabled="false"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Assessment4" ControlStyle-Width="80px">
                    
                    <ItemTemplate>
                        <asp:TextBox ID="tbAssessment4" runat="server" Text='<%# Bind("assessment4") %>' Enabled="false"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Assessment5" ControlStyle-Width="80px">
                    
                    <ItemTemplate>
                         <asp:TextBox ID="tbAssessment5" runat="server" Text='<%# Bind("assessment5") %>' Enabled="false"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Assessment6" ControlStyle-Width="80px">
                    
                    <ItemTemplate>
                        <asp:TextBox ID="tbAssessment6" runat="server" Text='<%# Bind("assessment6") %>' Enabled="false"></asp:TextBox>
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
