<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="non_academic_assessments.aspx.cs" Inherits="Alex.pages.non_academic_assessments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
        function checkSpecialKeys(e) {
            if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
                return false;
            else
                return true;
        }
    </script>

    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10 h1">
            Mark Non-Academic Assessments
        </div>

        <div class="col-lg-2">
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row">
            <div class="col-lg-12">
                <div class="float-e-margins">
                   <div class="col-lg-6">
                            <label>Class</label><br />
                            <asp:DropDownList ID="ddlFormClass" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFormClass_SelectedIndexChanged"></asp:DropDownList>
                   </div>
                 </div>
            </div>
        </div>

        <br />
        <div id="divEdit" runat="server">
          <div class="row">
            <div class="col-lg-12">
              <div class="float-e-margins">
                <div class=" pull-right h4">
                            
                            <label class="text-blue">4 =&nbsp;Excellent,</label>&nbsp;&nbsp;
                           
                            <label class="text-blue">3 =&nbsp;Good,</label>&nbsp;&nbsp;
                           
                            <label class="text-blue">2 =&nbsp;Average,</label>&nbsp;&nbsp;
                          
                            <label class="text-blue">1 =&nbsp;Poor</label>&nbsp;&nbsp;
                       
                       
                <asp:Button ID="btnEdit" runat="server" Text="Click to mark" CssClass="btn-primary" OnClick="btnEdit_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn-primary" OnClick="btnCancel_Click"  Visible="false" />
                <asp:Button ID="btnUploadAssessments" runat="server" Text="Save assessments" CssClass="btn-primary"  OnClick="btnUploadAssessments_Click"  />
              </div>
            </div>
          </div>
        </div>
     </div>

        <asp:GridView ID="GridViewAssessments" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridViewAssessments_RowDataBound"
            CssClass="table table-striped table-bordered table-hover dataTables-example" >
            <Columns>
                 <asp:BoundField DataField="reg_id" HeaderText="reg_id" InsertVisible="False" ReadOnly="True" SortExpression="reg_id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="Name" HeaderStyle-Width="190px">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLinkPeople" runat="server" Text='<%# Eval("fullname") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Teacher Comment" ControlStyle-Width="95px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="50px" ItemStyle-Width="30px">
                    <ItemTemplate>
                        <asp:TextBox ID="tbAssessment1" runat="server" Text='<%# Bind("comment1")  %>' Enabled="false"  Height="200px" TextMode="MultiLine" MaxLength="120" onkeyDown="checkTextAreaMaxLength(this,event,'120');"
                                                                            Wrap="True" Font-Names="Arial" Width="400px"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Head Teacher Comment" ControlStyle-Width="95px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" ItemStyle-Width="30px">
                    <ItemTemplate>
                        <asp:TextBox ID="tbAssessment2" runat="server" Text='<%# Bind("comment2") %>' Enabled="false" Height="200px" TextMode="MultiLine" MaxLength="120" onkeyDown="checkTextAreaMaxLength(this,event,'120');"
                                                                            Wrap="True" Font-Names="Arial" Width="400px"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="float-e-margins">

                                    <div class="col-lg-4">
                                        <h4>Attentiveness</h4>
                                        <asp:DropDownList ID="dd1" runat="server" Text='<%# Bind("attentiveness") %>' Enabled="false" SelectedValue='<%# Bind("attentiveness") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <h4>Honesty</h4>
                                        <asp:DropDownList ID="dd2" runat="server" Text='<%# Bind("honesty") %>' Enabled="false" SelectedValue='<%# Bind("honesty") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <h4>Neatness</h4>
                                        <asp:DropDownList ID="dd3" runat="server" Text='<%# Bind("neatness") %>' Enabled="false" SelectedValue='<%# Bind("neatness") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <h4>Politeness</h4>
                                        <asp:DropDownList ID="dd4" runat="server" Text='<%# Bind("politeness") %>' Enabled="false" SelectedValue='<%# Bind("politeness") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class=" col-lg-4">
                                        <h4>Punctuality</h4>
                                        <asp:DropDownList ID="dd5" runat="server" Text='<%# Bind("punctuality") %>' Enabled="false" SelectedValue='<%# Bind("punctuality") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <h4>Relationship With Others</h4>
                                        <asp:DropDownList ID="dd6" runat="server" Text='<%# Bind("relationship_with_others") %>' Enabled="false" SelectedValue='<%# Bind("relationship_with_others") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <h4>Club Societies</h4>
                                        <asp:DropDownList ID="dd7" runat="server" Text='<%# Bind("club_societies") %>' Enabled="false" SelectedValue='<%# Bind("club_societies") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <h4>Drawing And Painting</h4>
                                        <asp:DropDownList ID="dd8" runat="server" Text='<%# Bind("drawing_and_painting") %>' Enabled="false" SelectedValue='<%# Bind("drawing_and_painting") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-4">
                                        <h4>Hand writing</h4>
                                        <asp:DropDownList ID="dd9" runat="server" Text='<%# Bind("hand_writing") %>' Enabled="false" SelectedValue='<%# Bind("hand_writing") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <h4>Hobbies</h4>
                                        <asp:DropDownList ID="dd10" runat="server" Text='<%# Bind("hobbies") %>' Enabled="false" SelectedValue='<%# Bind("hobbies") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <h4>Speech Fluency</h4>
                                        <asp:DropDownList ID="dd11" runat="server" Text='<%# Bind("speech_fluency") %>' Enabled="false" SelectedValue='<%# Bind("speech_fluency") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <h4>Sports And Games</h4>
                                        <asp:DropDownList ID="dd12" runat="server" Text='<%# Bind("sports_and_games") %>' Enabled="false" SelectedValue='<%# Bind("sports_and_games") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
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

         .text-blue {
            color: #0000FF;
        }
    </style>

</asp:Content>
