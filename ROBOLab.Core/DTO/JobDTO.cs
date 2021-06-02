using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ROBOLab.Core.DTO
{
    public class AddJobDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        //[Required]
        public string Properties { get; set; }

        // przy DODAWANIU jobs na pewno nie ma potrzeby podawania calego device type (albo id albo nazwa unikalna nazwa typu)
        //[Required]
        //public DeviceTypeDTO DeviceType { get; set; }

        [Required]
        public string DeviceTypeName { get; set; }
    }

    public class JobDTO : AddJobDTO
    {
        public int Id { get; set; }
    }
}
