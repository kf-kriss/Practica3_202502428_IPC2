using System.Globalization;

namespace Practica3_V2._1_IPC2.Models;

public abstract class Mascota
{
    private static int contador;

    protected Mascota(string nombre, double peso, string sexo, int edad, string propietario)
    {
        Nombre = nombre;
        Peso = peso;
        Sexo = sexo;
        Edad = edad;
        Propietario = propietario;
        Codigo = $"No-{Interlocked.Increment(ref contador).ToString("D5", CultureInfo.InvariantCulture)}";
    }

    public string Nombre { get; set; }
    public double Peso { get; set; }
    public string Sexo { get; set; }
    public int Edad { get; set; }
    public string Propietario { get; set; }
    public string Codigo { get; }
    public bool Enfermo { get; set; }

    public void CambiarEstado()
    {
        Enfermo = !Enfermo;
    }

    public abstract double CalcularDosis(double dosisPorKg);
}
