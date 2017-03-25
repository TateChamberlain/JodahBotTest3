using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using ClassLibraryDBL;

namespace BotApplication0
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            // var tempstring = Class2.GetDBLString();
            //Set up Synergy logicals from AppSettings in Web.config
            //Based on code by Steve Ives 

            Dictionary<String, String> settings = new Dictionary<string, string>();
            foreach (string key in ConfigurationManager.AppSettings)
            {
                if (key == "DAT") settings.Add(key, Server.MapPath(ConfigurationManager.AppSettings[key]));
            }
            SynergyEnvironment.SetEnvironment(settings);
        }
    }
}
