using System.ComponentModel.DataAnnotations;

namespace Practica3_V2._1_IPC2.Models;

public sealed class DosisViewModel
{
    [Required(ErrorMessage = "Selecciona una especie.")]
    [RegularExpression("^(Perro|Gato|Ave|Tortuga)$", ErrorMessage = "Selecciona una especie válida.")]
    [Display(Name = "Especie")]
    public string Especie { get; set; } = "";

    [Required(ErrorMessage = "Indica el peso de la mascota.")]
    [Range(typeof(decimal), "0.01", "10000", ErrorMessage = "El peso debe ser mayor que 0.")]
    [Display(Name = "Peso de la mascota (kg)")]
    public decimal Peso { get; set; }

    [Required(ErrorMessage = "Indica la dosis recomendada por kilogramo.")]
    [Range(typeof(decimal), "0", "1000000", ErrorMessage = "La dosis debe ser 0 o mayor.")]
    [Display(Name = "Dosis indicada (mg/kg)")]
    public decimal DosisPorKg { get; set; }

    public double? DosisCalculada { get; set; }
    public double? FactorDosis => Mascota.IntentarObtenerFactorDosis(Especie);
}
