<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="settings.aspx.cs" Inherits="Alex.pages.settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10 h1">
            Settings
        </div>
    </div>
    <div class=" wrapper-content  animated fadeInRight" >
    <div class="row">
        <div class="col-lg-12">
            <br />
        </div>
        <div class="col-lg-12">
            <div class="col-md-2">
                <h3>School Setup/Settings</h3>
            </div>
            <div class="col-md-3">
                <h3><a href="admin/school_details.aspx">Details</a></h3>
                <p>
                    Enter your School Details.
                </p>
                  <h3><a href="admin/school_logo.aspx">Logo</a></h3>
                <p>
                    Upload or change your School logo.
                </p>

                <h3><a href="admin/setup_academic_year.aspx">Academic Year</a></h3>
                <p>
                    Setup your Academic Year .
                </p>

                <h3><a href="admin/school_terms.aspx">Terms </a></h3>
                <p>
                    Add your school terms of your school choice.
                </p>
                <h3><a href="admin/term_wizard.aspx">Term Wizard </a></h3>
                <p>
                   Wizard that helps the complete process of set up new Term
                </p>
                <h3><a href="admin/unknown_profiles.aspx">Unknown Profiles data </a></h3>
                <p>
                   Correct unknown  or unfilled information of your people's profiles.   
                </p>
                 <h3><a href="admin/unknown_address.aspx">Unknown Profiles Address</a></h3>
                <p>
                    Correct unknown  or unfilled information of your people's Address. 
                </p>
                
            </div>
            <div class="col-md-3">

                <h3><a href="admin/setup_form.aspx">Class </a></h3>
                <p>
                    Setup your school Class.
                </p>
                <h3><a href="admin/setup_class.aspx">Arm</a></h3>
                <p>
                    Setup your school Arms.
                </p>

                <h3><a href="admin/setup_boarder_type.aspx">Boarder Types</a></h3>
                <p>
                    Setup your school Boarder Types 
                </p>

                 <h3><a href="admin/setup_boarder_fees.aspx">Boarder Fee</a></h3>
                <p>
                    Setup your school Boarder Fee
                </p>
                <br />
                <h3><a href="admin/setup_fee.aspx">Fee Setup</a></h3>
                <p>
                    Setup your school fee payment for each Term and Classes.    
                </p>

              
                <h3><a href="admin/setup_department.aspx">Department</a></h3>
                <p>
                    Setup your school Departments
                </p>
                <br />
                <h3><a href="admin/setup_school_timeline.aspx">Calendar</a></h3>
                <p>
                    Setup your School Calendar
                </p>
               
               
                

               <%-- <h3><a href="admin/setup_assessments_type.aspx">Create Assessments Type </a></h3>
                <p>
                    Setup your school Assessments Type 
                </p>--%>
            </div>
            <div class="col-md-3">
                
               

                <h3><a href="admin/setup_subjects.aspx">Subjects</a></h3>
                <p>
                    Setup your school class subjects. 
                </p>

                <h3><a href="admin/setup_assessments.aspx">Assign subject to a class</a></h3>
                <p>
                    Assign subjects to a class to do assessments
                </p>
                <h3><a href="admin/setup_assessmentweight.aspx">Assessments</a></h3>
                <p>
                    Setup weighting for assessments and Publish
                </p>
                <h3><a href="admin/export_data.aspx">Export data</a></h3>
                <p>
                    Download your school data into Excel Sheets.
                </p>

                <h3><a href="send_email.aspx">Send Email</a></h3>
                <p>
                    Send group Emails to Parents or Guardians.
                </p>
                <h3><a href="admin/registration_profile_delete.aspx">Delete Profiles</a></h3>
                <p>
                    Delete Non Active Registered Students
                </p>
            </div>

        </div>
    </div>


    <hr />
    <div id="divUserSettings" class="row" runat="server" visible="false">
        <div class="col-lg-12">
            <br />
        </div>
        <div class="col-lg-12">
            <div class="col-md-2">
                <h3>User Settings</h3>
            </div>
            <div class="col-md-3">
                <h3><a href="admin/setup_new_user.aspx">New User</a></h3>
                <p>
                    Enables you to new user  
                </p>
            </div>
            <div class="col-md-3">
                <h3><a href="admin/manage_users.aspx">Manage Users</a></h3>
                <p>
                    Manage your users.
                </p>
            </div>
           <%-- <div class="col-md-3">

                <h3><a href="admin/manage_parent_access.aspx">Enable/Disable Parents iQ</a></h3>
                <p>
                    Enable/Disable the access of iQ Parents Online.
                </p>
                <br />
            </div>--%>
        </div>

       <hr class=" table-bordered"/>
    </div>

   
    <div class="row">
        <div class="col-lg-12">
            <br />
        </div>
        <div class="col-lg-12">
            <div class="col-md-2">
                <h3>Accounts</h3>
            </div>
            <div class="col-md-3">
                <h3><a href="admin/sales_items.aspx">Sale Items </a></h3>
                <p>
                    Add your sale Items of your School  <small>Eg:Uniforms, Bus Fees</small>
                </p>
                <br />
           </div>
           

            <div class="col-md-3">
                <h3><a href="admin/setup_expenses_category.aspx">Expenses Category</a></h3>
                <p>
                    Add your own categorize expenses 
                </p>
                <br />
            </div>
            <div class="col-md-3">
               <%-- <h3><a href="admin/price_items.aspx">Sale Items Prices</a></h3>
                <p>
                    Setup your sale Items prices 
                </p>
                <br />--%>
                <h3><a href="admin/setup_bank_list.aspx">Setup Bank Name</a></h3>
                <p>
                    Add your list of Bank(s)  
                </p>
                <br />
             </div>
        </div>
    
    </div>
    <hr />
    <div class="row">
        <div class="col-lg-12">
            <br />
        </div>
        <div class="col-lg-12">
            <div class="col-md-2">
                <h3>Payroll</h3>
            </div>
            <div class="col-md-3">
                <h3><a href="admin/setup_payroll.aspx">Setup Payroll <%-- Joe 190118 1719  type 1--%>

                    </a></h3>
                <p>
                    Setup your employee payroll.
                </p>
                <br />

            </div>
             <%-- Joe 190118 1719 <div class="col-md-3">
                <h3><a href="admin/setup_payroll_2.aspx">Setup Payroll type 2</a></h3>
                <p>
                    Setup your employee payroll.
                </p>
                <br />

            </div>--%>

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
