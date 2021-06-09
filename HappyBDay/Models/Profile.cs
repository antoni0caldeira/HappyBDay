using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace HappyBDay.Models
{
    public partial class Profile
    {
        public Profile()
        {
            Users = new HashSet<Users>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Required field.")]
        [StringLength(100)]
        public string Name { get; set; }

        [InverseProperty("IdProfileNavigation")]
        public virtual ICollection<Users> Users { get; set; }
    }
}
