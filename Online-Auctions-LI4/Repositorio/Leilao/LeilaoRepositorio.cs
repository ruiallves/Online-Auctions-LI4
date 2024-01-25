using Microsoft.EntityFrameworkCore;
using Online_Auctions_LI4.Data;
using Online_Auctions_LI4.enums;
using Online_Auctions_LI4.Models;
using System.Data;

namespace Online_Auctions_LI4.Repositorio.Leilao
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
            return _bancoContext.Leilao.FirstOrDefault(l => l.Id == id);
        }

        public LeilaoModel ObterLeilaoPorProdutoId(int produtoId)
        {
            return _bancoContext.Leilao.FirstOrDefault(l => l.Produto_ID == produtoId);
        }

        public LeilaoModel Adicionar(LeilaoModel leilao)
        {
            _bancoContext.Leilao.Add(leilao);
            _bancoContext.SaveChanges();
            return leilao;
        }

        public LeilaoModel Iniciar(int leilaoId, DateTime novaDataFinal)
        {
            LeilaoModel leilaoDB = buscaLeilaoModel(leilaoId);

            if (leilaoDB == null)
            {
                throw new System.Exception("Houve um erro ao iniciar o leilão.");
            }

            leilaoDB.DataInicial = DateTime.Now;
            leilaoDB.DataFinal = novaDataFinal; 
            leilaoDB.Status = enums.LeilaoEnum.Iniciado;
            _bancoContext.SaveChanges();
            return leilaoDB;
        }

        public void SaveChanges()
        {
            _bancoContext.SaveChanges();
        }

        public List<LeilaoModel> GetAll()
        {
            return _bancoContext.Leilao.ToList();
        }

        public bool Apagar(int id)
        {
            LeilaoModel leilaoDB = buscaLeilaoModel(id);

            if (leilaoDB == null)
            {
                throw new Exception("Houve um erro ao apagar o leilao.");
            }

            _bancoContext.Leilao.Remove(leilaoDB);
            _bancoContext.SaveChanges();

            return true;
        }

        public LeilaoModel Editar(LeilaoModel leilao, double novoPreco)
        {
            LeilaoModel leilaoDB = buscaLeilaoModel(leilao.Id);

            if (leilaoDB == null)
            {
                throw new Exception("Houve um erro ao editar o leilao.");
            }
            
            leilaoDB.Quantia = novoPreco;
            _bancoContext.SaveChanges();
            return leilaoDB;
        }
    }
}
