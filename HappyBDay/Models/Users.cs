using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace HappyBDay.Models
{
    public partial class Users
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Required field.")]
        [StringLength(50)]
       
        public string Username { get; set; }

        [Column("Id_Profile")]
        [Required(ErrorMessage ="Required field.")]
        [Display(Name = "Profile")]
        public int IdProfile { get; set; }

        [ForeignKey(nameof(IdProfile))]
        [InverseProperty(nameof(Profile.Users))]
        [Display(Name = "Profile")]
        public virtual Profile IdProfileNavigation { get; set; }
    }
}
