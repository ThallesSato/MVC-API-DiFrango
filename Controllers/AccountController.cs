using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mvc_api.Database;
using mvc_api.Models;
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

    [HttpPost]
    public IActionResult Endereco(int? enderecoId, EnderecoViewModel model)
    {
        if (enderecoId != null && enderecoId > 0)
        {
            var ExistingEndereco = _context.Enderecos.FirstOrDefault(e => e.Id == enderecoId);
            TempData["Exists"] = true;
            return View(EnderecoViewModel.FromEndereco(ExistingEndereco));
        }

        if (model ==null) return View();
                
        
        // Se a validação não passar, retorna a pagina com os erros
        if (!ModelState.IsValid) return View(model);
        
        var cliente = _context.Clientes
            .FirstOrDefault(c =>
                c.Telefone == User.FindFirst(ClaimTypes.MobilePhone).Value);

        Models.Endereco endereco = new Endereco
        {
            Cep = model.Cep,
            Rua = model.Rua,
            Numero = model.Numero,
            Complemento = model.Complemento
        };
        
        cliente.Enderecos.Add(endereco);
        _context.Clientes.Update(cliente);
        _context.Enderecos.Add(endereco);
        _context.SaveChanges();
        
        Console.WriteLine(cliente.Enderecos.FindLast(c=> c.Rua != "a").Rua);
        Console.WriteLine(cliente.Enderecos.Count);
        TempData["Success"] = "Endereço salvo!";
        return View(model);
    }
}