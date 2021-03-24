using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ROBOLab.Core.DTO
{
    public class DeviceToViewDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
