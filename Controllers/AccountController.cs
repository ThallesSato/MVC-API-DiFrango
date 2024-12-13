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
            .First(c =>
                c.Telefone == User.FindFirstValue(ClaimTypes.MobilePhone));
        // Retorna a view com o cliente
        return View(ClienteViewModel.FromCliente(cliente));
    }

    [HttpGet]
    public IActionResult Endereco(int? enderecoId)
    {
        // Se endereço existe ...
        if (enderecoId != null && enderecoId > 0)
        {
            // Busca o endereço pelo Id no BD
            var existingEndereco = _context.Enderecos.FirstOrDefault(e => e.Id == enderecoId);
            
            // Se não for nulo...
            if (existingEndereco != null)
            {
                // Retorna com o endereço e valor true no "exists"
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
        
        // Busca cliente no BD pelo telefone no cookie
        var cliente = _context.Clientes
            .First(c =>
                c.Telefone == User.FindFirstValue(ClaimTypes.MobilePhone));

        // Recebe todos os valores de endereço e seta
        Endereco endereco = new Endereco
        {
            Cep = model.Cep,
            Rua = model.Rua,
            Numero = model.Numero,
            Complemento = model.Complemento,
            Cidade = model.Cidade,
            Bairro = model.Bairro
        };
        
        // Se o endereço id existe ...
        if (enderecoId != null && (int)enderecoId > 0)
        {
            // Busca o endereço existente
            var existingEndereco = _context.Enderecos
                .FirstOrDefault(e => e.Id == (int)enderecoId);
            
            // Se não for nulo
            if (existingEndereco != null)
            {
                // Atribui os valores
                existingEndereco.Cep = model.Cep;
                existingEndereco.Rua = model.Rua;
                existingEndereco.Numero = model.Numero;
                existingEndereco.Complemento = model.Complemento;
                existingEndereco.Cidade = model.Cidade;
                existingEndereco.Bairro = model.Bairro;
                
                // Atualiza e salva
                _context.Enderecos.Update(existingEndereco);
                _context.SaveChanges();
                
                // Retorna com mensagem sucesso
                TempData["Success"] = "Endereço salvo!";
                return View(model);
            }
        }
        
        // Cria endereço, atualiza cliente e salva
        cliente.Enderecos.Add(endereco);
        _context.Clientes.Update(cliente);
        _context.SaveChanges();
        
        // Retorna com mensagem sucesso
        TempData["Success"] = "Endereço salvo!";
        return View(model);
    }
    
    [HttpGet]
    public IActionResult EditarConta()
    {
        // Busca cliente no BD pelo telefone no cookie
        var cliente = _context.Clientes
            .First(c =>
                c.Telefone == User.FindFirstValue(ClaimTypes.MobilePhone));
        // Retorna a view com o cliente
        return View(EditClienteViewModel.FromCliente(cliente));
    }
    
    [HttpPost]
    public IActionResult EditarConta(EditClienteViewModel model)
    {
        // Se a validação não passar, retorna a pagina com os erros
        if (!ModelState.IsValid) return View(model);
        
        // Busca cliente pelo cookie
        var cliente = _context.Clientes
            .First(c =>
                c.Telefone == User.FindFirstValue(ClaimTypes.MobilePhone));

        // Atualiza o nome no cliente
        cliente.FullName = model.FullName;
        
        // Atualiza e Salva o BD
        _context.Clientes.Update(cliente);
        _context.SaveChanges();
        
        // Retorna com sucesso
        TempData["Success"] = "Nome atualizado!";
        return View(model);
    }
}