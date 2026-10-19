using Microsoft.AspNetCore.Mvc;
using Practica3_V2._1_IPC2.Models;
using Practica3_V2._1_IPC2.Services;

namespace Practica3_V2._1_IPC2.Controllers;

public sealed class MascotasController(MascotasService servicio) : Controller
{
    public IActionResult Index()
    {
        return View(servicio.ObtenerTodas());
    }

    public IActionResult Details(string id)
    {
        Mascota? mascota = servicio.Buscar(id);
        return mascota is null ? NotFound() : View(mascota);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new MascotaFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(MascotaFormViewModel formulario)
    {
        ValidarDatosEspecificos(formulario);
        if (!ModelState.IsValid)
        {
            return View(formulario);
        }

        servicio.Registrar(CrearMascota(formulario));
        TempData["Mensaje"] = $"Se registró a {formulario.Nombre}. Guarda su código para encontrarla: se muestra en el listado.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(string id)
    {
        Mascota? mascota = servicio.Buscar(id);
        if (mascota is null)
        {
            return NotFound();
        }

        return View(CrearFormulario(mascota));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(string id, MascotaFormViewModel formulario)
    {
        Mascota? existente = servicio.Buscar(id);
        if (existente is null)
        {
            return NotFound();
        }

        if (formulario.Especie != ObtenerEspecie(existente))
        {
            ModelState.AddModelError(nameof(formulario.Especie), "No se puede cambiar la especie al editar una mascota.");
        }

        ValidarDatosEspecificos(formulario);
        if (!ModelState.IsValid)
        {
            return View(formulario);
        }

        bool actualizado = servicio.Actualizar(id, mascota => AplicarCambios(mascota, formulario));
        if (!actualizado)
        {
            return NotFound();
        }

        TempData["Mensaje"] = $"Se actualizaron los datos de {formulario.Nombre}.";
        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpGet]
    public IActionResult Delete(string id)
    {
        Mascota? mascota = servicio.Buscar(id);
        return mascota is null ? NotFound() : View(mascota);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(string id)
    {
        if (!servicio.Eliminar(id))
        {
            return NotFound();
        }

        TempData["Mensaje"] = "La mascota se eliminó correctamente.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CambiarEstado(string id)
    {
        Mascota? mascota = servicio.CambiarEstado(id);
        if (mascota is null)
        {
            return NotFound();
        }

        TempData["Mensaje"] = $"{mascota.Nombre} ahora figura como {(mascota.Enfermo ? "enferma" : "sana")}.";
        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpGet]
    public IActionResult Dosis(string? id)
    {
        if (string.IsNullOrWhiteSpace(id) || servicio.Buscar(id) is not { } mascota)
        {
            return View(new DosisViewModel { Codigo = id ?? "" });
        }

        return View(new DosisViewModel { Codigo = mascota.Codigo, NombreMascota = mascota.Nombre });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Dosis(DosisViewModel formulario)
    {
        Mascota? mascota = servicio.Buscar(formulario.Codigo);
        if (mascota is null)
        {
            ModelState.AddModelError(nameof(formulario.Codigo), "No encontramos ese código. Compruébalo en el listado de mascotas.");
        }

        if (!ModelState.IsValid)
        {
            return View(formulario);
        }

        formulario.NombreMascota = mascota!.Nombre;
        formulario.DosisCalculada = mascota.CalcularDosis((double)formulario.DosisPorKg);
        return View(formulario);
    }

    private void ValidarDatosEspecificos(MascotaFormViewModel formulario)
    {
        switch (formulario.Especie)
        {
            case "Perro":
                ValidarTexto(formulario.RazaPerro, nameof(formulario.RazaPerro), "Escribe la raza del perro.");
                ValidarTexto(formulario.Tamano, nameof(formulario.Tamano), "Indica el tamaño del perro.");
                break;
            case "Gato":
                ValidarTexto(formulario.RazaGato, nameof(formulario.RazaGato), "Escribe la raza del gato.");
                ValidarRespuesta(formulario.Esterilizado, nameof(formulario.Esterilizado));
                break;
            case "Ave":
                if (formulario.Envergadura is null or < 0)
                {
                    ModelState.AddModelError(nameof(formulario.Envergadura), "Indica una envergadura de 0 cm o mayor.");
                }

                ValidarRespuesta(formulario.PuedeVolar, nameof(formulario.PuedeVolar));
                break;
            case "Tortuga":
                ValidarTexto(formulario.TipoCaparazon, nameof(formulario.TipoCaparazon), "Indica el tipo de caparazón.");
                ValidarRespuesta(formulario.Acuatica, nameof(formulario.Acuatica));
                break;
        }
    }

    private void ValidarTexto(string? valor, string campo, string mensaje)
    {
        if (string.IsNullOrWhiteSpace(valor))
        {
            ModelState.AddModelError(campo, mensaje);
        }
    }

    private void ValidarRespuesta(bool? valor, string campo)
    {
        if (valor is null)
        {
            ModelState.AddModelError(campo, "Selecciona Sí o No.");
        }
    }

    private static Mascota CrearMascota(MascotaFormViewModel formulario)
    {
        Mascota mascota = formulario.Especie switch
        {
            "Perro" => new Perro(formulario.Nombre.Trim(), (double)formulario.Peso, formulario.Sexo,
                formulario.Edad, formulario.Propietario.Trim(), formulario.RazaPerro!.Trim(), formulario.Tamano!.Trim()),
            "Gato" => new Gato(formulario.Nombre.Trim(), (double)formulario.Peso, formulario.Sexo,
                formulario.Edad, formulario.Propietario.Trim(), formulario.RazaGato!.Trim(), formulario.Esterilizado!.Value),
            "Ave" => new Ave(formulario.Nombre.Trim(), (double)formulario.Peso, formulario.Sexo,
                formulario.Edad, formulario.Propietario.Trim(), (double)formulario.Envergadura!.Value, formulario.PuedeVolar!.Value),
            "Tortuga" => new Tortuga(formulario.Nombre.Trim(), (double)formulario.Peso, formulario.Sexo,
                formulario.Edad, formulario.Propietario.Trim(), formulario.TipoCaparazon!.Trim(), formulario.Acuatica!.Value),
            _ => throw new InvalidOperationException("El tipo de mascota recibido no está permitido.")
        };

        mascota.Enfermo = formulario.Enfermo;
        return mascota;
    }

    private static MascotaFormViewModel CrearFormulario(Mascota mascota)
    {
        var formulario = new MascotaFormViewModel
        {
            Nombre = mascota.Nombre,
            Peso = (decimal)mascota.Peso,
            Sexo = mascota.Sexo,
            Edad = mascota.Edad,
            Propietario = mascota.Propietario,
            Especie = ObtenerEspecie(mascota),
            Enfermo = mascota.Enfermo
        };

        switch (mascota)
        {
            case Perro perro:
                formulario.RazaPerro = perro.Raza;
                formulario.Tamano = perro.Tamano;
                break;
            case Gato gato:
                formulario.RazaGato = gato.Raza;
                formulario.Esterilizado = gato.Esterilizado;
                break;
            case Ave ave:
                formulario.Envergadura = (decimal)ave.Envergadura;
                formulario.PuedeVolar = ave.PuedeVolar;
                break;
            case Tortuga tortuga:
                formulario.TipoCaparazon = tortuga.TipoCaparazon;
                formulario.Acuatica = tortuga.Acuatica;
                break;
        }

        return formulario;
    }

    private static string ObtenerEspecie(Mascota mascota) => mascota switch
    {
        Perro => "Perro",
        Gato => "Gato",
        Ave => "Ave",
        Tortuga => "Tortuga",
        _ => throw new InvalidOperationException("La mascota tiene un tipo no reconocido.")
    };

    private static void AplicarCambios(Mascota mascota, MascotaFormViewModel formulario)
    {
        mascota.Nombre = formulario.Nombre.Trim();
        mascota.Peso = (double)formulario.Peso;
        mascota.Sexo = formulario.Sexo;
        mascota.Edad = formulario.Edad;
        mascota.Propietario = formulario.Propietario.Trim();
        mascota.Enfermo = formulario.Enfermo;

        switch (mascota)
        {
            case Perro perro:
                perro.Raza = formulario.RazaPerro!.Trim();
                perro.Tamano = formulario.Tamano!.Trim();
                break;
            case Gato gato:
                gato.Raza = formulario.RazaGato!.Trim();
                gato.Esterilizado = formulario.Esterilizado!.Value;
                break;
            case Ave ave:
                ave.Envergadura = (double)formulario.Envergadura!.Value;
                ave.PuedeVolar = formulario.PuedeVolar!.Value;
                break;
            case Tortuga tortuga:
                tortuga.TipoCaparazon = formulario.TipoCaparazon!.Trim();
                tortuga.Acuatica = formulario.Acuatica!.Value;
                break;
        }
    }
}
