using SkateFactory2.Data;
using SkateFactory2.Models.Enums;
using SkateFactory2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace SkateFactory2.WebService
{
    /// <summary>
    /// Summary description for SkateboardService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SkateboardService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public void Insert(Skateboard skateboard)
        {
            SkateboardData.Insert(skateboard, StaticMethods.GetConnectionString(this));
        }

        [WebMethod]
        public void Update(Skateboard skateboard)
        {
            SkateboardData.Update(skateboard, StaticMethods.GetConnectionString(this));
        }

        [WebMethod]
        public void Delete(int skateboardId)
        {
            SkateboardData.Delete(skateboardId, StaticMethods.GetConnectionString(this));
        }

        [WebMethod]
        public List<Skateboard> GetList(ESkateboardCriteria criteria)
        {
            var result = SkateboardData.GetList(criteria, StaticMethods.GetConnectionString(this));
            return result;
        }

        [WebMethod]
        public Skateboard SearchById(int skateboardId)
        {
            var result = SkateboardData.SearchById(skateboardId, StaticMethods.GetConnectionString(this));
            return result;
        }
    }
}
