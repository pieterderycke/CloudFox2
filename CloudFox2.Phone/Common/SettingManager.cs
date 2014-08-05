using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;

namespace CloudFox2.Phone.Common
{
    public class SettingManager
    {
        public const string PasswordResourceName = "MozillaSync";

        public SettingManager()
        {
            try
            {
                PasswordVault vault = new PasswordVault();
                PasswordCredential credential = vault.FindAllByResource(PasswordResourceName).FirstOrDefault();
                if (credential != null)
                {
                    UserName = credential.UserName;
                    Password = vault.Retrieve(PasswordResourceName, UserName).Password;
                }
            }
            catch (Exception) { }
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsLoggedIn { get { return !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password); } }

        public void Save()
        {
            PasswordVault vault = new PasswordVault();
            vault.Add(new PasswordCredential(PasswordResourceName, UserName, Password));
        }

        public void Clear()
        {
            PasswordVault vault = new PasswordVault();
            vault.Remove(vault.Retrieve(PasswordResourceName, UserName));
        }
    }
}
