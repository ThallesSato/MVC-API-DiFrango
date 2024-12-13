using System.ComponentModel.DataAnnotations;

namespace mvc_api.ViewModel.Login;

public class RegisterViewModel
{
    [Required(ErrorMessage = "O Nome completo é obrigatório")]
    [RegularExpression(@"^[A-Za-zÀ-ÖØ-öø-ÿ\s]+ [A-Za-zÀ-ÖØ-öø-ÿ\s]+$", ErrorMessage = "O nome completo deve conter um nome e um sobrenome.")]
    public string FullName { get; set; } = String.Empty;
    public string Telefone { get; set; } = String.Empty;
    [Required(ErrorMessage = "A senha é obrigatória")]
    [MinLength(6,ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
    public string Password { get; set; } = String.Empty;
    [Required(ErrorMessage = "Por favor confirme a senha")]
    [Compare(nameof(Password),ErrorMessage = "As senhas não conferem")]
    public string ConfirmPassword { get; set; } = String.Empty;
}