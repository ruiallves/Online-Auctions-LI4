using Online_Auctions_LI4.enums;
using Online_Auctions_LI4.Helper;
using System.ComponentModel.DataAnnotations;

namespace Online_Auctions_LI4.Models
{
    public class IndexViewModel
    {
        public List<ProdutoModel> ListaDeProdutos { get; set; }
        public UserModel UserModel { get; set; }
    }


}
