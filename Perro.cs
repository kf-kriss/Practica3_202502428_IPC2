class Perro : Mascota
{
    private string raza;
    private string tamaño;


    public string Raza { get => raza; set => raza = value; }
    public string Tamaño { get => tamaño; set => tamaño = value; }


    public Perro(string nombre, double peso, string sexo, int edad, string propietario, string raza, string tamaño)
        : base(nombre, peso, sexo, edad, propietario)
    {
        this.raza = raza;
        this.tamaño = tamaño;
    }


    public override double CalcularDosis(double dosisPorKg)
    {
        return Peso * dosisPorKg;
    }

    public override void MostrarInformacion()
    {
        base.MostrarInformacion();
        Console.WriteLine("Tipo: Perro");
        Console.WriteLine("Raza: " + raza);
        Console.WriteLine("Tamaño: " + tamaño);
    }
}