using Microsoft.AspNetCore.Mvc;
using Online_Auctions_LI4.Models;


namespace Online_Auctions_LI4.Controllers
{
    public class ProfileController : Controller
    {
        private string ImageUrl;

        public string GetImageUrl()
        {
            return ImageUrl;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
