<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="setup_assessments_type.aspx.cs" Inherits="Alex.pages.admin.setup_assessments_type" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../scripts/css/pagestyle.css" rel="stylesheet" />
   <div class="col-md-3 pull-right">
            <a href="../settings.aspx">
               <asp:Label runat="server" Text="&nbsp;Back to Settings" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"  ></asp:Label>
            </a>
        </div>
    <h2>Assessment Type Setup</h2>
    <div class=" wrapper-content  animated fadeInRight">
        <div class="row">
            <div class="col-lg-6">
                <div class="ibox">
                    <div role="form" id="form">
                        
                        <div class="form-group">
                            <div class="editor-label">
                                <label>Assessment Type</label><span class="required">*</span>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="tbAssessmentType" runat="server" placeholder="Assessment Type" CssClass="form-control"></asp:TextBox>
                                 <div class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvtbAssessmentType" ErrorMessage="Assessment Type Required" ForeColor="Red" ControlToValidate="tbAssessmentType"
                                                runat="server" Dispaly="Dynamic" />
                                 </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="editor-label">
                                <label>Description</label><span class="required">*</span>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="tbDescription" runat="server" placeholder="Assessment Type Description" CssClass="form-control"></asp:TextBox>
                                <div class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvtbDescription" ErrorMessage="Description Required" ForeColor="Red" ControlToValidate="tbDescription"
                                                runat="server" Dispaly="Dynamic" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="editor-label">
                                <label>Assessment Max Grade</label><span class="required">*</span>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="tbAssesssmentMaxGrade" runat="server" placeholder="Assessment Max Grade" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                 <div class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvtbAssesssmentMaxGrade" ErrorMessage="Max Grade Required" ForeColor="Red" ControlToValidate="tbAssesssmentMaxGrade"
                                                runat="server" Dispaly="Dynamic" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:Button ID="BtnSaveAssesssmentType" runat="server" Text="Save" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" OnClick="BtnSaveAssesssmentType_Click"/>
                </div>
              </div>
     
        </div>
        <h1>List of Assessments Type</h1>
        <asp:GridView ID="GridViewAssesssmentType" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example"
            OnRowEditing="GridViewAssesssmentType_RowEditing" OnRowCancelingEdit="GridViewAssesssmentType_RowCancelingEdit"
            OnRowUpdating="GridViewAssesssmentType_RowUpdating" OnRowDeleting="GridViewAssesssmentType_RowDeleting">
            <Columns>
                <asp:BoundField DataField="assessment_type_id" HeaderText="Assessment Type  ID" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                     <HeaderStyle CssClass="hidden"></HeaderStyle><ItemStyle CssClass="hidden"></ItemStyle> 
                 </asp:BoundField>
               
                <asp:TemplateField HeaderText="Assessment Type">
                    <EditItemTemplate>
                        <asp:TextBox ID="tbAssessmentType" runat="server" Text='<%# Bind("assessment_type") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblAssessmentType" runat="server" Text='<%# Bind("assessment_type") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description">
                    <EditItemTemplate>
                        <asp:TextBox ID="tbDescription" runat="server" Text='<%# Bind("assessment_name") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("assessment_name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Max Grade ">
                    <EditItemTemplate>
                        <asp:TextBox ID="tbMaxGrade" runat="server" Text='<%# Bind("assessment_max_grade") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblMaxGrade" runat="server" Text='<%# Bind("assessment_max_grade") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               
                <asp:TemplateField ShowHeader="false" HeaderText="Edit">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="Edit" CausesValidation="False" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="lnkUpdate" runat="server" Text="Update" CommandName="Update" CausesValidation="False" />
                        <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel" CommandName="Cancel" CausesValidation="False" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete ?');" Text="Delete"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>
        </asp:GridView>
    </div>
<%--  --Pages Styles--%>
 <style>
.required {
color: #F00;
}
</style>
</asp:Content>