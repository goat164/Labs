using System;
using System.ComponentModel.DataAnnotations;

namespace Lab1.Models
{
    public class Car
    {
        [Display(Name = "Identyfikator")]
        public int Id { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane.")]
        [StringLength(20, ErrorMessage = "Nazwa samochodu nie moze być dłuższa niż 20 znaków.")]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [NoFutureDate(ErrorMessage = "Data produkcji nie może być w przyszłości")]
        [Display(Name = "Data Produkcji")]
        public DateTime ProductionDate { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane.")]
        [StringLength(20, ErrorMessage = "Typ tadwozia nie moze być dłuższy niż 20 znaków.")]
        [Display(Name = "Typ Nadwozia")]
        public string Type { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane.")]
        [Range(0, 9999, ErrorMessage = "Wpisana pojemność silnika jest poza zakresem.")]
        [Display(Name = "Pojemność silnika cm3")]
        public int EngineCapacity { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane.")]
        [Range(0, 2000, ErrorMessage = "Wpisana moc jest poza zakresem.")]
        [Display(Name = "Moc")]
        public int Power { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane.")]
        [Range(0, 999999, ErrorMessage = "Wpisana cena jest poza zakresem.")]
        [Display(Name = "Cena")]
        public decimal Price { get; set; }
    }

    public class NoFutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var date = Convert.ToDateTime(value);
            return date <= DateTime.Now 
                ? ValidationResult.Success 
                : new ValidationResult(ErrorMessage);
        }
    }
}
