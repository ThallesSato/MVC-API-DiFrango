using System.ComponentModel.DataAnnotations;
using mvc_api.Models;

namespace mvc_api.ViewModel.Account;

public class EnderecoViewModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Rua é obrigatório")]
    public string Rua { get; set; } = String.Empty;
    public string? Numero { get; set; }
    public string? Complemento { get; set; }
    public string Bairro { get; set; } = String.Empty;
    public string Cidade { get; set; } = String.Empty;
    [Required(ErrorMessage = "Cep é obrigatório")]

    public string Cep { get; set; } = String.Empty;
    
    public static EnderecoViewModel FromEndereco(Endereco endereco)
    {
        return new EnderecoViewModel
        {
            Id = endereco.Id,
            Rua = endereco.Rua,
            Cep = endereco.Cep,
            Complemento = endereco.Complemento,
            Numero = endereco.Numero,
            Cidade = endereco.Cidade,
            Bairro = endereco.Bairro
        };
    }
}