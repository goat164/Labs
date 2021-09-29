using System;
using System.ComponentModel.DataAnnotations;

namespace AdvancedProgramming_Lesson2.Models
{
    public class Employee
    {
        [Required(ErrorMessage = "To pole jest wymagane.")]
        [Display(Name = "Identyfikator")]
        public int Id { get; set; }

        [Display(Name = "Imię")]
        [StringLength(30, ErrorMessage = "Imię pracownika nie moze być dłuższe niż 30 znaków.")]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        [StringLength(50, ErrorMessage = "Nazwisko pracownika nie moze być dłuższe niż 50 znaków.")]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string LastName { get; set; }

        [Display(Name = "Numer Pesel")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Numer pesel jest niepoprawny")]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public long InsuranceNumber { get; set; }

        [Display(Name = "Stanowisko")]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string Position { get; set; }

        [Display(Name = "Pensja Miesięczna")]
        [Range(0, Double.MaxValue, ErrorMessage = "Pensja poza zakresem.")]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public Decimal Salary { get; set; }

        [Display(Name = "Ilość Ostrzeżeń")]
        [Range(0, Double.MaxValue, ErrorMessage = "Ilość ostrzeżeń poza zakresem.")]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public int WarningsCount { get; set; }
        
        [Display(Name = "Zatrudniony od")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public DateTime EmployedSince { get; set; }
    }
}