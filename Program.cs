using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        List<Mascota> mascotas = new();

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("========================================");
            Console.WriteLine("       SISTEMA DE VETERINARIA");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Registrar una mascota");
            Console.WriteLine("2. Ver todas las mascotas");
            Console.WriteLine("3. Buscar una mascota por código");
            Console.WriteLine("4. Actualizar su estado de salud");
            Console.WriteLine("5. Calcular dosis para una mascota");
            Console.WriteLine("6. Salir");
            Console.WriteLine("----------------------------------------");

            Console.Write("Elige una opción (1-6): ");
            string? entrada = Console.ReadLine();

            if (entrada is null)
            {
                break;
            }

            if (!int.TryParse(entrada, out int opcion) || opcion < 1 || opcion > 6)
            {
                Console.WriteLine("Esa opción no existe. Escribe un número del 1 al 6.");
                Pausar();
                continue;
            }

            switch (opcion)
            {
                case 1:
                    RegistrarMascota(mascotas);
                    break;
                case 2:
                    MostrarMascotas(mascotas);
                    break;
                case 3:
                    BuscarMascota(mascotas);
                    break;
                case 4:
                    ActualizarEstadoMascota(mascotas);
                    break;
                case 5:
                    CalcularDosisMascota(mascotas);
                    break;
                case 6:
                    Console.WriteLine("Gracias por usar el sistema. ¡Hasta pronto!");
                    return;
            }
        }
    }

    static void RegistrarMascota(List<Mascota> mascotas)
    {
        Console.WriteLine();
        Console.WriteLine("=== Registro de mascota ===");

        string? nombre = LeerTextoObligatorio("Nombre de la mascota: ");
        if (nombre is null) return;

        double? peso = LeerDecimal("Peso en kg (mayor que 0): ", valor => valor > 0);
        if (peso is null) return;

        string? sexo = LeerSexo();
        if (sexo is null) return;

        int? edad = LeerEntero("Edad en años (0 o más): ", valor => valor >= 0);
        if (edad is null) return;

        string? propietario = LeerTextoObligatorio("Nombre del propietario: ");
        if (propietario is null) return;

        Console.WriteLine();
        Console.WriteLine("¿Qué tipo de mascota vas a registrar?");
        Console.WriteLine("1. Perro");
        Console.WriteLine("2. Gato");
        Console.WriteLine("3. Ave");
        Console.WriteLine("4. Tortuga");
        int? tipo = LeerEntero("Elige una opción (1-4): ", valor => valor >= 1 && valor <= 4);
        if (tipo is null) return;

        Mascota mascota;
        switch (tipo.Value)
        {
            case 1:
            {
                string? raza = LeerTextoObligatorio("Raza del perro: ");
                if (raza is null) return;

                string? tamano = LeerTextoObligatorio("Tamaño (pequeño, mediano o grande): ");
                if (tamano is null) return;

                mascota = new Perro(nombre, peso.Value, sexo, edad.Value, propietario, raza, tamano);
                break;
            }
            case 2:
            {
                string? raza = LeerTextoObligatorio("Raza del gato: ");
                if (raza is null) return;

                bool? esterilizado = LeerSiNo("¿Está esterilizado? (S/N): ");
                if (esterilizado is null) return;

                mascota = new Gato(nombre, peso.Value, sexo, edad.Value, propietario, raza, esterilizado.Value);
                break;
            }
            case 3:
            {
                double? envergadura = LeerDecimal("Envergadura de las alas en cm (0 o más): ", valor => valor >= 0);
                if (envergadura is null) return;

                bool? puedeVolar = LeerSiNo("¿Puede volar? (S/N): ");
                if (puedeVolar is null) return;

                mascota = new Ave(nombre, peso.Value, sexo, edad.Value, propietario, envergadura.Value, puedeVolar.Value);
                break;
            }
            default:
            {
                string? tipoCaparazon = LeerTextoObligatorio("Tipo de caparazón: ");
                if (tipoCaparazon is null) return;

                bool? acuatica = LeerSiNo("¿Es acuática? (S/N): ");
                if (acuatica is null) return;

                mascota = new Tortuga(nombre, peso.Value, sexo, edad.Value, propietario, tipoCaparazon, acuatica.Value);
                break;
            }
        }

        bool? enferma = LeerSiNo("¿Está enferma actualmente? (S/N): ");
        if (enferma is null) return;

        mascota.Enfermo = enferma.Value;
        mascotas.Add(mascota);

        Console.WriteLine();
        Console.WriteLine("¡Registro completado!");
        Console.WriteLine($"Código de la mascota: {mascota.Codigo}");
        Console.WriteLine("Guarda este código: lo necesitarás para buscarla o actualizar su estado.");
        Pausar();
    }

    static void MostrarMascotas(List<Mascota> mascotas)
    {
        Console.WriteLine();
        Console.WriteLine("=== Mascotas registradas ===");

        if (mascotas.Count == 0)
        {
            Console.WriteLine("Todavía no hay mascotas registradas.");
        }
        else
        {
            foreach (Mascota mascota in mascotas)
            {
                Console.WriteLine("----------------------------------------");
                mascota.MostrarInformacion();
            }

            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"Total: {mascotas.Count} mascota(s).");
        }

        Pausar();
    }

    static void BuscarMascota(List<Mascota> mascotas)
    {
        Console.WriteLine();
        Console.WriteLine("=== Buscar mascota ===");

        Mascota? mascota = LeerMascotaPorCodigo(mascotas);
        if (mascota is not null)
        {
            Console.WriteLine();
            mascota.MostrarInformacion();
        }

        Pausar();
    }

    static void ActualizarEstadoMascota(List<Mascota> mascotas)
    {
        Console.WriteLine();
        Console.WriteLine("=== Actualizar estado de salud ===");

        Mascota? mascota = LeerMascotaPorCodigo(mascotas);
        if (mascota is null)
        {
            Pausar();
            return;
        }

        Console.WriteLine($"Estado actual: {(mascota.Enfermo ? "Enferma" : "Sana")}");
        Console.WriteLine("1. Marcar como sana");
        Console.WriteLine("2. Marcar como enferma");
        int? opcion = LeerEntero("Elige el nuevo estado (1-2): ", valor => valor == 1 || valor == 2);
        if (opcion is null) return;

        mascota.Enfermo = opcion.Value == 2;
        Console.WriteLine($"Estado actualizado: {(mascota.Enfermo ? "Enferma" : "Sana")}.");
        Pausar();
    }

    static void CalcularDosisMascota(List<Mascota> mascotas)
    {
        Console.WriteLine();
        Console.WriteLine("=== Calcular dosis ===");
        Console.WriteLine("El cálculo usará la especie y el peso guardados en el registro.");

        Mascota? mascota = LeerMascotaPorCodigo(mascotas);
        if (mascota is null)
        {
            Pausar();
            return;
        }

        double? dosisPorKg = LeerDecimal("Dosis indicada por kg (mg/kg, 0 o más): ", valor => valor >= 0);
        if (dosisPorKg is null) return;

        double dosis = mascota.CalcularDosis(dosisPorKg.Value);
        Console.WriteLine();
        Console.WriteLine($"Dosis calculada para {mascota.Nombre} ({mascota.Codigo}): {dosis.ToString("0.##", CultureInfo.CurrentCulture)} mg.");
        Console.WriteLine("Este resultado es solo para la práctica; no sustituye la indicación de un veterinario.");
        Pausar();
    }

    static Mascota? LeerMascotaPorCodigo(List<Mascota> mascotas)
    {
        if (mascotas.Count == 0)
        {
            Console.WriteLine("Aún no hay mascotas registradas. Registra una primero desde la opción 1.");
            return null;
        }

        string? codigo = LeerTextoObligatorio("Escribe el código de la mascota (por ejemplo, No-00001): ");
        if (codigo is null) return null;

        Mascota? mascota = mascotas.Find(
            elemento => string.Equals(elemento.Codigo, codigo, StringComparison.OrdinalIgnoreCase));

        if (mascota is null)
        {
            Console.WriteLine($"No encontramos una mascota con el código \"{codigo}\".");
            Console.WriteLine("Revisa el código en «Ver todas las mascotas» e inténtalo de nuevo.");
        }

        return mascota;
    }

    static string? LeerTextoObligatorio(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            string? entrada = Console.ReadLine();
            if (entrada is null) return null;

            entrada = entrada.Trim();
            if (entrada.Length > 0) return entrada;

            Console.WriteLine("Este dato no puede quedar vacío. Inténtalo de nuevo.");
        }
    }

    static string? LeerSexo()
    {
        while (true)
        {
            string? entrada = LeerTextoObligatorio("Sexo (M/F o Masculino/Femenino): ");
            if (entrada is null) return null;

            if (entrada.Equals("M", StringComparison.OrdinalIgnoreCase) ||
                entrada.Equals("Masculino", StringComparison.OrdinalIgnoreCase))
            {
                return "M";
            }

            if (entrada.Equals("F", StringComparison.OrdinalIgnoreCase) ||
                entrada.Equals("Femenino", StringComparison.OrdinalIgnoreCase))
            {
                return "F";
            }

            Console.WriteLine("Respuesta no válida. Escribe M para masculino o F para femenino.");
        }
    }

    static bool? LeerSiNo(string mensaje)
    {
        while (true)
        {
            string? entrada = LeerTextoObligatorio(mensaje);
            if (entrada is null) return null;

            if (entrada.Equals("S", StringComparison.OrdinalIgnoreCase) ||
                entrada.Equals("SI", StringComparison.OrdinalIgnoreCase) ||
                entrada.Equals("SÍ", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (entrada.Equals("N", StringComparison.OrdinalIgnoreCase) ||
                entrada.Equals("NO", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            Console.WriteLine("Respuesta no válida. Escribe S para sí o N para no.");
        }
    }

    static int? LeerEntero(string mensaje, Func<int, bool> esValido)
    {
        while (true)
        {
            Console.Write(mensaje);
            string? entrada = Console.ReadLine();
            if (entrada is null) return null;

            if (int.TryParse(entrada, out int valor) && esValido(valor))
            {
                return valor;
            }

            Console.WriteLine("El valor no es válido. Revisa el rango indicado e inténtalo de nuevo.");
        }
    }

    static double? LeerDecimal(string mensaje, Func<double, bool> esValido)
    {
        while (true)
        {
            Console.Write(mensaje);
            string? entrada = Console.ReadLine();
            if (entrada is null) return null;

            bool sePudoLeer = double.TryParse(
                entrada,
                NumberStyles.Float,
                CultureInfo.CurrentCulture,
                out double valor);

            if (sePudoLeer && double.IsFinite(valor) && esValido(valor))
            {
                return valor;
            }

            string separador = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            Console.WriteLine($"Número no válido. Usa el separador decimal \"{separador}\" y revisa el rango.");
        }
    }

    static void Pausar()
    {
        Console.WriteLine();
        Console.Write("Presiona Enter para volver al menú...");
        Console.ReadLine();
    }
}
