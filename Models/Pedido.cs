using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace mvc_api.Models;

public class Pedido
{
    [Key]
    public int Id { get; set; }
    public Cliente Cliente { get; set; }
    public DateTime DataHoraReserva { get; set; }
    public DateTime DataHoraCriado { get; set; }
    public List<ProdutoPedido> Produtos { get; set; }
    public bool Aprovado { get; set; }
    public bool Pago { get; set; }
    [JsonIgnore]
    public bool Deletado { get; set; }
}