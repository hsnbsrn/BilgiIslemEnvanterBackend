using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crud1.Models
{
    public class Personel
    {
        public int Id { get; set; }
        public string Adsoyad { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public int DepartmanId { get; set; }
        public string Departman { get; set; }
    }
}
