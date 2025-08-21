namespace ProjetoFinal.Server.Services.Conn
{
    /// <summary>
    /// Contrato para acessar a string de conexão do inquilino (tenant).
    /// </summary>
    public interface ITenantConnectionAccessor
    {
        /// <summary>
        /// connectionString do inquilino (tenant).
        /// </summary>
        string? ConnectionString { get; set; }
    }

    /// <summary>
    /// Acessor para a string de conexão do inquilino (tenant).
    /// </summary>
    public class TenantConnectionAccessor : ITenantConnectionAccessor
    {
        /// <summary>
        /// string de conexão do inquilino (tenant).
        /// </summary>
        public string? ConnectionString { get; set; }
    }


}
