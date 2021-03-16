using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ROBOLab.Core.Models
{
    public class Job
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string JobProperties { get; set; }

        [Required]
        public int DeviceTyoe { get; set; }

        [Required]
        public DeviceType DeviceType { get; set; }
    }
}
