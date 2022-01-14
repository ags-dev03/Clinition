using Emergency.Classes;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Xml.Linq;
using System.Xml;
using System.Web.Configuration;

namespace Emergency
{
    public class Global : HttpApplication
    {
        
        void Application_Start(object sender, EventArgs e)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AreaRegistration.RegisterAllAreas();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
           
        }

        protected void Application_End()
        {
           
        }
    }
}