using Microsoft.AspNetCore.Mvc;
using Online_Auctions_LI4.Helper;
using Online_Auctions_LI4.Models;
using Online_Auctions_LI4.Repositorio.User;

namespace Online_Auctions_LI4.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IUserRepositorio _userRepositorio;
        public RegisterController(IUserRepositorio userRepositorio)
        {
            _userRepositorio = userRepositorio;
        }
            public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registar(UserModel user)
        {
            if (!_userRepositorio.IsNIFUnique(user.NIF.ToString()))
            {
                TempData["ErroLicitacao"] = "Uma conta com esse NIF já existe, tente novamente.";
                return RedirectToAction("Index", "Register");
            }

            if (!_userRepositorio.IsEmailUnique(user.Email))
            {
                TempData["ErroLicitacao"] = "Uma conta com esse Email já existe, tente novamente.";
                return RedirectToAction("Index", "Register");
            }

            if (!_userRepositorio.IsUserMailUnique(user.Username))
            {
                TempData["ErroLicitacao"] = "Uma conta com esse Username já existe, tente novamente.";
                return RedirectToAction("Index", "Register");
            }

            if (!ModelState.IsValid)
            {
                return View("Index", user); 
            }

            user.UserEnum = enums.UserEnum.Licitador;
            user.setSenhaHash();
            _userRepositorio.Register(user);

            return RedirectToAction("Index", "Login");
        }


    }
}
