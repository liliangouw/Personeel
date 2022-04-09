using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Personeel.MVCSite.Filters
{
    public class PersonnelAuthAttribute:AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //当用户存储在cookie中且session数据为空时，把cookie的数据复制给session
            if (filterContext.HttpContext.Request.Cookies["userId"] != null &&
                filterContext.HttpContext.Session["userId"] == null)
            {
                filterContext.HttpContext.Session["userId"] = filterContext.HttpContext.Request.Cookies["userId"].Value;
                filterContext.HttpContext.Session["userName"] = filterContext.HttpContext.Request.Cookies["userName"].Value;
                filterContext.HttpContext.Session["userEmail"] = filterContext.HttpContext.Request.Cookies["userEmail"].Value;
            }
            //判断cookie或session是否有值
            if (!(filterContext.HttpContext.Session["userId"] != null ||
                filterContext.HttpContext.Request.Cookies["userId"] != null))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary()
                {
                    {"controller","Login"},
                    {"action", "Login"}
                });
            }
        }
    }
}