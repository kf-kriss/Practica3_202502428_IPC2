class Ave : Mascota
{
    private double envergadura;
    private bool puedeVolar;


    public double Envergadura { get => envergadura; set => envergadura = value; }
    public bool PuedeVolar { get => puedeVolar; set => puedeVolar = value; }


    public Ave(string nombre, double peso, string sexo, int edad, string propietario, double envergadura, bool puedeVolar)
        : base(nombre, peso, sexo, edad, propietario)
    {
        this.envergadura = envergadura;
        this.puedeVolar = puedeVolar;
    }

    public override double CalcularDosis(double dosisPorKg)
    {
        return Peso * dosisPorKg * 0.5;
    }

    public override void MostrarInformacion()
    {
        base.MostrarInformacion();
        Console.WriteLine("Tipo: Ave");
        Console.WriteLine("Envergadura: " + envergadura);
        Console.WriteLine("Puede volar: " + (puedeVolar ? "Sí" : "No"));
    }
}