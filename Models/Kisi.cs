using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Deneme3.Models
{
    public class Kisi
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int KisiId { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }

        public string FullAdi => $"{Adi} {Soyadi}";
        public string Email { get; set; }
        public string DogumTarihi { get; set; }
        public string TelNumarasi { get; set; }
        public string Cinsiyet { get; set; }
        public string Yas { get; set; }

        public string ProfilFotografiYolu { get; set; }

        
        

        
    }
}
