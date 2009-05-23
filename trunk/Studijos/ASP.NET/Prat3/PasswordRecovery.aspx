<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PasswordRecovery.aspx.cs" Inherits="LoggedIn_PasswordRecovery" Title="Password recovery" Theme="Default" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:PasswordRecovery ID="PasswordRecovery1" runat="server" 
            SuccessPageUrl="~/Login.aspx" meta:resourcekey="PasswordRecovery1Resource1">
            <MailDefinition From="no-replay@Prat2.loc" Subject="Your password recovery">
            </MailDefinition>
            <QuestionTemplate>

                            <table border="0" cellpadding="0" cellspacing="0" class="popupbox">
                                <tr>
                                    <td align="center" colspan="2" class="label">
                                        <asp:Label ID="Label6" runat="server" Text="Identity Confirmation" 
                                            meta:resourcekey="Label6Resource1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2" style="color:Black;font-style:italic;">
                                        <asp:Label ID="Label5" runat="server" 
                                            Text="Answer the following question to receive your password." 
                                            meta:resourcekey="Label5Resource1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="nameLabel">
                                        <asp:Label ID="Label4" runat="server" Text="User Name:" 
                                                meta:resourcekey="Label4Resource1" />
                                        </div>
                                        <div class="value">
                                        <asp:Literal ID="UserName" runat="server" meta:resourcekey="UserNameResource1"></asp:Literal>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="nameLabel">
                                        <asp:Label ID="UserNameLabel" runat="server" Text="Question:" 
                                                meta:resourcekey="UserNameLabelResource1" />
                                        </div>
                                        <div class="value">
                                        <asp:Literal ID="Question" runat="server" meta:resourcekey="QuestionResource1"></asp:Literal>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                      <div class="nameLabel">
                                        <asp:Label ID="AnswerLabel" runat="server" AssociatedControlID="Answer" 
                                              meta:resourcekey="AnswerLabelResource1">Answer:</asp:Label>
                                      </div>
                                      <div class="value">
                                        <asp:TextBox ID="Answer" runat="server" Font-Size="0.8em" 
                                              meta:resourcekey="AnswerResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="AnswerRequired" runat="server" 
                                            ControlToValidate="Answer" ErrorMessage="Answer is required." 
                                            ToolTip="Answer is required." ValidationGroup="PasswordRecovery1" 
                                              meta:resourcekey="AnswerRequiredResource1">*</asp:RequiredFieldValidator>
                                      </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2" style="color:Red;">
                                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False" 
                                            meta:resourcekey="FailureTextResource1"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                      <div class="buttonArea" style="padding-left: 150px" >
                                        <asp:Button ID="SubmitButton" runat="server"  Width="100px"
                                            CommandName="Submit" CssClass="simpleButton"
                                            Text="Submit" ValidationGroup="PasswordRecovery1" 
                                              meta:resourcekey="SubmitButtonResource1" />
                                      </div>
                                    </td>
                                </tr>
                            </table>
            </QuestionTemplate>
            <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
            <SuccessTextStyle Font-Bold="True" ForeColor="#5D7B9D" />
            <TextBoxStyle Font-Size="0.8em" />
            <UserNameTemplate>

                            <table border="0" cellpadding="0" cellspacing="0" class="popupbox">
                                <tr>
                                    <td align="center" class="label" >
                                        <asp:Label ID="Label3" runat="server" Text="Forgot Your Password?" 
                                            meta:resourcekey="Label3Resource1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="color:Black;font-style:italic;">
                                        <asp:Label ID="Label2" runat="server" 
                                            Text="Enter your User Name to receive your password." 
                                            meta:resourcekey="Label2Resource1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    <div class="nameLabel">
                                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" 
                                            meta:resourcekey="UserNameLabelResource2">User Name:</asp:Label>
                                    </div>
                                    <div class="value">                                    
                                        <asp:TextBox ID="UserName" runat="server" Font-Size="0.8em" 
                                            meta:resourcekey="UserNameResource2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                            ControlToValidate="UserName" ErrorMessage="User Name is required." 
                                            ToolTip="User Name is required." ValidationGroup="PasswordRecovery1" 
                                            meta:resourcekey="UserNameRequiredResource1">*</asp:RequiredFieldValidator>
                                    </div>
                                </tr>
                                <tr>
                                    <td align="center" style="color:Red;">
                                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False" 
                                            meta:resourcekey="FailureTextResource2"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                      <div class="buttonArea" style="padding-left: 150px">
                                        <asp:Button ID="SubmitButton" runat="server" CssClass="simpleButton"
                                            Text="Submit" ValidationGroup="PasswordRecovery1" Width="100px" 
                                              CommandName="Submit" meta:resourcekey="SubmitButtonResource2"  />
                                      </div>
                                    </td>
                                </tr>
                            </table>

            </UserNameTemplate>
            <TitleTextStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.9em" 
                ForeColor="White" />
            <SuccessTemplate>
                <table border="0" cellpadding="0" cellspacing="0" 
                    class ="popupbox">
                    <tr>                    
                        <td class="label">
                          <asp:Label ID="Label1" runat="server" Text="Info" 
                                meta:resourcekey="Label1Resource1" />
                        </td>
                        <td>                           
                           <asp:Label ID="UserNameLabel" runat="server" 
                                Text="Your password has been sent to you." 
                                meta:resourcekey="UserNameLabelResource3" />
                        </td>
                    </tr>
                </table>
            </SuccessTemplate>
            <SubmitButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" 
                BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" 
                ForeColor="#284775" />
        </asp:PasswordRecovery>
    
    </div>
    </form>
</body>
</html>
