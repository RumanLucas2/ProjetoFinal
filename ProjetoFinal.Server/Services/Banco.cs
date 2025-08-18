using ProjetoFinal.Server.Models;

namespace ProjetoFinal.Server.Services
{
    #region summary
    /// <summary>
    /// Banco de dados simulado para armazenar as lojas.
    /// </summary>
    /// 
    #endregion
    public static class Banco
    {
        #region summary
        /// <summary>
        /// Construtor estático para inicializar a lista de lojas.
        /// </summary>
        /// 
        #endregion
        public static List<Loja> Lojas { get; set; } = new List<Loja>();

        #region summary
        /// <summary>
        /// Procura uma loja pelo login e senha enviados.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        /// 
        #endregion
        public static Task<Loja?> FindLojaByUser(string login, string senha)
        {
            return LoginService.AutenticarAsync(login, senha);
        }
    }
}
