namespace ProjetoFinal.Server.Services
{
    using BlMadre.C_.Models;
    using ProjetoFinal.Server.Models;

    /// <summary>
    /// Banco de dados simulado para armazenar as lojas.
    /// </summary>
    public static class Banco
    {
        public static List<Loja> Lojas { get; set; } = new List<Loja>();

        /// <summary>
        /// Procura uma loja pelo login e senha enviados.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        public static Task<Loja?> FindLojaByUser(string login, string senha)
        {
            return LoginService.AutenticarAsync(login, senha);
        }
    }
}
