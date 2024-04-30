using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Deneme3.Models
{
    public class IlerlemeKayit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int KayıtId { get; set; }
        public int Kilo { get; set; }
        public int Boy { get; set; }
        public int VücutYagOrani { get; set; }
        public int KasKütlesi { get; set; }
        public int VücutKitleIndexi { get; set; }

        public int DanisanID { get; set; } // Eşleşen özellik

        [ForeignKey("DanisanID")]
        public Danisan Danisan { get; set; }
        public DateTime time { get; set; }
    }

}
