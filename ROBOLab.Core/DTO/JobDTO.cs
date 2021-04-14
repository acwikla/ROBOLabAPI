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

        [Required]
        public string Properties { get; set; }

        [Required]
        public DeviceTypeDTO DeviceType { get; set; }
    }

    public class JobDTO : AddJobDTO
    {
        public int Id { get; set; }
    }
}
