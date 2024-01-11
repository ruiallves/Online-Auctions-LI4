using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Online_Auctions_LI4.Filters;
using Online_Auctions_LI4.Helper;
using Online_Auctions_LI4.Models;
using Online_Auctions_LI4.Repositorio;
using System.Diagnostics;

namespace Online_Auctions_LI4.Controllers
{
    [PaginaParaUserLogado]
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly ISessao _sessao;

        public ProdutoController(IProdutoRepositorio produtoRepositorio, ISessao sessao)
        {
              _produtoRepositorio = produtoRepositorio;
              _sessao = sessao;
        }
        public IActionResult Index()
        {
            List<ProdutoModel> produtos = _produtoRepositorio.listaProdutos();
            return View(produtos);
        }

        public IActionResult CriarProduto()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(ProdutoModel produto)
        {
            _produtoRepositorio.Adicionar(produto);
            return RedirectToAction("Index");
        }

        public IActionResult Adicionar(ProdutoModel model)
        {
            model.Utilizador_ID = idSessão();
            _produtoRepositorio.Adicionar(model);
            return RedirectToAction("Index", "Produto");
        }

        public int idSessão()
        {
            UserModel user = _sessao.BuscarSessaoDoUser();
            return user.Id;
        }
    }
}
