using Microsoft.AspNetCore.Mvc;
using Online_Auctions_LI4.Data;
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
            if (NovaLicitacaoMaior(model, leilaoId))
            {
                _licitacaoRepositorio.Licitar(model);
                TempData["SucessoLicitacao"] = "Licita��o realizada com sucesso!";

                LeilaoModel leilaoAtual = _leilaoRepositorio.buscaLeilaoModel(leilaoId);
                leilaoAtual.Quantia = model.Valor;
                _leilaoRepositorio.SaveChanges();

                return RedirectToAction("getLeilao", new { id = leilaoId });
            }
            else
            {
                TempData["ErroLicitacao"] = "A nova licita��o n�o � maior do que a �ltima. Por favor, fa�a uma licita��o maior.";

                return RedirectToAction("getLeilao", new { id = leilaoId });
            }
        }

        public Boolean NovaLicitacaoMaior(LicitacaoModel model, int leilaoId)
        {
            LeilaoModel leilao = _leilaoRepositorio.buscaLeilaoModel(leilaoId);
            LicitacaoModel ultimaLicita��o = _licitacaoRepositorio.BuscarUltimaLicitacaoPorLeilao(leilaoId);

            if(ultimaLicita��o == null)
            {
                if(model.Valor > leilao.Quantia)
                {
                    return true;
                }
            }
            else
            {
                if(model.Valor > ultimaLicita��o.Valor)
                {
                    return true;
                }
            }

            return false;
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
