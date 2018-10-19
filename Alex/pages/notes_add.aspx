<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="notes_add.aspx.cs" Inherits="Alex.pages.notes_add" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <link href="../scripts/css/pagestyle.css" rel="stylesheet" />
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
         $(function () { $('.datepick').datepicker({ changeMonth: true, changeYear: true, yearRange: '-100:' + new Date().getFullYear(), dateFormat: 'dd/mm/yy', maxDate: new Date() }); });
     </script>
      <div class="wrapper wrapper-content fadeInRight">
          <h1>Add Notes</h1>
          
                <div class="row">
                    <div class="col-lg-8">
                        <div class="ibox">
                            <div role="form" id="Div2" runat="server">
                                <div class="form-group">
                                    <div class="editor-label">
                                        <label>Comment </label><span class="required">*</span>
                                    </div>
                                    <div class="editor-field">
                                        <asp:TextBox ID="tbComment1" runat="server" placeholder="Enter Comment " CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                        <div class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Comment Required" ForeColor="Red" ControlToValidate="tbComment1"
                                                runat="server" Dispaly="Dynamic" />
                                        </div>
                                    </div>
                                </div>
                             

                                <div class="form-group">
                                    <div class="editor-label">
                                        <label>Date</label><span class="required">*</span>
                                    </div>
                                    <div class="editor-field">
                                        <asp:TextBox ID="tbAddNotesDate" runat="server" CssClass="form-control datepick" placeholder="dd/mm/yyyy"></asp:TextBox>
                                        <div class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="Date Required" ForeColor="Red" ControlToValidate="tbAddNotesDate"
                                                runat="server" Dispaly="Dynamic" />
                                        </div>
                                    </div>
                                </div>

                                 <div class="form-group">
                                    <div class="editor-label">
                                        <label>Note Type</label><span class="required">*</span>
                                    </div>
                                    <div class="editor-field">
                                        <asp:TextBox ID="tbNoteType" runat="server" CssClass="form-control" placeholder="Note Type"></asp:TextBox>
                                        <div class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvtbNoteType" ErrorMessage="Type Required" ForeColor="Red" ControlToValidate="tbNoteType"
                                                runat="server" Dispaly="Dynamic" />
                                        </div>
                                    </div>
                                </div>



                            </div>
                            <asp:Button ID="btnAddNotesForm" runat="server" Text="Save" CssClass="btn btn-sm btn-primary m-t-n-xs" CausesValidation="true" Font-Bold="True" OnClick="btnAddNotesForm_Click"  />
                            <asp:Button ID="btnAddNotesCancel" runat="server" Text="Cancel" CssClass="btn btn-sm btn-primary m-t-n-xs" CausesValidation="false" Font-Bold="True" OnClick="btnAddNotesCancel_Click" />
                        </div>
                    </div>
                </div>
      </div>
  <style type="text/css">
        
        .required {
            color: #F00;
        }
    </style>
 
          
</asp:Content>
