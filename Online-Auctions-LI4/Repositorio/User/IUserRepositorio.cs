using Online_Auctions_LI4.Models;

namespace Online_Auctions_LI4.Repositorio.User
{
    public interface IUserRepositorio
    {
        UserModel ProcuraPorLogin(string login);
        UserModel Register(UserModel user);
        UserModel ProcuraPorId(int userId);
        UserModel Editar(UserModel user);
        public bool IsNIFUnique(String nif);
        public bool IsEmailUnique(string email);
        public bool IsUserMailUnique(string userMail);
        List<UserModel> GetAllUsers();
        public bool Apagar(int id);

    }
}
