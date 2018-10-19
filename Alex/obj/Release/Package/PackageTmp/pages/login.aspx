<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Alex.pages.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

<meta charset="utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0"/>
<meta name="description" content="iQ is a suite of integrated education management applications that facilitates the management of your school." />
     <link rel="shortcut icon" href="../images/favicon.png"/>
    <title>iQ</title>
  
    <link href="../scripts/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../scripts/font-awesome/css/font-awesome.css" rel="stylesheet"/>

    <link href="../scripts/css/animate.css" rel="stylesheet"/>
    <link href="../scripts/css/style.css" rel="stylesheet"/>

</head>
<body class="gray-bg">
    <form id="form1" runat="server">
    <div class="loginColumns animated fadeInDown">
        <div class="row">

            <div class="col-md-6">
                <h2 class="font-bold">Welcome to <b>iQ</b></h2>

                <p>
                    A suite of integrated education management applications that facilitates the management of your school.
                </p>

                <p>
                    iQ processes focus on cost saving, fee management, payroll management, time management and ensures important student data becomes visible and available across the organization in real time.
                </p>

                <p>
                    Easy to use and accessible on any device on the go
                </p>

                <%--<p>
                    <small>Part of Torilo Group</small>
                </p>--%>

            </div>
            <div class="col-md-6">
                <div class="ibox-content">
                    <div class="m-t" role="form">
                        <div class="form-group">
                            <asp:TextBox ID="TbUserName" runat="server" type="email" class="form-control" placeholder="Username" required=""></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="TbPassword" runat="server" type="password" class="form-control" placeholder="Password" required=""></asp:TextBox>
                        </div>
                        <asp:Button ID="BtnLogin" runat="server" Text="Login" type="submit" class="btn btn-primary block full-width m-b" OnClick="BtnLogin_Click" />
                      <%--  <a href="#"> <small>Forgot password?</small>
                       
                        </a>--%>

                       <%-- <p class="text-muted text-center">
                            <small>Do not have an account?</small>
                        </p>
                        <a class="btn btn-sm btn-white btn-block" href="register.aspx">Create an account</a>--%>
                  </div>
              </div>
          </div>
      </div>
        <hr/>
        <div class="row">
            <div class="col-md-6">
                Copyright iQ © 2015-<%= DateTime.Now.Year.ToString() %>
            </div><br /><br />
          <%--  <div class="col-md-12 h3 red-bg">
              <i class="fa fa-envelope-o">&nbsp;Contact us at:</i><strong>info.iQ@torilo.co.uk</strong>&nbsp; for Username and Password 
            </div>--%>
        </div>
    </div> 
    
    </form>
</body>
</html>
