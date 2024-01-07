using Microsoft.AspNetCore.Mvc;
using Online_Auctions_LI4.Filters;
using Online_Auctions_LI4.Models;
using Online_Auctions_LI4.Repositorio;
using System.Diagnostics;

namespace Online_Auctions_LI4.Controllers
{
    [PaginaParaUserLogado]
    public class LeilaoController : Controller
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        public LeilaoController(IProdutoRepositorio produtoRepositorio)
        {
              _produtoRepositorio = produtoRepositorio;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(ProdutoModel produto)
        {
            _produtoRepositorio.Adicionar(produto);
            return RedirectToAction("Index");
        }
    }
}
