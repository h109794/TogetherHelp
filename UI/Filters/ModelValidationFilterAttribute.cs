using System.Web.Mvc;
using Global;

namespace UI.Filters
{
    public class ModelValidationFilterAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.HttpMethod == Method.Post)
            {
                var modelState = filterContext.Controller.ViewData.ModelState;
                if (!modelState.IsValid)
                {
                    filterContext.Controller.TempData.Add(Key.ErrorMessages, modelState);
                    filterContext.Result = new RedirectResult(filterContext.HttpContext.Request.Url.ToString());
                    return;
                }
            }

            if (filterContext.HttpContext.Request.HttpMethod == Method.Get)
            {
                var errorMessages = filterContext.Controller.TempData[Key.ErrorMessages];
                if (errorMessages != null)
                {
                    filterContext.Controller.ViewData.ModelState.Merge(errorMessages as ModelStateDictionary);
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext) { }
    }
}
