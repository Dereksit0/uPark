using System.ComponentModel.DataAnnotations;

namespace UPark.Data.Models;

public class Reserva
{
    [Key]
    public int IdReserva { get; set; }
    public string Matricula { get; set; } = string.Empty;
    public int IdEspacio { get; set; }
    public DateTime FechaHora { get; set; } = DateTime.Now;
    public string Estado { get; set; } = "Activa"; // Activa, Cancelada, Completada

    public Usuario Usuario { get; set; } = null!;
    public Espacio Espacio { get; set; } = null!;
}
