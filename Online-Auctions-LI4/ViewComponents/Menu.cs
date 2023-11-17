using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Online_Auctions_LI4.Models;

namespace Online_Auctions_LI4.ViewComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string sessaoUser = HttpContext.Session.GetString("sessaoUserLogado");

            if (string.IsNullOrEmpty(sessaoUser))
            {
                return Content(string.Empty);
            }

            UserModel user = JsonConvert.DeserializeObject<UserModel>(sessaoUser);

            return View(user);
        }

    }
}
