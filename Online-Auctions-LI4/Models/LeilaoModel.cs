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
        [Required]
        public string Titulo { get; set; }
        [Required]
        public DateTime DataIncial { get; set; }
        [Required]
        public DateTime DataFinal { get; set; }
        [Required]
        public int Quantia { get; set; }
        [Required]
        public int Produto_ID { get; set; }
        [Required]
        public int Licitacao_ID { get; set; }
        [Required]
        public int HistoricoDeLances_ID { get; set; }
    }

}
