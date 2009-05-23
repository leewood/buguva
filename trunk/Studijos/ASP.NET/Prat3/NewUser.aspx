<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewUser.aspx.cs" Inherits="NewUser" Title="New user" Theme="Default" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" BackColor="#F7F6F3" CssClass="popupbox"
            BorderColor="#E6E2D8" BorderStyle="Solid" BorderWidth="1px" 
            ContinueDestinationPageUrl="~/Login.aspx" Font-Names="Verdana" 
            Font-Size="0.8em" meta:resourcekey="CreateUserWizard1Resource1" 
            oncontinuebuttonclick="CreateUserWizard1_ContinueButtonClick" 
            oncreateduser="CreateUserWizard1_CreatedUser" 
            oncreatinguser="CreateUserWizard1_CreatingUser" 
            onfinishbuttonclick="CreateUserWizard1_FinishButtonClick">
            <SideBarStyle BackColor="#5D7B9D" BorderWidth="0px" Font-Size="0.9em" 
                VerticalAlign="Top" />
            <SideBarButtonStyle BorderWidth="0px" Font-Names="Verdana" ForeColor="White" />
            <ContinueButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" 
                BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" 
                ForeColor="#284775" />
            <NavigationButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" 
                BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" 
                ForeColor="#284775" />
            <HeaderStyle BackColor="#5D7B9D" BorderStyle="Solid" Font-Bold="True" 
                Font-Size="0.9em" ForeColor="White" HorizontalAlign="Center" />
            <CreateUserButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" 
                BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" 
                ForeColor="#284775" />
            <TitleTextStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <StepStyle BorderWidth="0px" />
            <WizardSteps>
                <asp:CreateUserWizardStep runat="server" 
                    meta:resourcekey="CreateUserWizardStepResource1" >
                    <ContentTemplate>
                        <table border="0" style="font-family:Verdana;font-size:100%;">
                            <tr>
                                <td align="center" colspan="2" 
                                    style="color:White;background-color:#5D7B9D;font-weight:bold;">
                                    <asp:Label ID="Label1" runat="server" Text="Sign Up for Your New Account" 
                                        meta:resourcekey="Label1Resource1" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" 
                                        meta:resourcekey="UserNameLabelResource1">User Name:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="UserName" runat="server" meta:resourcekey="UserNameResource1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                        ControlToValidate="UserName" ErrorMessage="User Name is required." 
                                        ToolTip="User Name is required." ValidationGroup="CreateUserWizard1" 
                                        meta:resourcekey="UserNameRequiredResource1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" 
                                        meta:resourcekey="PasswordLabelResource1">Password:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Password" runat="server" TextMode="Password" 
                                        meta:resourcekey="PasswordResource1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                        ControlToValidate="Password" ErrorMessage="Password is required." 
                                        ToolTip="Password is required." ValidationGroup="CreateUserWizard1" 
                                        meta:resourcekey="PasswordRequiredResource1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="ConfirmPasswordLabel" runat="server" 
                                        AssociatedControlID="ConfirmPassword" 
                                        meta:resourcekey="ConfirmPasswordLabelResource1">Confirm Password:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password" 
                                        meta:resourcekey="ConfirmPasswordResource1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" 
                                        ControlToValidate="ConfirmPassword" 
                                        ErrorMessage="Confirm Password is required." 
                                        ToolTip="Confirm Password is required." 
                                        ValidationGroup="CreateUserWizard1" 
                                        meta:resourcekey="ConfirmPasswordRequiredResource1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email" 
                                        meta:resourcekey="EmailLabelResource1">E-mail:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Email" runat="server" meta:resourcekey="EmailResource1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="EmailRequired" runat="server" 
                                        ControlToValidate="Email" ErrorMessage="E-mail is required." 
                                        ToolTip="E-mail is required." ValidationGroup="CreateUserWizard1" 
                                        meta:resourcekey="EmailRequiredResource1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="QuestionLabel" runat="server" AssociatedControlID="Question" 
                                        meta:resourcekey="QuestionLabelResource1">Security Question:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Question" runat="server" meta:resourcekey="QuestionResource1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="QuestionRequired" runat="server" 
                                        ControlToValidate="Question" ErrorMessage="Security question is required." 
                                        ToolTip="Security question is required." 
                                        ValidationGroup="CreateUserWizard1" 
                                        meta:resourcekey="QuestionRequiredResource1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="AnswerLabel" runat="server" AssociatedControlID="Answer" 
                                        meta:resourcekey="AnswerLabelResource1">Security Answer:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Answer" runat="server" meta:resourcekey="AnswerResource1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="AnswerRequired" runat="server" 
                                        ControlToValidate="Answer" ErrorMessage="Security answer is required." 
                                        ToolTip="Security answer is required." ValidationGroup="CreateUserWizard1" 
                                        meta:resourcekey="AnswerRequiredResource1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:CompareValidator ID="PasswordCompare" runat="server" 
                                        ControlToCompare="Password" ControlToValidate="ConfirmPassword" 
                                        Display="Dynamic" 
                                        ErrorMessage="The Password and Confirmation Password must match." 
                                        ValidationGroup="CreateUserWizard1" 
                                        meta:resourcekey="PasswordCompareResource1"></asp:CompareValidator>
                                </td>
                            </tr>                            
                            <tr>
                                <td align="right">
                                    <asp:Label ID="NameLabel" runat="server">Name:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Name" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ControlToValidate="Name" ErrorMessage="Name required." 
                                        ToolTip="Name required." 
                                        ValidationGroup="CreateUserWizard1" >*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label5" runat="server">Surname:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Surname" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                        ControlToValidate="Surname" ErrorMessage="Surname required." 
                                        ToolTip="Surname required." 
                                        ValidationGroup="CreateUserWizard1" >*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label6" runat="server">City:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="City" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                        ControlToValidate="City" ErrorMessage="City required." 
                                        ToolTip="City required." 
                                        ValidationGroup="CreateUserWizard1" >*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            
                            <tr>
                                <td align="center" colspan="2" style="color:Red;">
                                    <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False" 
                                        meta:resourcekey="ErrorMessageResource1"></asp:Literal>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <CustomNavigationTemplate>
                        <table border="0" cellspacing="5" style="width:100%;height:100%;">
                            <tr align="right">
                                <td align="right" colspan="0">
                                    <asp:Button ID="StepNextButton" runat="server" BackColor="#FFFBFF" 
                                        BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" 
                                        CommandName="MoveNext" Font-Names="Verdana" ForeColor="#284775" 
                                        Text="Create User" ValidationGroup="CreateUserWizard1" 
                                        meta:resourcekey="StepNextButtonResource1" />
                                </td>
                            </tr>
                        </table>
                    </CustomNavigationTemplate>
                </asp:CreateUserWizardStep>
                <asp:CompleteWizardStep runat="server" 
                    meta:resourcekey="CompleteWizardStepResource1" />
            </WizardSteps>
        </asp:CreateUserWizard>
    
    </div>
    </form>
</body>
</html>
