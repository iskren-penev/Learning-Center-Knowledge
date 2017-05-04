using System.Web;
using System.Web.Mvc;

namespace LearningCenter.App
{
    using System;

    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute()
            {
                ExceptionType = typeof(ArgumentNullException),
                View = "OoutOfRangeError"
            });
            filters.Add(new HandleErrorAttribute()
            {
                ExceptionType = typeof(Exception),
                View = "Error"
            });
        }
    }
}
