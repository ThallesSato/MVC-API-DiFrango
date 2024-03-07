using System.ComponentModel.DataAnnotations;

namespace mvc_api.Models;

public class Categoria
{
    [Key]
    public int Id { get; set; }
    public string? Nome { get; set; }
}