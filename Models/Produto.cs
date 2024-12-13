using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvc_api.Models;

public class Produto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Nome  { get; set; } = String.Empty;
    public string?  Desc  { get; set; }
    public float Price  { get; set; }
    public Categoria Categoria { get; set; } = new();
}