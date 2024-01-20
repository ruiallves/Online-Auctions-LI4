using Microsoft.AspNetCore.Mvc;
using Online_Auctions_LI4.Filters;
using Online_Auctions_LI4.Helper;
using Online_Auctions_LI4.Models;
using Online_Auctions_LI4.Repositorio.Leilao;
using Online_Auctions_LI4.Repositorio.Produto;
using Online_Auctions_LI4.Repositorio.User;


namespace Online_Auctions_LI4.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserRepositorio _userRepositorio;
        private readonly ISessao _sessao;
        private readonly ILeilaoRepositorio _leilaoRepositorio;
        private readonly IProdutoRepositorio _produtoRepositorio;

        public ProfileController(IUserRepositorio userRepositorio, ISessao sessao, ILeilaoRepositorio leilaoRepositorio, IProdutoRepositorio produtoRepositorio)
        {
            _userRepositorio = userRepositorio;
            _sessao = sessao;
            _leilaoRepositorio = leilaoRepositorio;
            _produtoRepositorio = produtoRepositorio;
        }

        [PaginaParaUserLogado]
        public ActionResult Index()
        {
            UserModel user = idSessão();

            var produtos = _produtoRepositorio.ListarProdutosPorUsuario(user.Id);
            var leiloes = _leilaoRepositorio.GetAll();

            Dictionary<UserModel, Dictionary<LeilaoModel, ProdutoModel>> leiloesDoUsuario = new Dictionary<UserModel, Dictionary<LeilaoModel, ProdutoModel>>
    {
        { user, new Dictionary<LeilaoModel, ProdutoModel>() }
    };

            foreach (var leilao in leiloes)
            {
                foreach(var produto in produtos)
                {
                    if(leilao.Produto_ID == produto.Id)
                    {
                        leiloesDoUsuario[user].Add(leilao, produto);
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
