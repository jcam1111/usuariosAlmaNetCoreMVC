// Controllers/AuthController.cs
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using UsuariosAlmaNetCoreMVC.Data;
using UsuariosAlmaNetCoreMVC.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UsuariosAlmaNetCoreMVC.Services;
using UsuariosAlmaNetCoreMVC.Interfaces;

namespace UsuariosAlmaNetCoreMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

        private readonly IEmailService _emailService;
        private readonly IJwtService _jwtService;

        public AuthController(ApplicationDbContext context, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _jwtService = jwtService;
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
            var usuario = await _context.Usuarios.SingleOrDefaultAsync(u => u.Nombre == nombreUsuario);
            
            var result = await _signInManager.PasswordSignInAsync(nombreUsuario, password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                //return RedirectToAction("Index", "Home");
                var token = _jwtService.GenerateToken(usuario);
                return Ok(new { Token = token });
            }

            if (!PasswordHasher.VerifyPasswordHash(password, usuario.PasswordHash, usuario.PasswordSalt))
            {
                ModelState.AddModelError("", "Usuario o contraseña incorrectos");
                return View();
            }

            //if (usuario == null || !PasswordHasher.VerifyPasswordHash(request.Password, usuario.PasswordHash, usuario.PasswordSalt))
            //{
            //    return Unauthorized();
            //}

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
            if (string.IsNullOrEmpty(usuario.Email))
            {
                ModelState.AddModelError("Email", "El correo electrónico es requerido.");
                return View(usuario);
            }

            if (string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "La contraseña no puede estar vacía.");
                return View(usuario);
            }
            // Validar si el email ya existe
            if (await _context.Usuarios.AnyAsync(u => u.Email == usuario.Email))
            {
                ModelState.AddModelError("Email", "El email ya está registrado.");
                return View(usuario);
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                    {
                        // Genera el hash y el salt
                        PasswordHasher.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
                        usuario.PasswordHash = passwordHash;
                        usuario.PasswordSalt = passwordSalt;

                        // Guarda el usuario en la base de datos
                        //_context.Usuarios.Add(usuario);
                        _context.Usuarios.AddAsync(usuario);
                        await _context.SaveChangesAsync();

                        // Enviar correo de confirmación
                        var message = $"<h1>Bienvenido {usuario.Nombre}</h1><p>Tu cuenta ha sido registrada correctamente.</p>";
                        await _emailService.SendEmailAsync(usuario.Email, "Registro exitoso", message);

                    // Confirmar la transacción
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    // Si ocurre un error, revertir la transacción
                    await transaction.RollbackAsync();
                    ModelState.AddModelError("", "Ocurrió un error durante el registro. Por favor, inténtalo de nuevo.");
                    return View(usuario);
                }
            }

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
