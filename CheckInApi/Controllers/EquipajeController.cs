using CheckInApi.Modelos;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class EquipajeController : ControllerBase
{
    private readonly CheckInService servicio;

    public EquipajeController(CheckInService servicio)
    {
        this.servicio = servicio;
    }

    [HttpGet("{idPasajero:int}")]
    public ActionResult<List<Equipaje>> ObtenerEquipajes(int idPasajero)
    {
        var equipajes = servicio.ObtenerEquipajes(idPasajero);
        if (equipajes == null)
        {
            return NotFound($"No se encontró el pasajero con ID: {idPasajero}");
        }
        return Ok(equipajes);
    }

    [HttpPost("{idPasajero:int}")]
    public ActionResult <List <Equipaje>> AgregarEquipaje(int idPasajero, [FromBody] EquipajeRequest datos)
    {
        var (exito, mensaje) = servicio.AgregarEquipaje(idPasajero, datos);
        if (!exito)
        {
            return BadRequest(mensaje);
        }
        return Ok(mensaje);
    }

    [HttpDelete("{idPasajero:int}/{idEquipaje}")]
    public ActionResult EliminarEquipaje(int idPasajero, string idEquipaje)
    {
        var (exito, mensaje) = servicio.EliminarEquipaje(idPasajero, idEquipaje);
        if (!exito)
        {
            return BadRequest(mensaje);
        }
        return Ok(mensaje);
    }
}