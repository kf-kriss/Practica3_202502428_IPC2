using System.Collections;

namespace Practica3_V2._1_IPC2.Models;

public sealed class ListaMascotas : IEnumerable<Mascota>
{
    private sealed class Nodo(Mascota mascota)
    {
        public Mascota Mascota { get; } = mascota;
        public Nodo? Siguiente { get; set; }
    }

    private Nodo? cabeza;
    private Nodo? cola;

    public int Cantidad { get; private set; }

    public void Agregar(Mascota mascota)
    {
        ArgumentNullException.ThrowIfNull(mascota);
        var nuevo = new Nodo(mascota);

        if (cabeza is null)
        {
            cabeza = nuevo;
        }
        else
        {
            cola!.Siguiente = nuevo;
        }

        cola = nuevo;
        Cantidad++;
    }

    public Mascota? BuscarPorCodigo(string codigo)
    {
        for (Nodo? actual = cabeza; actual is not null; actual = actual.Siguiente)
        {
            if (string.Equals(actual.Mascota.Codigo, codigo, StringComparison.OrdinalIgnoreCase))
            {
                return actual.Mascota;
            }
        }

        return null;
    }

    public bool EliminarPorCodigo(string codigo)
    {
        Nodo? anterior = null;
        Nodo? actual = cabeza;

        while (actual is not null)
        {
            if (string.Equals(actual.Mascota.Codigo, codigo, StringComparison.OrdinalIgnoreCase))
            {
                if (anterior is null)
                {
                    cabeza = actual.Siguiente;
                }
                else
                {
                    anterior.Siguiente = actual.Siguiente;
                }

                if (cola == actual)
                {
                    cola = anterior;
                }

                Cantidad--;
                return true;
            }

            anterior = actual;
            actual = actual.Siguiente;
        }

        return false;
    }

    public Mascota[] ObtenerTodas()
    {
        var mascotas = new Mascota[Cantidad];
        int indice = 0;
        foreach (Mascota mascota in this)
        {
            mascotas[indice++] = mascota;
        }

        return mascotas;
    }

    public IEnumerator<Mascota> GetEnumerator()
    {
        for (Nodo? actual = cabeza; actual is not null; actual = actual.Siguiente)
        {
            yield return actual.Mascota;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
