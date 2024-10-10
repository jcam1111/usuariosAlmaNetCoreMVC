using UsuariosAlmaNetCoreMVC.Models;

namespace UsuariosAlmaNetCoreMVC.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(Usuario usuario);
    }
}
