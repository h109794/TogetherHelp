using System.Web.Mvc;
using UI.Filters;

namespace UI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
#if DEBUG
            filters.Add(new HandleErrorAttribute());
#endif
            filters.Add(new TransactionControlFilter());
            filters.Add(new ValidateLoginFilter());
        }
    }
}
