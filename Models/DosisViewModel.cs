using System.ComponentModel.DataAnnotations;

namespace Practica3_V2._1_IPC2.Models;

public sealed class DosisViewModel
{
    [Required]
    public string Codigo { get; set; } = "";

    [Required(ErrorMessage = "Indica la dosis recomendada por kilogramo.")]
    [Range(typeof(decimal), "0", "1000000", ErrorMessage = "La dosis debe ser 0 o mayor.")]
    [Display(Name = "Dosis indicada (mg/kg)")]
    public decimal DosisPorKg { get; set; }

    public string? NombreMascota { get; set; }
    public double? DosisCalculada { get; set; }
}
