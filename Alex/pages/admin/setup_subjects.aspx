<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="setup_subjects.aspx.cs" Inherits="Alex.pages.admin.setup_subjects" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../scripts/css/pagestyle.css" rel="stylesheet" />
       <div class="col-md-3 pull-right">
            <a href="../settings.aspx">
               <asp:Label runat="server" Text="&nbsp;Back to Settings" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"  ></asp:Label>
            </a>
        </div>
    <h2>Subjects Setup</h2>

    <div class=" wrapper-content  animated fadeInRight">
        <div class="row">
            <div class="col-lg-6">
                <div class="ibox">
                    <div role="form" id="form">
                        <div class="form-group">
                            <div class="editor-label">
                                <label>Subject</label><span class="required">*</span>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="tbSetupSubject" runat="server" placeholder="Enter subject name" ValidationGroup="SubjectSetup" CssClass="form-control" MaxLength="150"></asp:TextBox>
                                 <div class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvtbSetupSubject" ErrorMessage="Subject name Required" ValidationGroup="SubjectSetup" ForeColor="Red" ControlToValidate="tbSetupSubject"
                                                runat="server" Dispaly="Dynamic" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="editor-label">
                                <label>Grading Type</label><span class="required">*</span>
                            </div>
                            <div class="editor-field">
                                <asp:DropDownList runat="server" ID="ddlGrading" ValidationGroup="SetupForm" CssClass="form-control" >
                                <asp:ListItem Value="Numeric Score" Text="Numeric Score"></asp:ListItem>
                                <asp:ListItem Value="Ratings" Text="Ratings"></asp:ListItem>
                                </asp:DropDownList>
                                <div class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvtbSection" ErrorMessage="Grading Required" ValidationGroup="SetupForm" ForeColor="Red" ControlToValidate="ddlGrading"
                                                runat="server" Dispaly="Dynamic" />
                                </div>
                            </div>
                        </div>
                        <asp:Button ID="BtnSaveSetupSubject" runat="server" Text="Save" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" ValidationGroup="SubjectSetup"  OnClick="BtnSaveSetupSubject_Click" />
                    </div>
                </div>
                <h1>List of Subjects </h1>


                <asp:GridView ID="GridViewSubjects" runat="server" AutoGenerateColumns="False"
                    Visible="False" CssClass="table table-striped table-bordered table-hover dataTables-example"
                    OnRowEditing="GridViewSubjects_RowEditing" OnRowCancelingEdit="GridViewSubjects_RowCancelingEdit" 
                    OnRowUpdating="GridViewSubjects_RowUpdating" OnRowDeleting="GridViewSubjects_RowDeleting"> 
                    
                    <Columns>
                        <asp:BoundField DataField="subject_id" HeaderText="Subject Id" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"><HeaderStyle CssClass="hidden"></HeaderStyle>
                        <ItemStyle CssClass="hidden"></ItemStyle> </asp:BoundField>
                       
                        <asp:TemplateField HeaderText="Class">
                            <ItemTemplate>
                                <%#Eval("subject_name")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="tbSubjectName" runat="server" Text='<%# Eval("subject_name") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtbSubjectName" ErrorMessage="Subject name Required" ForeColor="Red" ControlToValidate="tbSubjectName"
                                                runat="server" Dispaly="Dynamic" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Grading Type">
                            <ItemTemplate>
                                <asp:Label ID="lblGradingn" runat="server" Text='<%# Eval("grading_type")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                               <asp:DropDownList ID = "ddlEditGrading" runat="server" Text='<%# Bind("grading_type") %>'   >
                                    <asp:ListItem Value="Numeric Score" Text="Numeric Score"></asp:ListItem>
                                    <asp:ListItem Value="Ratings" Text="Ratings"></asp:ListItem>
                              </asp:DropDownList>
                             <div class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvddlEditGrading" ErrorMessage="Section Required" ForeColor="Red" ControlToValidate="ddlEditGrading"
                                                runat="server" Dispaly="Dynamic" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="Edit"/>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="lnkUpdate" runat="server" Text="Update" CommandName="Update"/>
                                <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel" CommandName="Cancel"  CausesValidation="False" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                              <ItemTemplate>
                                  <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete ?');" Text="Delete"></asp:LinkButton>
                              </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

<%--  --Pages Styles--%>
 <style>
.required {
color: #F00;
}
</style>

</asp:Content>
