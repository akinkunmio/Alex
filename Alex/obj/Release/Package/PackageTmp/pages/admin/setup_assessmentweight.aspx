<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="setup_assessmentweight.aspx.cs" Inherits="Alex.pages.admin.setup_assessmentweight" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../scripts/css/pagestyle.css" rel="stylesheet" />
     <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
     <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
     <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <style>
        .ui-datepicker {
            margin-top: 7px;
            margin-left: -30px;
            margin-bottom: 0px;
            position: absolute;
            z-index: 1000;
        }
    </style>
    <script type="text/javascript">
        // Joe 250118 2229 Make calendar show previous and future years $(function () { $('.datepick').prop('readonly', true).datepicker({ changeMonth: true, changeYear: true, yearRange: '-100:' + new Date().getFullYear(), dateFormat: 'dd/mm/yy' }); });
        $(function () { $('.datepick').prop('readonly', true).datepicker({ changeMonth: true, changeYear: true, yearRange: '-4:+1', dateFormat: 'dd/mm/yy' }); });
    </script>
<div class="wrapper-content">
    <div class="col-md-3 pull-right">
        <a href="../settings.aspx">
            <asp:Label runat="server" Text="&nbsp;Back to Settings" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"></asp:Label>
        </a><br />
        <label>Next Term Start Date</label>
          <asp:GridView ID="GridViewNxtTrmSrtDt" runat="server" AutoGenerateColumns="False" CssClass="ibox-content"
                  OnRowEditing="GridViewNxtTrmSrtDt_RowEditing" OnRowCancelingEdit="GridViewNxtTrmSrtDt_RowCancelingEdit" 
                  OnRowUpdating="GridViewNxtTrmSrtDt_RowUpdating" ShowHeader="False">
                                            <Columns>
                                                <asp:BoundField DataField="ay_term_id" HeaderText="ay_term_id" InsertVisible="False" ReadOnly="True" SortExpression="ay_term_id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                                                    <ItemStyle CssClass="hidden"></ItemStyle>
                                                </asp:BoundField>
                                               
                                                <asp:TemplateField HeaderText="Start Date">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="tbNxtTrmSrtDt" runat="server" Text='<%# Bind("next_term_start_date") %>'  CssClass="datepick input-group date"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvtbSchoolName" ErrorMessage="*" ForeColor="Red" ControlToValidate="tbNxtTrmSrtDt"
                                                            runat="server" Dispaly="Dynamic" />
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNextTermStartDate" runat="server" Text='<%# Bind("next_term_start_date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="Edit" CssClass="btn-primary" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="lnkUpdate" runat="server" Text="Update" CommandName="Update" CssClass="btn-primary" />
                                                        <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel" CommandName="Cancel" CausesValidation="false" CssClass="btn-primary" />
                                                    </EditItemTemplate>
                                                   
                                                </asp:TemplateField>
                                            </Columns>
         </asp:GridView>
    </div> <%--</div>--%>
    <h1>Assessment Weighting & Grades</h1>
   <%-- <div class=" wrapper-content  animated fadeInRight">--%>
        <asp:DropDownList ID="ddlAcademicYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:DropDownList ID="ddlTerm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTerm_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:DropDownList ID="ddlSection" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
        </asp:DropDownList>
       <div id="divCurrentTerm" class="row" runat="server">
            <div class="col-lg-10">
                <h3>NOTE: Please add Weighting according to School Term Assessments, Leave rest of them blank</h3>

                <div class="pull-right" id="divEditUpdate" runat="server">
                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn-primary" OnClick="btnEdit_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn-primary" OnClick="btnCancel_Click" Visible="false" CausesValidation="false"  />
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn-primary" OnClick="btnUpdate_Click" Visible="false" />
                </div>
                <asp:GridView ID="GridViewWeighting" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example"
                    OnRowEditing="GridViewWeighting_RowEditing" OnRowCancelingEdit="GridViewWeighting_RowCancelingEdit"
                    OnRowUpdating="GridViewWeighting_RowUpdating" ShowFooter="true" OnRowDataBound="GridViewWeighting_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="aw_id" HeaderText="aw ID" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                            <HeaderStyle CssClass="hidden"></HeaderStyle>
                            <ItemStyle CssClass="hidden"></ItemStyle>
                            <FooterStyle CssClass="hidden" />
                        </asp:BoundField>
                       <%-- <asp:BoundField DataField="assessment" HeaderText="Assessment" ReadOnly="true" />--%>
                        <asp:TemplateField HeaderText="assessment">
                            <ItemTemplate>
                                <asp:TextBox ID="tbAssessment" runat="server" Text='<%# Bind("assessment") %>'  Enabled="false"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Weighting">
                            <ItemTemplate>
                                <asp:TextBox ID="tbTotalMarks" runat="server" Text='<%# Bind("assessment_weight") %>' DataFormatString="{0:D}" Enabled="false"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="REVtbTotalMarks" ControlToValidate="tbTotalMarks" runat="server" ForeColor="Red"
                                    ErrorMessage="Only Numbers allowed" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Publish">
                            <ItemTemplate>
                                <asp:Button ID="btnPublish" runat="server" CssClass="btn-sm btn-default bg-success m-t-n-xs" OnClick="btnPublish_Click" Text='Publish' ToolTip="Click to Publish or UnPublish" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="publish_name" HeaderText="Publish" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                            <HeaderStyle CssClass="hidden"></HeaderStyle>
                            <ItemStyle CssClass="hidden"></ItemStyle>
                            <FooterStyle CssClass="hidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="published" HeaderText="Published" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                            <HeaderStyle CssClass="hidden"></HeaderStyle>
                            <ItemStyle CssClass="hidden"></ItemStyle>
                            <FooterStyle CssClass="hidden" />
                        </asp:BoundField>

                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblZeroAssessments" runat="server" Text=""></asp:Label>
            </div>
            <div class="col-lg-10">
                <h2>Review Grades</h2>
                <br />
                <asp:GridView ID="GridViewGrade" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example"
                    OnRowEditing="GridViewGrade_RowEditing" OnRowCancelingEdit="GridViewGrade_RowCancelingEdit" OnRowUpdating="GridViewGrade_RowUpdating">
                    <Columns>
                        <asp:BoundField DataField="g_id" HeaderText="Grade ID" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                            <HeaderStyle CssClass="hidden"></HeaderStyle>
                            <ItemStyle CssClass="hidden"></ItemStyle>
                            <FooterStyle CssClass="hidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ayt_id" HeaderText="ay ID" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                            <HeaderStyle CssClass="hidden"></HeaderStyle>
                            <ItemStyle CssClass="hidden"></ItemStyle>
                            <FooterStyle CssClass="hidden" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Grade" ControlStyle-Width="50">
                            <EditItemTemplate>
                                <asp:TextBox ID="tbGrade" runat="server" Text='<%# Bind("grade") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtbGrade" ErrorMessage="Required" ForeColor="Red" ControlToValidate="tbGrade"
                                    runat="server" Dispaly="Dynamic" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblGrade" runat="server" Text='<%# Bind("grade") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Grade Upper Level" ControlStyle-Width="50">
                            <EditItemTemplate>
                                <asp:TextBox ID="tbGradeUpper" runat="server" Text='<%# Bind("grade_upper_level") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtbGradeUpper" ErrorMessage="Required" ForeColor="Red" ControlToValidate="tbGradeUpper"
                                    runat="server" Dispaly="Dynamic" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblGradeUpper" runat="server" Text='<%# Bind("grade_upper_level") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Grade Lower Level" ControlStyle-Width="50">
                            <EditItemTemplate>
                                <asp:TextBox ID="tbGradeLower" runat="server" Text='<%# Bind("grade_lower_level") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtbGradeLower" ErrorMessage="Required" ForeColor="Red" ControlToValidate="tbGradeLower"
                                    runat="server" Dispaly="Dynamic" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblGradeLower" runat="server" Text='<%# Bind("grade_lower_level") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Classification" ControlStyle-Width="200">
                            <EditItemTemplate>
                                <asp:TextBox ID="tbclassification" runat="server" Text='<%# Bind("classification") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtbclassification" ErrorMessage="Required" ForeColor="Red" ControlToValidate="tbclassification"
                                    runat="server" Dispaly="Dynamic" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblclassification" runat="server" Text='<%# Bind("classification") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks" ControlStyle-Width="200">
                            <EditItemTemplate>
                                <asp:TextBox ID="tbremarks" runat="server" Text='<%# Bind("remarks") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtbremarks" ErrorMessage="Required" ForeColor="Red" ControlToValidate="tbremarks"
                                    runat="server" Dispaly="Dynamic" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblremarks" runat="server" Text='<%# Bind("remarks") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="false" HeaderText="Edit">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="Edit" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="lnkUpdate" runat="server" Text="Update" CommandName="Update" />
                                <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel" CommandName="Cancel" CausesValidation="False" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblZeroGrade" runat="server" Text=""></asp:Label>
            </div>
        </div>
        <%--<h2>Assessment Publish  & Grade information for Academic Terms </h2>--%>
       
       
          <div id="divPreviousTerm" class="row" runat="server">
            <div class="col-lg-10">
                <asp:GridView ID="GridViewPublish" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example">
                    <Columns>
                        <asp:BoundField DataField="assessment" HeaderText="Assessment"></asp:BoundField>
                        <asp:BoundField DataField="assessment_weight" HeaderText="Weighting"></asp:BoundField>
                        <asp:BoundField DataField="publish_info" HeaderText="Published"></asp:BoundField>
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblZeroRecords" runat="server" Text=""></asp:Label>
            </div>

            <div class="col-lg-10">
                            <h2>Grades</h2>
                <asp:GridView ID="GridViewYearTermGrade" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example">
                    <Columns>
                        <asp:BoundField DataField="grade" HeaderText="Grade"></asp:BoundField>
                        <asp:BoundField DataField="grade_upper_level" HeaderText="Grade Upper Level"></asp:BoundField>
                        <asp:BoundField DataField="grade_lower_level" HeaderText="Grade Lower Level"></asp:BoundField>
                        <asp:BoundField DataField="classification" HeaderText="Classification"></asp:BoundField>
                        <asp:BoundField DataField="remarks" HeaderText="Remarks"></asp:BoundField>
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblYearTermGrade" runat="server" Text=""></asp:Label>
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
