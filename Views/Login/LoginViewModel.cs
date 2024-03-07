using System.ComponentModel.DataAnnotations;

namespace mvc_api.Views.Login;

public class LoginViewModel
{
    [Required]
    [Display(Name = "Telefone")]
    [Length(11,11,ErrorMessage = "Número de telefone inválido")]
    public string Telefone { get; set; }
}