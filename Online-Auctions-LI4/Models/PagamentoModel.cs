using System.ComponentModel.DataAnnotations;

namespace Online_Auctions_LI4.Models
{
    public class PagamentoModel
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int Utilizador_ID { get; set; }
        [Required]
        public int Produto_ID { get; set; }
    }
}
