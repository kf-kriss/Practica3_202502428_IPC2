namespace Practica3_V2._1_IPC2.Models;

public sealed class Perro(
    string nombre,
    double peso,
    string sexo,
    int edad,
    string propietario,
    string raza,
    string tamano) : Mascota(nombre, peso, sexo, edad, propietario)
{
    public string Raza { get; set; } = raza;
    public string Tamano { get; set; } = tamano;

    public override double CalcularDosis(double dosisPorKg) => Peso * dosisPorKg;
}
