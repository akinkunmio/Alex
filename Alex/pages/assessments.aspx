<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="assessments.aspx.cs" Inherits="Alex.pages.assessments" %>

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
        <script type="text/javascript">

     function checkTextAreaMaxLength(textBox, e, length) {

            var mLen = textBox["MaxLength"];
            if (null == mLen)
                mLen = length;

            var maxLength = parseInt(mLen);
            if (!checkSpecialKeys(e)) {
                if (textBox.value.length > maxLength - 1) {
                    if (window.event)//IE
                        e.returnValue = false;
                    else//Firefox
                        e.preventDefault();
                }
            }
        }
        </script>

    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10 h1">
            Mark Academic Assessments
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
                        <%--<div class="col-lg-2">
                           <asp:Button ID="btnSearchFormClass" runat="server" Text="GO" type="search" CssClass="btn-primary" OnClick="btnSearchFormClass_Click" />
                        </div>--%>
                 </div>
          </div>
           <%--<div class="col-md-6">
            <h4>Assessment Weighting</h4>
            <label class="btn dim">Term 1 Mid Term Assessment:</label><asp:Label ID="lblAss1" runat="server" Text="Not setup yet" CssClass="btn dim"></asp:Label>
            <label class="btn dim ">Term 1 Exam:</label><asp:Label ID="lblAss2" runat="server" Text="Not setup yet" CssClass="btn   dim"></asp:Label><br />
            <label class="btn dim">Term 2 Mid Term Assessment:</label><asp:Label ID="lblAss3" runat="server" Text="Not setup yet" CssClass="btn   dim"></asp:Label>
            <label class=" btn dim">Term 2 Exam:</label><asp:Label ID="lblAss4" runat="server" Text="Not setup yet" CssClass="btn   dim"></asp:Label><br />
            <label class="btn dim">Term 3 Mid Term Assessment:</label><asp:Label ID="lblAss5" runat="server" Text="Not setup yet" CssClass="btn   dim"></asp:Label>
            <label class=" btn dim">Term 3 Exam:</label><asp:Label ID="lblAss6" runat="server" Text="Not setup yet" CssClass="btn   dim"></asp:Label>
           </div>--%>
      </div>
    </div>  
             
       <br />
        <div id="divEdit" runat="server">
            <div class="pull-right">
                <asp:Button ID="btnEdit" runat="server" Text="Click to mark" CssClass="btn-primary" OnClick="btnEdit_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn-primary"  OnClick="btnCancel_Click"  Visible="false" />
                <asp:Button ID="btnUploadAssessments" runat="server" Text="Save assessments" CssClass="btn-primary" OnClick="btnUploadAssessments_Click" />
            </div>
            <%--<h4>Assesments for Class: <asp:Label ID="lblClass" runat="server" Text=""></asp:Label>&nbsp;&amp;&nbsp;Subject : <asp:Label ID="lblSubject" runat="server" Text=""></asp:Label></h4>--%>
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
                <asp:TemplateField HeaderText="Assessment 1" ControlStyle-Width="95px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="50px" ItemStyle-Width="30px">
                    <ItemTemplate>
                        <asp:TextBox ID="tbAssessment1" runat="server" Text='<%# Bind("assessment1")  %>' Enabled="false"></asp:TextBox>
                       
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Assessment 2" ControlStyle-Width="95px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" ItemStyle-Width="30px">
                    <ItemTemplate>
                        <asp:TextBox ID="tbAssessment2" runat="server" Text='<%# Bind("assessment2") %>' Enabled="false"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Assessment 3" ControlStyle-Width="95px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" ItemStyle-Width="30px">
                    <ItemTemplate>
                        <asp:TextBox ID="tbAssessment3" runat="server" Text='<%# Bind("assessment3") %>' Enabled="false"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Assessment 4" ControlStyle-Width="95px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" ItemStyle-Width="30px">

                    <ItemTemplate>
                        <asp:TextBox ID="tbAssessment4" runat="server" Text='<%# Bind("assessment4") %>' Enabled="false"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Exam" ControlStyle-Width="95px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" ItemStyle-Width="30px">

                    <ItemTemplate>
                        <asp:TextBox ID="tbAssessment5" runat="server" Text='<%# Bind("assessment5") %>' Enabled="false"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
<%-- Joe 010218 2009 --%>
<asp:TemplateField HeaderText="Comment" ControlStyle-Width="300px" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="50px" ItemStyle-Width="30px">
                    <ItemTemplate>
                        <asp:TextBox ID="tbcomment" runat="server" Text='<%# Bind("comment")  %>' Enabled="false"  Height="60px" TextMode="MultiLine" MaxLength="50" onkeyDown="checkTextAreaMaxLength(this,event,'50');"
                                                                            Wrap="True" Font-Names="Arial" Width="400px"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                                <%--<asp:BoundField DataField="updated_by" HeaderText="Marked By" ReadOnly="true"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" ItemStyle-Width="30px"/>--%>
              






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
