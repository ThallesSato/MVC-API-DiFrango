using Microsoft.AspNetCore.Mvc;
using mvc_api.Database;
using mvc_api.Models;
using mvc_api.ViewModel.Login;

namespace mvc_api.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly AppDbContext _context;

    public LoginController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    [HttpGet]
    public IActionResult Auth()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Auth(AuthViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        
        var cliente = _context.Clientes
            .FirstOrDefault(c => c.Telefone == model.Telefone);
        
        if (cliente == null) return RedirectToAction("Register", "Login",model);

        TempData["Telefone"] = cliente.Telefone;
        TempData["Nome"] = cliente.FullName.Split(" ")[0];
        return RedirectToAction("Login", "Login");
    }
    
    [HttpGet]
    public IActionResult Login()
    { 
        LoginViewModel login = new LoginViewModel();
        
        login.Telefone = TempData["Telefone"].ToString();
        login.Nome = TempData["Nome"].ToString();
    
        return View(login);
    }
    
    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    { 
        if (!ModelState.IsValid) return View(model);
        
        var cliente = _context.Clientes
            .FirstOrDefault(c => c.Telefone == model.Telefone);

        if (!BCrypt.Net.BCrypt.Verify(model.Password, cliente.PasswordHash))
        {
            ModelState.AddModelError(string.Empty, "Senha inválida");
            return View(model);
        }
        TempData["Success"] = "Logado com sucesso!";
        return View(model);
    }

    [HttpGet]
    public IActionResult Register(AuthViewModel model)
    {
        RegisterViewModel register = new RegisterViewModel();
        register.Telefone = model.Telefone;
        return View(register);
    }

    [HttpPost]
    public IActionResult Register(RegisterViewModel model)
    { 
        if (!ModelState.IsValid) return View(model);
        
        try
        {
            var cliente = new Cliente
            {
                FullName = model.FullName,
                Telefone = model.Telefone,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password)
            };

            _context.Clientes.Add(cliente);
            _context.SaveChanges();
            
            TempData["Success"] = "Registro concluído com sucesso!";
            return View(model);
        } 
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }
    }
}