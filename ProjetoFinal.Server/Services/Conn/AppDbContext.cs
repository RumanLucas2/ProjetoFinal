namespace ProjetoFinal.Server.Services.Conn
{
    using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    private readonly ITenantConnectionAccessor _tenant;

    public AppDbContext(DbContextOptions<AppDbContext> options,
                        ITenantConnectionAccessor tenant) : base(options)
    {
        _tenant = tenant;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            if (string.IsNullOrWhiteSpace(_tenant.ConnectionString))
                throw new InvalidOperationException("Connection do tenant não resolvida.");

            optionsBuilder.UseSqlServer(_tenant.ConnectionString);
        }
    }

    public DbSet<Produto> Produtos => Set<Produto>();
}

public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; } = "";
}

}
