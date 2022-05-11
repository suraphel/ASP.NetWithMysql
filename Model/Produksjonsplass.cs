using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mysqltest.Model
{
    public class Produksjonsplass
    {

        public int produksjonsplassid { get; set; }
        public string? kommunenummer { get; set; }
        public int? gaardsnummer { get; set; }
        public int bruksnummer { get; set; }
        public int bygningsnummer { get; set; }
        public string koordinater { get; set; }
        public string koordinatsystem { get; set; }
        public DateTime opprettetdato { get; set; }
        public DateTime lastchanged { get; set; }
    }
}
