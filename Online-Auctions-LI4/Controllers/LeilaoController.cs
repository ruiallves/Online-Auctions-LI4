using Microsoft.AspNetCore.Mvc;
using Online_Auctions_LI4.Data;
using Online_Auctions_LI4.Filters;
using Online_Auctions_LI4.Helper;
using Online_Auctions_LI4.Models;
using Online_Auctions_LI4.Repositorio.Leilao;
using Online_Auctions_LI4.Repositorio.Licitacao;
using Online_Auctions_LI4.Repositorio.Produto;
using Online_Auctions_LI4.Repositorio.User;

namespace Online_Auctions_LI4.Controllers
{
    [PaginaParaUserLogado]
    public class LeilaoController : Controller
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly ILeilaoRepositorio _leilaoRepositorio;
        private readonly ILicitacaoRepositorio _licitacaoRepositorio;
        private readonly ISessao _sessao;
        private readonly IUserRepositorio _usuarioRepositorio;

        public LeilaoController(IProdutoRepositorio produtoRepositorio, ILeilaoRepositorio leilaoRepositorio, ILicitacaoRepositorio licitacaoRepositorio, ISessao sessao, IUserRepositorio usuarioRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
            _leilaoRepositorio = leilaoRepositorio;
            _licitacaoRepositorio = licitacaoRepositorio;
            _sessao = sessao;
            _usuarioRepositorio = usuarioRepositorio;
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
            LeilaoModel leilaoModel = _leilaoRepositorio.buscaLeilaoModel(leilaoId);
            ProdutoModel produto = _produtoRepositorio.getProductByID(leilaoModel.Produto_ID);

            if(model.Utilizador_ID == produto.Utilizador_ID)
            {
                TempData["ErroLicitacao"] = "Você não pode licitar no seu próprio leilão.";
                return RedirectToAction("getLeilao", new { id = leilaoId });
            }

            if (NovaLicitacaoMaior(model, leilaoId))
            {
                _licitacaoRepositorio.Licitar(model);
                TempData["SucessoLicitacao"] = "Licitação realizada com sucesso!";

                LeilaoModel leilaoAtual = _leilaoRepositorio.buscaLeilaoModel(leilaoId);
                leilaoAtual.Quantia = model.Valor;
                _leilaoRepositorio.SaveChanges();

                return RedirectToAction("getLeilao", new { id = leilaoId });
            }
            else
            {
                TempData["ErroLicitacao"] = "A nova licitação não é maior do que a última. Por favor, faça uma licitação maior.";

                return RedirectToAction("getLeilao", new { id = leilaoId });
            }
        }

        public Boolean NovaLicitacaoMaior(LicitacaoModel model, int leilaoId)
        {
            LeilaoModel leilao = _leilaoRepositorio.buscaLeilaoModel(leilaoId);
            LicitacaoModel ultimaLicitação = _licitacaoRepositorio.BuscarUltimaLicitacaoPorLeilao(leilaoId);

            if(ultimaLicitação == null)
            {
                if(model.Valor > leilao.Quantia)
                {
                    return true;
                }
            }
            else
            {
                if(model.Valor > ultimaLicitação.Valor)
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
            UserModel userSessao = _sessao.BuscarSessaoDoUser();
            LicitacaoModel ultimaLicitacao = _licitacaoRepositorio.BuscarUltimaLicitacaoPorLeilao(id);
            UserModel userAVencer = _sessao.BuscarSessaoDoUser();
            if (ultimaLicitacao != null && ultimaLicitacao.Utilizador_ID != null)
            {
                userAVencer = _usuarioRepositorio.ProcuraPorId(ultimaLicitacao.Utilizador_ID);
            }
            int numeroLicitacoes = _licitacaoRepositorio.ContarLicitacoesPorLeilao(id);

            if (produtoAssociado != null && !string.IsNullOrEmpty(produtoAssociado.Imagem))
            {
                ViewBag.Imagem = Url.Content("~/" + produtoAssociado.Imagem);
            }
            else
            {
                ViewBag.Imagem = Url.Content("~/caminho/imagem/default.jpg");
            }

            Dictionary<UserModel, int> userLicitacoesDict = new Dictionary<UserModel, int>
            {
                { userSessao, numeroLicitacoes }
            };

            Dictionary<ProdutoModel, Dictionary<UserModel, Dictionary<UserModel, int>>> produtoUserDict =
                new Dictionary<ProdutoModel, Dictionary<UserModel, Dictionary<UserModel, int>>>
                {
            { produtoAssociado, new Dictionary<UserModel, Dictionary<UserModel, int>> { { userAVencer, userLicitacoesDict } } }
                };

            Dictionary<LeilaoModel, Dictionary<ProdutoModel, Dictionary<UserModel, Dictionary<UserModel, int>>>> leilaoComProduto =
                new Dictionary<LeilaoModel, Dictionary<ProdutoModel, Dictionary<UserModel, Dictionary<UserModel, int>>>>
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
