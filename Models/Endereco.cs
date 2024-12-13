using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvc_api.Models;

public class Endereco
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Rua { get; set; } = String.Empty;
    public string? Numero { get; set; }
    public string? Complemento { get; set; }
    public string Bairro { get; set; } = String.Empty;
    public string Cidade { get; set; } = String.Empty;
    public string Cep { get; set; } = String.Empty;
    
}