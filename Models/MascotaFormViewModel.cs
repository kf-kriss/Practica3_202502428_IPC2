using System.ComponentModel.DataAnnotations;

namespace Practica3_V2._1_IPC2.Models;

public sealed class MascotaFormViewModel
{
    [Required(ErrorMessage = "Escribe el nombre de la mascota.")]
    [StringLength(60, ErrorMessage = "El nombre no puede superar los 60 caracteres.")]
    [Display(Name = "Nombre")]
    public string Nombre { get; set; } = "";

    [Required(ErrorMessage = "Indica el peso.")]
    [Range(typeof(decimal), "0.01", "10000", ErrorMessage = "El peso debe ser mayor que 0.")]
    [Display(Name = "Peso (kg)")]
    public decimal Peso { get; set; }

    [Required(ErrorMessage = "Selecciona el sexo.")]
    [RegularExpression("^[MF]$", ErrorMessage = "Selecciona masculino o femenino.")]
    [Display(Name = "Sexo")]
    public string Sexo { get; set; } = "";

    [Required(ErrorMessage = "Indica la edad.")]
    [Range(0, 200, ErrorMessage = "La edad debe estar entre 0 y 200 años.")]
    [Display(Name = "Edad (años)")]
    public int Edad { get; set; }

    [Required(ErrorMessage = "Escribe el nombre del propietario.")]
    [StringLength(80, ErrorMessage = "El nombre del propietario no puede superar los 80 caracteres.")]
    [Display(Name = "Propietario")]
    public string Propietario { get; set; } = "";

    [Required(ErrorMessage = "Selecciona el tipo de mascota.")]
    [RegularExpression("^(Perro|Gato|Ave|Tortuga)$", ErrorMessage = "Selecciona un tipo de mascota válido.")]
    [Display(Name = "Tipo de mascota")]
    public string Especie { get; set; } = "Perro";

    [Display(Name = "Raza")]
    public string? RazaPerro { get; set; }

    [Display(Name = "Raza")]
    public string? RazaGato { get; set; }

    [Display(Name = "Tamaño")]
    public string? Tamano { get; set; }

    [Display(Name = "Envergadura (cm)")]
    public decimal? Envergadura { get; set; }

    [Display(Name = "Tipo de caparazón")]
    public string? TipoCaparazon { get; set; }

    [Display(Name = "¿Está esterilizado?")]
    public bool? Esterilizado { get; set; }

    [Display(Name = "¿Puede volar?")]
    public bool? PuedeVolar { get; set; }

    [Display(Name = "¿Es acuática?")]
    public bool? Acuatica { get; set; }

    [Display(Name = "¿Está enferma?")]
    public bool Enfermo { get; set; }
}
