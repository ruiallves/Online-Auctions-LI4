using Online_Auctions_LI4.Models;

namespace Online_Auctions_LI4.Repositorio.Produto
{
    public interface IProdutoRepositorio
    {
        ProdutoModel Adicionar(ProdutoModel produto);
        public List<ProdutoModel> listaProdutos();
        public ProdutoModel getProductByID(int id);
        public List<ProdutoModel> ListarProdutosPorUsuario(int usuarioId);
        public UserModel getUserByProductID(int id);
    }
}
