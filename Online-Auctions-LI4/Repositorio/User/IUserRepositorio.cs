using Online_Auctions_LI4.Models;

namespace Online_Auctions_LI4.Repositorio.User
{
    public interface IUserRepositorio
    {
        UserModel ProcuraPorLogin(string login);
        UserModel Register(UserModel user);
        UserModel ProcuraPorId(int userId);
    }
}
