using System.ComponentModel.DataAnnotations;

namespace UPark.Data.Models;

public class Espacio
{
    [Key]
    public int IdEspacio { get; set; }
    public int Numero { get; set; }
    public string Estado { get; set; } = "Libre";
    public int IdEstacionamiento { get; set; }

    // Navegación
    public Estacionamiento Estacionamiento { get; set; } = null!;
}