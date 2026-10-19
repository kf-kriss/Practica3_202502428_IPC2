class Gato : Mascota
{
    private string raza;
    private bool esterilizado;


    public string Raza { get => raza; set => raza = value; }
    public bool Esterilizado { get => esterilizado; set => esterilizado = value; }


    public Gato(string nombre, double peso, string sexo, int edad, string propietario, string raza, bool esterilizado)
        : base(nombre, peso, sexo, edad, propietario)
    {
        this.raza = raza;
        this.esterilizado = esterilizado;
    }

    public override double CalcularDosis(double dosisPorKg)
    {
        return Peso * dosisPorKg * 0.9;
    }

    public override void MostrarInformacion()
    {
        base.MostrarInformacion();
        Console.WriteLine("Tipo: Gato");
        Console.WriteLine("Raza: " + raza);
        Console.WriteLine("Esterilizado: " + (esterilizado ? "Sí" : "No"));
    }
}