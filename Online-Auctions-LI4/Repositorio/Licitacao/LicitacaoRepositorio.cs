using Online_Auctions_LI4.Data;
using Online_Auctions_LI4.Models;
using Online_Auctions_LI4.Repositorio.Licitacao;
using System.Data;

namespace Online_Auctions_LI4.Repositorio.Licitacao
{
    public class LicitacaoRepositorio : ILicitacaoRepositorio
    {
        private readonly BancoContext _bancoContext;
        public LicitacaoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
       
        public LicitacaoModel Licitar(LicitacaoModel model)
        {
            _bancoContext.Licitacao.Add(model);
            _bancoContext.SaveChanges();
            return model;   
        }

        public LicitacaoModel BuscarUltimaLicitacaoPorLeilao(int leilaoId)
        {
            var ultimaLicitacao = _bancoContext.Licitacao
                .Where(l => l.Leilao_ID == leilaoId)
                .OrderByDescending(l => l.Horario)
                .FirstOrDefault();
            return ultimaLicitacao;
        }
    }
}
