using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaWeb.Models
{
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid GuidId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public string Image { get; set; }
        public string Phone { get; set; }
        public string Language { get; set; }
        public bool InfictionStatus { get; set; }

        public List<MedicalStateModel>  MedicalStateModels { get; set; }

        public List<LocationModel>  LocationModels { get; set; }
        [ForeignKey("CountryModel")]
        public int  CountryModelId { get; set; }

    }
}
