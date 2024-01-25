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
            // Check if NIF is unique
            if (!_userRepositorio.IsNIFUnique(user.NIF))
            {
                ModelState.AddModelError(nameof(UserModel.NIF), "This NIF is already in use.");
            }

            // Check if email is unique
            if (!_userRepositorio.IsEmailUnique(user.Email))
            {
                ModelState.AddModelError(nameof(UserModel.Email), "This email is already in use.");
            }

            // Check if userMail is unique
            if (!_userRepositorio.IsUserMailUnique(user.Email))
            {
                ModelState.AddModelError(nameof(UserModel.Email), "This userMail is already in use.");
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
