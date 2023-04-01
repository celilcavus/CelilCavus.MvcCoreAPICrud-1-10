using CelilCavus.Validator;

namespace CelilCavus.Models.Entites
{
   
    public class Dersler
    {
        public int id { get; set; }
        public string ders_kodu { get; set; }
        public string ders_adi { get; set; }
        public string ders_hocasi { get; set; }
        public string ders_donemi { get; set; }
    }
}