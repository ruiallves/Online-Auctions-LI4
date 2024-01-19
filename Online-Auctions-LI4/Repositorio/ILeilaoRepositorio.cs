using Online_Auctions_LI4.Models;

namespace Online_Auctions_LI4.Repositorio
{
    public interface ILeilaoRepositorio
    {
        public int buscaLeilao(int id);
        public LeilaoModel Adicionar(LeilaoModel leilao);

        public LeilaoModel buscaLeilaoModel(int id);
        public LeilaoModel ObterLeilaoPorProdutoId(int produtoId);
        public LeilaoModel Iniciar(int leilaoId, DateTime novaDataFinal);
    }
}
