using SRV.ProductionService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Filters
{
    public class TransactionControlFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (filterContext.Exception is null)
            {
                TransactionControl.Commit();
            }
            else
            {
                TransactionControl.Rollback();
            }
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
        }
    }
}