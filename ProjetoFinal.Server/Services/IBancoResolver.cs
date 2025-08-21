namespace ProjetoFinal.Server.Services
{
    /// <summary>
    /// Interface para resolver o banco de dados do usuário baseado no ID.
    /// </summary>
    public interface IBancoResolver
    {
        /// <summary>
        /// Resolve o banco de dados do usuário baseado no ID fornecido.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        string? Resolve(string userId);
    }

    /// <summary>
    /// Banco de dados em memória que resolve o banco baseado no ID do usuário.
    /// </summary>
    public class BancoResolverInMemory : IBancoResolver
    {
        private readonly IConfiguration _cfg;

        /// <summary>
        /// Banco de dados em memória que resolve o banco baseado no ID do usuário.
        /// </summary>
        /// <param name="cfg"></param>
        public BancoResolverInMemory(IConfiguration cfg) => _cfg = cfg;

        /// <summary>
        /// Resolve o banco de dados do usuário baseado no ID fornecido.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string? Resolve(string userId)
        {
            var section = _cfg.GetSection("BancoMap");
            // Tenta encontrar a key exata; você pode normalizar (ToLower) se quiser
            return section[userId];
        }
    }

}
