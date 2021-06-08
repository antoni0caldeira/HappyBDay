using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace HappyBDay.Models
{
    public partial class Departments
    {
        public Departments()
        {
            Consultants = new HashSet<Consultants>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Required field.")]
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty("IdDepartmentsNavigation")]
        public virtual ICollection<Consultants> Consultants { get; set; }
    }
}
