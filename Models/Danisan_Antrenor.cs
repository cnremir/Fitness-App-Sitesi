using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Deneme3.Models
{
    public class Danisan_Antrenor
    {
        [Key]
        public int eslesmeID { get; set; }

        public int DanisanID { get; set; }
        public int AntrenorID { get; set; }




    }
}
