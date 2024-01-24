using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Auctions_LI4.Filters;
using Online_Auctions_LI4.Models;
using Online_Auctions_LI4.Repositorio.Leilao;
using Online_Auctions_LI4.Repositorio.Produto;
using System.Diagnostics;

namespace Online_Auctions_LI4.Controllers
{
    [PaginaParaUserLogado]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly ILeilaoRepositorio _leilaoRepositorio;

        public HomeController(ILogger<HomeController> logger, IProdutoRepositorio produtoRepositorio, ILeilaoRepositorio leilaoRepositorio)
        {
            _logger = logger;
            _produtoRepositorio = produtoRepositorio;
            _leilaoRepositorio = leilaoRepositorio;
        }

        public IActionResult Index()
        {
            var leiloesEProdutos = new Dictionary<LeilaoModel, ProdutoModel>();

            var leiloes = _leilaoRepositorio.GetAll();
            var produtos = _produtoRepositorio.listaProdutos();

            foreach (var leilao in leiloes)
            {
                var produtoAssociado = produtos.FirstOrDefault(p => p.Id == leilao.Produto_ID);
                if (produtoAssociado != null && leilao.Status == enums.LeilaoEnum.Iniciado)
                {
                    leiloesEProdutos.Add(leilao, produtoAssociado);
                }
            }

            return View(leiloesEProdutos);
        }


        public IActionResult Faq()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
