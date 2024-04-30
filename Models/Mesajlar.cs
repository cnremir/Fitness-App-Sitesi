using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Deneme3.Models
{
    public class Mesajlar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int AliciID { get; set; }
        public int GonderenID { get; set; }
        public string Mesaj { get; set; }
        public DateTime Tarih { get; set; }

    }
}
