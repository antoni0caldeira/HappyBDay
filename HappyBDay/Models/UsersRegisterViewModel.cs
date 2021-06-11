using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HappyBDay.Models
{
    public class UsersRegisterViewModel
    {
        [Required(ErrorMessage = "Required field.")]
        [StringLength(50)]
        public string Username { get; set; }

        [Column("Id_Profile")]
        [Required(ErrorMessage = "Required field.")]
        public int IdProfile { get; set; }

        [Required(ErrorMessage = "Required field.")]
        [EmailAddress]
        [StringLength(256)]
        public string Email { get; set; }


        [Required(ErrorMessage = "Required field.")]
        [StringLength(256)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Required field.")]
        [StringLength(256)]
        [Compare("Password",ErrorMessage ="Not the same password")]
        [Display(Name = "Confirm your password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }


    }
}
