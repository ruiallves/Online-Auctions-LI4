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
            var viewModel = new ProfileModel();
            return View(viewModel);
        }

        public IActionResult Index(string imageUrl)
        {
            ProfileModel profileModel = new ProfileModel
            {
                ImageUrl = "https://example.com/default-profile-picture.jpg",
                Name = "Jeronimo Antonio",
                Username = "jermsant",
                Location = "Braga, Portugal"
            };

            return View(profileModel);
        }
    }
}
