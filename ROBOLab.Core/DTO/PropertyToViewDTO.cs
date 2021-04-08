using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ROBOLab.Core.DTO
{
    public class PropertyToViewDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public DeviceTypeDTO DeviceType { get; set; }

        public bool IsMode { get; set; }
    }
}
