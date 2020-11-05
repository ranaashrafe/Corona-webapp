using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaWeb.Models
{
    public class LocationModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime Date { get; set; }
        public double Speed{ get; set; }
        [ForeignKey("UserModel")]
        public int UserModelId { get; set; }
        public UserModel  UserModel { get; set; }
    }
}
