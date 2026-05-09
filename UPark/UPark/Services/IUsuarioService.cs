using UPark.Data.Models;

namespace UPark.Services;

public interface IUsuarioService
{
    Task<Usuario?> LoginAsync(string matricula, string contrasena);
    Task<bool> ExisteMatriculaAsync(string matricula);
    Task<(bool Success, string Error)> RegistrarAsync(string nombre, string matricula, string correo, string contrasena);
    Task<bool> CambiarContrasenaAsync(string matricula, string contrasenaActual, string nuevaContrasena);
    Task<bool> CorreoExisteAsync(string correo);
    Task<bool> RestablecerContrasenaAsync(string correo, string nuevaContrasena);
}
