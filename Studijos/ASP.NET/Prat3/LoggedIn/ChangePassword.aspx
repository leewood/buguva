<%@ Page Language="C#" MasterPageFile="~/LoggedIn/PreferencesMaster.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="LoggedIn_ChangePassword" Title="Changing password" Theme="Default" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PrefPlaceHolder" Runat="Server">
    <asp:ChangePassword ID="ChangePassword1" runat="server" 
        CancelDestinationPageUrl="~/LoggedIn/Personalization.aspx" 
        ContinueDestinationPageUrl="~/LoggedIn/Personalization.aspx" meta:resourcekey="ChangePassword1Resource1" 
         >
        <TitleTextStyle BackColor="#6B696B" Font-Bold="True" ForeColor="#FFFFFF" />
        <ChangePasswordTemplate>
            <h3><asp:Literal  ID="headerText" runat="server" Text="Change your password" 
                    meta:resourcekey="headerTextResource1" /></h3>
            <div class="toolbar">
              <div class="extButton">
               <asp:ImageButton ID="UpdateButtonImg" runat="server" CommandName="ChangePassword" 
                      SkinID="SaveImageButton" CssClass="simpleImageClear" 
                      meta:resourcekey="UpdateButtonImgResource1" />
                <asp:Button ID="ChangePasswordPushButton" runat="server" 
                    CommandName="ChangePassword" Text="Change Password"  CssClass="simpleClear"
                    ValidationGroup="ChangePassword1" 
                      meta:resourcekey="ChangePasswordPushButtonResource1" />                 
              </div>                
               <label class="separator">|</label>              
              <div class="extButton">
               <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Cancel" 
                      SkinID="CancelImageButton" CssClass="simpleImageClear" 
                      meta:resourcekey="ImageButton1Resource1" />
                <asp:Button ID="CancelPushButton" runat="server" CausesValidation="False" 
                    CommandName="Cancel" Text="Cancel" CssClass="simpleClear" 
                      meta:resourcekey="CancelPushButtonResource1" />               
              </div>
            </div>
            <asp:Label ID="CurrentPasswordLabel" runat="server"  CssClass="title"
                AssociatedControlID="CurrentPassword" 
                meta:resourcekey="CurrentPasswordLabelResource1">Password:</asp:Label>
            <asp:TextBox ID="CurrentPassword" runat="server" TextMode="Password" 
                meta:resourcekey="CurrentPasswordResource1"></asp:TextBox>
            <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" 
                ControlToValidate="CurrentPassword" ErrorMessage="Password is required." 
                ToolTip="Password is required." ValidationGroup="ChangePassword1" 
                meta:resourcekey="CurrentPasswordRequiredResource1">*</asp:RequiredFieldValidator>
            <label class="separator"></label>
            <asp:Label ID="NewPasswordLabel" runat="server" CssClass="title"
                AssociatedControlID="NewPassword" 
                meta:resourcekey="NewPasswordLabelResource1">New Password:</asp:Label>
            <asp:TextBox ID="NewPassword" runat="server" TextMode="Password" 
                meta:resourcekey="NewPasswordResource1"></asp:TextBox>
            <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" 
                ControlToValidate="NewPassword" ErrorMessage="New Password is required." 
                ToolTip="New Password is required." ValidationGroup="ChangePassword1" 
                meta:resourcekey="NewPasswordRequiredResource1">*</asp:RequiredFieldValidator>
            <label class="separator"></label>
            <asp:Label ID="ConfirmNewPasswordLabel" runat="server"  CssClass="title"
                AssociatedControlID="ConfirmNewPassword" 
                meta:resourcekey="ConfirmNewPasswordLabelResource1">Confirm New Password:</asp:Label>
            <asp:TextBox ID="ConfirmNewPassword" runat="server" TextMode="Password" 
                meta:resourcekey="ConfirmNewPasswordResource1"></asp:TextBox>
            <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" 
                ControlToValidate="ConfirmNewPassword" 
                ErrorMessage="Confirm New Password is required." 
                ToolTip="Confirm New Password is required." 
                ValidationGroup="ChangePassword1" 
                meta:resourcekey="ConfirmNewPasswordRequiredResource1">*</asp:RequiredFieldValidator>                                
            <label class="separator"></label>                
                                
            <asp:CompareValidator ID="NewPasswordCompare" runat="server" 
                ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" 
                Display="Dynamic" 
                ErrorMessage="The Confirm New Password must match the New Password entry." 
                ValidationGroup="ChangePassword1" 
                meta:resourcekey="NewPasswordCompareResource1"></asp:CompareValidator>                                                            
            <asp:Literal ID="FailureText" runat="server" EnableViewState="False" 
                meta:resourcekey="FailureTextResource1"></asp:Literal>
        </ChangePasswordTemplate>
        <SuccessTemplate>
            <table border="0" cellpadding="1" cellspacing="0" 
                style="border-collapse:collapse;">
                <tr>
                    <td>
                        <table border="0" cellpadding="0">
                            <tr>
                                <td align="center" colspan="2" 
                                    style="color:White;background-color:#6B696B;font-weight:bold;">                                    
                                    <asp:Literal ID="lit1" runat="server" Text="Change Password Complete" 
                                        meta:resourcekey="lit1Resource1" /></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal1" runat="server" 
                                        Text="Your password has been changed!" meta:resourcekey="Literal1Resource1" /></td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:Button ID="ContinuePushButton" runat="server" CausesValidation="False" 
                                        CommandName="Continue" Text="Continue" 
                                        meta:resourcekey="ContinuePushButtonResource1" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </SuccessTemplate>
        
    </asp:ChangePassword>
</asp:Content>

