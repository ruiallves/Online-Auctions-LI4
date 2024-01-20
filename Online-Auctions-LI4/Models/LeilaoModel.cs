using Online_Auctions_LI4.enums;
using Online_Auctions_LI4.Helper;
using System.ComponentModel.DataAnnotations;

namespace Online_Auctions_LI4.Models
{
    public class LeilaoModel
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        [Required]
        public Double Quantia { get; set; }
        [Required]
        public int Produto_ID { get; set; }
        [Required]
        public LeilaoEnum Status { get; set; }
        public string Imagem { get; set; }
    }

}
