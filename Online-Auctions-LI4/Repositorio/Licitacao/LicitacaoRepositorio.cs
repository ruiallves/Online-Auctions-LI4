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
    }
}
