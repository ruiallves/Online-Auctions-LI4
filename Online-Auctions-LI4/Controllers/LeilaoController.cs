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
                // Obter informações do produto associado
                var produtoAssociado = _produtoRepositorio.getProductByID(leilaoModel.Produto_ID);

                // Verificar se o produtoAssociado não é nulo antes de acessar suas propriedades
                if (produtoAssociado != null && !string.IsNullOrEmpty(produtoAssociado.Imagem))
                {
                    ViewBag.Imagem = Url.Content("~/" + produtoAssociado.Imagem);
                }
                else
                {
                    // Defina um valor padrão ou trate de outra forma quando o produtoAssociado ou a imagem são nulos
                    ViewBag.Imagem = Url.Content("~/caminho/imagem/default.jpg");
                }
            }
            else
            {
                // Defina um valor padrão ou trate de outra forma quando o leilaoModel é nulo
                ViewBag.Imagem = Url.Content("~/caminho/imagem/default.jpg");
            }

            return View(leilaoModel);
        }


    }
}
