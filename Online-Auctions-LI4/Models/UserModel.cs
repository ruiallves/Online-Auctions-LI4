using Online_Auctions_LI4.enums;
using System.ComponentModel.DataAnnotations;

namespace Online_Auctions_LI4.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int NIF { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string morada { get; set; }
        [Required]
        public UserEnum UserEnum { get; set; }


        public bool SenhaValida(string senha)
        {
            return Password == senha;
        }

    }
}
