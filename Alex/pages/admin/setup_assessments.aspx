<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="setup_assessments.aspx.cs" Inherits="Alex.pages.admin.setup_assessments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../scripts/css/pagestyle.css" rel="stylesheet" />
    <script type="text/javascript">
        function CheckAllEmp(Checkbox) {
            var GridViewCreateAssessments = document.getElementById("<%=GridViewCreateAssessments.ClientID %>");
            for (i = 1; i < GridViewCreateAssessments.rows.length; i++) {
                GridViewCreateAssessments.rows[i].cells[3].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
   </script>
    <script type="text/javascript">
        function Validate_Checkbox() {

            var chks = document.getElementsByTagName('input');
            var hasChecked = false;
            for (var i = 0; i < chks.length; i++) {
                if (chks[i].checked) {
                    hasChecked = true;
                    break;
                }
            }
            if (hasChecked == false) {
                alert("Please select at least one subject");

                return false;
            }

            return true;
        }
</script>
     <div class="col-md-3 pull-right">
            <a href="../settings.aspx">
               <asp:Label runat="server" Text="&nbsp;Back to Settings" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"  ></asp:Label>
            </a>
    </div>
    <h2>View & Assign Subjects to a Class</h2>
    <div class=" wrapper-content  animated fadeInRight">
        <div class="col-lg-12" id="left">
            <div class="ibox">
                <div role="form" id="form">
                   <%-- <div class="col-lg-2">
                        <label>Assessment Type</label><span class="required">*</span><br />
                        <asp:DropDownList ID="ddlAssessmentType" runat="server"></asp:DropDownList>
                        <div class="pull-right">
                            <asp:RequiredFieldValidator ID="rfvddlAssessmentType" ErrorMessage="Assessment Type Required" ForeColor="Red" ControlToValidate="ddlAssessmentType"
                                runat="server" Dispaly="Dynamic" />
                        </div>
                    </div>--%>
                    <div class="col-lg-2">
                        <label>Class</label><span class="required">*</span><br />
                        <asp:DropDownList ID="ddlFormClass" runat="server" ValidationGroup="SubjectAssessmentSetup"></asp:DropDownList>
                        <div class="pull-right">
                            <asp:RequiredFieldValidator ID="rfvddlFormClass" ErrorMessage="Class Required" ForeColor="Red" ValidationGroup="SubjectAssessmentSetup" ControlToValidate="ddlFormClass"
                                runat="server" Dispaly="Dynamic" />
                        </div>

                    </div>
                </div>
            </div>
            <asp:Button ID="BtnSearchAvailSub" runat="server" Text="GO" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" ValidationGroup="SubjectAssessmentSetup" OnClick="BtnSearchAvailSub_Click" />
        </div>
        <div class="row">

            <div class="col-lg-6" id="divCreateSubjects" runat="server" visible="false">
                <div>
                     <h3>Avaliable subjects to assign to class  <asp:Label ID="lblClass" runat="server" Text=""></asp:Label></h3>
                     <asp:Button ID="btnCreateAssessments" runat="server" Text="Assign" CssClass="small blue-bg" OnClick="btnCreateAssessments_Click" OnClientClick="return Validate_Checkbox()" />
                </div>
                <asp:GridView ID="GridViewCreateAssessments" runat="server" CssClass="table table-striped table-bordered table-hover dataTables-example"
                    AutoGenerateColumns="False">

                    <Columns>
                        <asp:TemplateField ItemStyle-Width="40px">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkboxSelect" runat="server" AutoPostBack="true" OnCheckedChanged="chkboxSelect_CheckedChanged" />
                               
                            </HeaderTemplate>

                            <ItemTemplate>
                                <asp:CheckBox ID="chkSubject" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="subject_id" HeaderText="subject_id" InsertVisible="False" ReadOnly="True" SortExpression="app_id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                            <HeaderStyle CssClass="hidden"></HeaderStyle>
                            <ItemStyle CssClass="hidden"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="subject_name" HeaderText="Subject" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblZeroSubjects" runat="server" Text=""></asp:Label>
               
            </div>


            <div class="col-lg-6" id="divListAssessments" runat="server" visible="false">
                <h3>List of subjects offered in class <asp:Label ID="lblClassName" runat="server" Text=""></asp:Label></h3>
               
                <asp:GridView ID="GridViewListAssesssmentType" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example"
                    OnRowDeleting="GridViewListAssesssmentType_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="class_subjects_id" HeaderText="Subject ID" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                            <HeaderStyle CssClass="hidden"></HeaderStyle>
                            <ItemStyle CssClass="hidden"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="subject_name" HeaderText="Subject" />
                        <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete ?');" Text="Delete"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblZeroRecords" runat="server" Text=""></asp:Label>
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
