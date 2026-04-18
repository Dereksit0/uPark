using System.ComponentModel.DataAnnotations;

namespace UPark.Data.Models;

public class Usuario
{
    [Key]
    public string Matricula { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string CorreoInstitucional { get; set; } = string.Empty;
    public string Contrasena { get; set; } = string.Empty;
    public string Rol { get; set; } = "Estudiante";

    // Navegación
    public ICollection<Notificacion> Notificaciones { get; set; } = new List<Notificacion>();
}