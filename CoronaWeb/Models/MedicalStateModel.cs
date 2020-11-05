using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaWeb.Models
{
    public class MedicalStateModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("UserModel")]
        public int UserModelId { get; set; }
        public UserModel UserModel { get; set; }
        [ForeignKey("DiseaseModel")]
        public int DiseaseModelId { get; set; }
       public DiseaseModel DiseaseModel { get; set; }
    



    }
}
