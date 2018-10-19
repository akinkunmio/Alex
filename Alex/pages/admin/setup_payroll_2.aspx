<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="setup_payroll_2.aspx.cs" Inherits="Alex.pages.admin.setup_payroll_2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-md-3 pull-right">
        <a href="../settings.aspx">
            <asp:Label runat="server" Text="&nbsp;Back to Settings" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"></asp:Label>
        </a>
    </div>
    <h1>Setup Payroll</h1>
    <br />
   <div class="wrapper wrapper-content animated fadeInRight">
    <asp:DetailsView ID="DetailsViewSetupPayroll" runat="server" AutoGenerateRows="False" DataSourceID="SqlDataSourceSetupPayroll"
        OnItemCreated="DetailsViewSetupPayroll_ItemCreated" CssClass="table table-striped table-bordered table-hover dataTables-example" Width="799px" DataKeyNames="ps_id">
        <Fields>
           
            <asp:BoundField DataField="ps_id" HeaderText="ps_id" SortExpression="ps_id" InsertVisible="False" ReadOnly="True" Visible="false" />
            <asp:BoundField DataField="add_gross" HeaderText="Gross" SortExpression="add_gross" />
            <asp:BoundField DataField="add_basic_salary" HeaderText="Basic Salary" SortExpression="add_basic_salary" />
            <asp:TemplateField HeaderText="ALLOWANCES">
                
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </ItemTemplate>
                <HeaderStyle BackColor="#45B48F" BorderColor="White" />
                <ItemStyle BackColor="#45B48F" BorderColor="White" Font-Bold="True" />
            </asp:TemplateField>
            <asp:BoundField DataField="add_cost_of_living_allowance" HeaderText="Cost Of Living Allowance" SortExpression="add_cost_of_living_allowance" />
            <asp:BoundField DataField="add_cola" HeaderText="Cola Allowance" SortExpression="add_cola" />
           
            <asp:BoundField DataField="add_enhance" HeaderText="Enhance Allowance" SortExpression="add_enhance" />
            <asp:BoundField DataField="add_transport" HeaderText="Transport Allowance" SortExpression="add_transport" />
           
            <asp:BoundField DataField="add_productivity" HeaderText="Productivity Allowance" SortExpression="add_productivity" />
            <asp:BoundField DataField="add_responsibility" HeaderText="Responsibility Allowance" SortExpression="add_responsibility" />
            <asp:BoundField DataField="add_housing" HeaderText="Housing Allowance" SortExpression="add_housing" />
            <asp:BoundField DataField="add_personalized" HeaderText="Personalized Allowance" SortExpression="add_personalized" />
            <asp:BoundField DataField="add_child_allowance" HeaderText="Child Allowance" SortExpression="add_child_allowance" />
            <asp:BoundField DataField="add_utility" HeaderText="Utility Allowance" SortExpression="add_utility" />
            <asp:BoundField DataField="add_others" HeaderText="Other Allowance" SortExpression="add_others" />
            <asp:TemplateField HeaderText="DEDUCTIONS">
              
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server"></asp:Label>
                </ItemTemplate>
                <HeaderStyle BackColor="#45B48F" BorderColor="White" />
                <ItemStyle BackColor="#45B48F" BorderColor="White" Font-Bold="True" />
            </asp:TemplateField>
            <asp:BoundField DataField="deduct_paye" HeaderText="Paye" SortExpression="deduct_paye" />
            <asp:BoundField DataField="deduct_tithe" HeaderText="Tithe" SortExpression="deduct_tithe" />
            <asp:BoundField DataField="deduct_pencom" HeaderText="Pencom" SortExpression="deduct_pencom" />
            <asp:BoundField DataField="deduct_cooperative" HeaderText="Cooperative" SortExpression="deduct_cooperative" />
            <asp:BoundField DataField="deduct_socials" HeaderText="Socials" SortExpression="deduct_socials" />
            <asp:BoundField DataField="deduct_personal_account" HeaderText="Personal Account" SortExpression="deduct_personal_account" />
            <asp:BoundField DataField="deduct_rent_comeback" HeaderText="Rent Comeback" SortExpression="deduct_rent_comeback" />
            <asp:BoundField DataField="deduct_others" HeaderText="Other" SortExpression="deduct_others" />
             <asp:CommandField ShowEditButton="True" EditText=" Edit" ShowInsertButton="True" InsertText="Save" ControlStyle-CssClass="btn btn-primary" HeaderStyle-Width="80px">
                <HeaderStyle Width="80px"></HeaderStyle>
                <ControlStyle CssClass="btn btn-primary" Width="80px"></ControlStyle>
            </asp:CommandField>
        </Fields>
        <FooterTemplate>

            <div>
                <asp:Label ID="lblTotal" runat="server" />
            </div>

        </FooterTemplate>

        <EmptyDataTemplate>
            <asp:Button ID="SetupPayroll" runat="server" CommandName="New" InsertText="Add" Text="Setup Payroll Now" CssClass="btn btn-sm btn-primary" />
        </EmptyDataTemplate>

    </asp:DetailsView>
    <asp:SqlDataSource ID="SqlDataSourceSetupPayroll" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
        InsertCommand="sp_ms_hr_bab_payroll_sttings_add" InsertCommandType="StoredProcedure"
        SelectCommand="sp_ms_hr_bab_payroll_sttings_list_all" SelectCommandType="StoredProcedure"
        UpdateCommand="sp_ms_hr_bab_payroll_sttings_edit" UpdateCommandType="StoredProcedure">
        <InsertParameters>
            <asp:Parameter Name="add_gross" Type="Decimal" />
            <asp:Parameter Name="add_basic_salary" Type="Decimal" />
            <asp:Parameter Name="add_cost_of_living_allowance" Type="Decimal" />
            <asp:Parameter Name="add_cola" Type="Decimal" />
            <asp:Parameter Name="add_enhance" Type="Decimal" />
            <asp:Parameter Name="add_transport" Type="Decimal" />
            <asp:Parameter Name="add_productivity" Type="Decimal" />
            <asp:Parameter Name="add_responsibility" Type="Decimal" />
            <asp:Parameter Name="add_housing" Type="Decimal" />
            <asp:Parameter Name="add_personalized" Type="Decimal" />
            <asp:Parameter Name="add_child_allowance" Type="Decimal" />
            <asp:Parameter Name="add_utility" Type="Decimal" />
            <asp:Parameter Name="add_others" Type="Decimal" />
            <asp:Parameter Name="deduct_paye" Type="Decimal" />
            <asp:Parameter Name="deduct_tithe" Type="Decimal" />
            <asp:Parameter Name="deduct_pencom" Type="Decimal" />
            <asp:Parameter Name="deduct_cooperative" Type="Decimal" />
            <asp:Parameter Name="deduct_socials" Type="Decimal" />
            <asp:Parameter Name="deduct_personal_account" Type="Decimal" />
            <asp:Parameter Name="deduct_rent_comeback" Type="Decimal" />
            <asp:Parameter Name="deduct_others" Type="Decimal" />
            <asp:Parameter Name="created_by" Type="String" />
           
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="add_gross" Type="Decimal" />
            <asp:Parameter Name="add_basic_salary" Type="Decimal" />
            <asp:Parameter Name="add_cost_of_living_allowance" Type="Decimal" />
            <asp:Parameter Name="add_cola" Type="Decimal" />
            <asp:Parameter Name="add_enhance" Type="Decimal" />
            <asp:Parameter Name="add_transport" Type="Decimal" />
            <asp:Parameter Name="add_productivity" Type="Decimal" />
            <asp:Parameter Name="add_responsibility" Type="Decimal" />
            <asp:Parameter Name="add_housing" Type="Decimal" />
            <asp:Parameter Name="add_personalized" Type="Decimal" />
            <asp:Parameter Name="add_child_allowance" Type="Decimal" />
            <asp:Parameter Name="add_utility" Type="Decimal" />
            <asp:Parameter Name="add_others" Type="Decimal" />
            <asp:Parameter Name="deduct_paye" Type="Decimal" />
            <asp:Parameter Name="deduct_tithe" Type="Decimal" />
            <asp:Parameter Name="deduct_pencom" Type="Decimal" />
            <asp:Parameter Name="deduct_cooperative" Type="Decimal" />
            <asp:Parameter Name="deduct_socials" Type="Decimal" />
            <asp:Parameter Name="deduct_personal_account" Type="Decimal" />
            <asp:Parameter Name="deduct_rent_comeback" Type="Decimal" />
            <asp:Parameter Name="deduct_others" Type="Decimal" />
            <asp:Parameter Name="updated_by" Type="String" />
          
        </UpdateParameters>
    </asp:SqlDataSource>
   </div>
</asp:Content>
