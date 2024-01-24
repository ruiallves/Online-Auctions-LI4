using Microsoft.EntityFrameworkCore;
using Online_Auctions_LI4.Data;
using Online_Auctions_LI4.Models;

namespace Online_Auctions_LI4.Repositorio.Produto
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

        public ProdutoModel getProductByID(int id)
        {
            return _bancoContext.Produto.FirstOrDefault(x => x.Id == id);
        }

        public List<ProdutoModel> ListarProdutosPorUsuario(int usuarioId)
        {
            return _bancoContext.Produto.Where(p => p.Utilizador_ID == usuarioId).ToList();
        }

        public UserModel getUserByProductID(int id)
        {
            return _bancoContext.Users.Find(id);
        }

        public ProdutoModel Editar(ProdutoModel produto)
        {
            ProdutoModel produtoDB = getProductByID(produto.Id);

            if(produtoDB == null)
            {
                throw new Exception("Houve um erro ao editar produto.");
            }

            produtoDB.Titulo = produto.Titulo;
            produtoDB.Localizacao = produto.Localizacao;
            produtoDB.PrecoBase = produto.PrecoBase;
            produtoDB.Tipologia = produto.Tipologia;
            produtoDB.Categoria = produto.Categoria;
            produtoDB.Condicao = produto.Condicao;
            produtoDB.AreaUtil = produto.AreaUtil;
            produtoDB.CertificadoEnergetico = produto.CertificadoEnergetico;
            if(produto.Imagem != null)
            {
                produtoDB.Imagem = produto.Imagem;
            }
            produtoDB.Descricao = produto.Descricao;

            _bancoContext.Produto.Update(produtoDB);
            _bancoContext.SaveChanges();
            return produtoDB;
        }

        public bool Apagar(int id)
        {
            ProdutoModel produtoDB = getProductByID(id);

            if (produtoDB == null)
            {
                throw new Exception("Houve um erro ao apagar o produto.");
                return false;
            }

            _bancoContext.Produto.Remove(produtoDB);
            _bancoContext.SaveChanges();

            return true;
        }
    }
}
