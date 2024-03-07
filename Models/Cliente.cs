using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace mvc_api.Models;

public class Cliente
{
    [Key]
    public int Id { get; set; }
    public string? FullName { get; set; }
    public string? Telefone { get; set; }
    public List<Endereco> Enderecos { get; set; } = new List<Endereco>();
    [JsonIgnore]
    public string? PasswordHash { get; set; }
}