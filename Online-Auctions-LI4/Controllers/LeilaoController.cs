using Microsoft.AspNetCore.Mvc;
using Online_Auctions_LI4.Filters;
using Online_Auctions_LI4.Models;
using Online_Auctions_LI4.Repositorio.Leilao;
using Online_Auctions_LI4.Repositorio.Licitacao;
using Online_Auctions_LI4.Repositorio.Produto;

namespace Online_Auctions_LI4.Controllers
{
    [PaginaParaUserLogado]
    public class LeilaoController : Controller
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly ILeilaoRepositorio _leilaoRepositorio;
        private readonly ILicitacaoRepositorio _licitacaoRepositorio;

        public LeilaoController(IProdutoRepositorio produtoRepositorio, ILeilaoRepositorio leilaoRepositorio, ILicitacaoRepositorio licitacaoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
            _leilaoRepositorio = leilaoRepositorio;
            _licitacaoRepositorio = licitacaoRepositorio;
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

        public IActionResult Licitar(LicitacaoModel model, int leilaoId)
        {
            _licitacaoRepositorio.Licitar(model);
            return RedirectToAction("getLeilao", new { id = leilaoId });
        }

        public IActionResult getLeilao(int id)
        {
            LeilaoModel leilaoModel = _leilaoRepositorio.buscaLeilaoModel(id);

            if (leilaoModel == null)
            {
                return RedirectToAction("Index", "Produto");
            }

            ProdutoModel produtoAssociado = _produtoRepositorio.getProductByID(leilaoModel.Produto_ID);
            UserModel userAssociado = _produtoRepositorio.getUserByProductID(produtoAssociado.Utilizador_ID);

            if (produtoAssociado != null && !string.IsNullOrEmpty(produtoAssociado.Imagem))
            {
                ViewBag.Imagem = Url.Content("~/" + produtoAssociado.Imagem);
            }
            else
            {
                ViewBag.Imagem = Url.Content("~/caminho/imagem/default.jpg");
            }

            Dictionary<ProdutoModel, UserModel> produtoUserDict =
                new Dictionary<ProdutoModel, UserModel> { { produtoAssociado, userAssociado } };

            Dictionary<LeilaoModel, Dictionary<ProdutoModel, UserModel>> leilaoComProduto =
                new Dictionary<LeilaoModel, Dictionary<ProdutoModel, UserModel>>
                {
            { leilaoModel, produtoUserDict }
                };

            return View(leilaoComProduto);
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
