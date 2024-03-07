using Microsoft.AspNetCore.Mvc;
using mvc_api.Database;
using mvc_api.Models;
using mvc_api.Views.Login;

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
    public IActionResult Auth(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            if (_context.Clientes.Any(c => c.Telefone == model.Telefone))
            {
                var cliente = _context.Clientes
                    .FirstOrDefault(c => c.Telefone == model.Telefone);
                return RedirectToAction("Login", "Login", cliente);
            }
            else
            {
                return RedirectToAction("Register", "Login", model);
            } 
        }

        return View(model);
    }

    public IActionResult Login(Cliente cliente)
    {
        return View();
    }

    public IActionResult Register(LoginViewModel model)
    {
        return View();
    }
    
}