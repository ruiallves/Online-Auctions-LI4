using Microsoft.AspNetCore.Mvc;
using Online_Auctions_LI4.Filters;

namespace Online_Auctions_LI4.Controllers
{
    [PaginaParaUserLogado]
    public class RestritoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
