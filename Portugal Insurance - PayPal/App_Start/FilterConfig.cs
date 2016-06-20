using System.Web;
using System.Web.Mvc;

namespace Portugal_Insurance___PayPal
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
