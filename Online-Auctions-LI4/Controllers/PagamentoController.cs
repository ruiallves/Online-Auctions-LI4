using Microsoft.AspNetCore.Mvc;
using Online_Auctions_LI4.Filters;
using Online_Auctions_LI4.Helper;
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

        public IActionResult Index()
        {
            return View();
        }
    }
}
