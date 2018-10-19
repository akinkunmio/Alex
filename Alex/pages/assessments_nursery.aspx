<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="assessments_nursery.aspx.cs" Inherits="Alex.pages.assessments_nursery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var MaxValue = 250;
            $('table[id*=GridViewAssessments] tbody input[id*=tbAssessment1]').keyup(function (e) {
                $(this).parent().find("span").html("");
                if ($(this).val() >= MaxValue) {
                    $(this).parent().find("span.error").html("Error message");
                    return false;
                }
            });
        });
    </script>
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10 h1">
           Mark Nursery Assessments
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
                            <asp:DropDownList ID="ddlFormClass" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFormClass_SelectedIndexChanged"></asp:DropDownList>
                        </div> 
                        <div class="col-lg-4">
                            <label>Subject</label><br />
                            <asp:DropDownList ID="ddlSubject" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        
                 </div>
          </div>
           
      </div>
    </div>  
             
       <br />
        <div id="divEdit" runat="server">
            <div class="pull-right">
                <asp:Button ID="btnEdit" runat="server" Text="Click to mark" CssClass="btn-primary" OnClick="btnEdit_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn-primary"  OnClick="btnCancel_Click"  Visible="false" />
                <asp:Button ID="btnUploadAssessments" runat="server" Text="Save assessments" CssClass="btn-primary" OnClick="btnUploadAssessments_Click" />
            </div>
            
        </div>

        <asp:GridView ID="GridViewAssessments" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridViewAssessments_RowDataBound"
            CssClass="table table-striped table-bordered table-hover dataTables-example" UseAccessibleHeader="true">
            <Columns>
                <asp:BoundField DataField="assessment_id" HeaderText="assessment_id" InsertVisible="False" ReadOnly="True" SortExpression="assessment_id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
               
                <asp:TemplateField HeaderText="Name" HeaderStyle-Width="190px">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLinkPeople" runat="server" Text='<%# Eval("fullname") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ratings" ControlStyle-Width="200px"  HeaderStyle-Width="50px" ItemStyle-Width="100px">
                    <ItemTemplate>
                       <asp:DropDownList ID="dd1" runat="server" Text='<%# Bind("publish8") %>' Enabled="false" SelectedValue='<%# Bind("publish8") %>' AppendDataBoundItems="true">
                                <asp:ListItem Value="Always" Text="Always"></asp:ListItem>
                                <asp:ListItem Value="Sometimes" Text="Sometimes"></asp:ListItem>
                                <asp:ListItem Value="Never" Text="Never"></asp:ListItem>
                                <asp:ListItem Value="Not Observed" Text="Not Observed"></asp:ListItem>
                                <asp:ListItem Value=""></asp:ListItem>
                            </asp:DropDownList>
                       
                    </ItemTemplate>
                </asp:TemplateField>
     </Columns>
        </asp:GridView>
         <asp:Label ID="lblZeroWeighting" runat="server" Text=""></asp:Label>
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

