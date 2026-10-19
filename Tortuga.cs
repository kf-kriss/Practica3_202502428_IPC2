class Tortuga : Mascota
{
    private string tipoCaparazon;
    private bool acuatica;


    public string TipoCaparazon { get => tipoCaparazon; set => tipoCaparazon = value; }
    public bool Acuatica { get => acuatica; set => acuatica = value; }

    public Tortuga(string nombre, double peso, string sexo, int edad, string propietario, string tipoCaparazon, bool acuatica)
        : base(nombre, peso, sexo, edad, propietario)
    {
        this.tipoCaparazon = tipoCaparazon;
        this.acuatica = acuatica;
    }

    public override double CalcularDosis(double dosisPorKg)
    {
        return Peso * dosisPorKg * 0.8;
    }

    public override void MostrarInformacion()
    {
        base.MostrarInformacion();
        Console.WriteLine("Tipo: Tortuga");
        Console.WriteLine("Tipo de caparazón: " + tipoCaparazon);
        Console.WriteLine("Acuática: " + (acuatica ? "Sí" : "No"));
    }
}