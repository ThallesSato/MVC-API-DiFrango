using System.ComponentModel.DataAnnotations;
using mvc_api.Models;

namespace mvc_api.ViewModel.Account;

public class EnderecoViewModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Rua é obrigatório")]
    public string Rua { get; set; }
    public string? Numero { get; set; }
    public string? Complemento { get; set; }
    [Required(ErrorMessage = "Cep é obrigatório")]
    public string? Cep { get; set; }
    
    public static EnderecoViewModel FromEndereco(Endereco endereco)
    {
        return new EnderecoViewModel
        {
            Id = endereco.Id,
            Rua = endereco.Rua,
            Cep = endereco.Cep,
            Complemento = endereco.Complemento,
            Numero = endereco.Numero
        };
    }
}