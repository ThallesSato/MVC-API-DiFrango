using System.ComponentModel.DataAnnotations;

namespace mvc_api.ViewModel.Login;

public class AuthViewModel
{
    [Required(ErrorMessage = "Insira seu telefone")]
    [Display(Name = "Telefone")]
    [Length(11,11,ErrorMessage = "Número de telefone inválido")]
    public string Telefone { get; set; } = String.Empty;
}