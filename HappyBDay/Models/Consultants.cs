using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace HappyBDay.Models
{
    public partial class Consultants
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required field.")]
        [StringLength(100, ErrorMessage = "The characters limit was exceeded(100).")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required field.")]
        [EmailAddress(ErrorMessage = "Incorrect email adress.")]
        [StringLength(100, ErrorMessage = "The characters limit was exceeded(100).")] //Talvez mudar para 255 caracteres
        public string Email { get; set; }

        [Required(ErrorMessage = "Required field.")] // Talvez alterar para nullable
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Phone number must have 9 digits.")]
        [RegularExpression(@"(9[1236]|2\d)\d{7}", ErrorMessage = "Incorrect phone number.")]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        public bool Status { get; set; }

        public int Age { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage ="Required field.")]
        [Column("Date_Of_Birth", TypeName = "date")]
        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }

        [Column("Consultant_Number")]
        [Display(Name = "Consultant Number")]
        [RegularExpression(@"\d{8}",ErrorMessage ="Invalid value")]
        public string ConsultantNumber { get; set; }

        [StringLength(12)]
        public string Gender { get; set; }

        [Column("Id_Departments")]
        [Required]
        [Display(Name = "Name of the departement")]
        public int IdDepartments { get; set; }

        [ForeignKey(nameof(IdDepartments))]
        [InverseProperty(nameof(Departments.Consultants))]
        [Display(Name = "Name of the departement")]
        public virtual Departments IdDepartmentsNavigation { get; set; }
    }
}
