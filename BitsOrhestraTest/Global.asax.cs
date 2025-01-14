using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BitsOrhestraTest.App_Start;

namespace BitsOrchestraTest
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            AutofacConfig.RegisterDependencies();
        }
    }
}
