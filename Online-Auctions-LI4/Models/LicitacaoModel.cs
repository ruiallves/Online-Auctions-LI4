using Online_Auctions_LI4.enums;
using Online_Auctions_LI4.Helper;
using System.ComponentModel.DataAnnotations;

namespace Online_Auctions_LI4.Models
{
    public class LicitacaoModel
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int Valor {  get; set; }
        [Required]
        public DateTime Horario { get; set; }
        [Required]
        public int Utilizador_ID { get; set; }
        [Required]
        public int Leilao_ID { get; set; }
    }

}
