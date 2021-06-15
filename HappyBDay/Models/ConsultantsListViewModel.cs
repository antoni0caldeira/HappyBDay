using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyBDay.Models
{
    public class ConsultantsListViewModel
    {

        public List<Consultants> Consultants { get; set; }

        public Pagination Pagination { get; set; }

        public string NomePesquisar { get; set; }
    }
}
