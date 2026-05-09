using Microsoft.EntityFrameworkCore;
using UPark.Data.Context;
using UPark.Data.Models;

namespace UPark.Services;

public class UsuarioService : IUsuarioService
{
    private readonly UParkDbContext _context;

    public UsuarioService(UParkDbContext context) => _context = context;

    public async Task<Usuario?> LoginAsync(string matricula, string contrasena)
        => await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Matricula == matricula && u.Contrasena == contrasena);

    public async Task<bool> ExisteMatriculaAsync(string matricula)
        => await _context.Usuarios.AnyAsync(u => u.Matricula == matricula);

    public async Task<(bool Success, string Error)> RegistrarAsync(
        string nombre, string matricula, string correo, string contrasena)
    {
        if (await _context.Usuarios.AnyAsync(u => u.Matricula == matricula))
            return (false, "Ya existe un usuario registrado con esa matrícula.");

        if (await _context.Usuarios.AnyAsync(u => u.CorreoInstitucional == correo))
            return (false, "Ya existe un usuario registrado con ese correo.");

        _context.Usuarios.Add(new Usuario
        {
            Matricula = matricula,
            Nombre = nombre,
            CorreoInstitucional = correo,
            Contrasena = contrasena,
            Rol = "Estudiante"
        });
        await _context.SaveChangesAsync();
        return (true, string.Empty);
    }

    public async Task<bool> CambiarContrasenaAsync(string matricula, string contrasenaActual, string nuevaContrasena)
    {
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Matricula == matricula && u.Contrasena == contrasenaActual);
        if (usuario == null) return false;
        usuario.Contrasena = nuevaContrasena;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> CorreoExisteAsync(string correo)
        => await _context.Usuarios.AnyAsync(u => u.CorreoInstitucional == correo);

    public async Task<bool> RestablecerContrasenaAsync(string correo, string nuevaContrasena)
    {
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.CorreoInstitucional == correo);
        if (usuario == null) return false;
        usuario.Contrasena = nuevaContrasena;
        await _context.SaveChangesAsync();
        return true;
    }
}
