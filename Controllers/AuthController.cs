// Controllers/AuthController.cs
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using UsuariosAlmaNetCoreMVC.Data;
using UsuariosAlmaNetCoreMVC.Models;
using System.Threading.Tasks;

namespace UsuariosAlmaNetCoreMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
              
        public AuthController(ApplicationDbContext context, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        
        //public IActionResult Login(string nombreUsuario, string password)
        [HttpPost]        
        public async Task<IActionResult> Login(string nombreUsuario, string password)
        {
            //var usuario = _context.Usuarios.FirstOrDefault(u => u.Nombre == nombreUsuario && u.Password == password);
            //if (usuario != null)
            //{
            //    // Autenticación exitosa
            //    return RedirectToAction("Index", "Home");
            //}
            var result = await _signInManager.PasswordSignInAsync(nombreUsuario, password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Usuario o contraseña incorrectos");
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(Usuario usuario, string password)
        {
            var result = await _userManager.CreateAsync(usuario, password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(usuario, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(usuario);
        }
    }

}
