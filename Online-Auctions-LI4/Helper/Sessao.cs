using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Online_Auctions_LI4.Models;

namespace Online_Auctions_LI4.Helper
{
    public class Sessao : ISessao
    {
        private readonly IHttpContextAccessor _httpContext;

        public Sessao(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public UserModel BuscarSessaoDoUser()
        {
            string sessaoUser = _httpContext.HttpContext.Session.GetString("sessaoUserLogado");

            if(string.IsNullOrEmpty(sessaoUser))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<UserModel>(sessaoUser); 
        }

        public void CriarSessaoDoUser(UserModel user)
        {
            string valor = JsonConvert.SerializeObject(user);
            _httpContext.HttpContext.Session.SetString("sessaoUserLogado", valor);
        }

        public void RemoverSessaoDoUser()
        {
            _httpContext.HttpContext.Session.Remove("sessaoUserLogado");
        }
    }
}
