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

        public IActionResult EditarProduto(int id)
        {
            ProdutoModel produto = _produtoRepositorio.getProductByID(id);
            return View(produto);
        }

        [HttpPost]
        public IActionResult Criar(ProdutoModel produto)
        {
            _produtoRepositorio.Adicionar(produto);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Editar(ProdutoModel produto)
        {
            _produtoRepositorio.Editar(produto);
            LeilaoModel leilaoDoProduto = _leilaoRepositorio.ObterLeilaoPorProdutoId(produto.Id);
            _leilaoRepositorio.Editar(leilaoDoProduto, produto.PrecoBase);
            return RedirectToAction("Index");
        }
        public IActionResult Apagar(int id)
        {
            _produtoRepositorio.Apagar(id);
            LeilaoModel leilaoDoProduto = _leilaoRepositorio.ObterLeilaoPorProdutoId(id);
            _leilaoRepositorio.Apagar(leilaoDoProduto.Id);
            return RedirectToAction("Index");
        }

        public IActionResult Adicionar(ProdutoModel model)
        {
            model.Utilizador_ID = idSessao();
            AjustarSemiDescricao(model.Descricao);
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

        public String AjustarSemiDescricao(String descricao)
        {
            if (descricao.Length > 180)
            {
                return descricao.Substring(0, 180) + "...";
            }
            else
            {
                return descricao;
            }
        }

    }
}
