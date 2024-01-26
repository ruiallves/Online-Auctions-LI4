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

        public UserModel Editar(UserModel user)
        {
            UserModel userDB = ProcuraPorId(user.Id);
            if(userDB == null)
            {
                throw new Exception("Não foi possivel editar o user");

            }

            userDB.Name = user.Name;
            userDB.Username = user.Username;
            if(user.Password != null)
            {
                userDB.Password = user.Password;
            }
            userDB.Email = user.Email;
            userDB.NIF = user.NIF;
            userDB.morada = user.morada;

            _context.Users.Update(userDB);
            _context.SaveChanges();
            return userDB;
        }

        public bool IsNIFUnique(String nif)
        {
                return !_context.Users.Any(u => String.Equals(u.NIF,nif));
        }

        public bool IsEmailUnique(string email)
        {
            return !_context.Users.Any(u => u.Email == email);
        }

        public bool IsUserMailUnique(string userMail)
        {
            return !_context.Users.Any(u => u.Email == userMail);
        }

        public List<UserModel> GetAllUsers()
        {
            return _context.Users.ToList();
        }
    }
}
