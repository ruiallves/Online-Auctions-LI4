using Online_Auctions_LI4.Models;

namespace Online_Auctions_LI4.Repositorio
{
    public interface IProdutoRepositorio
    {
        ProdutoModel Adicionar(ProdutoModel produto);
        public List<ProdutoModel> listaProdutos();
        public ProdutoModel getProductByID(int id);

    }
}
