using Online_Auctions_LI4.Data;
using Online_Auctions_LI4.Models;

namespace Online_Auctions_LI4.Repositorio.User
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
            return _context.Users.FirstOrDefault(x => x.Email.ToUpper() == login.ToUpper());
        }

        public UserModel Register(UserModel user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public UserModel ProcuraPorId(int userId)
        {
            return _context.Users.FirstOrDefault(x => x.Id == userId);
        }
    }
}
