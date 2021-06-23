using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HappyBDay.Models
{
    public class EmailModel
    {
        [Required, Display(Name = "Email de destino"), EmailAddress]
        public string Destination { get; set; }

        [Required, Display(Name = "Assunto")]
        public string Subject { get; set; }

        [Required, Display(Name = "Mensagem")]
        public string Message { get; set; }
    }
}