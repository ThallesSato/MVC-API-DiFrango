using System.ComponentModel.DataAnnotations;

namespace mvc_api.Models;

public class Endereco
{
    [Key]
    public int Id { get; set; }
    public string? Rua { get; set; }
    public string? Numero { get; set; }
    public string? Complemento { get; set; }
    public string? Bairro { get; set; }
    public string? Cidade { get; set; }
    public string? Cep { get; set; }
    
}