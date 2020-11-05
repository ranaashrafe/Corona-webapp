using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaWeb.Models
{
    public class StatisticsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public double Infected { get; set; }
        public double Dead { get; set; }
        public double Cured { get; set; }
        public int CountryModelId { get; set; }

    }
}
