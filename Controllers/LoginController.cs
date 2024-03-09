using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        // Se o usuário estiver logado, redireciona para a página inicial
        if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
        return View();
    }
    
    [HttpPost]
    public IActionResult Auth(AuthViewModel model)
    {
        // Se a validação não passar, retorna a pagina com os erros
        if (!ModelState.IsValid) return View(model);
        
        // Busca cliente no BD pelo telefone
        var cliente = _context.Clientes
            .FirstOrDefault(c => c.Telefone == model.Telefone);
        
        // Atribui o telefone do cliente no TempData para ser usado mais tarde
        TempData["Telefone"] = model.Telefone;
        
        // Se cliente inexistente, redireciona para tela de cadastro
        if (cliente == null) return RedirectToAction("Register", "Login");

        // Atribui o nome do cliente no TempData para ser usado no login
        TempData["Nome"] = cliente.FullName.Split(" ")[0];
        
        // Retorna para a tela de login
        return RedirectToAction("Login", "Login");
    }

    [HttpGet]
    public IActionResult Register()
    {
        // Se autenticado, redireciona para a página inicial
        if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
        
        // Se o telefone não estiver no TempData, redireciona para tela de autenticação (telefone)
        if (!TempData.ContainsKey("Telefone")) return RedirectToAction("Auth", "Login");
        
        // Construtor da entidade RegisterViewModel
        RegisterViewModel register = new RegisterViewModel();
        
        // Atribui o Telefone na model
        register.Telefone = TempData["Telefone"].ToString();
        return View(register);
    }

    [HttpPost]
    public IActionResult Register(RegisterViewModel model)
    { 
        // Se a validação não passar, retorna a pagina com os erros
        if (!ModelState.IsValid) return View(model);
        
        try
        {
            // Construtor da entidade Cliente com os dados
            var cliente = new Cliente
            {
                FullName = model.FullName,
                Telefone = model.Telefone,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password) // Senha encriptada
            };

            // Adiciona e salva o Cliente no BD
            _context.Clientes.Add(cliente);
            _context.SaveChanges();

            // Atribui mensagem de sucesso no TempData e retorna
            TempData["Success"] = "Registro concluído com sucesso!";
            return View(model);
        } 
        catch (Exception ex) 
        {
            // Caso haja erro, adiciona o erro no ModelState e retorna
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }
    }
    
    [HttpGet]
    public IActionResult Login()
    {
        // Se não possuir telefone no TempData, redireciona para tela de autenticação (telefone)
        if (!TempData.ContainsKey("Telefone")) return RedirectToAction("Auth", "Login");
        
        // Construtor da entidade LoginViewModel
        LoginViewModel login = new LoginViewModel();
        
        // Resgata os dados do TempData e insere na model
        login.Telefone = TempData["Telefone"].ToString();
        login.Nome = TempData["Nome"].ToString();
        
        return View(login);
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    { 
        // Se a validação não passar, retorna a pagina com os erros
        if (!ModelState.IsValid) return View(model);
        
        try
        {
            // Busca cliente no BD pelo telefone
            var cliente = _context.Clientes
                .FirstOrDefault(c => c.Telefone == model.Telefone);

            // Se senha invalida ...
            if (!BCrypt.Net.BCrypt.Verify(model.Password, cliente.PasswordHash)) // Compara a senha informada com a senha encriptada
            {
                // ... Atribui o erro e retorna
                ModelState.AddModelError(string.Empty, "Senha inválida");
                return View(model);
            }
            
            //
            // Início da parte de cookie
            //
            
            // Cria uma lista de Claims
            var claims = new List<Claim>
            {
                // Adiciona um claim para o cookie com o nome do Cliente
                new Claim(ClaimTypes.Name, cliente.FullName.Split(" ")[0])
            };

            // Cria uma identidade de claims com as informações do usuário
            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Construtor de propriedades do cookie
            var authProperties = new AuthenticationProperties();
            
            // Se "Manter conectado" estiver marcado, adiciona propiedade de cookie persistente
            if (model.RememberMe) authProperties.IsPersistent = true;
            
            // Autentica o cookie
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(claimsIdentity), 
                authProperties);
            
            //
            // Fim da parte de cookie
            //
            
            // Atribui mensagem de sucesso no TempData e retorna
            TempData["Success"] = "Logado com sucesso!";
            return View(model);
        }
        catch (Exception ex)
        {
            // Caso haja erro, adiciona o erro no ModelState e retorna
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Login");
    }
}