using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deneme3.Models
{
    public class Plan 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int planID { get; set; }
        public string planAdi { get; set; }
        public string planHedefi { get; set; }
        public DateTime baslangicTarihi { get; set; }
        public int sureHafta { get; set; }
        public string planDetay { get; set; }
        
        public int olusturanKisiID { get; set; }
        public int atananDanisanID { get; set; }
        public string atananDanisanAdSoyadi { get; set; }
        public string planTuru { get; set; }


    }
}
