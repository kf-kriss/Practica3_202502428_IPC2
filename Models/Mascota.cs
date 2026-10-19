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

    public static double? IntentarObtenerFactorDosis(string especie) => especie switch
    {
        "Perro" => 1.0,
        "Gato" => 0.9,
        "Ave" => 0.5,
        "Tortuga" => 0.8,
        _ => null
    };

    public static double ObtenerFactorDosis(string especie) =>
        IntentarObtenerFactorDosis(especie)
        ?? throw new ArgumentOutOfRangeException(nameof(especie), "El tipo de mascota no es válido.");

    public static double CalcularDosisPorTipo(string especie, double peso, double dosisPorKg)
    {
        return peso * dosisPorKg * ObtenerFactorDosis(especie);
    }
}
