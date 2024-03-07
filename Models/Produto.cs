using System.ComponentModel.DataAnnotations;

namespace mvc_api.Models;

public class Produto
{
    [Key]
    public int Id { get; set; }
    public string Nome  { get; set; }
    public string  Desc  { get; set; }
    public float Price  { get; set; }
    public Categoria Categoria { get; set; }
}