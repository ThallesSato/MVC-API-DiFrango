using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mvc_api.Database;
using mvc_api.ViewModel.Account;

namespace mvc_api.Controllers;

[Authorize]
public class AccountController : Controller
{
    private readonly AppDbContext _context;
    public AccountController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public IActionResult MinhaConta()
    {
        // Busca cliente no BD pelo telefone no cookie
        var cliente = _context.Clientes
            .FirstOrDefault(c =>
                c.Telefone == User.FindFirst(ClaimTypes.MobilePhone).Value);
                
        // Retorna a view com o cliente
        return View(ClienteViewModel.FromCliente(cliente));
    }
}