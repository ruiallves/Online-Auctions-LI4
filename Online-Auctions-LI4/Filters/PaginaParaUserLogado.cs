using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using Online_Auctions_LI4.Models;

namespace Online_Auctions_LI4.Filters
{
    public class PaginaParaUserLogado : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            string sessaoUser = context.HttpContext.Session.GetString("sessaoUserLogado");

            if(string.IsNullOrEmpty(sessaoUser) )
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { {"controller", "login" }, {"action","Index"} });
            }

            else
            {
                UserModel user = JsonConvert.DeserializeObject<UserModel>(sessaoUser);

                if(user == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "login" }, { "action", "Index" } });
                }
            }

            base.OnActionExecuted(context);
        }
    }
}
