using Practica3_V2._1_IPC2.Models;

namespace Practica3_V2._1_IPC2.Services;

public sealed class MascotasService
{
    private readonly object sincronizacion = new();
    private readonly ListaMascotas mascotas = new();

    public Mascota[] ObtenerTodas()
    {
        lock (sincronizacion)
        {
            return mascotas.ObtenerTodas();
        }
    }

    public Mascota? Buscar(string codigo)
    {
        lock (sincronizacion)
        {
            return mascotas.BuscarPorCodigo(codigo);
        }
    }

    public void Registrar(Mascota mascota)
    {
        lock (sincronizacion)
        {
            mascotas.Agregar(mascota);
        }
    }

    public bool Actualizar(string codigo, Action<Mascota> aplicarCambios)
    {
        lock (sincronizacion)
        {
            Mascota? mascota = mascotas.BuscarPorCodigo(codigo);
            if (mascota is null)
            {
                return false;
            }

            aplicarCambios(mascota);
            return true;
        }
    }

    public Mascota? CambiarEstado(string codigo)
    {
        lock (sincronizacion)
        {
            Mascota? mascota = mascotas.BuscarPorCodigo(codigo);
            if (mascota is null)
            {
                return null;
            }

            mascota.CambiarEstado();
            return mascota;
        }
    }

    public bool Eliminar(string codigo)
    {
        lock (sincronizacion)
        {
            return mascotas.EliminarPorCodigo(codigo);
        }
    }
}
