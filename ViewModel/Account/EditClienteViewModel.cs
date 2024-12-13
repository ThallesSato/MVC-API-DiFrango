using mvc_api.Models;

namespace mvc_api.ViewModel.Account;

public class EditClienteViewModel
{
    public string FullName { get; set; } = String.Empty;
    public static EditClienteViewModel FromCliente(Cliente cliente)
    {
        return new EditClienteViewModel
        {
            FullName = cliente.FullName
        };
    }
}