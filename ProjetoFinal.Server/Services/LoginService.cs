namespace ProjetoFinal.Server.Services
{
    using ProjetoFinal.Server.Models;
    using Microsoft.AspNetCore.Http;

    public class LoginService
    {
        /// <summary>
        /// Autentica um usuário da loja com base no login e senha fornecidos.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        public static Task<Loja?> AutenticarAsync(string login, string senha)
        {
            Loja loja = Banco.Lojas
            .Find(l => l.Login == login)!;
            if (loja == null) return Task.FromResult<Loja?>(null);
            if(!Password.Verify(senha, loja.Senha)) return Task.FromResult<Loja?>(null);
            return Task.FromResult(loja)!;
        }
    }
}
