using System.ComponentModel.DataAnnotations;

namespace Online_Auctions_LI4.Models
{
    public class ProfileModel
    {
        internal string ImageUrl;

        [Key]

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(20, ErrorMessage = "Username cannot be longer than 20 characters.")]
        public string Username { get; set; }

        [StringLength(50, ErrorMessage = "Location cannot be longer than 50 characters.")]
        public string Location { get; set; }
    }
}
