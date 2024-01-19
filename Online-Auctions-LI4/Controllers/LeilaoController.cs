using Microsoft.AspNetCore.Mvc;
using Online_Auctions_LI4.Filters;
using Online_Auctions_LI4.Models;
using Online_Auctions_LI4.Repositorio;

namespace Online_Auctions_LI4.Controllers
{
    [PaginaParaUserLogado]
    public class LeilaoController : Controller
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly ILeilaoRepositorio _leilaoRepositorio;

        public LeilaoController(IProdutoRepositorio produtoRepositorio, ILeilaoRepositorio leilaoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
            _leilaoRepositorio = leilaoRepositorio;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Adicionar(LeilaoModel model)
        {
            _leilaoRepositorio.Adicionar(model);
            return RedirectToAction("Index", "Produto");
        }

        public IActionResult getLeilao(int id)
        {

            LeilaoModel leilaoModel = _leilaoRepositorio.buscaLeilaoModel(id);

            if (leilaoModel != null)
            {
                var produtoAssociado = _produtoRepositorio.getProductByID(leilaoModel.Produto_ID);

                if (produtoAssociado != null && !string.IsNullOrEmpty(produtoAssociado.Imagem))
                {
                    ViewBag.Imagem = Url.Content("~/" + produtoAssociado.Imagem);
                }
                else
                {
                    ViewBag.Imagem = Url.Content("~/caminho/imagem/default.jpg");
                }
            }
            else
            {
                ViewBag.Imagem = Url.Content("~/caminho/imagem/default.jpg");
            }

            return View(leilaoModel);
        }

        [HttpPost]
        public IActionResult Iniciar(int leilaoId, DateTime novaDataFinal)
        {
            try
            {
                _leilaoRepositorio.Iniciar(leilaoId, novaDataFinal);
                return RedirectToAction("Index", "Produto");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Erro", "Home");
            }
        }


    }
}
