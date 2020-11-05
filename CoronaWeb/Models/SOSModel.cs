using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaWeb.Models
{
    public class SOSModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Phone { get; set; }
        [ForeignKey("CountryModel")]
        public int CountryModelId { get; set; }
    }
}
