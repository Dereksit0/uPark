using Microsoft.EntityFrameworkCore;
using UPark.Data.Context;
using UPark.Data.Models;

namespace UPark.Services;

public class ReservaService : IReservaService
{
    private readonly UParkDbContext _context;

    public ReservaService(UParkDbContext context) => _context = context;

    public async Task<Espacio?> GetEspacioAsync(int numero, string estacionamientoNombre)
        => await _context.Espacios
            .Include(e => e.Estacionamiento)
            .FirstOrDefaultAsync(e => e.Numero == numero && e.Estacionamiento.Nombre == estacionamientoNombre);

    public async Task<(bool Success, string Error)> ReservarEspacioAsync(string matricula, int idEspacio)
    {
        var reservaExistente = await _context.Reservas
            .FirstOrDefaultAsync(r => r.Matricula == matricula && r.Estado == "Activa");

        if (reservaExistente != null)
            return (false, "Ya tienes una reserva activa. Cancélala antes de reservar otro espacio.");

        var espacio = await _context.Espacios.FindAsync(idEspacio);
        if (espacio == null || espacio.Estado != "Libre")
            return (false, "El espacio ya no está disponible. Por favor elige otro.");

        espacio.Estado = "Ocupado";
        _context.Reservas.Add(new Reserva
        {
            Matricula = matricula,
            IdEspacio = idEspacio,
            FechaHora = DateTime.Now,
            Estado = "Activa"
        });
        await _context.SaveChangesAsync();
        return (true, string.Empty);
    }

    public async Task<Reserva?> GetReservaActivaAsync(string matricula)
        => await _context.Reservas
            .Include(r => r.Espacio)
            .ThenInclude(e => e.Estacionamiento)
            .FirstOrDefaultAsync(r => r.Matricula == matricula && r.Estado == "Activa");

    public async Task<bool> CancelarReservaAsync(int idReserva)
    {
        var reserva = await _context.Reservas
            .Include(r => r.Espacio)
            .FirstOrDefaultAsync(r => r.IdReserva == idReserva);

        if (reserva == null) return false;

        reserva.Estado = "Cancelada";
        reserva.Espacio.Estado = "Libre";
        await _context.SaveChangesAsync();
        return true;
    }
}
