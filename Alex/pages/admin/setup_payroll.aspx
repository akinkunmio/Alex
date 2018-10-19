<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="setup_payroll.aspx.cs" Inherits="Alex.pages.admin.setup_payroll" %>

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
    <%-- DataKeyNames="add_payroll_settings_id"--%>
    <asp:DetailsView ID="DetailsViewSetupPayroll" runat="server" AutoGenerateRows="False" DataSourceID="SqlDataSourceSetupPayroll"
        OnItemCreated="DetailsViewSetupPayroll_ItemCreated" CssClass="table table-striped table-bordered table-hover dataTables-example" Width="799px">
        <Fields>
            <%--<asp:BoundField DataField="add_payroll_settings_id" HeaderText="add_payroll_settings_id" ReadOnly="True" SortExpression="add_payroll_settings_id" />--%>
            <%--<asp:BoundField DataField="staff_no" HeaderText="Staff No" SortExpression="staff_no" />--%>
            <%--<asp:BoundField DataField="add_basic_pay_percent" HeaderText="Basic Pay %" SortExpression="add_basic_pay_percent" />--%>
            <asp:TemplateField HeaderText="ALLOWANCES">
                
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </ItemTemplate>
                <HeaderStyle BackColor="#45B48F" BorderColor="White" />
                <ItemStyle BackColor="#45B48F" BorderColor="White" Font-Bold="True" />
            </asp:TemplateField>
            <asp:BoundField DataField="add_rent_allow_percent" HeaderText="Housing Allowance %" SortExpression="add_rent_allow_percent" />
            <asp:BoundField DataField="add_transport_allow_percent" HeaderText="Transport Allowance %" SortExpression="add_transport_allow_percent" />
            <asp:BoundField DataField="add_medical_allow_percent" HeaderText="Medical Allowance %" SortExpression="add_medical_allow_percent" />
            <asp:BoundField DataField="add_meal_allow_percent" HeaderText="Meal Allowance %" SortExpression="add_meal_allow_percent" />
            <asp:BoundField DataField="add_utility_allow_percent" HeaderText="Utility Allowance %" SortExpression="add_utility_allow_percent" />
            <%--<asp:BoundField DataField="add_other_allow_percent" HeaderText="Other Allowances %" SortExpression="add_other_allow_percent" />--%>
            <asp:TemplateField HeaderText="DEDUCTIONS">
              
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server"></asp:Label>
                </ItemTemplate>
                <HeaderStyle BackColor="#45B48F" BorderColor="White" />
                <ItemStyle BackColor="#45B48F" BorderColor="White" Font-Bold="True" />
            </asp:TemplateField>
            <asp:BoundField DataField="deduct_tax_percent" HeaderText="Tax %" SortExpression="deduct_tax_percent" />
            <asp:BoundField DataField="deduct_pesion_scheme_percent" HeaderText="Pension %" SortExpression="deduct_pesion_scheme_percent" />
            <%--<asp:BoundField DataField="deduct_staff_welfare_percent" HeaderText="Welfare %  " SortExpression="deduct_staff_welfare_percent" />
            <asp:BoundField DataField="deduct_staff_coop_percent" HeaderText="Co-operative %" SortExpression="deduct_staff_coop_percent" />
            <asp:BoundField DataField="deduct_staff_loan_percent" HeaderText="Loan %" SortExpression="deduct_staff_loan_percent" />
            <asp:BoundField DataField="deduct_staff_others_percent" HeaderText="Other Deductions %" SortExpression="deduct_staff_others_percent" />--%>
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
            <h1>Setup Payroll</h1>
            <asp:Button ID="SetupPayroll" runat="server" CommandName="New" InsertText="Add" Text="Setup Payroll Now" CssClass="btn btn-sm btn-primary" />
        </EmptyDataTemplate>

    </asp:DetailsView>
    <asp:SqlDataSource ID="SqlDataSourceSetupPayroll" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>"
        InsertCommand="sp_ms_hr_payroll_sttings_add" InsertCommandType="StoredProcedure"
        SelectCommand="sp_ms_hr_payroll_sttings_list_all" SelectCommandType="StoredProcedure"
        UpdateCommand="sp_ms_hr_payroll_sttings_edit" UpdateCommandType="StoredProcedure">
        <InsertParameters>
            <asp:Parameter Name="staff_no" Type="Int32" />
            <asp:Parameter Name="add_rent_allow_percent" Type="Decimal" />
            <asp:Parameter Name="add_transport_allow_percent" Type="Decimal" />
            <asp:Parameter Name="add_medical_allow_percent" Type="Decimal" />
            <asp:Parameter Name="add_meal_allow_percent" Type="Decimal" />
            <asp:Parameter Name="add_utility_allow_percent" Type="Decimal" />
            <asp:Parameter Name="deduct_tax_percent" Type="Decimal" />
            <asp:Parameter Name="deduct_pesion_scheme_percent" Type="Decimal" />
            <asp:Parameter Name="deduct_staff_welfare_percent" Type="Decimal" />
            <asp:Parameter Name="created_by" Type="String" />
           
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="staff_no" Type="Int32" />
            <asp:Parameter Name="add_rent_allow_percent" Type="Decimal" />
            <asp:Parameter Name="add_transport_allow_percent" Type="Decimal" />
            <asp:Parameter Name="add_medical_allow_percent" Type="Decimal" />
            <asp:Parameter Name="add_meal_allow_percent" Type="Decimal" />
            <asp:Parameter Name="add_utility_allow_percent" Type="Decimal" />
            <asp:Parameter Name="deduct_tax_percent" Type="Decimal" />
            <asp:Parameter Name="deduct_pesion_scheme_percent" Type="Decimal" />
            <asp:Parameter Name="deduct_staff_welfare_percent" Type="Decimal" />
            <asp:Parameter Name="updated_by" Type="String" />
          
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>


