using System.Text.Json.Serialization;

namespace mvc_api.Models;

public class ProdutoPedido
{
    [JsonIgnore]
    public int PedidoId { get; set; }
    [JsonIgnore]
    public int ProdutoId { get; set; }
    [JsonIgnore] 
    public Pedido Pedido { get; set; } = new();
    public Produto Produto { get; set; } = new ();
    public float Quantidade{ get; set; }
}