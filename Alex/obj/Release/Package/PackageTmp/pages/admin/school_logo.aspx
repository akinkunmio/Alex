<%@ Page Title="" Language="C#" MasterPageFile="~/pages/master.Master" AutoEventWireup="true" CodeBehind="school_logo.aspx.cs" Inherits="Alex.pages.admin.school_logo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <script type="text/javascript">
        function ValidateUpload() {
            var fuData = document.getElementById("<%=FileUpload1.ClientID %>") ;
            if (fuData.value == '') {
                return false;
            }
            else {
                document.getElementById("<%=Upload.ClientID %>").style.display = "";
            }
            return true;
        }
          function ValidateUpload2() {
              var fuData = document.getElementById("<%=FileUpload2.ClientID %>");
              if (fuData.value == '') {
                  return false;
              }
              else {
                  document.getElementById("<%=btnUploadBgLogo.ClientID %>").style.display = "";
              }
              return true;
          }
    </script>

    <div class="col-md-3 pull-right">
        <a href="../settings.aspx">
            <asp:Label runat="server" Text="&nbsp;Back to Settings" CssClass="fa fa-arrow-circle-left btn btn-sm btn-info m-t-n-xs" Font-Bold="True"></asp:Label>
        </a>
    </div>

    <h1>School Logo</h1>



    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row">
            <div class="col-lg-12">
                <div class="float-e-margins">
                    <div class="col-md-6">
                        <div class="gray-bg">
                            <h3>Upload your School Logo </h3>
                           <asp:ScriptManager ID="ScriptManagerUpload" runat="server"></asp:ScriptManager>
                             <asp:UpdatePanel ID="UpdatePanelUpload" runat="server" UpdateMode="conditional">
                                <ContentTemplate>
                                    <asp:FileUpload ID="FileUpload1" runat="server"  />
                                   <%-- <asp:RequiredFieldValidator ErrorMessage="Required" ControlToValidate="FileUpload1"
                                              runat="server" Display="Dynamic" ForeColor="Red"  ValidationGroup="LogoGroup" />--%><br />
                                    <asp:Button ID="Upload" runat="server" Text="Upload" CssClass="btn btn-primary" OnClick="Upload_Click" ValidationGroup="LogoGroup" Style="display: none;"   /><br />
                                    <asp:CustomValidator ID="CustomValidator1" runat="server"  ControlToValidate="FileUpload1" ClientValidationFunction="ValidateUpload" ValidationGroup="LogoGroup" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="Upload" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div><label>NOTE: Logo's should be 100x100 px size only and Max size 200KB </label><br /><asp:Label runat="server" ID="lblStatuslogo" Text="" />
                    </div>
                    <div class="col-md-6">
                        <div class="gray-bg">
                            <h3>Upload your Parent's Online Background Logo </h3>
                            
                            <asp:UpdatePanel ID="UpdatePanelBgLogo" runat="server" UpdateMode="conditional">
                                <ContentTemplate>
                                    <asp:FileUpload ID="FileUpload2" runat="server" />
                                   <%-- <asp:RequiredFieldValidator ErrorMessage="Required" ControlToValidate="FileUpload2"
                                                                runat="server" Display="Dynamic" ForeColor="Red" ValidationGroup="BgGroup" />--%><br />
                                    <asp:Button ID="btnUploadBgLogo" runat="server" Text="Upload" CssClass="btn btn-primary" OnClick="btnUploadBgLogo_Click"  ValidationGroup="BgGroup" Style="display: none;"   /><br />
                                    <asp:CustomValidator ID="CustomValidator2" runat="server"  ControlToValidate="FileUpload2" ClientValidationFunction="ValidateUpload2" ValidationGroup="BgGroup" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnUploadBgLogo" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    
                </div>
                <br /><asp:Label runat="server" ID="lblStatus" Text=""  />
            </div>
        </div>
    </div>
</asp:Content>
