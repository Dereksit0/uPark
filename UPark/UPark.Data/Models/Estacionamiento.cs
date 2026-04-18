using System.ComponentModel.DataAnnotations;

namespace UPark.Data.Models;

public class Estacionamiento
{
    [Key]
    public int IdEstacionamiento { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Ubicacion { get; set; } = string.Empty;
    public int CapacidadTotal { get; set; }

    // Navegación
    public ICollection<Espacio> Espacios { get; set; } = new List<Espacio>();
}