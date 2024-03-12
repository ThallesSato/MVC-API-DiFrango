using mvc_api.Models;

namespace mvc_api.ViewModel.Account;

public class ClienteViewModel
{
    public string? FullName { get; set; }
    public string? Telefone { get; set; }
    public List<Endereco> Enderecos { get; set; } = new List<Endereco>();
    
    public static ClienteViewModel FromCliente(Cliente cliente)
    {
        return new ClienteViewModel
        {
            FullName = cliente.FullName,
            Telefone = cliente.Telefone,
            Enderecos = cliente.Enderecos
        };
    }
}
