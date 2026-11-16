using CheckInApi.Modelos;
using CheckInApi.Modelos.Exceptions;
//aca tenemos la operacion
public class CheckInService
{
    private readonly IPasajeroRepository repositorio;

    public CheckInService(IPasajeroRepository repositorio)
    {
        this.repositorio = repositorio;
    }
// Equipaje
    public List<Equipaje>? ObtenerEquipajes(int idPasajero)
    {
        Pasajero? pasajero = repositorio.ObtenerPorId(idPasajero);
        if (pasajero == null)
        {
            return null;
        }
        return pasajero.Equipajes;
    }

    public  (bool exito, string mensaje) AgregarEquipaje(int idPasajero, EquipajeRequest datos)
    {
        Pasajero? pasajero = repositorio.ObtenerPorId(idPasajero);
        if (pasajero == null)
        {
            return (false, "Error al agregar el equipaje a un pasajero inexistente.");
        }
        Equipaje nuevo;
        try
        {
            nuevo = new Equipaje(
                datos.pesoKg ?? 0,
                datos.id ?? string.Empty
            );
        }
        catch (DatosInvalidosException)
        {
            // Manejar la excepción según sea necesario, por ejemplo, registrarla o devolver un mensaje de error.
            return (false, "Error al agregar el equipaje al pasajero.");
        }

        pasajero.Equipajes.Add(nuevo);
        bool exito = repositorio.Actualizar(pasajero);
        if (!exito)
        {
            return (false, "Error al actualizar el pasajero con el nuevo equipaje.");
        }
        else
        {
            return (true, "Equipaje agregado correctamente al pasajero.");
        }
    }

    public (bool exito, string mensaje) EliminarEquipaje(int idPasajero, string idEquipaje)
    {
        Pasajero? pasajero = repositorio.ObtenerPorId(idPasajero);
        if (pasajero == null)
        {
            return (false, "Error al eliminar el equipaje de un pasajero inexistente.");
        }

        var equipajeAEliminar = pasajero.Equipajes.Find(e => e._id == idEquipaje);
        if (equipajeAEliminar == null)
        {
            return (false, "Error al eliminar el equipaje: no se encontró el equipaje con el ID proporcionado.");
        }

        pasajero.Equipajes.Remove(equipajeAEliminar);
        bool exito = repositorio.Actualizar(pasajero);
        if (!exito)
        {
            return (false, "Error al actualizar el pasajero después de eliminar el equipaje.");
        }
        else
        {
            return (true, "Equipaje eliminado correctamente del pasajero.");
        }
    }
    // Pasaje

    public Pasaje? ObtenerPasaje(int idPasajero)
    {
        Pasajero? pasajero = repositorio.ObtenerPorId(idPasajero);
        if (pasajero == null)
        {
            return null;
        }
        return pasajero.Pasaje;
    }
    
    public (bool exito, string mensaje) AgregarPasaje(int idPasajero, PasajeRequest datos)
    {
        Pasajero? pasajero = repositorio.ObtenerPorId(idPasajero);
        if (pasajero == null)
        {
            return (false, "Error al agregar el pasaje a un pasajero inexistente.");
        }
        Pasaje nuevo;
        try
        {
            nuevo = new Pasaje(
                datos.TipoVuelo ?? string.Empty,
                datos.TipoTarifa ?? string.Empty,
                datos.NroVuelo ?? string.Empty
            );
        }
        catch (DatosInvalidosException)
        {
            // Manejar la excepción según sea necesario, por ejemplo, registrarla o devolver un mensaje de error.
            return (false, "Error al agregar el pasaje al pasajero.");
        }

        pasajero.Pasaje = nuevo;
        bool exito = repositorio.Actualizar(pasajero);
        if (!exito)
        {
            return (false, "Error al actualizar el pasajero con el nuevo pasaje.");
        }
        else
        {
            return (true, "Pasaje agregado correctamente al pasajero.");
        }
    }


}