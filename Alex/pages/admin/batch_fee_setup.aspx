<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="batch_fee_setup.aspx.cs" Inherits="Alex.pages.admin.batch_fee_setup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10 h1">
            Batch Fee Set Up
        </div>

        <div class="col-lg-2">
           <a href="../admin/setup_fee.aspx">
            <br /><asp:Label runat="server" Text="&nbsp;Back to Fee Setup" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"></asp:Label>
          </a>
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <h4>From</h4>
        <div class="row">
            <div class="col-lg-12">
                <div class="float-e-margins">
                    <div class="col-md-14">
                        <div class=" col-lg-2">
                            <label>Academic Year</label><br />
                            <asp:DropDownList ID="ddlFromBatchFeeYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFromBatchFeeYear_SelectedIndexChanged" ></asp:DropDownList>
                        </div>
                        <div class="col-lg-2">
                            <label>Term</label><br />
                            <asp:DropDownList ID="ddlFromBatchFeeTerm" runat="server" ></asp:DropDownList>
                        </div>
                   </div>
                </div>

            </div>
        </div>

       
        <br />
        <h4>To</h4>
        <div class="col-md-14">
            <div class=" col-lg-2">
                <label>Academic Year</label><br />
                <asp:DropDownList ID="ddlToBatchFeeYear" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="ddlToBatchFeeYear_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="col-lg-2">
                <label>Term</label><br />
                <asp:DropDownList ID="ddlToBatchFeeTerm" runat="server"></asp:DropDownList>
            </div>
          
            <br />

            <asp:Button ID="btnBatchFeeSetup" runat="server" Text="Set Up Now" CssClass="btn-primary" OnClick="btnBatchFeeSetup_Click"   />
        </div>
        <div class="col-lg-14 col-lg-offset-1">
            <br />
            <asp:RequiredFieldValidator ID="rfvddlYearBReg" ErrorMessage="Academic Year required" ForeColor="Red" ControlToValidate="ddlToBatchFeeYear"
                runat="server" Dispaly="Dynamic" />
            &nbsp;&nbsp;&nbsp;
            <asp:RequiredFieldValidator ID="rfvddlTermBReg" ErrorMessage="Term field required" ForeColor="Red" ControlToValidate="ddlToBatchFeeTerm"
                runat="server" Dispaly="Dynamic" />
            &nbsp;&nbsp;&nbsp;
            
        </div>
   
         
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

