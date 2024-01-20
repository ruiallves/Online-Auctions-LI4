﻿using Microsoft.EntityFrameworkCore;
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
            return _bancoContext.Produto.Find(id);
        }

        public List<ProdutoModel> ListarProdutosPorUsuario(int usuarioId)
        {
            return _bancoContext.Produto.Where(p => p.Utilizador_ID == usuarioId).ToList();
        }

        public UserModel getUserByProductID(int id)
        {
            return _bancoContext.Users.Find(id);
        }
    }
}