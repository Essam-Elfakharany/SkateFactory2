using SkateFactory2.Models;
using SkateFactory2.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Services;

namespace SkateFactory2.WebService
{
    /// <summary>
    /// Summary description for UserService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class UserService : System.Web.Services.WebService
    {
        #region Support Methods
        public string ComputePasswordHash(string userName, string password)
        {
            string pepper = ConfigurationManager.AppSettings["Pepper"];
            int keySize = 64;
            int iterations = 1000;
            var hashAlgorithm = HashAlgorithmName.SHA512;
            byte[] salt = ComputeSalt(userName, pepper);
            var r = new Rfc2898DeriveBytes(password, salt, iterations, hashAlgorithm);
            string result = Convert.ToBase64String(r.GetBytes(keySize));
            return result;
        }

        public byte[] ComputeSalt(string userName, string pepper)
        {
            byte[] result = Encoding.UTF8.GetBytes(userName + pepper);
            return result;
        }
        #endregion

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public bool UserAndPasswordAreValid(string userName, string password)
        {
            string passwordHash = ComputePasswordHash(userName, password);
            var user = new User();
            user.Email = userName;
            user.Password = passwordHash;
            string cnString = StaticMethods.GetConnectionString(this);
            return UserData.UserAndPasswordAreValid(user, cnString);
        }

        [WebMethod]
        public bool Register(string userName, string password)
        {
            bool result = false;
            string cnString = StaticMethods.GetConnectionString(this);
            
            if (UserData.UserIsUnique(userName, cnString))
            {
                string passwordHash = ComputePasswordHash(userName, password);
                var newUser = new User() { Email = userName, Password = passwordHash };
                UserData.Insert(newUser, cnString);
                result = true;
            }

            return result;
        }

    }
}
