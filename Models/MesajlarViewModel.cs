using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Deneme3.Models
{
    public class MesajlarViewModel
    {
        public Kisi kisi1 { get; set; }
        public Kisi kisi2 { get; set; }
        public List<Mesajlar> mesajlars { get; set; }

    }
}
