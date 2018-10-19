<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="term_wizard.aspx.cs" Inherits="Alex.pages.admin.term_wizard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="iQ is a suite of integrated education management applications that facilitates the management of your school." />
    <link rel="shortcut icon" href="../../images/favicon.png" />
    <title>iQ</title>
    <link href="../../scripts/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../scripts/font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link href="../../scripts/css/animate.css" rel="stylesheet" />
    <link href="../../scripts/css/style.css" rel="stylesheet" />
    <script src="../../scripts/js/jquery-2.1.1.js"></script>
    <script src="../../scripts/js/bootstrap.min.js"></script>
    <script src="../../scripts/js/plugins/metisMenu/jquery.metisMenu.js"></script>
    <script src="../../scripts/js/plugins/slimscroll/jquery.slimscroll.min.js"></script>
    <script src="../../scripts/js/plugins/jeditable/jquery.jeditable.js"></script>
    <link href="../../scripts/css/pagestyle.css" rel="stylesheet" />
    <script src="../../scripts/js/custom.js"></script>
    <script src="../../scripts/js/plugins/pace/pace.min.js"></script>

    <script src="../../scripts/js/plugins/metisMenu/jquery.metisMenu.js"></script>
    <script src="../../scripts/js/plugins/slimscroll/jquery.slimscroll.min.js"></script>


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
        $(function () { $('.datepick').datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); });

    </script>



</head>
<body class="gray-bg">
    <form id="form1" runat="server">
        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="col-lg-10 col-lg-offset-4 h1">
                New Term Set Up Wizard
                 <div class="h5 col-lg-offset-7">
                     <a href="../settings.aspx">
                         <asp:Label runat="server" Text="&nbsp;Back to Settings" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"></asp:Label>
                     </a>
                 </div>
                <div class="h4 col-lg-offset-8">

                    <i class="fa fa-sign-out"></i><a href="../../pages/logout.aspx">Logout</a>

                </div>
            </div>
        </div>

        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="row">
                <div class="col-lg-12">
                    <div class="float-e-margins">
                        <div class="col-md-12 col-lg-offset-2">
                            <asp:Wizard ID="Wizard1" runat="server" DisplayCancelButton="false" OnCancelButtonClick="Wizard1_CancelButtonClick" OnFinishButtonClick="Wizard1_FinishButtonClick"
                                HeaderText="Setup Term " ActiveStepIndex="0" CausesValidation="false">
                                <HeaderStyle BackColor="White" BorderColor="#FFCC66" Font-Size="Large" ForeColor="#CC9900" HorizontalAlign="Center" Font-Bold="True" />
                                <SideBarTemplate>
                                    <asp:DataList ID="SideBarList" runat="server" HorizontalAlign="Justify" RepeatDirection="Horizontal">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="SideBarButton" runat="server" ForeColor="green" OnClientClick="return false" CssClass="btn"></asp:LinkButton>
                                        </ItemTemplate>
                                        <SelectedItemStyle Font-Bold="True" CssClass="yellow-bg" />
                                    </asp:DataList>
                                    </tr><tr>
                                </SideBarTemplate>
                                <StartNavigationTemplate>
                                    <div class="col-md-9 col-lg-offset-4">
                                        <asp:Button ID="btnNext" runat="server" CssClass="btn-group btn-success btn-lg" Text="Next" CommandName="MoveNext" CausesValidation="true" />
                                    </div>

                                </StartNavigationTemplate>
                                <StepNavigationTemplate>
                                    <div class="">
                                        <asp:Button ID="btnPrevious" runat="server"
                                            CssClass=" btn-danger btn-group btn-lg"
                                            Text="Previous"
                                            CommandName="MovePrevious" OnClick="btnPrevious_Click" />

                                        <asp:Button ID="btnNext" runat="server"
                                            CssClass="btn-group btn-success btn-lg"
                                            Text="Next"
                                            CommandName="MoveNext"
                                            CausesValidation="true" OnClick="btnNext_Click" />
                                    </div>

                                </StepNavigationTemplate>
                                <FinishNavigationTemplate>
                                    <asp:Button ID="btnFinishPrevious" runat="server" Text="Previous" CssClass="btn-group btn-danger btn-lg" CommandName="MovePrevious" CausesValidation="false" />
                                    <asp:Button ID="btnFinishFinish" runat="server" Text="Finish" CssClass="btn-group btn-primary btn-lg" CommandName="MoveComplete" />
                                </FinishNavigationTemplate>

                                <%--   <NavigationButtonStyle BackColor="#CCCCCC" BorderColor="#CCFF66" BorderStyle="Ridge" />--%>
                                <WizardSteps>
                                    <asp:WizardStep ID="WizardStep1" runat="server" Title="1.Set Up Term">
                                        <div class=" wrapper-content  animated fadeInRight">
                                            <div class="row" id="divSetupTermForm" runat="server">
                                                <div class="col-lg-8">
                                                    <div class="ibox">
                                                        <div role="form" id="form">
                                                            <div class="form-group">
                                                                <div class="editor-label">
                                                                    <label>Select Academic Year</label><span class="required">*</span>
                                                                </div>
                                                                <div class="editor-field">
                                                                    <asp:DropDownList ID="ddlAcademicYear" runat="server" ValidationGroup="TermSetup" CssClass="form-control">
                                                                    </asp:DropDownList>
                                                                    <div class="pull-right">
                                                                        <asp:RequiredFieldValidator ID="rfvAcademicYear" ErrorMessage=" Academic Year Required" ValidationGroup="TermSetup" ForeColor="Red" ControlToValidate="ddlAcademicYear"
                                                                            runat="server" Dispaly="Dynamic" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="editor-label">
                                                                    <label>Term</label><span class="required">*</span>
                                                                </div>
                                                                <div class="editor-field">
                                                                    <asp:DropDownList runat="server" ID="ddlTermName" CssClass="form-control" ValidationGroup="TermSetup">
                                                                        <asp:ListItem Value="1st" Text="1st"></asp:ListItem>
                                                                        <asp:ListItem Value="2nd" Text="2nd"></asp:ListItem>
                                                                        <asp:ListItem Value="3rd" Text="3rd"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <div class="pull-right">
                                                                        <asp:RequiredFieldValidator ID="rfvtbTermName" ErrorMessage="Term Required" ValidationGroup="TermSetup" ForeColor="Red" ControlToValidate="ddlTermName"
                                                                            runat="server" Dispaly="Dynamic" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="editor-label">
                                                                    <label>Start Date</label><span class="required">*</span>
                                                                </div>
                                                                <div class="editor-field">
                                                                    <asp:TextBox ID="tbStartDate" runat="server" placeholder="dd/mm/yyyy" ValidationGroup="TermSetup" CssClass="form-control datepick"></asp:TextBox>
                                                                    <div class="pull-right">
                                                                        <asp:RequiredFieldValidator ID="rfvtbStartDate" ErrorMessage="Start Required" ValidationGroup="TermSetup" ForeColor="Red" ControlToValidate="tbStartDate"
                                                                            runat="server" Dispaly="Dynamic" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="editor-label">
                                                                    <label>End date</label><span class="required">*</span>
                                                                </div>
                                                                <div class="editor-field">
                                                                    <asp:TextBox ID="tbEndDate" runat="server" placeholder="dd/mm/yyyy" CssClass="form-control datepick" ValidationGroup="TermSetup"></asp:TextBox>
                                                                    <div class="pull-right">
                                                                        <asp:RequiredFieldValidator ID="rfvtbEndDate" ErrorMessage="End Required" ForeColor="Red" ValidationGroup="TermSetup" ControlToValidate="tbEndDate"
                                                                            runat="server" Dispaly="Dynamic" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <asp:Button ID="BtnSaveTerm" runat="server" Text="Save" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" ValidationGroup="TermSetup" OnClick="BtnSaveTerm_Click" />
                                                    </div>
                                                </div>
                                                <div class=" col-md-2">
                                                    <div class="wrapper wrapper-content animated fadeInUp">
                                                        <ul class="notes">
                                                            <li>
                                                                <div>
                                                                    <small>Academic Term</small>
                                                                    <h4>Setup Term,save and verify from the list and scroll down to click Next </h4>
                                                                    <p>Select your academic year, input your term name<strong>(e.g. 1 or Term1)</strong>, start and end date.Once this information has been entered hit save and proceed by clicking next. </p>
                                                                </div>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                            <h1>List of Existing Terms</h1>
                                            <asp:GridView ID="GridViewTerm" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example"
                                                OnRowEditing="GridViewTerm_RowEditing" OnRowCancelingEdit="GridViewTerm_RowCancelingEdit"
                                                OnRowUpdating="GridViewTerm_RowUpdating" OnRowDeleting="GridViewTerm_RowDeleting" OnRowDataBound="GridViewTerm_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="ay_term_id" HeaderText="Term ID" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                        <HeaderStyle CssClass="hidden"></HeaderStyle>
                                                        <ItemStyle CssClass="hidden"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="acad_year" HeaderText="Academic Year " ReadOnly="true" />
                                                    <asp:TemplateField HeaderText="Term">
                                                        <EditItemTemplate>
                                                            <asp:DropDownList runat="server" ID="ddlEditTermName" ValidationGroup="tbTerm" SelectedValue='<%# Bind("term_name") %>'>
                                                                <asp:ListItem Value="1st" Text="1st"></asp:ListItem>
                                                                <asp:ListItem Value="2nd" Text="2nd"></asp:ListItem>
                                                                <asp:ListItem Value="3rd" Text="3rd"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvtbTerm" ErrorMessage="Term Required" ForeColor="Red" ControlToValidate="ddlEditTermName"
                                                                runat="server" Dispaly="Dynamic" />
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTerm" runat="server" Text='<%# Bind("term_name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Start Date">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="tbTermStartDate" runat="server" Text='<%# Bind("ay_term_start_date") %>' CssClass="datepick"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtbTermStartDate" ErrorMessage="Date Required" ForeColor="Red" ControlToValidate="tbTermStartDate"
                                                                runat="server" Dispaly="Dynamic" />
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTermStartDate" runat="server" Text='<%# Bind("ay_term_start_date") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="End Date ">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="tbTermEndDate" runat="server" Text='<%# Bind("ay_term_end_date") %>' CssClass="datepick"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtbTermEndDate" ErrorMessage="Date Required" ForeColor="Red" ControlToValidate="tbTermEndDate"
                                                                runat="server" Dispaly="Dynamic" />
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTermEndDate" runat="server" Text='<%# Bind("ay_term_end_date") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="status" HeaderText="Status" ReadOnly="true"></asp:BoundField>
                                                    <asp:TemplateField ShowHeader="false" HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="Edit" />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:LinkButton ID="lnkUpdate" runat="server" Text="Update" CommandName="Update" />
                                                            <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel" CommandName="Cancel" CausesValidation="False" />
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete ?');" Text="Delete"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnStatus" runat="server" CssClass="btn-sm btn-default blue-skin m-t-n-xs" OnClick="btnStatus_Click" Text='Make Active' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </asp:WizardStep>
                                    <asp:WizardStep ID="WizardStep2" runat="server" Title="2.Set Up Fees">
                                        <div id="divBtnFeeSeutupOptions" runat="server">
                                            <asp:Button ID="btnSetupFeeManual" runat="server" Text="Setup Fee for each class" CssClass="h3 btn-primary" OnClick="btnSetupFeeManual_Click" />
                                            <h2>or </h2>
                                            <asp:Button ID="btnSetupFeeCopyPrevious" runat="server" Text="Copy Fees" CssClass="h3 btn-primary" OnClick="btnSetupFeeCopyPrevious_Click" />
                                        </div>
                                        <div id="divSetupFeeManual" runat="server" visible="false">
                                            <div class=" wrapper-content  animated fadeInRight">
                                                <h2>Setup Fee for each Class</h2>
                                                <div style="width: 799px;" class="row">
                                                    <div class="col-lg-8">
                                                        <br />
                                                        <div class="ibox">
                                                            <div role="form" id="form2">
                                                                <div>
                                                                    <div class="form-group">
                                                                        <div class="editor-label">
                                                                            <label>Academic Year</label>
                                                                        </div>
                                                                        <div class="editor-field">
                                                                            <asp:DropDownList ID="ddlFeeSetupYear" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFeeSetupYear_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                            <div class="pull-right">
                                                                                <asp:RequiredFieldValidator ID="rfvddlFeeSetupYear" ValidationGroup="FeeSetupGroup" ErrorMessage="Year Required" ForeColor="Red" ControlToValidate="ddlFeeSetupYear"
                                                                                    runat="server" Dispaly="Dynamic" />
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group">
                                                                        <div class="editor-label">
                                                                            <label>Term</label>
                                                                        </div>
                                                                        <div class="editor-field">
                                                                            <asp:DropDownList ID="ddlFeeSetupTerm" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFeeSetupTerm_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                            <div class="pull-right">
                                                                                <asp:RequiredFieldValidator ID="rfvddlFeeSetupTerm" ValidationGroup="FeeSetupGroup" ErrorMessage="Term Required" ForeColor="Red" ControlToValidate="ddlFeeSetupTerm"
                                                                                    runat="server" Dispaly="Dynamic" />
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group">
                                                                        <div class="editor-label">
                                                                            <label>Class</label>
                                                                        </div>
                                                                        <div class="editor-field">
                                                                            <asp:DropDownList ID="ddlFeeSetupForm" runat="server" CssClass="form-control">
                                                                            </asp:DropDownList>
                                                                            <div class="pull-right">
                                                                                <asp:RequiredFieldValidator ID="rfvddlFeeSetupForm" ValidationGroup="FeeSetupGroup" ErrorMessage="Class Required" ForeColor="Red" ControlToValidate="ddlFeeSetupForm"
                                                                                    runat="server" Dispaly="Dynamic" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="editor-label">
                                                                            <label>Fee Amount</label>
                                                                        </div>
                                                                        <div class="editor-field">
                                                                            <asp:TextBox ID="tbFeeSetupAmount" runat="server" placeholder="Enter Fee" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                                                            <div class="pull-right">
                                                                                <asp:RequiredFieldValidator ID="rfvtbFeeSetupAmount" ValidationGroup="FeeSetupGroup" ErrorMessage="Fee Required" ForeColor="Red" ControlToValidate="tbFeeSetupAmount"
                                                                                    runat="server" Dispaly="Dynamic" />
                                                                            </div>
                                                                        </div>
                                                                    </div>


                                                                    <asp:Button ID="BtnSaveFeSetup" runat="server" Text="Save" ValidationGroup="FeeSetupGroup" CausesValidation="true" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" OnClick="BtnSaveFeeSetup_Click" />

                                                                </div>
                                                            </div>
                                                        </div>
                                                        <h1>Fee List</h1>
                                                        <asp:GridView ID="GridViewFee" runat="server" DataKeyNames="form_name" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example"
                                                            OnRowEditing="GridViewFee_RowEditing"
                                                            OnRowCancelingEdit="GridViewFee_RowCancelingEdit"
                                                            OnRowUpdating="GridViewFee_RowUpdating"
                                                            OnRowDeleting="GridViewFee_RowDeleting">
                                                            <Columns>
                                                                <asp:BoundField DataField="acad_year" HeaderText="Acad Year" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                                                                    <ItemStyle CssClass="hidden"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="term_name" HeaderText="Term" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                                                                    <ItemStyle CssClass="hidden"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="form_name" HeaderText="Class" ReadOnly="true" />
                                                                <asp:BoundField DataField="fee_id" HeaderText="Fee Id" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                                                                    <ItemStyle CssClass="hidden"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="form_id" HeaderText="FormId" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                                                                    <ItemStyle CssClass="hidden"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="acad_y_term_id" HeaderText="acad_y_term_id" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                                                                    <ItemStyle CssClass="hidden"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Fee ₦">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="tbAmount" runat="server" Text='<%# Bind("amount") %>'></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvtbAmount" ErrorMessage="Fee Required" ForeColor="Red" ControlToValidate="tbAmount"
                                                                            runat="server" Dispaly="Dynamic" />
                                                                        <asp:RegularExpressionValidator ID="revtbAmount" ControlToValidate="tbAmount" runat="server"
                                                                            ErrorMessage="Only Numbers allowed" ForeColor="Red" ValidationExpression="^\d+([,\.]\d{1,2})?$"></asp:RegularExpressionValidator>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <%#Eval("amount")%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Edit">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="Edit" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:LinkButton ID="lnkUpdate" runat="server" Text="Update" CommandName="Update" />
                                                                        <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel" CommandName="Cancel" CausesValidation="False" />
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete ?');" Text="Delete"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                            </Columns>
                                                        </asp:GridView>
                                                        <asp:Label ID="lblZeroRecords" runat="server" Text=""></asp:Label>
                                                    </div>
                                                    <div class=" col-md-2">
                                                        <div class="wrapper wrapper-content animated fadeInUp">
                                                            <ul class="notes">
                                                                <li>
                                                                    <div>
                                                                        <small>Setup Fee</small>
                                                                        <h4>Input the school fees for each of the classes</h4>
                                                                        <p>
                                                                            The drop down for Term will only display new Term if steps 1 have been set up correctly. 
                                                                    No worries if you want to make changes later on- You can do so in the Settings page at any time.<br />
                                                                            <strong>Note:Fees is the total of all mandatory fees (tuition,lesson etc)</strong>
                                                                        </p>
                                                                    </div>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="divSetupFeeCopyPrevious" runat="server" visible="false">
                                            <div class="wrapper wrapper-content animated fadeInRight">
                                                <h2>Copy Fees</h2>
                                                <br />

                                                <div style="width: 799px;" class="row">
                                                    <div class="ibox col-lg-8">

                                                        <h4>From</h4>
                                                        <div class="float-e-margins">
                                                            <div class="col-md-14">
                                                                <div class="col-lg-4">
                                                                    <label>Academic Year</label><br />
                                                                    <asp:DropDownList ID="ddlFromBatchFeeYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFromBatchFeeYear_SelectedIndexChanged"></asp:DropDownList>
                                                                </div>
                                                                <div class="col-lg-4">
                                                                    <label>Term</label><br />
                                                                    <asp:DropDownList ID="ddlFromBatchFeeTerm" runat="server"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <br />
                                                        <h4>To</h4>
                                                        <div class="float-e-margins col-md-14">
                                                            <div class=" col-lg-4">
                                                                <label>Academic Year</label><br />
                                                                <asp:DropDownList ID="ddlToBatchFeeYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlToBatchFeeYear_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                            <div class="col-lg-4">
                                                                <label>Term</label><br />
                                                                <asp:DropDownList ID="ddlToBatchFeeTerm" runat="server"></asp:DropDownList>
                                                            </div>

                                                            <br />

                                                            <asp:Button ID="btnBatchFeeSetup" runat="server" Text="Copy Fee" CssClass="btn-primary" OnClick="btnBatchFeeSetup_Click" ValidationGroup="BatchFeeSetUpButton" />
                                                        </div>
                                                        <div class="col-lg-14 col-lg-offset-1">
                                                            <br />
                                                            <asp:RequiredFieldValidator ID="rfvddlYearBReg" ErrorMessage="Academic Year required" ForeColor="Red" ControlToValidate="ddlToBatchFeeYear" ValidationGroup="BatchFeeSetUpButton"
                                                                runat="server" Dispaly="Dynamic" />
                                                            &nbsp;&nbsp;&nbsp;
                                                       <asp:RequiredFieldValidator ID="rfvddlTermBReg" ErrorMessage="Term field required" ForeColor="Red" ControlToValidate="ddlToBatchFeeTerm" ValidationGroup="BatchFeeSetUpButton"
                                                           runat="server" Dispaly="Dynamic" />
                                                            &nbsp;&nbsp;&nbsp;
            
                                                        </div>

                                                        <h1>Fee List</h1>
                                                        <asp:GridView ID="GridViewBatchFee" runat="server" DataKeyNames="form_name" AutoGenerateColumns="False"
                                                            CssClass="table table-striped table-bordered table-hover dataTables-example">
                                                            <Columns>
                                                                <asp:BoundField DataField="acad_year" HeaderText="Acad Year" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                                                                    <ItemStyle CssClass="hidden"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="term_name" HeaderText="Term" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                                                                    <ItemStyle CssClass="hidden"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="form_name" HeaderText="Class" ReadOnly="true" />
                                                                <asp:BoundField DataField="fee_id" HeaderText="Fee Id" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                                                                    <ItemStyle CssClass="hidden"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="form_id" HeaderText="FormId" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                                                                    <ItemStyle CssClass="hidden"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="acad_y_term_id" HeaderText="acad_y_term_id" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                                                                    <ItemStyle CssClass="hidden"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Fee ₦">
                                                                    <ItemTemplate>
                                                                        <%#Eval("amount")%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>


                                                    <div class=" col-md-2">
                                                        <div class="wrapper wrapper-content animated fadeInUp">
                                                            <ul class="notes">
                                                                <li>
                                                                    <div>
                                                                        <small>Setup Fee</small>
                                                                        <h4>Input the school fees for each of the classes</h4>
                                                                        <p>
                                                                            The drop down for Term will only display new Term if steps 1 have been set up correctly. 
                                                                    No worries if you want to make changes later on- You can do so in the Settings page at any time.<br />
                                                                            <strong>Note:Fees is the total of all mandatory fees (tuition,lesson etc)</strong>
                                                                        </p>
                                                                    </div>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>
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
                                        </div>
                                    </asp:WizardStep>
                                    <asp:WizardStep ID="WizardStep3" runat="server" Title="3.Review Assessment Weights ">
                                        <div class=" wrapper-content  animated fadeInRight">
                                            <h3>NOTE: Please add Weighting according to School Term Assessments, Leave rest of them blank</h3>

                                            <div class="pull-right" id="divEditUpdate" runat="server">
                                                <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn-primary" OnClick="btnEdit_Click" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn-primary" OnClick="btnCancel_Click" Visible="false" />
                                                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn-primary" OnClick="btnUpdate_Click" Visible="false" />
                                            </div>
                                            <asp:GridView ID="GridViewWeighting" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example"
                                                OnRowEditing="GridViewWeighting_RowEditing" OnRowCancelingEdit="GridViewWeighting_RowCancelingEdit"
                                                OnRowUpdating="GridViewWeighting_RowUpdating" ShowFooter="true">
                                                <Columns>
                                                    <asp:BoundField DataField="aw_id" HeaderText="aw ID" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                        <HeaderStyle CssClass="hidden"></HeaderStyle>
                                                        <ItemStyle CssClass="hidden"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="assessment" HeaderText="Assessment" ReadOnly="true" />
                                                    <asp:TemplateField HeaderText="Weighting">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="tbTotalMarks" runat="server" Text='<%# Bind("assessment_weight") %>' DataFormatString="{0:D}" Enabled="false"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="REVtbTotalMarks" ControlToValidate="tbTotalMarks" runat="server" ForeColor="Red"
                                                                ErrorMessage="Only Numbers allowed" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="publish_name" HeaderText="Publish" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                        <HeaderStyle CssClass="hidden"></HeaderStyle>
                                                        <ItemStyle CssClass="hidden"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="published" HeaderText="Published" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                        <HeaderStyle CssClass="hidden"></HeaderStyle>
                                                        <ItemStyle CssClass="hidden"></ItemStyle>
                                                    </asp:BoundField>

                                                </Columns>
                                            </asp:GridView>
                                            <asp:Label ID="lblZeroAssessments" runat="server" Text=""></asp:Label>
                                        </div>
                                        <%--  --Pages Styles--%>
                                        <style>
                                            .required {
                                                color: #F00;
                                            }
                                        </style>
                                    </asp:WizardStep>
                                    <asp:WizardStep ID="WizardStep4" runat="server" Title="4.Batch Registration">
                                        <script type="text/javascript">
                                            function CheckAllEmp(Checkbox) {
                                                var GridViewProcessApplications = document.getElementById("<%=GridViewBatchRegistrations.ClientID %>");
                                                for (i = 1; i < GridViewBatchRegistrations.rows.length; i++) {
                                                    GridViewBatchRegistrations.rows[i].cells[3].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
                                                }
                                            }
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
                                                    alert("Please select at least one Student");

                                                    return false;
                                                }

                                                return true;
                                            }
                                        </script>


                                        <div class="wrapper wrapper-content animated fadeInRight">
                                            <h4>From</h4>
                                            <div class="row">
                                                <div class="col-lg-14">
                                                    <div class="float-e-margins">
                                                        <div class="col-md-14">
                                                            <div class=" col-lg-2">
                                                                <label>Academic Year</label><br />
                                                                <asp:DropDownList ID="ddlFromBRYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFromBRYear_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                            &nbsp;	&nbsp;
                                                            <div class="col-lg-2">
                                                                <label>Term</label><br />
                                                                <asp:DropDownList ID="ddlFromBRTerm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFromBRTerm_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                            &nbsp;	&nbsp;
                                                            <div class="col-lg-2">
                                                                <label>Class</label><br />
                                                                <asp:DropDownList ID="ddlForm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlForm_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                            &nbsp;	&nbsp;
                                                            <div class="col-lg-2">
                                                                <label>Arm</label><br />
                                                                <asp:DropDownList ID="ddlClass" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>

                                                        </div>
                                                    </div>

                                                </div>
                                            </div>


                                            <div id="divProcessNow" runat="server" visible="false">
                                                <br />
                                                <h4>To</h4>
                                                <div class="col-md-14">
                                                    <div class=" col-lg-2">
                                                        <label>Academic Year</label><br />
                                                        <asp:DropDownList ID="ddlYearBReg" runat="server" ValidationGroup="BRGroup" AutoPostBack="true" OnSelectedIndexChanged="ddlYearBReg_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <label>Term</label><br />
                                                        <asp:DropDownList ID="ddlTermBReg" runat="server" ValidationGroup="BRGroup"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <label>Class</label><br />
                                                        <asp:DropDownList ID="ddlFormBReg" runat="server" ValidationGroup="BRGroup"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <label>Arm</label><br />
                                                        <asp:DropDownList ID="ddlClassBReg" runat="server" ValidationGroup="BRGroup"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-2">
                                                    </div>
                                                    <br />

                                                    <asp:Button ID="btnBatchRegistrations" runat="server" Text="Process Now" CssClass="col-lg-offset-1 btn-primary" ValidationGroup="BRGroup" OnClick="btnBatchRegistrations_Click" OnClientClick="return Validate_Checkbox()" />
                                                </div>
                                                <div class="col-lg-14 col-lg-offset-1">
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Academic Year required" ForeColor="Red" ControlToValidate="ddlYearBReg"
                                                        runat="server" Dispaly="Dynamic" ValidationGroup="BRGroup" />
                                                    &nbsp;&nbsp;&nbsp;
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Term field required" ForeColor="Red" ControlToValidate="ddlTermBReg"
                runat="server" Dispaly="Dynamic" ValidationGroup="BRGroup" />
                                                    &nbsp;&nbsp;&nbsp;
            <asp:RequiredFieldValidator ID="rfvddlFormBReg" ErrorMessage="Class field required" ForeColor="Red" ControlToValidate="ddlFormBReg" ValidationGroup="BRGroup"
                runat="server" Dispaly="Dynamic" />
                                                    &nbsp; &nbsp;&nbsp;&nbsp;
            <asp:RequiredFieldValidator ID="rfvddlClassBReg" ErrorMessage="Arm field required " ForeColor="Red" ControlToValidate="ddlClassBReg" ValidationGroup="BRGroup"
                runat="server" Dispaly="Dynamic" />

                                                </div>
                                            </div>
                                            <asp:GridView ID="GridViewBatchRegistrations" runat="server" CssClass="table table-striped table-bordered table-hover dataTables-example"
                                                AutoGenerateColumns="False"
                                                OnRowDataBound="GridViewBatchRegistrations_RowDataBound"
                                                OnPageIndexChanging="GridViewBatchRegistrations_PageIndexChanging"
                                                AllowPaging="false" PageSize="50" ShowFooter="false">
                                                <PagerStyle BackColor="#1ab394" ForeColor="white" HorizontalAlign="Center" CssClass="cssPager" />
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-Width="40px">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkboxSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkboxSelectAll_CheckedChanged" />
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkStudent" runat="server"></asp:CheckBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="person_id" HeaderText="person_id" InsertVisible="False" ReadOnly="True" SortExpression="person_id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                        <HeaderStyle CssClass="hidden"></HeaderStyle>
                                                        <ItemStyle CssClass="hidden"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="reg_id" HeaderText="reg_id" InsertVisible="False" ReadOnly="True" SortExpression="reg_id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                        <HeaderStyle CssClass="hidden"></HeaderStyle>
                                                        <ItemStyle CssClass="hidden"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="title" HeaderText="Title" />
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyperLinkPeople" runat="server" Text='<%# Eval("fullname") %>'></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="dob" HeaderText="Date Of Birth" />
                                                    <asp:BoundField DataField="form_name" HeaderText="Class" />
                                                    <asp:BoundField DataField="Class_name" HeaderText="Arm" />
                                                    <asp:BoundField DataField="reg_date" HeaderText="Registration Date" />
                                                    <asp:BoundField DataField="status" HeaderText="Registration Status" />

                                                </Columns>
                                            </asp:GridView>



                                            <br />

                                        </div>
                                        <br />
                                        <div class="col-md-6">
                                            <asp:Label ID="lblZeroBatchRegistrations" runat="server" Text=""></asp:Label>
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

                                    </asp:WizardStep>
                                    <asp:WizardStep ID="WizardStep5" runat="server" Title="5.Term active Decision ">
                                        <h2>New Term was active or make active of your wish</h2>
                                        <asp:GridView ID="GridViewTermActive" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover dataTables-example"
                                            OnRowEditing="GridViewTerm_RowEditing" OnRowCancelingEdit="GridViewTerm_RowCancelingEdit"
                                            OnRowUpdating="GridViewTerm_RowUpdating" OnRowDeleting="GridViewTerm_RowDeleting" OnRowDataBound="GridViewTerm_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="ay_term_id" HeaderText="Term ID" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                                                    <ItemStyle CssClass="hidden"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="acad_year" HeaderText="Academic Year " ReadOnly="true" />
                                                <asp:TemplateField HeaderText="Term">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="tbTerm" runat="server" Text='<%# Bind("term_name") %>'></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvtbTerm" ErrorMessage="Term Required" ForeColor="Red" ControlToValidate="tbTerm"
                                                            runat="server" Dispaly="Dynamic" />
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTerm" runat="server" Text='<%# Bind("term_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Start Date">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="tbTermStartDate" runat="server" Text='<%# Bind("ay_term_start_date") %>' CssClass="datepick"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvtbTermStartDate" ErrorMessage="Date Required" ForeColor="Red" ControlToValidate="tbTermStartDate"
                                                            runat="server" Dispaly="Dynamic" />
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTermStartDate" runat="server" Text='<%# Bind("ay_term_start_date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="End Date ">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="tbTermEndDate" runat="server" Text='<%# Bind("ay_term_end_date") %>' CssClass="datepick"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvtbTermEndDate" ErrorMessage="Date Required" ForeColor="Red" ControlToValidate="tbTermEndDate"
                                                            runat="server" Dispaly="Dynamic" />
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTermEndDate" runat="server" Text='<%# Bind("ay_term_end_date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="status" HeaderText="Status" ReadOnly="true"></asp:BoundField>
                                                <asp:TemplateField ShowHeader="false" HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="Edit" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="lnkUpdate" runat="server" Text="Update" CommandName="Update" />
                                                        <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel" CommandName="Cancel" CausesValidation="False" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete ?');" Text="Delete"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnStatus" runat="server" CssClass="btn-sm btn-default blue-skin m-t-n-xs" OnClick="btnStatus_Click" Text='Make Active' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:WizardStep>


                                </WizardSteps>
                            </asp:Wizard>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <style>
        .lightBoxGallery {
            text-align: center;
        }

            .lightBoxGallery img {
                margin: 5px;
            }
    </style>
</body>
</html>
