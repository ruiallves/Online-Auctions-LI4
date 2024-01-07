using Microsoft.EntityFrameworkCore;
using Online_Auctions_LI4.Data;
using Online_Auctions_LI4.Models;

namespace Online_Auctions_LI4.Repositorio
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private readonly BancoContext _bancoContext;
        public ProdutoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public ProdutoModel Adicionar(ProdutoModel produto)
        {
            _bancoContext.Produto.Add(produto);
            _bancoContext.SaveChanges();
            return produto;
        }

        public List<ProdutoModel> listaProdutos()
        {
            return _bancoContext.Produto.ToList();
        }
    }
}
