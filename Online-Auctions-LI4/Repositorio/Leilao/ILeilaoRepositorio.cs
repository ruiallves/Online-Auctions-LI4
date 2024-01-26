using Online_Auctions_LI4.Models;

namespace Online_Auctions_LI4.Repositorio.Leilao
{
    public interface ILeilaoRepositorio
    {
        public int buscaLeilao(int id);
        public LeilaoModel Adicionar(LeilaoModel leilao);

        public LeilaoModel buscaLeilaoModel(int id);
        public LeilaoModel ObterLeilaoPorProdutoId(int produtoId);
        public LeilaoModel Iniciar(int leilaoId, DateTime novaDataFinal);
        public List<LeilaoModel> GetAll();
        public void SaveChanges();
        public bool Apagar(int id);
        public LeilaoModel Editar (LeilaoModel leilao, double novoPreco);
        public int GetLeiloesCriados(int userId);
    }
}
