namespace Practica3_V2._1_IPC2.Models;

public sealed class Ave(
    string nombre,
    double peso,
    string sexo,
    int edad,
    string propietario,
    double envergadura,
    bool puedeVolar) : Mascota(nombre, peso, sexo, edad, propietario)
{
    public double Envergadura { get; set; } = envergadura;
    public bool PuedeVolar { get; set; } = puedeVolar;

    public override double CalcularDosis(double dosisPorKg) => Peso * dosisPorKg * 0.5;
}
