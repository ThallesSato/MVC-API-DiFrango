using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            .Include(c => c.Enderecos)
            .FirstOrDefault(c =>
                c.Telefone == User.FindFirst(ClaimTypes.MobilePhone).Value);
        // Retorna a view com o cliente
        return View(ClienteViewModel.FromCliente(cliente));
    }

    [HttpGet]
    public IActionResult Endereco(int? enderecoId)
    {
        if (enderecoId != null && enderecoId > 0)
        {
            var existingEndereco = _context.Enderecos.FirstOrDefault(e => e.Id == enderecoId);
            if (existingEndereco != null)
            {
                TempData["Exists"] = true;
                var enderecoViewModel = EnderecoViewModel.FromEndereco(existingEndereco);
                return View(enderecoViewModel);
            }
        }

        return View(new EnderecoViewModel());
    }

    [HttpPost]
    public IActionResult Endereco(int? enderecoId, EnderecoViewModel model)
    {
        if (model == null) return View();
                
        
        // Se a validação não passar, retorna a pagina com os erros
        if (!ModelState.IsValid) return View(model);
        
        var cliente = _context.Clientes
            .FirstOrDefault(c =>
                c.Telefone == User.FindFirst(ClaimTypes.MobilePhone).Value);

        Endereco endereco = new Endereco
        {
            Cep = model.Cep,
            Rua = model.Rua,
            Numero = model.Numero,
            Complemento = model.Complemento,
            Cidade = model.Cidade,
            Bairro = model.Bairro
        };
        
        if (enderecoId != null && (int)enderecoId > 0)
        {
            var existingEndereco = _context.Enderecos
                .FirstOrDefault(e => e.Id == (int)enderecoId);
            if (existingEndereco != null)
            {
                existingEndereco.Cep = model.Cep;
                existingEndereco.Rua = model.Rua;
                existingEndereco.Numero = model.Numero;
                existingEndereco.Complemento = model.Complemento;
                existingEndereco.Cidade = model.Cidade;
                existingEndereco.Bairro = model.Bairro;
                _context.Enderecos.Update(existingEndereco);
                _context.SaveChanges();
                
                TempData["Success"] = "Endereço salvo!";
                return View(model);
            }
        }
        
        
        cliente.Enderecos.Add(endereco);
        _context.Clientes.Update(cliente);
        _context.SaveChanges();
        
        TempData["Success"] = "Endereço salvo!";
        return View(model);
    }
    
    [HttpGet]
    public IActionResult EditarConta()
    {
        // Busca cliente no BD pelo telefone no cookie
        var cliente = _context.Clientes
            .FirstOrDefault(c =>
                c.Telefone == User.FindFirst(ClaimTypes.MobilePhone).Value);
        // Retorna a view com o cliente
        return View(EditClienteViewModel.FromCliente(cliente));
    }
}