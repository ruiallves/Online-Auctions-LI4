using Online_Auctions_LI4.Data;
using Online_Auctions_LI4.Models;

namespace Online_Auctions_LI4.Repositorio
{
    public class LeilaoRepositorio : ILeilaoRepositorio
    {
        private readonly BancoContext _bancoContext;
        public LeilaoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public int buscaLeilao(int id)
        {
            var produto = _bancoContext.Produto.Find(id);

            if (produto != null)
            {
                var leilao = _bancoContext.Leilao
                    .Where(l => l.Produto_ID == id)
                    .FirstOrDefault();

                return leilao.Id;
            }
            return 0;
        }

        public LeilaoModel buscaLeilaoModel(int id)
        {
            // Obter o leilão com base no ID
            var leilao = _bancoContext.Leilao.Find(id);

            // Verificar se o leilão foi encontrado
            if (leilao != null)
            {
                return leilao;
            }

            // Se não foi encontrado, retorne null ou trate conforme necessário
            return null;
        }

        public LeilaoModel Adicionar(LeilaoModel leilao)
        {
            _bancoContext.Leilao.Add(leilao);
            _bancoContext.SaveChanges();
            return leilao;
        }

    }
}
