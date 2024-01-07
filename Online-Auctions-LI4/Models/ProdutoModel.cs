using Online_Auctions_LI4.enums;
using Online_Auctions_LI4.Helper;
using System.ComponentModel.DataAnnotations;

namespace Online_Auctions_LI4.Models
{
    public class ProdutoModel
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Imagem { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public Double PrecoBase { get; set; }
        [Required]
        public int Utilizador_ID { get; set; }
        [Required]
        public int Categoria_ID{ get; set; }
    }

}
