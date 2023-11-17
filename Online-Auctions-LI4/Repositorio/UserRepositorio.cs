using Online_Auctions_LI4.Data;
using Online_Auctions_LI4.Models;

namespace Online_Auctions_LI4.Repositorio
{
    public class UserRepositorio : IUserRepositorio
    {
        private readonly BancoContext _context;

        public UserRepositorio(BancoContext context)
        {
            _context = context; 
        }

        public UserModel ProcuraPorLogin(string login)
        {
            return _context.Users.FirstOrDefault(x => x.Username.ToUpper() == login.ToUpper());
        }
    }
}
