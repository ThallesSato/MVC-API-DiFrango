using Microsoft.EntityFrameworkCore;
using mvc_api.Models;

namespace mvc_api.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<ProdutoPedido> ProdutosPedidos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(e =>
        {
            e.HasIndex(pp => pp.Telefone).IsUnique();
        });
        
        modelBuilder.Entity<ProdutoPedido>(e =>
        {
            e.HasKey(pp => new { pp.ProdutoId, pp.PedidoId });
        });
    }
}