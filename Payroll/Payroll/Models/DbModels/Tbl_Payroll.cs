using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Payroll.Models.DbModels
{
    public class Tbl_Payroll
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [MaxLength(30), Required]
        [Display(Name = "Tipo")]
        public string Role { get; set; }

        [Required]
        [Display(Name = "Seccion")]
        [RegularExpression("^[0-9]*$",ErrorMessage = "Numeros enteros solamente")]
        public Int16 Section { get; set; }

        [Required, MaxLength(30), MinLength(2)]
        [Display(Name = "Nombres")]
        public string Name { get; set; }

        [Required, MaxLength(30), MinLength(2)]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }


        [Required]
        [Display(Name = "Horas")]
        public decimal Hours { get; set; }

        [Required]
        [Display(Name = "Importe")]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "Creado")]
        public DateTime Created { get; set; }

        [Required]
        [Display(Name = "Modificado")]
        public DateTime Modified { get; set; }

        [Required]
        [Display(Name = "Eliminado")]
        public bool Deleted { get; set; }
    }
}