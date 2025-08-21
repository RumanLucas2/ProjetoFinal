namespace ProjetoFinal.Server.Services.Conn
{
    using Microsoft.Data.SqlClient;  // <- este é o namespace certo

    using Microsoft.Extensions.Configuration;

    public static class ConnStringHelper
    {
        public static string BuildTenantConnString(IConfiguration cfg, string databaseName)
        {
            var s = cfg.GetSection("Sql");
            var csb = new SqlConnectionStringBuilder
            {
                DataSource = s["Server"],
                InitialCatalog = databaseName,
                IntegratedSecurity = bool.TryParse(s["TrustedConnection"], out var t) && t,
                TrustServerCertificate = bool.TryParse(s["TrustServerCertificate"], out var ts) && ts
            };
            // Se não usar Integrated Security, configure usuário/senha aqui:
            // csb.UserID = s["User"];
            // csb.Password = s["Password"];
            return csb.ToString();
        }
    }

}
