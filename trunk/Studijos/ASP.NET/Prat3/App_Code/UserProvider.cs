using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

/// <summary>
/// Summary description for UserProvider
/// </summary>
/// 
public class UserInfo
{
    public string UserName { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string City { get; set; }
    public string Email { get; set; }
    public string Language { get; set; }
    public string Culture { get; set; }
    public string Theme { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime LastActivityDate { get; set; }
    public DateTime LastLoginDate { get; set; }
}

public class UserProvider
{
    public static List<UserInfo> Users
    {
        get
        {
            List<UserInfo> result = new List<UserInfo>();
            try
            {
                MainDBDataClassesDataContext context = new MainDBDataClassesDataContext();
                MembershipProvider provider = System.Web.Security.Membership.Provider;
                int total = 0;
                int temp = 0;
                provider.FindUsersByName("%", 0, 1, out total);
                var users = provider.FindUsersByName("%", 0, total, out temp);
                foreach (var ouser in users)
                {
                    MembershipUser user = (MembershipUser)ouser;
                    string userName = user.UserName;
                    var item = (from d in context.UserConfigs where d.Username == userName select d).ToList();
                    var item2 = (from k in context.UserPersonalInfos where k.UserName == userName select k).ToList();
                    UserInfo info = new UserInfo() { LastActivityDate = user.LastActivityDate, CreationDate = user.CreationDate, LastLoginDate = user.LastLoginDate, UserName = user.UserName, Email = user.Email };
                    if (item.Count > 0)
                    {
                        info.Theme = item[0].Theme;
                        info.Language = item[0].Language;
                        info.Culture = item[0].Culture;
                    }
                    if (item2.Count > 0)
                    {
                        info.City = item2[0].City;
                        info.Name = item2[0].Name;
                        info.Surname = item2[0].Surname;
                    }
                    result.Add(info);
                }
            }
            catch (Exception)
            {
            }
            return result;
        }
    }

	public UserProvider()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
