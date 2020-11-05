using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaWeb.Models
{
    public class CountryModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Flag { get; set; }
        public  string Language { get; set; }
        [ForeignKey("StatisticsModel")]
        public int StatisticsModelId { get; set; }
       public StatisticsModel StatisticsModel { get; set; }
        [ForeignKey("SOSModel")]
        public List<SOSModel> SOSModels { get; set; }
        [ForeignKey("UserModel")]
        public List <UserModel> UserModels { get; set; }

        public List<NewsSourceModel>  NewsSourceModels { get; set; }

    }
}
