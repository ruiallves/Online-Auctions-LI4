using Microsoft.AspNetCore.Mvc;
using Online_Auctions_LI4.Helper;
using Online_Auctions_LI4.Models;
using Online_Auctions_LI4.Repositorio;

namespace Online_Auctions_LI4.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserRepositorio _userRepositorio;
        private readonly ISessao _sessao;


        public LoginController(IUserRepositorio userRepositorio, ISessao sessao) 
        {
            _userRepositorio = userRepositorio;
            _sessao = sessao;
        }
        public IActionResult Index()
        {
            //se o user tiver logado, redirecionar para a home.

            if(_sessao.BuscarSessaoDoUser() != null)
            {
                return RedirectToAction("Index","Home");
            }
            return View();
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoDoUser();
            return RedirectToAction("Index","Login");
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserModel user = _userRepositorio.ProcuraPorLogin(loginModel.Login);

                    if(user != null)
                    {
                        if (user.SenhaValida(loginModel.Senha))
                        {
                            _sessao.CriarSessaoDoUser(user);
                            return RedirectToAction("Index", "Home");
                        }
                    }


                    TempData["MensagemErro"] = $"Usuario e/ou Senha incorreto(s)";
                }

                return View("Index");

            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = "Erro: " + ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
