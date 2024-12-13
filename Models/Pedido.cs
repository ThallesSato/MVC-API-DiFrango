using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace mvc_api.Models;

public class Pedido
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public Cliente Cliente { get; set; } = new();
    public DateTime DataHoraReserva { get; set; }
    public DateTime DataHoraCriado { get; set; }
    public List<ProdutoPedido> Produtos { get; set; } = new();
    public string Estado { get; set; } = String.Empty;
    public bool Pago { get; set; }
    [JsonIgnore]
    public bool Deletado { get; set; }
}