using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaWeb.Models
{
    public class NewsModel
    {
        [Key]
     
        public int Id { get; set; }
     
        public string Title { get; set; }
        public string Text { get; set; }
        public string Detail { get; set; }
        public string Image { get; set; }

        public string  Date { get; set; }
       // [ForeignKey("NewsSourceModel")]
        public int NewsSourceModelId { get; set; } 
        public NewsSourceModel NewsSourceModel { get; set; }
    }
     
}
