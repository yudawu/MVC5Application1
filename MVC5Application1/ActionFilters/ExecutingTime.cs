using System;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ExecutingTimeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewData["ActionTimeStart"] = DateTime.Now;
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.Controller.ViewData["ActionTimeEnd"] = DateTime.Now;
            filterContext.Controller.ViewData["ActionTimeSpan"] = string.Format("{0}ms", (filterContext.Controller.ViewBag.ActionTimeEnd - filterContext.Controller.ViewBag.ActionTimeStart).Milliseconds);
        }
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.Controller.ViewData["ResultTimeStart"] = DateTime.Now;
        }
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            filterContext.Controller.ViewData["ResultTimeEnd"] = DateTime.Now;
            filterContext.Controller.ViewData["ResultTimeSpan"] = string.Format("{0}ms", (filterContext.Controller.ViewBag.ResultTimeEnd - filterContext.Controller.ViewBag.ResultTimeStart).Milliseconds);
        }
    }
}