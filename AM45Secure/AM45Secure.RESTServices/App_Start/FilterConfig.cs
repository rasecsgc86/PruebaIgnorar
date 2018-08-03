using System.Web;
using System.Web.Mvc;

namespace AM45Secure.RESTServices
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
