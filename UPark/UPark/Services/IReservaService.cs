using UPark.Data.Models;

namespace UPark.Services;

public interface IReservaService
{
    Task<Espacio?> GetEspacioAsync(int numero, string estacionamientoNombre);
    Task<(bool Success, string Error)> ReservarEspacioAsync(string matricula, int idEspacio);
    Task<Reserva?> GetReservaActivaAsync(string matricula);
    Task<bool> CancelarReservaAsync(int idReserva);
}
