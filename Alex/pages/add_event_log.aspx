<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="add_event_log.aspx.cs" Inherits="Alex.pages.add_event_log" %>
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
    $(function () { $('.datepick').datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); });
</script>
     <div class="col-md-3 pull-right">
            <a href="../pages/dashboard.aspx">
               <asp:Label runat="server" Text="&nbsp;Back to Dashboard" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"  ></asp:Label>
            </a>
    </div>
    <h2>School Event Log's</h2>
    <div class=" wrapper-content  animated fadeInRight">
        <div class="row">
            <div class="col-lg-6">
                <div class="ibox">
                    <div role="form" id="form">
                        <div>
                            <div class="form-group">
                                <div class="editor-label">
                                    <label>Academic Year</label><span class="required">*</span>
                                </div>
                                <div class="editor-field">
                                    <asp:DropDownList ID="ddlAcademicYear" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <div class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvddlAcademicYear" ValidationGroup="SchoolTimelineSetup" ErrorMessage="Year Required" ForeColor="Red" ControlToValidate="ddlAcademicYear"
                                            runat="server" Dispaly="Dynamic" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="editor-label">
                                    <label>Term</label><span class="required">*</span>
                                </div>
                                <div class="editor-field">
                                    <asp:DropDownList ID="ddlTerm" runat="server" CssClass="form-control" AutoPostBack="true" >
                                    </asp:DropDownList>
                                    <div class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvddlTerm" ValidationGroup="SchoolTimelineSetup" ErrorMessage="Term Required" ForeColor="Red" ControlToValidate="ddlTerm"
                                            runat="server" Dispaly="Dynamic" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="editor-label">
                                    <label>Date</label><span class="required">*</span>
                                </div>
                                 <div class="editor-field">
                                        <asp:TextBox ID="tbTimelineDate" runat="server" rel="date"  type="text" ValidationGroup="SchoolTimelineSetup"  CssClass="form-control datepick" placeholder="dd/mm/yyyy"></asp:TextBox>
                                      <div class="pull-right">
                                              <asp:RequiredFieldValidator ID="rfvtbTimelineDate" ErrorMessage="Date Required" ValidationGroup="SchoolTimelineSetup" ForeColor="Red" ControlToValidate="tbTimelineDate"
                                               runat="server" Dispaly="Dynamic"/>
                                     </div>
                                </div>
                            </div>

                                <div class="form-group">
                                    <div class="editor-label">
                                        <label>Event</label><span class="required">*</span>
                                    </div>
                                    <div class="editor-field">
                                        <asp:TextBox ID="tbEvent" runat="server" placeholder="Event Name" ValidationGroup="SchoolTimelineSetup" CssClass="form-control"></asp:TextBox>
                                          <div class="pull-right">
                                              <asp:RequiredFieldValidator ID="rfvtbEvent" ErrorMessage="Event Required" ValidationGroup="SchoolTimelineSetup" ForeColor="Red" ControlToValidate="tbEvent"
                                               runat="server" Dispaly="Dynamic"/>
                                         </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="editor-label">
                                        <label>Description</label><span class="required">*</span>
                                    </div>
                                    <div class="editor-field">
                                        <asp:TextBox ID="tbDescription" runat="server" placeholder="Event Description" ValidationGroup="SchoolTimelineSetup" CssClass="form-control"></asp:TextBox>
                                        <div class="pull-right">
                                              <asp:RequiredFieldValidator ID="rfvtbDescription" ValidationGroup="SchoolTimelineSetup" ErrorMessage="Event Description Required" ForeColor="Red" ControlToValidate="tbDescription"
                                               runat="server" Dispaly="Dynamic"/>
                                       </div>
                                    </div>
                                </div>
                               <div class="form-group">
                                    <div class="editor-label">
                                        <label>Category</label><span class="required">*</span>
                                    </div>
                                    <div class="editor-field">
                                        <asp:TextBox ID="tbCategory" runat="server" placeholder="Category" ValidationGroup="SchoolTimelineSetup" CssClass="form-control"></asp:TextBox>
                                        <div class="pull-right">
                                              <asp:RequiredFieldValidator ID="rfvtbCategory" ErrorMessage="Category Required" ForeColor="Red" ValidationGroup="SchoolTimelineSetup" ControlToValidate="tbCategory"
                                               runat="server" Dispaly="Dynamic"/>
                                       </div>
                                    </div>
                                </div>
                                  <%-- <div class="form-group">
                                    <div class="editor-label">
                                        <label>Created By</label><span class="required">*</span>
                                    </div>
                                    <div class="editor-field">
                                        <asp:TextBox ID="tbCreatedBy" runat="server" placeholder="Enter your Name" CssClass="form-control"></asp:TextBox>
                                         <div class="pull-right">
                                              <asp:RequiredFieldValidator ID="rfvtbCreatedBy" ErrorMessage="Name Required" ForeColor="Red" ControlToValidate="tbCreatedBy"
                                               runat="server" Dispaly="Dynamic"/>
                                       </div>
                                    </div>
                                </div>--%>
                             
                              <asp:Button ID="BtnSaveTimeLine" runat="server" Text="Save" CssClass="btn btn-sm btn-primary m-t-n-xs" Font-Bold="True" ValidationGroup="SchoolTimelineSetup" OnClick="BtnSaveTimeLine_Click" />
                            
                        </div>
                    </div>
                </div>
            </div>
        </div>
<h1>List of Current Term Event Log's</h1>
        <asp:GridView ID="GridViewTimeLine" runat="server" AutoGenerateColumns="False"  CssClass="table table-striped table-bordered table-hover dataTables-example" 
            OnRowEditing="GridViewTimeLine_RowEditing" 
            OnRowCancelingEdit="GridViewTimeLine_RowCancelingEdit"
            OnRowUpdating="GridViewTimeLine_RowUpdating"
            OnRowDeleting="GridViewTimeLine_RowDeleting">
            <Columns>
                 <asp:BoundField DataField="event_log_id" HeaderText="event_log_id" ReadOnly="true" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                     <HeaderStyle CssClass="hidden"></HeaderStyle>
                     <ItemStyle CssClass="hidden"></ItemStyle> 
                 </asp:BoundField>
                 <asp:BoundField DataField="Day" HeaderText="Day"  ReadOnly="true"/>
                 <asp:BoundField DataField="Month" HeaderText="Month"  ReadOnly="true"/>
                 <asp:TemplateField HeaderText="Date" SortExpression="Date">
                     <EditItemTemplate>
                         <asp:TextBox ID="tbDate" runat="server" Text='<%# Bind("Date") %>' CssClass="datepick"></asp:TextBox>
                     </EditItemTemplate>
                     <ItemTemplate>
                         <asp:Label ID="lblDate" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Event" SortExpression="Event">
                     <EditItemTemplate>
                         <asp:TextBox ID="tbEvent" runat="server" Text='<%# Bind("Event") %>'></asp:TextBox>
                     </EditItemTemplate>
                     <ItemTemplate>
                         <asp:Label ID="lblEvent" runat="server" Text='<%# Bind("Event") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Description" SortExpression="Description">
                     <EditItemTemplate>
                         <asp:TextBox ID="tbDescription" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                     </EditItemTemplate>
                     <ItemTemplate>
                         <asp:Label ID="tbDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Edit">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="Edit"  CausesValidation="False" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="lnkUpdate" runat="server" Text="Update" CommandName="Update"  CausesValidation="False" />
                        <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel" CommandName="Cancel"  CausesValidation="False" />
                    </EditItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                              <ItemTemplate>
                                  <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete ?');"></asp:LinkButton>
                              </ItemTemplate>
                        </asp:TemplateField>
           </Columns>
        </asp:GridView>
  </div>
 <style>
.required {
color: #F00;
}
</style>     
</asp:Content>
