using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Models
{
    public class SearchResult
    {
        public List<Models.Object> Objects { get; set; }
        public Paging Paging { get; set; }
    }

    public class Paging
    {
        public int AantalPaginas { get; set; }
        public int HuidigePagina { get; set; }

    }
}
