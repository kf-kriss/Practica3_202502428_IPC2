namespace Practica3_V2._1_IPC2.Models;

public sealed class Tortuga(
    string nombre,
    double peso,
    string sexo,
    int edad,
    string propietario,
    string tipoCaparazon,
    bool acuatica) : Mascota(nombre, peso, sexo, edad, propietario)
{
    public string TipoCaparazon { get; set; } = tipoCaparazon;
    public bool Acuatica { get; set; } = acuatica;

    public override double CalcularDosis(double dosisPorKg) => Mascota.CalcularDosisPorTipo("Tortuga", Peso, dosisPorKg);
}
