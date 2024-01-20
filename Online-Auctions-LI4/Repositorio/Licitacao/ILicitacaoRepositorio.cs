using Online_Auctions_LI4.Models;

namespace Online_Auctions_LI4.Repositorio.Licitacao
{
    public interface ILicitacaoRepositorio
    {
        public LicitacaoModel Licitar(LicitacaoModel model);
        public LicitacaoModel BuscarUltimaLicitacaoPorLeilao(int leilaoId);
    }
}
