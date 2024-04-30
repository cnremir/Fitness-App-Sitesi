using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deneme3.Models
{
    public class Uzmanlik
    {
        [Key]
        public int UzmanlıkID { get; set; }
        public string UzmanlıkAd { get; set; }
    }
}
