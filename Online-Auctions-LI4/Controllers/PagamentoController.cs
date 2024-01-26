using Microsoft.AspNetCore.Mvc;
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
    public class PagamentoController : Controller
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly ILeilaoRepositorio _leilaoRepositorio;
        private readonly ILicitacaoRepositorio _licitacaoRepositorio;
        private readonly ISessao _sessao;
        private readonly IUserRepositorio _usuarioRepositorio;

        public PagamentoController(IProdutoRepositorio produtoRepositorio, ILeilaoRepositorio leilaoRepositorio, ILicitacaoRepositorio licitacaoRepositorio, ISessao sessao, IUserRepositorio usuarioRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
            _leilaoRepositorio = leilaoRepositorio;
            _licitacaoRepositorio = licitacaoRepositorio;
            _sessao = sessao;
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Index(int produto_ID, int user_ID)
        {
            ProdutoModel produto = _produtoRepositorio.getProductByID(produto_ID);
            UserModel usuario = _usuarioRepositorio.ProcuraPorId(user_ID);

            if (produto != null && usuario != null)
            {
                var dados = new Dictionary<ProdutoModel, Dictionary<UserModel, LeilaoModel>>
        {
            {
                produto, new Dictionary<UserModel, LeilaoModel>
                {
                    { usuario, _leilaoRepositorio.ObterLeilaoPorProdutoId(produto_ID) }
                }
            }
        };

                return View(dados);
            }
            else
            {
                return NotFound();
            }
        }



    }
}
