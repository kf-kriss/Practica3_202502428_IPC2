abstract class Mascota
{
    private static int contador = 1;

    private string nombre;
    private double peso;
    private string sexo;
    private int edad;
    private string propietario;
    private string codigo;
    private bool enfermo;


    public string Nombre { get => nombre; set => nombre = value; }
    public double Peso { get => peso; set => peso = value; }
    public string Sexo { get => sexo; set => sexo = value; }
    public int Edad { get => edad; set => edad = value; }
    public string Propietario { get => propietario; set => propietario = value; }
    public string Codigo { get => codigo; }
    public bool Enfermo { get => enfermo; set => enfermo = value; }


    public Mascota(string nombre, double peso, string sexo, int edad, string propietario)
    {
        this.nombre = nombre;
        this.peso = peso;
        this.sexo = sexo;
        this.edad = edad;
        this.propietario = propietario;

        codigo = GenerarCodigo();
        enfermo = false;
    }

    private string GenerarCodigo()
    {
        string codigoGenerado = "No-" + contador.ToString("D5");
        contador++;

        return codigoGenerado;
    }

    public void CambiarEstado()
    {
        enfermo = !enfermo;
    }

    public abstract double CalcularDosis(double dosisPorKg);

    public virtual void MostrarInformacion()
    {
        Console.WriteLine("Código: " + codigo);
        Console.WriteLine("Nombre: " + nombre);
        Console.WriteLine("Peso: " + peso + " kg");
        Console.WriteLine("Sexo: " + sexo);
        Console.WriteLine("Edad: " + edad + " años");
        Console.WriteLine("Propietario: " + propietario);
        Console.WriteLine("Enfermo: " + (enfermo ? "Sí" : "No"));
    }
}