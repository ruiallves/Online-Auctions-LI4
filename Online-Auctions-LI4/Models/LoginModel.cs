using System.ComponentModel.DataAnnotations;

namespace Online_Auctions_LI4.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Digite o Email.")]
        public string Login {  get; set; }
        [Required(ErrorMessage = "Digite a Password.")]
        public string Senha {  get; set; }
    }
}
