namespace Practica3_V2._1_IPC2.Models;

public sealed class Gato(
    string nombre,
    double peso,
    string sexo,
    int edad,
    string propietario,
    string raza,
    bool esterilizado) : Mascota(nombre, peso, sexo, edad, propietario)
{
    public string Raza { get; set; } = raza;
    public bool Esterilizado { get; set; } = esterilizado;

    public override double CalcularDosis(double dosisPorKg) => Peso * dosisPorKg * 0.9;
}
