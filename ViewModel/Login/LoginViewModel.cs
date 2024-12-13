using System.ComponentModel.DataAnnotations;

namespace mvc_api.ViewModel.Login;

public class LoginViewModel
{
    [Required(ErrorMessage = "Insira sua senha")]
    [MinLength(6,ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
    public string? Password{ get; set; }
    public string? Telefone { get; set; }
    public string? Nome { get; set; }
    public bool RememberMe { get; set; }
}