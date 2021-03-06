﻿<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="reports.aspx.cs" Inherits="Alex.pages.reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function menuselection(arg) {
            $(arg).removeClass("btn btn-primary");
            $(arg).addClass("btn btn-active");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class=" row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10 h1">
            Reports
        </div>
    </div>
    <div class="wrapper wrapper-content fadeInRight">
        <div id="divStudents" runat="server" class="row">
            <div class="col-lg-12">
                <br />
            </div>
            <div class="col-lg-12">
                <div class="col-md-2">
                    <h3>Students</h3>
                </div>
                <div class="col-md-3">
                    <h3><a href="student_reports/student_stats.aspx">Student Statistics</a></h3>
                    <p>
                        Overview of Total Student numbers including Gender and Registration Status
                    </p>

                    <br />

                    <h3><a href="student_reports/non_returning_students.aspx">Non Returning Students</a></h3>
                    <p>
                        Names of Students registered to a class in the previous Term but not yet registered to the current Term
                    </p>

                    <br />

                    <h3><a href="student_reports/total_year_term.aspx">Total No of Students by Academic year and Term</a></h3>
                    <p>
                        Historical No of Registered Students (All available Academic Years & Terms)
                    </p>

                    <br />

                    <h3><a href="student_reports/student_registrations.aspx">List of Registered Students in a Class</a></h3>
                    <p>
                        List of students registered in current and previous academic terms with gender and status summary
                    </p>
                   
                </div>
                <div class="col-md-3">
                    
                    <h3><a href="student_reports/full_attendance_students.aspx">List of students with 100 percent attendance</a></h3>
                    <p>
                        List of students with 100 percent attendance of current Academic Term
                    </p>

                    <%--   <h3><a href="student_reports/id_cards.aspx">Student Id Cards</a></h3>
                    <p>
                       Id Cards for the school pupils
                    </p>--%>

                    <br />

                     <h3><a href="student_reports/student_debtors.aspx">Academic Term Debtors </a></h3>
                    <p>
                        List of Academic Term Debtors(now Debtors filtered by Term)
                    </p>

                    <br /><br />

                   <h3><a href="student_reports/student_debtors_year.aspx">List of Debtors (Cummulative Debts)</a></h3>
                    <p>
                        List of Students owing fees 
                    </p>

                    <br /><br />
                    
                    <h3><a href="student_reports/ict.aspx">Students ICT cards</a></h3>
                    <p>
                         ICT Cards for the school pupils
                    </p>
                </div>
                <div class="col-md-3">
                 
                    <h3><a href="student_reports/purchases_summary_date_item.aspx">Sold Items Summary</a></h3>
                    <p>
                        Sold Items Summary filtered by date and item
                    </p>

                    <br />

                    <h3><a href="student_reports/all_report_cards.aspx">Students Report Cards</a></h3>
                    <p>
                        Various formats of Students Report Cards
                    </p>
                    <br />

                    <h3><a href="student_reports/student_duplicate_profiles.aspx">Identical or Duplicate Profiles</a></h3>
                    <p>
                       Use to identify identical profiles that could be duplicate entries
                    </p>
                     <br />

                    <h3><a href="student_reports/student_attendance_report.aspx">Attendance</a></h3>
                    <p>
                       View Daily or Monthly attendance details
                    </p>
                </div>
            </div>
        </div>
        <hr />
        <div id="DivAccFee" runat="server" class="row">
            <div class="col-lg-12">
                <br />
            </div>
            <div class="col-lg-12">
                <div class="col-md-2">
                    <h3>Accounts (Tuition Fee)</h3>
                </div>
                <div class="col-md-3">
                   
                    <div id="divrep2" runat="server">
                        <h3><a href="student_reports/student_fee_summary.aspx">Tuition Fee Accounts Summary</a></h3>
                        <p>
                            Total Expected Tuition fees income, Total amount of fees received to date and total amount of fees outstanding for all academic years and terms
                        </p>
                    </div>
                    <br />
                    <div id="divrep1" runat="server">
                        <h3><a href="student_reports/student_payments.aspx">Recent Tuition Fee Payments</a></h3>
                        <p>
                            List of Recent Tuition Fee Payments with daily, weekly and Monthly visibility
                        </p>
                    </div>
                </div>
                <div class="col-md-3">
                   
                    <div id="divFeePaymentsDates" runat="server">
                        <h3><a href="student_reports/student_payments_selected_dates.aspx">Tuition Fee Payments</a></h3>
                        <p>
                            List of  Tuition Fee Payments in between the selected dates.
                        </p>
                    </div>
                    <br />
                    <div>
                        <h3><a href="student_reports/student_scholarship.aspx">Students on Scholarship/Discount</a></h3>
                        <p>
                            List of Students granted Scholarship/Discount filtered by Academic Session & Term.
                        </p>
                    </div>

                </div>
                <div class="col-md-3">


                    <div>
                        <h3><a href="student_reports/student_fee_full_paid.aspx">No of Students who have paid Tuition Fees in full</a></h3>
                        <p>
                            Breakdown by Class of Total No of Students who have paid Tuition Fees in Full
                        </p>
                    </div>
                    <br />
                    <div>
                        <h3><a href="student_reports/boarder_students.aspx">Students on Boarders</a></h3>
                        <p>
                            List of Students on Boarders filtered by Academic Year & Term.
                        </p>
                    </div>

                </div>
            </div>
        </div>
        <hr />
        <div id="divSales" runat="server" class="row">
            <div class="col-lg-12">
                <br />
            </div>
            <div class="col-lg-12">
                <div class="col-md-2">
                    <h3>Accounts (Sales)</h3>
                </div>
                <div class="col-md-3">
                   
                    <div id="divSaleItemPayments" runat="server">
                        <h3><a href="student_reports/saleItem_payments.aspx">Recent Sale Item Payments</a></h3>
                        <p>
                            List of Recent Sale Item Payments with daily,weekly and Monthly visibility
                        </p>
                        <br />
                    </div>
                </div>
               <%-- <div class="col-md-3">
                    <h3><a href="student_reports/purchases_date_item.aspx">Sold Items List of Payments</a></h3>
                    <p>
                        Sold Items List of Payments filtered by date and item
                    </p>

                    <br />
                </div>--%>
                <div id="divSalesDates" class="col-md-3" runat="server">
                    <h3><a href="student_reports/saleItem_paymentmethod.aspx">Sold Items Payments</a></h3>
                    <p>
                        Sold Items List of Payments filtered by Payment Method and Date
                    </p>
                    <br />
                </div>

            </div>
        </div>
        <hr />
        <div id="divAccounts" runat="server" class="row">
            <div class="col-lg-12">
                <br />
            </div>
            <div class="col-lg-12">
                <div class="col-md-2">
                    <h3>Accounts</h3>
                </div>
                <div class="col-md-3">
                    <h3><a href="student_reports/expenses_category_date.aspx">Expense Account</a></h3>
                    <p>
                        List of Expenses Account by category and Date
                    </p>
                    <br />
                </div>
                <div id="divProfit_loss" class="col-md-3" runat="server">
                    <h3><a href="student_reports/profit_loss.aspx">Profit & Loss Account</a></h3>
                    <p>
                        List of Profit & Loss Account
                    </p>

                    <br />
                </div>
                <%-- <div id="divSmsHistory" class="col-md-3" runat="server">
                    <h3><a href="student_reports/sms_history.aspx">SMS History</a></h3>
                    <p>
                        Log of Sent SMS by Date
                    </p>

                    <br />
                </div>--%>
            </div>
        </div>
        <hr />
        <div class="row" id="divEmployee" runat="server">
            <div class="col-lg-12">
                <br />
            </div>
            <div class="col-lg-12">
                <div class="col-md-2">
                    <h3>Employees</h3>
                </div>
                <div class="col-md-3">
                    <h3><a href="employee_reports/employee_stats.aspx">Employee Statistics</a></h3>
                    <p>
                        Overview of Total Employee numbers including Gender and by Department
                    </p>
                    <br />
                    <h3><a href="employee_reports/employees_salary_paid.aspx">Employees Salary Paid</a></h3>
                    <p>
                        List of Employees Monthly Salary Payment by year,month
                    </p>
                    <br />
                </div>
                <div id="divMonthlySalary" class="col-md-3" runat="server">
                    <h3><a href="employee_reports/monthly_salary_bank.aspx">Monthly Salary Bank Payment</a></h3>
                    <p>
                        List of Employees Monthly Salary Bank Payment by year,month and bank
                    </p>
                    <br />

<%--Joe 220118                    <h3><a href="employee_reports/employees_payslip2.aspx">Employee Payslips 2</a></h3>
                    <p>
                        Employees Payslips by year,month
                    </p>
                    <br />--%>
                </div>
               
                <div class="col-md-3">
                    <h3><a href="employee_reports/employees_payslip.aspx">Employee Payslips</a></h3>
                    <p>
                        Employees Payslips by year,month
                    </p>
                    <br />
                </div>
               

            </div>
        </div>

    </div>



    <style>
        hr {
            border: none;
            height: 1px;
            /* Set the hr color */
            color: #333; /* old IE */
            background-color: #333; /* Modern Browsers */
        }
    </style>


</asp:Content>
