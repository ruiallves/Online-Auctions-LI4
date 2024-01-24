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
    public class ProfileController : Controller
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly ILeilaoRepositorio _leilaoRepositorio;
        private readonly ILicitacaoRepositorio _licitacaoRepositorio;
        private readonly ISessao _sessao;
        private readonly IUserRepositorio _usuarioRepositorio;

        public ProfileController(IProdutoRepositorio produtoRepositorio, ILeilaoRepositorio leilaoRepositorio, ILicitacaoRepositorio licitacaoRepositorio, ISessao sessao, IUserRepositorio usuarioRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
            _leilaoRepositorio = leilaoRepositorio;
            _licitacaoRepositorio = licitacaoRepositorio;
            _sessao = sessao;
            _usuarioRepositorio = usuarioRepositorio;
        }

        [PaginaParaUserLogado]
        public ActionResult Index()
        {
            UserModel userSessao = idSessão();
            var leiloes = _leilaoRepositorio.GetAll();
            List<ProdutoModel> produtos;

            if (userSessao.UserEnum == enums.UserEnum.Leiloeiro || userSessao.UserEnum == enums.UserEnum.Admin)
            {
                produtos = _produtoRepositorio.ListarProdutosPorUsuario(userSessao.Id);
            }
            else
            {
                produtos = _licitacaoRepositorio.BuscarLeiloesPorUsuario(userSessao.Id);
            }

            Dictionary<UserModel, Dictionary<LeilaoModel, Dictionary<ProdutoModel, UserModel>>> leiloesDoUsuario =
                new Dictionary<UserModel, Dictionary<LeilaoModel, Dictionary<ProdutoModel, UserModel>>>
            {
        { userSessao, new Dictionary<LeilaoModel, Dictionary<ProdutoModel, UserModel>>() }
            };

            foreach (var leilao in leiloes)
            {
                foreach (var produto in produtos)
                {
                    if (leilao.Produto_ID == produto.Id)
                    {
                        LicitacaoModel ultimaLicitacao = _licitacaoRepositorio.BuscarUltimaLicitacaoPorLeilao(leilao.Id);

                        if (ultimaLicitacao != null)
                        {
                            UserModel userVencedor = _usuarioRepositorio.ProcuraPorId(ultimaLicitacao.Utilizador_ID);

                            if (!leiloesDoUsuario[userSessao].ContainsKey(leilao))
                            {
                                leiloesDoUsuario[userSessao].Add(leilao, new Dictionary<ProdutoModel, UserModel>());
                            }

                            leiloesDoUsuario[userSessao][leilao].Add(produto, userVencedor);
                        }
                        else
                        {
                            UserModel userVencedor = userSessao;

                            if (!leiloesDoUsuario[userSessao].ContainsKey(leilao))
                            {
                                leiloesDoUsuario[userSessao].Add(leilao, new Dictionary<ProdutoModel, UserModel>());
                            }

                            leiloesDoUsuario[userSessao][leilao].Add(produto, userVencedor);
                        }
                    }
                }
            }

            return View(leiloesDoUsuario);
        }


        public UserModel idSessão()
        {
            UserModel user = _sessao.BuscarSessaoDoUser();
            return user;
        }
    }
}
