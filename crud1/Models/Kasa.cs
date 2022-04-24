using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crud1.Models
{
    public class Kasa
    {
        public int Id { get; set; }
        public string EkranKarti { get; set; }
        public string Islemci { get; set; }
        public string Anakart { get; set; }
        public string Hdmi { get; set; }
        public string Vga { get; set; }
        public string Dvi { get; set; }
        public int Cihazid { get; set; }
        public int Konumid { get; set; }
        public string Ip { get; set; }
    }
}
