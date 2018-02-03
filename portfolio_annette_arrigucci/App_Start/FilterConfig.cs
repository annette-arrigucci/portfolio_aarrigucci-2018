using System.Web;
using System.Web.Mvc;

namespace portfolio_annette_arrigucci
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
