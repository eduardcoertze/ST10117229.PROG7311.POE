using System.Web;
using System.Web.Mvc;

namespace ST10117229.PROG7311.POE
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
