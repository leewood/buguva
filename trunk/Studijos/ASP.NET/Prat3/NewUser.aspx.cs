using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class NewUser : ExtendedPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
    {
        Control cnt = CreateUserWizard1.ActiveStep.Controls[0];
        TextBox nameBox = (TextBox)cnt.FindControl("Name");
        TextBox surnameBox = (TextBox)cnt.FindControl("Surname");
        TextBox cityBox = (TextBox)cnt.FindControl("City");
        TextBox usernameBox = (TextBox)cnt.FindControl("UserName");
        TextBox emailBox = (TextBox)cnt.FindControl("Email");
        MainDBDataClassesDataContext context = new MainDBDataClassesDataContext();
        UserPersonalInfo info = new UserPersonalInfo();
        info.City = cityBox.Text;
        info.UserName = usernameBox.Text;
        info.Surname = surnameBox.Text;
        info.Name = nameBox.Text;
        info.EMail = emailBox.Text;
        context.UserPersonalInfos.InsertOnSubmit(info);
        context.SubmitChanges();
    }

    protected void CreateUserWizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
    {

    }
    protected void CreateUserWizard1_ContinueButtonClick(object sender, EventArgs e)
    {

    }
    protected void CreateUserWizard1_CreatingUser(object sender, LoginCancelEventArgs e)
    {

    }
}
