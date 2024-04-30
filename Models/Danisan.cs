using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Deneme3.Models
{
    public class Danisan : Kisi
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]

        public int DanisanID { get; set; }
        public int Kg { get; set; }
        public int Boy { get; set; }
        public string Hedef { get; set; }

        public int GunlukKaloriIhtiyaci { get; set; }
        public int GunlukYaktigiKalori { get; set; }
        [ForeignKey("KisiId")]
        public Kisi Kisi { get; set; }
        
    }
}
