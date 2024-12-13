using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace mvc_api.Models;

public class Cliente
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public List<Endereco> Enderecos { get; set; } = new ();
    [JsonIgnore]
    public string? PasswordHash { get; set; }
}