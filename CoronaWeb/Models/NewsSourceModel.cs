using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaWeb.Models
{
    public class NewsSourceModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
       
        public List<NewsModel>  NewsModels { get; set; }
        [ForeignKey("CountryModel")]
        public int CountryModelId { get; set; }
        public CountryModel CountryModel { get; set; }
    }
}
