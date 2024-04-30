using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Deneme3.Models
{
    public class Antrenor : Kisi
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]

        public int AntrenorID { get; set; }
       
        public string UzmanlikAlani { get; set; }
        public string Deneyimleri { get; set; }


        public int danisanSayisi { get; set; }
        [ForeignKey("KisiId")]
        public Kisi Kisi { get; set; }
        
    }

    
}
