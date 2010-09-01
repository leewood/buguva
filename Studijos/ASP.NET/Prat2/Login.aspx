<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login"  Theme="Default" Title="Login page" culture="auto" meta:resourcekey="PageResource1" uiculture="auto"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
    
      function pageLoad() {
      }
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <asp:Login ID="Login1" runat="server" 
             CssClass="popupbox"
            CreateUserText="Register" CreateUserUrl="~/NewUser.aspx" Font-Names="Verdana" 
            Font-Size="0.8em" ForeColor="#333333" onauthenticate="Login1_Authenticate" 
            PasswordRecoveryText="Forgot your password?" 
            PasswordRecoveryUrl="~/PasswordRecovery.aspx" 
            DestinationPageUrl="PublicNews.aspx" meta:resourcekey="Login1Resource1">
            <TextBoxStyle Font-Size="0.8em" />
            <LoginButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" 
                BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284775" />
            <LayoutTemplate>
               <table border="0" cellpadding="0" cellspacing="0">
                   <tr>
                    <td align="center" class="label" colspan="2" >
                            <asp:Label ID="LogInHeaderLabel" runat="server" Text="Log In" 
                                meta:resourcekey="LogInHeaderLabelResource1" />
                    </td>
                    </tr>
                    <tr>
                      <td>
                        <div class="nameLabel">
                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" 
                                meta:resourcekey="UserNameLabelResource1">User Name:</asp:Label>
                        </div>
                        <div class="value">
                            <asp:TextBox ID="UserName" runat="server" Font-Size="0.8em" 
                                meta:resourcekey="UserNameResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                ControlToValidate="UserName" ErrorMessage="User Name is required." 
                                ToolTip="User Name is required." ValidationGroup="Login1" 
                                meta:resourcekey="UserNameRequiredResource1">*</asp:RequiredFieldValidator>
                        </div>
                       </td>
                    </tr>
                    <tr>
                      <td>
                        <div class="nameLabel">
                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" 
                                meta:resourcekey="PasswordLabelResource1">Password:</asp:Label>
                        </div>
                        <div class="value">
                            <asp:TextBox ID="Password" runat="server" Font-Size="0.8em" TextMode="Password" 
                                meta:resourcekey="PasswordResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                ControlToValidate="Password" ErrorMessage="Password is required." 
                                ToolTip="Password is required." ValidationGroup="Login1" 
                                meta:resourcekey="PasswordRequiredResource1">*</asp:RequiredFieldValidator>
                        </div>
                      </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left" style="padding-left: 5px;">
                            <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me next time." 
                                meta:resourcekey="RememberMeResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" style="color:Red;">
                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False" 
                                meta:resourcekey="FailureTextResource1"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                      <td>
                        <div class="linkArea">
                            <asp:HyperLink ID="CreateUserLink" runat="server" NavigateUrl="~/NewUser.aspx" 
                                CssClass="miniLink" meta:resourcekey="CreateUserLinkResource1">Register</asp:HyperLink>
                            <br />
                            <asp:HyperLink ID="PasswordRecoveryLink" runat="server" CssClass="miniLink" 
                                NavigateUrl="~/PasswordRecovery.aspx" 
                                meta:resourcekey="PasswordRecoveryLinkResource1">Forgot your password?</asp:HyperLink>                        
                        </div>
                        <div style="padding-right: 5px;">
                            <asp:Button ID="LoginButton" runat="server" CommandName="Login" 
                                Text="Log In"  CssClass="simpleButton"
                                ValidationGroup="Login1" meta:resourcekey="LoginButtonResource1" />
                        </div>
                      </td>
                    </tr>
                </table>
            </LayoutTemplate>
            <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
            <TitleTextStyle CssClass="label" />
        </asp:Login>
        <br />
    </div>
    </form>
</body>
</html>
