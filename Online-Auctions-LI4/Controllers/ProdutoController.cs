using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Online_Auctions_LI4.Filters;
using Online_Auctions_LI4.Helper;
using Online_Auctions_LI4.Models;
using Online_Auctions_LI4.Repositorio.Leilao;
using Online_Auctions_LI4.Repositorio.Produto;
using System.Diagnostics;

namespace Online_Auctions_LI4.Controllers
{
    [PaginaParaUserLogado]
    [PaginaParaLeiloeiro]
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly ISessao _sessao;
        private readonly ILeilaoRepositorio _leilaoRepositorio;

        public ProdutoController(IProdutoRepositorio produtoRepositorio, ISessao sessao, ILeilaoRepositorio leilaoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
            _sessao = sessao;
            _leilaoRepositorio = leilaoRepositorio;
        }
        [PaginaParaUserLogado]
        public IActionResult Index()
        {
            int userId = idSessao();
            List<ProdutoModel> produtosDoUsuario = _produtoRepositorio.ListarProdutosPorUsuario(userId);

            Dictionary<ProdutoModel, LeilaoModel> produtosComLeiloes = new Dictionary<ProdutoModel, LeilaoModel>();

            foreach (var produto in produtosDoUsuario)
            {
                LeilaoModel leilaoDoProduto = _leilaoRepositorio.ObterLeilaoPorProdutoId(produto.Id);
                produtosComLeiloes.Add(produto, leilaoDoProduto);
            }

            return View(produtosComLeiloes);
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
            model.Utilizador_ID = idSessao();
            model.AjustarSemiDescricao();
            _produtoRepositorio.Adicionar(model);

            LeilaoModel leilaoModel = new LeilaoModel();
            leilaoModel.Produto_ID = model.Id;
            leilaoModel.Quantia = model.PrecoBase;
            leilaoModel.Status = enums.LeilaoEnum.Espera;
            leilaoModel.Imagem = model.Imagem;
            _leilaoRepositorio.Adicionar(leilaoModel);

            return RedirectToAction("Index", "Produto");
        }


        public int idSessao()
        {
            UserModel user = _sessao.BuscarSessaoDoUser();
            return user.Id;
        }

    }
}
