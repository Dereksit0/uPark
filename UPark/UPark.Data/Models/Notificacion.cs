using System.ComponentModel.DataAnnotations;

namespace UPark.Data.Models;

public class Notificacion
{
    [Key]
    public int IdNotificacion { get; set; }
    public string Mensaje { get; set; } = string.Empty;
    public DateTime FechaHora { get; set; } = DateTime.Now;
    public string Matricula { get; set; } = string.Empty;

    // Navegación
    public Usuario Usuario { get; set; } = null!;
}