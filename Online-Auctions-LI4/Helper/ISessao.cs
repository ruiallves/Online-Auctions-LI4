using Online_Auctions_LI4.Models;

namespace Online_Auctions_LI4.Helper
{
    public interface ISessao
    {
        void CriarSessaoDoUser(UserModel user);
        void RemoverSessaoDoUser();
        UserModel BuscarSessaoDoUser();
    }
}
