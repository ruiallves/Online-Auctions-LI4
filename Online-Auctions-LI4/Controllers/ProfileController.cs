using Microsoft.AspNetCore.Mvc;
using Online_Auctions_LI4.Filters;
using Online_Auctions_LI4.Helper;
using Online_Auctions_LI4.Models;
using Online_Auctions_LI4.Repositorio;


namespace Online_Auctions_LI4.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserRepositorio _userRepositorio;
        private readonly ISessao _sessao;

        public ProfileController(IUserRepositorio userRepositorio, ISessao sessao)
        {
            _userRepositorio = userRepositorio;
            _sessao = sessao;
        }

        [PaginaParaUserLogado]
        public ActionResult Index()
        {
            UserModel user = idSessão();

            if (user != null)
            {
                return View(user);
            }
            return View();
        }

        public UserModel idSessão()
        {
            UserModel user = _sessao.BuscarSessaoDoUser();
            return user;
        }
    }
}
