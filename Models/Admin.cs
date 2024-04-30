using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Deneme3.Models
{
    public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public int ID { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }

    }
}
