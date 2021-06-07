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
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(9)]
        public string Phone { get; set; }
        public bool Status { get; set; }
        [Column("Date_Of_Birth", TypeName = "date")]
        public DateTime DateOfBirth { get; set; }
        [Column("Consultant_Number")]
        public int ConsultantNumber { get; set; }
        [StringLength(12)]
        public string Gender { get; set; }
        [Column("Id_Departments")]
        public int IdDepartments { get; set; }

        [ForeignKey(nameof(IdDepartments))]
        [InverseProperty(nameof(Departments.Consultants))]
        public virtual Departments IdDepartmentsNavigation { get; set; }
    }
}
